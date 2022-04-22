using NUnit.Framework;
using NUnitTest.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest
{
    class SimpleTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestTransient()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>();

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            a.Step();
            b.Step();

            Assert.AreEqual(2, a.N);
        }

        [Test]
        public void TestSingleton()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>(Container.IContainer.Lifestyle.Singleton);

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            a.Step();
            b.Step();

            Assert.AreEqual(3, a.N);
        }
    }
}
