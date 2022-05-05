using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Container
{
    [Serializable]
    public class Pair<K, V>
    {
        [XmlIgnore]
        public K Key;
        public V Value;

        public Pair(K Key, V Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public Pair() { }
    }

    static class ConfigurationXML
    {
        public static void Serialize(Dictionary<Type, InstanceProducer> container, string outputFile)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Pair<Type, SerializableInstanceProducer>[]));

            // сохранение массива в файл
            using FileStream fs = new(outputFile, FileMode.OpenOrCreate);
            var SerializableContainer = container
                .Select(pair => new Pair<Type, SerializableInstanceProducer>
                (
                    pair.Key,
                    (SerializableInstanceProducer)pair.Value
                )).ToArray();
            formatter.Serialize(fs, SerializableContainer);
        }

        public static Dictionary<Type, InstanceProducer> Deserialize(string inputFile)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Dictionary<Type, InstanceProducer>));
            using (FileStream fs = new FileStream(inputFile, FileMode.OpenOrCreate))
            {
                var newpeople = formatter.Deserialize(fs) as KeyValuePair<Type, InstanceProducer>[];
                return newpeople.ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }
    }
}
