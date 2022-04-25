using Container;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClasses;

namespace NUnitTest
{
    class UnregisteredTypes
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestUnregisteredType()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>();

            var a = container.GetInstance<ICalc>();
            
            try
            {
                var b = container.GetInstance<IAnimal>();
            }
            catch (UnregisteredTypeException)
            {
                return;
            }

            Assert.Fail("Got unregistered type");
        }
    }
}
