using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Microplate
{
    public interface IPlateFormat
    {
        int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Default position naming for this format.
        /// </summary>
        Microplate.IPositionNaming PositionNaming
        {
            get;
            set;
        }

        int Width
        {
            get;
            set;
        }
    }
}
