using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microplate
{
    public class PlateType
    {
        public string Name
        {
            get;
            set;
        }

        public PlateFormat Format
        {
            get;
            set;
        }

        public string Manufacturer
        {
            get;
            set;
        }

        public decimal Volume
        {
            get;
            set;
        }
    }
}
