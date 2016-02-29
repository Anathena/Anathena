using System;

namespace FASMSharedInterface
{
    public interface ISharedAssemblyInterface
    {
        Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, UInt64 BaseAddress);

    } // End interface

} // End namespace