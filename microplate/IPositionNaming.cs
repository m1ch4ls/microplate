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
        /// GetPosition postition name for given coords.
        /// </summary>
        string GetRowName(int row);

        /// <summary>
        /// Convert position name to coords.
        /// </summary>
        Point GetCoords(string name);

        string GetColName(int col);
    }
}
