using System;
using System.Collections.Generic;
using System.ComponentModel;
using Anathema.MemoryManagement.Helpers;
using Anathema.MemoryManagement.Internals;
using Anathema.MemoryManagement.Native;
using Anathema;

namespace Anathema.MemoryManagement.Memory
{
    /// <summary>
    /// Static core class providing tools for memory editing.
    /// </summary>
    public static class MemoryCore
    {
        #region Allocate
        /// <summary>
        /// Reserves a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="ProcessHandle">The handle to a process.</param>
        /// <param name="Size">The size of the region of memory to allocate, in bytes.</param>
        /// <param name="ProtectionFlags">The memory protection for the region of pages to be allocated.</param>
        /// <param name="AllocationFlags">The type of memory allocation.</param>
        /// <returns>The base address of the allocated region.</returns>
        public static IntPtr Allocate(SafeMemoryHandle ProcessHandle, Int32 Size, MemoryProtectionFlags ProtectionFlags = MemoryProtectionFlags.ExecuteReadWrite,
            MemoryAllocationFlags AllocationFlags = MemoryAllocationFlags.Commit)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(ProcessHandle, "processHandle");

            // Allocate a memory page
            IntPtr Address = NativeMethods.VirtualAllocEx(ProcessHandle, IntPtr.Zero, Size, AllocationFlags, ProtectionFlags);

            // Check whether the memory page is valid
            if (Address != IntPtr.Zero)
                return Address;

            // If the pointer isn't valid, throws an exception
            // throw new Win32Exception(String.Format("Couldn't allocate memory of {0} byte(s).", Size));
            return Address;
        }
        #endregion

        #region CloseHandle
        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="Handle">A valid handle to an open object.</param>
        public static void CloseHandle(IntPtr Handle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(Handle, "handle");

            // Close the handle
            if (!NativeMethods.CloseHandle(Handle))
            {
                // throw new Win32Exception(String.Format("Couldn't close he handle 0x{0}.", Handle));
            }
        }

        #endregion

        #region Free
        /// <summary>
        /// Releases a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to a process.</param>
        /// <param name="Address">A pointer to the starting address of the region of memory to be freed.</param>
        public static void Free(SafeMemoryHandle ProcessHandle, IntPtr Address)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(ProcessHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(Address, "address");

            // Free the memory
            if (!NativeMethods.VirtualFreeEx(ProcessHandle, Address, 0, MemoryReleaseFlags.Release))
            {
                // If the memory wasn't correctly freed, throws an exception
                // throw new Win32Exception(String.Format("The memory page 0x{0} cannot be freed.", Address.ToString("X")));
            }
        }

        #endregion

        #region NtQueryInformationProcess
        /// <summary>
        /// etrieves information about the specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process to query.</param>
        /// <returns>A <see cref="ProcessBasicInformation"/> structure containg process information.</returns>
        public static ProcessBasicInformation NtQueryInformationProcess(SafeMemoryHandle processHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(processHandle, "processHandle");

            // Create a structure to store process info
            ProcessBasicInformation ProcessInfo = new ProcessBasicInformation();

            // Get the process info
            Int32 Result = NativeMethods.NtQueryInformationProcess(processHandle, ProcessInformationClass.ProcessBasicInformation, ref ProcessInfo, ProcessInfo.Size, IntPtr.Zero);

            // If the function succeeded
            if (Result == 0)
                return ProcessInfo;

            // Else, couldn't get the process info, throws an exception
            // throw new ApplicationException(string.Format("Couldn't get the information from the process, error code '{0}'.", Result));
            return ProcessInfo;
        }

        #endregion

        #region OpenProcess
        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="AccessFlags">The access level to the process object.</param>
        /// <param name="ProcessId">The identifier of the local process to be opened.</param>
        /// <returns>An open handle to the specified process.</returns>
        public static SafeMemoryHandle OpenProcess(ProcessAccessFlags AccessFlags, Int32 ProcessId)
        {
            // Get an handle from the remote process
            SafeMemoryHandle Handle = NativeMethods.OpenProcess(AccessFlags, false, ProcessId);

            // Check whether the handle is valid
            if (!Handle.IsInvalid && !Handle.IsClosed)
                return Handle;

            // Else the handle isn't valid, throws an exception
            // throw new Win32Exception(String.Format("Couldn't open the process {0}.", ProcessId));
            return Handle;
        }

        #endregion

