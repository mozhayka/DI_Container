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
            //TestSimpleBound();
            //TestBoundTwoInterfaces();
            //TestDoubleGetInstance();
            TestReflectionContainer();
        }

        private void NewTest()
        {
            Console.WriteLine("----------------------------");
        }

        /*
        public void TestSimpleBound()
        {
            MyContainer container = new();

            container.Register<IAnimal, Cat>();

            var animal = container.GetInstance<IAnimal>();

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

        public void TestDoubleGetInstance()
        {
            MyContainer container = new();

            container.Register<IAnimal, Cat>();

            var animal = container.GetInstance<IAnimal>();
            var animal2 = container.GetInstance<IAnimal>();
            animal2.Voice();

            NewTest();
        }
        */

        public void TestReflectionContainer()
        {
            Container container = new();

            container.Register<IAnimal, Cat>();

            var cat1 = container.GetInstance<IAnimal>();
            var cat2 = container.GetInstance<IAnimal>();

            cat1.Voice();

            NewTest();
        }
    }
}
