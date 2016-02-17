using SharpDisasm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Anathema
{
    public interface LuaFunctions
    {
        // General
        IntPtr GetModuleAddress(String ModuleName);
        Int32 GetAssemblySize(String Assembly);
        Byte[] GetAssemblyBytes(String Assembly, IntPtr Address);
        Byte[] GetInstructionBytes(IntPtr Address, Int32 MinimumInstructionBytes);

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

        // Patterns
        IntPtr SearchAOB(Byte[] Bytes);
        IntPtr SearchAOB(String Pattern);
        IntPtr[] SearchAllAOB(String Pattern);

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

        public Byte[] GetInstructionBytes(IntPtr Address, Int32 MinimumInstructionBytes)
        {
            this.PrintDebugTag();

            const Int32 Largestx86InstructionSize = 15;

            // Read original bytes at code cave jump
            Boolean ReadSuccess;

            Byte[] OriginalBytes = OSInterface.Process.ReadBytes(Address, Largestx86InstructionSize, out ReadSuccess);

            if (!ReadSuccess || OriginalBytes == null || OriginalBytes.Length <= 0)
                return null;

            // Grab instructions at code entry point
            List<Instruction> Instructions = OSInterface.Architecture.Disassembler.Disassemble(OriginalBytes, OSInterface.Process.Is32Bit(), Address);

            // Determine size of instructions we need to overwrite
            Int32 ReplacedInstructionSize = 0;
            foreach (Instruction Instruction in Instructions)
            {
                ReplacedInstructionSize += Instruction.Length;
                if (ReplacedInstructionSize >= MinimumInstructionBytes)
                    break;
            }

            if (ReplacedInstructionSize < MinimumInstructionBytes)
                return null;

            // Truncate to only the bytes we will need to save
            OriginalBytes = OriginalBytes.LargestSubArray(0, ReplacedInstructionSize);

            return OriginalBytes;
        }

        public Int32 GetAssemblySize(String Assembly)
        {
            this.PrintDebugTag();

            Assembly = ResolveKeywords(Assembly);

            Byte[] Bytes = OSInterface.Architecture.Assembler.Assemble(OSInterface.Process.Is32Bit(), Assembly, IntPtr.Zero);

            return (Bytes == null ? 0 : Bytes.Length);
        }

        public Byte[] GetAssemblyBytes(String Assembly, IntPtr Address)
        {
            this.PrintDebugTag();

            Assembly = ResolveKeywords(Assembly);

            return OSInterface.Architecture.Assembler.Assemble(OSInterface.Process.Is32Bit(), Assembly, Address);
        }

        public IntPtr GetModuleAddress(String ModuleName)
        {
            this.PrintDebugTag();

            IntPtr Address = IntPtr.Zero;
            foreach (NormalizedModule Module in OSInterface.Process.GetModules())
            {
                if (Module.Name.ToLower() == ModuleName.ToLower())
                {
                    Address = Module.BaseAddress;
                    break;
                }
            }

            return Address;
        }

        public IntPtr AllocateMemory(Int32 Size)
        {
            this.PrintDebugTag();

            IntPtr Address = OSInterface.Process.AllocateMemory(Size);
            RemoteAllocations.Add(Address);

            return Address;
        }

        public void DeallocateMemory(IntPtr Address)
        {
            this.PrintDebugTag();

            foreach (IntPtr AllocationAddress in RemoteAllocations)
            {
                if (AllocationAddress == Address)
                {
                    OSInterface.Process.DeallocateMemory(AllocationAddress);
                    RemoteAllocations.Remove(AllocationAddress);
                    break;
                }
            }

            return;
        }

        public void DeallocateAllMemory()
        {
            this.PrintDebugTag();

            foreach (IntPtr Address in RemoteAllocations)
                OSInterface.Process.DeallocateMemory(Address);

            RemoteAllocations.Clear();
        }

        public IntPtr CreateCodeCave(IntPtr Entry, String Assembly)
        {
            this.PrintDebugTag();

            Assembly = ResolveKeywords(Assembly);

            Int32 AssemblySize = GetAssemblySize(Assembly);

            // Handle case where allocation is not needed
            if (AssemblySize < JumpSize)
            {
                Byte[] OriginalBytes = GetInstructionBytes(Entry, AssemblySize);

                if (OriginalBytes == null)
                    throw new Exception("Could not gather original bytes");

                // Determine number of no-ops to fill dangling bytes
                String NoOps = (OriginalBytes.Length - AssemblySize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", OriginalBytes.Length - AssemblySize)).TrimEnd(',');

                Byte[] InjectionBytes = OSInterface.Architecture.Assembler.Assemble(OSInterface.Process.Is32Bit(), Assembly + "\n" + NoOps, Entry);
                OSInterface.Process.WriteBytes(Entry, InjectionBytes);

                CodeCave CodeCave = new CodeCave(Entry, OriginalBytes, Entry);
                CodeCaves.Add(CodeCave);

                return Entry;
            }
            else
            {
                Byte[] OriginalBytes = GetInstructionBytes(Entry, JumpSize);

                if (OriginalBytes == null)
                    throw new Exception("Could not gather original bytes");

                // Not able to collect enough bytes to even place a jump!
                if (OriginalBytes.Length < JumpSize)
                    throw new Exception("Not enough bytes at address to jump");

                // Determine number of no-ops to fill dangling bytes
                String NoOps = (OriginalBytes.Length - JumpSize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", OriginalBytes.Length - JumpSize)).TrimEnd(',');

                // Allocate memory
                IntPtr RemoteAllocation = OSInterface.Process.AllocateMemory(AssemblySize);
                RemoteAllocations.Add(RemoteAllocation);

                // Write injected code to new page
                Byte[] InjectionBytes = OSInterface.Architecture.Assembler.Assemble(OSInterface.Process.Is32Bit(), Assembly, RemoteAllocation);
                OSInterface.Process.WriteBytes(RemoteAllocation, InjectionBytes);

                // Write in the jump to the code cave
                String CodeCaveJump = "jmp " + "0x" + Conversions.ToAddress(RemoteAllocation) + "\n" + NoOps;
                Byte[] JumpBytes = OSInterface.Architecture.Assembler.Assemble(OSInterface.Process.Is32Bit(), CodeCaveJump, Entry);
                OSInterface.Process.WriteBytes(Entry, JumpBytes);

                // Save this code cave for later deallocation
                CodeCave CodeCave = new CodeCave(RemoteAllocation, OriginalBytes, Entry);
                CodeCaves.Add(CodeCave);

                return RemoteAllocation;
            }
        }

        public IntPtr GetCaveExitAddress(IntPtr Address)
        {
            this.PrintDebugTag();

            Byte[] OriginalBytes = GetInstructionBytes(Address, JumpSize);
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

            Address = Address.Add(OriginalByteSize);

            return Address;
        }

        public void RemoveCodeCave(IntPtr Address)
        {
            this.PrintDebugTag();

            foreach (CodeCave CodeCave in CodeCaves)
            {
                if (CodeCave.Entry != Address)
                    continue;

                OSInterface.Process.Write<Byte[]>(CodeCave.Entry, CodeCave.OriginalBytes);

                // If these are equal, the cave is an in-place edit and not an allocation
                if (CodeCave.Entry == CodeCave.RemoteAllocation)
                    continue;

                OSInterface.Process.DeallocateMemory(CodeCave.RemoteAllocation);

            }
        }

        public void RemoveAllCodeCaves()
        {
            this.PrintDebugTag();

            foreach (CodeCave CodeCave in CodeCaves)
            {
                OSInterface.Process.WriteBytes(CodeCave.Entry, CodeCave.OriginalBytes);

                // If these are equal, the cave is an in-place edit and not an allocation
                if (CodeCave.Entry == CodeCave.RemoteAllocation)
                    continue;

                OSInterface.Process.DeallocateMemory(CodeCave.RemoteAllocation);
            }
            CodeCaves.Clear();
        }

        public void SetKeyword(String Keyword, IntPtr Address)
        {
            this.PrintDebugTag(Keyword, Address.ToString("x"));

            Keywords[Keyword] = "0x" + Conversions.ToAddress(Address);
        }

        public void SetGlobalKeyword(String GlobalKeyword, IntPtr Address)
        {
            this.PrintDebugTag(GlobalKeyword, Address.ToString("x"));

            GlobalKeywords[GlobalKeyword] = "0x" + Conversions.ToAddress(Address);
        }

        public void ClearKeyword(String Keyword)
        {
            this.PrintDebugTag(Keyword);

            if (Keywords.ContainsKey(Keyword))
                Keywords.Remove(Keyword);
        }

        public void ClearGlobalKeyword(String GlobalKeyword)
        {
            this.PrintDebugTag(GlobalKeyword);

            String ValueRemoved;
            if (GlobalKeywords.ContainsKey(GlobalKeyword))
                GlobalKeywords.TryRemove(GlobalKeyword, out ValueRemoved);
        }

        public void ClearAllKeywords()
        {
            this.PrintDebugTag();

            Keywords.Clear();
        }

        public void ClearAllGlobalKeywords()
        {
            this.PrintDebugTag();

            GlobalKeywords.Clear();
        }

        public IntPtr SearchAOB(Byte[] Bytes)
        {
            this.PrintDebugTag();

            IntPtr Address = OSInterface.Process.SearchAOB(Bytes);
            return Address;
        }

        public IntPtr SearchAOB(String Pattern)
        {
            this.PrintDebugTag(Pattern);

            return OSInterface.Process.SearchAOB(Pattern);
        }

        public IntPtr[] SearchAllAOB(String Pattern)
        {
            this.PrintDebugTag(Pattern);

            return OSInterface.Process.SearchllAOB(Pattern);
        }

        public SByte ReadSByte(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<SByte>(Address, out Success);
        }

        public Byte ReadByte(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<Byte>(Address, out Success);
        }

        public Int16 ReadInt16(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<Int16>(Address, out Success);
        }

        public Int32 ReadInt32(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<Int32>(Address, out Success);
        }

        public Int64 ReadInt64(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<Int64>(Address, out Success);
        }

        public UInt16 ReadUInt16(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<UInt16>(Address, out Success);
        }

        public UInt32 ReadUInt32(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<UInt32>(Address, out Success);
        }

        public UInt64 ReadUInt64(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<UInt64>(Address, out Success);
        }

        public Single ReadSingle(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<Single>(Address, out Success);
        }

        public Double ReadDouble(IntPtr Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return OSInterface.Process.Read<Double>(Address, out Success);
        }

        public Byte[] ReadBytes(IntPtr Address, Int32 Count)
        {
            this.PrintDebugTag(Address.ToString("x"), Count.ToString());

            Boolean Success;
            return OSInterface.Process.ReadBytes(Address, Count, out Success);
        }

        // Writing
        public void WriteSByte(IntPtr Address, SByte Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<SByte>(Address, Value);
        }

        public void WriteByte(IntPtr Address, Byte Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<Byte>(Address, Value);
        }

        public void WriteInt16(IntPtr Address, Int16 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<Int16>(Address, Value);
        }

        public void WriteInt32(IntPtr Address, Int32 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<Int32>(Address, Value);
        }

        public void WriteInt64(IntPtr Address, Int64 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<Int64>(Address, Value);
        }

        public void WriteUInt16(IntPtr Address, UInt16 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<UInt16>(Address, Value);
        }

        public void WriteUInt32(IntPtr Address, UInt32 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<UInt32>(Address, Value);
        }

        public void WriteUInt64(IntPtr Address, UInt64 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<UInt64>(Address, Value);
        }

        public void WriteSingle(IntPtr Address, Single Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<Single>(Address, Value);
        }

        public void WriteDouble(IntPtr Address, Double Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            OSInterface.Process.Write<Double>(Address, Value);
        }

        public void WriteBytes(IntPtr Address, Byte[] Values)
        {
            this.PrintDebugTag(Address.ToString("x"));

            OSInterface.Process.WriteBytes(Address, Values);
        }

    } // End interface

} // End namespace