using Container;
using NUnit.Framework;
using System;
using System.IO;
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
        public void TestContainerDeserialize()
        {
            var container = new Container.Container();

            container.Register<IAnimal, Cat>();
            string filename = "container.xml";
            var cont = new Container.Container(filename);

            var a = cont.GetInstance<IAnimal>();
            a.Voice();
        }

        [Test]
        public void TestContainerSerializeDeserializeLifestyles()
        {
            var container = new Container.Container();

            container.Register<IFigure, Circle>(Lifestyle.Transient);
            container.Register<ICalc, Add>(Lifestyle.Singleton);
            container.Register<ICount, Counter>(Lifestyle.Scoped);

            string filename = "TestXMLLifestyles.xml";
            container.Serialize(filename);
            var container2 = new Container.Container(filename);

            var a = container2.GetInstance<IFigure>();
            var b = container2.GetInstance<IFigure>();
            Assert.AreNotSame(a, b);

            var c = container2.GetInstance<ICalc>();
            var d = container2.GetInstance<ICalc>();
            Assert.AreSame(c, d);

            var e = container.GetInstance<ICount>();
            var f = container.GetInstance<ICount>();
            Assert.AreSame(e, f);

            using (container.BeginLifetimeScope())
            {
                var g = container.GetInstance<ICount>();
                var h = container.GetInstance<ICount>();
                Assert.AreNotSame(e, g);
                Assert.AreSame(h, g);
            }
        }
    }
}
