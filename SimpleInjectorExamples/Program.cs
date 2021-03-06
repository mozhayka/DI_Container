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
            // CyclicDependenciesExamples();
            ScopedLifestyleExamples();
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

        static void ScopedLifestyleExamples()
        {
            Console.WriteLine("Scoped");
            ScopedLifestyleEx.ScopedRegistration();

            Console.WriteLine("Double scope");
            ScopedLifestyleEx.ScopedUsingInsideUsing();
        }

        // Simple Injector doesn't support XML based configuration
        // https://simpleinjector.readthedocs.org/en/latest/decisions.html#no-support-for-xml
    }
}
