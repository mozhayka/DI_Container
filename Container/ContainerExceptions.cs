﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class UnregisteredTypeException : Exception
    {
        public UnregisteredTypeException(string message)
            : base(message) { }
    }

    public class CyclicDependenceException : Exception
    {
        public CyclicDependenceException(string message)
            : base(message) { }
    }
}
