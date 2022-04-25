using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClasses
{
    public interface IFigure
    {
        public int GetSquare();
        public void Scale(int s);
    }

    public class Circle : IFigure
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

    public class Rectangle : IFigure
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
