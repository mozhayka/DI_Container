using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Classes
{
    class Add : ICalc
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
}
