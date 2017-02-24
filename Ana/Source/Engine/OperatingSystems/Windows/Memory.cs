namespace Ana.Source.Engine.OperatingSystems.Windows
{
    using Native;
    using Processes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Utils;
    using Utils.Extensions;
    using static Native.Enumerations;
    using static Native.Structures;

    /// <summary>
    /// Static class providing tools for windows memory editing internals.
    /// </summary>
    internal static class Memory
    {
        /// <summary>
        /// The number of retry attempts for memory allocation. This is used to prevent cases of bad luck for 64-bit memory allocation when a specific address is requested.
        /// </summary>
        private const Int32 AllocateRetryCount = 4;

        /// <summary>
        /// A windows constraint on the address alignment of an allocated virtual memory page.
        /// </summary>
        private const Int32 AllocAlignment = 0x10000;

        /// <summary>
        /// Reads an array of bytes in the memory form the target process.
        /// </summary>
        /// <param name="processHandle">A handle to the process with memory that is being read.</param>
        /// <param name="address">A pointer to the base address in the specified process from which to read.</param>
        /// <param name="size">The number of bytes to be read from the specified process.</param>
        /// <param name="success">Whether or not the read operation succeeded.</param>
        /// <returns>The bytes read from the target, can be null or empty on failure.</returns>
        public static Byte[] ReadBytes(IntPtr processHandle, IntPtr address, Int32 size, out Boolean success)
        {
            // Allocate the buffer
            Byte[] buffer = new Byte[size];
            Int32 bytesRead;

            // Read the data from the target process
            success = NativeMethods.ReadProcessMemory(processHandle, address, buffer, size, out bytesRead) && size == bytesRead;

            return buffer;
        }

        /// <summary>
        /// Writes bytes to memory in a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process memory to be modified.</param>
        /// <param name="address">A pointer to the base address in the specified process to which data is written.</param>
        /// <param name="byteArray">A buffer that contains data to be written in the address space of the specified process.</param>
        /// <returns>The number of bytes written.</returns>
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

        /// <summary>
        /// Reserves a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">The handle to a process.</param>
        /// <param name="allocAddress">The rough address of where the allocation should take place.</param>
        /// <param name="size">The size of the region of memory to allocate, in bytes.</param>
        /// <param name="protectionFlags">The memory protection for the region of pages to be allocated.</param>
        /// <param name="allocationFlags">The type of memory allocation.</param>
        /// <returns>The base address of the allocated region</returns>
        public static IntPtr Allocate(
            IntPtr processHandle,
            IntPtr allocAddress,
            Int32 size,
            MemoryProtectionFlags protectionFlags = MemoryProtectionFlags.ExecuteReadWrite,
            MemoryAllocationFlags allocationFlags = MemoryAllocationFlags.Commit | MemoryAllocationFlags.Reserve)
        {
            if (allocAddress != IntPtr.Zero)
            {
                /* A specific address has been given. We will modify it to support the following constraints:
                 *  - Aligned by 0x10000 / 65536
                 *  - Pointing to an unallocated region of memory
                 *  - Within +/- 2GB (using 1GB for safety) of address space of the originally specified address, such as to always be in range of a far jump instruction
                 * Note: A retry count has been put in place because VirtualAllocEx with an allocAddress specified may be invalid by the time we request the allocation.
                 */

                IntPtr result = IntPtr.Zero;
                Int32 retryCount = 0;

                // Request all chunks of unallocated memory. These will be very large in a 64-bit process.
                IEnumerable<MemoryBasicInformation64> freeMemory = Memory.QueryUnallocatedMemory(
                    processHandle,
                    allocAddress.Subtract(Int32.MaxValue >> 1, wrapAround: false),
                    allocAddress.Add(Int32.MaxValue >> 1, wrapAround: false));

                // Convert to normalized regions
                IEnumerable<NormalizedRegion> regions = freeMemory.Select(x => new NormalizedRegion(x.BaseAddress, unchecked((Int32)x.RegionSize)));

                // Chunk the large regions into smaller regions based on the allocation size (minimum size is the alloc alignment to prevent creating too many chunks)
                List<NormalizedRegion> subRegions = new List<NormalizedRegion>();
                foreach (NormalizedRegion region in regions)
                {
                    region.BaseAddress = region.BaseAddress.Subtract(region.BaseAddress.Mod(Memory.AllocAlignment), wrapAround: false);
                    subRegions.AddRange(region.ChunkNormalizedRegion(Math.Max(size, Memory.AllocAlignment)).Select(x => x).Where(x => x.RegionSize >= size));
                }

                do
                {
                    // Sample a random chunk and attempt to allocate the memory
                    result = subRegions.ElementAt(StaticRandom.Next(0, subRegions.Count())).BaseAddress;
                    result = NativeMethods.VirtualAllocEx(processHandle, result, size, allocationFlags, protectionFlags);

                    if (result != IntPtr.Zero || retryCount >= Memory.AllocateRetryCount)
                    {
                        break;
                    }

                    retryCount++;
                }
                while (result == IntPtr.Zero);

                return result;
            }
            else
            {
                // Allocate a memory page
                return NativeMethods.VirtualAllocEx(processHandle, allocAddress, size, allocationFlags, protectionFlags);
            }
        }

        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="accessFlags">The access level to the process object.</param>
        /// <param name="process">The identifier of the local process to be opened.</param>
        /// <returns>An open handle to the specified process.</returns>
        public static IntPtr OpenProcess(ProcessAccessFlags accessFlags, NormalizedProcess process)
        {
            return NativeMethods.OpenProcess(accessFlags, false, process == null ? 0 : process.ProcessId);
        }

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="handle">A valid handle to an open object.</param>
        public static void CloseHandle(IntPtr handle)
        {
            // Close the handle
            NativeMethods.CloseHandle(handle);
        }

        /// <summary>
        /// Releases a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to a process.</param>
        /// <param name="address">A pointer to the starting address of the region of memory to be freed.</param>
        public static void Free(IntPtr processHandle, IntPtr address)
        {
            // Free the memory
            NativeMethods.VirtualFreeEx(processHandle, address, 0, MemoryReleaseFlags.Release);
        }

        /// <summary>
        /// Changes the protection on a region of committed pages in the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory protection is to be changed.</param>
        /// <param name="address">A pointer to the base address of the region of pages whose access protection attributes are to be changed.</param>
        /// <param name="size">The size of the region whose access protection attributes are changed, in bytes.</param>
        /// <param name="protection">The memory protection option.</param>
        /// <returns>The old protection of the region in a <see cref="MemoryBasicInformation32"/> structure.</returns>
        public static MemoryProtectionFlags ChangeProtection(IntPtr processHandle, IntPtr address, Int32 size, MemoryProtectionFlags protection)
        {
            // Create the variable storing the old protection of the memory page
            MemoryProtectionFlags oldProtection;

            // Change the protection in the target process
            NativeMethods.VirtualProtectEx(processHandle, address, size, protection, out oldProtection);

            return oldProtection;
        }

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="startAddress">A pointer to the starting address of the region of pages to be queried.</param>
        /// <param name="endAddress">A pointer to the ending address of the region of pages to be queried.</param>
        /// <returns>
        /// A collection of <see cref="MemoryBasicInformation64"/> structures containing info about all virtual pages in the target process.
        /// </returns>
        public static IEnumerable<MemoryBasicInformation64> QueryUnallocatedMemory(
            IntPtr processHandle,
            IntPtr startAddress,
            IntPtr endAddress)
        {
            if (startAddress.ToUInt64() >= endAddress.ToUInt64())
            {
                yield return new MemoryBasicInformation64();
            }

            Boolean wrappedAround = false;
            Int32 queryResult;

            // Enumerate the memory pages
            do
            {
                // Allocate the structure to store information of memory
                MemoryBasicInformation64 memoryInfo = new MemoryBasicInformation64();

                if (!Environment.Is64BitProcess)
                {
                    // 32 Bit struct is not the same
                    MemoryBasicInformation32 memoryInfo32 = new MemoryBasicInformation32();

                    // Query the memory region (32 bit native method)
                    queryResult = NativeMethods.VirtualQueryEx(processHandle, startAddress, out memoryInfo32, Marshal.SizeOf(memoryInfo32));

                    // Copy from the 32 bit struct to the 64 bit struct
                    memoryInfo.AllocationBase = memoryInfo32.AllocationBase;
                    memoryInfo.AllocationProtect = memoryInfo32.AllocationProtect;
                    memoryInfo.BaseAddress = memoryInfo32.BaseAddress;
                    memoryInfo.Protect = memoryInfo32.Protect;
                    memoryInfo.RegionSize = memoryInfo32.RegionSize;
                    memoryInfo.State = memoryInfo32.State;
                    memoryInfo.Type = memoryInfo32.Type;
                }
                else
                {
                    // Query the memory region (64 bit native method)
                    queryResult = NativeMethods.VirtualQueryEx(processHandle, startAddress, out memoryInfo, Marshal.SizeOf(memoryInfo));
                }

                // Increment the starting address with the size of the page
                IntPtr previousFrom = startAddress;
                startAddress = startAddress.Add(memoryInfo.RegionSize);

                if (previousFrom.ToUInt64() > startAddress.ToUInt64())
                {
                    wrappedAround = true;
                }

                if ((memoryInfo.State & MemoryStateFlags.Free) != 0)
                {
                    // Return the unallocated memory page
                    yield return memoryInfo;
                }
                else
                {
                    // Ignore actual memory
                    continue;
                }
            }
            while (startAddress.ToUInt64() < endAddress.ToUInt64() && queryResult != 0 && !wrappedAround);
        }

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="startAddress">A pointer to the starting address of the region of pages to be queried.</param>
        /// <param name="endAddress">A pointer to the ending address of the region of pages to be queried.</param>
        /// <param name="requiredProtection">Protection flags required to be present.</param>
        /// <param name="excludedProtection">Protection flags that must not be present.</param>
        /// <param name="allowedTypes">Memory types that can be present.</param>
        /// <returns>
        /// A collection of <see cref="MemoryBasicInformation64"/> structures containing info about all virtual pages in the target process.
        /// </returns>
        public static IEnumerable<MemoryBasicInformation64> VirtualPages(
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

            Boolean wrappedAround = false;
            Int32 queryResult;

            // Enumerate the memory pages
            do
            {
                // Allocate the structure to store information of memory
                MemoryBasicInformation64 memoryInfo = new MemoryBasicInformation64();

                if (!Environment.Is64BitProcess)
                {
                    // 32 Bit struct is not the same
                    MemoryBasicInformation32 memoryInfo32 = new MemoryBasicInformation32();

                    // Query the memory region (32 bit native method)
                    queryResult = NativeMethods.VirtualQueryEx(processHandle, startAddress, out memoryInfo32, Marshal.SizeOf(memoryInfo32));

                    // Copy from the 32 bit struct to the 64 bit struct
                    memoryInfo.AllocationBase = memoryInfo32.AllocationBase;
                    memoryInfo.AllocationProtect = memoryInfo32.AllocationProtect;
                    memoryInfo.BaseAddress = memoryInfo32.BaseAddress;
                    memoryInfo.Protect = memoryInfo32.Protect;
                    memoryInfo.RegionSize = memoryInfo32.RegionSize;
                    memoryInfo.State = memoryInfo32.State;
                    memoryInfo.Type = memoryInfo32.Type;
                }
                else
                {
                    // Query the memory region (64 bit native method)
                    queryResult = NativeMethods.VirtualQueryEx(processHandle, startAddress, out memoryInfo, Marshal.SizeOf(memoryInfo));
                }

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