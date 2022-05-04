using Container;
using NUnit.Framework;
using TestClasses;
using static Container.IContainer;

namespace NUnitTest
{
    class AdvancedTests
    {
        [Test]
        public void TestMultipleRegistrations()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>(Lifestyle.Singleton);
            container.Register<IFigure, Circle>(Lifestyle.Transient);

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            a.Step();
            b.Step();

            Assert.AreEqual(3, a.N);

            var c1 = container.GetInstance<IFigure>();
            var c2 = container.GetInstance<IFigure>();

            c1.Scale(3);
            c2.Scale(2);

            Assert.AreEqual(c1.GetSquare(), 27);
            Assert.AreEqual(c2.GetSquare(), 12);
        }

        [Test]
        public void TestChangeRegistrations()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>(Lifestyle.Singleton);
            container.Register<ICalc, Mult>(Lifestyle.Transient);

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            a.Step();
            b.Step();

            Assert.AreEqual(4, a.N);
        }
    }
}
