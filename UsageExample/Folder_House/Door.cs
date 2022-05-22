using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageExample
{
    interface Door
    {
        public bool IsOpened { get; }
        public void Open();
        public void Close();
        public void KnockDown();
        public void Print();
    }

    class SimpleDoor : Door
    {
        public bool IsOpened { get; private set; }
        private bool IsBroken = false;

        public SimpleDoor()
        {
            IsOpened = false;
        }

        public void Close()
        {
            if (IsBroken)
                return;
            IsOpened = false;
        }

        public void KnockDown()
        {
            IsBroken = true;
            IsOpened = true;
        }

        public void Open()
        {
            IsOpened = true;
        }

        public void Print()
        {
            Console.WriteLine($"Door is {(IsBroken ? "broken" : (IsOpened ? "opened" : "closed"))}");
        }
    }
}
