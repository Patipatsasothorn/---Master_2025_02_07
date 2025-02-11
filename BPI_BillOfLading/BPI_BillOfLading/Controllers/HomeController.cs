using BPI_BillOfLading.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BPI_BillOfLading.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string url)
        {
            try
            {
                //return RedirectToAction("Index", "PickUp");

                var uri = new Uri(url);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                string userName = query["UserName"];
                string company = query["Company"];

                var pathSegments = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                string controller = pathSegments.Length > 0 ? pathSegments[0] : "Unknown";

                if (string.IsNullOrEmpty(userName))
                {
                    return Redirect("https://webapp.bpi-concretepile.co.th:9020/Account/Login");
                }
                else
                {
                    return Redirect($"~/{controller}/Index?Company={company}&username={userName}");
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Exception: {ex.Message}" });
            }
        }
    }
}
