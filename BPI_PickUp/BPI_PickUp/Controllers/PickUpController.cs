using BPI_PickUp.Models;
using BPI_PickUp.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection.Metadata;

namespace BPI_PickUp.Controllers
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetFormData()
        {
            var getPlant = await _context.Plants
        .Select(i => i.Plant1)
        .Distinct()
        .ToListAsync();

            var getReasonCode = await _pigContext.BolUsersPolicies
                .Where(i => i.DataType == "REA" && i.UserId == 274)
                .Select(i => i.DataCode)
                .ToListAsync();

            var getReasonDesc = await _context.Reasons
                .Where(i => getReasonCode.Contains(i.ReasonCode) && i.Company == "BPI")
                .Select(i => new { Value = i.ReasonCode, Text = i.Description })
                .ToListAsync();

            var getDep = await (from u in _pigContext.BolUsersPolicies
                                join d in _pigContext.Deps on u.DataCode equals d.DepCode
                                where u.UserId == 274 && u.DataType == "DEP"
                                select new
                                {
                                    Value = d.DepCode, // Assuming DepCode is the value to use
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
        public IActionResult GetPartNum(string partNum)
        {
            var searchPart = _context.Parts
               .Where(p => p.PartNum == partNum && p.Company == "BPI")
               .Select(p => new 
               {
                   p.PartNum,
                   p.PartDescription,
                   p.Ium

               }).ToList();

            var searchWh = _context.PartWhses
                .Where(w => w.Company == "BPI" && w.PartNum == partNum )
                .Select(w => w.WarehouseCode)
                .ToList();

            var searchBin = _context.PartBins
                .Where(b => b.Company == "BPI" && b.PartNum == partNum)
                .Select(b => b.BinNum)
                .ToList();

            if (searchPart.Count > 0)
            {
                return Json(new { success = true, searchPart, searchWh, searchBin });
            }
            else
            {
                return Json(new { success = false });
            }

        }

        [HttpPost]
        public IActionResult SaveDocument([FromBody] DocumentModel model)
        {
            try
            {
                var company = "BPI";
                var comCheck = company switch
                {
                    "BPI" => 10,
                    "SAC" => 20,
                    "S145" => 30
                };

                DateTime nowDate = DateTime.Now;
                var convDate = nowDate.ToString("yyMMdd");
                int running = 1;

                _pigContext.BolDocHeads.Add(new BolDocHead
                {
                    Company = company,
                    Plant = model.Plant,
                    DocId = long.Parse(comCheck + convDate + running.ToString("D4")),
                    Status = "S",
                    DocDate = nowDate,
                    CreateBy = 274,
                    Reason = model.Reason,
                    Dep = model.Department,
                    ReqDate = model.RequiredDate,
                    Remark = model.Remarks,
                    //UpdateDate = nowDate,
                    //UpdateBy = 274

                });

                foreach (var part in model.Parts)
                {
                    _pigContext.BolDocDetails.Add(new BolDocDetail
                    { 
                        DocId = long.Parse(comCheck + convDate + running.ToString("D4")),
                        PartNum = part.PartNum,
                        Qty = part.Quantity,
                        Unit = part.Unit,
                        WareHouse = part.Warehouse,
                        Bin = part.Bin,
                    });
                }

                _pigContext.SaveChanges();

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult GetDocumentData(string documentNumber)
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
                    Status = h.Status == "S" ? "บันทึกข้อมูล" : h.Status,
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

            var descriptions = _context.Parts
                .Where(p => p.Company == "BPI" && partNumbers.Contains(p.PartNum))
                .Select(p => new { p.PartNum, p.PartDescription })
                .ToDictionary(p => p.PartNum, p => p.PartDescription);

            var dataWithDescriptions = getDataDocDetail.Select(r => new
            {
                r.PartNum,
                Description = descriptions.ContainsKey(r.PartNum) ? descriptions[r.PartNum] : "ไม่พบข้อมูล",
                r.Qty,
                r.Unit,
                r.WareHouse,
                Bin = r.Bin ?? ""
            }).ToList();

            return Json(new { success = true, getData, dataWithDescriptions });
        }

        [HttpPost]
        public IActionResult SendForApproval(string documentNumber)
        {
            try
            {
                var document = _pigContext.BolDocHeads.Where(d => d.DocId == long.Parse(documentNumber)).ToList();

                if (document != null)
                {
                    document[0].Status = "W";
                    document[0].UpdateDate = DateTime.Now;
                    document[0].UpdateBy = 274;

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

        [HttpPost]
        public IActionResult CancelDocument(string documentNumber)
        {
            try
            {
                var document = _pigContext.BolDocHeads.Where(d => d.DocId == long.Parse(documentNumber)).ToList();

                if (document != null)
                {
                    document[0].Status = "C";
                    document[0].UpdateDate = DateTime.Now;
                    document[0].UpdateBy = 274;

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
        public IActionResult SearchPartModal(string query)
        {
            try
            {
                var parts = _context.Parts
                    .Where(p => p.Company == "BPI" && p.PartNum.Contains(query) || p.PartDescription.Contains(query))
                    .Select(p => new
                    {
                        p.PartNum,
                        p.PartDescription,
                        p.Ium,
                    })
                    .ToList();

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

    }
}
