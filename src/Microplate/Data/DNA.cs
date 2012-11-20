using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Microplate.Data
{
    [Serializable]
    public class DNA : IData
    {
        public Image ToImage()
        {
            throw new System.NotImplementedException();
        }

        public Control ToControl()
        {
            throw new System.NotImplementedException();
        }

        public void FromString(string s)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DNA"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> from which to create a new instance.</param>
        /// <param name="context">The source (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        public DNA(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
