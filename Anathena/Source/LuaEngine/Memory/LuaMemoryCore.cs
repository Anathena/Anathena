using Ana.Source.Engine;
using Ana.Source.Engine.Architecture.Disassembler.SharpDisasm;
using Ana.Source.Engine.OperatingSystems;
using Ana.Source.Engine.Processes;
using Ana.Source.Utils.Extensions;
using Ana.Source.Utils.Validation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Ana.Source.LuaEngine.Memory
{
    public class LuaMemoryCore : IMemoryCore, IProcessObserver
    {
        private EngineCore EngineCore;

        private static ConcurrentDictionary<String, String> GlobalKeywords;
        private ConcurrentDictionary<String, String> Keywords;
        private List<UInt64> RemoteAllocations;
        private List<CodeCave> CodeCaves;

        private const Int32 JumpSize = 5;

        private struct CodeCave
        {
            public Byte[] OriginalBytes;
            public UInt64 RemoteAllocation;
            public UInt64 Entry;

            public CodeCave(UInt64 RemoteAllocation, Byte[] OriginalBytes, UInt64 Entry)
            {
                this.RemoteAllocation = RemoteAllocation;
                this.OriginalBytes = OriginalBytes;
                this.Entry = Entry;
            }
        }

        public LuaMemoryCore()
        {
            InitializeProcessObserver();

            GlobalKeywords = new ConcurrentDictionary<String, String>();
            RemoteAllocations = new List<UInt64>();
            Keywords = new ConcurrentDictionary<String, String>();
            CodeCaves = new List<CodeCave>();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
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

            return Assembly;
        }

        public Byte[] GetInstructionBytes(UInt64 Address, Int32 MinimumInstructionBytes)
        {
            this.PrintDebugTag();

            const Int32 Largestx86InstructionSize = 15;

            // Read original bytes at code cave jump
            Boolean ReadSuccess;

            Byte[] OriginalBytes = EngineCore.Memory.ReadBytes(Address.ToIntPtr(), Largestx86InstructionSize, out ReadSuccess);

            if (!ReadSuccess || OriginalBytes == null || OriginalBytes.Length <= 0)
                return null;

            // Grab instructions at code entry point
            List<Instruction> Instructions = EngineCore.Disassembler.Disassemble(OriginalBytes, EngineCore.Memory.IsProcess32Bit(), Address.ToIntPtr());

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

        public Int32 GetAssemblySize(String Assembly, UInt64 Address)
        {
            this.PrintDebugTag();

            Assembly = ResolveKeywords(Assembly);

            Byte[] Bytes = EngineCore.Assembler.Assemble(EngineCore.Memory.IsProcess32Bit(), Assembly, Address.ToIntPtr());

            return (Bytes == null ? 0 : Bytes.Length);
        }

        public Byte[] GetAssemblyBytes(String Assembly, UInt64 Address)
        {
            this.PrintDebugTag();

            Assembly = ResolveKeywords(Assembly);

            return EngineCore.Assembler.Assemble(EngineCore.Memory.IsProcess32Bit(), Assembly, Address.ToIntPtr());
        }

        public UInt64 GetModuleAddress(String ModuleName)
        {
            this.PrintDebugTag();

            UInt64 Address = 0;
            foreach (NormalizedModule Module in EngineCore.Memory.GetModules())
            {
                if (Module.Name.Equals(ModuleName, StringComparison.OrdinalIgnoreCase))
                {
                    Address = Module.BaseAddress.ToUInt64();
                    break;
                }
            }

            return Address;
        }

        public UInt64 AllocateMemory(Int32 Size)
        {
            this.PrintDebugTag();

            UInt64 Address = EngineCore.Memory.AllocateMemory(Size).ToUInt64();
            RemoteAllocations.Add(Address);

            return Address;
        }

        public void DeallocateMemory(UInt64 Address)
        {
            this.PrintDebugTag();

            foreach (UInt64 AllocationAddress in RemoteAllocations)
            {
                if (AllocationAddress == Address)
                {
                    EngineCore.Memory.DeallocateMemory(AllocationAddress.ToIntPtr());
                    RemoteAllocations.Remove(AllocationAddress);
                    break;
                }
            }

            return;
        }

        public void DeallocateAllMemory()
        {
            this.PrintDebugTag();

            foreach (UInt64 Address in RemoteAllocations)
                EngineCore.Memory.DeallocateMemory(Address.ToIntPtr());

            RemoteAllocations.Clear();
        }

        public UInt64 CreateCodeCave(UInt64 Entry, String Assembly)
        {
            this.PrintDebugTag();

            Assembly = ResolveKeywords(Assembly);

            Int32 AssemblySize = GetAssemblySize(Assembly, Entry);

            // Handle case where allocation is not needed
            if (AssemblySize < JumpSize)
            {
                Byte[] OriginalBytes = GetInstructionBytes(Entry, AssemblySize);

                if (OriginalBytes == null)
                    throw new Exception("Could not gather original bytes");

                // Determine number of no-ops to fill dangling bytes
                String NoOps = (OriginalBytes.Length - AssemblySize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", OriginalBytes.Length - AssemblySize)).TrimEnd(',');

                Byte[] InjectionBytes = EngineCore.Assembler.Assemble(EngineCore.Memory.IsProcess32Bit(), Assembly + "\n" + NoOps, Entry.ToIntPtr());
                EngineCore.Memory.WriteBytes(Entry.ToIntPtr(), InjectionBytes);

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
                UInt64 RemoteAllocation = EngineCore.Memory.AllocateMemory(AssemblySize).ToUInt64();
                RemoteAllocations.Add(RemoteAllocation);

                // Write injected code to new page
                Byte[] InjectionBytes = EngineCore.Assembler.Assemble(EngineCore.Memory.IsProcess32Bit(), Assembly, RemoteAllocation.ToIntPtr());
                EngineCore.Memory.WriteBytes(RemoteAllocation.ToIntPtr(), InjectionBytes);

                // Write in the jump to the code cave
                String CodeCaveJump = "jmp " + "0x" + Conversions.ToAddress(RemoteAllocation) + "\n" + NoOps;
                Byte[] JumpBytes = EngineCore.Assembler.Assemble(EngineCore.Memory.IsProcess32Bit(), CodeCaveJump, Entry.ToIntPtr());
                EngineCore.Memory.WriteBytes(Entry.ToIntPtr(), JumpBytes);

                // Save this code cave for later deallocation
                CodeCave CodeCave = new CodeCave(RemoteAllocation, OriginalBytes, Entry);
                CodeCaves.Add(CodeCave);

                return RemoteAllocation;
            }
        }

        public UInt64 GetCaveExitAddress(UInt64 Address)
        {
            this.PrintDebugTag();

            Byte[] OriginalBytes = GetInstructionBytes(Address, JumpSize);
            Int32 OriginalByteSize;

            if (OriginalBytes != null && OriginalBytes.Length < JumpSize)
            {
                // Determine the size of the minimum number of instructions we will be overwriting
                OriginalByteSize = OriginalBytes.Length;
            }
            else
            {
                // Fall back if something goes wrong
                OriginalByteSize = JumpSize;
            }

            Address = Address.ToIntPtr().Add(OriginalByteSize).ToUInt64();

            return Address;
        }

        public void RemoveCodeCave(UInt64 Address)
        {
            this.PrintDebugTag();

            foreach (CodeCave CodeCave in CodeCaves)
            {
                if (CodeCave.Entry != Address)
                    continue;

                EngineCore.Memory.WriteBytes(CodeCave.Entry.ToIntPtr(), CodeCave.OriginalBytes);

                // If these are equal, the cave is an in-place edit and not an allocation
                if (CodeCave.Entry == CodeCave.RemoteAllocation)
                    continue;

                EngineCore.Memory.DeallocateMemory(CodeCave.RemoteAllocation.ToIntPtr());

            }
        }

        public void RemoveAllCodeCaves()
        {
            this.PrintDebugTag();

            foreach (CodeCave CodeCave in CodeCaves)
            {
                EngineCore.Memory.WriteBytes(CodeCave.Entry.ToIntPtr(), CodeCave.OriginalBytes);

                // If these are equal, the cave is an in-place edit and not an allocation
                if (CodeCave.Entry == CodeCave.RemoteAllocation)
                    continue;

                EngineCore.Memory.DeallocateMemory(CodeCave.RemoteAllocation.ToIntPtr());
            }
            CodeCaves.Clear();
        }

        public void SetKeyword(String Keyword, UInt64 Address)
        {
            this.PrintDebugTag(Keyword, Address.ToString("x"));
            String Mapping = "0x" + Conversions.ToAddress(Address);
            Keywords[Keyword] = Mapping;
        }

        public void SetGlobalKeyword(String GlobalKeyword, UInt64 Address)
        {
            this.PrintDebugTag(GlobalKeyword, Address.ToString("x"));

            GlobalKeywords[GlobalKeyword] = "0x" + Conversions.ToAddress(Address);
        }

        public void ClearKeyword(String Keyword)
        {
            this.PrintDebugTag(Keyword);

            String Result;
            if (Keywords.ContainsKey(Keyword))
                Keywords.TryRemove(Keyword, out Result);
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

        public UInt64 SearchAOB(Byte[] Bytes)
        {
            this.PrintDebugTag();

            UInt64 Address = EngineCore.Memory.SearchAOB(Bytes).ToUInt64();
            return Address;
        }

        public UInt64 SearchAOB(String Pattern)
        {
            this.PrintDebugTag(Pattern);

            return EngineCore.Memory.SearchAOB(Pattern).ToUInt64();
        }

        public UInt64[] SearchAllAOB(String Pattern)
        {
            this.PrintDebugTag(Pattern);
            List<IntPtr> AOBs = new List<IntPtr>(EngineCore.Memory.SearchllAOB(Pattern));
            List<UInt64> ConvertedAOBs = new List<UInt64>();
            AOBs.ForEach(x => ConvertedAOBs.Add(x.ToUInt64()));
            return ConvertedAOBs.ToArray();
        }

        public SByte ReadSByte(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<SByte>(Address.ToIntPtr(), out Success);
        }

        public Byte ReadByte(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<Byte>(Address.ToIntPtr(), out Success);
        }

        public Int16 ReadInt16(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<Int16>(Address.ToIntPtr(), out Success);
        }

        public Int32 ReadInt32(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<Int32>(Address.ToIntPtr(), out Success);
        }

        public Int64 ReadInt64(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<Int64>(Address.ToIntPtr(), out Success);
        }

        public UInt16 ReadUInt16(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<UInt16>(Address.ToIntPtr(), out Success);
        }

        public UInt32 ReadUInt32(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<UInt32>(Address.ToIntPtr(), out Success);
        }

        public UInt64 ReadUInt64(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<UInt64>(Address.ToIntPtr(), out Success);
        }

        public Single ReadSingle(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<Single>(Address.ToIntPtr(), out Success);
        }

        public Double ReadDouble(UInt64 Address)
        {
            this.PrintDebugTag(Address.ToString("x"));

            Boolean Success;
            return EngineCore.Memory.Read<Double>(Address.ToIntPtr(), out Success);
        }

        public Byte[] ReadBytes(UInt64 Address, Int32 Count)
        {
            this.PrintDebugTag(Address.ToString("x"), Count.ToString());

            Boolean Success;
            return EngineCore.Memory.ReadBytes(Address.ToIntPtr(), Count, out Success);
        }

        // Writing
        public void WriteSByte(UInt64 Address, SByte Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<SByte>(Address.ToIntPtr(), Value);
        }

        public void WriteByte(UInt64 Address, Byte Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<Byte>(Address.ToIntPtr(), Value);
        }

        public void WriteInt16(UInt64 Address, Int16 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<Int16>(Address.ToIntPtr(), Value);
        }

        public void WriteInt32(UInt64 Address, Int32 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<Int32>(Address.ToIntPtr(), Value);
        }

        public void WriteInt64(UInt64 Address, Int64 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<Int64>(Address.ToIntPtr(), Value);
        }

        public void WriteUInt16(UInt64 Address, UInt16 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<UInt16>(Address.ToIntPtr(), Value);
        }

        public void WriteUInt32(UInt64 Address, UInt32 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<UInt32>(Address.ToIntPtr(), Value);
        }

        public void WriteUInt64(UInt64 Address, UInt64 Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<UInt64>(Address.ToIntPtr(), Value);
        }

        public void WriteSingle(UInt64 Address, Single Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<Single>(Address.ToIntPtr(), Value);
        }

        public void WriteDouble(UInt64 Address, Double Value)
        {
            this.PrintDebugTag(Address.ToString("x"), Value.ToString());

            EngineCore.Memory.Write<Double>(Address.ToIntPtr(), Value);
        }

        public void WriteBytes(UInt64 Address, Byte[] Values)
        {
            this.PrintDebugTag(Address.ToString("x"));

            EngineCore.Memory.WriteBytes(Address.ToIntPtr(), Values);
        }

    } // End interface

} // End namespace