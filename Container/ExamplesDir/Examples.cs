using Container.Examples.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Examples
{
    class Examples : IExamples
    {
        public void RunAll()
        {
            TestSimpleBound();
        }

        public void TestSimpleBound()
        {
            MyContainer.MyContainer container = new();

            container.Register<IAnimal, Cat>();

            IAnimal animal = container.GetInstance<IAnimal>();
            animal.Voice();
        }
    }
}
