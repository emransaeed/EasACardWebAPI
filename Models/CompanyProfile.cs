﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasACardWebAPI.Models
{
    public class CompanyProfile
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string VerificationCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string AccountStatus { get; set; }
    }
}