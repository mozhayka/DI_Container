using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.MyContainerDir
{
    class Scope
    {
        public static string GetScope()
        {
            return Environment.CurrentDirectory;
        }
    }
}
