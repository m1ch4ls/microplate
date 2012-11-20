using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Microplate
{
    /// <summary>
    /// Container for plate format parameters. Used in <see cref="Format"/>.
    /// </summary>
    [Serializable]
    public struct Format
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> struct.
        /// 
        /// Note: Number of rows <see cref="Height"/> is the first parameter.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="defaultNaming">The default position naming naming.</param>
        /// <param name="name">Optional name of the format</param>
        public Format(int height, int width, IPositionNaming defaultNaming, string name = "")
        {
            Height = height;
            Width = width;
            PositionNaming = defaultNaming;
            Name = name;
        }

        /// <summary>
        /// Width of the plate using this format.
        /// </summary>
        public int Width;


        /// <summary>
        /// Height of the plate using this format.
        /// </summary>
        public int Height;

        /// <summary>
        /// Default position naming for this format.
        /// </summary>
        public IPositionNaming PositionNaming;

        /// <summary>
        /// Optional name of the format
        /// </summary>
        public string Name;

        public bool IsValid()
        {
            return Height > 0 && Width > 0;
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Format");
            writer.WriteAttributeString("Width", Width.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("Height", Height.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("PositionNaming", Array.IndexOf(PositionNamings.Defined, PositionNaming).ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();
        }

        public void FromXml(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                if (reader.Name == "Width")
                    Width = reader.ReadContentAsInt();
                else if (reader.Name == "Height")
                    Height = reader.ReadContentAsInt();
                else if (reader.Name == "Name")
                    Name = reader.ReadContentAsString();
                else if (reader.Name == "PositionNaming")
                {
                    var index = reader.ReadContentAsInt();
                    if (index < PositionNamings.Defined.Length) PositionNaming = PositionNamings.Defined[index];
                }
            }
            reader.MoveToElement();
        }
    }
}
