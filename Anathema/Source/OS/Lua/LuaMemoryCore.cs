using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public interface LuaFunctions
    {
        UInt64 GetModuleAddress(String ModuleName);
        UInt64 GetCaveExitAddress(UInt64 Address);

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

        private List<RemoteAllocation> RemoteAllocations;

        public LuaMemoryCore()
        {
            RemoteAllocations = new List<RemoteAllocation>();

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
            UInt64 Result = 0;
            foreach (RemoteModule Module in MemoryEditor.Modules.RemoteModules)
            {
                if (Module.Name.ToLower() == ModuleName.ToLower())
                {
                    Result = unchecked((UInt64)(Int64)Module.BaseAddress);
                    break;
                }
            }
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString());
            return Result;
        }

        public UInt64 GetCaveExitAddress(UInt64 Address)
        {
            UInt64 Result = Address + 5;

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString());
            return Result;
        }

        public UInt64 AllocateMemory(Int32 Size)
        {
            RemoteAllocation RemoteAllocation = MemoryEditor.Memory.Allocate(Size);
            RemoteAllocations.Add(RemoteAllocation);

            UInt64 Result = unchecked((UInt64)(Int64)RemoteAllocation.BaseAddress);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString());
            return Result;
        }

        public void DeallocateMemory(UInt64 Address)
        {
            Boolean Result = false;

            foreach (RemoteAllocation RemoteAllocation in RemoteAllocations)
            {
                if (unchecked((UInt64)(Int64)RemoteAllocation.BaseAddress) == Address)
                {
                    Result = true;
                    MemoryEditor.Memory.Deallocate(RemoteAllocation);
                    break;
                }
            }

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Result == true ? "(success)" : "(failed)"));
            return;
        }

        public Int32 GetAssemblySize(String Assembly)
        {
            Byte[] Bytes = MemoryEditor.Assembly.Assembler.Assemble(Assembly);
            Int32 Result = (Bytes == null ? 0 : Bytes.Length);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result);
            return Result;
        }

        public Byte[] CreateCodeCave() { return null; }
        public void SaveMemory(UInt64 Address, Int32 Size = 0)
        {

        }

        public void RestoreMemory(UInt64 Address)
        {

        }

        public void RestoreAllMemory()
        {

        }

        public void SetKeyword(String Keyword, UInt64 Address)
        {

        }

        public void SetGlobalKeyword(String Keyword, UInt64 Address)
        {

        }

        public void ClearKeyword(String Keyword)
        {

        }

        public void ClearGlobalKeyword(String Keyword)
        {

        }

        public void ClearAllKeywords()
        {

        }

        public void ClearAllGlobalKeywords()
        {

        }

    } // End interface

} // End namespace