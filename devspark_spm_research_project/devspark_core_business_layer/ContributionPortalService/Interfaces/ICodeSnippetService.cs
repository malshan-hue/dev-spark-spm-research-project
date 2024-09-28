using devspark_core_model.ContributionPortalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.ContributionPortalService.Interfaces
{
    public interface ICodeSnippetService
    {
        Task<bool> InsertCodeSnippet(CodeSnippetLibrary codesnippet);

        Task<IEnumerable<CodeSnippetLibrary>> GetAllCodeSnippets();

        Task<CodeSnippetLibrary> GetCodeSnippetById(int id);

        Task<bool> UpdateCodeSnippet(CodeSnippetLibrary codesnippet);

        Task<bool> DeleteCodeSnippet(int id);
    }
}
