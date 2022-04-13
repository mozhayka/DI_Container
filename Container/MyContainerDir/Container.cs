﻿using System;
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
        {
            if (!container.ContainsKey(typeof(Interface)))
                throw new Exception("Interface is not registered");

            var constructor = container[typeof(Interface)].GetConstructor(new Type[] { });
            return (Interface) constructor.Invoke(new object[] { });
        }

        public void Register<Interface, Realization>(Lifestyle lifestyle = Lifestyle.Singleton) where Realization : class
        {
            if (!typeof(Interface).IsAssignableFrom(typeof(Realization)))
                throw new Exception("Its not realization of this interface");

            container[typeof(Interface)] = typeof(Realization);
        }
    }
}
