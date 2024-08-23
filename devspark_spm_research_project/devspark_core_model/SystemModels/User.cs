using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.SystemModels
{
    public class User
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("User Email")]
        public string UserEmail { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool Remember { get; set; }
    }
}
