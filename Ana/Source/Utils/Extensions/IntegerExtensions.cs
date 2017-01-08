namespace Ana.Source.Utils.Extensions
{
    using System;

    /// <summary>
    /// Contains extension methods for integers.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Converts the given integer to a <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="integer">The integer to convert.</param>
        /// <returns>The integer converted to a <see cref="IntPtr"/>.</returns>
        public static IntPtr ToIntPtr(this Int64 integer)
        {
            return unchecked((IntPtr)integer);
        }

        /// <summary>
        /// Converts the given integer to a <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="integer">The integer to convert.</param>
        /// <returns>The integer converted to a <see cref="IntPtr"/>.</returns>
        public static UIntPtr ToUIntPtr(this Int64 integer)
        {
            return unchecked((UIntPtr)(UInt64)integer);
        }

        /// <summary>
        /// Converts the given integer to a <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="integer">The integer to convert.</param>
        /// <returns>The integer converted to a <see cref="IntPtr"/>.</returns>
        public static IntPtr ToIntPtr(this UInt64 integer)
        {
            return unchecked((IntPtr)(Int64)integer);
        }

        /// <summary>
        /// Converts the given integer to a <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="integer">The integer to convert.</param>
        /// <returns>The integer converted to a <see cref="IntPtr"/>.</returns>
        public static UIntPtr ToUIntPtr(this UInt64 integer)
        {
            return unchecked((UIntPtr)integer);
        }

        /// <summary>
        /// Converts the given integer to a <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="integer">The integer to convert.</param>
        /// <returns>The integer converted to a <see cref="IntPtr"/>.</returns>
        public static IntPtr ToIntPtr(this Int32 integer)
        {
            return unchecked((IntPtr)integer);
        }

        /// <summary>
        /// Converts the given integer to a <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="integer">The integer to convert.</param>
        /// <returns>The integer converted to a <see cref="IntPtr"/>.</returns>
        public static UIntPtr ToUIntPtr(this Int32 integer)
        {
            return unchecked((UIntPtr)(UInt64)integer);
        }

        /// <summary>
        /// Converts the given integer to a <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="integer">The integer to convert.</param>
        /// <returns>The integer converted to a <see cref="IntPtr"/>.</returns>
        public static IntPtr ToIntPtr(this UInt32 integer)
        {
            return unchecked((IntPtr)(Int64)integer);
        }

        /// <summary>
        /// Converts the given integer to a <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="integer">The integer to convert.</param>
        /// <returns>The integer converted to a <see cref="IntPtr"/>.</returns>
        public static UIntPtr ToUIntPtr(this UInt32 integer)
        {
            return unchecked((UIntPtr)integer);
        }
    }
    //// End class
}
//// End namespace