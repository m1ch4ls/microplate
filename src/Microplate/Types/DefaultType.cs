using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microplate.Data;

namespace Microplate.Types
{
    [Serializable]
    public class DefaultType : IPlateType
    {
        public string Name { get; set; }
        public Format Format { get; set; }
        public string Manufacturer { get; set; }
        public decimal Volume { get; set; }

        public DefaultType()
        {
            // init at least the format
            Format = Formats.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultType"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> from which to create a new instance.</param>
        /// <param name="context">The source (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        public DefaultType(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            Format = (Format) info.GetValue("Format", typeof(Format));
            Manufacturer = info.GetString("Manufacturer");
            Volume = info.GetDecimal("Volume");
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Format", Format);
            info.AddValue("Manufacturer", Manufacturer);
            info.AddValue("Volume", Volume);
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Meta");

            writer.WriteElementString("Name", Name);
            Format.ToXml(writer);
            writer.WriteElementString("Manufacturer", Manufacturer);
            writer.WriteElementString("Volume", Volume.ToString(CultureInfo.InvariantCulture));

            writer.WriteEndElement();
        }

        public void FromXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "Name")
                        Name = reader.ReadElementContentAsString();
                    else if (reader.Name == "Manufacturer")
                        Manufacturer = reader.ReadElementContentAsString();
                    else if (reader.Name == "Volume")
                        Volume = reader.ReadElementContentAsDecimal();
                    else if (reader.Name == "Format")
                        Format.FromXml(reader);

                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Meta")
                {
                    reader.ReadEndElement();
                    break;
                }
            }
        }
    }
}
