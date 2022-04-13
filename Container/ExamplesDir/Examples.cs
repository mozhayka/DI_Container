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
            TestLifestyleTransient();
        }

        private void NewTest()
        {
            Console.WriteLine("----------------------------");
        }

        public void TestLifestyleTransient()
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
