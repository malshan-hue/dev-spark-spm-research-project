using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.SystemModels
{
    public class EntraIdUser
    {
        public int UserId { get; set; }
        // Basic User Information
        public string Id { get; set; } // Unique identifier
        public bool AccountEnabled {  get; set; } 
        public string DisplayName { get; set; } // Display name
        public string GivenName { get; set; } // First name
        public string Surname { get; set; } // Last name
        public string UserPrincipalName { get; set; } // Email address/UPN
        public string MailNickname { get; set; } // Email alias

        // Contact Information
        public string Mail { get; set; } // Primary email
        public string MobilePhone { get; set; } // Mobile phone number
        public string BusinessPhones { get; set; } // Business phone numbers
        public string OfficeLocation { get; set; } // Office location
        public string StreetAddress { get; set; } // Street address
        public string City { get; set; } // City
        public string State { get; set; } // State
        public string Country { get; set; } // Country
        public string PostalCode { get; set; } // Postal code

        // Job Information
        public string JobTitle { get; set; } // Job title
        public string Department { get; set; } // Department
        public string CompanyName { get; set; } // Company name

        #region NAVIGATIONAL PROPERTIES
        public DevSparkUser DevSparkUser { get; set; }
        #endregion
    }
}
