using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microplate.Data;

namespace Microplate
{
    [Serializable]
    public class Plate : ISerializable
    {
        public readonly IData[] Content;

        /// <summary>
        /// Proxy to <see cref="Format.Width"/>
        /// </summary>
        public int Width
        {
            get { return Type.Format.Width; }
        }

        /// <summary>
        /// Proxy to <see cref="Format.Height"/>
        /// </summary>
        public int Height
        {
            get { return Type.Format.Height; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Plate"/> is locked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if locked; otherwise, <c>false</c>.
        /// </value>
        public bool Locked { get; set; }

        private StateRecord currentState;
        /// <summary>
        /// Gets the current state of this <see cref="Plate"/>.
        /// </summary>
        /// <value>
        /// <see cref="StateRecord"/>
        /// </value>
        public StateRecord CurrentState
        {
            get { return currentState ?? (currentState = new StateRecord()); }
        }

        public IPlateType Type { get; private set; }

        public DateTime Created { get; set; }

        public DateTime LastChanged { get; set; }

        public readonly Type DataType;

        /// <summary>
        /// Initializes a new instance of the <see cref="Plate"/> class. Creates shallow copy of the plate.
        /// </summary>
        /// <param name="plate">The plate.</param>
        public Plate(Plate plate) : this(plate.Type, plate.DataType, plate.Content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plate"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="content">The content.</param>
        public Plate(IPlateType type, Type dataType, IData[] content = null)
        {
            if (dataType.GetInterfaces().Length == 0 || dataType.GetInterfaces().All(x => x != typeof(IData)))
            {
                throw new ArgumentException("Must implement IData interface", "dataType");
            }

            DataType = dataType;

            if (type == null || !type.Format.IsValid())
            {
                throw new ArgumentException("Must contain valid plate format", "type");
            }

            if (content == null)
            {
                Content = new IData[type.Format.Width * type.Format.Height];
                // fill in Content
                for (int i = 0; i < Content.Length; i++)
                {
                    Content[i] = (IData)Activator.CreateInstance(dataType);
                }
            }
            else
            {
                if (content.Length != type.Format.Width * type.Format.Height)
                    throw new ArgumentException("Content doesn't match provided format", "content");
                Content = content;
            }
            
            Created = LastChanged = DateTime.Now;

            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plate"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> from which to create a new instance.</param>
        /// <param name="context">The source (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        public Plate(SerializationInfo info, StreamingContext context)
        {
            Content = (IData[]) info.GetValue("Content", typeof (IData[]));
            Type = (IPlateType) info.GetValue("Type", typeof (IPlateType));
            Created = info.GetDateTime("Created");
            LastChanged = info.GetDateTime("LastChanged");
            DataType = (Type) info.GetValue("DataType", typeof(Type));
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Content", Content);
            info.AddValue("Type", Type);
            info.AddValue("Created", Created);
            info.AddValue("LastChanged", LastChanged);
            info.AddValue("DataType", DataType);
        }

        /// <summary>
        /// Indexed by coordinates.
        /// </summary>
        public IData this[int row, int col]
        {
            get { return Content[row*Width + col]; }
            set { Content[row*Width + col] = value; }
        }

        /// <summary>
        /// Indexed by alphanumeric position.
        /// </summary>
        public IData this[string pos]
        {
            get
            {
                var naming = PositionNamings.AlphaNumericDirect;
                if (naming.IsValid(pos, Type.Format))
                {
                    var point = naming.GetCoords(pos, Type.Format);
                    return this[point.X, point.Y];                    
                }
                throw new InvalidPosition(pos);
            }
            set
            {
                var naming = PositionNamings.AlphaNumericDirect;
                if (naming.IsValid(pos, Type.Format))
                {
                    var point = naming.GetCoords(pos, Type.Format);
                    this[point.X, point.Y] = value;
                }              
            }
        }

        /// <summary>
        /// Indexed by numeric position.
        /// </summary>
        public IData this[int pos]
        {
            get
            {
                var naming = PositionNamings.NumericDirect;
                if (naming.IsValid(pos, Type.Format))
                {
                    var point = naming.GetCoords(pos, Type.Format);
                    return this[point.X, point.Y];
                }
                throw new InvalidPosition(pos);
            }
            set
            {
                var naming = PositionNamings.NumericDirect;
                if (naming.IsValid(pos, Type.Format))
                {
                    var point = naming.GetCoords(pos, Type.Format);
                    this[point.X, point.Y] = value;
                }
            }
        }

        /// <summary>
        /// Mixed indexing, by letter and number.
        /// </summary>
        public IData this[string row, int col]
        {
            get { return this[row + col.ToString(CultureInfo.InvariantCulture)]; }
            set { this[row + col.ToString(CultureInfo.InvariantCulture)] = value; }
        }

        public static Plate<T> ToPlate<T>(Plate plate) where T : IData
        {
            if (typeof(T) == plate.DataType)
            {
                return new Plate<T>(plate);
            }

            throw new ArgumentException("Plate DataType provided doesn't match plate DataType required", "plate");
        }

        public static Plate FromFile(string filename)
        {
            var file = File.OpenRead(filename);
            var plate = FromStream(file);
            file.Close();

            return plate;
        }

        public static Plate FromStream(Stream stream)
        {
            var reader = new XmlTextReader(stream);
            Type dataType = null;
            IPlateType plateType = null;
            DateTime created = DateTime.Today;
            DateTime lastChanged = DateTime.Now;
            var items = new List<string>();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "Plate")
                    {
                        var typeName = reader.GetAttribute("DataType");
                        if (typeName != null) dataType = System.Type.GetType(typeName);
                    }
                    else if (reader.Name == "Type")
                    {
                        var typeName = reader.GetAttribute("FullName");
                        if (typeName != null)
                        {
                            var typeType = System.Type.GetType(typeName);
                            if (typeType != null) plateType = (IPlateType)Activator.CreateInstance(typeType);
                        }

                        if (plateType != null)
                        {
                            reader.Read();
                            plateType.FromXml(reader);
                        }
                    }
                    else if (reader.Name == "Content")
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Item")
                            {
                                items.Add(reader.ReadElementContentAsString());
                            } else if (reader.NodeType == XmlNodeType.EndElement)
                            {
                                reader.ReadEndElement();
                                break;
                            }
                        }
                    } else if (reader.Name == "Created")
                    {
                        created = reader.ReadElementContentAsDateTime();
                    } else if (reader.Name == "LastChanged")
                    {
                        lastChanged = reader.ReadElementContentAsDateTime();
                    }
                } else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    if (reader.Name == "Plate")
                    {
                        reader.ReadEndElement();
                        break;
                    }
                }
            }

