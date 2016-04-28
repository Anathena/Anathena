using System;
using System.Collections.Generic;
using System.Reflection;

namespace Anathema.Utils.OS
{
    [Flags]
    public enum MemoryProtectionEnum
    {
        Write = 0x1,
        Execute = 0x2,
        CopyOnWrite = 0x4
    }

    [Flags]
    public enum MemoryTypeEnum
    {
        None = 0x1,
        Private = 0x2,
        Image = 0x4,
        Mapped = 0x8
    }

    public interface IOperatingSystemInterface
    {
        // Virtual pages
        IEnumerable<NormalizedRegion> GetVirtualPages(MemoryProtectionEnum RequiredProtection, MemoryProtectionEnum ExcludedProtection,
                                                    MemoryTypeEnum AllowedTypes, IntPtr StartAddress, IntPtr EndAddress);
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
        [Obfuscation(Exclude = true)]
        dynamic Read(Type ElementType, IntPtr Address, out Boolean Success);
        T Read<T>(IntPtr Address, out Boolean Success);
        Byte[] ReadBytes(IntPtr Address, Int32 Count, out Boolean Success);

        // Writing
        [Obfuscation(Exclude = true)]
        void Write(Type ElementType, IntPtr Address, dynamic Value);
        void Write<T>(IntPtr Address, T Value);
        void WriteBytes(IntPtr Address, Byte[] Values);

    } // End interface

} // End namespace