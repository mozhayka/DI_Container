using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Classes
{
    class Circle : IFigure
    {
        private int r = 1;
        private const int pi = 3;

        public Circle() { }

        public Circle(int r)
        {
            this.r = r;
        }

        public int GetSquare()
        {
            return r * r * pi;
        }

        public void Scale(int s)
        {
            r *= s;
        }
    }
}
