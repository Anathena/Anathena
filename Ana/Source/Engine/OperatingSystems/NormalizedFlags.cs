using System;

namespace Anathena.Source.Engine.OperatingSystems
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

} // End namespace