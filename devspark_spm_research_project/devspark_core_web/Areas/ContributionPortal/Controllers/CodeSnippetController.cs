using devspark_core_model.ContributionPortalModels;
using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Areas.ContributionPortal.Controllers
{
    public class CodeSnippetController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CodeSnippet codesnippet)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CodeSnippet codesnippet)
        {
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(CodeSnippet codesnippet)
        {
            return View();
        }
    }
}
