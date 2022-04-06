using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Examples.Animals
{
    class Dog : IAnimal
    {
        public Dog()
        {
            Console.WriteLine("New Dog");
        }

        public void Voice()
        {
            Console.WriteLine("Gav");
        }
    }
}