        #region ReadBytes
        /// <summary>
        /// Reads an array of bytes in the memory form the target process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process with memory that is being read.</param>
        /// <param name="Address">A pointer to the base address in the specified process from which to read.</param>
        /// <param name="Size">The number of bytes to be read from the specified process.</param>
        /// <returns>The collection of read bytes.</returns>
        public static byte[] ReadBytes(SafeMemoryHandle ProcessHandle, IntPtr Address, Int32 Size, out Boolean Success)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(ProcessHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(Address, "address");

            // Allocate the buffer
            Byte[] Buffer = new Byte[Size];
            Int32 BytesRead;

            // Read the data from the target process
            Success = (NativeMethods.ReadProcessMemory(ProcessHandle, Address, Buffer, Size, out BytesRead) && Size == BytesRead);
            if (Success)
                return Buffer;

            // Else the data couldn't be read, throws an exception
            // throw new Win32Exception(string.Format("Couldn't read {0} byte(s) from 0x{1}.", size, address.ToString("X")));
            return Buffer;
        }

        #endregion

        #region ChangeProtection
        /// <summary>
        /// Changes the protection on a region of committed pages in the virtual address space of a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process whose memory protection is to be changed.</param>
        /// <param name="Address">A pointer to the base address of the region of pages whose access protection attributes are to be changed.</param>
        /// <param name="Size">The size of the region whose access protection attributes are changed, in bytes.</param>
        /// <param name="protection">The memory protection option.</param>
        /// <returns>The old protection of the region in a <see cref="Native.MemoryBasicInformation32"/> structure.</returns>
        public static MemoryProtectionFlags ChangeProtection(SafeMemoryHandle ProcessHandle, IntPtr Address, int Size, MemoryProtectionFlags protection)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(ProcessHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(Address, "address");

            // Create the variable storing the old protection of the memory page
            MemoryProtectionFlags OldProtection;

            // Change the protection in the target process
            if (NativeMethods.VirtualProtectEx(ProcessHandle, Address, Size, protection, out OldProtection))
            {
                // Return the old protection
                return OldProtection;
            }

            // Else the protection couldn't be changed, throws an exception
            // throw new Win32Exception(string.Format("Couldn't change the protection of the memory at 0x{0} of {1} byte(s) to {2}.", address.ToString("X"), size, protection));
            return OldProtection;
        }

        #endregion

        #region Query
        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="BaseAddress">A pointer to the base address of the region of pages to be queried.</param>
        /// <returns>A <see cref="Native.MemoryBasicInformation64"/> structures in which information about the specified page range is returned.</returns>
        public static MemoryBasicInformation64 Query(SafeMemoryHandle ProcessHandle, IntPtr BaseAddress)
        {
            // Allocate the structure to store information of memory

            MemoryBasicInformation64 MemoryInfo64 = new MemoryBasicInformation64();

#if x86
            MemoryBasicInformation32 MemoryInfo32;
            // Query the memory region
            if(NativeMethods.VirtualQueryEx(processHandle, baseAddress, out MemoryInfo32, MarshalType<MemoryBasicInformation32>.Size) != 0)
            {
                // Copy from the 32 bit struct to the 64 bit struct
                MemoryInfo64.AllocationBase = MemoryInfo32.AllocationBase;
                MemoryInfo64.AllocationProtect = MemoryInfo32.AllocationProtect;
                MemoryInfo64.BaseAddress = MemoryInfo32.BaseAddress;
                MemoryInfo64.Protect = MemoryInfo32.Protect;
                MemoryInfo64.RegionSize = MemoryInfo32.RegionSize;
                MemoryInfo64.State = MemoryInfo32.State;
                MemoryInfo64.Type = MemoryInfo32.Type;

                return MemoryInfo64;
            }
#else
            // Query the memory region
            if (NativeMethods.VirtualQueryEx(ProcessHandle, BaseAddress, out MemoryInfo64, MarshalType<MemoryBasicInformation64>.Size) != 0)
            {
                return MemoryInfo64;
            }
#endif

            // Else the information couldn't be got
            throw new Win32Exception(string.Format("Couldn't query information about the memory region 0x{0}", BaseAddress.ToString("X")));
        }
        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="AddressFrom">A pointer to the starting address of the region of pages to be queried.</param>
        /// <param name="AddressTo">A pointer to the ending address of the region of pages to be queried.</param>
        /// <returns>A collection of <see cref="Native.MemoryBasicInformation64"/> structures.</returns>
        public static IEnumerable<MemoryBasicInformation64> Query(SafeMemoryHandle ProcessHandle, IntPtr AddressFrom, IntPtr AddressTo, Boolean IgnoreSettings = false)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(ProcessHandle, "processHandle");

            // Convert the addresses to UInt64
            UInt64 NumberFrom = (UInt64)AddressFrom.ToInt64();
            UInt64 NumberTo = (UInt64)AddressTo.ToInt64();

            // The first address must be lower than the second
            if (NumberFrom >= NumberTo)
                throw new ArgumentException("The starting address must be lower than the ending address.", "addressFrom");

