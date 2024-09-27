using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public enum AchivingLevelEnum: int
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Beginner")]
        Beginner = 1,

        [Display(Name = "Intermediate")]
        Intermediate = 2,

        [Display(Name = "Advanced")]
        Advanced = 3,

        [Display(Name = "Expert")]
        Expert = 4,

        [Display(Name = "Master")]
        Master = 5
    }
}
