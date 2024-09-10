using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Areas.LearnerPortal.Controllers
{
    public class DashboardController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
