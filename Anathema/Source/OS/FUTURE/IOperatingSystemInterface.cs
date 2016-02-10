using System;

namespace Binarysharp.MemoryManagement
{
    /// <summary>
    /// Specifies the interface requir
    /// </summary>
    public abstract class IOperatingSystemInterface
    {
        public IVirtualMemoryInterface VirtualMemoryInterface;
        public IProcessInterface ProcessInterface;

    } // End interface

} // End namespace