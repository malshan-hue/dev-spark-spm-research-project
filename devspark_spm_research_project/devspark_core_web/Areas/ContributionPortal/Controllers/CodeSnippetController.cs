using devspark_core_business_layer.ContributionPortalService.Interfaces;
using devspark_core_model.ContributionPortalModels;
using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Areas.ContributionPortal.Controllers
{
    [Area("ContributionPortal")]
    public class CodeSnippetController : Controller
    {
        private readonly ICodeSnippetService _codeSnippetService;

        private static readonly HttpClient client = new HttpClient();

        public CodeSnippetController(ICodeSnippetService codeSnippetService)
        {
            _codeSnippetService = codeSnippetService;
        }

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
        public async Task<IActionResult> Create(CodeSnippet codesnippet)
        {
            bool status = await _codeSnippetService.InsertCodeSnippet(codesnippet);
            if (status)
            {
                return RedirectToAction("Index", "CodeSnippet");
            }
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
