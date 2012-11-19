using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Microplate
{
    public interface IPlateType : ISerializable
    {
        string Name
        {
            get;
            set;
        }

        Format Format
        {
            get;
            set;
        }

        string Manufacturer
        {
            get;
            set;
        }

        decimal Volume
        {
            get;
            set;
        }
    }
}
