using Container;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestClasses;

namespace NUnitTest
{
    public class KnownType { }

    class XmlConfig
    {
        [Test]
        public void TestTypeToString()
        {
            string cat = typeof(Cat).ToString();
            //Type type = Type.GetType(cat); //target type
            
            Assembly asm = typeof(KnownType).Assembly;
            Type type = asm.GetType(cat);

            Assert.AreEqual(typeof(Cat), type);
        }

        [Test]
        public void TestSerialize()
        {
            var container = new Container.Container();

            container.Register<IAnimal, Cat>();

            container.Serialize();
            try
            {
                container.Serialize();
            }
            catch 
            {
                Assert.Fail("Serialize failed");
            }
        }
    }
}
