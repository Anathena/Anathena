using System;

namespace Binarysharp.MemoryManagement
{
    public interface IVirtualMemoryInterface
    {
        void WriteProcessMemory(IntPtr Address, Byte Value);

        Byte ReadProcessMemory(IntPtr Address);

    } // End interface

} // End namespace