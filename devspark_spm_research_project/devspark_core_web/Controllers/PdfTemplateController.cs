using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Controllers
{
    public class PdfTemplateController : Controller
    {
        public IActionResult CourseTemplate()
        {
            return View();
        }

        public IActionResult Answers()
        {
            return View();
        }
    }
}
