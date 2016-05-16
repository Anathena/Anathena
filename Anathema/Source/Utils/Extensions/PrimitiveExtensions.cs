using System;

namespace Anathema.Utils.Extensions
{
    public static class PrimitiveExtensions
    {
        /// <summary>
        /// Bounds the value between the min and the maximum values provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Val"></param>
        /// <param name="Min"></param>
        /// <param name="Max"></param>
        /// <returns></returns>
        public static T Clamp<T>(this T Val, T Min, T Max) where T : IComparable<T>
        {
            if (Val.CompareTo(Min) < 0) return Min;
            else if (Val.CompareTo(Max) > 0) return Max;
            else return Val;
        }

    } // End calss

} // End namespace