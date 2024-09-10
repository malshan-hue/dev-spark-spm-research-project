using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public enum AreaOfStudyEnum: int
    {
        [Display(Name = "Java")]
        Java = 1,

        [Display(Name = "C#")]
        CSharp = 2,

        [Display(Name = "Python")]
        Python = 3,

        [Display(Name = "JavaScript")]
        JavaScript = 4,

        [Display(Name = "TypeScript")]
        TypeScript = 5,

        [Display(Name = "PHP")]
        PHP = 6,

        [Display(Name = "Ruby")]
        Ruby = 7,

        [Display(Name = "Swift")]
        Swift = 8,

        [Display(Name = "Kotlin")]
        Kotlin = 9,

        [Display(Name = "Go")]
        Go = 10,

        [Display(Name = "Rust")]
        Rust = 11,

        [Display(Name = "SQL")]
        SQL = 12,

        [Display(Name = "C++")]
        CPlusPlus = 13,

        [Display(Name = "C")]
        C = 14,

        [Display(Name = "R")]
        R = 15,

        [Display(Name = "Dart")]
        Dart = 16,

        [Display(Name = "Shell")]
        Shell = 17,

        [Display(Name = "MATLAB")]
        MATLAB = 18,

        [Display(Name = "Scala")]
        Scala = 19
    }
}
