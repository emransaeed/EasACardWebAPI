using EasACardWebAPI.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApiContrib.Formatting.Jsonp;

namespace EasACardWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();

            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            //GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var config = GlobalConfiguration.Configuration;
            GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());
            //GlobalConfiguration.Configuration.AddJsonpFormatter(config.Formatters.JsonFormatter, "callback");
            config.Formatters.Insert(0, new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter));
        }
    }
}
