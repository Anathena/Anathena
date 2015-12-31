using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    static class SubArrayHelper
    {
        /// <summary>
        /// Extention method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="Index"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] Data, Int32 Index, Int32 Length)
        {
            if (Data.Length - Index < Length)
                return null;

            T[] Result = new T[Length];
            Array.Copy(Data, Index, Result, 0, Length);
            return Result;
        }
    }
}
