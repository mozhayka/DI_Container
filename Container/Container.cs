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
        readonly Dictionary<Type, InstanceProducer> instances;

        public Container()
        {
            instances = new();
        }

        public Interface GetInstance<Interface>()
            where Interface : class
        {
            if (!instances.ContainsKey(typeof(Interface)))
                throw new UnregisteredTypeException("Interface is not registered");

            return (Interface) instances[typeof(Interface)].GetInstance();
        }

        public void Register<Interface, Realization>(Lifestyle lifestyle = Lifestyle.Transient)
            where Interface : class
            where Realization : class, Interface
        {
            instances[typeof(Interface)] = new(typeof(Interface), typeof(Realization), lifestyle);
        }

        public void CheckCyclicDependencies()
        {
            foreach (var instance in instances.Values)
            {
                instance.CheckCyclicDependencies();
            }
        }

        public void BeginNewScope()
        {
            Scope.OpenNewScope();
        }

        public void EndOfScope()
        {
            Scope.CloseScope();
        }
    }
}
