using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Container.IContainer;

namespace Container
{
    public class SerializableInstanceProducer
    {
        public Type IType, RType;
        public Lifestyle lifestyle;
        public Pair<string, object>[] ScopedObjects;

        public SerializableInstanceProducer(Type IType, Type RType, Lifestyle lifestyle, Dictionary<string, object> ScopedObjects) 
        {
            this.IType = IType;
            this.RType = RType;
            this.lifestyle = lifestyle;
            this.ScopedObjects = ScopedObjects
                .Select(x => new Pair<string, object>
                (
                    x.Key,
                    x.Value
                )).ToArray();
        }

        public SerializableInstanceProducer() { }

        public static implicit operator SerializableInstanceProducer(InstanceProducer x)
        {
            return new SerializableInstanceProducer(x.IType, x.RType, x.lifestyle, x.ScopedObjects);
        }

        public static implicit operator InstanceProducer(SerializableInstanceProducer x)
        {
            return new InstanceProducer(x.IType, x.RType, x.lifestyle, x.ScopedObjects.ToDictionary(pair => pair.Key, pair => pair.Value));
        }
    }

    public class InstanceProducer
    {
        public Type IType, RType;
        public Lifestyle lifestyle;
        private Func<object> instanceCreator;
        public Dictionary<string, object> ScopedObjects;

        public InstanceProducer(Type interface_type, Type realization_type, Lifestyle lifestyle)
        {
            IType = interface_type;
            RType = realization_type;
            this.lifestyle = lifestyle;
            InitInstanceCreator();
            ScopedObjects = new();
        }

        public InstanceProducer(Type interface_type, Type realization_type, Lifestyle lifestyle, Dictionary<string, object> ScopedObjects)
        {
            IType = interface_type;
            RType = realization_type;
            this.lifestyle = lifestyle;
            InitInstanceCreator();
            this.ScopedObjects = ScopedObjects;
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
