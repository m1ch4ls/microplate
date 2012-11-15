using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microplate
{
    class InvalidPosition : Exception
    {
        public InvalidPosition(string pos)
            : base(String.Format("Position {0} is invalid", pos))
        {  
        }

        public InvalidPosition(int pos)
            : base(String.Format("Position {0} is invalid", pos))
        {
        }
    }
}
