using System;

namespace AnathenaProxy
{
    public interface IFasmServiceInterface
    {
        Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, UInt64 BaseAddress);

    } // End interface

} // End namespace