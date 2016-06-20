using System;

namespace Anathema.Source.LuaEngine.Memory
{
    public interface IMemoryCore
    {
        /// <summary>
        /// Returns the address of the specified module name. Returns 0 on failure.
        /// </summary>
        /// <param name="ModuleName"></param>
        /// <returns></returns>
        UInt64 GetModuleAddress(String ModuleName);

        /// <summary>
        /// Determines the size of the first instruction at the given address.
        /// </summary>
        /// <param name="Assembly"></param>
        /// <param name="Address"></param>
        /// <returns></returns>
        Int32 GetAssemblySize(String Assembly, UInt64 Address);

        /// <summary>
        /// Converts the instruction with a frame of reference at a specific address to raw bytes.
        /// </summary>
        /// <param name="Assembly"></param>
        /// <param name="Address"></param>
        /// <returns></returns>
        Byte[] GetAssemblyBytes(String Assembly, UInt64 Address);

        /// <summary>
        /// Returns the bytes of multiple instructions, as long as they are greater than the specified minimum.
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="MinimumInstructionBytes"></param>
        /// <returns></returns>
        Byte[] GetInstructionBytes(UInt64 Address, Int32 MinimumInstructionBytes);


        /// <summary>
        /// Allocates memory in the target process, and returns the address of the new memory.
        /// </summary>
        /// <param name="Size"></param>
        /// <returns></returns>
        UInt64 AllocateMemory(Int32 Size);

        /// <summary>
        /// Deallocates memory previously allocated at a specified address.
        /// </summary>
        /// <param name="Address"></param>
        void DeallocateMemory(UInt64 Address);

        /// <summary>
        /// Deallocates all allocated memory for the parent lua script.
        /// </summary>
        void DeallocateAllMemory();

        /// <summary>
        /// Creates a code cave that jumps from a given entry address and executes the given assembly.
        /// </summary>
        /// <param name="Entry"></param>
        /// <param name="Assembly"></param>
        /// <returns></returns>
        UInt64 CreateCodeCave(UInt64 Entry, String Assembly);

        /// <summary>
        /// Determines the address that a code cave would need to return to, if one were to be created at the specified address.
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        UInt64 GetCaveExitAddress(UInt64 Address);

        /// <summary>
        /// Removes a created code cave at the specified address.
        /// </summary>
        /// <param name="Address"></param>
        void RemoveCodeCave(UInt64 Address);

        /// <summary>
        /// Removes all created code caves by the parent lua script.
        /// </summary>
        void RemoveAllCodeCaves();

        /// <summary>
        /// Binds a keyword to a given value for use in the lua script.
        /// </summary>
        /// <param name="Keyword"></param>
        /// <param name="Address"></param>
        void SetKeyword(String Keyword, UInt64 Address);

        /// <summary>
        /// Binds a keyword to a given value for use in all lua scripts.
        /// </summary>
        /// <param name="Keyword"></param>
        /// <param name="Address"></param>
        void SetGlobalKeyword(String Keyword, UInt64 Address);

        /// <summary>
        /// Clears the specified keyword created by the parent lua script.
        /// </summary>
        /// <param name="Keyword"></param>
        void ClearKeyword(String Keyword);

        /// <summary>
        /// Clears the specified global keyword created by any lua script.
        /// </summary>
        /// <param name="Keyword"></param>
        void ClearGlobalKeyword(String Keyword);

        /// <summary>
        /// Clears all keywords created by the parent lua script.
        /// </summary>
        void ClearAllKeywords();

        /// <summary>
        /// Clears all global keywords created by any lua script.
        /// </summary>
        void ClearAllGlobalKeywords();

        /// <summary>
        /// Searches for the first address that matches the array of bytes
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        UInt64 SearchAOB(Byte[] Bytes);

        /// <summary>
        /// Searches for the first address that matches the given array of byte pattern.
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        UInt64 SearchAOB(String Pattern);

        /// <summary>
        /// Searches for all addresses that match the given array of byte pattern.
        /// </summary>
        /// <param name="Pattern"></param>
        /// <returns></returns>
        UInt64[] SearchAllAOB(String Pattern);

        /// <summary>
        /// Reads the SByte at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        SByte ReadSByte(UInt64 Address);

        /// <summary>
        /// Reads the Byte at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        Byte ReadByte(UInt64 Address);

        /// <summary>
        /// Reads the Int16 at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        Int16 ReadInt16(UInt64 Address);

        /// <summary>
        /// Reads the Int32 at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        Int32 ReadInt32(UInt64 Address);

        /// <summary>
        /// Reads the Int64 at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        Int64 ReadInt64(UInt64 Address);

        /// <summary>
        /// Reads the UInt16 at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        UInt16 ReadUInt16(UInt64 Address);

        /// <summary>
        /// Reads the UInt32 at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        UInt32 ReadUInt32(UInt64 Address);

        /// <summary>
        /// Reads the UInt64 at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        UInt64 ReadUInt64(UInt64 Address);

        /// <summary>
        /// Reads the Single at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        Single ReadSingle(UInt64 Address);

        /// <summary>
        /// Reads the Double at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        Double ReadDouble(UInt64 Address);

        /// <summary>
        /// Reads the array of bytes of the specified count at the given address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        Byte[] ReadBytes(UInt64 Address, Int32 Count);


        /// <summary>
        /// Writes the SByte value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteSByte(UInt64 Address, SByte Value);

        /// <summary>
        /// Writes the Byte value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteByte(UInt64 Address, Byte Value);

        /// <summary>
        /// Writes the Int16 value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteInt16(UInt64 Address, Int16 Value);

        /// <summary>
        /// Writes the Int32 value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteInt32(UInt64 Address, Int32 Value);

        /// <summary>
        /// Writes the Int64 value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteInt64(UInt64 Address, Int64 Value);

        /// <summary>
        /// Writes the UInt16 value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteUInt16(UInt64 Address, UInt16 Value);

        /// <summary>
        /// Writes the UInt32 value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteUInt32(UInt64 Address, UInt32 Value);

        /// <summary>
        /// Writes the UInt64 value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteUInt64(UInt64 Address, UInt64 Value);

        /// <summary>
        /// Writes the Single value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteSingle(UInt64 Address, Single Value);

        /// <summary>
        /// Writes the Double value at the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteDouble(UInt64 Address, Double Value);

        /// <summary>
        /// Writes the Byte array to the specified address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        void WriteBytes(UInt64 Address, Byte[] Values);

    } // End interface
}
