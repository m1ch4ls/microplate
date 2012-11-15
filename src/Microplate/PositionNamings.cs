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
        public static readonly NumericCoords NumericCoords = new NumericCoords();

        public static readonly AlphaNumericCoords AlphaNumericCoords = new AlphaNumericCoords();

        public static readonly NumericDirect NumericDirect = new NumericDirect();

        public static readonly AlphaNumericDirect AlphaNumericDirect = new AlphaNumericDirect();

        public static IPositionNaming Default { get { return NumericCoords; } }
    }
}
