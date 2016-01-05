/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binarysharp.MemoryManagement.Memory
{
    /// <summary>
    /// Represents a contiguous range of memory in a remote process.
    /// </summary>
    public class RemoteRegion : RemotePointer, IEquatable<RemoteRegion>
    {
        #region Properties
        /// <summary>
        /// The span of the pointer in the target process
        /// </summary>
        public int RegionSize { get; set; }

        /// <summary>
        /// The span of the pointer in the target process
        /// </summary>
        public IntPtr EndAddress { get { return BaseAddress + RegionSize; }  set { this.RegionSize = (Int32)((UInt64)value - (UInt64)BaseAddress); } }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteRegion"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="baseAddress">The base address of the memory region.</param>
        public RemoteRegion(MemorySharp memorySharp, IntPtr baseAddress, int RegionSize) : base(memorySharp, baseAddress)
        {
            this.RegionSize = RegionSize;
        }
        #endregion


        #region Methods

        #region GetRegionsOfVirtualPages

        #endregion

        #region Equals (override)
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((RemoteRegion)obj);
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        public bool Equals(RemoteRegion other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || (BaseAddress.Equals(other.BaseAddress) && MemorySharp.Equals(other.MemorySharp) &&
                                                    RegionSize.Equals(other.RegionSize));
        }
        #endregion
        #region GetHashCode (override)
        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        public override int GetHashCode()
        {
            return BaseAddress.GetHashCode() ^ MemorySharp.GetHashCode() ^ RegionSize.GetHashCode();
        }
        #endregion
        #region Operator (override)
        public static bool operator ==(RemoteRegion left, RemoteRegion right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RemoteRegion left, RemoteRegion right)
        {
            return !Equals(left, right);
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
        public override string ToString()
        {
            return string.Format("BaseAddress = 0x{0:X} Size = 0x{1:X}", BaseAddress.ToInt64(), RegionSize);
        }
        #endregion
        #endregion
    }
}
