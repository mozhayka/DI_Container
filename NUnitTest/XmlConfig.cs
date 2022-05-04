using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClasses;

namespace NUnitTest
{
    class XmlConfig
    {
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
