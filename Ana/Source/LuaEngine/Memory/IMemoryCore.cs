namespace Ana.Source.LuaEngine.Memory
{
    using System;

    /// <summary>
    /// Interface to provide access to memory manipulations in an external process
    /// </summary>
    internal interface IMemoryCore
    {
        /// <summary>
        /// Returns the address of the specified module name. Returns 0 on failure
        /// </summary>
        /// <param name="moduleName">The name of the module to calculate the address of</param>
        /// <returns>The read address</returns>
        UInt64 GetModuleAddress(String moduleName);

        /// <summary>
        /// Determines the size of the first instruction at the given address
        /// </summary>
        /// <param name="assembly">The assembly code to measure</param>
        /// <param name="address">The base address of the assembly code</param>
        /// <returns>The size, in number of bytes, of the assembly code</returns>
        Int32 GetAssemblySize(String assembly, UInt64 address);

        /// <summary>
        /// Converts the instruction with a frame of reference at a specific address to raw bytes
        /// </summary>
        /// <param name="assembly">The assembly code to disassemble</param>
        /// <param name="address">The base address of the assembly code</param>
        /// <returns>The disassembled bytes of the assembly code</returns>
        Byte[] GetAssemblyBytes(String assembly, UInt64 address);

        /// <summary>
        /// Returns the bytes of multiple instructions, as long as they are greater than the specified minimum
        /// ie with a minimum of 16 bytes, if we get three 5 byte instructions, we will need to read the entire
        /// next instruction
        /// </summary>
        /// <param name="address">The base address to begin reading instructions</param>
        /// <param name="minimumInstructionBytes">The minimum number of bytes the instructions must take</param>
        /// <returns>The bytes read from memory</returns>
        Byte[] GetInstructionBytes(UInt64 address, Int32 minimumInstructionBytes);

        /// <summary>
        /// Allocates memory in the target process, and returns the address of the new memory
        /// </summary>
        /// <param name="size">The size of the allocation</param>
        /// <returns>The address of the allocated memory</returns>
        UInt64 AllocateMemory(Int32 size);

        /// <summary>
        /// Deallocates memory previously allocated at a specified address
        /// </summary>
        /// <param name="address">The address to perform the deallocation</param>
        void DeallocateMemory(UInt64 address);

        /// <summary>
        /// Deallocates all allocated memory for the parent lua script.
        /// </summary>
        void DeallocateAllMemory();

        /// <summary>
        /// Creates a code cave that jumps from a given entry address and executes the given assembly
        /// </summary>
        /// <param name="entry">The address to jump to the code cave</param>
        /// <param name="assembly">The assembly code to disassemble and inject into the code cave</param>
        /// <returns>The address of the code cave</returns>
        UInt64 CreateCodeCave(UInt64 entry, String assembly);

        /// <summary>
        /// Determines the address that a code cave would need to return to, if one were to be created at the specified address
        /// </summary>
        /// <param name="address">The address of the code cave</param>
        /// <returns>The address to which the code cave will return upon completion</returns>
        UInt64 GetCaveExitAddress(UInt64 address);

        /// <summary>
        /// Removes a created code cave at the specified address
        /// </summary>
        /// <param name="address">The address of the code cave</param>
        void RemoveCodeCave(UInt64 address);

        /// <summary>
        /// Removes all created code caves by the parent lua script
        /// </summary>
        void RemoveAllCodeCaves();

        /// <summary>
        /// Binds a keyword to a given value for use in the lua script
        /// </summary>
        /// <param name="keyword">The local keyword to bind</param>
        /// <param name="address">The address to which the keyword is bound</param>
        void SetKeyword(String keyword, UInt64 address);

        /// <summary>
        /// Binds a keyword to a given value for use in all lua scripts
        /// </summary>
        /// <param name="keyword">The global keyword to bind</param>
        /// <param name="address">The address to which the keyword is bound</param>
        void SetGlobalKeyword(String keyword, UInt64 address);

        /// <summary>
        /// Clears the specified keyword created by the parent lua script
        /// </summary>
        /// <param name="keyword">The local keyword to clear</param>
        void ClearKeyword(String keyword);

        /// <summary>
        /// Clears the specified global keyword created by any lua script
        /// </summary>
        /// <param name="keyword">The global keyword to clear</param>
        void ClearGlobalKeyword(String keyword);

        /// <summary>
        /// Clears all keywords created by the parent lua script
        /// </summary>
        void ClearAllKeywords();

        /// <summary>
        /// Clears all global keywords created by any lua script
        /// </summary>
        void ClearAllGlobalKeywords();

        /// <summary>
        /// Searches for the first address that matches the array of bytes
        /// </summary>
        /// <param name="bytes">The array of bytes to search for</param>
        /// <returns>The address of the first first array of byte match</returns>
        UInt64 SearchAOB(Byte[] bytes);

        /// <summary>
        /// Searches for the first address that matches the given array of byte pattern
        /// </summary>
        /// <param name="pattern">The pattern string for which to search</param>
        /// <returns>The address of the first first pattern match</returns>
        UInt64 SearchAOB(String pattern);

        /// <summary>
        /// Searches for all addresses that match the given array of byte pattern
        /// </summary>
        /// <param name="pattern">The array of bytes to search for</param>
        /// <returns>The addresses of all matches</returns>
        UInt64[] SearchAllAob(String pattern);

        /// <summary>
        /// Reads the SByte at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The SByte read from memory</returns>
        SByte ReadSByte(UInt64 address);

        /// <summary>
        /// Reads the Byte at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The Byte read from memory</returns>
        Byte ReadByte(UInt64 address);

        /// <summary>
        /// Reads the Int16 at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The Int16 read from memory</returns>
        Int16 ReadInt16(UInt64 address);

        /// <summary>
        /// Reads the Int32 at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The Int32 read from memory</returns>
        Int32 ReadInt32(UInt64 address);

        /// <summary>
        /// Reads the Int64 at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The Int64 read from memory</returns>
        Int64 ReadInt64(UInt64 address);

        /// <summary>
        /// Reads the UInt16 at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The UInt16 read from memory</returns>
        UInt16 ReadUInt16(UInt64 address);

        /// <summary>
        /// Reads the UInt32 at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The UInt32 read from memory</returns>
        UInt32 ReadUInt32(UInt64 address);

        /// <summary>
        /// Reads the UInt64 at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The UInt64 read from memory</returns>
        UInt64 ReadUInt64(UInt64 address);

        /// <summary>
        /// Reads the Single at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The Single read from memory</returns>
        Single ReadSingle(UInt64 address);

        /// <summary>
        /// Reads the Double at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <returns>The Double read from memory</returns>
        Double ReadDouble(UInt64 address);

        /// <summary>
        /// Reads the array of bytes of the specified count at the given address
        /// </summary>
        /// <param name="address">The address of the read</param>
        /// <param name="count">The number of bytes to read</param>
        /// <returns>The bytes read at the address</returns>
        Byte[] ReadBytes(UInt64 address, Int32 count);

        /// <summary>
        /// Writes the SByte value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteSByte(UInt64 address, SByte value);

        /// <summary>
        /// Writes the Byte value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteByte(UInt64 address, Byte value);

        /// <summary>
        /// Writes the Int16 value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteInt16(UInt64 address, Int16 value);

        /// <summary>
        /// Writes the Int32 value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteInt32(UInt64 address, Int32 value);

        /// <summary>
        /// Writes the Int64 value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteInt64(UInt64 address, Int64 value);

        /// <summary>
        /// Writes the UInt16 value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteUInt16(UInt64 address, UInt16 value);

        /// <summary>
        /// Writes the UInt32 value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteUInt32(UInt64 address, UInt32 value);

        /// <summary>
        /// Writes the UInt64 value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteUInt64(UInt64 address, UInt64 value);

        /// <summary>
        /// Writes the Single value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteSingle(UInt64 address, Single value);

        /// <summary>
        /// Writes the Double value at the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="value">The value of the write</param>
        void WriteDouble(UInt64 address, Double value);

        /// <summary>
        /// Writes the Byte array to the specified address
        /// </summary>
        /// <param name="address">The address of the write</param>
        /// <param name="values">The values of the write</param>
        void WriteBytes(UInt64 address, Byte[] values);
    }
    //// End interface
}
//// End namespace