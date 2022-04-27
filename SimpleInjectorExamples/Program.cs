using System;
using System.Text;
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
            CyclicDependenciesExamples();
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

        static void CyclicDependenciesExamples()
        {
            Console.WriteLine("Cyclic");
            CyclicDependencies.Registration();
        }
    }
}
