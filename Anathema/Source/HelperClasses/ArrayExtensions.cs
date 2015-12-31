using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    static class ArrayExtensions
    {
        /// <summary>
        /// Extention method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ArrayA"></param>
        /// <param name="Index"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] ArrayA, Int32 Index, Int32 Length)
        {
            if (ArrayA.Length - Index < Length)
                return null;

            T[] Result = new T[Length];
            Array.Copy(ArrayA, Index, Result, 0, Length);
            return Result;
        }

        public static T[] Concat<T>(this T[] ArrayA, T[] ArrayB)
        {
            if (ArrayA == null && ArrayB == null) return null;
            if (ArrayA == null) return ArrayB;
            if (ArrayB == null) return ArrayA;

            int OldLength = ArrayA.Length;
            Array.Resize<T>(ref ArrayA, ArrayA.Length + ArrayB.Length);
            Array.Copy(ArrayB, 0, ArrayA, OldLength, ArrayB.Length);
            return ArrayA;
        }
    }
}
