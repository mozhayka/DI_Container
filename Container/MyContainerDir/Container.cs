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
        readonly Dictionary<Type, Type> container;
        readonly Dictionary<Type, object> SingletonObjects;

        public Container()
        {
            container = new();
            SingletonObjects = new();
        }

        public Interface GetInstance<Interface>()
            where Interface : class
        {
            if (container.ContainsKey(typeof(Interface)))
            {
                var constructor = container[typeof(Interface)].GetConstructor(new Type[] { });
                return (Interface)constructor.Invoke(new object[] { });
            }

            if (SingletonObjects.ContainsKey(typeof(Interface)))
            {
                return (Interface) SingletonObjects[typeof(Interface)];
            }

            throw new Exception("Interface is not registered");
        }

        public void Register<Interface, Realization>(Lifestyle lifestyle = Lifestyle.Transient)
            where Interface : class
            where Realization : class, Interface
        {
            if (lifestyle == Lifestyle.Transient)
            {
                container[typeof(Interface)] = typeof(Realization);
            }
            if (lifestyle == Lifestyle.Singleton)
            {
                var constructor = typeof(Realization).GetConstructor(new Type[] { });
                SingletonObjects[typeof(Interface)] = constructor.Invoke(new object[] { });
            }
        }
    }
}
