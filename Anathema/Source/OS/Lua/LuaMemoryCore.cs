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
        void DeallocateAllMemory();

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

        private static Dictionary<String, String> GlobalKeywords = new Dictionary<String, String>();
        private Dictionary<String, String> Keywords;
        private List<RemoteAllocation> RemoteAllocations;
        private List<CodeCave> CodeCaves;

        private const Int32 JumpSize = 5;

        private struct CodeCave
        {
            public RemoteAllocation RemoteAllocation;
            public Byte[] OriginalBytes;
            public UInt64 Entry;

            public CodeCave(RemoteAllocation RemoteAllocation, Byte[] OriginalBytes, UInt64 Entry)
            {
                this.RemoteAllocation = RemoteAllocation;
                this.OriginalBytes = OriginalBytes;
                this.Entry = Entry;
            }
        }

        public LuaMemoryCore()
        {
            InitializeProcessObserver();
        }

        public void Initialize()
        {
            // Reinitialize local object collections. If the user has not deallocated all allocated memory, that is on them.
            RemoteAllocations = new List<RemoteAllocation>();
            Keywords = new Dictionary<String, String>();
            CodeCaves = new List<CodeCave>();
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
            if (Assembly == null)
                return String.Empty;

            Assembly = Assembly.Replace("\t", "");

            foreach (KeyValuePair<String, String> Keyword in Keywords)
                Assembly = Assembly.Replace(Keyword.Key, Keyword.Value);

            foreach (KeyValuePair<String, String> GlobalKeyword in GlobalKeywords)
                Assembly = Assembly.Replace(GlobalKeyword.Key, GlobalKeyword.Value);

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
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString("X"));
            return Result;
        }

        public Int32 GetAssemblySize(String Assembly)
        {
            Assembly = ReplaceKeywords(Assembly);

            Byte[] Bytes = MemoryEditor.Assembly.Assembler.Assemble(Assembly);
            Int32 Result = (Bytes == null ? 0 : Bytes.Length);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result + "B");
            return Result;
        }

        public UInt64 AllocateMemory(Int32 Size)
        {
            RemoteAllocation RemoteAllocation = MemoryEditor.Memory.Allocate(Size);
            RemoteAllocations.Add(RemoteAllocation);

            UInt64 Result = unchecked((UInt64)(Int64)RemoteAllocation.BaseAddress);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString("X") + " (" + Size.ToString() + ")");
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
                    RemoteAllocations.Remove(RemoteAllocation);
                    break;
                }
            }

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Result == true ? "(success)" : "(failed)"));
            return;
        }

        public void DeallocateAllMemory()
        {
            foreach (RemoteAllocation RemoteAllocation in RemoteAllocations)
            {
                MemoryEditor.Memory.Deallocate(RemoteAllocation);
            }
            RemoteAllocations.Clear();

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public UInt64 CreateCodeCave(UInt64 Entry, String Assembly)
        {
            Assembly = ReplaceKeywords(Assembly);

            Int32 Size = GetAssemblySize(Assembly);

            // Allocate memory
            RemoteAllocation RemoteAllocation = MemoryEditor.Memory.Allocate(Size);
            RemoteAllocations.Add(RemoteAllocation);
            UInt64 Result = unchecked((UInt64)(Int64)RemoteAllocation.BaseAddress);

            // Write injected code
            MemoryEditor.Assembly.Inject(Assembly, RemoteAllocation.BaseAddress);

            // Read original bytes at code cave jump
            Boolean ReadSuccess;
            Byte[] OriginalBytes = MemoryEditor.Read<Byte>((IntPtr)Entry, JumpSize, out ReadSuccess, false);

            // Write in the jump to the code cave
            MemoryEditor.Assembly.Inject("jmp " + "0x" + Result.ToString("X"), unchecked((IntPtr)Entry));

            // Save this code cave for later deallocation
            CodeCave CodeCave = new CodeCave(RemoteAllocation, OriginalBytes, Entry);
            CodeCaves.Add(CodeCave);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString("X") + " (" + Size.ToString() + ")");
            return Result;
        }

        public UInt64 GetCaveExitAddress(UInt64 Address)
        {
            UInt64 Result = Address + JumpSize;

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString("X"));
            return Result;
        }

        public void RemoveCodeCave(UInt64 Address)
        {
            Boolean Result = false;
            foreach (CodeCave CodeCave in CodeCaves)
            {
                if (CodeCave.Entry == Address)
                {
                    Result = true;
                    MemoryEditor.Write<Byte[]>((IntPtr)CodeCave.Entry, CodeCave.OriginalBytes, false);
                    MemoryEditor.Memory.Deallocate(CodeCave.RemoteAllocation);
                }
            }
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Result == true ? "(success)" : "(failed)"));
        }

        public void RemoveAllCodeCaves()
        {
            foreach (CodeCave CodeCave in CodeCaves)
            {
                MemoryEditor.WriteBytes((IntPtr)CodeCave.Entry, CodeCave.OriginalBytes, false);
                MemoryEditor.Memory.Deallocate(CodeCave.RemoteAllocation);
            }
            CodeCaves.Clear();

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void SetKeyword(String Keyword, UInt64 Address)
        {
            Keywords[Keyword] = "0x" + Address.ToString("X");

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Keyword + " => " + Keywords[Keyword]);
        }

        public void SetGlobalKeyword(String GlobalKeyword, UInt64 Address)
        {
            GlobalKeywords[GlobalKeyword] = "0x" + Address.ToString("X");

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + GlobalKeyword + " => " + GlobalKeywords[GlobalKeyword]);
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