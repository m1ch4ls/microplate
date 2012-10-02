using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Microplate
{
    public interface IContent
    {
        string ToString();

        Image ToImage();
    }
}
