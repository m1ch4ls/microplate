using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Microplate.Namings
{
    public class NumericCoords : IPositionNaming
    {
        /// <summary>
        /// GetPosition position name for given coordinates.
        /// </summary>
        public string GetRowName(int row)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert position name to coordinates.
        /// </summary>
        public Point GetCoords(string name)
        {
            throw new NotImplementedException();
        }

        public string GetColName(int col)
        {
            throw new NotImplementedException();
        }
    }
}
