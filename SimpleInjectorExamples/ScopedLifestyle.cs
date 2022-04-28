using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClasses;

namespace SimpleInjectorExamples
{
    public class ScopedLifestyleEx
    {
        public static void ScopedRegistration()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<ICalc, Add>(Lifestyle.Scoped);

            // Вне using нельзя вызывать GetInstance()
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var c = container.GetInstance<ICalc>();
                var d = container.GetInstance<ICalc>();
                Console.WriteLine($"c.N = {c.N}, d.N = {d.N}");
                c.Step();
                c.Step();
                d.Step();
                Console.WriteLine($"c.N = {c.N}, d.N = {d.N}");
            }
        }
    }
}
