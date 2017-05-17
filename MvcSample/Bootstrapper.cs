using MvcSample.Models;
using MvcSample.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MvcSample
{
    public static class Bootstrapper
    {
        /// <summary>
        /// Register our custom controller factory with ASP.NET MVC
        /// </summary>
        public static void Setup()
        {
            var container = BuildContainer();
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory(container));
        }

        /// <summary>
        /// build the IoC container and register all the necessary types in it.
        /// </summary>
        /// <returns></returns>
        public static ICustomIocContainer BuildContainer()
        {
            ICustomIocContainer container = new CustomIocContainer();
            container.RegisterType<ICustomIocContainer, CustomIocContainer>(container);
            container.RegisterType<IFoodRepository, FoodRepository>();
            container.RegisterType<IFoodService, FoodService>();
            container.RegisterTypesByAssembly("Controller", LifeCycleType.Transient, Assembly.GetExecutingAssembly());

            return container;
        }
    }
}