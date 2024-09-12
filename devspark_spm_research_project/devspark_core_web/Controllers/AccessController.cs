using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_model.SystemModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace devspark_core_web.Controllers
{
    public class AccessController : Controller
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly IUserService _userService;
        private readonly IMicrosoftGraphService _MicrosoftGraphService;

        public AccessController(IUserService userService, GraphServiceClient graphServiceClient, IMicrosoftGraphService microsoftGraphService)
        {
            _userService = userService;
            _graphServiceClient = graphServiceClient;
            _MicrosoftGraphService = microsoftGraphService;
        }

        //signin view page
        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DevSparkHome", "DevsparkLanding");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MicrosoftSignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        }

        //register view page
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        //register  post action where data will capture
        [HttpPost]
        public async Task<IActionResult> Register(DevSparkUser user)
        {
            bool status = await _MicrosoftGraphService.CreateUserInMicrosoftEntraId(user);

            if (status)
            {
                return RedirectToAction("SignIn");
            }

            return View();
        }
    }
}
