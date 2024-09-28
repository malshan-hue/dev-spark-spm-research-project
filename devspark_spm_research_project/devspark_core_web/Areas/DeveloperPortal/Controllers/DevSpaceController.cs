using Azure.Core;
using devspark_core_business_layer.DeveloperPortalService.Interfaces;
using devspark_core_model.DeveloperPortalModels;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static NuGet.Packaging.PackagingConstants;


namespace devspark_core_web.Areas.DeveloperPortal.Controllers
{
    [Authorize]
    [Area("DeveloperPortal")]
    public class DevSpaceController : Controller 
    {
        private readonly ICreateDevSpace _createDevSpace;
        private static readonly HttpClient client = new HttpClient();

        public DevSpaceController(ICreateDevSpace createDevSpace)
        {
            _createDevSpace = createDevSpace;
        }

        public async Task<IActionResult> HomeIndex()
        {
            ICollection<devspark_core_model.DeveloperPortalModels.Folder> folders = new List<devspark_core_model.DeveloperPortalModels.Folder>();

            var allDevSpaces = await _createDevSpace.GetDevSpaces(); // This retrieves all folders
            int? userId = HttpContext.Session.GetInt32("userId");

            foreach (var folder in allDevSpaces)
            {
                if(folder.UserId == (int)userId)
                {
                    folders.Add(folder);
                }
                
            }

            // Pass the list of folders to the view
            return View(folders);

        }

