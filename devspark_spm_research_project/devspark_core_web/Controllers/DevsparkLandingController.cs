using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace devspark_core_web.Controllers
{
    public class DevsparkLandingController : Controller
    {
        private readonly IUserService _userService;

        public DevsparkLandingController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DevSparkHome()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userPrincipalName = User.FindFirst(ClaimTypes.Upn)?.Value;
                var userObjectidentifier = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

                var user = _userService.GetUserByEntraIdNameIdentifier(userObjectidentifier).Result;

                if (user != null)
                {
                    HttpContext.Session.SetInt32("userId", user.DevSparkUser.UserId);
                    HttpContext.Session.SetString("displayName", user.DisplayName);
                    HttpContext.Session.SetBool("accountEnabled", user.AccountEnabled);

                }

            }

            return View();
        }
    }
}
