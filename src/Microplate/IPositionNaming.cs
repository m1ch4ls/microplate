using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Microplate
{
    /// <summary>
    /// We have 4 main position naming conventions. This is common interface.
    /// </summary>
    public interface IPositionNaming
    {
        /// <summary>
        /// Row name for given position naming.
        /// </summary>
        /// <param name="row">Internal row index starting with 0.</param>
        /// <param name="format">A current format</param>
        /// <returns>
        /// Row name string.
        /// </returns>
        string GetRowName(int row, Format format);

        /// <summary>
        /// Column name for given position naming.
        /// </summary>
        /// <param name="col">Internal column index starting with 0.</param>
        /// <param name="format">A current format</param>
        /// <returns>
        /// Column name string
        /// </returns>
        string GetColName(int col, Format format);

        /// <summary>
        /// Convert position name to coordinates.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="format">A current format</param>
        /// <returns><see cref="Point"/> of given coordinates</returns>
        Point GetCoords(string name, Format format);
    }
}
