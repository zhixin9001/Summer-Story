using SummerStoryService.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SummerStoryService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.MapHttpAttributeRoutes();
            config.Filters.Add(new AuthFilterAttribute());
            config.Filters.Add(new ExceptionFilter());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.EnableCors
        }
    }
}
