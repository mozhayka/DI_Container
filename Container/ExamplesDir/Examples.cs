using Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    class Examples : IExamples
    {
        public void RunAll()
        {
            TestSimpleBound();
            TestBoundTwoInterfaces();
        }

        private void NewTest()
        {
            Console.WriteLine("----------------------------");
        }

        public void TestSimpleBound()
        {
            MyContainer container = new();

            container.Register<IAnimal, Cat>();

            IAnimal animal = container.GetInstance<IAnimal>();
            animal.Voice();

            NewTest();
        }

        public void TestBoundTwoInterfaces()
        {
            MyContainer container = new();

            container.Register<IAnimal, Dog>();

            IAnimal animal = container.GetInstance<IAnimal>();

            container.Register<ICar, Car2>();

            var car = container.GetInstance<ICar>();
            animal.Voice();
            car.Beep();

            NewTest();
        }
    }
}
