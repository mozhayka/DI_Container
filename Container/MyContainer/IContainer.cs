using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.MyContainer
{
    interface IContainer
    {
        public void Register<Interface, Realization>();
        public Type GetInstance<Interface>();
    }
}
