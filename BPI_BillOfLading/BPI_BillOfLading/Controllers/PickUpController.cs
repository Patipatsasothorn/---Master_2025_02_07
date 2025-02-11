using BPI_BillOfLading.Models;
using BPI_BillOfLading.Models.Data;
using BPI_BillOfLading.Models.PickUpModel;
using BPI_BillOfLading.Models.StockOutModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using QRCoder;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Globalization;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BPI_BillOfLading.Controllers
{
    public class PickUpController : Controller
    {
        private readonly BpiLiveContext _context;
        private readonly BpigContext _pigContext;

        public PickUpController(BpiLiveContext context, BpigContext bpigContext)
        {
            _context = context;
            _pigContext = bpigContext;
        }

        public IActionResult Index(string Company, string UserName)
        {
            //var company = HttpContext.Session.GetString("Company");
            //var username = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(UserName))
            {
                return Redirect("https://webapp.bpi-concretepile.co.th:14002/Account/Login");
            }

            HttpContext.Session.SetString("Company", Company);
            HttpContext.Session.SetString("User", UserName);

            return RedirectToAction("RenderLoadingScreen");
        }

        public IActionResult RenderLoadingScreen()
        {
            // ดึงค่าจาก Session
            var company = HttpContext.Session.GetString("Company");
            var user = HttpContext.Session.GetString("User");

            // ตรวจสอบว่าข้อมูลใน Session ถูกต้อง
            if (string.IsNullOrWhiteSpace(company) || string.IsNullOrWhiteSpace(user))
            {
                return Redirect("https://webapp.bpi-concretepile.co.th:14002/#/authen");
            }

            ViewBag.Company = company;
            ViewBag.Username = user;

            //ViewData["Company"] = company;
            //ViewData["User"] = user;

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetFormData(string username, string company)
        {
            var getId = await _pigContext.UserRights
                    .Where(u => u.UserName == username)
                    .Select(u => u.UserId)
                    .SingleOrDefaultAsync();

            var getPlant = await _context.Plants
                .Select(i => i.Plant1)
                .Distinct()
                .ToListAsync();

            var getReasonCode = await _pigContext.BolUsersPolicies
                .Where(i => i.DataType == "REA" && i.UserId == getId)
                .Select(i => i.DataCode)
                .ToListAsync();

            var getReasonDesc = await _context.Reasons
                .Where(i => getReasonCode.Contains(i.ReasonCode) && i.Company == company)
                .Select(i => new { Value = i.ReasonCode, Text = i.Description })
                .ToListAsync();

            var getDep = await (from u in _pigContext.BolUsersPolicies
                                join d in _pigContext.Deps on u.DataCode equals d.DepCode
                                where u.UserId == getId && u.DataType == "DEP"
                                select new
                                {
                                    Value = d.DepCode,
                                    Text = d.DepName
                                })
                                .ToListAsync();

            return Json(new
            {
                success = true,
                plant = getPlant.Select(p => new { Value = p, Text = p }).ToList(),
                reason = getReasonDesc,
                dep = getDep
            });
        }

        [HttpGet]
        public IActionResult GetPartNum(string partNum, string company)
        {
            var query = from p in _context.Parts
                        join u in _context.Uoms
                        on new { p.Company, UomCode = p.Ium } equals new { u.Company, UomCode = u.Uomcode } into uJoin
                        from u in uJoin.DefaultIfEmpty() // LEFT JOIN
                        where p.Company == company && p.PartNum == partNum
                        select new
                        {
                            PartNum = p.PartNum,
                            PartDescription = p.PartDescription,
                            UOMCode = u.Uomcode,
                            UOMDesc = u.Uomdesc
                        };

            var searchPart = query.ToList();


            return Json(new { success = true, searchPart });
        }

        /*[HttpGet]
        public IActionResult GetBin(string selectedWarehouse, string company)
        {
            var searchBin = _context.WhseBins
                .Where(b => b.Company == company && b.WarehouseCode == selectedWarehouse)
                .Select(b =>  new
                {
                    b.BinNum,
                    b.Description
                })
                .ToList();

            if (searchBin.Count > 0)
            {
                return Json(new { success = true, searchBin });
            }
            else
            {
                return Json(new { success = false });
            }
        }*/

        [HttpPost]
        public IActionResult SaveDocument([FromBody] DocumentModel model)
        {
            try
            {
                var getId = _pigContext.UserRights
                        .Where(u => u.UserName == model.Username)
                        .Select(u => u.UserId)
                        .SingleOrDefault();

                var comCheck = model.Company switch
                {
                    "BPI" => 10,
                    "SAC" => 20,
                    "S145" => 30
                };

                DateTime nowDate = DateTime.Now;
                var convDate = nowDate.ToString("yyMMdd", CultureInfo.InvariantCulture);

                long docId;

                if (model.Status == "บันทึก")
                {
                    var checkUpdateDocHead = _pigContext.BolDocHeads
                                      .SingleOrDefault(d => d.DocId == long.Parse(model.DocumentNumber));

                    if (checkUpdateDocHead != null)
                    {
                        checkUpdateDocHead.Plant = model.Plant;
                        checkUpdateDocHead.Status = "S";
                        checkUpdateDocHead.Reason = model.Reason;
                        checkUpdateDocHead.Dep = model.Department;
                        checkUpdateDocHead.ReqDate = DateTime.ParseExact(model.RequiredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        checkUpdateDocHead.Remark = model.Remarks;
                        checkUpdateDocHead.UpdateDate = nowDate;
                        checkUpdateDocHead.UpdateBy = getId;

                        var deleteDocDetailt = _pigContext.BolDocDetails
                                                .Where(d => d.DocId == long.Parse(model.DocumentNumber))
                                                .ToList();

                        _pigContext.BolDocDetails.RemoveRange(deleteDocDetailt);

                        foreach (var part in model.Parts)
                        {
                            _pigContext.BolDocDetails.Add(new BolDocDetail
                            {
                                DocId = long.Parse(model.DocumentNumber),
                                PartNum = part.PartNum,
                                Qty = part.Quantity,
                                Status = "S",
                                Unit = part.Unit,
                                WareHouse = part.Warehouse,
                                Bin = part.Bin,
                            });
                        }

                        _pigContext.SaveChanges();
                    }

                    docId = long.Parse(model.DocumentNumber);
                }
                else if (model.Status == "แก้ไข")
                {
                    var checkUpdateDocHead = _pigContext.BolDocHeads
                                      .SingleOrDefault(d => d.DocId == long.Parse(model.DocumentNumber));

                    if (checkUpdateDocHead != null)
                    {
                        checkUpdateDocHead.Plant = model.Plant;
                        checkUpdateDocHead.Status = "S";
                        checkUpdateDocHead.Reason = model.Reason;
                        checkUpdateDocHead.Dep = model.Department;
                        checkUpdateDocHead.ReqDate = DateTime.ParseExact(model.RequiredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        checkUpdateDocHead.Remark = model.Remarks;
                        checkUpdateDocHead.UpdateDate = nowDate;
                        checkUpdateDocHead.UpdateBy = getId;

                        var deleteDocDetailt = _pigContext.BolDocDetails
                                                .Where(d => d.DocId == long.Parse(model.DocumentNumber))
                                                .ToList();

                        _pigContext.BolDocDetails.RemoveRange(deleteDocDetailt);

                        foreach (var part in model.Parts)
                        {
                            _pigContext.BolDocDetails.Add(new BolDocDetail
                            {
                                DocId = long.Parse(model.DocumentNumber),
                                PartNum = part.PartNum,
                                Qty = part.Quantity,
                                Status = "S",
                                Unit = part.Unit,
                                WareHouse = part.Warehouse,
                                Bin = part.Bin,
                            });
                        }

                        _pigContext.SaveChanges();
                    }

                    docId = long.Parse(model.DocumentNumber);
                }
                else
                {
                    var latestDocId = _pigContext.BolDocHeads
                        .Where(d => d.Company == model.Company && d.DocDate.HasValue && d.DocDate.Value.Date == nowDate.Date)
                        .OrderByDescending(d => d.DocId)
                        .Select(d => d.DocId)
                        .FirstOrDefault();

                    int running;
                    if (latestDocId != 0)
                    {
                        var latestRunning = int.Parse(latestDocId.ToString().Substring(8));
                        running = latestRunning + 1;
                    }
                    else
                    {
                        running = 1;
                    }

                    docId = long.Parse(comCheck + convDate + running.ToString("D4"));

                    _pigContext.BolDocHeads.Add(new BolDocHead
                    {
                        Company = model.Company,
                        Plant = model.Plant,
                        DocId = docId,
                        Status = "S",
                        DocDate = DateTime.Today,
                        CreateBy = getId,
                        Reason = model.Reason,
                        Dep = model.Department,
                        ReqDate = DateTime.ParseExact(model.RequiredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Remark = model.Remarks,
                    });

                    foreach (var part in model.Parts)
                    {
                        _pigContext.BolDocDetails.Add(new BolDocDetail
                        {
                            DocId = docId,
                            PartNum = part.PartNum,
                            Qty = part.Quantity,
                            Status = "S",
                            Unit = part.Unit,
                            WareHouse = part.Warehouse,
                            Bin = part.Bin,
                        });
                    }

                    _pigContext.SaveChanges();
                }

                return Json(new { success = true, data = docId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetDocumentData(string documentNumber, string company)
        {
            var getData = _pigContext.BolDocHeads
                .Where(h => h.DocId == long.Parse(documentNumber))
                .Select(h => new
                {
                    h.DocId,
                    DocDate = h.DocDate.HasValue ? h.DocDate.Value.ToString("dd/MM/yyyy") : "",
                    h.Plant,
                    h.Reason,
                    h.Dep,
                    ReqDate = h.ReqDate.HasValue ? h.ReqDate.Value.ToString("dd/MM/yyyy") : "",
                    h.Remark,
                    h.Status,
                })
                .ToList();

            var getDataDocDetail = _pigContext.BolDocDetails
                .Where(d => d.DocId == long.Parse(documentNumber))
                .Select(d => new
                {
                    d.PartNum,
                    d.Qty,
                    d.Unit,
                    d.WareHouse,
                    d.Bin
                })
                .ToList();

            var partNumbers = getDataDocDetail.Select(r => r.PartNum).Distinct().ToList();

            //var searchUOM = _context.PartUoms
            //    .Where(u => u.Company == company && partNumbers.Contains(u.PartNum))
            //    .Select(u => new { u.PartNum, u.Uomcode })
            //    .ToList();

            var searchUOM = (from p in _context.PartUoms
                             join u in _context.Uoms
                             on new { p.Company, p.Uomcode } equals new { u.Company, u.Uomcode }
                             where p.Company == company && partNumbers.Contains(p.PartNum)
                             select new
                             {
                                 p.PartNum,
                                 u.Uomdesc,
                                 u.Uomcode

                             }).ToList();

            /*var searchWh = _context.PartWhses
                .Where(w => w.Company == company && partNumbers.Contains(w.PartNum))
                .Select(w => new { w.PartNum, w.WarehouseCode })
                .ToList();*/

            //var searchWh = _context.PartWhses
            //     .Join(
            //         _context.Warehses,
            //         p => new { p.Company, p.WarehouseCode },
            //         w => new { w.Company, w.WarehouseCode },
            //         (p, w) => new { p.WarehouseCode, w.Description, p.Company, p.PartNum }
            //     )
            //     .Where(x => x.Company == company && partNumbers.Contains(x.PartNum)) // เงื่อนไข WHERE
            //     .GroupBy(x => new { x.WarehouseCode, x.Description, x.PartNum })
            //     .Select(g => new
            //     {
            //         PartNum = g.Key.PartNum,
            //         WarehouseCode = g.Key.WarehouseCode,
            //         Description = g.Key.Description
            //     })
            //     .ToList();

            var descriptions = _context.Parts
                .Where(p => p.Company == "BPI" && partNumbers.Contains(p.PartNum))
                .Select(p => new { p.PartNum, p.PartDescription })
                .ToDictionary(p => p.PartNum, p => p.PartDescription);

            var dataWithDescriptions = getDataDocDetail.Select(r => new
            {
                r.PartNum,
                Description = descriptions.ContainsKey(r.PartNum) ? descriptions[r.PartNum] : "ไม่พบข้อมูล",
                r.Qty,
                Uoms = searchUOM.Where(u => u.PartNum == r.PartNum).Select(u => new { u.Uomcode, u.Uomdesc }).ToList(),
                //Warehouses = searchWh.Where(w => w.PartNum == r.PartNum).Select(w => new { w.WarehouseCode, w.Description }).ToList(),
                //r.Bin,
                r.Unit,
                //r.WareHouse
            }).ToList();

            return Json(new { success = true, getData, dataWithDescriptions });
        }


        [HttpPost]
        public IActionResult SendForApproval([FromBody] DocumentModel model)
        {
            try
            {
                decimal finalCost = 0;
                string msg = "";

                string costId = model.Plant switch
                {
                    "MfgSys" => "1",
                    "Segment" => "Segment",
                    _ => throw new Exception("Invalid Plant value")
                };

                foreach (var part in model.Parts)
                {
                    var partCostData = _context.PartCosts
                        .Where(p => p.Company == model.Company && p.PartNum == part.PartNum && p.CostId == costId)
                        .Select(p => new { p.StdMaterialCost, p.FifoavgMaterialCost })
                        .FirstOrDefault();

                    if (partCostData != null)
                    {
                        decimal partCost = partCostData.StdMaterialCost > 0 ? partCostData.StdMaterialCost : partCostData.FifoavgMaterialCost;

                        if (partCost > 5000)
                        {
                            msg = "ใบเบิกนี้มียอด มากกว่า 5,000 ต้องรออนุมัติ";

                            var getId = _pigContext.UserRights
                                .Where(u => u.UserName == model.Username)
                                .Select(u => u.UserId)
                                .FirstOrDefault();

                            if (!long.TryParse(model.DocumentNumber, out long docId))
                            {
                                return Json(new { success = false, message = "หมายเลขเอกสารไม่ถูกต้อง" });
                            }

                            var docHead = _pigContext.BolDocHeads.SingleOrDefault(d => d.DocId == docId);
                            var docDetails = _pigContext.BolDocDetails.Where(h => h.DocId == docId).ToList();

                            if (docHead != null)
                            {
                                docHead.Status = "W";
                                docHead.UpdateDate = DateTime.Now;
                                docHead.UpdateBy = getId;

                                foreach (var item in docDetails)
                                {
                                    item.Status = "W";
                                }

                                _pigContext.SaveChanges();
                                return Json(new { success = true, documentNumber = model.DocumentNumber, message = msg });
                            }
                            else
                            {
                                return Json(new { success = false, message = "ไม่พบเอกสาร" });
                            }
                        }

                        finalCost += partCost;
                    }
                    else
                    {
                        return Json(new { success = false, message = $"ไม่พบข้อมูล part: {part.PartNum} และ costId: {costId}" });
                    }
                }

                var getIdNoApproval = _pigContext.UserRights
                    .Where(u => u.UserName == model.Username)
                    .Select(u => u.UserId)
                    .FirstOrDefault();

                if (!long.TryParse(model.DocumentNumber, out long docIdNoApproval))
                {
                    return Json(new { success = false, message = "หมายเลขเอกสารไม่ถูกต้อง" });
                }

                var docHeadNoApproval = _pigContext.BolDocHeads.SingleOrDefault(d => d.DocId == docIdNoApproval);
                var docDetailsNoApproval = _pigContext.BolDocDetails.Where(h => h.DocId == docIdNoApproval).ToList();

                if (docHeadNoApproval != null)
                {
                    docHeadNoApproval.Status = "A";
                    docHeadNoApproval.UpdateDate = DateTime.Now;
                    docHeadNoApproval.UpdateBy = getIdNoApproval;

                    foreach (var item in docDetailsNoApproval)
                    {
                        item.Status = "A";
                    }

                    _pigContext.SaveChanges();
                    return Json(new { success = true, documentNumber = model.DocumentNumber });
                }
                else
                {
                    return Json(new { success = false, message = "ไม่พบเอกสาร" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"เกิดข้อผิดพลาด: {ex.Message}" });
            }
        }


        [HttpPost]
        public IActionResult CancelDocument(string documentNumber, string username)
        {
            try
            {
                var getId = _pigContext.UserRights
                        .Where(u => u.UserName == username)
                        .Select(u => u.UserId)
                        .SingleOrDefault();

                var document = _pigContext.BolDocHeads.Where(d => d.DocId == long.Parse(documentNumber)).ToList();
                var docDetail = _pigContext.BolDocDetails.Where(h => h.DocId == long.Parse(documentNumber)).ToList();

                if (document != null)
                {
                    document[0].Status = "C";
                    document[0].UpdateDate = DateTime.Now;
                    document[0].UpdateBy = getId;

                    foreach (var item in docDetail)
                    {
                        item.Status = "C";
                    }

                    _pigContext.SaveChanges();

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "ไม่พบเอกสาร" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"เกิดข้อผิดพลาด: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult SearchPartModal(string query, string company)
        {
            try
            {
                var resultParts = (from p in _context.Parts
                             where p.Company == company && new[] { "2", "3", "4", "7" }.Contains(p.PartNum.Substring(0, 1))
                             orderby p.PartNum
                             select new
                             {
                                 p.PartNum,
                                 p.PartDescription

                             }).ToList();

                var parts = resultParts.Where(p => p.PartNum.Contains(query) || p.PartDescription.Contains(query)).ToList();

                //var parts = _context.Parts
                //    .Where(p => p.Company == company
                //        && (p.PartNum.StartsWith("4")) || p.PartNum.StartsWith("7"))
                //        /*&& (p.PartNum.Contains(query) || p.PartDescription.Contains(query)))   */
                //    .Select(p => new
                //    {
                //        p.PartNum,
                //        p.PartDescription
                //    })
                //    .ToList();

                if (parts.Count > 0)
                {
                    return Json(new { success = true, data = parts });
                }
                else
                {
                    return Json(new { success = false, message = "ไม่พบข้อมูล" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาด: " + ex.Message });
            }
        }

        //[HttpGet]
        //public IActionResult GeneratePreview(long docId)
        //{
        //    var result = GetData(docId);

        //    PdfDocument document = new PdfDocument();
        //    GlobalFontSettings.FontResolver = new CustomFontResolver();

        //    XFont font = new XFont("thsarabun", 14);
        //    XFont fontB = new XFont("thsarabun", 14, XFontStyleEx.Bold);
        //    XFont fontHeader = new XFont("thsarabun", 16, XFontStyleEx.Bold);

        //    var comCheck = result[0].Company switch
        //    {
        //        "BPI" => "บริษัท บางปะอินเสาเข็มคอนกรีต จำกัด",
        //        "SAC" => "บริษัท ศรีอยุธยา คอนกรีต จำกัด",
        //        "S145" => "บริษัท 145 ศรีอยุธยาคอนกรีต จำกัด"
        //    };

        //    double currentYPos = 180;
        //    int rowNumber = 1;
        //    string prevWarehouseDescription = result[0].WarehouseDescription;

        //    int totalPage = 0;
        //    int currentPage = 1;

        //    PdfPage page = document.AddPage();
        //    XGraphics gfx = XGraphics.FromPdfPage(page);

        //    int resultTotalPage = result.GroupBy(r => r.WarehouseDescription).Count();
        //    totalPage = resultTotalPage;

        //    void DrawHeader(XGraphics gfx, string whDescription)
        //    {
        //        string titleText = comCheck;
        //        XSize titleSize = gfx.MeasureString(titleText, fontHeader);
        //        double titleXPos = (page.Width - titleSize.Width) / 2;
        //        gfx.DrawString(titleText, fontHeader, XBrushes.Black, new XPoint(titleXPos, 70));

        //        string reportName = "ใบเบิกของ";
        //        gfx.DrawString(reportName, fontHeader, XBrushes.Black, new XPoint(32, 90));

        //        string docNum = "เลขที่ใบเบิก " + result[0].DocId;
        //        gfx.DrawString(docNum, font, XBrushes.Black, new XPoint(410, 90));

        //        string docDate = "วันที่เบิก     " + result[0].DocDate.ToString("dd/MM/yyyy");
        //        gfx.DrawString(docDate, font, XBrushes.Black, new XPoint(410, 105));

        //        string qrCodeText = result[0].DocId.ToString();

        //        using (var qrGenerator = new QRCodeGenerator())
        //        using (var qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q))
        //        using (var qrCode = new QRCode(qrCodeData))
        //        using (var qrCodeImage = qrCode.GetGraphic(3))
        //        {
        //            using (var ms = new MemoryStream())
        //            {
        //                qrCodeImage.Save(ms, new PngEncoder());
        //                ms.Seek(0, SeekOrigin.Begin);
        //                var image = SixLabors.ImageSharp.Image.Load<Rgba32>(ms);

        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    image.Save(memoryStream, new PngEncoder());
        //                    memoryStream.Seek(0, SeekOrigin.Begin);
        //                    XImage xImage = XImage.FromStream(memoryStream);
        //                    gfx.DrawImage(xImage, new XPoint(520, 55));
        //                }
        //            }
        //        }

        //        string createName = "ผู้เบิก " + result[0].UserName;
        //        gfx.DrawString(createName, font, XBrushes.Black, new XPoint(32, 120));

        //        string depName = "หน่วยงาน " + result[0].DepName;
        //        gfx.DrawString(depName, font, XBrushes.Black, new XPoint(120, 120));

        //        string whName = "คลัง " + whDescription;
        //        gfx.DrawString(whName, font, XBrushes.Black, new XPoint(210, 120));

        //        string dateReq = "วันที่ต้องการ " + result[0].ReqDate.ToString("dd/MM/yyyy");
        //        gfx.DrawString(dateReq, font, XBrushes.Black, new XPoint(340, 120));

        //        string pageNumberText = "หน้าที่ " + currentPage + " / " + totalPage;
        //        gfx.DrawString(pageNumberText, font, XBrushes.Black, new XPoint(530, 130));

        //        XPen linePen = new XPen(XColors.Black, 1);
        //        gfx.DrawLine(linePen, new XPoint(32, 135), new XPoint(575, 135));

        //        // Table headers
        //        gfx.DrawString("ลำดับ", fontB, XBrushes.Black, new XPoint(40, 150));
        //        gfx.DrawString("Bin", fontB, XBrushes.Black, new XPoint(75, 150));
        //        gfx.DrawString("รหัสสินค้า", fontB, XBrushes.Black, new XPoint(130, 150));
        //        gfx.DrawString("รายการ", fontB, XBrushes.Black, new XPoint(255, 150));
        //        gfx.DrawString("จำนวน", fontB, XBrushes.Black, new XPoint(500, 150));
        //        gfx.DrawString("หน่วย", fontB, XBrushes.Black, new XPoint(545, 150));

        //        XPen linePen1 = new XPen(XColors.Black, 1);
        //        gfx.DrawLine(linePen1, new XPoint(32, 160), new XPoint(575, 160));
        //    }

        //    DrawHeader(gfx, prevWarehouseDescription);

        //    foreach (var item in result)
        //    {
        //        // Check if WarehouseDescription has changed
        //        if (item.WarehouseDescription != prevWarehouseDescription)
        //        {
        //            // Add new page if warehouse description changes
        //            currentPage++;
        //            page = document.AddPage();
        //            gfx = XGraphics.FromPdfPage(page);
        //            DrawHeader(gfx, item.WarehouseDescription);

        //            currentYPos = 180; // Reset Y position for new page
        //            prevWarehouseDescription = item.WarehouseDescription;

        //            rowNumber = 1;
        //        }

        //        gfx.DrawString(rowNumber.ToString(), font, XBrushes.Black, new XPoint(40, currentYPos));
        //        gfx.DrawString(item.BinNum, font, XBrushes.Black, new XPoint(75, currentYPos));
        //        gfx.DrawString(item.PartNum, font, XBrushes.Black, new XPoint(130, currentYPos));

        //        string partDesc = item.PartDescription;
        //        double maxWidth = 230;
        //        var partDescLines = SplitTextToLines(gfx, partDesc, font, maxWidth);

        //        gfx.DrawString(partDescLines[0], font, XBrushes.Black, new XPoint(255, currentYPos));

        //        gfx.DrawString(item.Qty.ToString(), font, XBrushes.Black, new XPoint(500, currentYPos));
        //        gfx.DrawString(item.Unit, font, XBrushes.Black, new XPoint(545, currentYPos));

        //        currentYPos += 20;
        //        for (int i = 1; i < partDescLines.Count; i++)
        //        {
        //            gfx.DrawString(partDescLines[i], font, XBrushes.Black, new XPoint(255, currentYPos));
        //            currentYPos += 20;
        //        }
        //        rowNumber++;
        //    }

        //    //int dataCount = result.Count(); // จำนวนข้อมูล
        //    //double rowHeight = 35; // ความสูงเฉลี่ยของแต่ละแถว
        //    //double lineLength = 135 + (dataCount * rowHeight); // คำนวณความยาวตามจำนวนข้อมูล

        //    //XPen lineVertical = new XPen(XColors.Black, 1);
        //    //gfx.DrawLine(lineVertical, new XPoint(32, 135), new XPoint(32, lineLength)); // วาดเส้น

        //    MemoryStream stream = new MemoryStream();
        //    document.Save(stream, false);
        //    byte[] pdfBytes = stream.ToArray();

        //    return File(pdfBytes, "application/pdf", "PickingList.pdf");
        //}

        [HttpGet]
        public IActionResult GeneratePreview(long docId)
        {
            var result = GetData(docId);

            PdfDocument document = new PdfDocument();
            GlobalFontSettings.FontResolver = new CustomFontResolver();

            XFont font = new XFont("thsarabun", 14);
            XFont fontB = new XFont("thsarabun", 14, XFontStyleEx.Bold);
            XFont fontHeader = new XFont("thsarabun", 16, XFontStyleEx.Bold);

            var comCheck = result[0].Company switch
            {
                "BPI" => "บริษัท บางปะอินเสาเข็มคอนกรีต จำกัด",
                "SAC" => "บริษัท ศรีอยุธยา คอนกรีต จำกัด",
                "S145" => "บริษัท 145 ศรีอยุธยาคอนกรีต จำกัด"
            };

            double currentYPos = 180;
            int rowNumber = 1;

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            void DrawHeader(XGraphics gfx)
            {
                string titleText = comCheck;
                XSize titleSize = gfx.MeasureString(titleText, fontHeader);
                double titleXPos = (page.Width - titleSize.Width) / 2;
                gfx.DrawString(titleText, fontHeader, XBrushes.Black, new XPoint(titleXPos, 70));

                string reportName = "ใบเบิกของ";
                gfx.DrawString(reportName, fontHeader, XBrushes.Black, new XPoint(32, 90));

                string docNum = "เลขที่ใบเบิก " + result[0].DocId;
                gfx.DrawString(docNum, font, XBrushes.Black, new XPoint(410, 90));

                string docDate = "วันที่เบิก     " + result[0].DocDate.ToString("dd/MM/yyyy");
                gfx.DrawString(docDate, font, XBrushes.Black, new XPoint(410, 105));

                string qrCodeText = result[0].DocId.ToString();

                using (var qrGenerator = new QRCodeGenerator())
                using (var qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q))
                using (var qrCode = new QRCode(qrCodeData))
                using (var qrCodeImage = qrCode.GetGraphic(3))
                {
                    using (var ms = new MemoryStream())
                    {
                        qrCodeImage.Save(ms, new PngEncoder());
                        ms.Seek(0, SeekOrigin.Begin);
                        var image = SixLabors.ImageSharp.Image.Load<Rgba32>(ms);

                        using (var memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, new PngEncoder());
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            XImage xImage = XImage.FromStream(memoryStream);
                            gfx.DrawImage(xImage, new XPoint(520, 55));
                        }
                    }
                }

                string createName = "ผู้เบิก " + result[0].UserName;
                gfx.DrawString(createName, font, XBrushes.Black, new XPoint(32, 120));

                string depName = "หน่วยงาน " + result[0].DepName;
                gfx.DrawString(depName, font, XBrushes.Black, new XPoint(150, 120));

                string dateReq = "วันที่ต้องการ " + result[0].ReqDate.ToString("dd/MM/yyyy");
                gfx.DrawString(dateReq, font, XBrushes.Black, new XPoint(340, 120));

                XPen linePen = new XPen(XColors.Black, 1);
                gfx.DrawLine(linePen, new XPoint(32, 135), new XPoint(575, 135));

                // Table headers
                gfx.DrawString("ลำดับ", fontB, XBrushes.Black, new XPoint(40, 150));
                gfx.DrawString("รหัสสินค้า", fontB, XBrushes.Black, new XPoint(130, 150));
                gfx.DrawString("รายการ", fontB, XBrushes.Black, new XPoint(255, 150));
                gfx.DrawString("จำนวน", fontB, XBrushes.Black, new XPoint(500, 150));
                gfx.DrawString("หน่วย", fontB, XBrushes.Black, new XPoint(545, 150));

                XPen linePen1 = new XPen(XColors.Black, 1);
                gfx.DrawLine(linePen1, new XPoint(32, 160), new XPoint(575, 160));
            }

            DrawHeader(gfx);

            foreach (var item in result)
            {
                gfx.DrawString(rowNumber.ToString(), font, XBrushes.Black, new XPoint(40, currentYPos));
                gfx.DrawString(item.PartNum, font, XBrushes.Black, new XPoint(130, currentYPos));

                string partDesc = item.PartDescription;
                double maxWidth = 230;
                var partDescLines = SplitTextToLines(gfx, partDesc, font, maxWidth);

                gfx.DrawString(partDescLines[0], font, XBrushes.Black, new XPoint(255, currentYPos));

                gfx.DrawString(item.Qty.ToString(), font, XBrushes.Black, new XPoint(500, currentYPos));
                gfx.DrawString(item.Unit, font, XBrushes.Black, new XPoint(545, currentYPos));

                currentYPos += 20;
                for (int i = 1; i < partDescLines.Count; i++)
                {
                    gfx.DrawString(partDescLines[i], font, XBrushes.Black, new XPoint(255, currentYPos));
                    currentYPos += 20;
                }
                rowNumber++;
            }

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] pdfBytes = stream.ToArray();

            return File(pdfBytes, "application/pdf", "PickingList.pdf");
        }


        private List<ReportsModel> GetData(long docId)
        {
            var resultData = _pigContext.ReportsModels
                .FromSqlInterpolated($"EXEC BPI_BillOfLoading_PickUp {docId}")
                .ToList();

            return resultData;
        }

        private List<string> SplitTextToLines(XGraphics gfx, string text, XFont font, double maxWidth)
        {
            List<string> lines = new List<string>();
            string[] words = text.Split(' ');
            string currentLine = "";

            foreach (var word in words)
            {
                string testLine = currentLine.Length > 0 ? currentLine + " " + word : word;
                double width = gfx.MeasureString(testLine, font).Width;

                if (width < maxWidth)
                {
                    currentLine = testLine;
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = word;
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                lines.Add(currentLine);
            }

            return lines;
        }
    }
}
