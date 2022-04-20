using NUnit.Framework;
using NUnitTest.Classes;
using System;

namespace NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestTransient();
            TestSingleton();
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

            Assert.AreEqual(a.N, 2);
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

            Assert.AreEqual(a.N, 3);
        }
    }
}