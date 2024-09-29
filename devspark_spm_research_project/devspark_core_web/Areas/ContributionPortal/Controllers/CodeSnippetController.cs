using devspark_core_business_layer.ContributionPortalService.Interfaces;
using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_model.ContributionPortalModels;
using devspark_core_model.LearnerPortalModels;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.DataProtection;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var snippets = await _codeSnippetService.GetAllCodeSnippets();
            return View(snippets);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CodeSnippetLibrary codesnippet)
        {
            bool status = await _codeSnippetService.InsertCodeSnippet(codesnippet);
            if (status)
            {
                return RedirectToAction("Index", "CodeSnippet");
            }
            return View();
        }

        // Edit (GET): Fetch the code snippet by ID and pass it to the view
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var snippet = await _codeSnippetService.GetCodeSnippetById(id);
            if (snippet == null)
            {
                return NotFound();
            }
            return View(snippet);
        }

        // Edit (POST): Update the code snippet
        [HttpPost]
        public async Task<IActionResult> Edit(CodeSnippetLibrary codesnippet)
        {
            if (!ModelState.IsValid)
            {
                return View(codesnippet);
            }

            bool status = await _codeSnippetService.UpdateCodeSnippet(codesnippet);
            if (status)
            {
                return RedirectToAction("Index", "CodeSnippet");
            }

            return View(codesnippet);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // Call the service layer to delete the code snippet by id
            bool status = await _codeSnippetService.DeleteCodeSnippet(id);

            if (status)
            {
                // If deletion is successful, redirect to the index view
                return RedirectToAction("Index");
            }

            // If deletion fails, return to the view with an error
            return View("Error", new { message = "Error deleting the code snippet" });
        }

        [HttpPost]
        public async Task<ActionResult> GenerateCodeSnippetPdf(int id)
        {
            var snippet = await _codeSnippetService.GetCodeSnippetById(id);

            string htmlContent = await ITextSharpPdfHelper.RenderViewToStringAsync(this, "CodeLibrarySnippetTemplate", snippet);

            byte[] pdfBytes = ITextSharpPdfHelper.GeneratePdfFromHtml(htmlContent);

            var pdfName = snippet.Title + ".pdf";

            // Return the PDF as a file download
            return File(pdfBytes, "application/pdf", pdfName);
        }
    }
}
