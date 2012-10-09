using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microplate.Types
{
    public class DefaultType : IPlateType
    {
        public string Name { get; set; }
        public Format Format { get; set; }
        public string Manufacturer { get; set; }
        public decimal Volume { get; set; }
    }
}
