using System;
using System.Linq;
using Xunit;
using MvcSample.Support;
using System.Reflection;
using MvcSample.Controllers;
using System.Web.Mvc;
using MvcSample.Models;
using System.Collections.Generic;

namespace MvcSample.Test
{
    public class CustomDependencyResolverTest
    {
    //    private interface IFoo { string Name(); }
    //    private class Foo : IFoo
    //    {
    //        public string Name() { return "Smokey"; }
    //    }

    //    private interface IBar { string FullName(); }

    //    private class Bar : IBar
    //    {
    //        IFoo foo;
    //        public Bar(IFoo foo)
    //        {
    //            this.foo = foo;
    //        }
    //        public string FullName()
    //        {
    //            return this.foo.Name() + " Stover";
    //        }
    //    }

    //    [Fact]
    //    public void TestGetService()
    //    {
    //        var container = new CustomIocContainer();
    //        container.RegisterType<IFoo, Foo>();
    //        container.RegisterType<IBar, Bar>();
    //        var resolver = new CustomDependencyResolver(container);
    //        var bar = resolver.GetService(typeof(IBar));
    //        Assert.IsType(typeof(Bar), bar);
    //    }
    //    [Fact]
    //    public void TestGetServiceNotFound()
    //    {
    //        var container = new CustomIocContainer();
    //        var resolver = new CustomDependencyResolver(container);
    //        Exception ex = Assert.Throws<KeyNotFoundException>(() => resolver.GetService(typeof(IBar)));
    //        Assert.Equal("interfaceType of IBar not found", ex.Message);
    //    }
    }
}
