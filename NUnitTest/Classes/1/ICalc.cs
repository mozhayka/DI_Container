using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Classes
{
    interface ICalc
    {
        public int N { get; }
        public void Step();
    }
}
