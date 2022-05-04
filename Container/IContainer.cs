﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{        
    public enum Lifestyle
    {
        Singleton,
        Transient,
        Scoped
    }

    public interface IContainer
    {


        public void Register<Interface, Realization>(Lifestyle lifestyle) 
            where Interface : class 
            where Realization : class, Interface;

        public Interface GetInstance<Interface>()
            where Interface : class;

        public void CheckCyclicDependencies();

        public void OpenScope();
        public void CloseScope();
        public DisposableScope BeginLifetimeScope();
    }
}
