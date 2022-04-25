using SimpleInjector;
using TestClasses;
using System;

namespace SimpleInjectorExamples
{
    class SimpleExamples
    {
        public static void Registration()
        {
            var container = new Container();

            container.Register<IAnimal, Cat>(); // Без параметров работает как Transient

            var a = container.GetInstance<IAnimal>();

            a.Voice();
        }

        public static void TransientRegistration()
        {
            var container = new Container();

            container.Register<ICalc, Add>(Lifestyle.Transient);

            Console.WriteLine("a init");
            var a = container.GetInstance<ICalc>();
            Console.WriteLine("b init");
            var b = container.GetInstance<ICalc>();
            Console.WriteLine($"a.N = {a.N}, b.N = {b.N}");

            Console.WriteLine("a step");
            a.Step();
            Console.WriteLine("a step");
            a.Step();
            Console.WriteLine("b step");
            b.Step();

            Console.WriteLine($"a.N = {a.N}, b.N = {b.N}");
        }

        public static void SingletonRegistration()
        {
            var container = new Container();

            container.Register<ICalc, Add>(Lifestyle.Singleton);

            Console.WriteLine("a init");
            var a = container.GetInstance<ICalc>();
            Console.WriteLine("b init");
            var b = container.GetInstance<ICalc>();
            Console.WriteLine($"a.N = {a.N}, b.N = {b.N}");

            Console.WriteLine("a step");
            a.Step();
            Console.WriteLine("a step");
            a.Step();
            Console.WriteLine("b step");
            b.Step();

            Console.WriteLine($"a.N = {a.N}, b.N = {b.N}");
        }
    }

}
