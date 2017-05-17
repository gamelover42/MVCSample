using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Support
{
    public class TypeRegistration
    {
        public TypeRegistration(Type interfaceType,
            Type concreteType,
            LifeCycleType lifeCycle,
            object instance = null)
        {
            InterfaceType = interfaceType;
            ConcreteType = concreteType;
            LifeCycle = lifeCycle;
            Instance = instance;
        }
        public Type InterfaceType { get; set; }
        public Type ConcreteType { get; set; }
        public LifeCycleType LifeCycle { get; set; }
        public object Instance { get; internal set; }
    }
}