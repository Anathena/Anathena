using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Modules;
using SharpDisasm;
using System;
using System.Collections.Concurrent;
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
        // General
        UInt64 GetModuleAddress(String ModuleName);
        Int32 GetAssemblySize(String Assembly);

        // Allocations
        UInt64 AllocateMemory(Int32 Size);
        void DeallocateMemory(UInt64 Address);
        void DeallocateAllMemory();

        // Code caves
        UInt64 CreateCodeCave(UInt64 Entry, String Assembly);
        UInt64 GetCaveExitAddress(UInt64 Address);
        void RemoveCodeCave(UInt64 Address);
        void RemoveAllCodeCaves();

        // Keywords
        void SetKeyword(String Keyword, UInt64 Address);
        void SetGlobalKeyword(String Keyword, UInt64 Address);
        void ClearKeyword(String Keyword);
        void ClearGlobalKeyword(String Keyword);
        void ClearAllKeywords();
        void ClearAllGlobalKeywords();

        // Reading
        SByte ReadSByte(UInt64 Address);
        Byte ReadByte(UInt64 Address);
        Int16 ReadInt16(UInt64 Address);
        Int32 ReadInt32(UInt64 Address);
        Int64 ReadInt64(UInt64 Address);
        UInt16 ReadUInt16(UInt64 Address);
        UInt32 ReadUInt32(UInt64 Address);
        UInt64 ReadUInt64(UInt64 Address);
        Single ReadSingle(UInt64 Address);
        Double ReadDouble(UInt64 Address);
        Byte[] ReadBytes(UInt64 Address, Int32 Count);

        // Writing
        void WriteSByte(UInt64 Address, SByte Value);
        void WriteByte(UInt64 Address, Byte Value);
        void WriteInt16(UInt64 Address, Int16 Value);
        void WriteInt32(UInt64 Address, Int32 Value);
        void WriteInt64(UInt64 Address, Int64 Value);
        void WriteUInt16(UInt64 Address, UInt16 Value);
        void WriteUInt32(UInt64 Address, UInt32 Value);
        void WriteUInt64(UInt64 Address, UInt64 Value);
        void WriteSingle(UInt64 Address, Single Value);
        void WriteDouble(UInt64 Address, Double Value);
        void WriteBytes(UInt64 Address, Byte[] Values);

    } // End interface

    public class LuaMemoryCore : LuaFunctions, IProcessObserver
    {
        private MemoryEditor MemoryEditor;

        private static ConcurrentDictionary<String, String> GlobalKeywords = new ConcurrentDictionary<String, String>();
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
            string NoOps = (OriginalBytes.Length - JumpSize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", OriginalBytes.Length - JumpSize)).TrimEnd(',');

            // Write in the jump to the code cave
            String CodeCaveJump = "jmp " + "0x" + Conversions.ToAddress(Result) + "\n" + NoOps;
            MemoryEditor.Assembly.Inject(ProcessSelector.IsProcess32Bit(MemoryEditor.Native.Handle), CodeCaveJump, unchecked((IntPtr)Entry));

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
            String ValueRemoved;
            if (GlobalKeywords.ContainsKey(GlobalKeyword))
                GlobalKeywords.TryRemove(GlobalKeyword, out ValueRemoved);

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

        public SByte ReadSByte(UInt64 Address)
        {
            Boolean Success;
            SByte Result = MemoryEditor.Read<SByte>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Byte ReadByte(UInt64 Address)
        {
            Boolean Success;
            Byte Result = MemoryEditor.Read<Byte>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Int16 ReadInt16(UInt64 Address)
        {
            Boolean Success;
            Int16 Result = MemoryEditor.Read<Int16>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Int32 ReadInt32(UInt64 Address)
        {
            Boolean Success;
            Int32 Result = MemoryEditor.Read<Int32>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Int64 ReadInt64(UInt64 Address)
        {
            Boolean Success;
            Int64 Result = MemoryEditor.Read<Int64>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public UInt16 ReadUInt16(UInt64 Address)
        {
            Boolean Success;
            UInt16 Result = MemoryEditor.Read<UInt16>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public UInt32 ReadUInt32(UInt64 Address)
        {
            Boolean Success;
            UInt32 Result = MemoryEditor.Read<UInt32>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public UInt64 ReadUInt64(UInt64 Address)
        {
            Boolean Success;
            UInt64 Result = MemoryEditor.Read<UInt64>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Single ReadSingle(UInt64 Address)
        {
            Boolean Success;
            Single Result = MemoryEditor.Read<Single>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Double ReadDouble(UInt64 Address)
        {
            Boolean Success;
            UInt64 Result = MemoryEditor.Read<UInt64>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Byte[] ReadBytes(UInt64 Address, Int32 Count)
        {
            Boolean Success;
            Byte[] Result = MemoryEditor.ReadBytes(unchecked((IntPtr)Address), Count, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        // Writing
        public void WriteSByte(UInt64 Address, SByte Value)
        {
            MemoryEditor.Write<SByte>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteByte(UInt64 Address, Byte Value)
        {
            MemoryEditor.Write<Byte>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteInt16(UInt64 Address, Int16 Value)
        {
            MemoryEditor.Write<Int16>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteInt32(UInt64 Address, Int32 Value)
        {
            MemoryEditor.Write<Int32>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteInt64(UInt64 Address, Int64 Value)
        {
            MemoryEditor.Write<Int64>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteUInt16(UInt64 Address, UInt16 Value)
        {
            MemoryEditor.Write<UInt16>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteUInt32(UInt64 Address, UInt32 Value)
        {
            MemoryEditor.Write<UInt32>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteUInt64(UInt64 Address, UInt64 Value)
        {
            MemoryEditor.Write<UInt64>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteSingle(UInt64 Address, Single Value)
        {
            MemoryEditor.Write<Single>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteDouble(UInt64 Address, Double Value)
        {
            MemoryEditor.Write<Double>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteBytes(UInt64 Address, Byte[] Values)
        {
            MemoryEditor.WriteBytes(unchecked((IntPtr)Address), Values);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

    } // End interface

} // End namespace