using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microplate
{
    public interface PlateType
    {
        string Name
        {
            get;
            set;
        }

        PlateFormat Format
        {
            get;
            set;
        }

        string Manufacturer
        {
            get;
            set;
        }

        decimal Volume
        {
            get;
            set;
        }
    }
}
