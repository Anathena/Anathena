namespace Ana.Source.Engine.OperatingSystems
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    internal interface IOperatingSystemAdapter
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

        // Environment
        Boolean IsOS32Bit();
        Boolean IsOS64Bit();
        Boolean IsAnathena32Bit();
        Boolean IsAnathena64Bit();

        // Process
        Process GetProcess();
        Boolean IsProcess32Bit();
        Boolean IsProcess32Bit(Process Process);
        Boolean IsProcess64Bit();
        Boolean IsProcess64Bit(Process Process);

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
    }
    //// End interface
}
//// End namespace