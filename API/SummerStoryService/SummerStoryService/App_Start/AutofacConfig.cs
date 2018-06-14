using Autofac;
using Autofac.Integration.WebApi;
using IService;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace SummerStoryService.App_Start
{
    public class AutofacConfig
    {
        public static void Config()
        {
            var builder = new ContainerBuilder();
            Assembly[] assemblies = new Assembly[] { Assembly.Load("Service") };
            builder.RegisterAssemblyTypes(assemblies)
                .Where(a => !a.IsAbstract && typeof(IServiceFlag).IsAssignableFrom(a)
                ).AsImplementedInterfaces().PropertiesAutowired();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(a => !a.IsAbstract && a.Name.EndsWith("Repository")).AsImplementedInterfaces().PropertiesAutowired();

            //builder.RegisterApiControllers(assemblies).PropertiesAutowired();
            var container = builder.Build();
            // Set the WebApi dependency resolver.  
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //注册解析
            //GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}