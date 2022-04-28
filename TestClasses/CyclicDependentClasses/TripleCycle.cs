using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClasses.CyclicDependentClasses
{

    public class C1 : I
    {
        private C2 c;
        public C1()
        {
            Console.WriteLine("Creating instance of C1");
            c = new C2();
        }
    }

    public class C2 : I
    {
        private C3 c;
        public C2()
        {
            Console.WriteLine("Creating instance of C2");
            c = new C3();
        }
    }

    public class C3 : I
    {
        private C1 c;
        public C3()
        {
            Console.WriteLine("Creating instance of C3");
            c = new C1();
        }
    }

    public class C4 : I
    {
        private C1 c;
        public C4()
        {
            Console.WriteLine("Creating instance of C4");
            c = new C1();
        }
    }
}
