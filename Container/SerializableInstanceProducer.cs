using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Container
{
    [Serializable]
    public class SerializableInstanceProducer
    {
        public string IType, RType;
        public Lifestyle lifestyle;
        public Pair<string, object>[] ScopedObjects;

        public SerializableInstanceProducer(Type IType, Type RType, Lifestyle lifestyle, Dictionary<string, object> ScopedObjects)
        {
            this.IType = IType.ToString();
            this.RType = RType.ToString();
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

        //public static implicit operator InstanceProducer(SerializableInstanceProducer x)
        //{
        //    return new InstanceProducer(x.IType, x.RType, x.lifestyle, x.ScopedObjects.ToDictionary(pair => pair.Key, pair => pair.Value));
        //}
    }
}
