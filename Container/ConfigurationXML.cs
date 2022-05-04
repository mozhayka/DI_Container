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
        public K Key { get; set; }
        public V Value { get; set; }
    }

    static class ConfigurationXML
    {
        public static void Serialize(Dictionary<Type, InstanceProducer> container, string outputFile)
        {
            //var i = (new Dictionary<Type, InstanceProducer>()).ToArray();
            XmlSerializer formatter = new XmlSerializer(typeof(Pair<Type, InstanceProducer>[]));

            // сохранение массива в файл
            using (FileStream fs = new FileStream(outputFile, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, container
                    .Select(pair => new Pair<Type, InstanceProducer>
                    {
                        Key = pair.Key,
                        Value = pair.Value
                    }));
            }
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
