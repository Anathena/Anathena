using Anathema.MemoryManagement.Native;
using System;

namespace Anathema.MemoryManagement.Memory
{
    /// <summary>
    /// Represents a virtual page in the remote process.
    /// </summary>
    public class RemoteVirtualPage : RemotePointer, IEquatable<RemoteVirtualPage>
    {
        /// <summary>
        /// Contains information about the memory.
        /// </summary>
        public MemoryBasicInformation64 Information
        {
            get { return MemoryCore.Query(MemorySharp.Handle, BaseAddress); }
        }

        /// <summary>
        /// Gets if the <see cref="RemoteVirtualPage"/> is valid.
        /// </summary>
        public override Boolean IsValid
        {
            get { return base.IsValid && Information.State != MemoryStateFlags.Free; }
        }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteVirtualPage"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="WindowsOSInterface"/> object.</param>
        /// <param name="baseAddress">The base address of the virtual page.</param>
        internal RemoteVirtualPage(WindowsOSInterface MemorySharp, IntPtr BaseAddress) : base(MemorySharp, BaseAddress)
        {

        }

        #endregion

        #region Methods
        #region ChangeProtection
        /// <summary>
        /// Changes the protection of the n next bytes in remote process.
        /// </summary>
        /// <param name="Protection">The new protection to apply.</param>
        /// <param name="MustBeDisposed">The resource will be automatically disposed when the finalizer collects the object.</param>
        /// <returns>A new instance of the <see cref="MemoryProtection"/> class.</returns>
        public MemoryProtection ChangeProtection(MemoryProtectionFlags Protection = MemoryProtectionFlags.ExecuteReadWrite, Boolean MustBeDisposed = true)
        {
            return new MemoryProtection(MemorySharp, BaseAddress, (Int32)Information.RegionSize, Protection, MustBeDisposed);
        }

        #endregion
        #region Equals (override)
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override Boolean Equals(Object obObject)
        {
            if (ReferenceEquals(null, obObject)) return false;
            if (ReferenceEquals(this, obObject)) return true;
            return obObject.GetType() == GetType() && Equals((RemoteVirtualPage)obObject);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        public Boolean Equals(RemoteVirtualPage Other)
        {
            if (ReferenceEquals(null, Other)) return false;
            return ReferenceEquals(this, Other) || (BaseAddress.Equals(Other.BaseAddress) && MemorySharp.Equals(Other.MemorySharp) &&
                                                    Information.RegionSize.Equals(Other.Information.RegionSize));
        }

        #endregion
        #region GetHashCode (override)
        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        public override int GetHashCode()
        {
            return BaseAddress.GetHashCode() ^ MemorySharp.GetHashCode() ^ Information.RegionSize.GetHashCode();
        }

        #endregion
        #region Operator (override)
        public static Boolean operator ==(RemoteVirtualPage Left, RemoteVirtualPage Right)
        {
            return Equals(Left, Right);
        }

        public static Boolean operator !=(RemoteVirtualPage Left, RemoteVirtualPage Right)
        {
            return !Equals(Left, Right);
        }

        #endregion
        #region Release
        /// <summary>
        /// Releases the memory used by the virtual page.
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
            return String.Format("BaseAddress = 0x{0:X} Size = 0x{1:X} Protection = {2}", BaseAddress.ToInt64(),
                                 Information.RegionSize, Information.Protect);
        }

        #endregion
        #endregion

    } // End class

} // End namespace