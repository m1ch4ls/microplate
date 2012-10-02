using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Microplate
{
    public interface IPositionNaming
    {

        /// <summary>
        /// GetPosition position name for given coordinates.
        /// </summary>
        string GetRowName(int row);

        /// <summary>
        /// Convert position name to coordinates.
        /// </summary>
        Point GetCoords(string name);

        string GetColName(int col);
    }
}
