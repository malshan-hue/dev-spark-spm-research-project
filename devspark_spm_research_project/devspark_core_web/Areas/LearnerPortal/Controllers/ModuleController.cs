using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_model.LearnerPortalModels;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Areas.LearnerPortal.Controllers
{
    [Authorize]
    [Area("LearnerPortal")]
    public class ModuleController : Controller
    {
        private readonly IModuleService _moduleService;
        private readonly IDataProtector _dataProtector;

        public ModuleController(IModuleService moduleService, IDataProtectionProvider dataProtectionProvider)
        {
            _moduleService = moduleService;
            _dataProtector = dataProtectionProvider.CreateProtector(GlobalHelpers.LearnerPortalDataProtectorSecret);
        }

        public async Task<IActionResult> ViewModule(string moduleEncryptedKey)
        {
            int moduleId = Convert.ToInt32(_dataProtector.Unprotect(moduleEncryptedKey));
            Module module = await _moduleService.GetModuleByModuleId(moduleId);
            module.EncryptedKey = _dataProtector.Protect(module.ModuleId.ToString());
            module.Submodules.ToList().ForEach(e => { e.EncryptedKey = _dataProtector.Protect(e.SubmoduleId.ToString()); });

            return View(module);
        }
    }
}
