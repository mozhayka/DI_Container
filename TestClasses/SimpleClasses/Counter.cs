using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClasses
{
    public interface ICount
    {
        public int N { get; }
        public void Inc();
    }

    public class Counter : ICount
    {
        public int N { get; private set;}

        public Counter()
        {
            N = 0;
        }

        public void Inc()
        {
            N++;
        }
    }
}
