using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binarysharp.MemoryManagement
{
    interface LuaFunctions
    {
        UInt64 GetModuleAddress(String ModuleName);
        UInt64 GetReturnAddress(UInt64 Address);

        Byte[] CreateCodeCave();

        void RestoreCode();

    } // End interface

} // End namespace