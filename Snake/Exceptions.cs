using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class IncorrectValueExciption : Exception
    {
        public IncorrectValueExciption(string message) : base(message)
        {
        }
    }
}
