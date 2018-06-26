using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Travel_Project_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest()
        {
            //if (HttpContext.Current.Request.Path != "/" && HttpContext.Current.Request.Path != "/User/LoginPage" && HttpContext.Current.Request.Path != "/User/Login")
            //{
            //    if (Travel_Project_MVC.Models.Systems.MySession.CurrentSession == null)
            //    {
            //        throw new Exception("You are not authorized to view the requested page");
            //    }
            //    else
            //    {
            //        if (Travel_Project_MVC.Models.Systems.MySession.CurrentSession.UserId == Guid.Empty)
            //        {
            //            throw new Exception("You are not authorized to view the requested page");
            //        }
            //    }
            //}
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
