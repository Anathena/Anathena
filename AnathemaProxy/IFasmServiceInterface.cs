using System;

namespace AnathenaProxy
{
    public interface IFasmServiceInterface
    {
        Byte[] Assemble(String Assembly);

    } // End interface

} // End namespace