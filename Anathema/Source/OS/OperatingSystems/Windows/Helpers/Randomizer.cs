using System;
using System.Text;

namespace Anathema.Source.OS.OperatingSystems.Windows.Helpers
{
    /// <summary>
    /// Static helper class providing tools for generating random numbers or strings.
    /// </summary>
    public static class Randomizer
    {
        /// <summary>
        /// Provides random engine.
        /// </summary>
        private static readonly Random Random = new Random();
        /// <summary>
        /// Allowed characters in random strings.
        /// </summary>
        private static readonly Char[] AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

        #region GenerateNumber
        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="MinValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="MaxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue.</returns>
        public static Int32 GenerateNumber(Int32 MinValue, Int32 MaxValue)
        {
            return Random.Next(MinValue, MaxValue);
        }

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="MaxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero.</param>
        /// <returns>A 32-bit signed integer greater than or equal to zero, and less than maxValue.</returns>
        public static Int32 GenerateNumber(Int32 MaxValue)
        {
            return Random.Next(MaxValue);
        }

        /// <summary>
        /// Returns a nonnegative random number.
        /// </summary>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than <see cref="Int32.MaxValue"/>.</returns>
        public static int GenerateNumber()
        {
            return Random.Next();
        }

        #endregion

        #region GenerateString
        /// <summary>
        /// Returns a random string where its size is within a specified range.
        /// </summary>
        /// <param name="MinSize">The inclusive lower bound of the size of the string returned.</param>
        /// <param name="MaxSize">The exclusive upper bound of the size of the string returned.</param>
        /// <returns></returns>
        public static string GenerateString(Int32 MinSize = 40, Int32 MaxSize = 40)
        {
            // Create the string builder with a specific capacity
            StringBuilder Builder = new StringBuilder(GenerateNumber(MinSize, MaxSize));

            // Fill the string builder
            for (Int32 Index = 0; Index < Builder.Capacity; Index++)
            {
                Builder.Append(AllowedChars[GenerateNumber(AllowedChars.Length - 1)]);
            }

            return Builder.ToString();
        }

        #endregion

        #region GenerateGuid
        /// <summary>
        /// Initializes a new instance of the <see cref="Guid"/> structure.
        /// </summary>
        /// <returns>A new <see cref="Guid"/> object.</returns>
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        #endregion

    } // End class

} // End namespace