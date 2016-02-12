using System;

namespace Anathema
{
    /// <summary>
    /// Specifies the interface requir
    /// </summary>
    public class NormalizedRegion
    {
        public IntPtr BaseAddress;
        public Int32 RegionSize;

        public IntPtr EndAddress { get { return BaseAddress.Add((IntPtr)RegionSize); } set { this.RegionSize = (Int32)value.Subtract(BaseAddress); } }

        public NormalizedRegion(IntPtr BaseAddress, Int32 RegionSize)
        {
            this.BaseAddress = BaseAddress;
            this.RegionSize = RegionSize;
        }

    } // End interface

} // End namespace