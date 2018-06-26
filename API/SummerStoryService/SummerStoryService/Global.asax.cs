using SummerStoryService.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;

namespace SummerStoryService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //AutofacConfig.Config();
        }

        protected void Application_AuthorizeRequest()
        {
            //var s= this.Request.Headers["Authorization"];
        }
    }
}
