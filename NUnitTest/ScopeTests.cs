using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClasses;
using static Container.IContainer;

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
    }
}
