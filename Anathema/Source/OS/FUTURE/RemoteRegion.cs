using System;

namespace Anathema
{
    /// <summary>
    /// Specifies the interface requir
    /// </summary>
    public class FUTURERemoteRegion
    {
        public IntPtr Base;
        public IntPtr Size;

        public IntPtr EndAddress { get { return Base.Add(Size); } set { this.Size = value.Subtract(Base); } }


    } // End interface

} // End namespace