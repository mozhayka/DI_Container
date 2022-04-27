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
}
