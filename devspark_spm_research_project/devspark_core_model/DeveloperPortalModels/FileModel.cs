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

        [DisplayName("Folder ID")]
        public int FolderId { get; set; }

        [DisplayName("File Title")]
        public string FileTitle { get; set; }

        [DisplayName("Language")]
        public string Language { get; set; }

        public string Extension { get; set; }

        [DisplayName("Code Snippet")]
        public string CodeSnippet { get; set; }

        public bool IsNew { get; set; }

    }
}