        public async Task<IActionResult> DevPage(int Folder_id, int File_id, string File_Title, string File_Language, string File_CodeSnippet)
        {
            var viewModel = new DevPageViewModel
            {
                Folder_id = Folder_id,
                File_id = File_id,
                File_Title = File_Title,
                File_Language = File_Language,
                File_CodeSnippet = File_CodeSnippet
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateDevSpace()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevSpace(string folderName, string fileName, string language)
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            // Create a new FileModel
            var newFile = new FileModel
            {
                FileTitle = fileName,
                Language = language,
                Extension = GetFileExtensionsMapping(language),
                CodeSnippet = GetDefaultCodeSnippet(language),
                IsNew = true,
                UserId = (int)userId
            };

            var allDevSpaces = await _createDevSpace.GetDevSpaces(); // This retrieves all folders

            // Check if the folder already exists
            var folder = allDevSpaces.FirstOrDefault(f => f.FolderTitle == folderName);

            if (folder != null)
            {
                // Append the new file to the existing folder's file list
                folder.Files.Add(newFile);

                // Update the folder and its files in the database
                bool status = await _createDevSpace.UpdateDevSpace(folder); // Assuming UpdateDevSpace method is used for updates
                if (status)
                {
                    return RedirectToAction("HomeIndex");
                }
            }
            else
            {
                // If the folder doesn't exist, create a new folder with the new file
                var newFolder = new devspark_core_model.DeveloperPortalModels.Folder
                {
                    FolderTitle = folderName,
                    Files = new List<FileModel> { newFile },
                    UserId = (int)userId
                };

                // Insert the new folder and its files into the database
                bool status = await _createDevSpace.CreateDevSpace(newFolder);

                if (status)
                {
                    return RedirectToAction("HomeIndex");
                }
            }

            // If the operation fails, return the same view
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFolder(int folderId)
        {
            // Use folderId to initialize or pass data to the view
            ViewBag.FolderId = folderId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFolder(int folderId, string folderName)
        {
            var allDevSpaces = await _createDevSpace.GetDevSpaces(); // This retrieves all folders

            // Check if the folder already exists
            var folder = allDevSpaces.FirstOrDefault(f => f.Id == folderId);

            if (folder != null)
            {
                folder.FolderTitle = folderName;

                // update folder
                bool status = await _createDevSpace.UpdateFolder(folder); // Assuming UpdateDevSpace method is used for updates
                if (status)
                {
                    return RedirectToAction("HomeIndex");
                }

            }
            else
            {
                // If the operation fails, return the same view
                return View();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFile(int fileId, int folderId)
        {
            // Use folderId to initialize or pass data to the view
            ViewBag.fileId = fileId;
            ViewBag.folderId = folderId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFile(int fileId, int folderId, string fileName)
        {
            var allDevSpaces = await _createDevSpace.GetDevSpaces(); // This retrieves all folders

            // Check if the folder already exists
            var folder = allDevSpaces.FirstOrDefault(f => f.Id == folderId);
            var file = folder.Files.FirstOrDefault(f => f.Id == fileId);

            if (file != null)
            {
                file.FileTitle = fileName;

                // update folder
                bool status = await _createDevSpace.UpdateFile(file); // Assuming UpdateDevSpace method is used for updates
                if (status)
                {
                    return RedirectToAction("HomeIndex");
                }

            }
            else
            {
                // If the operation fails, return the same view
                return View();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateNewFolder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewFolder(string folderName)
        {
            var allDevSpaces = await _createDevSpace.GetDevSpaces(); // This retrieves all folders

            // Check if the folder already exists
            var folder = allDevSpaces.FirstOrDefault(f => f.FolderTitle == folderName);
            int? userId = HttpContext.Session.GetInt32("userId");

            if (folder != null)
            {
                // If the operation fails, return the same view
                return RedirectToAction("HomeIndex");

            }
            else
            {
                // If the folder doesn't exist, create a new folder with the new file
                var newFolder = new devspark_core_model.DeveloperPortalModels.Folder
                {
                    FolderTitle = folderName,
                    UserId = (int)userId
                };

                // create new folder
                bool status = await _createDevSpace.CreateNewFolder(newFolder); // Assuming UpdateDevSpace method is used for updates
                if (status)
                {
                    return RedirectToAction("HomeIndex");
                }
            }

            // If the operation fails, return the same view
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateNewFile(int folderId)
        {
            // Use folderId to initialize or pass data to the view
            ViewBag.FolderId = folderId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewFile(int folderId, string fileName, string language)
        {
            // Create a new FileModel
            var newFile = new FileModel
            {
                FolderId = folderId,
                FileTitle = fileName,
                Language = language,
                Extension = GetFileExtensionsMapping(language),
                CodeSnippet = GetDefaultCodeSnippet(language),
                IsNew = true
            };

            bool status = await _createDevSpace.CreateNewFile(newFile); // Assuming UpdateDevSpace method is used for updates
            if (status)
            {
                return RedirectToAction("HomeIndex");
            }

            return View();
        }

        [HttpPost]
        [Route("api/submit")]
        public async Task<IActionResult> SubmitCode([FromBody] CodeSubmissionRequest request)
        {
            var requestBody = new
            {
                language_id = request.LanguageId,
                source_code = request.SourceCode,
                stdin = request.Stdin
            };

            var apiRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://judge0-ce.p.rapidapi.com/submissions?fields=*"),
                Headers =
                {
                    { "x-rapidapi-key", "7204430a99msh429d38d58e7c526p15c2bcjsn930ca895f5ff" },
                    { "x-rapidapi-host", "judge0-ce.p.rapidapi.com" },
                },
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };
            //7779012381mshe167561007d0a2dp16d290jsn80e8e2bec821
            //837584a094msh74f33cabdc0fcffp1933fdjsn5a335fb0eddd
            //7204430a99msh429d38d58e7c526p15c2bcjsn930ca895f5ff
            try
            {
                using (var response = await client.SendAsync(apiRequest))
                {
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var responseJson = JObject.Parse(responseBody);
                    var submissionId = responseJson["token"]?.ToString();

                    // Return data using ActionResult
                    return Ok(new { SubmissionId = submissionId });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error submitting code: " + ex.Message);
                return null;
            }
        }

        [HttpGet]
        [Route("api/result/{submissionId}")]
        public async Task<IActionResult> GetResult(string submissionId)
        {
            const int statusIdCompleted = 3; // Status ID for completed submission
            const int statusIdRunning = 1; // Status ID for running
            const int statusIdQueued = 2; // Status ID for queued

            bool isCompleted = false;
            string finalResult = string.Empty;

            while (!isCompleted)
            {
                // Wait before polling again
                await Task.Delay(2000); // 2 seconds delay between polls

                var result = await FetchSubmissionStatusAsync(submissionId);
                if (result != null)
                {
                    var statusId = result["status"]["id"]?.ToObject<int>() ?? 0;
                    Console.WriteLine($"Current Status ID: {statusId}");

                    if (statusId == statusIdCompleted)
                    {
                        isCompleted = true;
                        // Fetch and display the final result
                        var finalResultJson = await FetchSubmissionResultAsync(submissionId);

                        // Convert JObject to a string representation
                        finalResult = finalResultJson?.ToString() ?? "No result returned.";
                        return Ok(new { FinalResult = finalResult });
                    }
                    else if (statusId != statusIdRunning && statusId != statusIdQueued)
                    {
                        // If the status is neither running nor queued, stop polling and show result
                        var finalResultJson = await FetchSubmissionResultAsync(submissionId);

                        // Convert JObject to a string representation
                        finalResult = finalResultJson?.ToString() ?? "No result returned.";
                        return Ok(new { FinalResult = finalResult });
                    }
                    // Otherwise, continue polling
                }

            }
            return Ok(new { FinalResult = finalResult });
        }

        [HttpPost]
        [Route("api/save")]
        public async Task<IActionResult> SaveCode([FromBody] SaveCodeRequest request)
        {
            try
            {
                var allDevSpaces = await _createDevSpace.GetDevSpaces();

                // Find the folder by its ID
                var folder = allDevSpaces.FirstOrDefault(f => f.Id == request.FolderId);
                if (folder == null)
                {
                    return NotFound(new { success = false, message = "Folder not found." });
                }

                // Find the file by its ID
                var file = folder.Files.FirstOrDefault(f => f.Id == request.fileId);
                if (file == null)
                {
                    return NotFound(new { success = false, message = "File not found." });
                }

                // Update file properties with new values
                file.FileTitle = request.FileTitle;
                file.Language = request.Language;
                file.Extension = GetFileExtensionsMapping(request.Language);
                file.CodeSnippet = request.SourceCode;

                // Call the method to update the file in the database
                bool status = await _createDevSpace.UpdateFileInfo(file); // Assuming it will update the folder and its files

                if (status)
                {
                    // Return a success response
                    return Ok(new { success = true, message = "Code saved successfully." });
                }
                else
                {
                    // Return a failure response if the database update fails
                    return StatusCode(500, new { success = false, message = "Failed to update the database." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, new { success = false, message = "An error occurred while saving the code." });
            }
        }

        [HttpPost]
        [Route("api/deleteFolder")]
        public async Task<IActionResult> DeleteFolder([FromBody] SaveCodeRequest request)
        {
            try
            {
                var allDevSpaces = await _createDevSpace.GetDevSpaces();
                var folder = allDevSpaces.FirstOrDefault(f => f.Id == request.FolderId);

                if (folder != null)
                {
                    // Call the method to delete the folder in the database
                    bool status = await _createDevSpace.DeleteFolder(request.FolderId);

                    if (status)
                    {
                        // Return a success response
                        return Ok(new { success = true, message = "Folder Deleted successfully." });
                    }
                }

                return StatusCode(500, new { success = false, message = "Failed to delete the folder." });
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the folder." });
            }
        }

        [HttpPost]
        [Route("api/deleteFile")]
        public async Task<IActionResult> DeleteFile([FromBody] SaveCodeRequest request)
        {
            try
            {
                var allDevSpaces = await _createDevSpace.GetDevSpaces();
                var folder = allDevSpaces.FirstOrDefault(f => f.Id == request.FolderId);
                var file = folder.Files.FirstOrDefault(f => f.Id == request.fileId);

                if (file != null)
                {
                    // Call the method to delete the file in the database
                    bool status = await _createDevSpace.DeleteFile(request.fileId);

                    if (status)
                    {
                        // Return a success response
                        return Ok(new { success = true, message = "Folder Deleted successfully." });
                    }
                }

                return StatusCode(500, new { success = false, message = "Failed to delete the folder." });
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the folder." });
            }
        }

        static async Task<JObject> FetchSubmissionStatusAsync(string submissionId)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://judge0-ce.p.rapidapi.com/submissions/{submissionId}?base64_encoded=true&fields=*"),
                Headers =
            {
                { "x-rapidapi-key", "7204430a99msh429d38d58e7c526p15c2bcjsn930ca895f5ff" },
                { "x-rapidapi-host", "judge0-ce.p.rapidapi.com" },
            },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return JObject.Parse(body);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching submission status: " + ex.Message);
                return null;
            }
        }

        static async Task<JObject> FetchSubmissionResultAsync(string submissionId)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://judge0-ce.p.rapidapi.com/submissions/{submissionId}?base64_encoded=true&fields=*"),
                Headers =
            {
                { "x-rapidapi-key", "7204430a99msh429d38d58e7c526p15c2bcjsn930ca895f5ff" },
                { "x-rapidapi-host", "judge0-ce.p.rapidapi.com" },
            },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Submission Result: " + body);
                    return JObject.Parse(body);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching submission result: " + ex.Message);
                return null;
            }
        }

        public class CodeSubmissionRequest
        {
            public int LanguageId { get; set; }
            public string SourceCode { get; set; }
            public string Stdin { get; set; }
        }

        public class SaveCodeRequest
        {
            public string FileTitle { get; set; }
            public int FolderId { get; set; }
            public int fileId { get; set; }
            public string Language { get; set; }
            public string SourceCode { get; set; }
        }

        private string GetDefaultCodeSnippet(string language)
        {
            
            var defaultCodes = new Dictionary<string, string>
            {
                { "java", "public class Main {\r\n    public static void main(String[] args) {\r\n        System.out.println(\"Hello, World!\");\r\n    }\r\n}" },
                { "javascript", "// Define a function to print Hello, World!\r\nfunction printMessage() {\r\n    const message = \"Hello, World!\";\r\n    console.log(message);\r\n}\r\n\r\n// Call the function\r\nprintMessage();" },
                { "cpp", "#include <iostream>\r\nusing namespace std;\r\n\r\nint main() {\r\n    int a, b;\r\n    cin>>a>>b;\r\n    int sum = 0;\r\n    for(int i = a; i <= b; i++){\r\n        sum += i;\r\n    }\r\n    cout << \"Sum between a and b is \"<< sum;\r\n    return 0;\r\n}" },
                { "c", "#include <stdio.h>\r\n\r\nint main() {\r\n    printf(\"Hello, World!\");\r\n    return 0;\r\n}" },
                { "csharp", "using System;\r\n\r\nclass HelloWorld {\r\n    static void Main() {\r\n        Console.WriteLine(\"Hello, World!\");\r\n    }\r\n}" },
                { "python", "# Define a function to print Hello, World!\r\ndef print_message():\r\n    message = \"Hello, World!\"\r\n    print(message)\r\n\r\n# Call the function\r\nprint_message()" }
            };

            return defaultCodes.ContainsKey(language) ? defaultCodes[language] : string.Empty;
        }

        private string GetFileExtensionsMapping(string language)
        {
            var extensions = new Dictionary<string, string>
            {
                { "java", "java" },
                { "javascript", "js" },
                { "cpp", "cpp" },
                { "c", "c" },
                { "csharp", "cs" },
                { "python", "py" }
            };

            return extensions.ContainsKey(language) ? extensions[language] : string.Empty;
        }
    }
}
