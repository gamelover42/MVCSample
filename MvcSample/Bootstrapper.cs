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
        public static void Setup()
        {
            var container = BuildContainer();
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory(container));
        }

        public static ICustomIocContainer BuildContainer()
        {
            var container = new CustomIocContainer();
            container.RegisterType<ICustomIocContainer, CustomIocContainer>(container);
            container.RegisterType<IFoodRepository, FoodRepository>();
            container.RegisterTypesByAssembly("Controller", LifeCycleType.Transient, Assembly.GetExecutingAssembly());

            return container;
        }
    }
}