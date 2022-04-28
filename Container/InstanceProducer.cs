using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Container.IContainer;

namespace Container
{
    class InstanceProducer
    {
        Type IType, RType;
        Lifestyle lifestyle;
        Func<object> instanceCreator;
        Dictionary<string, object> ScopedObjects = new();

        public InstanceProducer(Type interface_type, Type realization_type, Lifestyle lifestyle)
        {
            IType = interface_type;
            RType = realization_type;
            this.lifestyle = lifestyle;
            InitInstanceCreator();
        }

        public object GetInstance()
        {
            return instanceCreator();
        }

        private void InitInstanceCreator()
        {
            switch (lifestyle)
            {
                case Lifestyle.Transient:
                    instanceCreator = () => RType.GetConstructor(Array.Empty<Type>()).Invoke(Array.Empty<object>());
                    break;
                case Lifestyle.Singleton:
                    var SingleObject = RType.GetConstructor(Array.Empty<Type>()).Invoke(Array.Empty<object>());
                    instanceCreator = () => SingleObject;
                    break;
                case Lifestyle.Scoped:
                    instanceCreator = () => InstanceCreatorScoped();
                    break;
            }
        }

        private object InstanceCreatorScoped()
        {
            var scope = Scope.GetScope();
            if (!ScopedObjects.ContainsKey(scope))
            {
                var SingleObject = RType.GetConstructor(Array.Empty<Type>()).Invoke(Array.Empty<object>());
                ScopedObjects[scope] = SingleObject;
            }
            return ScopedObjects[scope];
        }

        public void CheckCyclicDependencies()
        {
            CyclicDependencies cd = new(RType);
            if (cd.Check() == false)
                throw new CyclicDependenceException($"{RType} is a cyclic type");
        }
    }
}
