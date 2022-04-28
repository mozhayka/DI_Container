using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    static class Scope
    {
        static StringBuilder scope = new("scope_0");
        static int n = 1;
        public static string GetScope()
        {
            return scope.ToString();
        }

        public static void OpenNewScope()
        {
            n++;
            scope.Append($"_{n}");
        }

        public static void CloseScope()
        {
            var str = scope.ToString();
            int len = str.Length, i = 1;
            while (str[len - i] != '_')
            {
                i++;
            }
            scope.Remove(len - i, i);
        }
    }

    public class DisposableScope : IDisposable
    {
        public DisposableScope()
        {
            Scope.OpenNewScope();
        }

        public void Dispose()
        {
            Scope.CloseScope();
        }
    }
}
