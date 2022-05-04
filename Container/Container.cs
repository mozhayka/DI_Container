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

        public Container(string fileName)
        {
            instances = Deserialize(fileName);
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

        public void Register<T1, T2>(object singleton)
        {
            throw new NotImplementedException();
        }

        public void OpenScope()
        {
            Scope.OpenNewScope();
        }

        public void CloseScope()
        {
            Scope.CloseScope();
        }

        public DisposableScope BeginLifetimeScope()
        {
            return new DisposableScope();
        }

        public void Serialize(string outputFile = "container.xml")
        {
            ConfigurationXML.Serialize(instances, outputFile);
        }

        private Dictionary<Type, InstanceProducer> Deserialize(string inputFile = "container.xml")
        {
            return ConfigurationXML.Deserialize(inputFile);
        }
    }
}
