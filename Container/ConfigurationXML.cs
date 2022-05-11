using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Container
{
    public class Pair<K, V>
    {
        public K Key;
        public V Value;

        public Pair(K Key, V Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public Pair() { }
    }

    public class Conv
    {
        public static Type StrToType(string str_type)
        {
            Type type = Type.GetType(str_type);
            return type;
        }

        public static string TypeToStr(Type type)
        {
            string str_type = type.AssemblyQualifiedName;
            return str_type;
        }
    }

    public static class ConfigurationXML
    {    
        public static void Serialize(Dictionary<Type, InstanceProducer> container, string outputFile)
        {
            XmlSerializer formatter = new(typeof(Pair<string, SerializableInstanceProducer>[]));

            // сохранение массива в файл
            using FileStream fs = new(outputFile, FileMode.Create);

            var SerializableContainer = container
                .Select(pair => new Pair<string, SerializableInstanceProducer>
                (
                    Conv.TypeToStr(pair.Key),
                    pair.Value
                )).ToArray();

            formatter.Serialize(fs, SerializableContainer);
        }

        public static Dictionary<Type, InstanceProducer> Deserialize(string inputFile)
        {
            XmlSerializer formatter = new(typeof(Pair<string, SerializableInstanceProducer>[]));
            using FileStream fs = new(inputFile, FileMode.OpenOrCreate);

            var newpeople = formatter.Deserialize(fs) as Pair<string, SerializableInstanceProducer>[];
            return newpeople.ToDictionary(pair => Conv.StrToType(pair.Key), pair => (InstanceProducer)pair.Value);
        }
    }
}