            if (dataType != null && plateType != null)
            {
                var plate = new Plate(plateType, dataType);
                for (int i = 0; i < plate.Content.Length; i++)
                {
                    plate.Content[i].FromString(items[i]);
                }

                plate.Created = created;
                plate.LastChanged = lastChanged;

                return plate;
            }

            return null;
        }

        public void Save(Stream stream)
        {
            var writer = new XmlTextWriter(stream, Encoding.UTF8) { Formatting = Formatting.Indented };
            writer.WriteStartDocument();
            writer.WriteStartElement("Plate");

            writer.WriteStartAttribute("DataType");
            writer.WriteString(DataType.FullName);
            writer.WriteEndAttribute();

            writer.WriteStartElement("Content");
            foreach (var data in Content)
            {
                writer.WriteStartElement("Item");
                writer.WriteString(data.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Type");
            writer.WriteAttributeString("FullName", Type.GetType().FullName);
            Type.ToXml(writer);
            writer.WriteEndElement();

            writer.WriteElementString("Created", Created.ToString("o"));
            writer.WriteElementString("LastChanged", LastChanged.ToString("o"));
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        public void Save(string filename)
        {
            var file = File.Open(filename, FileMode.Create, FileAccess.Write);
            Save(file);
            file.Close();
        }
    }

    [Serializable]
    public class Plate<T> : Plate where T : IData
    {
        public Plate(IPlateType type) : base(type, typeof(T))
        {
        }

        public Plate(Plate plate) : base(plate.Type, typeof(T), plate.Content)
        {
        }

        public Plate(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Indexed by coordinates.
        /// </summary>
        public new T this[int row, int col]
        {
            get { return (T)base[row, col]; }
            set { base[row, col] = value; }
        }

        /// <summary>
        /// Indexed by alphanumeric position.
        /// </summary>
        public new T this[string pos]
        {
            get { return (T) base[pos]; }
            set { base[pos] = value; }
        }

        /// <summary>
        /// Indexed by numeric position.
        /// </summary>
        public new T this[int pos]
        {
            get { return (T)base[pos]; }
            set { base[pos] = value; }
        }

        /// <summary>
        /// Mixed indexing, by letter and number.
        /// </summary>
        public new T this[string row, int col]
        {
            get { return (T) base[row, col]; }
            set { base[row, col] = value; }
        }
    }
}