            // Create the variable storing the result of the call of VirtualQueryEx
            Int32 Result;

            // Get settings of pages to require
            Array TypeEnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            Boolean[] RequiredTypeFlags = Settings.GetInstance().GetTypeSettings();
            MemoryProtectionFlags RequiredProtectionFlags = Settings.GetInstance().GetRequiredProtectionSettings();
            MemoryProtectionFlags IgnoredProtectionFlags = Settings.GetInstance().GetIgnoredProtectionSettings();

            // Get settings of pages to ignore

            // Enumerate the memory pages
            do
            {
                // Allocate the structure to store information of memory
                MemoryBasicInformation64 MemoryInfo;
#if x86
                // 32 Bit struct is not the same
                MemoryBasicInformation32 MemoryInfo32;

                // Get the next memory page
                Result = NativeMethods.VirtualQueryEx(ProcessHandle, new IntPtr(NumberFrom), out MemoryInfo, MarshalType<MemoryBasicInformation32>.Size);

                 // Copy from the 32 bit struct to the 64 bit struct
                MemoryInfo.AllocationBase = MemoryInfo32.AllocationBase;
                MemoryInfo.AllocationProtect = MemoryInfo32.AllocationProtect;
                MemoryInfo.BaseAddress = MemoryInfo32.BaseAddress;
                MemoryInfo.Protect = MemoryInfo32.Protect;
                MemoryInfo.RegionSize = MemoryInfo32.RegionSize;
                MemoryInfo.State = MemoryInfo32.State;
                MemoryInfo.Type = MemoryInfo32.Type;
#else

                // Query the memory region
                Result = NativeMethods.VirtualQueryEx(ProcessHandle, new IntPtr((Int64)NumberFrom), out MemoryInfo, MarshalType<MemoryBasicInformation64>.Size);
#endif          
                // Increment the starting address with the size of the page
                NumberFrom += (UInt64)MemoryInfo.RegionSize;

                // Ignore free memory. These are unallocated memory regions.
                if ((MemoryInfo.State & MemoryStateFlags.Free) != 0)
                    continue;

                // At least one readable memory flag is required
                if ((MemoryInfo.Protect & MemoryProtectionFlags.ReadOnly) == 0 && (MemoryInfo.Protect & MemoryProtectionFlags.ExecuteRead) == 0 &&
                    (MemoryInfo.Protect & MemoryProtectionFlags.ExecuteReadWrite) == 0 && (MemoryInfo.Protect & MemoryProtectionFlags.ReadWrite) == 0)
                    continue;

                // Do not bother with this shit, this memory is not worth scanning
                if ((MemoryInfo.Protect & MemoryProtectionFlags.ZeroAccess) != 0 || (MemoryInfo.Protect & MemoryProtectionFlags.NoAccess) != 0 || (MemoryInfo.Protect & MemoryProtectionFlags.Guard) != 0)
                    continue;

                if (!IgnoreSettings)
                {
                    // Enforce type constraints
                    if (RequiredTypeFlags[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.None)] == false)
                        continue;

                    // Ensure at least one required protection flag is set
                    if ((MemoryInfo.Protect & RequiredProtectionFlags) == 0)
                        continue;

                    // Ensure no ignored protection flags are set
                    if ((MemoryInfo.Protect & IgnoredProtectionFlags) != 0)
                        continue;
                }

                // Return the memory page
                yield return MemoryInfo;

            } while (NumberFrom < NumberTo && Result != 0);
        }

        #endregion

        #region WriteBytes
        /// <summary>
        /// Writes data to an area of memory in a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process memory to be modified.</param>
        /// <param name="Address">A pointer to the base address in the specified process to which data is written.</param>
        /// <param name="ByteArray">A buffer that contains data to be written in the address space of the specified process.</param>
        /// <returns>The number of bytes written.</returns>
        public static Int32 WriteBytes(SafeMemoryHandle ProcessHandle, IntPtr Address, Byte[] ByteArray)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(ProcessHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(Address, "address");

            // Create the variable storing the number of bytes written
            Int32 BytesWritten;

            // Write the data to the target process
            if (NativeMethods.WriteProcessMemory(ProcessHandle, Address, ByteArray, ByteArray.Length, out BytesWritten))
            {
                // Check whether the length of the data written is equal to the inital array
                if (BytesWritten == ByteArray.Length)
                    return BytesWritten;
            }

            // Else the data couldn't be written, throws an exception
            // throw new Win32Exception(string.Format("Couldn't write {0} bytes to 0x{1}", byteArray.Length, address.ToString("X")));
            return 0;
        }

        #endregion

    } // End class

} // End namespace