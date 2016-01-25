using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// A <see cref="Marker" /> handle.
    /// </summary>
    /// <remarks>
    /// This is an opaque type, meaning it can be used by a <see cref="Scintilla" /> control but
    /// otherwise has no public members of its own.
    /// </remarks>
    public struct MarkerHandle
    {
        internal IntPtr Value;

        /// <summary>
        /// A read-only field that represents an uninitialized handle.
        /// </summary>
        public static readonly MarkerHandle Zero;

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance or null.</param>
        /// <returns>true if <paramref name="obj" /> is an instance of <see cref="MarkerHandle" /> and equals the value of this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return (obj is IntPtr) && Value == ((MarkerHandle)obj).Value;
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
        /// Determines whether two specified instances of <see cref="MarkerHandle" /> are equal.
        /// </summary>
        /// <param name="a">The first handle to compare.</param>
        /// <param name="b">The second handle to compare.</param>
        /// <returns>true if <paramref name="a" /> equals <paramref name="b" />; otherwise, false.</returns>
        public static bool operator ==(MarkerHandle a, MarkerHandle b)
        {
            return a.Value == b.Value;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="MarkerHandle" /> are not equal.
        /// </summary>
        /// <param name="a">The first handle to compare.</param>
        /// <param name="b">The second handle to compare.</param>
        /// <returns>true if <paramref name="a" /> does not equal <paramref name="b" />; otherwise, false.</returns>
        public static bool operator !=(MarkerHandle a, MarkerHandle b)
        {
            return a.Value != b.Value;
        }
    }
}
