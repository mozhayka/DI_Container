using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Classes
{
    class Mult : ICalc
    {
        public int N { get; private set; }

        Mult()
        {
            N = 2;
        }

        public void Step()
        {
            N *= 2;
        }
    }
}
