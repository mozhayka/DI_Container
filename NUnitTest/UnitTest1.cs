using NUnit.Framework;
using NUnitTest.Classes;

namespace NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Test1();
            Test2();
        }

        [Test]
        public void Test1()
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
        public void Test2()
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