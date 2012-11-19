using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microplate.Namings
{
    [Serializable]
    public class AlphaNumericCoords : IPositionNaming
    {
        private const string LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int LETTERS_LENGTH = 26;

        /// <summary>
        /// Row name index starting with A. Returns A for all negative integers.
        /// </summary>
        /// <param name="row">Internal row index starting with 0.</param>
        /// <param name="format">Arbitrary format of the <see cref="Plate"/></param>
        /// <returns>
        /// Row name string.
        /// </returns>
        public virtual string GetRowName(int row, Format format)
        {
            if (row <= 0) return "A";
            if (row >= format.Height) row = format.Height - 1;
            else row += 1;

            var sb = String.Empty;
            while (row > 0)
            {
                var index = (row-1) % LETTERS_LENGTH;
                sb = (LETTERS[index]) + sb;
                row = (row - index) / LETTERS_LENGTH;
            }

            return sb;
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

        static readonly Regex CoordsRegex = new Regex(@"^([a-zA-Z]+)[, ]*(\d+)$", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Convert position name to coordinates.
        /// 
        /// Example:
        /// "A,3" -> Point(0,2)
        /// "B,5" -> Point(1,4)
        /// "B 5" -> Point(1,4)
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="format">Arbitrary format of the <see cref="Plate"/></param>
        /// <returns><see cref="Point"/> of given coordinates</returns>
        public virtual Point GetCoords(string name, Format format)
        {
            var point = new Point(0, 0);

            var matches = CoordsRegex.Matches(name.Trim().ToUpper());
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    var groups = match.Groups;
                    var x = groups[1].Value;
                    int c = x.Length;
                    while (c > 0)
                    {
                        c--;
                        point.X += LETTERS.IndexOf(x[c]) + (LETTERS_LENGTH * c);
                    }

                    if (point.X > 0)
                        point.X = point.X < format.Height ? point.X : (format.Height - 1);

                    int y;
                    int.TryParse(groups[2].Value, out y);
                    
                    if (y > 0)
                        point.Y = y < format.Width ? y - 1 : (format.Width - 1);
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
        public bool IsValid(string name, Format format)
        {
            return CoordsRegex.IsMatch(name);
        }
    }
}
