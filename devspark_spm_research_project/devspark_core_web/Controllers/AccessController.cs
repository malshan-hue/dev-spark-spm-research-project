using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_model.SystemModels;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using System.Security.Claims;

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
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MicrosoftSignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/Access/SignInSuccess" });
        }

        [HttpGet]
        public async Task<IActionResult> SignInSuccess()
        {
            #region SYSTEM NOTIFICATION

            List<SystemNotification> systemNotifications = new List<SystemNotification>();

            SystemNotification systemNotification = new SystemNotification()
            {
                Title = $"Hello! {User.FindFirstValue("name")}",
                Message = "Welcome to DevSpark Dashboard",
                Time = DateTime.Now.ToString("dd/MM/yyyy"),
                NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Success),
                NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight)
            };
            systemNotifications.Add(systemNotification);

            TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);
            #endregion

            if (User.Identity.IsAuthenticated)
            {
                var userPrincipalName = User.FindFirst(ClaimTypes.Upn)?.Value;
                var userNameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = _userService.GetUserByEntraIdNameIdentifier(userNameIdentifier).Result;

                if(user != null)
                {
                    HttpContext.Session.SetInt32("userId", user.DevSparkUser.UserId);
                    HttpContext.Session.SetString("displayName", user.DisplayName);
                    HttpContext.Session.SetBool("accountEnabled", user.AccountEnabled);

                    return RedirectToAction("DevSparkHome", "DevsparkLanding");
                }

            }

            return RedirectToAction("SignIn", "Access");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "DevsparkLanding");
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
                return RedirectToAction("SignIn", "Access");
            }

            return View();
        }
    }
}