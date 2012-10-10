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
    public class NumericCoords : IPositionNaming
    {
        /// <summary>
        /// Row name index starting with 1. Returns 1 for all negative integers.
        /// </summary>
        /// <param name="row">Internal row index starting with 0.</param>
        /// <returns>
        /// Row name string.
        /// </returns>
        public string GetRowName(int row)
        {
            return row <= 0 ? "1" : (row + 1).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Column name for given position naming.
        /// </summary>
        /// <param name="col">Internal column index starting with 0.</param>
        /// <returns>
        /// Column name string
        /// </returns>
        public string GetColName(int col)
        {
            return col <= 0 ? "1" : (col + 1).ToString(CultureInfo.InvariantCulture);
        }

        readonly Regex coords = new Regex(@"^(\d+)[, ]+(\d+)$", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Convert position name to coordinates.
        /// 
        /// Example:
        /// "1,3" -> Point(0,2)
        /// "2,5" -> Point(1,4)
        /// "2 5" -> Point(1,4)
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><see cref="Point"/> of given coordinates</returns>
        public Point GetCoords(string name)
        {
            var point = new Point(0, 0);
            var matches = coords.Matches(name.Trim());
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    var groups = match.Groups;
                    int x = 0, y = 0;
                    int.TryParse(groups[1].Value, out x);
                    int.TryParse(groups[2].Value, out y);

                    if (x > 0)
                        point.X = x - 1;
                    if (y > 0)
                        point.Y = y - 1;
                    break;
                }
            }

            return point;
        }
    }
}
