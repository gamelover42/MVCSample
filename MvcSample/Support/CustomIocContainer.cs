using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcSample.Support
{
    public enum LifeCycleType { Transient, Singleton }

    public class CustomIocContainer : ICustomIocContainer
    {
        private class Registration
        {
            public Registration(Type interfaceType, 
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

        IList<Registration> registrations = new List<Registration>();

        public void RegisterType<T, C>()
        {
            RegisterType<T, C>(LifeCycleType.Transient, null);
        }

        public void RegisterType<T, C>(object instance)
        {
            RegisterType<T, C>(LifeCycleType.Singleton, instance);
        }

        public void RegisterType<T, C>(LifeCycleType lifeCycle, object instance)
        {
            registrations.Add(new Registration(typeof(T), typeof(C), lifeCycle, instance));
        }

        /// <summary>
        /// Registers all types from the supplied assemblies which match "suffix"
        /// </summary>
        /// <param name="suffix"></param>
        /// <param name="lifeCycle"></param>
        /// <param name="assemblies"></param>
        public void RegisterTypesByAssembly(string suffix, LifeCycleType lifeCycle, params Assembly[] assemblies)
        {
            var types = from a in assemblies
                        where !a.IsDynamic
                        from t in a.GetExportedTypes()
                        where t.IsClass
                        where t.Name.EndsWith(suffix)
                        select t;

            foreach (Type concreteType in types)
            {
                registrations.Add(new Registration(concreteType, concreteType, lifeCycle));
            }
        }

        public object Resolve(string v)
        {
            var instance = registrations
                .Where(r => r.InterfaceType.Name.EndsWith(v))
                .Select(r => Resolve(r.InterfaceType));
            
            return instance.FirstOrDefault();
        }

        public object Resolve(Type interfaceType)
        {
            object instance = null;
            if (interfaceType != null)
            {
                Registration reg = registrations
                    .Where(r => r.InterfaceType == interfaceType)
                    .FirstOrDefault();

                if (reg != null)
                {
                    instance = reg.Instance;
                    if (instance == null)
                    {
                        ConstructorInfo constructor = reg.ConcreteType.GetConstructors()
                                .First();

                        object[] parameters = constructor.GetParameters()
                            .Select(p => Resolve(p.ParameterType))
                            .ToArray();

                        instance = constructor.Invoke(parameters);
                    }
                    if (reg.LifeCycle == LifeCycleType.Singleton)
                    {
                        reg.Instance = instance;
                    }
                }
                else
                {
                    throw new KeyNotFoundException("interfaceType of " + interfaceType.Name + " not found");
                }
            }
            return instance;
        }
    }
}