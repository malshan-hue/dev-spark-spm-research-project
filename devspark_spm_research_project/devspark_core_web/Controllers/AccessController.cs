using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_model.SystemModels;
using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserService _userService;

        public AccessController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            bool status = await _userService.InsertUser(user);

            if (status)
            {
                return RedirectToAction("Index", "DevsparkLanding");
            }

            return View();
        }
    }
}
