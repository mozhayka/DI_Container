using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClasses.CyclicDependentClasses;

namespace SimpleInjectorExamples
{
    class CyclicDependencies
    {
        public static void Registration()
        {
            var container = new Container();

            container.Register<I, A>();

            container.Verify();
            Console.WriteLine("Get instance");
            var a = container.GetInstance<I>();
        }
    }
}
