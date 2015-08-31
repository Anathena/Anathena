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
using System.ComponentModel;
using Binarysharp.MemoryManagement.Helpers;
using Binarysharp.MemoryManagement.Internals;
using Binarysharp.MemoryManagement.Native;

namespace Binarysharp.MemoryManagement.Memory
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
        /// <param name="processHandle">The handle to a process.</param>
        /// <param name="size">The size of the region of memory to allocate, in bytes.</param>
        /// <param name="protectionFlags">The memory protection for the region of pages to be allocated.</param>
        /// <param name="allocationFlags">The type of memory allocation.</param>
        /// <returns>The base address of the allocated region.</returns>
        public static IntPtr Allocate(SafeMemoryHandle processHandle, int size, MemoryProtectionFlags protectionFlags = MemoryProtectionFlags.ExecuteReadWrite,
            MemoryAllocationFlags allocationFlags = MemoryAllocationFlags.Commit)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            
            // Allocate a memory page
            var ret = NativeMethods.VirtualAllocEx(processHandle, IntPtr.Zero, size, allocationFlags, protectionFlags);

            // Check whether the memory page is valid
            if (ret != IntPtr.Zero)
                return ret;

            // If the pointer isn't valid, throws an exception
            throw new Win32Exception(string.Format("Couldn't allocate memory of {0} byte(s).", size));
        }
        #endregion

        #region CloseHandle
        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="handle">A valid handle to an open object.</param>
        public static void CloseHandle(IntPtr handle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(handle, "handle");

            // Close the handle
            if (!NativeMethods.CloseHandle(handle))
            {
                throw new Win32Exception(string.Format("Couldn't close he handle 0x{0}.", handle));
            }
        }
        #endregion

        #region Free
        /// <summary>
        /// Releases a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to a process.</param>
        /// <param name="address">A pointer to the starting address of the region of memory to be freed.</param>
        public static void Free(SafeMemoryHandle processHandle, IntPtr address)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(address, "address");

            // Free the memory
            if (!NativeMethods.VirtualFreeEx(processHandle, address, 0, MemoryReleaseFlags.Release))
            {
                // If the memory wasn't correctly freed, throws an exception
                throw new Win32Exception(string.Format("The memory page 0x{0} cannot be freed.", address.ToString("X")));
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
            var info = new ProcessBasicInformation();

            // Get the process info
            var ret = NativeMethods.NtQueryInformationProcess(processHandle, ProcessInformationClass.ProcessBasicInformation, ref info, info.Size, IntPtr.Zero);

            // If the function succeeded
            if (ret == 0)
                return info;

            // Else, couldn't get the process info, throws an exception
            throw new ApplicationException(string.Format("Couldn't get the information from the process, error code '{0}'.", ret));
        }
        #endregion

        #region OpenProcess
        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="accessFlags">The access level to the process object.</param>
        /// <param name="processId">The identifier of the local process to be opened.</param>
        /// <returns>An open handle to the specified process.</returns>
        public static SafeMemoryHandle OpenProcess(ProcessAccessFlags accessFlags, int processId)
        {
            // Get an handle from the remote process
            var handle = NativeMethods.OpenProcess(accessFlags, false, processId);

            // Check whether the handle is valid
            if (!handle.IsInvalid && !handle.IsClosed)
                return handle;

            // Else the handle isn't valid, throws an exception
            throw new Win32Exception(string.Format("Couldn't open the process {0}.", processId));
        }
        #endregion

        #region ReadBytes
        /// <summary>
        /// Reads an array of bytes in the memory form the target process.
        /// </summary>
        /// <param name="processHandle">A handle to the process with memory that is being read.</param>
        /// <param name="address">A pointer to the base address in the specified process from which to read.</param>
        /// <param name="size">The number of bytes to be read from the specified process.</param>
        /// <returns>The collection of read bytes.</returns>
        public static byte[] ReadBytes(SafeMemoryHandle processHandle, IntPtr address, int size)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(address, "address");
            
            // Allocate the buffer
            var buffer = new byte[size];
            int nbBytesRead;

            // Read the data from the target process
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, size, out nbBytesRead) && size == nbBytesRead)
                return buffer;

            // Else the data couldn't be read, throws an exception
            throw new Win32Exception(string.Format("Couldn't read {0} byte(s) from 0x{1}.", size, address.ToString("X")));
        }
        #endregion

        #region ChangeProtection
        /// <summary>
        /// Changes the protection on a region of committed pages in the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory protection is to be changed.</param>
        /// <param name="address">A pointer to the base address of the region of pages whose access protection attributes are to be changed.</param>
        /// <param name="size">The size of the region whose access protection attributes are changed, in bytes.</param>
        /// <param name="protection">The memory protection option.</param>
        /// <returns>The old protection of the region in a <see cref="Native.MemoryBasicInformation"/> structure.</returns>
        public static MemoryProtectionFlags ChangeProtection(SafeMemoryHandle processHandle, IntPtr address, int size, MemoryProtectionFlags protection)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(address, "address");
            
            // Create the variable storing the old protection of the memory page
            MemoryProtectionFlags oldProtection;

            // Change the protection in the target process
            if(NativeMethods.VirtualProtectEx(processHandle, address, size, protection, out oldProtection))
            {
                // Return the old protection
                return oldProtection;
            }

            // Else the protection couldn't be changed, throws an exception
            throw new Win32Exception(string.Format("Couldn't change the protection of the memory at 0x{0} of {1} byte(s) to {2}.", address.ToString("X"), size, protection));
        }
        #endregion

        #region Query
        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="baseAddress">A pointer to the base address of the region of pages to be queried.</param>
        /// <returns>A <see cref="Native.MemoryBasicInformation"/> structures in which information about the specified page range is returned.</returns>
        public static MemoryBasicInformation Query(SafeMemoryHandle processHandle, IntPtr baseAddress)
        {
            // Allocate the structure to store information of memory
            MemoryBasicInformation memoryInfo;

            // Query the memory region
            if(NativeMethods.VirtualQueryEx(processHandle, baseAddress, out memoryInfo, MarshalType<MemoryBasicInformation>.Size) != 0)
                return memoryInfo;

            // Else the information couldn't be got
            throw new Win32Exception(string.Format("Couldn't query information about the memory region 0x{0}", baseAddress.ToString("X")));
        }
        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="addressFrom">A pointer to the starting address of the region of pages to be queried.</param>
        /// <param name="addressTo">A pointer to the ending address of the region of pages to be queried.</param>
        /// <returns>A collection of <see cref="Native.MemoryBasicInformation"/> structures.</returns>
        public static IEnumerable<MemoryBasicInformation> Query(SafeMemoryHandle processHandle, IntPtr addressFrom, IntPtr addressTo)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            
            // Convert the addresses to Int64
            var numberFrom = addressFrom.ToInt64();
            var numberTo = addressTo.ToInt64();

            // The first address must be lower than the second
            if (numberFrom >= numberTo)
                throw new ArgumentException("The starting address must be lower than the ending address.", "addressFrom");
            
            // Create the variable storing the result of the call of VirtualQueryEx
            int ret;

            // Enumerate the memory pages
            do
            {
                // Allocate the structure to store information of memory
                MemoryBasicInformation memoryInfo;

                // Get the next memory page
                ret = NativeMethods.VirtualQueryEx(processHandle, new IntPtr(numberFrom), out memoryInfo, MarshalType<MemoryBasicInformation>.Size);

                // Increment the starting address with the size of the page
                numberFrom += memoryInfo.RegionSize;

                // Return the memory page
                if(memoryInfo.State != MemoryStateFlags.Free)
                    yield return memoryInfo;

            } while (numberFrom < numberTo && ret != 0);
        }
        #endregion

        #region WriteBytes
        /// <summary>
        /// Writes data to an area of memory in a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process memory to be modified.</param>
        /// <param name="address">A pointer to the base address in the specified process to which data is written.</param>
        /// <param name="byteArray">A buffer that contains data to be written in the address space of the specified process.</param>
        /// <returns>The number of bytes written.</returns>
        public static int WriteBytes(SafeMemoryHandle processHandle, IntPtr address, byte[] byteArray)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(address, "address");

            // Create the variable storing the number of bytes written
            int nbBytesWritten;

            // Write the data to the target process
            if (NativeMethods.WriteProcessMemory(processHandle, address, byteArray, byteArray.Length, out nbBytesWritten))
            {
                // Check whether the length of the data written is equal to the inital array
                if (nbBytesWritten == byteArray.Length)
                    return nbBytesWritten;
            }

            // Else the data couldn't be written, throws an exception
            throw new Win32Exception(string.Format("Couldn't write {0} bytes to 0x{1}", byteArray.Length, address.ToString("X")));
        }
        #endregion
    }
}
