using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microplate
{
    public class Plate : IEnumerable<IData>
    {
        private readonly IData[] content;

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

        public Plate(IPlateType type, Type dataType)
        {
            if (dataType.GetInterfaces().Length == 0 || dataType.GetInterfaces().All(x => x != typeof(IData)))
            {
                throw new ArgumentException("Must implement IData interface", "dataType");
            }

            if (type == null || !type.Format.IsValid())
            {
                throw new ArgumentException("Must contain valid plate format", "type");
            }

            content = new IData[type.Format.Width * type.Format.Height];

            Created = LastChanged = DateTime.Now;

            Type = type;
        }

        /// <summary>
        /// Indexed by coordinates.
        /// </summary>
        public IData this[int row, int col]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Indexed by alphanumeric position.
        /// </summary>
        public IData this[string pos]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Indexed by numeric position.
        /// </summary>
        public IData this[int pos]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Mixed indexing, by letter and number.
        /// </summary>
        public IData this[string row, int col]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
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
            return ((IEnumerable<IData>)content).GetEnumerator();
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
