using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    class Car1 : ICar
    {
        public void Beep()
        {
            Console.WriteLine("Beep");
        }
    }
}
