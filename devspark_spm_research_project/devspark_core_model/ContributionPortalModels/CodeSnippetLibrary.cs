using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.ContributionPortalModels
{
    public class CodeSnippetLibrary
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Language is required")]
        [DisplayName("Language")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [DisplayName("Code")]
        public string Code {  get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Tags")]
        public string Tags { get; set; }
    }
}
