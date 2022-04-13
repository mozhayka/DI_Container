using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public interface IContainer
    {
        public enum Lifestyle
        {
            Singleton,
            Transient
        }

        public void Register<Interface, Realization>(Lifestyle lifestyle) where Realization : class;
        public Interface GetInstance<Interface>();
    }
}
