﻿using NUnit.Framework;
using NUnitTest.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Container.IContainer;

namespace NUnitTest
{
    class AdvancedTests
    {
        [SetUp]
        public void Setup()
        {

        }

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

            Assert.AreEqual(a.N, 3);

            var c1 = container.GetInstance<IFigure>();
            var c2 = container.GetInstance<IFigure>();

            c1.Scale(3);
            c2.Scale(2);

            Assert.AreEqual(c1.GetSquare(), 27);
            Assert.AreEqual(c2.GetSquare(), 12);
        }
    }
}
