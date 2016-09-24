namespace Ana.Source.Engine.OperatingSystems
{
    using Processes;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// An interface that describes general methods for memory manipulations that must be handled by the operating system
    /// </summary>
    internal interface IOperatingSystemAdapter : IProcessListener
    {
        /// <summary>
        /// Gets regions of memory allocated in the remote process based on provided parameters
        /// </summary>
        /// <param name="requiredProtection">Protection flags required to be present</param>
        /// <param name="excludedProtection">Protection flags that must not be present</param>
        /// <param name="allowedTypes">Memory types that can be present</param>
        /// <param name="startAddress">The start address of the query range</param>
        /// <param name="endAddress">The end address of the query range</param>
        /// <returns>A collection of pointers to virtual pages in the target process</returns>
        IEnumerable<NormalizedRegion> GetVirtualPages(
            MemoryProtectionEnum requiredProtection,
            MemoryProtectionEnum excludedProtection,
            MemoryTypeEnum allowedTypes,
            IntPtr startAddress,
            IntPtr endAddress);

        /// <summary>
        /// Gets all virtual pages in the opened process
        /// </summary>
        /// <returns>A collection of regions in the process</returns>
        IEnumerable<NormalizedRegion> GetAllVirtualPages();

        /// <summary>
        /// Gets the maximum address possible in the target process
        /// </summary>
        /// <returns>The maximum address possible in the target process</returns>
        IntPtr GetMaximumAddress();

        /// <summary>
        /// Gets the maximum usermode address possible in the target process
        /// </summary>
        /// <returns>The maximum usermode address possible in the target process</returns>
        IntPtr GetMaximumUserModeAddress();

        /// <summary>
        /// Gets all modules in the opened process
        /// </summary>
        /// <returns>A collection of modules in the process</returns>
        IEnumerable<NormalizedModule> GetModules();

        /// <summary>
        /// Allocates memory in the opened process
        /// </summary>
        /// <param name="size">The size of the memory allocation</param>
        /// <returns>A pointer to the location of the allocated memory</returns>
        IntPtr AllocateMemory(Int32 size);

        /// <summary>
        /// Deallocates memory in the opened process
        /// </summary>
        /// <param name="address">The address to perform the region wide deallocation</param>
        void DeallocateMemory(IntPtr address);

        /// <summary>
        /// Gets the address of the stack in the opened process
        /// </summary>
        /// <returns>A pointer to the stack of the opened process</returns>
        IntPtr GetStackAddress();

        /// <summary>
        /// Gets the addresses of the heaps in the opened process
        /// </summary>
        /// <returns>A collection of pointers to all heaps in the opened process</returns>
        IEnumerable<IntPtr> GetHeapAddresses();

        /// <summary>
        /// Determines if the operating system is 32 bit
        /// </summary>
        /// <returns>A boolean indicating if the OS is 32 bit or not</returns>
        Boolean IsOperatingSystem32Bit();

        /// <summary>
        /// Determines if the operating system is 64 bit
        /// </summary>
        /// <returns>A boolean indicating if the OS is 64 bit or not</returns>
        Boolean IsOperatingSystem64Bit();

        /// <summary>
        /// Determines if this program is 32 bit
        /// </summary>
        /// <returns>A boolean indicating if this program is 32 bit or not</returns>
        Boolean IsAnathena32Bit();

        /// <summary>
        /// Determines if this program is 64 bit
        /// </summary>
        /// <returns>A boolean indicating if this program is 64 bit or not</returns>
        Boolean IsAnathena64Bit();

        /// <summary>
        /// Determines if a process is 32 bit
        /// </summary>
        /// <param name="process">The process to check</param>
        /// <returns>Returns true if the process is 32 bit, otherwise false</returns>
        Boolean IsProcess32Bit(NormalizedProcess process);

        /// <summary>
        /// Determines if a process is 64 bit
        /// </summary>
        /// <param name="process">The process to check</param>
        /// <returns>Returns true if the process is 64 bit, otherwise false</returns>
        Boolean IsProcess64Bit(NormalizedProcess process);

        /// <summary>
        /// Searches for an array of bytes in the opened process
        /// </summary>
        /// <param name="bytes">The byte array to search for</param>
        /// <returns>The address of the first match</returns>
        IntPtr SearchAob(Byte[] bytes);

        /// <summary>
        /// Searches for an array of bytes in the opened process
        /// </summary>
        /// <param name="pattern">The string pattern to search for</param>
        /// <returns>The address of the first match</returns>
        IntPtr SearchAob(String pattern);

        /// <summary>
        /// Searches for an array of bytes in the opened process
        /// </summary>
        /// <param name="pattern">The string pattern to search for</param>
        /// <returns>The address of all matches</returns>
        IEnumerable<IntPtr> SearchllAob(String pattern);

        /// <summary>
        /// Reads a value from the opened processes memory
        /// </summary>
        /// <param name="elementType">The data type to read</param>
        /// <param name="address">The address to read from</param>
        /// <param name="success">Whether or not the read succeeded</param>
        /// <returns>The value read from memory</returns>
        [Obfuscation(Exclude = true)]
        dynamic Read(Type elementType, IntPtr address, out Boolean success);

        /// <summary>
        /// Reads a value from the opened processes memory
        /// </summary>
        /// <typeparam name="T">The data type to read</typeparam>
        /// <param name="address">The address to read from</param>
        /// <param name="success">Whether or not the read succeeded</param>
        /// <returns>The value read from memory</returns>
        T Read<T>(IntPtr address, out Boolean success);

        /// <summary>
        /// Reads an array of bytes from the opened processes memory
        /// </summary>
        /// <param name="address">The address to read from</param>
        /// <param name="count">The number of bytes to read</param>
        /// <param name="success">Whether or not the read succeeded</param>
        /// <returns>The array of bytes read from memory, if the read succeeded</returns>
        Byte[] ReadBytes(IntPtr address, Int32 count, out Boolean success);

        /// <summary>
        /// Writes a value to memory in the opened process
        /// </summary>
        /// <param name="elementType">The data type to write</param>
        /// <param name="address">The address to write to</param>
        /// <param name="value">The value to write</param>
        [Obfuscation(Exclude = true)]
        void Write(Type elementType, IntPtr address, dynamic value);

        /// <summary>
        /// Writes a value to memory in the opened process
        /// </summary>
        /// <typeparam name="T">The data type to write</typeparam>
        /// <param name="address">The address to write to</param>
        /// <param name="value">The value to write</param>
        void Write<T>(IntPtr address, T value);

        /// <summary>
        /// Writes a value to memory in the opened process
        /// </summary>
        /// <param name="address">The address to write to</param>
        /// <param name="values">The value to write</param>
        void WriteBytes(IntPtr address, Byte[] values);
    }
    //// End interface
}
//// End namespace