using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASMSharedInterface
{
    public interface ISharedAssemblyInterface
    {
        Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, UInt64 BaseAddress);

    } // End interface

} // End namespace