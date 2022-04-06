using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    class MyContainer : IContainer
    {
        readonly Dictionary<Type, Object> container;
        public MyContainer()
        {
            container = new();
        }

        public Interface GetInstance<Interface>()
        {
            if (!container.ContainsKey(typeof(Interface)))
                throw new Exception("Interface is not registered");
            
            return (Interface) container[typeof(Interface)];
        }

        public void Register<Interface, Realization>() where Realization : new()
        {
            if (!typeof(Interface).IsAssignableFrom(typeof(Realization)))
                throw new Exception("Its not realization of this interface");
            container[typeof(Interface)] = new Realization();
        }
    }
}
