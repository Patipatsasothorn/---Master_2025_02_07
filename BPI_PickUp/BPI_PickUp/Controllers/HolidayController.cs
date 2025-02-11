using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Controllers
{
    public class HolidayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
