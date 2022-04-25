using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClasses
{
    public interface ICalc
    {
        public int N { get; }
        public void Step();
    }

    public class Add : ICalc
    {
        public int N { get; private set; }

        public Add()
        {
            N = 1;
        }

        public void Step()
        {
            N++;
        }
    }

    public class Mult : ICalc
    {
        public int N { get; private set; }

        public Mult()
        {
            N = 2;
        }

        public void Step()
        {
            N *= 2;
        }
    }
}
