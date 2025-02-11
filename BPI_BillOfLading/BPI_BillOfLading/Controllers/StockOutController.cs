using BPI_BillOfLading.context;
using BPI_BillOfLading.Models;
using BPI_BillOfLading.Models.Data;
using BPI_BillOfLading.Models.StockOutModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BPI_BillOfLading.Controllers
{
    public class StockOutController : Controller
    {
        private readonly STRContext _STR;
        private readonly STRContextStockOut _StockOut;
        private readonly BpigContext _pigContext;
        private readonly WHContext _wHContext;
        private readonly allWHContext _allWHContext;
        private readonly TableBillContext _TBL;
        private readonly TableBillContextStockOut _TBLStock;
        private readonly BillOfLoadingContextStockOut _TBLBillOfLoading;
        private readonly STRPagetwContextStockOut _TBLPagetwContext;
        private readonly lotContext _lotContext;
        private readonly WHbyColumnContext _wHbyColumnContext;
        private readonly binbycolumnContext _binbycolumnContext;
        private readonly onhandQtycontext _onhandQtycontext;

        public StockOutController(STRContext sTR, BpigContext pigContext, WHContext wHContext, allWHContext allWHContext, TableBillContext tBL, STRContextStockOut stockOut, TableBillContextStockOut tBLStock,
            
            BillOfLoadingContextStockOut TBLBillOfLoading, STRPagetwContextStockOut tBLPagetwContext,
            
            
            lotContext lotContext, WHbyColumnContext wHbyColumnContext, binbycolumnContext binbycolumnContext,
            onhandQtycontext onhandQtycontext)

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
            _lotContext = lotContext;
            _wHbyColumnContext = wHbyColumnContext;
            _binbycolumnContext = binbycolumnContext;
            _onhandQtycontext = onhandQtycontext;
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

        [HttpGet("StockOut/Privacy/{docId?}")]
        public IActionResult Privacy(string docId, string date, string Status, string UserName, string Company)
        {
            ViewData["docId"] = docId;
            ViewData["date"] = date;
            ViewData["Status"] = Status;
            ViewData["UserName"] = UserName;
            ViewData["Company"] = Company;

            return View();
        }

        [HttpGet("StockOut/GetBillOfLading")]
        public IActionResult BillOfLading(string docNumber, string WHCode, string Company)
        {
            var query = _TBLPagetwContext.STRPageModelStockOuts
                        .FromSqlInterpolated($"EXEC BillOfLading {docNumber},{WHCode},{Company}")
                        .ToList();

            return Json(query);
        }

        [HttpGet("StockOut/Holiday")]
        public IActionResult Holiday(string docNumber)
        {
            var holidays = (from h in _pigContext.Holidays
                            where h.Company == "BPI"
                            select h).ToList();
            return Json(holidays);
        }

        [HttpGet("StockOut/WHDescription")]
        public IActionResult WHDescription(string docNumber, string Company)
        {

            var query = _wHContext.WHModels
                      .FromSqlInterpolated($"EXEC WHDescription {docNumber},{Company}")
                          .ToList();
            return Json(query);
        }

        [HttpGet("StockOut/WHCODE")]
        public IActionResult WHCODE(string Description)
        {
            var query = _allWHContext.allWHModels
                      .FromSqlInterpolated($"EXEC allWH {Description}")
                          .ToList();
            return Json(query);
        }

        [HttpGet("StockOut/BPI_BillOfLoading_Wh")]
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

        [HttpGet("StockOut/TableBill")]
        public IActionResult TableBill(string docNumber, string WHCode, string Company,string username)
        {
            // กำหนดค่า WHCode เป็น string ว่าง หากไม่ได้ระบุค่า
            WHCode = "";
            var getId = _pigContext.UserRights
                   .Where(u => u.UserName == username)
                   .Select(u => u.UserId)
                   .SingleOrDefault();
            var query = _TBLStock.TableBillModelStockOuts
                    .FromSqlInterpolated($"EXEC GetTabelBill {docNumber},{Company},{getId}")
                        .ToList();
            return Json(query);
        }

        [HttpGet("StockOut/Lotnum")]
        public IActionResult Lotnum(string Partnum, string WH, string Company,string bin)
        {
            try
            {
                var query = _lotContext.lotnumModels
                    .FromSqlInterpolated($"EXEC Lotnum {Company}, {Partnum}, {WH},{bin}")
                    .ToList();

                if (query == null || !query.Any())
                {
                    return Json(new { success = false, message = "ไม่พบข้อมูล lot number" });
                }

                return Json(query);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"เกิดข้อผิดพลาด: {ex.Message}" });
            }
        }

        [HttpGet("StockOut/WHbycolumn")]
        public IActionResult WHbycolumn(string Partnum, string docid, string Company, string username)
        {
            try
            {
                var getId = _pigContext.UserRights
                     .Where(u => u.UserName == username)
                     .Select(u => u.UserId)
                     .SingleOrDefault();

                var query = _wHbyColumnContext.WHbyColumnModels
                    .FromSqlInterpolated($"EXEC WHCode {Company}, {Partnum}, {docid},{getId}")
                    .ToList();

                if (query == null || !query.Any())
                {
                    return Json(new { success = false, message = "ไม่พบข้อมูล lot number" });
                }

                return Json(query);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"เกิดข้อผิดพลาด: {ex.Message}" });
            }
        }
        [HttpGet("StockOut/binbycolumn")]
        public IActionResult binbycolumn(string Partnum, string docid, string whcode, string username)
        {
            try
            {
                var getId = _pigContext.UserRights
                       .Where(u => u.UserName == username)
                       .Select(u => u.UserId)
                       .SingleOrDefault();

                var query = _binbycolumnContext.binbycolumnModels
                    .FromSqlInterpolated($"EXEC binbycolumn {whcode}, {Partnum}, {docid},{getId}")
                    .ToList();

                if (query == null || !query.Any())
                {
                    return Json(new { success = false, message = "ไม่พบข้อมูล binbycolumn" });
                }

                return Json(query);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"เกิดข้อผิดพลาด: {ex.Message}" });
            }
        }
        [HttpGet("StockOut/onhandQty")]
        public IActionResult onhandQty(string Partnum, string WH,string Company,string lot,string bin)
        {
            try
            {
                var query = _onhandQtycontext.onhandQtyModels
                    .FromSqlInterpolated($"EXEC onhandQty {Company}, {Partnum}, {WH},{bin},{lot}")
                    .ToList();

                if (query == null || !query.Any())
                {
                    return Json(new { success = false, message = "ไม่พบข้อมูล onhandQty" });
                }

                return Json(query);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"เกิดข้อผิดพลาด: {ex.Message}" });
            }
        }
        [HttpPost]
        public IActionResult SavePayment(string docNumber, string DetailID, string PayaQTY, string Unit, string Createdate, string username, string lotnum, string partNum, string Plant, string trandate, string ReasonCode, string bin, string Dimcode, string company, string WH, string DOCID)
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
                        "SELECT COUNT(1) FROM BPIG.dbo.BOL_Paid WHERE DetailID = @DetailID and lotnum = @lotnum", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@DetailID", DetailID);
                        checkCommand.Parameters.AddWithValue("@lotnum", lotnum);

                        var exists = (int)checkCommand.ExecuteScalar() > 0;

                        if (exists)
                        {
                            return Json(new { success = true, message = "Skipped duplicate DetailID." });
                        }
                    }

                    // INSERT ข้อมูลและดึง PaidID
                    long newPaidID;
                    using (var insertCommand = new SqlCommand(
                        "INSERT INTO BPIG.dbo.BOL_Paid (DetailID, QTY, Unit, CreateDate, CreateBy, lotnum) " +
                        "OUTPUT INSERTED.PaidID " +
                        "VALUES (@DetailID, @QTY, @Unit, @Createdate, @Createby, @lotnum)", connection))
                    {
                        insertCommand.Parameters.AddWithValue("@DetailID", DetailID);
                        insertCommand.Parameters.AddWithValue("@QTY", PayaQTY);
                        insertCommand.Parameters.AddWithValue("@Unit", Unit);
                        insertCommand.Parameters.AddWithValue("@Createdate", Createdate);
                        insertCommand.Parameters.AddWithValue("@Createby", getId);
                        insertCommand.Parameters.AddWithValue("@lotnum", lotnum);

                        newPaidID = (long)insertCommand.ExecuteScalar();
                        totalRowsSaved++; // นับเพิ่มเมื่อ INSERT สำเร็จ

                    }

                    // อัปเดตสถานะใน BOL_DocDetail
                    using (var updateDocDetailCommand = new SqlCommand(
                    "UPDATE BPIG.dbo.BOL_DocDetail SET Status = 'P', WareHouse = @WareHouse, Bin = @Bin " +
                    "WHERE DocID = @DocID AND DetailID = @DetailID AND Partnum = @Partnum", connection))
                    {
                        updateDocDetailCommand.Parameters.AddWithValue("@DocID", docNumber);
                        updateDocDetailCommand.Parameters.AddWithValue("@DetailID", DetailID);
                        updateDocDetailCommand.Parameters.AddWithValue("@Partnum", partNum);
                        updateDocDetailCommand.Parameters.AddWithValue("@WareHouse", WH); // เพิ่มค่า Warehouse
                        updateDocDetailCommand.Parameters.AddWithValue("@Bin", bin); // เพิ่มค่า Bin

                        var rowsAffected = updateDocDetailCommand.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            return Json(new { success = false, message = "No matching record found in BOL_DocDetail to update." });
                        }
                    }

                    // ตรวจสอบสถานะใน BOL_DocDetail ทั้งหมด (เฉพาะครั้งเดียวหลังการบันทึกทั้งหมด)
                    bool allDetailsPaid;
                    using (var checkStatusCommand = new SqlCommand(
                        "SELECT COUNT(1) FROM BPIG.dbo.BOL_DocDetail WHERE DocID = @docNumber AND (Status != 'P' OR Status IS NULL)", connection))
                    {
                        checkStatusCommand.Parameters.AddWithValue("@docNumber", docNumber);
                        allDetailsPaid = (int)checkStatusCommand.ExecuteScalar() == 0;
                    }

                    if (allDetailsPaid)
                    {
                        // ตรวจสอบและอัปเดต BOL_DocHead
                        using (var checkDocHeadStatusCommand = new SqlCommand(
                            "SELECT COUNT(1) FROM BPIG.dbo.BOL_DocHead WHERE DocID = @docNumber AND (Status != 'P' OR Status IS NULL)", connection))
                        {
                            checkDocHeadStatusCommand.Parameters.AddWithValue("@docNumber", docNumber);

                            var docHeadRequiresUpdate = (int)checkDocHeadStatusCommand.ExecuteScalar() > 0;

                            if (docHeadRequiresUpdate)
                            {
                                using (var updateDocHeadCommand = new SqlCommand(
                                    "UPDATE BPIG.dbo.BOL_DocHead " +
                                    "SET Status = 'P', UpdateDate = @Createdate " +
                                    "WHERE DocID = @DocID AND (Status IS NULL OR Status != 'P')", connection))
                                {
                                    updateDocHeadCommand.Parameters.AddWithValue("@DocID", docNumber);
                                    updateDocHeadCommand.Parameters.AddWithValue("@Createdate", Createdate);

                                    var rowsUpdated = updateDocHeadCommand.ExecuteNonQuery();
                                    if (rowsUpdated == 0)
                                    {
                                        return Json(new
                                        {
                                            success = true,
                                            PaidID = newPaidID,
                                            DetailID = DetailID,
                                            lotnum = lotnum,
                                            QTY = PayaQTY,
                                            partNum = partNum,
                                            Plant = Plant,
                                            trandate = trandate,
                                            ReasonCode = ReasonCode,
                                            Bin = bin,
                                            Dimcode = Unit,
                                            DOCID = DOCID,
                                            company = company,
                                            WH = WH,
                                            message = "No matching record found in BOL_DocHead to update."
                                        });
                                    }
                                    else
                                    {
                                        // อัปเดตสำเร็จ ส่ง hideButton = true
                                        return Json(new
                                        {
                                            success = true,
                                            hideButton = true,
                                            PaidID = newPaidID,
                                            DetailID = DetailID,
                                            lotnum = lotnum,
                                            QTY = PayaQTY,
                                            partNum = partNum,
                                            Plant = Plant,
                                            trandate = trandate,
                                            ReasonCode = ReasonCode,
                                            Bin = bin,
                                            Dimcode = Unit,
                                            DOCID = DOCID,
                                            company = company,
                                            WH = WH,
                                            totalRowsSaved,
                                            message = "BOL_DocHead updated successfully."
                                        });
                                    }
                                }
                            }
                        }
                    }

                    return Json(new
                    {
                        success = true,
                        PaidID = newPaidID,
                        DetailID = DetailID ?? "",
                        lotnum = lotnum ?? "",
                        QTY = PayaQTY ?? "",
                        partNum = partNum ?? "",
                        Plant = Plant ?? "",
                        trandate = trandate ?? "",
                        ReasonCode = ReasonCode ?? "",
                        Bin = bin ?? "",
                        Dimcode = Unit ?? "",
                        DOCID = DOCID ?? "",
                        company = company ?? "",
                        WH = WH ?? "",
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        //[HttpPost]
        //public IActionResult SavePayment(string docNumber, string DetailID, string PayaQTY, string Unit, string Createdate, string username, string lotnum, string partNum, string Plant, string trandate, string ReasonCode, string bin, string Dimcode, string company, string WH, string DOCID)
        //{
        //    try
        //    {
        //        var getId = _pigContext.UserRights
        //                .Where(u => u.UserName == username)
        //                .Select(u => u.UserId)
        //                .SingleOrDefault();

        //        using (var connection = new SqlConnection("Server=192.168.10.2;Database=BPIG;User Id=webapp;Password=web@pp; TrustServerCertificate=True;"))
        //        {
        //            connection.Open();
        //            lotnum = lotnum ?? "";

        //            // ตรวจสอบว่ามี DetailID อยู่แล้วหรือไม่
        //            var checkCommand = new SqlCommand(
        //                "SELECT COUNT(1) FROM BPIG.dbo.BOL_Paid WHERE DetailID = @DetailID and lotnum = @lotnum", connection);
        //            checkCommand.Parameters.AddWithValue("@DetailID", DetailID);
        //            checkCommand.Parameters.AddWithValue("@lotnum", lotnum);

        //            var exists = (int)checkCommand.ExecuteScalar() > 0;

        //            if (exists)
        //            {
        //                return Json(new { success = true, message = "Skipped duplicate DetailID." });
        //            }

        //            // INSERT ข้อมูลและดึง PaidID
        //            var insertCommand = new SqlCommand(
        //                "INSERT INTO BPIG.dbo.BOL_Paid (DetailID, QTY, Unit, CreateDate, CreateBy, lotnum) " +
        //                "OUTPUT INSERTED.PaidID " +
        //                "VALUES (@DetailID, @QTY, @Unit, @Createdate, @Createby, @lotnum)", connection);

        //            insertCommand.Parameters.AddWithValue("@DetailID", DetailID);
        //            insertCommand.Parameters.AddWithValue("@QTY", PayaQTY);
        //            insertCommand.Parameters.AddWithValue("@Unit", Unit);
        //            insertCommand.Parameters.AddWithValue("@Createdate", Createdate);
        //            insertCommand.Parameters.AddWithValue("@Createby", getId);
        //            insertCommand.Parameters.AddWithValue("@lotnum", lotnum);

        //            // ดึง PaidID
        //            var newPaidID = (long)insertCommand.ExecuteScalar();

        //            // อัปเดตสถานะใน BOL_DocDetail
        //            var updateDocDetailCommand = new SqlCommand(
        //                "UPDATE BPIG.dbo.BOL_DocDetail SET Status = 'P' " +
        //                "WHERE DocID = @DocID AND DetailID = @DetailID AND Partnum = @Partnum", connection);

        //            updateDocDetailCommand.Parameters.AddWithValue("@DocID", docNumber);
        //            updateDocDetailCommand.Parameters.AddWithValue("@DetailID", DetailID);
        //            updateDocDetailCommand.Parameters.AddWithValue("@Partnum", partNum);

        //            var rowsAffected = updateDocDetailCommand.ExecuteNonQuery();

        //            if (rowsAffected == 0)
        //            {
        //                return Json(new { success = false, message = "No matching record found in BOL_DocDetail to update." });
        //            }

        //            // ตรวจสอบสถานะใน BOL_DocDetail
        //            var checkstatus = new SqlCommand(
        //                "SELECT COUNT(1) FROM BPIG.dbo.BOL_DocDetail WHERE DocID = @docNumber AND (Status != 'P' OR Status IS NULL)", connection);
        //            checkstatus.Parameters.AddWithValue("@docNumber", docNumber);

        //            var output = (int)checkstatus.ExecuteScalar() == 0;

        //            if (output)
        //            {
        //                var updatedh = new SqlCommand(
        //                   "UPDATE BPIG.dbo.BOL_DocHead " +
        //                            "SET Status = 'P', UpdateDate = @Createdate " +
        //                            "WHERE DocID = @DocID AND (Status IS NULL OR Status != 'P')", connection);

        //                updatedh.Parameters.AddWithValue("@DocID", docNumber);
        //                updatedh.Parameters.AddWithValue("@Createdate", Createdate);

        //                var rowsstatus = updatedh.ExecuteNonQuery();

        //                if (rowsstatus == 0)
        //                {
        //                    return Json(new { success = false, message = "No matching record found in BOL_DocHead to update." });
        //                }

        //                return Json(new { success = true, hideButton = true, PaidID = newPaidID, DetailID = DetailID, lotnum = lotnum, QTY = PayaQTY, partNum = partNum, Plant = Plant, trandate = trandate, ReasonCode = ReasonCode, Bin = bin, Dimcode = Dimcode, DOCID = DOCID, company = company, WH = WH });
        //            }

        //            return Json(new { success = true, PaidID = newPaidID, DetailID = DetailID, lotnum = lotnum, QTY = PayaQTY, partNum = partNum, Plant = Plant, trandate = trandate, ReasonCode = ReasonCode, Bin = bin, Dimcode = Dimcode, DOCID = DOCID, company = company, WH = WH });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}


        [HttpPost]
        public IActionResult SaveCsvToServer([FromBody] CsvRequest data)
        {
            if (data == null || string.IsNullOrEmpty(data.CsvData) || string.IsNullOrEmpty(data.FileName))
            {
                return BadRequest("Invalid data");
            }

            // กำหนดเส้นทางไฟล์
            string directoryPath = @"\\192.168.10.2\ERPAutoDMT\Live\BPI IssueMisc\Input";
            //string directoryPath = @"C:\Users\USER069\Desktop\ทดสอบ_savecsv";

            string filePath = Path.Combine(directoryPath, data.FileName);

            // ตรวจสอบว่าไฟล์มีอยู่แล้วหรือไม่
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(data.FileName);
            string fileExtension = Path.GetExtension(data.FileName);
            int fileIndex = 1;

            // ถ้าไฟล์มีอยู่แล้ว ให้เพิ่ม (1), (2), ... ที่ชื่อไฟล์
            while (System.IO.File.Exists(filePath))
            {
                string newFileName = $"{fileNameWithoutExtension}({fileIndex}){fileExtension}";
                filePath = Path.Combine(directoryPath, newFileName);
                fileIndex++;
            }

            try
            {
                // บันทึกไฟล์ CSV
                System.IO.File.WriteAllText(filePath, data.CsvData);
                return Ok(new { message = "File saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving file: {ex.Message}");
            }
        }



        public async Task<IActionResult> GetFilteredData(DateTime Startdate, DateTime Stopdate, string status, string Company)
        {
            //string Company = "BPI";

            // Use proper SQL parameterization to avoid SQL injection attacks
            var data = await _StockOut.STRModelStockOuts
                .FromSqlInterpolated($"EXEC [recordpayments] {Company}, {Startdate}, {Stopdate}")
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
