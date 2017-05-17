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
    public class CustomIocContainerTest
    {
        private interface IFoo { string Name(); }
        private class Foo : IFoo {
            public string Name() { return "Smokey"; }
        }

        private interface IBar { string FullName(); }

        private class Bar : IBar {
            IFoo foo;
            public Bar(IFoo foo)
            {
                this.foo = foo;
            }
            public string FullName() {
                return this.foo.Name() + " Stover";
            }       
        }

        [Fact]
        public void TestRegisterType()
        {
            ICustomIocContainer container = new CustomIocContainer();
            container.RegisterType<IFoo, Foo>();
            var resolved = container.Resolve(typeof(IFoo));

            Assert.IsType(typeof(Foo), resolved);
        }

        [Fact]
        public void TestRegisterTypeWithDepends()
        {
            ICustomIocContainer container = new CustomIocContainer();
            container.RegisterType<IFoo, Foo>();
            container.RegisterType<IBar, Bar>();
            var resolved = container.Resolve(typeof(IBar));

            Assert.IsType(typeof(Bar), resolved);
        }

        [Fact]
        public void TestRegisterAssemblyClasses()
        {
            //get assembly where our controllers are
            var assembly = typeof(HomeController).Assembly;

            ICustomIocContainer container = new CustomIocContainer();
            container.RegisterTypesByAssembly("Controller", LifeCycleType.Transient, assembly);
            var resolved = container.Resolve(typeof(HomeController));

            Assert.IsType(typeof(HomeController), resolved);
        }

        [Fact]
        public void TestRegisterAssemblyClassesWithDepends()
        {
            //get assembly where our controllers are
            var assembly = typeof(FoodController).Assembly;

            ICustomIocContainer container = new CustomIocContainer();
            container.RegisterType<IFoodRepository, FoodRepository>();
            container.RegisterType<IFoodService, FoodService>();
            container.RegisterTypesByAssembly("Controller", LifeCycleType.Transient, assembly);
            var resolved = container.Resolve(typeof(FoodController));

            Assert.IsType(typeof(FoodController), resolved);
        }

        [Fact]
        public void TestRegisterAssemblyClassesWithMissingDepends()
        {
            //get assembly where our controllers are
            var assembly = typeof(FoodController).Assembly;

            ICustomIocContainer container = new CustomIocContainer();
            container.RegisterTypesByAssembly("Controller", LifeCycleType.Transient, assembly);
            Exception ex = Assert.Throws<KeyNotFoundException>(() => container.Resolve(typeof(FoodController)));
            Assert.Equal("interfaceType of IFoodService not found", ex.Message);
        }

        [Fact]
        public void TestRegisterSingleton()
        {
            ICustomIocContainer container = new CustomIocContainer();
            var foo = new Foo();
            container.RegisterType<IFoo, Foo>(foo);
            var resolved = container.Resolve(typeof(IFoo));

            Assert.Same(foo, resolved);
        }
    }
}
