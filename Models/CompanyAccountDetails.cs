﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasACardWebAPI.Models
{
    public class CompanyAccountDetails
    {
        public long CompanyID { get; set; }
        public string UserName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CardsLimit { get; set; }
    }
}