using Container;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClasses;
using TestClasses.CyclicDependentClasses;

namespace NUnitTest
{
    class CyclicDependence
    {
        [Test]
        public void TestNonCyclicClass()
        {
            var cd = new CyclicDependencies(typeof(Circle));

            Assert.IsTrue(cd.Check());
        }

        [Test]
        public void TestCyclicClass()
        {
            var cd = new CyclicDependencies(typeof(A));

            Assert.IsFalse(cd.Check());
        }

        [Test]
        public void TestRegisterCyclicClass()
        {
            var container = new Container.Container();
            container.Register<I, A>();

            Assert.IsTrue(CyclicCheck.IsCyclic(container));
            try
            {
                container.CheckCyclicDependencies();
            }
            catch (CyclicDependenceException)
            {
                return;
            }
            Assert.Fail("Container didn't find cyclic dependence");
        }

        [Test]
        public void TestRegisterNonCyclicClasses()
        {
            var container = new Container.Container();
            container.Register<IAnimal, Cat>();
            container.Register<IFigure, Circle>(IContainer.Lifestyle.Singleton);
            container.Register<ICalc, Add>(IContainer.Lifestyle.Scoped);

            try
            {
                container.CheckCyclicDependencies();
            }
            catch (CyclicDependenceException)
            {
                Assert.Fail("Container don't have cyclic dependence, but failed");
            }
        }

        [Test]
        public void TestRegisterWithCyclicClasses()
        {
            var container = new Container.Container();
            container.Register<IAnimal, Cat>();
            container.Register<IFigure, Circle>(IContainer.Lifestyle.Singleton);
            container.Register<I, A>(IContainer.Lifestyle.Scoped);

            try
            {
                container.CheckCyclicDependencies();
            }
            catch (CyclicDependenceException)
            {
                return;
            }
            Assert.Fail("Container didn't find cyclic dependence");
        }

        [Test]
        public void TestRegisterSelfCyclicClass()
        {
            var container = new Container.Container();
            container.Register<I, C>();

            try
            {
                container.CheckCyclicDependencies();
            }
            catch (CyclicDependenceException)
            {
                return;
            }
            Assert.Fail("Container didn't find cyclic dependence");
        }

        [Test]
        public void TestRegisterTripleCyclicClass()
        {
            var container = new Container.Container();
            container.Register<I, C1>();

            try
            {
                container.CheckCyclicDependencies();
            }
            catch (CyclicDependenceException)
            {
                return;
            }
            Assert.Fail("Container didn't find cyclic dependence");
        }
    }

    static class CyclicCheck
    {
        public static bool IsCyclic(Container.Container container)
        {
            try
            {
                container.CheckCyclicDependencies();
            }
            catch (CyclicDependenceException)
            {
                return true;
            }
            return false;
        }
    }
        
}
