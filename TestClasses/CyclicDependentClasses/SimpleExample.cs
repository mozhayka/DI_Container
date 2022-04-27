using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClasses.CyclicDependentClasses
{
    public interface I
    {
    }

    public class A : I
    {
        private B b;
        public A()
        {
            Console.WriteLine("Creating instance of A");
            b = new B();
        }
    }

    public class B
    {
        private A a;
        public B()
        {
            Console.WriteLine("Creating instance of B");
            Console.WriteLine("Press enter");
            Console.ReadLine();
            a = new A();
        }
    }
}
