using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microplate.Data;

namespace Microplate
{
    public class Plate : IEnumerable<IData>
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

            if (content == null) Content = new IData[type.Format.Width * type.Format.Height];
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

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<IData> GetEnumerator()
        {
            return ((IEnumerable<IData>)Content).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Plate<T> ToPlate<T>(Plate plate) where T : IData
        {
            if (typeof(T) == plate.DataType)
            {
                return new Plate<T>(plate);
            }

            throw new ArgumentException("Plate DataType provided doesn't match plate DataType required", "plate");
        }
    }

    public class Plate<T> : Plate, IEnumerable<T> where T : IData
    {
        public Plate(IPlateType type) : base(type, typeof(T))
        {
        }

        public Plate(Plate plate) : base(plate.Type, typeof(T), plate.Content)
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

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public new IEnumerator<T> GetEnumerator()
        {
            return Content.Cast<T>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
