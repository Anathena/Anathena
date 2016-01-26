using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public interface LuaFunctions
    {
        UInt64 GetModuleAddress(String ModuleName);
        UInt64 GetReturnAddress(UInt64 Address);

        Byte[] CreateCodeCave();
        void RestoreCode();

    } // End interface

    public class LuaMemoryCore : LuaFunctions
    {
        private MemorySharp MemoryEditor;

        public LuaMemoryCore(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public UInt64 GetModuleAddress(String ModuleName)
        {
            return 0;
        }

        public UInt64 GetReturnAddress(UInt64 Address)
        {
            return 0;
        }

        public Byte[] CreateCodeCave()
        {
            return null;
        }

        public void RestoreCode()
        {

        }

    } // End interface

} // End namespace