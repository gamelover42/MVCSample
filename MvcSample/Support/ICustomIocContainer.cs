using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvcSample.Support
{
    public interface ICustomIocContainer
    {
        void RegisterType<T, C>(object instance);
        void RegisterType<T, C>(LifeCycleType lifeCycle = LifeCycleType.Transient);
        void RegisterTypesByAssembly(string suffix, LifeCycleType lifeCycle, params Assembly[] assemblies);
        //object Resolve(string v);
        object Resolve(Type interfaceType);
    }
}
