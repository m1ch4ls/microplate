using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microplate
{
    public interface IData
    {
        string ToString();

        Image ToImage();

        Control ToControl();
    }
}
