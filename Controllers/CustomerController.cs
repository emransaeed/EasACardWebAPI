using EasACardWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EasACardWebAPI.Controllers
{
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    [RoutePrefix("Customer")]
    public class CustomerController : ApiController
    {
        IList<IndividualProfile> customers = new List<IndividualProfile>();
        IList<CompanyProfile> companyCustomers = new List<CompanyProfile>();
        IList<IndividualAccountDetails> customerAccountDetails = new List<IndividualAccountDetails>();
        IList<CompanyAccountDetails> companyAccountDetails = new List<CompanyAccountDetails>();

        public CustomerController()
        {
            if (HttpContext.Current.Application["Customers"] != null)
            {
                customers = (List<IndividualProfile>)HttpContext.Current.Application["Customers"];
                companyCustomers = (List<CompanyProfile>)HttpContext.Current.Application["CompanyCustomers"];
            }
            else
            {
                CreateTestData();
            }
        }

        private void CreateTestData()
        {
            IndividualProfile ip = new IndividualProfile();
            ip.Country = "PK";
            ip.Email = "test@test.com";
            ip.Name = "test";
            ip.Password = "psw";
            ip.Phone = "023232";
            ip.UserID = "1";
            ip.UserName = "u";
            ip.VerificationCode = "v";
            customers.Add(ip);

            ip = new IndividualProfile();
            ip.Country = "PK1";
            ip.Email = "test1@test.com";
            ip.Name = "test1";
            ip.Password = "psw1";
            ip.Phone = "0232321";
            ip.UserID = "2";
            ip.UserName = "u1";
            ip.VerificationCode = "v1";
            customers.Add(ip);

            HttpContext.Current.Application["Customers"] = customers;

            CompanyProfile cp = new CompanyProfile();
            cp.Address = "A1";
            cp.City = "Sydney";
            cp.CompanyID = "C1";
            cp.ContactPerson = "Imran";
            cp.Country = "Australia";
            cp.Email = "test@test.com";
            cp.Mobile = "1234";
            cp.Name = "C1";
            cp.Password = "p";
            cp.Phone = "1234";
            cp.UserName = "cu1";
            cp.VerificationCode = "v1";
            companyCustomers.Add(cp);

            HttpContext.Current.Application["CompanyCustomers"] = companyCustomers;
        }

        //Customer/GetAllCustomers
        [Route("GetAllCustomers")]
        [HttpGet]
        public IList<IndividualProfile> GetAllCustomers()
        {
            return customers;
        }

        //Customer/GetCustomer?userid=1
        [HttpGet]
        [Route("GetCustomer")]
        public IndividualProfile GetCustomer(string userId)
        {
            var customer = customers.FirstOrDefault((p) => p.UserID == userId);
            if (customer == null)
            {
                return null;
            }
            return customer;
        }

        //Customer/RegisterIndividual
        [Route("RegisterIndividual")]
        [HttpPost]
        public IHttpActionResult RegisterIndividual(IndividualProfile profile)
        {
            customers.Add(profile);
            HttpContext.Current.Application["Customers"] = customers;
            return Json(new { Status = "Ok" });
        }

        //Customer/RegisterCompany
        [Route("RegisterCompany")]
        [HttpPost]
        public IHttpActionResult RegisterCompany(CompanyProfile profile)
        {
            companyCustomers.Add(profile);
            HttpContext.Current.Application["CompanyCustomers"] = companyCustomers;
            return Json(new { Status = "Ok" });
        }

        //Customer/LoginIndividual
        [Route("LoginIndividual")]
        [HttpPost]
        public IHttpActionResult LoginIndividual(UserProfile up)
        {
            int count = customers.Where(c => c.UserName == up.UserName && c.Password == up.Password).Count();
            if (count == 1)
            {
                return Json(new { Status = "Ok", CardsLimit = 100, ExpiryDate = DateTime.Now, UserName = "1" }); 
            }
            else
            {
                return Json(new { Status = "Failed" }); 
            }
        }

        //Customer/LoginCompany
        [Route("LoginCompany")]
        [HttpPost]
        public IHttpActionResult LoginCompany(UserProfile up)
        {
            int count = companyCustomers.Where(c => c.UserName == up.UserName && c.Password == up.Password).Count();
            if (count == 1)
            {
                return Json(new { Status = "Ok", UserName = "1", CardsLimit = 100, ExpiryDate = DateTime.Now });
            }
            else
            {
                return Json(new { Status = "Failed" });
            }
        }

        //Customer/VerifyIndividual
        [Route("VerifyIndividual")]
        [HttpPost]
        public IHttpActionResult VerifyIndividual(UserProfile up)
        {
            int count = customers.Where(c => c.UserName == up.UserName && c.VerificationCode == up.VerificationCode).Count();
            if (count == 1)
            {
                return Json(new { Status = "Ok", CardsLimit = 100, ExpiryDate = DateTime.Now, UserName = "1" });
            }
            else
            {
                return Json(new { Status = "Failed" });
            }
        }

        //Customer/VerifyCompany
        [Route("VerifyCompany")]
        [HttpPost]
        public IHttpActionResult VerifyCompany(UserProfile up)
        {
            int count = companyCustomers.Where(c => c.UserName == up.UserName && c.VerificationCode == up.VerificationCode).Count();
            if (count == 1)
            {
                return Json(new { Status = "Ok", UserName = "1", CardsLimit = 100, ExpiryDate = DateTime.Now });
            }
            else
            {
                return Json(new { Status = "Failed" });
            }
        }
    }
}
