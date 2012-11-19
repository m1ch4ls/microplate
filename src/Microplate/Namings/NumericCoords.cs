using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microplate.Namings
{
    [Serializable]
    public class NumericCoords : IPositionNaming
    {
        /// <summary>
        /// Row name index starting with 1. Returns 1 for all negative integers.
        /// </summary>
        /// <param name="row">Internal row index starting with 0.</param>
        /// <param name="format">Arbitrary format of the <see cref="Plate"/></param>
        /// <returns>
        /// Row name string.
        /// </returns>
        public virtual string GetRowName(int row, Format format)
        {
            return row <= 0
                       ? "1"
                       : row >= format.Height
                             ? (format.Height - 1).ToString(CultureInfo.InvariantCulture)
                             : (row + 1).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Column name for given position naming.
        /// </summary>
        /// <param name="col">Internal column index starting with 0.</param>
        /// <param name="format">Arbitrary format of the <see cref="Plate"/></param>
        /// <returns>
        /// Column name string
        /// </returns>
        public virtual string GetColName(int col, Format format)
        {
            return col <= 0
                       ? "1"
                       : col >= format.Width
                             ? (format.Width - 1).ToString(CultureInfo.InvariantCulture)
                             : (col + 1).ToString(CultureInfo.InvariantCulture);
        }

        static readonly Regex CoordsRegex = new Regex(@"^(\d+)[, ]+(\d+)$", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Convert position name to coordinates.
        /// 
        /// Example:
        /// "1,3" -> Point(0,2)
        /// "2,5" -> Point(1,4)
        /// "2 5" -> Point(1,4)
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="format">Arbitrary format of the <see cref="Plate"/></param>
        /// <returns><see cref="Point"/> of given coordinates</returns>
        public virtual Point GetCoords(string name, Format format)
        {
            var point = new Point(0, 0);
            var matches = CoordsRegex.Matches(name.Trim());
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    var groups = match.Groups;
                    int x = 0, y = 0;
                    int.TryParse(groups[1].Value, out x);
                    int.TryParse(groups[2].Value, out y);

                    if (x > 0)
                        point.X = x < format.Height ? x - 1 : (format.Height - 1);
                    if (y > 0)
                        point.Y = y < format.Width ? y - 1 : (format.Width - 1);
                    break;
                }
            }

            return point;
        }

        /// <summary>
        /// Determines whether the specified name is valid position.
        /// </summary>
        /// <param name="name">The position name.</param>
        /// <param name="format">A current format.</param>
        /// <returns>
        ///   <c>true</c> if the specified name is valid position; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsValid(string name, Format format)
        {
            return CoordsRegex.IsMatch(name);
        }
    }
}
