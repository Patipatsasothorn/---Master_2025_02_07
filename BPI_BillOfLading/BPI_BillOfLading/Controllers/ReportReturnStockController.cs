using BPI_BillOfLading.Models;
using BPI_BillOfLading.Models.ReportModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace BPI_BillOfLading.Controllers
{
    public class ReportReturnStockController : Controller
    {
        private readonly BpiLiveContext _context;
        private readonly ReportReturnStockOutContext _store;
        public ReportReturnStockController(BpiLiveContext context, ReportReturnStockOutContext store)
        {
            _context = context;
            _store = store;
        }

        public IActionResult Index(string Company, string UserName)
        {
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
        public IActionResult GetPlants()
        {
            var getPlant = _context.Plants
                .GroupBy(p => p.Plant1)
                .Select(g => new
                {
                    Plant = g.Key
                })
                .ToList();

            return Json(new { success = true, plants = getPlant });
        }

        [HttpPost]
        public IActionResult GetData(string company, string plant, string beginDate, string endDate)
        {
            DateTime convertDateStart = DateTime.ParseExact(beginDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime convertDateStop = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var stopDate = convertDateStop.ToString("yyyyMMdd");
            var startDate = convertDateStart.ToString("yyyyMMdd");

            var getData = _store.ReportReturnStockOutModels
                .FromSqlInterpolated($"EXEC reportruturn {company},{plant},{startDate}, {stopDate}")
                .ToList();

            if (getData == null || !getData.Any())
            {
                return Json(new { success = false, message = "No data found." });
            }

            return Json(new { success = true, pickUp = getData });
        }

    }
}
