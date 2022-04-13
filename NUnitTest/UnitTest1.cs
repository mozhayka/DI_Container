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
        }

        [Test]
        public void Test1()
        {
            var container = new Container.Container();

            container.Register<ICalc, Add>();

            var a = container.GetInstance<ICalc>();
            a.Step();
            Assert.AreEqual(a.N, 2);
        }
    }
}