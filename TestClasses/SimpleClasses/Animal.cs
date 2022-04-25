using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClasses
{
    public interface IAnimal
    {
        public void Voice();
    }

    public class Cat : IAnimal
    {
        public Cat()
        {
            Console.WriteLine("New Cat");
        }

        public void Voice()
        {
            Console.WriteLine("Maw");
        }
    }
}
