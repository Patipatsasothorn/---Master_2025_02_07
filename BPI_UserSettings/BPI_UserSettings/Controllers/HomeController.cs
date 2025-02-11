using BPI_UserSettings.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BPI_UserSettings.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "UserSettings");
        }

    }
}
