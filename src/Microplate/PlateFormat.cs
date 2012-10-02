using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Microplate
{
    /// <summary>
    /// Container for plate format parameters. Used in <see cref="PlateFormat"/>.
    /// </summary>
    public class PlateFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlateFormat"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="defaultNaming">The default position naming naming.</param>
        public PlateFormat(int width, int height, IPositionNaming defaultNaming)
        {
            this.Width = width;
            this.Height = height;
            this.PositionNaming = defaultNaming;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlateFormat"/> class.
        /// </summary>
        public PlateFormat() { }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Default position naming for this format.
        /// </summary>
        public IPositionNaming PositionNaming
        {
            get;
            set;
        }
    }
}
