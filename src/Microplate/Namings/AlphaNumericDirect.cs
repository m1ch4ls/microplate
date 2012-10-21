using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Microplate.Namings
{
    public class AlphaNumericDirect : IPositionNaming
    {
        /// <summary>
        /// GetPosition position name for given coordinates.
        /// </summary>
        public string GetRowName(int row, Format format)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert position name to coordinates.
        /// </summary>
        public Point GetCoords(string name, Format format)
        {
            throw new NotImplementedException();
        }

        public string GetColName(int col, Format format)
        {
            throw new NotImplementedException();
        }
    }
}
