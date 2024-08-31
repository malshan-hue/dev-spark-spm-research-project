using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.SystemModels
{
    public class DevSparkUser
    {
        public int UserId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Personal Email")]
        public string PersonalEmail { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Password")]
        public string PasswordSalt { get; set; }

    }
}
