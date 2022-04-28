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
                var a = container.GetInstance<ICalc>();
                var b = container.GetInstance<ICalc>();

                Console.WriteLine($"a.N = {a.N}, b.N = {b.N}");
                a.Step();
                a.Step();
                b.Step();
                Console.WriteLine($"a.N = {a.N}, b.N = {b.N}");
            }
        }

        public static void ScopedUsingInsideUsing()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<ICalc, Add>(Lifestyle.Scoped);

            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var a = container.GetInstance<ICalc>();
                var b = container.GetInstance<ICalc>();

                Console.WriteLine($"a.N = {a.N}, b.N = {b.N}");
                a.Step();
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    var c = container.GetInstance<ICalc>();
                    var d = container.GetInstance<ICalc>();

                    Console.WriteLine($"a.N = {a.N}, b.N = {b.N}, c.N = {c.N}, d.N = {d.N}");
                    c.Step();
                    c.Step();
                    d.Step();

                    a.Step();
                    Console.WriteLine($"a.N = {a.N}, b.N = {b.N}, c.N = {c.N}, d.N = {d.N}");
                }
                a.Step();
                b.Step();
                Console.WriteLine($"a.N = {a.N}, b.N = {b.N}");
            }
        }
    }
}
