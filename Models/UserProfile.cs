using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasACardWebAPI.Models
{
    public class UserProfile
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
    }
}