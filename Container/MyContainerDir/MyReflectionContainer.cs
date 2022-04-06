using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Container.IContainer;

namespace Container
{
    class MyReflectionContainer : IContainer
    {
        readonly Dictionary<Type, Type> container;
        public MyReflectionContainer()
        {
            container = new();
        }
        public Interface GetInstance<Interface>()
        {
            if (!container.ContainsKey(typeof(Interface)))
                throw new Exception("Interface is not registered");

            var constructor = container[typeof(Interface)].GetConstructor(new Type[] { });
            return (Interface) constructor.Invoke(new object[] { });
        }

        public void Register<Interface, Realization>(IContainer.Lifestyle lifestyle = Lifestyle.Singleton) where Realization : new()
        {
            if (!typeof(Interface).IsAssignableFrom(typeof(Realization)))
                throw new Exception("Its not realization of this interface");

            container[typeof(Interface)] = typeof(Realization);
        }
    }
}
