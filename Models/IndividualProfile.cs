using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EasACardWebAPI.Models
{
    public class IndividualProfile
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string VerificationCode { get; set; }
        public string AccountStatus { get; set; }
    }
}