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
        IntPtr GetModuleAddress(String ModuleName);
        Int32 GetAssemblySize(String Assembly);

        // Allocations
        IntPtr AllocateMemory(Int32 Size);
        void DeallocateMemory(IntPtr Address);
        void DeallocateAllMemory();

        // Code caves
        IntPtr CreateCodeCave(IntPtr Entry, String Assembly);
        IntPtr GetCaveExitAddress(IntPtr Address);
        void RemoveCodeCave(IntPtr Address);
        void RemoveAllCodeCaves();

        // Keywords
        void SetKeyword(String Keyword, IntPtr Address);
        void SetGlobalKeyword(String Keyword, IntPtr Address);
        void ClearKeyword(String Keyword);
        void ClearGlobalKeyword(String Keyword);
        void ClearAllKeywords();
        void ClearAllGlobalKeywords();

        // Reading
        SByte ReadSByte(IntPtr Address);
        Byte ReadByte(IntPtr Address);
        Int16 ReadInt16(IntPtr Address);
        Int32 ReadInt32(IntPtr Address);
        Int64 ReadInt64(IntPtr Address);
        UInt16 ReadUInt16(IntPtr Address);
        UInt32 ReadUInt32(IntPtr Address);
        UInt64 ReadUInt64(IntPtr Address);
        Single ReadSingle(IntPtr Address);
        Double ReadDouble(IntPtr Address);
        Byte[] ReadBytes(IntPtr Address, Int32 Count);

        // Writing
        void WriteSByte(IntPtr Address, SByte Value);
        void WriteByte(IntPtr Address, Byte Value);
        void WriteInt16(IntPtr Address, Int16 Value);
        void WriteInt32(IntPtr Address, Int32 Value);
        void WriteInt64(IntPtr Address, Int64 Value);
        void WriteUInt16(IntPtr Address, UInt16 Value);
        void WriteUInt32(IntPtr Address, UInt32 Value);
        void WriteUInt64(IntPtr Address, UInt64 Value);
        void WriteSingle(IntPtr Address, Single Value);
        void WriteDouble(IntPtr Address, Double Value);
        void WriteBytes(IntPtr Address, Byte[] Values);

    } // End interface

    public class LuaMemoryCore : LuaFunctions, IProcessObserver
    {
        private OSInterface OSInterface;

        private static ConcurrentDictionary<String, String> GlobalKeywords = new ConcurrentDictionary<String, String>();
        private Dictionary<String, String> Keywords;
        private List<IntPtr> RemoteAllocations;
        private List<CodeCave> CodeCaves;

        private const Int32 JumpSize = 5;

        private struct CodeCave
        {
            public Byte[] OriginalBytes;
            public IntPtr RemoteAllocation;
            public IntPtr Entry;

            public CodeCave(IntPtr RemoteAllocation, Byte[] OriginalBytes, IntPtr Entry)
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

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;
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
            IEnumerable<NormalizedModule> Modules = OSInterface.Process.GetModules();

            foreach (NormalizedModule Module in Modules)
                Assembly = Assembly.Replace(Module.Name, "0x" + Conversions.ToAddress(Module.BaseAddress.ToUInt64()));

            return Assembly;
        }

        private Byte[] GetInstructions(IntPtr Address)
        {
            const Int32 Largestx86InstructionSize = 15;

            // Read original bytes at code cave jump
            Boolean ReadSuccess;
            // TODO Math.Min(Largestx86InstructionSize, PageEndAddress - Address);
            Byte[] OriginalBytes = OSInterface.Process.ReadBytes(Address, Largestx86InstructionSize, out ReadSuccess);

            if (!ReadSuccess || OriginalBytes == null || OriginalBytes.Length <= 0)
                return null;

            // TODO: Offload IsProcecss64Bit to OSInterface (can write a Process extension method too)
            // Grab instructions at code entry point
            List<Instruction> Instructions = OSInterface.Architecture.Disassembler.Disassemble(OriginalBytes, OSInterface.Process.Is32Bit(), Address);

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

        public IntPtr GetModuleAddress(String ModuleName)
        {
            IntPtr Result = IntPtr.Zero;
            foreach (NormalizedModule Module in OSInterface.Process.GetModules())
            {
                if (Module.Name.ToLower() == ModuleName.ToLower())
                {
                    Result = Module.BaseAddress;
                    break;
                }
            }

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString("X"));
            return Result;
        }

        public Int32 GetAssemblySize(String Assembly)
        {
            Assembly = ResolveKeywords(Assembly);

            Byte[] Bytes = OSInterface.Architecture.Assembler.Assemble(OSInterface.Process.Is32Bit(), Assembly, IntPtr.Zero);
            Int32 Result = (Bytes == null ? 0 : Bytes.Length);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result + "B");
            return Result;
        }

        public IntPtr AllocateMemory(Int32 Size)
        {
            IntPtr Address = OSInterface.Process.AllocateMemory(Size);
            RemoteAllocations.Add(Address);
            
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Conversions.ToAddress(Address) + " (" + Size.ToString() + ")");
            return Address;
        }

        public void DeallocateMemory(IntPtr Address)
        {
            Boolean Result = false;

            foreach (IntPtr AllocationAddress in RemoteAllocations)
            {
                if (AllocationAddress == Address)
                {
                    Result = true;
                    OSInterface.Process.DeallocateMemory(AllocationAddress);
                    RemoteAllocations.Remove(AllocationAddress);
                    break;
                }
            }

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Conversions.ToAddress(Address) + " " + (Result == true ? "(success)" : "(failed)"));
            return;
        }

        public void DeallocateAllMemory()
        {
            foreach (IntPtr Address in RemoteAllocations)
                OSInterface.Process.DeallocateMemory(Address);

            RemoteAllocations.Clear();

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public IntPtr CreateCodeCave(IntPtr Entry, String Assembly)
        {
            Assembly = ResolveKeywords(Assembly);

            Int32 Size = GetAssemblySize(Assembly);

            // Allocate memory
            IntPtr RemoteAllocation = OSInterface.Process.AllocateMemory(Size);
            RemoteAllocations.Add(RemoteAllocation);

            // Write injected code to new page
            Byte[] InjectionBytes = OSInterface.Architecture.Assembler.Assemble(OSInterface.Process.Is32Bit(), Assembly, RemoteAllocation);
            OSInterface.Process.WriteBytes(RemoteAllocation, InjectionBytes);

            // Gather the original bytes
            Byte[] OriginalBytes = GetInstructions(Entry);

            if (OriginalBytes == null || OriginalBytes.Length < JumpSize)
                return RemoteAllocation;

            // Determine number of no-ops to fill dangling bytes
            string NoOps = (OriginalBytes.Length - JumpSize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", OriginalBytes.Length - JumpSize)).TrimEnd(',');

            // Write in the jump to the code cave
            String CodeCaveJump = "jmp " + "0x" + Conversions.ToAddress(RemoteAllocation) + "\n" + NoOps;
            Byte[] JumpBytes = OSInterface.Architecture.Assembler.Assemble(OSInterface.Process.Is32Bit(), CodeCaveJump, Entry);
            OSInterface.Process.WriteBytes(Entry, JumpBytes);

            // Save this code cave for later deallocation
            CodeCave CodeCave = new CodeCave(RemoteAllocation, OriginalBytes, Entry);
            CodeCaves.Add(CodeCave);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + RemoteAllocation.ToString("X") + " (" + Size.ToString() + ")");
            return RemoteAllocation;
        }

        public IntPtr GetCaveExitAddress(IntPtr Address)
        {
            Byte[] OriginalBytes = GetInstructions(Address);
            Int32 OriginalByteSize;

            if (OriginalBytes != null || OriginalBytes.Length < JumpSize)
            {
                // Determine the size of the minimum number of instructions we will be overwriting
                OriginalByteSize = OriginalBytes.Length;
            }
            else
            {
                // Fall back if something goes wrong
                OriginalByteSize = JumpSize;
            }

            IntPtr Result = Address.Add(OriginalByteSize);

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Result.ToString("X"));
            return Result;
        }

        public void RemoveCodeCave(IntPtr Address)
        {
            Boolean Result = false;
            foreach (CodeCave CodeCave in CodeCaves)
            {
                if (CodeCave.Entry == Address)
                {
                    Result = true;
                    OSInterface.Process.Write<Byte[]>(CodeCave.Entry, CodeCave.OriginalBytes);
                    OSInterface.Process.DeallocateMemory(CodeCave.RemoteAllocation);
                }
            }
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Result == true ? "(success)" : "(failed)"));
        }

        public void RemoveAllCodeCaves()
        {
            foreach (CodeCave CodeCave in CodeCaves)
            {
                OSInterface.Process.WriteBytes(CodeCave.Entry, CodeCave.OriginalBytes);
                OSInterface.Process.DeallocateMemory(CodeCave.RemoteAllocation);
            }
            CodeCaves.Clear();

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void SetKeyword(String Keyword, IntPtr Address)
        {
            Keywords[Keyword] = "0x" + Address.ToString("X");

            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + Keyword + " => " + Keywords[Keyword]);
        }

        public void SetGlobalKeyword(String GlobalKeyword, IntPtr Address)
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

        public SByte ReadSByte(IntPtr Address)
        {
            Boolean Success;
            SByte Result = OSInterface.Process.Read<SByte>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Byte ReadByte(IntPtr Address)
        {
            Boolean Success;
            Byte Result = OSInterface.Process.Read<Byte>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Int16 ReadInt16(IntPtr Address)
        {
            Boolean Success;
            Int16 Result = OSInterface.Process.Read<Int16>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Int32 ReadInt32(IntPtr Address)
        {
            Boolean Success;
            Int32 Result = OSInterface.Process.Read<Int32>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Int64 ReadInt64(IntPtr Address)
        {
            Boolean Success;
            Int64 Result = OSInterface.Process.Read<Int64>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public UInt16 ReadUInt16(IntPtr Address)
        {
            Boolean Success;
            UInt16 Result = OSInterface.Process.Read<UInt16>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public UInt32 ReadUInt32(IntPtr Address)
        {
            Boolean Success;
            UInt32 Result = OSInterface.Process.Read<UInt32>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public UInt64 ReadUInt64(IntPtr Address)
        {
            Boolean Success;
            UInt64 Result = OSInterface.Process.Read<UInt64>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Single ReadSingle(IntPtr Address)
        {
            Boolean Success;
            Single Result = OSInterface.Process.Read<Single>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Double ReadDouble(IntPtr Address)
        {
            Boolean Success;
            UInt64 Result = OSInterface.Process.Read<UInt64>(Address, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        public Byte[] ReadBytes(IntPtr Address, Int32 Count)
        {
            Boolean Success;
            Byte[] Result = OSInterface.Process.ReadBytes(Address, Count, out Success);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name + " " + (Success == true ? "(success)" : "(failed)"));
            return Result;
        }

        // Writing
        public void WriteSByte(IntPtr Address, SByte Value)
        {
            OSInterface.Process.Write<SByte>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteByte(IntPtr Address, Byte Value)
        {
            OSInterface.Process.Write<Byte>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteInt16(IntPtr Address, Int16 Value)
        {
            OSInterface.Process.Write<Int16>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteInt32(IntPtr Address, Int32 Value)
        {
            OSInterface.Process.Write<Int32>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteInt64(IntPtr Address, Int64 Value)
        {
            OSInterface.Process.Write<Int64>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteUInt16(IntPtr Address, UInt16 Value)
        {
            OSInterface.Process.Write<UInt16>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteUInt32(IntPtr Address, UInt32 Value)
        {
            OSInterface.Process.Write<UInt32>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteUInt64(IntPtr Address, UInt64 Value)
        {
            OSInterface.Process.Write<UInt64>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteSingle(IntPtr Address, Single Value)
        {
            OSInterface.Process.Write<Single>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteDouble(IntPtr Address, Double Value)
        {
            OSInterface.Process.Write<Double>(Address, Value);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

        public void WriteBytes(IntPtr Address, Byte[] Values)
        {
            OSInterface.Process.WriteBytes(Address, Values);
            Console.WriteLine("[LUA] " + MethodBase.GetCurrentMethod().Name);
        }

    } // End interface

} // End namespace