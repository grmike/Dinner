using Autofac;
using Autofac.Integration.Mvc;
using Dinner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Dinner.App_Start
{
    public class ContainerConfig
    {
        public static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            // MVC - Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            //builder.RegisterInstance(new HomeService()).As<HomeService>().SingleInstance();
            builder.RegisterType<AuthService>().SingleInstance();
            builder.RegisterType<HomeService>().SingleInstance();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}