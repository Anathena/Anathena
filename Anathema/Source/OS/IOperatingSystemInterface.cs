using System;
using System.Collections.Generic;

namespace Anathema
{
    public interface IOperatingSystemInterface
    {
        // Virtual pages
        IEnumerable<NormalizedRegion> GetVirtualPages(IntPtr StartAddress, IntPtr EndAddress);
        IEnumerable<NormalizedRegion> GetVirtualPages();
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