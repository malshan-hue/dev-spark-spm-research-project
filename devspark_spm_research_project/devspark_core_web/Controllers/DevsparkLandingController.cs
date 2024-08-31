using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Controllers
{
    public class DevsparkLandingController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> DevSparkHome()
        {
            return View();
        }
    }
}
