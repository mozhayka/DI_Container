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
        public void TestEmptyContainer()
        {
            var container = new Container.Container();

            Assert.IsFalse(CyclicCheck.IsCyclic(container));
        }

        [Test]
        public void TestRegisterCyclicClass()
        {
            var container = new Container.Container();
            container.Register<I, A>();

            Assert.IsTrue(CyclicCheck.IsCyclic(container));
        }

        [Test]
        public void TestRegisterNonCyclicClasses()
        {
            var container = new Container.Container();
            container.Register<IAnimal, Cat>();
            container.Register<IFigure, Circle>(IContainer.Lifestyle.Singleton);
            container.Register<ICalc, Add>(IContainer.Lifestyle.Scoped);

            Assert.IsFalse(CyclicCheck.IsCyclic(container));
        }

        [Test]
        public void TestRegisterWithCyclicClasses()
        {
            var container = new Container.Container();
            container.Register<IAnimal, Cat>();
            container.Register<IFigure, Circle>(IContainer.Lifestyle.Singleton);
            container.Register<I, A>(IContainer.Lifestyle.Scoped);

            Assert.IsTrue(CyclicCheck.IsCyclic(container));
        }

        [Test]
        public void TestRegisterSelfCyclicClass()
        {
            var container = new Container.Container();
            container.Register<I, C>();

            Assert.IsTrue(CyclicCheck.IsCyclic(container));
        }

        [Test]
        public void TestRegisterTripleCyclicClass()
        {
            var container = new Container.Container();
            container.Register<I, C1>();

            Assert.IsTrue(CyclicCheck.IsCyclic(container));
        }

        [Test]
        public void TestCycleC4()
        {
            var container = new Container.Container();
            container.Register<I, C4>();

            Assert.IsTrue(CyclicCheck.IsCyclic(container));
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
