using System;

namespace Anathema
{
    /// <summary>
    /// Specifies the interface requir
    /// </summary>
    public abstract class IOperatingSystemInterface
    {
        public IVirtualMemoryInterface VirtualMemoryInterface;
        public IArchitectureInterface ArchitectureInterface;

        public abstract Boolean IsOS32Bit();

    } // End interface

} // End namespace