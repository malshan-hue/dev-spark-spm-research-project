using devspark_core_model.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.SystemService.Interfaces
{
    public interface IUserService
    {
        Task<bool> InsertUser(EntraIdUser entraIdUser);
        Task<EntraIdUser> GetUserByEntraIdNameIdentifier(string nameIdentifier = "");
    }
}
