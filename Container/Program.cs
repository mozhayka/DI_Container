using Container.Examples;
using System;

namespace Container
{
    class Program
    {
        static void Main(string[] args)
        {
            IExamples example = new Examples.Examples();
            example.RunAll();
        }
    }
}
