using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Microplate.Namings;

namespace Microplate
{
    public static class PositionNamings
    {
        public static readonly IPositionNaming NumericCoords = new NumericCoords();

        public static readonly IPositionNaming AlphaNumericCoords = new AlphaNumericCoords();

        public static readonly IPositionNaming NumericDirect = new NumericDirect();

        public static readonly IPositionNaming AlphaNumericDirect = new AlphaNumericDirect();

        public static IPositionNaming Default { get { return NumericCoords; } }
    }
}
