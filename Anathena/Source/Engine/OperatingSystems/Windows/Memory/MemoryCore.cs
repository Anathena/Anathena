using Anathena.Source.Engine.OperatingSystems.Windows.Native;
using Anathena.Source.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Memory
{
    /// <summary>
    /// Static core class providing tools for memory editing.
    /// </summary>
    public static class MemoryCore
    {
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
            // Allocate a memory page
            IntPtr Address = NativeMethods.VirtualAllocEx(ProcessHandle, IntPtr.Zero, Size, AllocationFlags, ProtectionFlags);

            return Address;
        }

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="Handle">A valid handle to an open object.</param>
        public static void CloseHandle(IntPtr Handle)
        {
            // Close the handle
            NativeMethods.CloseHandle(Handle);
        }

        /// <summary>
        /// Releases a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to a process.</param>
        /// <param name="Address">A pointer to the starting address of the region of memory to be freed.</param>
        public static void Free(SafeMemoryHandle ProcessHandle, IntPtr Address)
        {
            // Free the memory
            NativeMethods.VirtualFreeEx(ProcessHandle, Address, 0, MemoryReleaseFlags.Release);
        }

        #region NtQueryInformationProcess
        /// <summary>
        /// etrieves information about the specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process to query.</param>
        /// <returns>A <see cref="ProcessBasicInformation"/> structure containg process information.</returns>
        public static ProcessBasicInformation NtQueryInformationProcess(SafeMemoryHandle ProcessHandle)
        {
            // Create a structure to store process info
            ProcessBasicInformation ProcessInfo = new ProcessBasicInformation();

            // Get the process info
            Int32 Result = NativeMethods.NtQueryInformationProcess(ProcessHandle, ProcessInformationClass.ProcessBasicInformation, ref ProcessInfo, ProcessInfo.Size, IntPtr.Zero);

            return ProcessInfo;
        }

        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="AccessFlags">The access level to the process object.</param>
        /// <param name="Process">The identifier of the local process to be opened.</param>
        /// <returns>An open handle to the specified process.</returns>
        public static SafeMemoryHandle OpenProcess(ProcessAccessFlags AccessFlags, Process Process)
        {
            // Get an handle from the remote process
            SafeMemoryHandle Handle = NativeMethods.OpenProcess(AccessFlags, false, Process == null ? 0 : Process.Id);

            return Handle;
        }

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
            // Create the variable storing the old protection of the memory page
            MemoryProtectionFlags OldProtection;

            // Change the protection in the target process
            NativeMethods.VirtualProtectEx(ProcessHandle, Address, Size, protection, out OldProtection);

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
            Int32 QueryResult;
            return Query(ProcessHandle, BaseAddress, out QueryResult);
        }

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="BaseAddress">A pointer to the base address of the region of pages to be queried.</param>
        /// <returns>A <see cref="Native.MemoryBasicInformation64"/> structures in which information about the specified page range is returned.</returns>
        public static MemoryBasicInformation64 Query(SafeMemoryHandle ProcessHandle, IntPtr BaseAddress, out Int32 QueryResult)
        {
            // Allocate the structure to store information of memory
            MemoryBasicInformation64 MemoryInfo64 = new MemoryBasicInformation64();

            if (!Environment.Is64BitProcess)
            {
                // 32 Bit struct is not the same
                MemoryBasicInformation32 MemoryInfo32 = new MemoryBasicInformation32();

                // Query the memory region
                QueryResult = NativeMethods.VirtualQueryEx(ProcessHandle, BaseAddress, out MemoryInfo32, Marshal.SizeOf(MemoryInfo32));

                // Copy from the 32 bit struct to the 64 bit struct
                MemoryInfo64.AllocationBase = MemoryInfo32.AllocationBase;
                MemoryInfo64.AllocationProtect = MemoryInfo32.AllocationProtect;
                MemoryInfo64.BaseAddress = MemoryInfo32.BaseAddress;
                MemoryInfo64.Protect = MemoryInfo32.Protect;
                MemoryInfo64.RegionSize = MemoryInfo32.RegionSize;
                MemoryInfo64.State = MemoryInfo32.State;
                MemoryInfo64.Type = MemoryInfo32.Type;
            }
            else
            {
                // Query the memory region
                QueryResult = NativeMethods.VirtualQueryEx(ProcessHandle, BaseAddress, out MemoryInfo64, Marshal.SizeOf(MemoryInfo64));
            }

            return MemoryInfo64;
        }

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="StartAddress">A pointer to the starting address of the region of pages to be queried.</param>
        /// <param name="EndAddress">A pointer to the ending address of the region of pages to be queried.</param>
        /// <returns>A collection of <see cref="Native.MemoryBasicInformation64"/> structures.</returns>
        public static IEnumerable<MemoryBasicInformation64> Query(SafeMemoryHandle ProcessHandle, IntPtr StartAddress, IntPtr EndAddress,
            MemoryProtectionFlags RequiredProtection, MemoryProtectionFlags ExcludedProtection, MemoryTypeEnum AllowedTypes)
        {
            if (StartAddress.ToUInt64() >= EndAddress.ToUInt64())
                yield return new MemoryBasicInformation64();

            // Create the variable storing the result of the call of VirtualQueryEx
            Int32 QueryResult;
            Boolean WrappedAround = false;

            // Enumerate the memory pages
            do
            {
                // Allocate the structure to store information of memory
                MemoryBasicInformation64 MemoryInfo = Query(ProcessHandle, StartAddress, out QueryResult);

                // Increment the starting address with the size of the page
                IntPtr PreviousFrom = StartAddress;
                StartAddress = StartAddress.Add(MemoryInfo.RegionSize);

                if (PreviousFrom.ToUInt64() > StartAddress.ToUInt64())
                    WrappedAround = true;

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

                // Enforce allowed types
                switch (MemoryInfo.Type)
                {
                    case MemoryTypeFlags.None:
                        if ((AllowedTypes & MemoryTypeEnum.None) == 0)
                            continue;
                        break;
                    case MemoryTypeFlags.Private:
                        if ((AllowedTypes & MemoryTypeEnum.Private) == 0)
                            continue;
                        break;
                    case MemoryTypeFlags.Image:
                        if ((AllowedTypes & MemoryTypeEnum.Image) == 0)
                            continue;
                        break;
                    case MemoryTypeFlags.Mapped:
                        if ((AllowedTypes & MemoryTypeEnum.Mapped) == 0)
                            continue;
                        break;
                }

                // Ensure at least one required protection flag is set
                if (RequiredProtection != 0 && (MemoryInfo.Protect & RequiredProtection) == 0)
                    continue;

                // Ensure no ignored protection flags are set
                if (ExcludedProtection != 0 && (MemoryInfo.Protect & ExcludedProtection) != 0)
                    continue;

                // Return the memory page
                yield return MemoryInfo;

            } while (StartAddress.ToUInt64() < EndAddress.ToUInt64() && QueryResult != 0 && !WrappedAround);
        }

        #endregion

        #region Read

        /// <summary>
        /// Reads an array of bytes in the memory form the target process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process with memory that is being read.</param>
        /// <param name="Address">A pointer to the base address in the specified process from which to read.</param>
        /// <param name="Size">The number of bytes to be read from the specified process.</param>
        /// <returns>The collection of read bytes.</returns>
        public static Byte[] ReadBytes(SafeMemoryHandle ProcessHandle, IntPtr Address, Int32 Size, out Boolean Success)
        {
            // Allocate the buffer
            Byte[] Buffer = new Byte[Size];
            Int32 BytesRead;

            // Read the data from the target process
            Success = (NativeMethods.ReadProcessMemory(ProcessHandle, Address, Buffer, Size, out BytesRead) && Size == BytesRead);

            return Buffer;
        }

        #endregion

        #region Write

        /// <summary>
        /// Writes data to an area of memory in a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle to the process memory to be modified.</param>
        /// <param name="Address">A pointer to the base address in the specified process to which data is written.</param>
        /// <param name="ByteArray">A buffer that contains data to be written in the address space of the specified process.</param>
        /// <returns>The number of bytes written.</returns>
        public static Int32 WriteBytes(SafeMemoryHandle ProcessHandle, IntPtr Address, Byte[] ByteArray)
        {
            // Create the variable storing the number of bytes written
            Int32 BytesWritten;

            // Write the data to the target process
            if (NativeMethods.WriteProcessMemory(ProcessHandle, Address, ByteArray, ByteArray.Length, out BytesWritten))
            {
                // Check whether the length of the data written is equal to the inital array
                if (BytesWritten == ByteArray.Length)
                    return BytesWritten;
            }

            return 0;
        }

        #endregion

    } // End class

} // End namespace