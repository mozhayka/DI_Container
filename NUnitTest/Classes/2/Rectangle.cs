using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Classes
{
    class Rectangle : IFigure
    {
        private int w, h;

        public Rectangle() : this(1, 1) { }
        
        public Rectangle(int w, int h)
        {
            this.w = w;
            this.h = h;
        }

        public int GetSquare()
        {
            return w * h;
        }

        public void Scale(int s)
        {
            w *= s;
            h *= s;
        }
    }
}
