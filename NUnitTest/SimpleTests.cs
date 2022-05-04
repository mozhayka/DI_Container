using Container;
using NUnit.Framework;
using TestClasses;


namespace NUnitTest
{
    class SimpleTests
    {

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

            container.Register<ICalc, Add>(Lifestyle.Singleton);

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            a.Step();
            b.Step();

            Assert.AreEqual(3, a.N);
        }
    }
}
