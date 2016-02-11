using System;

namespace Anathema
{
    public interface IVirtualMemoryInterface
    {
        // Addressing
        IntPtr GetStackAddress();
        IntPtr[] GetHeapAddresses();

        // Reading
        dynamic Read(Type ElementType, IntPtr Address);
        SByte ReadSByte(IntPtr Address);
        Byte ReadByte(IntPtr Address);
        Int16 ReadInt16(IntPtr Address);
        Int32 ReadInt32(IntPtr Address);
        Int64 ReadInt64(IntPtr Address);
        UInt16 ReadUInt16(IntPtr Address);
        UInt32 ReadUInt32(IntPtr Address);
        UInt64 ReadUInt64(IntPtr Address);
        Single ReadSingle(IntPtr Address);
        Double ReadDouble(IntPtr Address);
        Byte[] ReadBytes(IntPtr Address, Int32 Count);

        // Writing
        void Write(Type ElementType, IntPtr Address, dynamic Value);
        void WriteSByte(IntPtr Address, SByte Value);
        void WriteByte(IntPtr Address, Byte Value);
        void WriteInt16(IntPtr Address, Int16 Value);
        void WriteInt32(IntPtr Address, Int32 Value);
        void WriteInt64(IntPtr Address, Int64 Value);
        void WriteUInt16(IntPtr Address, UInt16 Value);
        void WriteUInt32(IntPtr Address, UInt32 Value);
        void WriteUInt64(IntPtr Address, UInt64 Value);
        void WriteSingle(IntPtr Address, Single Value);
        void WriteDouble(IntPtr Address, Double Value);
        void WriteBytes(IntPtr Address, Byte[] Values);
        
    } // End interface

} // End namespace