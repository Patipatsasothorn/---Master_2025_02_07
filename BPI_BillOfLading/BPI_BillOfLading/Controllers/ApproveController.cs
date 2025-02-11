using BPI_BillOfLading.context;
using BPI_BillOfLading.Models;
using BPI_BillOfLading.Models.ApproveModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BPI_BillOfLading.Controllers
{
    public class ApproveController : Controller
    {
        private readonly BpiLiveContext _context;
        private readonly BpigContext _pigContext;
        private readonly STRContext _STR;
        private readonly TableBillContext _TBL;
        private readonly ApproveContext _APP;

        public ApproveController(BpigContext bpigContext, BpiLiveContext context, STRContext STR, TableBillContext TBL, ApproveContext APP)
        {
            _pigContext = bpigContext;
            _context = context;
            _STR = STR;
            _TBL = TBL;
            _APP = APP;
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

        public async Task<IActionResult> GetFilteredData(DateTime startDate, DateTime endDate, string status, string username, string Company)
        {
            // เริ่มต้นด้วยการกรองตามวันที่
            //var query = _pigContext.BolDocHeads
            //    .Where(d => d.DocDate >= startDate && d.DocDate <= endDate);

            //// ตรวจสอบสถานะและปรับปรุงคิวรี
            //if (status != "ALL")
            //{
            //    query = query.Where(d => d.Status == status);
            //}

            // ดึงข้อมูลตามคิวรีที่กำหนด
            //var data = await query.ToListAsync();
            //var query = _pigContext.BolDocHeads
            //    .Where(d => d.DocDate >= startDate && d.DocDate <= endDate)
            //    .Where(d =>
            //        (status == "ALL" && new[] { "A", "W", "E", "U" }.Contains(d.Status)) ||
            //        (status != "ALL" && d.Status == status)
            //    );

            //// ดึงข้อมูลจากฐานข้อมูล
            //var data = await query.ToListAsync();

            var query = _APP.ApproveModels
                  .FromSqlInterpolated($"EXEC Approve {startDate},{endDate},{status},{username},{Company}")
                      .ToList();
            //ส่งข้อมูลในรูปแบบ JSON
            return Json(query);
        }

        [HttpGet("Approve/Privacy/{docId?}")]
        public IActionResult Privacy(string docId, string date, string Status, string Company)
        {
            ViewData["docId"] = docId;
            ViewData["date"] = date;
            ViewData["Status"] = Status;
            ViewData["Company"] = Company;

            return View();
        }

        [HttpGet("Approve/GetBillOfLading")]
        public IActionResult BillOfLading(string docNumber, string Company)
        {


            var query = _STR.STRModels
                    .FromSqlInterpolated($"EXEC BillOfLading {docNumber},{""},{Company}")
                        .ToList();
            return Json(query);
        }

        [HttpGet("Approve/TableBill")]
        public IActionResult TableBill(string docNumber, string Company)
        {


            var query = _TBL.TableBillModels
                    .FromSqlInterpolated($"EXEC TableBill {docNumber},{Company}")
                        .ToList();
            return Json(query);
        }

        [HttpPost]
        public IActionResult UpdateStatus(long docNumber, string status, string BEcuse)
        {
            if (docNumber <= 0 || string.IsNullOrEmpty(status))
            {
                return Json(new { success = false, message = "Invalid input parameters." });
            }

            try
            {
                // อัปเดตสถานะใน BolDocHead
                var document = _pigContext.BolDocHeads.SingleOrDefault(d => d.DocId == docNumber);

                if (document == null)
                {
                    return Json(new { success = false, message = "Document not found." });
                }

                document.Status = status;
                document.Remark = BEcuse; // อัปเดต Remark ด้วยค่า BEcuse

                // อัปเดตสถานะใน BolDocDetail
                var documentDetails = _pigContext.BolDocDetails.Where(d => d.DocId == docNumber).ToList();
                if (documentDetails.Any())
                {
                    foreach (var detail in documentDetails)
                    {
                        detail.Status = status; // อัปเดตสถานะในแต่ละรายการ

                    }
                }

                // บันทึกการเปลี่ยนแปลงในฐานข้อมูล
                _pigContext.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return Json(new { success = false, message = "Internal server error: " + ex.Message });
            }
        }

    }
}