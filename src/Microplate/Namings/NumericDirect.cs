using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Microplate.Namings
{
    /// <summary>
    /// This naming is the same as <see cref="NumericCoords"/> except for <see>
    ///                                                                     <cref>GetCoords</cref>
    ///                                                                   </see>.
    /// </summary>
    [Serializable]
    public class NumericDirect : NumericCoords
    {
        /// <summary>
        /// Convert position name to coordinates.
        /// 
        /// Example:
        /// format(8, 12)
        /// "13" -> Point(1,1)
        /// "52" -> Point(3,3)
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="format">Arbitrary format of the <see cref="Plate"/></param>
        /// <returns><see cref="Point"/> of given coordinates</returns>
        public override Point GetCoords(string name, Format format)
        {
            int pos;
            return int.TryParse(name.Trim(), out pos) ? GetCoords(pos, format) : new Point(0, 0);
        }

        /// <summary>
        /// Convert position name to coordinates.
        /// 
        /// Example:
        /// format(8, 12)
        /// "12" -> Point(0,11)
        /// "13" -> Point(1,0)
        /// "52" -> Point(4,3)
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="format">Arbitrary format of the <see cref="Plate"/></param>
        /// <returns><see cref="Point"/> of given coordinates</returns>
        public Point GetCoords(int pos, Format format)
        {
            if (pos > 0)
            {
                var point = new Point();
                if (pos >= format.Width * format.Height)
                {
                    point.X = format.Height - 1;
                    point.Y = format.Width - 1;
                }
                else
                {
                    point.X = pos / format.Width;
                    point.Y = pos % format.Width;
                    if (point.Y == 0)
                    {
                        point.Y = format.Width;
                        point.X = point.X > 0 ? point.X - 1 : point.X;
                    }

                    point.X = point.X >= format.Height ? (format.Height - 1) : point.X;
                    point.Y = point.Y >= format.Width ? (format.Width - 1) : (point.Y - 1);
                }

                return point;
            }

            return new Point(0, 0);
        }

        /// <summary>
        /// Determines whether the specified name is valid position.
        /// </summary>
        /// <param name="name">The position name.</param>
        /// <param name="format">A current format.</param>
        /// <returns>
        ///   <c>true</c> if the specified name is valid position; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(string name, Format format)
        {
            int pos;
            return int.TryParse(name, out pos) && pos <= format.Width * format.Height;
        }

        public bool IsValid(int pos, Format format)
        {
            return pos <= format.Width*format.Height;
        }
    }
}
