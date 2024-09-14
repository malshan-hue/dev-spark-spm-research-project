using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public enum ProgressStatusEnum : int
    {
        [Display( Name ="Not Started")]
        NotStarted = 1,

        [Display(Name = "In Progress")]
        InProgress = 2,

        [Display(Name = "Completed")]
        Completed = 3
    }
}
