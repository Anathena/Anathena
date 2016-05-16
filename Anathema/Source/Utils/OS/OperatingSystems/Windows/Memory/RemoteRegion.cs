using System;

namespace Anathema.MemoryManagement.Memory
{
    /// <summary>
    /// Represents a contiguous range of memory in a remote process.
    /// </summary>
    public class RemoteRegion : RemotePointer, IEquatable<RemoteRegion>
    {
        /// <summary>
        /// The span of the pointer in the target process
        /// </summary>
        public int RegionSize { get; set; }

        /// <summary>
        /// The span of the pointer in the target process
        /// </summary>
        public IntPtr EndAddress { get { return BaseAddress + RegionSize; }  set { this.RegionSize = (Int32)((UInt64)value - (UInt64)BaseAddress); } }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteRegion"/> class.
        /// </summary>
        /// <param name="MemorySharp">The reference of the <see cref="WindowsOSInterface"/> object.</param>
        /// <param name="BaseAddress">The base address of the memory region.</param>
        public RemoteRegion(WindowsOSInterface MemorySharp, IntPtr BaseAddress, Int32 RegionSize) : base(MemorySharp, BaseAddress)
        {
            this.RegionSize = RegionSize;
        }

        #endregion

        #region Methods

        #region Equals (override)
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override Boolean Equals(Object Object)
        {
            if (ReferenceEquals(null, Object)) return false;
            if (ReferenceEquals(this, Object)) return true;
            return Object.GetType() == GetType() && Equals((RemoteRegion)Object);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        public Boolean Equals(RemoteRegion Other)
        {
            if (ReferenceEquals(null, Other)) return false;
            return ReferenceEquals(this, Other) || (BaseAddress.Equals(Other.BaseAddress) && MemorySharp.Equals(Other.MemorySharp) &&
                                                    RegionSize.Equals(Other.RegionSize));
        }

        #endregion
        #region GetHashCode (override)
        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        public override Int32 GetHashCode()
        {
            return BaseAddress.GetHashCode() ^ MemorySharp.GetHashCode() ^ RegionSize.GetHashCode();
        }

        #endregion
        #region Operator (override)
        public static Boolean operator ==(RemoteRegion Left, RemoteRegion Right)
        {
            return Equals(Left, Right);
        }

        public static Boolean operator !=(RemoteRegion Left, RemoteRegion Right)
        {
            return !Equals(Left, Right);
        }

        #endregion
        #region Release
        /// <summary>
        /// Releases the memory used by the region.
        /// </summary>
        public void Release()
        {
            // Release the memory
            MemoryCore.Free(MemorySharp.Handle, BaseAddress);
            // Remove the pointer
            BaseAddress = IntPtr.Zero;
        }

        #endregion
        #region ToString (override)
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("BaseAddress = 0x{0:X} Size = 0x{1:X}", BaseAddress.ToInt64(), RegionSize);
        }

        #endregion
        #endregion

    } // End class

} // End namespace