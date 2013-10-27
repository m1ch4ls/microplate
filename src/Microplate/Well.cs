using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Microplate
{
    [Serializable]
    public class Well : ISerializable
    {
        public String Value = String.Empty;

        public int Row;
        public int Col;

        public Well(String value, int row, int col)
            : this(row, col)
        {
            Value = value;
        }

        public Well(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> from which to create a new instance.</param>
        /// <param name="context">The source (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        public Well(SerializationInfo info, StreamingContext context)
        {
            Value = info.GetString("Value");
            Row = info.GetInt32("Row");
            Col = info.GetInt32("Col");
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">
        /// The caller does not have the required permission.
        ///   </exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Well", Value);
            info.AddValue("Row", Row);
            info.AddValue("Col", Col);
        }
    }
}
