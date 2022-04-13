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

        public Container()
        {
            container = new();
        }

        public Interface GetInstance<Interface>() 
            where Interface : class
        {
            if (!container.ContainsKey(typeof(Interface)))
                throw new Exception("Interface is not registered");

            var constructor = container[typeof(Interface)].GetConstructor(new Type[] { });
            return (Interface) constructor.Invoke(new object[] { });
        }

        public void Register<Interface, Realization>(Lifestyle lifestyle = Lifestyle.Transient)
            where Interface : class
            where Realization : class, Interface
        {
            if (lifestyle == Lifestyle.Transient)
            {
                container[typeof(Interface)] = typeof(Realization);
            }
        }
    }
}
