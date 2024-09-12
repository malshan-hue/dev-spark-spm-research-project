using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.SystemModels
{
    public enum NotificationType
    {
        [Display(Name = "bg-primary")]
        Primary,
        [Display(Name = "bg-secondary")]
        Secondary,
        [Display(Name = "bg-success")]
        Success,
        [Display(Name = "bg-danger")]
        Danger,
        [Display(Name = "bg-warning")]
        Warning,
        [Display(Name = "bg-info")]
        Info,
        [Display(Name = "bg-dark")]
        Dark
    }
}
