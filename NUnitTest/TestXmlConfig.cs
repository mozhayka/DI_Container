using Container;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestClasses;

namespace NUnitTest
{ 
    class TestXmlConfig
    {
        [Test]
        public void TestTypeToString()
        {
            string cat = Conv.TypeToStr(typeof(Cat));
            Type cat_type = Conv.StrToType(cat);

            Assert.AreEqual(typeof(Cat), cat_type);
        }

        [Test]
        public void TestSerializeArrayPair()
        {
            XmlSerializer formatter = new(typeof(Pair<string, int>[]));

            using FileStream fs = new("test.xml", FileMode.Create);

            var obj = new Pair<string, int>[2];
            obj[0] = new Pair<string, int>("a", 1);
            obj[0] = new Pair<string, int>("b", 2);

            formatter.Serialize(fs, obj);
        }

        [Test]
        public void TestContainerSerialize()
        {
            var container = new Container.Container();

            container.Register<IAnimal, Cat>();
            container.Register<ICalc, Add>();

            try
            {
                container.Serialize();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [Test]
        public void TestDeserialize()
        {
            var container = new Container.Container();

            container.Register<IAnimal, Cat>();
            string filename = "container.xml";
            var cont = new Container.Container(filename);

            var a = cont.GetInstance<IAnimal>();
            a.Voice();
        }
    }
}
