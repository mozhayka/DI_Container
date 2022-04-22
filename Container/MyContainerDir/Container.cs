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
        readonly Dictionary<Type, System.Reflection.ConstructorInfo> TransientObjects;
        readonly Dictionary<Type, object> SingletonObjects;
        readonly Dictionary<Type, Dictionary<string, object>> ScopedObjects;

        public Container()
        {
            container = new();
            TransientObjects = new();
            SingletonObjects = new();
            ScopedObjects = new();

        }

        public Interface GetInstance<Interface>()
            where Interface : class
        {
            if (TransientObjects.ContainsKey(typeof(Interface)))
            {
                var constructor = TransientObjects[typeof(Interface)];
                return (Interface)constructor.Invoke(new object[] { });
            }

            if (SingletonObjects.ContainsKey(typeof(Interface)))
            {
                return (Interface)SingletonObjects[typeof(Interface)];
            }

            if (ScopedObjects.ContainsKey(typeof(Interface)))
            {
                var env = Environment.CurrentDirectory;
                if (!ScopedObjects[typeof(Interface)].ContainsKey(env))
                {
                    var constructor = container[typeof(Interface)].GetConstructor(new Type[] { });
                    ScopedObjects[typeof(Interface)][env] = constructor.Invoke(new object[] { });
                }
                return (Interface)ScopedObjects[typeof(Interface)][env];
            }
            throw new Exception("Interface is not registered");
        }

        public void Register<Interface, Realization>(Lifestyle lifestyle = Lifestyle.Transient)
            where Interface : class
            where Realization : class, Interface
        {
            switch (lifestyle)
            {
                case Lifestyle.Transient:
                    RegisterTransient<Interface, Realization>();
                    break;
                case Lifestyle.Singleton:
                    RegisterSingleton<Interface, Realization>();
                    break;
                case Lifestyle.Scoped:
                    RegisterScoped<Interface, Realization>();
                    break;
            }
        }

        public void RegisterTransient<Interface, Realization>()
            where Interface : class
            where Realization : class, Interface
        {
            container[typeof(Interface)] = typeof(Realization);
            var constructor = typeof(Realization).GetConstructor(new Type[] { });
            TransientObjects[typeof(Interface)] = constructor;
        }

        public void RegisterSingleton<Interface, Realization>()
            where Interface : class
            where Realization : class, Interface
        {
            container[typeof(Interface)] = typeof(Realization);
            var constructor = typeof(Realization).GetConstructor(new Type[] { });
            SingletonObjects[typeof(Interface)] = constructor.Invoke(new object[] { });
        }

        public void RegisterScoped<Interface, Realization>()
            where Interface : class
            where Realization : class, Interface
        {
            container[typeof(Interface)] = typeof(Realization);
            var constructor = typeof(Realization).GetConstructor(new Type[] { });
            //SingletonObjects[typeof(Interface)] = constructor.Invoke(new object[] { });
            ScopedObjects[typeof(Interface)] = new();
        }
    }
}
