using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// A <see cref="Scintilla" /> document.
    /// </summary>
    /// <remarks>
    /// This is an opaque type, meaning it can be used by a <see cref="Scintilla" /> control but
    /// otherwise has no public members of its own.
    /// </remarks>
    public struct Document
    {
        internal IntPtr Value;

        /// <summary>
        /// A read-only field that represents an uninitialized document.
        /// </summary>
        public static readonly Document Empty;

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance or null.</param>
        /// <returns>true if <paramref name="obj" /> is an instance of <see cref="Document" /> and equals the value of this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return (obj is IntPtr) && Value == ((Document)obj).Value;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Document" /> are equal.
        /// </summary>
        /// <param name="a">The first document to compare.</param>
        /// <param name="b">The second document to compare.</param>
        /// <returns>true if <paramref name="a" /> equals <paramref name="b" />; otherwise, false.</returns>
        public static bool operator ==(Document a, Document b)
        {
            return a.Value == b.Value;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Document" /> are not equal.
        /// </summary>
        /// <param name="a">The first document to compare.</param>
        /// <param name="b">The second document to compare.</param>
        /// <returns>true if <paramref name="a" /> does not equal <paramref name="b" />; otherwise, false.</returns>
        public static bool operator !=(Document a, Document b)
        {
            return a.Value != b.Value;
        }
    }
}
