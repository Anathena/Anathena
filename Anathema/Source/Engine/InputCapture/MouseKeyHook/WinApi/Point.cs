using System;
using System.Runtime.InteropServices;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi
{
    /// <summary>
    /// The Point structure defines the X- and Y- coordinates of a point.
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/gdi/rectangl_0tiq.asp
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Point
    {
        /// <summary>
        /// Specifies the X-coordinate of the point.
        /// </summary>
        public Int32 X;

        /// <summary>
        /// Specifies the Y-coordinate of the point.
        /// </summary>
        public Int32 Y;

        public Point(Int32 X, Int32 Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Boolean operator ==(Point A, Point B)
        {
            return A.X == B.X && A.Y == B.Y;
        }

        public static Boolean operator !=(Point A, Point B)
        {
            return !(A == B);
        }

        public Boolean Equals(Point Other)
        {
            return Other.X == X && Other.Y == Y;
        }

        public override Boolean Equals(Object Object)
        {
            if (ReferenceEquals(null, Object))
                return false;

            if (Object.GetType() != typeof(Point))
                return false;

            return Equals((Point)Object);
        }

        public override Int32 GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

    } // End class

} // End namespace