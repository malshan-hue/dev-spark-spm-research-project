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

        Task<bool> UpdateDevSpace(Folder folder);

        Task<bool> CreateNewFolder(Folder folder);

        Task<bool> UpdateFolder(Folder folder);

        Task<bool> CreateNewFile(FileModel file); 

        Task<bool> UpdateFile(FileModel file);

        Task<bool> UpdateFileInfo(FileModel file);

        Task<IEnumerable<Folder>> GetDevSpaces(); 

        Task<bool> DeleteFolder(int folderid); 

        Task<bool> DeleteFile(int fileid);
    }
}
