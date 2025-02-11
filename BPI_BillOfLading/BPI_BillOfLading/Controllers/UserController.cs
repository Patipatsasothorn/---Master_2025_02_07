using BPI_BillOfLading.Models;
using BPI_BillOfLading.Models.Data;
using BPI_BillOfLading.Models.UserSetting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Controllers
{
    public class UserController : Controller
    {
        private readonly BpiLiveContext _context;
        private readonly BpigContext _pigContext;

        public UserController(BpiLiveContext context, BpigContext pigContext)
        {
            _context = context;
            _pigContext = pigContext;
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

            var getId = _pigContext.UserRights
                  .Where(u => u.UserName == user)
                  .Select(u => u.UserId)
                  .SingleOrDefault();

            //ViewBag.Username = getId;

            ViewBag.Company = company;
            ViewBag.Username = getId;

            //ViewData["Company"] = company;
            //ViewData["User"] = user;

            return View("Index");
        }

        [HttpGet]
        public IActionResult GetBuildings()
        {
            var getData = _pigContext.Deps.ToList();

            return Json(new { success = true, getData });
        }

        [HttpGet]
        public IActionResult GetApprovers()
        {
            var approverList = _pigContext.UserRights.
                Select(i => new
                {
                    i.UserId,
                    i.UserName,
                    i.Name

                }).ToList();

            return Json(new { success = true, approverList });
        }

        [HttpGet]
        public IActionResult GetReasons()
        {
            var reasonList = _context.Reasons
                .Where(i => (i.ReasonCode.StartsWith("D") || i.ReasonCode.StartsWith("W")) && i.ReasonType == "M")
                .GroupBy(i => new { i.ReasonCode, i.Description })
                .Select(g => new
                {
                    ReasonCode = g.Key.ReasonCode,
                    Description = g.Key.Description
                })
                .OrderBy(i => i.ReasonCode)
                .ToList();

            return Json(new { success = true, reasonList });
        }

        [HttpGet]
        public IActionResult GetWh()
        {
            var whList = _context.Warehses
                 //.Where(w => w.Company == "BPI")
                 .Select(w => new
                 {
                     w.WarehouseCode,
                     w.Description
                 })
                 .OrderBy(w => w.WarehouseCode)
                 .ToList();

            return Json(new { success = true, whList });
        }

        [HttpGet]
        public IActionResult SearchBuildings(string searchTerm)
        {
            var getUserId = _pigContext.UserRights
                 .Where(i => i.UserName == searchTerm)
                 .Select(i => i.UserId)
                 .FirstOrDefault();  // เปลี่ยนจาก SingleOrDefault() เป็น FirstOrDefault() เพื่อลดข้อผิดพลาดหากมีข้อมูลซ้ำ

            if (getUserId == 0)
            {
                return Json(new { success = false, message = "ไม่พบข้อมูลผู้ใช้ในตาราง BPIG BOL_UsersPolicy " });
            }

            var getData = from u in _pigContext.UserRights
                          join b in _pigContext.BolUsersPolicies on u.UserId equals b.UserId
                          join d in _pigContext.Deps on b.DataCode equals d.DepCode
                          where b.DataType == "DEP" && b.UserId == getUserId
                          select new
                          {
                              d.DepName,
                              b.RowId
                          };

            var resultData = getData.ToList();

            return Json(new { success = true, resultData });
        }

        [HttpGet]
        public IActionResult SearchApproved(string searchTerm)
        {
            var getUserId = _pigContext.UserRights
                .Where(i => i.UserName == searchTerm)
                .Select(i => i.UserId)
                .FirstOrDefault();  // เปลี่ยนจาก SingleOrDefault() เป็น FirstOrDefault() เพื่อลดข้อผิดพลาดหากมีข้อมูลซ้ำ

            if (getUserId == 0)
            {
                return Json(new { success = false, message = "ไม่พบข้อมูลผู้ใช้ในตาราง BPIG BOL_UsersPolicy " });
            }

            var getData = from u in _pigContext.UserRights
                          join b in _pigContext.BolUsersPolicies on u.UserName equals b.DataCode
                          where b.DataType == "APP" && b.UserId == getUserId
                          select new
                          {
                              u.UserName,
                              u.Name,
                              b.RowId
                          };

            var resultData = getData.ToList();

            return Json(new { success = true, resultData });
        }

        [HttpGet]
        public IActionResult SearchReasons(string searchTerm)
        {
            var username = _pigContext.UserRights
                .Where(i => i.UserName == searchTerm)
                .Select(i => i.UserId)
                .SingleOrDefault();

            if (username == 0)
            {
                return Json(new { success = false, message = "ไม่พบข้อมูลผู้ใช้ในตาราง BPIG BOL_UsersPolicy " });
            }

            var getData = _pigContext.ReasonModels
                .FromSqlInterpolated($"EXEC BPI_BillOfLoading_User {username}")
                .ToList();

            return Json(new { success = true, getData });
        }

        [HttpGet]
        public IActionResult SearchWh(string searchTerm)
        {
            var username = _pigContext.UserRights
                .Where(i => i.UserName == searchTerm)
                .Select(i => i.UserId)
                .SingleOrDefault();

            if (username == 0)
            {
                return Json(new { success = false, message = "ไม่พบข้อมูลผู้ใช้ในตาราง BPIG BOL_UsersPolicy " });
            }

            var getData = _pigContext.WhModels
                .FromSqlInterpolated($"EXEC BPI_BillOfLoading_Wh {username}")
                .ToList();

            return Json(new { success = true, getData });
        }

        [HttpPost]
        public IActionResult SaveBuildings([FromBody] List<BuildingDataModel> dataToSave)
        {
            try
            {
                foreach (var item in dataToSave)
                {
                    var getId = _pigContext.UserRights
                        .Where(i => i.UserName == item.Username)
                        .Select(i => i.UserId)
                        .SingleOrDefault();

                    var getDataCode = _pigContext.Deps
                        .Where(d => d.DepName == item.SelectedBuilding)
                        .Select(d => d.DepCode)
                        .ToList();

                    var saveData = new BolUsersPolicy
                    {
                        UserId = getId,
                        DataType = "DEP",
                        DataCode = getDataCode[0],
                        CredateDate = DateTime.Now,
                        CreateBy = long.Parse(item.UserId)
                    };

                    _pigContext.BolUsersPolicies.Add(saveData);
                }

                _pigContext.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SaveApprovers([FromBody] List<ApproverModel> approvers)
        {
            try
            {
                foreach (var items in approvers)
                {
                    var getId = _pigContext.UserRights
                        .Where(i => i.UserName == items.ApproverName)
                        .Select(i => i.UserId)
                        .SingleOrDefault();

                    var saveData = new BolUsersPolicy
                    {
                        UserId = getId,
                        DataType = "APP",
                        DataCode = items.ApproverId,
                        CredateDate = DateTime.Now,
                        CreateBy = long.Parse(items.UserId)
                    };

                    _pigContext.BolUsersPolicies.Add(saveData);
                }

                _pigContext.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SaveReasons([FromBody] List<ReasonModelGet> reason)
        {
            try
            {
                foreach (var items in reason)
                {
                    var getId = _pigContext.UserRights
                        .Where(i => i.UserName == items.ReasonName)
                        .Select(i => i.UserId)
                        .SingleOrDefault();

                    var saveData = new BolUsersPolicy
                    {
                        UserId = getId,
                        DataType = "REA",
                        DataCode = items.ReasonCode,
                        CredateDate = DateTime.Now,
                        CreateBy = long.Parse(items.UserId)
                    };

                    _pigContext.BolUsersPolicies.Add(saveData);
                }

                _pigContext.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SaveWh([FromBody] List<WhModelGet> wh)
        {
            try
            {
                foreach (var items in wh)
                {
                    var getId = _pigContext.UserRights
                        .Where(i => i.UserName == items.WhName)
                        .Select(i => i.UserId)
                        .SingleOrDefault();

                    var saveData = new BolUsersPolicy
                    {
                        UserId = getId,
                        DataType = "WAR",
                        DataCode = items.WhCode,
                        CredateDate = DateTime.Now,
                        CreateBy = long.Parse(items.UserId)
                    };

                    _pigContext.BolUsersPolicies.Add(saveData);
                }

                _pigContext.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteRow([FromBody] BuildingDataModel b)
        {
            try
            {
                var policyToDelete = _pigContext.BolUsersPolicies
                    .FirstOrDefault(p => p.RowId == b.RowId);

                if (policyToDelete != null)
                {
                    _pigContext.BolUsersPolicies.Remove(policyToDelete);
                    _pigContext.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "ไม่พบข้อมูลที่ต้องการลบ" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteApprovedRow([FromBody] ApproverModel a)
        {
            try
            {
                var policyToDelete = _pigContext.BolUsersPolicies
                    .FirstOrDefault(p => p.RowId == a.RowId);

                if (policyToDelete != null)
                {
                    _pigContext.BolUsersPolicies.Remove(policyToDelete);
                    _pigContext.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "ไม่พบข้อมูลที่ต้องการลบ" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteReasonRow([FromBody] ReasonModel r)
        {
            try
            {
                var policyToDelete = _pigContext.BolUsersPolicies
                    .FirstOrDefault(p => p.RowId == r.RowId);

                if (policyToDelete != null)
                {
                    _pigContext.BolUsersPolicies.Remove(policyToDelete);
                    _pigContext.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "ไม่พบข้อมูลที่ต้องการลบ" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteWhRow([FromBody] WhModelGet w)
        {
            try
            {
                var policyToDelete = _pigContext.BolUsersPolicies
                    .FirstOrDefault(p => p.RowId == w.RowId);

                if (policyToDelete != null)
                {
                    _pigContext.BolUsersPolicies.Remove(policyToDelete);
                    _pigContext.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "ไม่พบข้อมูลที่ต้องการลบ" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
