using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageExample
{
    interface Window
    {
        public string Name { get; }
        public bool IsOpened { get; }
        public void Print();
        public void Close();
        public void Open();
    }

    class SimpleWindow : Window
    {
        static int N = 0;
        public string Name { get; }

        public bool IsOpened { get; private set; }

        public SimpleWindow()
        {
            Name = $"Window №{N}";
            N++;
            IsOpened = false;
        }

        public void Close()
        {
            IsOpened = false;
        }

        public void Open()
        {
            IsOpened = true;
        }

        public void Print()
        {
            Console.WriteLine($"{Name} is {(IsOpened ? "opened" : "closed")}");
        }
    }
}
