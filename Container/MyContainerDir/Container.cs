using Container.MyContainerDir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Container.IContainer;

namespace Container
{
    public class Container : IContainer
    {
        Dictionary<Type, Type> container;
        readonly Dictionary<Type, System.Reflection.ConstructorInfo> TransientConstructors;
        readonly Dictionary<Type, object> SingletonObjects;

        readonly Dictionary<Type, System.Reflection.ConstructorInfo> ScopedConstructors;
        readonly Dictionary<Type, Dictionary<string, object>> ScopedObjects;
        readonly Dictionary<Type, InstanceProducer> instances;

        public Container()
        {
            container = new();
            TransientConstructors = new();
            SingletonObjects = new();
            ScopedObjects = new();
            instances = new();
        }

        public Interface GetInstance<Interface>()
            where Interface : class
        {
            if (!instances.ContainsKey(typeof(Interface)))
                throw new Exception("Interface is not registered");

            return (Interface) instances[typeof(Interface)].GetInstance();
        }

        public void Register<Interface, Realization>(Lifestyle lifestyle = Lifestyle.Transient)
            where Interface : class
            where Realization : class, Interface
        {
            instances[typeof(Interface)] = new(typeof(Interface), typeof(Realization), lifestyle);
        }
    }
}
