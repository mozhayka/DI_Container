using System;
using SimpleInjector;


namespace Example
{
    interface IAnimal
    {
        public void Voice();
    }

    class Cat : IAnimal
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

    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();

            container.Register<IAnimal, Cat>();

            var a = container.GetInstance<IAnimal>();
            var b = container.GetInstance<IAnimal>();
            a.Voice();
        }
    }
}
