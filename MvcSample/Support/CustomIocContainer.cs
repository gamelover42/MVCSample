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
        //this holds the list of registered types
        private readonly IList<TypeRegistration> registrations = new List<TypeRegistration>();
        
        /// <summary>
        /// Register a type, it's concrete impementation to a single instance. Defaults to Singleton LifeCycleType.
        /// </summary>
        /// <typeparam name="T">Interface Type</typeparam>
        /// <typeparam name="C">Concrete class Type</typeparam>
        /// <param name="instance">The single class instance to use for future injections.</param>
        public void RegisterType<T, C>(object instance)
        {
            registrations.Add(new TypeRegistration(typeof(T), typeof(C), LifeCycleType.Singleton, instance));
        }

        /// <summary>
        /// Register a type and it's concrete impementation.
        /// </summary>
        /// <typeparam name="T">Interface Type</typeparam>
        /// <typeparam name="C">Concrete class Type</typeparam>
        /// <param name="lifeCycle">(optional) Defaults to Transient LifeCycleType</param>
        public void RegisterType<T, C>(LifeCycleType lifeCycle = LifeCycleType.Transient)
        {
            registrations.Add(new TypeRegistration(typeof(T), typeof(C), lifeCycle));
        }

        /// <summary>
        /// Registers all types from the supplied assemblies which match "suffix"
        /// </summary>
        /// <param name="suffix">Will only search for names that end the suffix, e.g. "Controller"</param>
        /// <param name="lifeCycle"></param>
        /// <param name="assemblies"></param>
        public void RegisterTypesByAssembly(string suffix, LifeCycleType lifeCycle, params Assembly[] assemblies)
        {
            //build a list of all types with names that end the suffix, e.g. "Controller"
            var types = from a in assemblies
                        where !a.IsDynamic
                        from t in a.GetExportedTypes()
                        where t.IsClass
                        where t.Name.EndsWith(suffix)
                        select t;

            foreach (Type concreteType in types)
            {
                registrations.Add(new TypeRegistration(concreteType, concreteType, lifeCycle));
            }
        }

        /// <summary>
        /// Creates an instance of interfaceType or finds a cached instance. Will recursively inject types
        /// into Type constructors as long as they are registered.
        /// </summary>
        /// <param name="interfaceType">The Interface or Abstract Type to retrieve.</param>
        /// <returns></returns>
        public object Resolve(Type interfaceType)
        {
            object instance = null;
            if (interfaceType != null)
            {
                TypeRegistration reg = registrations
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