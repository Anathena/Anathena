using System;

namespace Ana.Source.Utils.Extensions
{
    /// <summary>
    /// Extension methods for arrays
    /// </summary>
    static class ArrayExtensions
    {
        /// <summary>
        /// Returns the specified subarray. Returns null if the specified index is out of bounds.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrayA"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] arrayA, Int32 index, Int32 length)
        {
            if (arrayA == null || arrayA.Length - index < length)
            {
                return null;
            }

            T[] result = new T[length];
            Array.Copy(arrayA, index, result, 0, length);

            return result;
        }

        /// <summary>
        /// Returns the specified subarray. Attempts to return a smaller subarray if the specified length is out of bounds.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrayA"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] LargestSubArray<T>(this T[] arrayA, Int32 index, Int32 length)
        {
            if (arrayA == null)
            {
                return null;
            }

            if (arrayA.Length - index < length)
            {
                length = arrayA.Length - index;
            }

            if (length == 0)
            {
                return null;
            }

            T[] result = new T[length];
            Array.Copy(arrayA, index, result, 0, length);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrayA"></param>
        /// <param name="arrayB"></param>
        /// <returns></returns>
        public static T[] Concat<T>(this T[] arrayA, T[] arrayB)
        {
            if (arrayA == null && arrayB == null)
            {
                return null;
            }

            if (arrayA == null)
            {
                return arrayB;
            }

            if (arrayB == null)
            {
                return arrayA;
            }

            Int32 oldLength = arrayA.Length;
            Array.Resize<T>(ref arrayA, arrayA.Length + arrayB.Length);
            Array.Copy(arrayB, 0, arrayA, oldLength, arrayB.Length);

            return arrayA;
        }
    }
    //// End class
}
//// End namespace