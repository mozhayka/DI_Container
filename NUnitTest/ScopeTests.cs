using Container;
using NUnit.Framework;
using TestClasses;


namespace NUnitTest
{
    class ScopeTests
    {
        [Test]
        public void TestNewScope()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>(Lifestyle.Scoped);

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            a.Step();
            b.Step();

            Assert.AreEqual(3, a.N);

            container.OpenScope();

            var c  = container.GetInstance<ICalc>();
            var d = container.GetInstance<ICalc>();

            c.Step();

            Assert.AreEqual(2, c.N);
        }

        [Test]
        public void TestDisposableScope()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>(Lifestyle.Scoped);

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            a.Step();
            b.Step();

            Assert.AreEqual(3, b.N);

            using(container.BeginLifetimeScope())
            {
                var c = container.GetInstance<ICalc>();
                c.Step();
                Assert.AreEqual(2, c.N);
            }
            a.Step();
            Assert.AreEqual(4, a.N);
        }

        [Test]
        public void TestInnerUsingScope()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>(Lifestyle.Scoped);

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            a.Step();
            b.Step();

            Assert.AreEqual(3, b.N);

            using (container.BeginLifetimeScope())
            {
                var c = container.GetInstance<ICalc>();
                c.Step();
                Assert.AreEqual(2, c.N);
                using (container.BeginLifetimeScope())
                {
                    var d = container.GetInstance<ICalc>();
                    c.Step();
                    d.Step();
                    Assert.AreEqual(2, d.N);
                    Assert.AreEqual(3, c.N);
                }
                c.Step();
                Assert.AreEqual(4, c.N);
            }
            a.Step();
            Assert.AreEqual(4, a.N);
        }

        [Test]
        public void TestDoubleUsingScope()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>(Lifestyle.Scoped);

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            a.Step();
            b.Step();

            Assert.AreEqual(3, b.N);

            using (container.BeginLifetimeScope())
            {
                var c = container.GetInstance<ICalc>();
                c.Step();
                Assert.AreEqual(2, c.N);

                c.Step();
                Assert.AreEqual(3, c.N);
            }                

            using (container.BeginLifetimeScope())
            {
                var d = container.GetInstance<ICalc>();
                d.Step();
                b.Step();
                Assert.AreEqual(2, d.N);
            }

            a.Step();
            Assert.AreEqual(5, a.N);
        }

        [Test]
        public void TestRegisterTwoScoped()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>(Lifestyle.Scoped);
            container.Register<IFigure, Circle>(Lifestyle.Scoped);

            var a = container.GetInstance<ICalc>();
            var b = container.GetInstance<ICalc>();
            var c1 = container.GetInstance<IFigure>();
            a.Step();
            b.Step();

            Assert.AreEqual(3, b.N);

            using (container.BeginLifetimeScope())
            {
                var d = container.GetInstance<ICalc>();
                d.Step();
                Assert.AreEqual(2, d.N);

                var c2 = container.GetInstance<IFigure>();
                c2.Scale(2);
                Assert.AreEqual(12, c2.GetSquare());

                var c3 = container.GetInstance<IFigure>();
                c3.Scale(3);
                Assert.AreEqual(108, c2.GetSquare());
                Assert.AreEqual(108, c3.GetSquare());
            }

            a.Step();
            Assert.AreEqual(4, a.N);
            Assert.AreEqual(3, c1.GetSquare());
        }
    }
}
