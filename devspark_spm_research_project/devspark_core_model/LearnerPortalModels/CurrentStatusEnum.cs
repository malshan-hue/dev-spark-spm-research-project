using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public enum CurrentStatusEnum: int
    {
        [Display(Name = "Student")]
        Student = 1,

        [Display(Name = "Professional")]
        Professional = 2,
    }
}
