using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace Microplate
{
    public interface IData : ISerializable
    {
        string ToString();

        Image ToImage();

        Control ToControl();

        void FromString(string s);
    }
}
