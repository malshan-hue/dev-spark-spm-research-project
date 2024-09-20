using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public enum StudyPeriodEnum:int
    {
        [Display(Name = "1 Week")]
        OneWeek = 1,

        [Display(Name = "2 to 4 Weeks")]
        TwoToFourWeeks = 2,

        [Display(Name = "3 Months")]
        ThreeMonths = 3
    }
}
