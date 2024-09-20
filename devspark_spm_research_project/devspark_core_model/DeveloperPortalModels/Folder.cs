using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.DeveloperPortalModels
{
    public class Folder
    {
        public Folder()
        {
            Files = new List<FileModel>(); // Ensure Files is never null 
        }

        [DisplayName("Folder ID")]
        public int Id { get; set; }

        [DisplayName("Folder Title")]
        public string FolderTitle { get; set; }

        [DisplayName("Files")]
        public List<FileModel> Files { get; set; }
    }
}
