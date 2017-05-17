using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcSample.Support
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        private readonly ICustomIocContainer container;
        public CustomControllerFactory(ICustomIocContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return this.container.Resolve(controllerType) as IController;
        }
    }
}