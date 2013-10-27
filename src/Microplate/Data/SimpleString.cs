﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace Microplate.Data
{
    [Serializable]
    public class SimpleString : IData
    {
        public String Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleString"/> class.
        /// </summary>
        public SimpleString()
        {
            Value = String.Empty;
        }

        public override string ToString()
        {
            return Value;
        }

        public void FromString(string s)
        {
            Value = s;
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleString"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> from which to create a new instance.</param>
        /// <param name="context">The source (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        public SimpleString(SerializationInfo info, StreamingContext context)
        {
            Value = (string) info.GetValue("Value", typeof (string));
        }
    }
}