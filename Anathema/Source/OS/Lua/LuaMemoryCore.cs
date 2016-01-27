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

        UInt64 AllocateMemory(Int32 Size);
        void DeallocateMemory(UInt64 Address);

        Int32 GetAssemblySize(String Assembly);

        Byte[] CreateCodeCave();
        void SaveMemory(UInt64 Address, Int32 Size = 0);
        void RestoreMemory(UInt64 Address);
        void RestoreAllMemory();

        void SetKeyword(String Keyword, UInt64 Address);
        void SetGlobalKeyword(String Keyword, UInt64 Address);
        void ClearKeyword(String Keyword);
        void ClearGlobalKeyword(String Keyword);
        void ClearAllKeywords();
        void ClearAllGlobalKeywords();

    } // End interface

    public class LuaMemoryCore : LuaFunctions, IProcessObserver
    {
        private MemorySharp MemoryEditor;

        public LuaMemoryCore()
        {
            InitializeProcessObserver();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
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

        public ulong AllocateMemory(int Size)
        {
            throw new NotImplementedException();
        }

        public void DeallocateMemory(ulong Address)
        {
            throw new NotImplementedException();
        }

        public void RestoreMemory(ulong Address)
        {
            throw new NotImplementedException();
        }

        public void RestoreAllMemory()
        {
            throw new NotImplementedException();
        }

        public void SetKeyword(string Keyword, ulong Address)
        {
            throw new NotImplementedException();
        }

        public void SetGlobalKeyword(string Keyword, ulong Address)
        {
            throw new NotImplementedException();
        }

        public void ClearKeyword(string Keyword)
        {
            throw new NotImplementedException();
        }

        public void ClearGlobalKeyword(string Keyword)
        {
            throw new NotImplementedException();
        }

        public void ClearAllKeywords()
        {
            throw new NotImplementedException();
        }

        public void ClearAllGlobalKeywords()
        {
            throw new NotImplementedException();
        }

        public int GetAssemblySize(string Assembly)
        {
            throw new NotImplementedException();
        }

        public void SaveMemory(ulong Address, int Size = 0)
        {
            throw new NotImplementedException();
        }

    } // End interface

} // End namespace