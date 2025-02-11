using BPI_UserSettings.Models;
using BPI_UserSettings.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_UserSettings.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly BpigContext _context;
        private readonly BpiDevContext _contextErp;

        public UserSettingsController(BpigContext context, BpiDevContext contextErp)
        {
            _context = context;
            _contextErp = contextErp;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetBuildings()
        {
            var getData = _context.Deps.ToList();

            return Json(new { success = true, getData });
        }

        [HttpGet]
        public IActionResult GetApprovers()
        {
            var approverList = _context.UserRights.
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
           var reasonList = _contextErp.Reasons
                .Where(i => i.Company == "BPI" && i.ReasonCode.Contains("W"))
                .Select(i => new 
                {
                    i.ReasonCode,
                    i.Description
                })
                .OrderBy(i => i.ReasonCode)
                .ToList();

           return Json(new { success = true, reasonList });
        }

        [HttpGet]
        public IActionResult SearchBuildings(string searchTerm) 
        {
            var getData = from u in _context.UserRights
                          join b in _context.BolUsersPolicies on u.UserId equals b.UserId
                          join d in _context.Deps on b.DataCode equals d.DepCode
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
            var getUserId = _context.UserRights
                .Where(i => i.UserName == searchTerm)
                .Select(i => i.UserId)
                .SingleOrDefault();

            var getData = from u in _context.UserRights
                          join b in _context.BolUsersPolicies on u.UserName equals b.DataCode
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
            var username = _context.UserRights
                .Where(i => i.UserName == searchTerm)
                .Select(i => i.UserId)
                .SingleOrDefault();

            var getData = _context.ReasonModels
                .FromSqlInterpolated($"EXEC BPI_BillOfLoading_User {username}")
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
                    var getId = _context.UserRights
                        .Where(i => i.UserName == item.Username)
                        .Select(i => i.UserId)
                        .SingleOrDefault();

                    long runningNum = 1;

                    var maxRowId = _context.BolUsersPolicies.Max(i => i.RowId);

                    if (maxRowId > 0)
                    {
                        runningNum = maxRowId + 1;
                    }

                    var saveData = new BolUsersPolicy
                    {
                        RowId = runningNum,
                        UserId = getId,
                        DataType = "DEP",
                        DataCode = "PL",
                        CredateDate = DateTime.Now,
                        //CreateBy = empId
                    };

                    _context.BolUsersPolicies.Add(saveData);
                }

                _context.SaveChanges();

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
                    var getId = _context.UserRights
                        .Where(i => i.UserName == items.ApproverName)
                        .Select(i => i.UserId)
                        .SingleOrDefault();

                    long runningNum = 1;

                    var maxRowId = _context.BolUsersPolicies.Max(i => i.RowId);

                    if (maxRowId > 0)
                    {
                        runningNum = maxRowId + 1;
                    }

                    var saveData = new BolUsersPolicy
                    {
                        RowId = runningNum,
                        UserId = getId,
                        DataType = "APP",
                        DataCode = items.ApproverId,
                        CredateDate = DateTime.Now,
                    };

                    _context.BolUsersPolicies.Add(saveData);
                }

                _context.SaveChanges();

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
                    var getId = _context.UserRights
                        .Where(i => i.UserName == items.ReasonName)
                        .Select(i => i.UserId)
                        .SingleOrDefault();

                    long runningNum = 1;

                    var maxRowId = _context.BolUsersPolicies.Max(i => i.RowId);

                    if (maxRowId > 0)
                    {
                        runningNum = maxRowId + 1;
                    }

                    var saveData = new BolUsersPolicy
                    {
                        RowId = runningNum,
                        UserId = getId,
                        DataType = "REA",
                        DataCode = items.ReasonCode,
                        CredateDate = DateTime.Now,
                    };

                    _context.BolUsersPolicies.Add(saveData);
                }

                _context.SaveChanges();

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
                var policyToDelete = _context.BolUsersPolicies
                    .FirstOrDefault(p => p.RowId == b.RowId);

                if (policyToDelete != null)
                {
                    _context.BolUsersPolicies.Remove(policyToDelete);
                    _context.SaveChanges();
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
                var policyToDelete = _context.BolUsersPolicies
                    .FirstOrDefault(p => p.RowId == a.RowId);

                if (policyToDelete != null)
                {
                    _context.BolUsersPolicies.Remove(policyToDelete);
                    _context.SaveChanges();
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
                var policyToDelete = _context.BolUsersPolicies
                    .FirstOrDefault(p => p.RowId == r.RowId);

                if (policyToDelete != null)
                {
                    _context.BolUsersPolicies.Remove(policyToDelete);
                    _context.SaveChanges();
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
