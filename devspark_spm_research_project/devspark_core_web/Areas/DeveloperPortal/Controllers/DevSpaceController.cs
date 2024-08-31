using devspark_core_business_layer.DeveloperPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_model.DeveloperPortalModels;
using devspark_core_model.SystemModels;
using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Areas.DeveloperPortal.Controllers
{
    public class DevSpaceController : Controller
    {
        private readonly ICreateDevSpace _createDevSpace;

        public DevSpaceController(ICreateDevSpace createDevSpace)
        {
            _createDevSpace = createDevSpace;
        }

        public async Task<IActionResult> HomeIndex()
        {
            return View();
        }

        public async Task<IActionResult> DevPage()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateDevSpace()
        {
            return View();
        }


        public async Task<IActionResult> UpdateFolder()
        {
            return View();
        }

        public async Task<IActionResult> UpdateFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevSpace(string folderName, string fileName, string language)
        {
            // Create a new FileModel
            var newFile = new FileModel
            {
                FileTitle = fileName,
                Language = language,
                CodeSnippet = GetDefaultCodeSnippet(language)  
            };

            
            var newFolder = new Folder
            {
                FolderTitle = folderName,
                Files = new List<FileModel> { newFile }
            };

            bool status = await _createDevSpace.CreateDevSpace(newFolder);

            if (status)
            {
                return RedirectToAction("HomeIndex");
            }

            return View();
        }

        private string GetDefaultCodeSnippet(string language)
        {
            
            var defaultCodes = new Dictionary<string, string>
            {
                { "java", "// Java default code snippet" },
                { "javascript", "// JavaScript default code snippet" },
                { "cpp", "// C++ default code snippet" },
                { "c", "// C default code snippet" },
                { "csharp", "// C# default code snippet" },
                { "python", "# Python default code snippet" }
            };

            return defaultCodes.ContainsKey(language) ? defaultCodes[language] : string.Empty;
        }
    }
}
