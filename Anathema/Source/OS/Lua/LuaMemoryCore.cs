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
        Int32 GetAssemblySize(String Assembly);

        UInt64 AllocateMemory(Int32 Size);
        void DeallocateMemory(UInt64 Address);

        UInt64 CreateCodeCave(UInt64 Entry, String Assembly);
        UInt64 GetCaveExitAddress(UInt64 Address);

        void RemoveCodeCave(UInt64 Address);
        void RemoveAllCodeCaves();

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
        private static Dictionary<String, String> GlobalKeywords;
        private Dictionary<String, String> Keywords;

        public LuaMemoryCore()
        {
            RemoteAllocations = new List<RemoteAllocation>();
            Keywords = new Dictionary<String, String>();

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

        private String ReplaceKeywords(String Assembly)
        {
            foreach (KeyValuePair<String, String> Keyword in Keywords)
                Assembly = Assembly.Replace(Keyword.Key, Keyword.Value);

            foreach (KeyValuePair<String, String> GlobalKeyword in GlobalKeywords)
                Assembly = Assembly.Replace(GlobalKeyword.Key, GlobalKeyword.Value);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
            return Assembly;
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

        public Int32 GetAssemblySize(String Assembly)
        {
            Assembly = ReplaceKeywords(Assembly);

            Byte[] Bytes = MemoryEditor.Assembly.Assembler.Assemble(Assembly);
            Int32 Result = (Bytes == null ? 0 : Bytes.Length);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result);
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

        public UInt64 CreateCodeCave(UInt64 Entry, String Assembly)
        {
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
            return 0;
        }

        public UInt64 GetCaveExitAddress(UInt64 Address)
        {
            UInt64 Result = Address + 5;

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString());
            return Result;
        }

        public void RemoveCodeCave(UInt64 Address)
        {

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void RemoveAllCodeCaves()
        {

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void SetKeyword(String Keyword, UInt64 Address)
        {
            Keywords[Keyword] = "0x" + Address.ToString("X");

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Keywords[Keyword]);
        }

        public void SetGlobalKeyword(String GlobalKeyword, UInt64 Address)
        {
            GlobalKeywords[GlobalKeyword] = "0x" + Address.ToString("X");

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + GlobalKeywords[GlobalKeyword]);
        }

        public void ClearKeyword(String Keyword)
        {
            if (Keywords.ContainsKey(Keyword))
                Keywords.Remove(Keyword);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Keyword);
        }

        public void ClearGlobalKeyword(String GlobalKeyword)
        {
            if (GlobalKeywords.ContainsKey(GlobalKeyword))
                GlobalKeywords.Remove(GlobalKeyword);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + GlobalKeyword);
        }

        public void ClearAllKeywords()
        {
            Keywords.Clear();

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void ClearAllGlobalKeywords()
        {
            GlobalKeywords.Clear();

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

    } // End interface

} // End namespace