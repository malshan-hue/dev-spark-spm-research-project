using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.DeveloperPortalModels
{
    public class DevPageViewModel
    {
        public int Folder_id { get; set; }
        public int File_id { get; set; }
        public string File_Title { get; set; }
        public string File_Language { get; set; }
        public string File_CodeSnippet { get; set; }
    }

}
