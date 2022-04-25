using System;
using SimpleInjector;
using SimpleInjectorExamples;
using TestClasses;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            AllSimpleExamples();
        }

        static void AllSimpleExamples()
        {
            Console.WriteLine("Registration");
            SimpleExamples.Registration();

            Console.WriteLine("Transient");
            SimpleExamples.TransientRegistration();

            Console.WriteLine("Singleton");
            SimpleExamples.SingletonRegistration();
        }
    }
}
