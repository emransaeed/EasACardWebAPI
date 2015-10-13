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
        IList<IndividualAccountDetails> individualAccountDetails = new List<IndividualAccountDetails>();
        IList<CompanyAccountDetails> companyAccountDetails = new List<CompanyAccountDetails>();

        public CustomerController()
        {
            if (HttpContext.Current.Application["Customers"] != null)
            {
                customers = (List<IndividualProfile>)HttpContext.Current.Application["Customers"];
                companyCustomers = (List<CompanyProfile>)HttpContext.Current.Application["CompanyCustomers"];
                individualAccountDetails = (List<IndividualAccountDetails>)HttpContext.Current.Application["IndividualAccountDetails"];
                companyAccountDetails = (List<CompanyAccountDetails>)HttpContext.Current.Application["CompanyAccountDetails"];
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
            ip.Email = "test1@test.com";
            ip.Name = "test1";
            ip.Password = "psw";
            ip.Phone = "023232";
            ip.UserID = "1";
            ip.UserName = "u1";
            ip.VerificationCode = "psw";
            customers.Add(ip);

            CreateIndividualAccountDetails(100, DateTime.Now.AddMonths(6), "u1");

            ip = new IndividualProfile();
            ip.Country = "PK";
            ip.Email = "test2@test.com";
            ip.Name = "test2";
            ip.Password = "psw";
            ip.Phone = "0232321";
            ip.UserID = "2";
            ip.UserName = "u2";
            ip.VerificationCode = "psw";
            customers.Add(ip);

            CreateIndividualAccountDetails(100, DateTime.Now.AddMonths(6), "u2");

            HttpContext.Current.Application["Customers"] = customers;
            HttpContext.Current.Application["IndividualAccountDetails"] = individualAccountDetails;

            CompanyProfile cp = new CompanyProfile();
            cp.Address = "A1";
            cp.City = "Sydney";
            cp.CompanyID = "C1";
            cp.ContactPerson = "Imran";
            cp.Country = "Australia";
            cp.Email = "testc1@test.com";
            cp.Mobile = "1234";
            cp.Name = "C1";
            cp.Password = "p";
            cp.Phone = "1234";
            cp.UserName = "cu1";
            cp.VerificationCode = "cu1";
            companyCustomers.Add(cp);

            CreateCompanyAccountDetails(300, DateTime.Now.AddMonths(6), "cu1");

            HttpContext.Current.Application["CompanyCustomers"] = companyCustomers;
            HttpContext.Current.Application["CompanyAccountDetails"] = companyAccountDetails;
        }

        private void CreateCompanyAccountDetails(int cardsLimit, DateTime expiryDate, string userName)
        {
            CompanyAccountDetails cad = new CompanyAccountDetails();
            cad.CardsLimit = cardsLimit;
            cad.ExpiryDate = expiryDate;
            cad.UserName = userName;
            companyAccountDetails.Add(cad);
        }

        private void CreateIndividualAccountDetails(int cardsLimit, DateTime expiryDate, string userName)
        {
            IndividualAccountDetails iad = new IndividualAccountDetails();
            iad.CardsLimit = cardsLimit;
            iad.ExpiryDate = expiryDate;
            iad.UserName = userName;
            individualAccountDetails.Add(iad);
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
            profile.VerificationCode = profile.Password;
            customers.Add(profile);
            CreateIndividualAccountDetails(100, DateTime.Now.AddMonths(6), profile.UserName);
            
            HttpContext.Current.Application["Customers"] = customers;
            HttpContext.Current.Application["IndividualAccountDetails"] = individualAccountDetails;

            return Json(new { Status = "Ok" });
        }

        //Customer/RegisterCompany
        [Route("RegisterCompany")]
        [HttpPost]
        public IHttpActionResult RegisterCompany(CompanyProfile profile)
        {
            profile.VerificationCode = profile.Password;
            companyCustomers.Add(profile);
            CreateCompanyAccountDetails(300, DateTime.Now.AddMonths(6), profile.UserName);
            
            HttpContext.Current.Application["CompanyCustomers"] = companyCustomers;
            HttpContext.Current.Application["CompanyAccountDetails"] = companyAccountDetails;

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
                IndividualAccountDetails iad = individualAccountDetails.Where(u => u.UserName == up.UserName).First();
                return Json(new { Status = "Ok", CardsLimit = iad.CardsLimit, ExpiryDate = iad.ExpiryDate, UserName = iad.UserName });
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
                CompanyAccountDetails cad = companyAccountDetails.Where(u => u.UserName == up.UserName).First();
                return Json(new { Status = "Ok", CardsLimit = cad.CardsLimit, ExpiryDate = cad.ExpiryDate, UserName = cad.UserName });
            }
            else
            {
                return Json(new { Status = "Failed" });
            }
        }
    }
}
