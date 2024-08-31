using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.DeveloperPortalModels
{
    public class FileModel
    {
        [DisplayName("File ID")]
        public int Id { get; set; }

        [DisplayName("File Title")]
        public string FileTitle { get; set; }

        [DisplayName("Language")]
        public string Language { get; set; }

        [DisplayName("Code Snippet")]
        public string CodeSnippet { get; set; }
    }

    public class FolderModel
    {
        [DisplayName("Folder ID")]
        public int Id { get; set; }

        [DisplayName("Folder Title")]
        public string FolderTitle { get; set; }

        [DisplayName("Files")]
        public List<FileModel> Files { get; set; }
    }
}
