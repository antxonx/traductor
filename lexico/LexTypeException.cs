using System;
using System.Collections.Generic;
using System.Text;

namespace Lexico
{
    class LexTypeException : Exception
    {
        public LexTypeException(string message) : base(message) { }
    }
}
