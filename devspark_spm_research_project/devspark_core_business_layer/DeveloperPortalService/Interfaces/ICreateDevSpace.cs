using devspark_core_model.DeveloperPortalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.DeveloperPortalService.Interfaces
{
    public interface ICreateDevSpace
    {
        Task<bool> CreateDevSpace(Folder folder);
    }
}
