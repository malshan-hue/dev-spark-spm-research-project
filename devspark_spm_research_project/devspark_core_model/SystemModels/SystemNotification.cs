using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.SystemModels
{
    public class SystemNotification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public string NotificationType { get; set; }
        public string NotificationPlacement { get; set; }
    }
}
