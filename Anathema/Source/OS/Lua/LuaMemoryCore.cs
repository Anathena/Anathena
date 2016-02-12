using Anathema.MemoryManagement;
using Anathema.MemoryManagement.Memory;
using Anathema.MemoryManagement.Modules;
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
        private OSInterface MemoryEditor;

        private static ConcurrentDictionary<String, String> GlobalKeywords = new ConcurrentDictionary<String, String>();
        private Dictionary<String, String> Keywords;
        private List<IntPtr> RemoteAllocations;
        private List<CodeCave> CodeCaves;

        private const Int32 JumpSize = 5;

        private struct CodeCave
        {
            public IntPtr RemoteAllocation;
            public Byte[] OriginalBytes;
            public UInt64 Entry;

            public CodeCave(IntPtr RemoteAllocation, Byte[] OriginalBytes, UInt64 Entry)
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
            RemoteAllocations = new List<IntPtr>();
            Keywords = new Dictionary<String, String>();
            CodeCaves = new List<CodeCave>();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(OSInterface MemoryEditor)
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
            IEnumerable<NormalizedModule> Modules = MemoryEditor.Process.GetModules();

            foreach (NormalizedModule Module in Modules)
                Assembly = Assembly.Replace(Module.Name, "0x" + Conversions.ToAddress(Module.BaseAddress.ToUInt64()));

            return Assembly;
        }

        private Byte[] GetInstructions(UInt64 Address)
        {
            const Int32 Largestx86InstructionSize = 15;

            // Read original bytes at code cave jump
            Boolean ReadSuccess;
            // TODO Math.Min(Largestx86InstructionSize, PageEndAddress - Address);
            Byte[] OriginalBytes = MemoryEditor.Process.ReadBytes((IntPtr)Address, Largestx86InstructionSize, out ReadSuccess);

            if (!ReadSuccess || OriginalBytes == null || OriginalBytes.Length <= 0)
                return null;

            // TODO: Offload IsProcecss64Bit to OSInterface (can write a Process extension method too)
            // Grab instructions at code entry point
            List<Instruction> Instructions = MemoryEditor.Architecture.Disassembler.Disassemble(OriginalBytes, MemoryEditor.Process.Is32Bit(), Address);

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
            foreach (NormalizedModule Module in MemoryEditor.Process.GetModules())
            {
                if (Module.Name.ToLower() == ModuleName.ToLower())
                {
                    Result = Module.BaseAddress.ToUInt64();
                    break;
                }
            }

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString("X"));
            return Result;
        }

        public Int32 GetAssemblySize(String Assembly)
        {
            Assembly = ResolveKeywords(Assembly);

            Byte[] Bytes = MemoryEditor.Architecture.Assembler.Assemble(MemoryEditor.Process.Is32Bit(), Assembly, IntPtr.Zero);
            Int32 Result = (Bytes == null ? 0 : Bytes.Length);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result + "B");
            return Result;
        }

        public UInt64 AllocateMemory(Int32 Size)
        {
            IntPtr RemoteAllocation = MemoryEditor.Process.AllocateMemory(Size);
            RemoteAllocations.Add(RemoteAllocation);

            UInt64 Result = RemoteAllocation.ToUInt64();
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString("X") + " (" + Size.ToString() + ")");
            return Result;
        }

        public void DeallocateMemory(UInt64 Address)
        {
            Boolean Result = false;

            foreach (IntPtr RemoteAllocation in RemoteAllocations)
            {
                if (RemoteAllocation.ToUInt64() == Address)
                {
                    Result = true;
                    MemoryEditor.Process.DeallocateMemory(RemoteAllocation);
                    RemoteAllocations.Remove(RemoteAllocation);
                    break;
                }
            }

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Result == true ? "(success)" : "(failed)"));
            return;
        }

        public void DeallocateAllMemory()
        {
            foreach (IntPtr RemoteAllocation in RemoteAllocations)
                MemoryEditor.Process.DeallocateMemory(RemoteAllocation);

            RemoteAllocations.Clear();

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public UInt64 CreateCodeCave(UInt64 Entry, String Assembly)
        {
            Assembly = ResolveKeywords(Assembly);

            Int32 Size = GetAssemblySize(Assembly);

            // Allocate memory
            IntPtr RemoteAllocation = MemoryEditor.Process.AllocateMemory(Size);
            RemoteAllocations.Add(RemoteAllocation);
            UInt64 Result = RemoteAllocation.ToUInt64();

            // Write injected code to new page
            Byte[] InjectionBytes = MemoryEditor.Architecture.Assembler.Assemble(MemoryEditor.Process.Is32Bit(), Assembly, RemoteAllocation);
            MemoryEditor.Process.WriteBytes(RemoteAllocation, InjectionBytes);

            // Gather the original bytes
            Byte[] OriginalBytes = GetInstructions(Entry);

            if (OriginalBytes == null || OriginalBytes.Length < JumpSize)
                return Result;

            // Determine number of no-ops to fill dangling bytes
            string NoOps = (OriginalBytes.Length - JumpSize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", OriginalBytes.Length - JumpSize)).TrimEnd(',');

            // Write in the jump to the code cave
            String CodeCaveJump = "jmp " + "0x" + Conversions.ToAddress(Result) + "\n" + NoOps;
            Byte[] JumpBytes = MemoryEditor.Architecture.Assembler.Assemble(MemoryEditor.Process.Is32Bit(), CodeCaveJump, unchecked((IntPtr)Entry));
            MemoryEditor.Process.WriteBytes(unchecked((IntPtr)Entry), JumpBytes);

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
                    MemoryEditor.Process.Write<Byte[]>((IntPtr)CodeCave.Entry, CodeCave.OriginalBytes);
                    MemoryEditor.Process.DeallocateMemory(CodeCave.RemoteAllocation);
                }
            }
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Result == true ? "(success)" : "(failed)"));
        }

        public void RemoveAllCodeCaves()
        {
            foreach (CodeCave CodeCave in CodeCaves)
            {
                MemoryEditor.Process.WriteBytes((IntPtr)CodeCave.Entry, CodeCave.OriginalBytes);
                MemoryEditor.Process.DeallocateMemory(CodeCave.RemoteAllocation);
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
            SByte Result = MemoryEditor.Process.Read<SByte>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Byte ReadByte(UInt64 Address)
        {
            Boolean Success;
            Byte Result = MemoryEditor.Process.Read<Byte>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Int16 ReadInt16(UInt64 Address)
        {
            Boolean Success;
            Int16 Result = MemoryEditor.Process.Read<Int16>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Int32 ReadInt32(UInt64 Address)
        {
            Boolean Success;
            Int32 Result = MemoryEditor.Process.Read<Int32>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Int64 ReadInt64(UInt64 Address)
        {
            Boolean Success;
            Int64 Result = MemoryEditor.Process.Read<Int64>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public UInt16 ReadUInt16(UInt64 Address)
        {
            Boolean Success;
            UInt16 Result = MemoryEditor.Process.Read<UInt16>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public UInt32 ReadUInt32(UInt64 Address)
        {
            Boolean Success;
            UInt32 Result = MemoryEditor.Process.Read<UInt32>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public UInt64 ReadUInt64(UInt64 Address)
        {
            Boolean Success;
            UInt64 Result = MemoryEditor.Process.Read<UInt64>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Single ReadSingle(UInt64 Address)
        {
            Boolean Success;
            Single Result = MemoryEditor.Process.Read<Single>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Double ReadDouble(UInt64 Address)
        {
            Boolean Success;
            UInt64 Result = MemoryEditor.Process.Read<UInt64>(unchecked((IntPtr)Address), out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Byte[] ReadBytes(UInt64 Address, Int32 Count)
        {
            Boolean Success;
            Byte[] Result = MemoryEditor.Process.ReadBytes(unchecked((IntPtr)Address), Count, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        // Writing
        public void WriteSByte(UInt64 Address, SByte Value)
        {
            MemoryEditor.Process.Write<SByte>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteByte(UInt64 Address, Byte Value)
        {
            MemoryEditor.Process.Write<Byte>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteInt16(UInt64 Address, Int16 Value)
        {
            MemoryEditor.Process.Write<Int16>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteInt32(UInt64 Address, Int32 Value)
        {
            MemoryEditor.Process.Write<Int32>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteInt64(UInt64 Address, Int64 Value)
        {
            MemoryEditor.Process.Write<Int64>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteUInt16(UInt64 Address, UInt16 Value)
        {
            MemoryEditor.Process.Write<UInt16>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteUInt32(UInt64 Address, UInt32 Value)
        {
            MemoryEditor.Process.Write<UInt32>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteUInt64(UInt64 Address, UInt64 Value)
        {
            MemoryEditor.Process.Write<UInt64>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteSingle(UInt64 Address, Single Value)
        {
            MemoryEditor.Process.Write<Single>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteDouble(UInt64 Address, Double Value)
        {
            MemoryEditor.Process.Write<Double>(unchecked((IntPtr)Address), Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteBytes(UInt64 Address, Byte[] Values)
        {
            MemoryEditor.Process.WriteBytes(unchecked((IntPtr)Address), Values);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

    } // End interface

} // End namespace