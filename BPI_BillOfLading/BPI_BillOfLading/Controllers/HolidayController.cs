using BPI_BillOfLading.Models;
using BPI_BillOfLading.Models.Data;
using BPI_BillOfLading.Models.HolidayModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OfficeOpenXml;
using System.Globalization;

namespace BPI_BillOfLading.Controllers
{
    public class HolidayController : Controller
    {
        private readonly BpiLiveContext _context;
        private readonly BpigContext _pigContext;

        public HolidayController(BpiLiveContext context, BpigContext bpigContext)
        {
            _context = context;
            _pigContext = bpigContext;
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

        [HttpPost]
        public IActionResult SaveData([FromBody] SetHolidayModel model)
        {
            try
            {
                var getId = _pigContext.UserRights
                    .Where(u => u.UserName == model.Username)
                    .Select(u => u.UserId)
                    .SingleOrDefault();

                foreach (var items in model.TableData)
                {
                    if (!string.IsNullOrWhiteSpace(items.Company) &&
                        items.DateReq != null &&
                        !string.IsNullOrWhiteSpace(items.Description))
                    {
                        _pigContext.Holidays.Add(new Holiday
                        {
                            Company = items.Company,
                            Holiday1 = items.DateReq,
                            Detail = items.Description,
                            CreateDate = DateTime.Now,
                            CreateBy = getId
                        });
                    }
                }

                _pigContext.SaveChanges();

                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : null;

                return Json(new { success = false, message = innerExceptionMessage });
            }
        }

        [HttpGet]
        public IActionResult SearchData(string company, int year)
        {
            var holidays = _pigContext.Holidays
                .Where(h => h.Company == company && h.Holiday1.HasValue && h.Holiday1.Value.Year == year)
                .Select(h => new
                {
                    Holiday1 = h.Holiday1.HasValue ? h.Holiday1.Value.ToString("dd/MM/yyyy") : null, // แปลงรูปแบบวันที่
                    h.Detail
                })
                .ToList();

            return Json(new { success = true, holidays });
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile file, string company, string yearSelect)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var holidays = new List<TableDataModel>();
            var duplicates = new List<TableDataModel>(); // สร้าง List สำหรับข้อมูลซ้ำ

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var excelCompany = worksheet.Cells[row, 1].Text; // อ่านค่าบริษัทจากไฟล์ Excel
                        var excelYear = worksheet.Cells[row, 2].Text; // อ่านค่าบริษัทจากไฟล์ Excel

                        DateTime excelDate = DateTime.ParseExact(excelYear, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        var yearFromExcel = excelDate.Year.ToString();

                        // ตรวจสอบว่า company ตรงกันหรือไม่
                        if (excelCompany != company)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "ข้อมูลบริษัทในไฟล์ไม่ตรงกับบริษัทที่เลือก โปรดตรวจสอบ",
                                param = "f"
                            });
                        }
                        else if (yearFromExcel != yearSelect)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "ข้อมูลปีในไฟล์ ไม่ตรงกับปีที่เลือก โปรดตรวจสอบ",
                                param = "f"
                            });
                        }

                        var holiday = new TableDataModel
                        {
                            Company = excelCompany,
                            DateReq = DateOnly.Parse(excelYear),
                            Description = worksheet.Cells[row, 3].Text
                        };

                        DateTime convertDateStart = DateTime.ParseExact(holiday.DateReq.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string startDate = convertDateStart.ToString("yyyy-MM-dd");

                        var checkData = _pigContext.Holidays
                            .Where(x => x.Company == holiday.Company && x.Holiday1 == DateOnly.Parse(startDate))
                            .Select(x => new
                            {
                                x.Company,
                                Holiday1 = x.Holiday1.HasValue ? x.Holiday1.Value.ToString("dd/MM/yyyy") : null, // แปลงรูปแบบวันที่
                            })
                            .ToList();

                        holidays.Add(holiday);

                        if (checkData.Count > 0)
                        {
                            if (checkData.Count > 0)
                            {
                                duplicates.Add(holiday); // เก็บข้อมูลซ้ำ
                            }
                            else
                            {
                                holidays.Add(holiday); // เพิ่มข้อมูลใหม่
                            }
                        }
                    }
                }
            }

            if (duplicates.Count > 0)
            {
                return Json(new
                {
                    success = false,
                    message = "ข้อมูลเดิมจะถูกลบ แน่ใจหรือไม่?",
                    data = holidays,
                    duplicates
                });
            }

            return Json(new { success = true, data = holidays });
        }

        [HttpPost]
        public IActionResult DeleteDuplicates([FromBody] List<TableDataModel> duplicates)
        {
            foreach (var duplicate in duplicates)
            {
                var existingHoliday = _pigContext.Holidays
                    .FirstOrDefault(x => x.Company == duplicate.Company && x.Holiday1 == duplicate.DateReq);
                if (existingHoliday != null)
                {
                    _pigContext.Holidays.Remove(existingHoliday);
                }
            }
            _pigContext.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult DeleteRow(string company, string dateReq, string description)
        {
            try
            {
                var convertDate = DateOnly.ParseExact(dateReq, "dd/MM/yyyy", null);

                // ค้นหาและลบข้อมูลในฐานข้อมูล
                var holiday = _pigContext.Holidays
                    .FirstOrDefault(h => h.Company == company
                        && h.Holiday1 == convertDate
                        && h.Detail == description);

                if (holiday != null)
                {
                    _pigContext.Holidays.Remove(holiday);
                    _pigContext.SaveChanges();

                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "ไม่พบข้อมูลที่ต้องการลบ" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
