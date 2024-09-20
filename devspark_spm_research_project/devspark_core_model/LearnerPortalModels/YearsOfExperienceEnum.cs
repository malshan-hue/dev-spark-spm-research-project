using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public enum YearsOfExperienceEnum: int
    {
        [Display(Name ="None")]
        None = 1,

        [Display(Name = "1 - 2 Years")]
        OneToTwoYears = 2,

        [Display(Name = "3 or Above")]
        ThreeOrAbove = 3
    }
}
