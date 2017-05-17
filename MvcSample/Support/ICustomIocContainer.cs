using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSample.Support
{
    public interface ICustomIocContainer
    {
        void RegisterType<T, C>();
        void RegisterType<T, C>(object instance);
        void RegisterType<T, C>(LifeCycleType lifeCycle, object instance);
        object Resolve(Type requestedType);
    }
}
