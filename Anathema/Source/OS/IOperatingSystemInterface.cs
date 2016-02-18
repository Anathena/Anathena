using System;
using System.Collections.Generic;

namespace Anathema
{
    [Flags]
    public enum MemoryProtectionEnum
    {
        None        = 0x0,
        Write       = 0x1,
        Execute     = 0x2,
        CopyOnWrite = 0x4
    }

    [Flags]
    public enum MemoryTypeEnum
    {
        None        = 0x0,
        Private     = 0x1,
        Image       = 0x2,
        Mapped      = 0x4
    }

    public interface IOperatingSystemInterface
    {
        // Virtual pages
        IEnumerable<NormalizedRegion> GetVirtualPages(MemoryProtectionEnum RequiredProtection, MemoryProtectionEnum ExcludedProtection, IntPtr StartAddress, IntPtr EndAddress);
        IEnumerable<NormalizedRegion> GetVirtualPages(MemoryProtectionEnum RequiredProtection, MemoryProtectionEnum ExcludedProtection);
        IEnumerable<NormalizedRegion> GetAllVirtualPages();

        IEnumerable<NormalizedModule> GetModules();
        IntPtr AllocateMemory(Int32 Size);
        void DeallocateMemory(IntPtr Address);

        IntPtr GetStackAddress();
        IntPtr[] GetHeapAddresses();
        
        // Process
        Boolean Is32Bit();
        Boolean Is64Bit();
        String GetProcessName();

        // Pattern
        IntPtr SearchAOB(Byte[] Bytes);
        IntPtr SearchAOB(String Pattern);
        IntPtr[] SearchllAOB(String Pattern);

        // Reading
        dynamic Read(Type ElementType, IntPtr Address, out Boolean Success);
        T Read<T>(IntPtr Address, out Boolean Success);
        Byte[] ReadBytes(IntPtr Address, Int32 Count, out Boolean Success);

        // Writing
        void Write(Type ElementType, IntPtr Address, dynamic Value);
        void Write<T>(IntPtr Address, T Value);
        void WriteBytes(IntPtr Address, Byte[] Values);

    } // End interface

} // End namespace