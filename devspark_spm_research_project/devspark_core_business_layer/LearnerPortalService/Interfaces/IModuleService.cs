using devspark_core_model.LearnerPortalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.LearnerPortalService.Interfaces
{
    public interface IModuleService
    {
        Task<Module> GetModuleByModuleId(int moduleId);
    }
}
