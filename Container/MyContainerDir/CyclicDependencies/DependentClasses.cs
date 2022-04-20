using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.MyContainerDir.CyclicDependencies
{
    class DependentClasses
    {
        Type MainType;
        HashSet<Type> DependetTypes;

        public DependentClasses(Type type)
        {
            MainType = type;

        }

        private void FindDependentTypes()
        {

        }
    }
}
