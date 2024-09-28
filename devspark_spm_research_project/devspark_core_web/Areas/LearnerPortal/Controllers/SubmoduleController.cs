using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Areas.LearnerPortal.Controllers
{
    public class SubmoduleController : Controller
    {
        private readonly ISubmoduleService _submoduleService;
        private IDataProtector _dataProtector;

        public SubmoduleController(ISubmoduleService submoduleService, IDataProtectionProvider dataProtectionProvider)
        {
            _submoduleService = submoduleService;
            _dataProtector = dataProtectionProvider.CreateProtector(GlobalHelpers.LearnerPortalDataProtectorSecret);
        }

        public async Task<IActionResult> ViewSubModule(string subModuleEncryptedKey)
        {
            int subModuleId = Convert.ToInt32(_dataProtector.Unprotect(subModuleEncryptedKey));
            var subModule = await _submoduleService.GetSubmoduleById(subModuleId);
            subModule.EncryptedKey = _dataProtector.Protect(subModule.SubmoduleId.ToString());

            return View(subModule);
        }
    }
}
