using BPI_BillOfLading.context;
using BPI_BillOfLading.Models;
using BPI_BillOfLading.Models.Data;
using BPI_BillOfLading.Models.ReturnStockModel;
using BPI_BillOfLading.Models.StockOutModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BPI_BillOfLading.Controllers
{
    public class ReturnStockController : Controller
    {
        private readonly BpiLiveContext _context;

        private readonly STRContext _STR;
        private readonly STRContextStockOut _StockOut;
        private readonly BpigContext _pigContext;
        private readonly WHContext _wHContext;
        private readonly allWHContext _allWHContext;
        private readonly TableBillContext _TBL;
        private readonly TableBillContextStockOut _TBLStock;
        private readonly BillOfLoadingContextStockOut _TBLBillOfLoading;
        private readonly STRPagetwContextStockOut _TBLPagetwContext;
        private readonly RSTRPagetwContextStockOut _RTBLPagetwContext;
        private readonly convertfcontext _convertfcontext;
        private readonly statuscontext _status;
        private readonly RTableBillContextStockOut _RTBLStock;

        public ReturnStockController(STRContext sTR, BpigContext pigContext, WHContext wHContext,
            allWHContext allWHContext, TableBillContext tBL, STRContextStockOut stockOut,
            TableBillContextStockOut tBLStock, BillOfLoadingContextStockOut TBLBillOfLoading,
            STRPagetwContextStockOut tBLPagetwContext, RSTRPagetwContextStockOut RtBLPagetwContext,
            BpiLiveContext context, convertfcontext convert, statuscontext status, RTableBillContextStockOut rtBLStock
            )
        {
            _STR = sTR;
            _pigContext = pigContext;
            _wHContext = wHContext;
            _allWHContext = allWHContext;
            _TBL = tBL;
            _StockOut = stockOut;
            _TBLStock = tBLStock;
            _TBLBillOfLoading = TBLBillOfLoading;
            _TBLPagetwContext = tBLPagetwContext;
            _RTBLPagetwContext = RtBLPagetwContext;
            _context = context;
            _convertfcontext = convert;
            _status = status;
            _RTBLStock = rtBLStock;

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


        [HttpGet("ReturnStock/Privacy/{docId?}")]
        public IActionResult Privacy(string docId, string date, string Status, string UserName, string Company)
        {
            ViewData["docId"] = docId;
            ViewData["date"] = date;
            ViewData["Status"] = Status;
            ViewData["UserName"] = UserName;
            ViewData["Company"] = Company;

            return View();
        }

        [HttpGet("ReturnStock/GetBillOfLading")]
        public IActionResult BillOfLading(string docNumber, string WHCode, string Company)
        {
            var query = _TBLPagetwContext.STRPageModelStockOuts
                        .FromSqlInterpolated($"EXEC BillOfLading {docNumber},{WHCode},{Company}")
                        .ToList();

            return Json(query);
        }

        [HttpGet("ReturnStock/Holiday")]
        public IActionResult Holiday(string docNumber)
        {
            var holidays = (from h in _pigContext.Holidays
                            where h.Company == "BPI"
                            select h).ToList();
            return Json(holidays);
        }

        [HttpGet("ReturnStock/WHDescription")]
        public IActionResult WHDescription(string docNumber, string Company)
        {

            var query = _wHContext.WHModels
                      .FromSqlInterpolated($"EXEC WHDescription {docNumber},{Company}")
                          .ToList();
            return Json(query);
        }

        [HttpGet("ReturnStock/WHCODE")]
        public IActionResult WHCODE(string Description)
        {
            var query = _allWHContext.allWHModels
                      .FromSqlInterpolated($"EXEC allWH {Description}")
                          .ToList();
            return Json(query);
        }
        [HttpGet("ReturnStock/status")]
        public IActionResult status(string Description)
        {
            var query = _status.statusmodels
                      .FromSqlInterpolated($"EXEC status {Description}")
                          .ToList();
            return Json(query);
        }
        [HttpGet("ReturnStock/BPI_BillOfLoading_Wh")]
        public IActionResult BPI_BillOfLoading_Wh(string username)
        {
            var getId = _pigContext.UserRights
                        .Where(u => u.UserName == username)
                        .Select(u => u.UserId)
                        .SingleOrDefault();

            var query = _TBLBillOfLoading.BillOfLoadingModelStockOuts
                      .FromSqlInterpolated($"EXEC BPI_BillOfLoading_Wh {getId}")
                          .ToList();
            return Json(query);
        }
        [HttpGet("ReturnStock/convertf")]
        public IActionResult convertf(string partNum, string company, string unit)
        {

            var query = _convertfcontext.convertfmodels
                      .FromSqlInterpolated($"EXEC convertf {partNum},{company},{unit}")
                          .ToList();
            return Json(query);
        }
        [HttpGet("ReturnStock/TableBill")]
        public IActionResult TableBill(string docNumber, string WHCode, string Company)
        {
            var query = _RTBLStock.RTableBillModelStockOuts
                    .FromSqlInterpolated($"EXEC ReturnGetTabelBill {docNumber},{WHCode},{Company}")
                        .ToList();
            var getDataDocDetail = _pigContext.BolDocDetails
                .Where(d => d.DocId == long.Parse(docNumber))
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
                             where p.Company == Company && partNumbers.Contains(p.PartNum)
                             select new
                             {
                                 p.PartNum,
                                 u.Uomdesc,
                                 u.Uomcode

                             }).ToList();

            return Json(new { query, UOM = searchUOM });
        }
        [HttpPost]
        public IActionResult SavePayment(string docNumber, string WH, string DetailID, string PaidID, string qtyReturn, string unitreturn, string Createdate, string username, string lotnum, string partNum, string bin, string Plant, string trandate, string ReasonCode, string company)
        {
            try
            {
                var getId = _pigContext.UserRights
                        .Where(u => u.UserName == username)
                        .Select(u => u.UserId)
                        .SingleOrDefault();
                int totalRowsSaved = 0; // ตัวแปรสำหรับนับจำนวนแถวที่บันทึก

                using (var connection = new SqlConnection("Server=192.168.10.2;Database=BPIG;User Id=webapp;Password=web@pp; TrustServerCertificate=True;"))
                {
                    connection.Open();
                    lotnum = lotnum ?? "";

                    // ตรวจสอบว่ามี DetailID อยู่แล้วหรือไม่
                    using (var checkCommand = new SqlCommand(
                        "SELECT COUNT(1) FROM BPIG.dbo.BOL_Return WHERE DetailID = @DetailID and PaidID = @PaidID", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@DetailID", DetailID);
                        checkCommand.Parameters.AddWithValue("@PaidID", PaidID);

                        var exists = (int)checkCommand.ExecuteScalar() > 0;

                        if (exists)
                        {
                            return Json(new { success = false, message = "Skipped duplicate ReturnID." });
                        }
                    }

                    // INSERT ข้อมูลและดึง PaidID
                    long newReturnID;
                    using (var insertCommand = new SqlCommand(
                        "INSERT INTO BPIG.dbo.BOL_Return (PaidID,DetailID, QTY, Unit, CreateDate, CreateBy) " +
                        "OUTPUT INSERTED.ReturnID " +
                        "VALUES (@PaidID,@DetailID, @QTY, @Unit, @Createdate, @Createby)", connection))
                    {
                        insertCommand.Parameters.AddWithValue("@PaidID", PaidID);
                        insertCommand.Parameters.AddWithValue("@DetailID", DetailID);
                        insertCommand.Parameters.AddWithValue("@QTY", qtyReturn);
                        insertCommand.Parameters.AddWithValue("@Unit", unitreturn);
                        insertCommand.Parameters.AddWithValue("@Createdate", Createdate);
                        insertCommand.Parameters.AddWithValue("@Createby", getId);
                        insertCommand.Parameters.AddWithValue("@lotnum", lotnum);

                        newReturnID = (long)insertCommand.ExecuteScalar();
                        totalRowsSaved++; // นับเพิ่มเมื่อ INSERT สำเร็จ

                    }
                    return Json(new
                    {
                        success = true,
                        ReturnID = newReturnID,
                        DetailID = DetailID ?? "",
                        lotnum = lotnum ?? "",
                        QTY = qtyReturn ?? "",
                        partNum = partNum ?? "",
                        Plant = Plant ?? "",
                        trandate = trandate ?? "",
                        ReasonCode = ReasonCode ?? "",
                        Bin = bin ?? "",
                        Dimcode = unitreturn ?? "",
                        DOCID = docNumber ?? "",
                        company = company ?? "",
                        WH = WH ?? "",
                        PaidID = PaidID
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




        [HttpPost]
        public IActionResult SaveCsvToServer([FromBody] CsvRequest data)
        {
            if (data == null || string.IsNullOrEmpty(data.CsvData) || string.IsNullOrEmpty(data.FileName))
            {
                return BadRequest("Invalid data");
            }

            // กำหนดเส้นทางไฟล์
            string directoryPath = @"\\192.168.10.2\ERPAutoDMT\Live\BPI ReturnMisc\Input";
            //string directoryPath = @"C:\Users\USER069\Desktop\ทดสอบ_savecsv";

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(data.FileName);
            string fileExtension = Path.GetExtension(data.FileName);
            int fileIndex = 1;

            string filePath = Path.Combine(directoryPath, data.FileName);

            // ถ้าไฟล์มีอยู่แล้ว ให้เพิ่ม (1), (2), ... ที่ชื่อไฟล์
            while (System.IO.File.Exists(filePath))
            {
                string newFileName = $"{fileNameWithoutExtension}({fileIndex}){fileExtension}";
                filePath = Path.Combine(directoryPath, newFileName);
                fileIndex++;
            }

            try
            {
                // ตรวจสอบอีกครั้งก่อนเขียนไฟล์
                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.WriteAllText(filePath, data.CsvData);
                }
                else
                {
                    return Conflict(new { message = "File already exists after name check." });
                }

                return Ok(new { message = "File saved successfully!", filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving file: {ex.Message}");
            }
        }




        public async Task<IActionResult> GetFilteredData(DateTime Startdate, DateTime Stopdate, string Company)
        {
            //string Company = "BPI";

            // Use proper SQL parameterization to avoid SQL injection attacks
            var data = await _RTBLPagetwContext.RSTRPageModelStockOuts
                .FromSqlInterpolated($"EXEC [returnstockout] {Company}, {Startdate}, {Stopdate}")
                .ToListAsync(); // Ensure asynchronous execution

            // Return the data as JSON
            return Json(data);
        }

        public IActionResult Privacy(string username, string company)
        {
            if (string.IsNullOrWhiteSpace(company))
            {
                //return Redirect("https://192.168.2.144:40480/Account/Login");
                return Redirect("https://webapp.bpi-concretepile.co.th:9021/Account/Login");
            }

            //ViewBag.Token = token;
            //string username = JwtHelper.GetUsernameFromToken(token);  // ใช้ JwtHelper ที่นำเข้ามา
            //string Company = JwtHelper.GetCompanyFromToken(token);  // ใช้ JwtHelper ที่นำเข้ามา

            ViewBag.Username = username;  // ส่งค่า Username ไปยัง View
            ViewBag.Company = company;  // ส่งค่า Username ไปยัง View
            //ViewBag.Token = token; // or use ViewBag.Token = token;
            return View();
        }
    }
}