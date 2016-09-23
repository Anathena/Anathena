namespace Ana.Source.Engine.OperatingSystems.Windows
{
    using Native;
    using Processes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Utils.Extensions;

    /// <summary>
    /// Static class providing tools for windows memory editing internals
    /// </summary>
    internal static class Memory
    {
        #region Read

        /// <summary>
        /// Reads an array of bytes in the memory form the target process
        /// </summary>
        /// <param name="processHandle">A handle to the process with memory that is being read</param>
        /// <param name="address">A pointer to the base address in the specified process from which to read</param>
        /// <param name="size">The number of bytes to be read from the specified process</param>
        /// <param name="success">Whether or not the read operation succeeded</param>
        /// <returns>The bytes read from the target, can be null or empty on failure</returns>
        public static Byte[] ReadBytes(IntPtr processHandle, IntPtr address, Int32 size, out Boolean success)
        {
            // Allocate the buffer
            Byte[] buffer = new Byte[size];
            Int32 bytesRead;

            // Read the data from the target process
            success = NativeMethods.ReadProcessMemory(processHandle, address, buffer, size, out bytesRead) && size == bytesRead;

            return buffer;
        }

        #endregion

        #region Write

        /// <summary>
        /// Writes bytes to memory in a specified process
        /// </summary>
        /// <param name="processHandle">A handle to the process memory to be modified</param>
        /// <param name="address">A pointer to the base address in the specified process to which data is written</param>
        /// <param name="byteArray">A buffer that contains data to be written in the address space of the specified process</param>
        /// <returns>The number of bytes written</returns>
        public static Int32 WriteBytes(IntPtr processHandle, IntPtr address, Byte[] byteArray)
        {
            // Create the variable storing the number of bytes written
            Int32 bytesWritten;

            // Write the data to the target process
            if (NativeMethods.WriteProcessMemory(processHandle, address, byteArray, byteArray.Length, out bytesWritten))
            {
                // Check whether the length of the data written is equal to the inital array
                if (bytesWritten == byteArray.Length)
                {
                    return bytesWritten;
                }
            }

            return 0;
        }

        #endregion

        /// <summary>
        /// Reserves a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">The handle to a process.</param>
        /// <param name="size">The size of the region of memory to allocate, in bytes.</param>
        /// <param name="protectionFlags">The memory protection for the region of pages to be allocated.</param>
        /// <param name="allocationFlags">The type of memory allocation.</param>
        /// <returns>The base address of the allocated region</returns>
        public static IntPtr Allocate(
            IntPtr processHandle,
            Int32 size,
            MemoryProtectionFlags protectionFlags = MemoryProtectionFlags.ExecuteReadWrite,
            MemoryAllocationFlags allocationFlags = MemoryAllocationFlags.Commit)
        {
            // Allocate a memory page
            return NativeMethods.VirtualAllocEx(processHandle, IntPtr.Zero, size, allocationFlags, protectionFlags);
        }

        /// <summary>
        /// Opens an existing local process object
        /// </summary>
        /// <param name="accessFlags">The access level to the process object</param>
        /// <param name="process">The identifier of the local process to be opened</param>
        /// <returns>An open handle to the specified process</returns>
        public static IntPtr OpenProcess(ProcessAccessFlags accessFlags, NormalizedProcess process)
        {
            return NativeMethods.OpenProcess(accessFlags, false, process == null ? 0 : process.processId);
        }

        /// <summary>
        /// Closes an open object handle
        /// </summary>
        /// <param name="handle">A valid handle to an open object</param>
        public static void CloseHandle(IntPtr handle)
        {
            // Close the handle
            NativeMethods.CloseHandle(handle);
        }

        /// <summary>
        /// Releases a region of memory within the virtual address space of a specified process
        /// </summary>
        /// <param name="processHandle">A handle to a process</param>
        /// <param name="address">A pointer to the starting address of the region of memory to be freed</param>
        public static void Free(IntPtr processHandle, IntPtr address)
        {
            // Free the memory
            NativeMethods.VirtualFreeEx(processHandle, address, 0, MemoryReleaseFlags.Release);
        }

        /// <summary>
        /// Changes the protection on a region of committed pages in the virtual address space of a specified process
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory protection is to be changed</param>
        /// <param name="address">A pointer to the base address of the region of pages whose access protection attributes are to be changed</param>
        /// <param name="size">The size of the region whose access protection attributes are changed, in bytes</param>
        /// <param name="protection">The memory protection option</param>
        /// <returns>The old protection of the region in a <see cref="MemoryBasicInformation32"/> structure</returns>
        public static MemoryProtectionFlags ChangeProtection(IntPtr processHandle, IntPtr address, Int32 size, MemoryProtectionFlags protection)
        {
            // Create the variable storing the old protection of the memory page
            MemoryProtectionFlags oldProtection;

            // Change the protection in the target process
            NativeMethods.VirtualProtectEx(processHandle, address, size, protection, out oldProtection);

            return oldProtection;
        }

        /// <summary>
        /// Gets regions of memory allocated in the remote process based on provided parameters
        /// </summary>
        /// <param name="handle">Target process handle</param>
        /// <param name="startAddress">The start address of the query range</param>
        /// <param name="endAddress">The end address of the query range</param>
        /// <param name="requiredProtection">Protection flags required to be present</param>
        /// <param name="excludedProtection">Protection flags that must not be present</param>
        /// <param name="allowedTypes">Memory types that can be present</param>
        /// <returns>A collection of pointers to virtual pages in the target process</returns>
        public static IEnumerable<IntPtr> VirtualPages(
            IntPtr handle,
            IntPtr startAddress,
            IntPtr endAddress,
            MemoryProtectionFlags requiredProtection,
            MemoryProtectionFlags excludedProtection,
            MemoryTypeEnum allowedTypes)
        {
            return Query(handle, startAddress, endAddress, requiredProtection, excludedProtection, allowedTypes).Select(x => x.BaseAddress);
        }

        /// <summary>
        /// Gets all regions of memory allocated in the remote process
        /// </summary>
        /// <param name="handle">Target process handle</param>
        /// <returns>A collection of pointers to virtual pages in the target process</returns>
        public static IEnumerable<IntPtr> AllVirtualPages(IntPtr handle)
        {
            MemoryTypeEnum flags = MemoryTypeEnum.None | MemoryTypeEnum.Private | MemoryTypeEnum.Image | MemoryTypeEnum.Mapped;
            return Query(handle, IntPtr.Zero, IntPtr.Zero.MaxValue(), 0, 0, flags).Select(x => x.BaseAddress);
        }

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory information is queried</param>
        /// <param name="baseAddress">A pointer to the base address of the region of pages to be queried</param>
        /// <returns>A <see cref="MemoryBasicInformation64"/> structures in which information about the specified page range is returned</returns>
        public static MemoryBasicInformation64 Query(IntPtr processHandle, IntPtr baseAddress)
        {
            Int32 queryResult;
            return Query(processHandle, baseAddress, out queryResult);
        }

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory information is queried</param>
        /// <param name="baseAddress">A pointer to the base address of the region of pages to be queried</param>
        /// <param name="queryResult">The value returned by the native methods when performing query</param>
        /// <returns>A <see cref="MemoryBasicInformation64"/> structures in which information about the specified page range is returned</returns>
        public static MemoryBasicInformation64 Query(IntPtr processHandle, IntPtr baseAddress, out Int32 queryResult)
        {
            // Allocate the structure to store information of memory
            MemoryBasicInformation64 memoryInfo64 = new MemoryBasicInformation64();

            if (!Environment.Is64BitProcess)
            {
                // 32 Bit struct is not the same
                MemoryBasicInformation32 memoryInfo32 = new MemoryBasicInformation32();

                // Query the memory region
                queryResult = NativeMethods.VirtualQueryEx(processHandle, baseAddress, out memoryInfo32, Marshal.SizeOf(memoryInfo32));

                // Copy from the 32 bit struct to the 64 bit struct
                memoryInfo64.AllocationBase = memoryInfo32.AllocationBase;
                memoryInfo64.AllocationProtect = memoryInfo32.AllocationProtect;
                memoryInfo64.BaseAddress = memoryInfo32.BaseAddress;
                memoryInfo64.Protect = memoryInfo32.Protect;
                memoryInfo64.RegionSize = memoryInfo32.RegionSize;
                memoryInfo64.State = memoryInfo32.State;
                memoryInfo64.Type = memoryInfo32.Type;
            }
            else
            {
                // Query the memory region
                queryResult = NativeMethods.VirtualQueryEx(processHandle, baseAddress, out memoryInfo64, Marshal.SizeOf(memoryInfo64));
            }

            return memoryInfo64;
        }

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory information is queried</param>
        /// <param name="startAddress">A pointer to the starting address of the region of pages to be queried</param>
        /// <param name="endAddress">A pointer to the ending address of the region of pages to be queried</param>
        /// <param name="requiredProtection">Protection flags required to be present</param>
        /// <param name="excludedProtection">Protection flags that must not be present</param>
        /// <param name="allowedTypes">Memory types that can be present</param>
        /// <returns>
        /// A collection of <see cref="MemoryBasicInformation64"/> structures containing info about all virtual pages in the target process
        /// </returns>
        public static IEnumerable<MemoryBasicInformation64> Query(
            IntPtr processHandle,
            IntPtr startAddress,
            IntPtr endAddress,
            MemoryProtectionFlags requiredProtection,
            MemoryProtectionFlags excludedProtection,
            MemoryTypeEnum allowedTypes)
        {
            if (startAddress.ToUInt64() >= endAddress.ToUInt64())
            {
                yield return new MemoryBasicInformation64();
            }

            // Create the variable storing the result of the call of VirtualQueryEx
            Int32 queryResult;
            Boolean wrappedAround = false;

            // Enumerate the memory pages
            do
            {
                // Allocate the structure to store information of memory
                MemoryBasicInformation64 memoryInfo = Query(processHandle, startAddress, out queryResult);

                // Increment the starting address with the size of the page
                IntPtr previousFrom = startAddress;
                startAddress = startAddress.Add(memoryInfo.RegionSize);

                if (previousFrom.ToUInt64() > startAddress.ToUInt64())
                {
                    wrappedAround = true;
                }

                // Ignore free memory. These are unallocated memory regions.
                if ((memoryInfo.State & MemoryStateFlags.Free) != 0)
                {
                    continue;
                }

                // At least one readable memory flag is required
                if ((memoryInfo.Protect & MemoryProtectionFlags.ReadOnly) == 0 && (memoryInfo.Protect & MemoryProtectionFlags.ExecuteRead) == 0 &&
                    (memoryInfo.Protect & MemoryProtectionFlags.ExecuteReadWrite) == 0 && (memoryInfo.Protect & MemoryProtectionFlags.ReadWrite) == 0)
                {
                    continue;
                }

                // Do not bother with this shit, this memory is not worth scanning
                if ((memoryInfo.Protect & MemoryProtectionFlags.ZeroAccess) != 0 || (memoryInfo.Protect & MemoryProtectionFlags.NoAccess) != 0 || (memoryInfo.Protect & MemoryProtectionFlags.Guard) != 0)
                {
                    continue;
                }

                // Enforce allowed types
                switch (memoryInfo.Type)
                {
                    case MemoryTypeFlags.None:
                        if ((allowedTypes & MemoryTypeEnum.None) == 0)
                        {
                            continue;
                        }

                        break;
                    case MemoryTypeFlags.Private:
                        if ((allowedTypes & MemoryTypeEnum.Private) == 0)
                        {
                            continue;
                        }

                        break;
                    case MemoryTypeFlags.Image:
                        if ((allowedTypes & MemoryTypeEnum.Image) == 0)
                        {
                            continue;
                        }

                        break;
                    case MemoryTypeFlags.Mapped:
                        if ((allowedTypes & MemoryTypeEnum.Mapped) == 0)
                        {
                            continue;
                        }

                        break;
                }

                // Ensure at least one required protection flag is set
                if (requiredProtection != 0 && (memoryInfo.Protect & requiredProtection) == 0)
                {
                    continue;
                }

                // Ensure no ignored protection flags are set
                if (excludedProtection != 0 && (memoryInfo.Protect & excludedProtection) != 0)
                {
                    continue;
                }

                // Return the memory page
                yield return memoryInfo;
            }
            while (startAddress.ToUInt64() < endAddress.ToUInt64() && queryResult != 0 && !wrappedAround);
        }
    }
    //// End class
}
//// End namespace