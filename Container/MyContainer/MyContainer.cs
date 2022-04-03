using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.MyContainer
{
    class MyContainer : IContainer
    {
        readonly Dictionary<Type, Type> container;
        public MyContainer()
        {
            container = new();
        }

        public Type GetInstance<Interface>()
        {
            if (!container.ContainsKey(typeof(Interface)))
                throw new Exception("Interface is not registered");
            return container[typeof(Interface)];
        }

        public void Register<Interface, Realization>()
        {
            container[typeof(Interface)] = typeof(Realization);
        }
    }
}
