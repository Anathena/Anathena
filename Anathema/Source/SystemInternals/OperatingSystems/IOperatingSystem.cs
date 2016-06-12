using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Anathema.Source.SystemInternals.OperatingSystems
{

    public interface IOperatingSystem
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
        Process GetProcess();

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