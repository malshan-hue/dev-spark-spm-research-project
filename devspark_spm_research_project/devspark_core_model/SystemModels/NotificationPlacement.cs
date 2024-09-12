using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.SystemModels
{
    public enum NotificationPlacement
    {
        [Display(Name = "top-0 start-0")]
        TopLeft,
        [Display(Name = "top-0 start-50 translate-middle-x")]
        TopCenter,
        [Display(Name = "top-0 end-0")]
        TopRight,
        [Display(Name = "top-50 start-0 translate-middle-y")]
        MiddleLeft,
        [Display(Name = "top-50 start-50 translate-middle")]
        MiddleCenter,
        [Display(Name = "top-50 end-0 translate-middle-y")]
        MiddleRight,
        [Display(Name = "bottom-0 start-0")]
        BottomLeft,
        [Display(Name = "bottom-0 start-50 translate-middle-x")]
        BottomCenter,
        [Display(Name = "bottom-0 end-0")]
        BottomRight
    }
}
