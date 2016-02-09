using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Modules;
using SharpDisasm;
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
        private MemoryEditor MemoryEditor;

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

        public void UpdateMemoryEditor(MemoryEditor MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        private String ResolveKeywords(String Assembly)
        {
            if (Assembly == null)
                return String.Empty;

            Assembly = Assembly.Replace("\t", "");

            // Resolve keywords
            foreach (KeyValuePair<String, String> Keyword in Keywords)
                Assembly = Assembly.Replace(Keyword.Key, Keyword.Value);

            foreach (KeyValuePair<String, String> GlobalKeyword in GlobalKeywords)
                Assembly = Assembly.Replace(GlobalKeyword.Key, GlobalKeyword.Value);

            // Resolve module names
            IEnumerable<RemoteModule> Modules = MemoryEditor.Modules.RemoteModules;

            foreach (RemoteModule Module in Modules)
                Assembly = Assembly.Replace(Module.Name, "0x" + Conversions.ToAddress(unchecked((UInt64)Module.BaseAddress)));

            return Assembly;
        }

        private Byte[] GetInstructions(UInt64 Address)
        {
            const Int32 Largestx86InstructionSize = 15;

            // Read original bytes at code cave jump
            Boolean ReadSuccess;
            // TODO Math.Min(Largestx86InstructionSize, PageEndAddress - Address);
            Byte[] OriginalBytes = MemoryEditor.Read<Byte>((IntPtr)Address, Largestx86InstructionSize, out ReadSuccess);

            if (!ReadSuccess || OriginalBytes == null || OriginalBytes.Length <= 0)
                return null;

            // TODO: Offload IsProcecss64Bit to OSInterface (can write a Process extension method too)
            // Grab instructions at code entry point
            List<Instruction> Instructions = MemoryEditor.Assembly.Disassembler.Disassemble(OriginalBytes, ProcessSelector.IsProcess32Bit(MemoryEditor.Native.Handle), Address);

            // Determine size of instructions we need to overwrite
            Int32 ReplacedInstructionSize = 0;
            foreach (Instruction Instruction in Instructions)
            {
                ReplacedInstructionSize += Instruction.Length;
                if (ReplacedInstructionSize >= JumpSize)
                    break;
            }

            if (ReplacedInstructionSize < JumpSize)
                return null;

            // Truncate to only the bytes we will need to save
            OriginalBytes = OriginalBytes.LargestSubArray(0, ReplacedInstructionSize);

            return OriginalBytes;
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
            Assembly = ResolveKeywords(Assembly);

            Byte[] Bytes = MemoryEditor.Assembly.Assembler.Assemble(ProcessSelector.IsProcess32Bit(MemoryEditor.Native.Handle), Assembly, IntPtr.Zero);
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
            Assembly = ResolveKeywords(Assembly);

            Int32 Size = GetAssemblySize(Assembly);

            // Allocate memory
            RemoteAllocation RemoteAllocation = MemoryEditor.Memory.Allocate(Size);
            RemoteAllocations.Add(RemoteAllocation);
            UInt64 Result = unchecked((UInt64)(Int64)RemoteAllocation.BaseAddress);

            // Write injected code to new page
            MemoryEditor.Assembly.Inject(ProcessSelector.IsProcess32Bit(MemoryEditor.Native.Handle), Assembly, RemoteAllocation.BaseAddress);

            // Gather the original bytes
            Byte[] OriginalBytes = GetInstructions(Entry);

            if (OriginalBytes == null || OriginalBytes.Length < JumpSize)
                return Result;

            // Determine number of no-ops to fill dangling bytes
            string NoOps = "db " + String.Join(" ", Enumerable.Repeat("0x90", OriginalBytes.Length - JumpSize));

            // Write in the jump to the code cave
            MemoryEditor.Assembly.Inject(ProcessSelector.IsProcess32Bit(MemoryEditor.Native.Handle), "jmp " + "0x" + Result.ToString("X") + "\n" + NoOps, unchecked((IntPtr)Entry));

            // Save this code cave for later deallocation
            CodeCave CodeCave = new CodeCave(RemoteAllocation, OriginalBytes, Entry);
            CodeCaves.Add(CodeCave);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString("X") + " (" + Size.ToString() + ")");
            return Result;
        }

        public UInt64 GetCaveExitAddress(UInt64 Address)
        {
            Byte[] OriginalBytes = GetInstructions(Address);
            UInt64 OriginalByteSize;

            if (OriginalBytes != null || OriginalBytes.Length < JumpSize)
            {
                // Determine the size of the minimum number of instructions we will be overwriting
                OriginalByteSize = (UInt64)OriginalBytes.Length;
            }
            else
            {
                // Fall back if something goes wrong
                OriginalByteSize = JumpSize;
            }

            UInt64 Result = Address + OriginalByteSize;

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
                    MemoryEditor.Write<Byte[]>((IntPtr)CodeCave.Entry, CodeCave.OriginalBytes);
                    MemoryEditor.Memory.Deallocate(CodeCave.RemoteAllocation);
                }
            }
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Result == true ? "(success)" : "(failed)"));
        }

        public void RemoveAllCodeCaves()
        {
            foreach (CodeCave CodeCave in CodeCaves)
            {
                MemoryEditor.WriteBytes((IntPtr)CodeCave.Entry, CodeCave.OriginalBytes);
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