using EasACardWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public CustomerController()
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
        public void RegisterIndividual(IndividualProfile profile)
        {
            customers.Add(profile);
        }

        //Customer/RegisterCompany
        [Route("RegisterCompany")]
        [HttpPost]
        public void RegisterCompany(CompanyProfile profile)
        {
            companyCustomers.Add(profile);
        }

        //Customer/LoginIndividual
        [Route("LoginIndividual")]
        [HttpGet]
        public IndividualAccountDetails LoginIndividual(string username, string password)
        {
            //Call DAL to validate the user
            IndividualAccountDetails res = new IndividualAccountDetails();
            res.AccountDetailID = 1;
            res.CardsLimit=100;
            res.ExpiryDate = DateTime.Now;
            res.UserID = "1";
            return res;
        }

        //Customer/LoginCompany
        [Route("LoginCompany")]
        [HttpGet]
        public CompanyAccountDetails LoginCompany(string username, string password)
        {
            //Call DAL to validate the user
            CompanyAccountDetails res = new CompanyAccountDetails();
            res.CompanyID = 1;
            res.CardsLimit = 100;
            res.ExpiryDate = DateTime.Now;
            return res;
        }
    }
}
