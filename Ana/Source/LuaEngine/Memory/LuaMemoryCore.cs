namespace Ana.Source.LuaEngine.Memory
{
    using Engine;
    using Engine.Architecture.Disassembler.SharpDisasm;
    using Engine.OperatingSystems;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Utils.Extensions;
    using Utils.Validation;

    internal class LuaMemoryCore : IMemoryCore
    {
        private const Int32 JumpSize = 5;
        private const Int32 Largestx86InstructionSize = 15;

        public LuaMemoryCore()
        {
            LuaMemoryCore.GlobalKeywords = new ConcurrentDictionary<String, String>();
            this.RemoteAllocations = new List<UInt64>();
            this.Keywords = new ConcurrentDictionary<String, String>();
            this.CodeCaves = new List<CodeCave>();
        }

        private static ConcurrentDictionary<String, String> GlobalKeywords { get; set; }

        private ConcurrentDictionary<String, String> Keywords { get; set; }

        private List<UInt64> RemoteAllocations { get; set; }

        private List<CodeCave> CodeCaves { get; set; }

        public Byte[] GetInstructionBytes(UInt64 address, Int32 minimumInstructionBytes)
        {
            this.PrintDebugTag();

            // Read original bytes at code cave jump
            Boolean readSuccess;

            Byte[] originalBytes = EngineCore.GetInstance().OperatingSystemAdapter.ReadBytes(address.ToIntPtr(), LuaMemoryCore.Largestx86InstructionSize, out readSuccess);

            if (!readSuccess || originalBytes == null || originalBytes.Length <= 0)
            {
                return null;
            }

            // Grab instructions at code entry point
            List<Instruction> instructions = EngineCore.GetInstance().Architecture.GetDisassembler().Disassemble(originalBytes, EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), address.ToIntPtr());

            // Determine size of instructions we need to overwrite
            Int32 replacedInstructionSize = 0;
            foreach (Instruction instruction in instructions)
            {
                replacedInstructionSize += instruction.Length;
                if (replacedInstructionSize >= minimumInstructionBytes)
                {
                    break;
                }
            }

            if (replacedInstructionSize < minimumInstructionBytes)
            {
                return null;
            }

            // Truncate to only the bytes we will need to save
            originalBytes = originalBytes.LargestSubArray(0, replacedInstructionSize);

            return originalBytes;
        }

        public Int32 GetAssemblySize(String assembly, UInt64 address)
        {
            this.PrintDebugTag();

            assembly = this.ResolveKeywords(assembly);

            Byte[] bytes = EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), assembly, address.ToIntPtr());

            return bytes == null ? 0 : bytes.Length;
        }

        public Byte[] GetAssemblyBytes(String assembly, UInt64 address)
        {
            this.PrintDebugTag();

            assembly = this.ResolveKeywords(assembly);

            return EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), assembly, address.ToIntPtr());
        }

        public UInt64 GetModuleAddress(String moduleName)
        {
            this.PrintDebugTag();

            moduleName = moduleName?.Split('.')?.First();

            UInt64 address = 0;
            foreach (NormalizedModule module in EngineCore.GetInstance().OperatingSystemAdapter.GetModules())
            {
                String targetModuleName = module?.Name?.Split('.')?.First();
                if (targetModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    address = module.BaseAddress.ToUInt64();
                    break;
                }
            }

            return address;
        }

        public UInt64 AllocateMemory(Int32 size)
        {
            this.PrintDebugTag();

            UInt64 address = EngineCore.GetInstance().OperatingSystemAdapter.AllocateMemory(size).ToUInt64();
            this.RemoteAllocations.Add(address);

            return address;
        }

        public void DeallocateMemory(UInt64 address)
        {
            this.PrintDebugTag();

            foreach (UInt64 allocationAddress in this.RemoteAllocations)
            {
                if (allocationAddress == address)
                {
                    EngineCore.GetInstance().OperatingSystemAdapter.DeallocateMemory(allocationAddress.ToIntPtr());
                    this.RemoteAllocations.Remove(allocationAddress);
                    break;
                }
            }

            return;
        }

        public void DeallocateAllMemory()
        {
            this.PrintDebugTag();

            foreach (UInt64 address in this.RemoteAllocations)
            {
                EngineCore.GetInstance().OperatingSystemAdapter.DeallocateMemory(address.ToIntPtr());
            }

            this.RemoteAllocations.Clear();
        }

        public UInt64 CreateCodeCave(UInt64 entry, String assembly)
        {
            this.PrintDebugTag();

            assembly = this.ResolveKeywords(assembly);

            Int32 assemblySize = this.GetAssemblySize(assembly, entry);

            // Handle case where allocation is not needed
            if (assemblySize < LuaMemoryCore.JumpSize)
            {
                Byte[] originalBytes = this.GetInstructionBytes(entry, assemblySize);

                if (originalBytes == null)
                {
                    throw new Exception("Could not gather original bytes");
                }

                // Determine number of no-ops to fill dangling bytes
                String noOps = (originalBytes.Length - assemblySize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", originalBytes.Length - assemblySize)).TrimEnd(',');

                Byte[] injectionBytes = EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), assembly + "\n" + noOps, entry.ToIntPtr());
                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(entry.ToIntPtr(), injectionBytes);

                CodeCave codeCave = new CodeCave(entry, originalBytes, entry);
                this.CodeCaves.Add(codeCave);

                return entry;
            }
            else
            {
                Byte[] originalBytes = this.GetInstructionBytes(entry, LuaMemoryCore.JumpSize);

                if (originalBytes == null)
                {
                    throw new Exception("Could not gather original bytes");
                }

                // Not able to collect enough bytes to even place a jump!
                if (originalBytes.Length < LuaMemoryCore.JumpSize)
                {
                    throw new Exception("Not enough bytes at address to jump");
                }

                // Determine number of no-ops to fill dangling bytes
                String noOps = (originalBytes.Length - LuaMemoryCore.JumpSize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", originalBytes.Length - JumpSize)).TrimEnd(',');

                // Allocate memory
                UInt64 remoteAllocation = EngineCore.GetInstance().OperatingSystemAdapter.AllocateMemory(assemblySize).ToUInt64();
                this.RemoteAllocations.Add(remoteAllocation);

                // Write injected code to new page
                Byte[] injectionBytes = EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), assembly, remoteAllocation.ToIntPtr());
                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(remoteAllocation.ToIntPtr(), injectionBytes);

                // Write in the jump to the code cave
                String codeCaveJump = "jmp " + "0x" + Conversions.ToAddress(remoteAllocation) + "\n" + noOps;
                Byte[] jumpBytes = EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), codeCaveJump, entry.ToIntPtr());
                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(entry.ToIntPtr(), jumpBytes);

                // Save this code cave for later deallocation
                CodeCave codeCave = new CodeCave(remoteAllocation, originalBytes, entry);
                this.CodeCaves.Add(codeCave);

                return remoteAllocation;
            }
        }

        public UInt64 GetCaveExitAddress(UInt64 address)
        {
            this.PrintDebugTag();

            Byte[] originalBytes = this.GetInstructionBytes(address, LuaMemoryCore.JumpSize);
            Int32 originalByteSize;

            if (originalBytes != null && originalBytes.Length < LuaMemoryCore.JumpSize)
            {
                // Determine the size of the minimum number of instructions we will be overwriting
                originalByteSize = originalBytes.Length;
            }
            else
            {
                // Fall back if something goes wrong
                originalByteSize = LuaMemoryCore.JumpSize;
            }

            address = address.ToIntPtr().Add(originalByteSize).ToUInt64();

            return address;
        }

        public void RemoveCodeCave(UInt64 address)
        {
            this.PrintDebugTag();

            foreach (CodeCave codeCave in this.CodeCaves)
            {
                if (codeCave.Entry != address)
                {
                    continue;
                }

                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(codeCave.Entry.ToIntPtr(), codeCave.OriginalBytes);

                // If these are equal, the cave is an in-place edit and not an allocation
                if (codeCave.Entry == codeCave.RemoteAllocation)
                {
                    continue;
                }

                EngineCore.GetInstance().OperatingSystemAdapter.DeallocateMemory(codeCave.RemoteAllocation.ToIntPtr());
            }
        }

        public void RemoveAllCodeCaves()
        {
            this.PrintDebugTag();

            foreach (CodeCave codeCave in this.CodeCaves)
            {
                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(codeCave.Entry.ToIntPtr(), codeCave.OriginalBytes);

                // If these are equal, the cave is an in-place edit and not an allocation
                if (codeCave.Entry == codeCave.RemoteAllocation)
                {
                    continue;
                }

                EngineCore.GetInstance().OperatingSystemAdapter.DeallocateMemory(codeCave.RemoteAllocation.ToIntPtr());
            }

            this.CodeCaves.Clear();
        }

        public void SetKeyword(String keyword, UInt64 address)
        {
            this.PrintDebugTag(keyword, address.ToString("x"));

            String mapping = "0x" + Conversions.ToAddress(address);
            this.Keywords[keyword] = mapping;
        }

        public void SetGlobalKeyword(String globalKeyword, UInt64 address)
        {
            this.PrintDebugTag(globalKeyword, address.ToString("x"));

            LuaMemoryCore.GlobalKeywords[globalKeyword] = "0x" + Conversions.ToAddress(address);
        }

        public void ClearKeyword(String keyword)
        {
            this.PrintDebugTag(keyword);

            String result;
            if (this.Keywords.ContainsKey(keyword))
            {
                this.Keywords.TryRemove(keyword, out result);
            }
        }

        public void ClearGlobalKeyword(String globalKeyword)
        {
            this.PrintDebugTag(globalKeyword);

            String valueRemoved;
            if (LuaMemoryCore.GlobalKeywords.ContainsKey(globalKeyword))
            {
                LuaMemoryCore.GlobalKeywords.TryRemove(globalKeyword, out valueRemoved);
            }
        }

        public void ClearAllKeywords()
        {
            this.PrintDebugTag();

            this.Keywords.Clear();
        }

        public void ClearAllGlobalKeywords()
        {
            this.PrintDebugTag();

            LuaMemoryCore.GlobalKeywords.Clear();
        }

        public UInt64 SearchAOB(Byte[] bytes)
        {
            this.PrintDebugTag();

            UInt64 address = EngineCore.GetInstance().OperatingSystemAdapter.SearchAob(bytes).ToUInt64();
            return address;
        }

        public UInt64 SearchAOB(String pattern)
        {
            this.PrintDebugTag(pattern);

            return EngineCore.GetInstance().OperatingSystemAdapter.SearchAob(pattern).ToUInt64();
        }

        public UInt64[] SearchAllAob(String pattern)
        {
            this.PrintDebugTag(pattern);
            List<IntPtr> aobResults = new List<IntPtr>(EngineCore.GetInstance().OperatingSystemAdapter.SearchllAob(pattern));
            List<UInt64> convertedAobs = new List<UInt64>();
            aobResults.ForEach(x => convertedAobs.Add(x.ToUInt64()));
            return convertedAobs.ToArray();
        }

        public SByte ReadSByte(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<SByte>(address.ToIntPtr(), out readSuccess);
        }

        public Byte ReadByte(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<Byte>(address.ToIntPtr(), out readSuccess);
        }

        public Int16 ReadInt16(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<Int16>(address.ToIntPtr(), out readSuccess);
        }

        public Int32 ReadInt32(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<Int32>(address.ToIntPtr(), out readSuccess);
        }

        public Int64 ReadInt64(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<Int64>(address.ToIntPtr(), out readSuccess);
        }

        public UInt16 ReadUInt16(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<UInt16>(address.ToIntPtr(), out readSuccess);
        }

        public UInt32 ReadUInt32(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<UInt32>(address.ToIntPtr(), out readSuccess);
        }

        public UInt64 ReadUInt64(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<UInt64>(address.ToIntPtr(), out readSuccess);
        }

        public Single ReadSingle(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<Single>(address.ToIntPtr(), out readSuccess);
        }

        public Double ReadDouble(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<Double>(address.ToIntPtr(), out readSuccess);
        }

        public Byte[] ReadBytes(UInt64 address, Int32 count)
        {
            this.PrintDebugTag(address.ToString("x"), count.ToString());

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.ReadBytes(address.ToIntPtr(), count, out readSuccess);
        }

        public void WriteSByte(UInt64 address, SByte value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<SByte>(address.ToIntPtr(), value);
        }

        public void WriteByte(UInt64 address, Byte value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<Byte>(address.ToIntPtr(), value);
        }

        public void WriteInt16(UInt64 address, Int16 value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<Int16>(address.ToIntPtr(), value);
        }

        public void WriteInt32(UInt64 address, Int32 value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<Int32>(address.ToIntPtr(), value);
        }

        public void WriteInt64(UInt64 address, Int64 value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<Int64>(address.ToIntPtr(), value);
        }

        public void WriteUInt16(UInt64 address, UInt16 value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<UInt16>(address.ToIntPtr(), value);
        }

        public void WriteUInt32(UInt64 address, UInt32 value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<UInt32>(address.ToIntPtr(), value);
        }

        public void WriteUInt64(UInt64 address, UInt64 value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<UInt64>(address.ToIntPtr(), value);
        }

        public void WriteSingle(UInt64 address, Single value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<Single>(address.ToIntPtr(), value);
        }

        public void WriteDouble(UInt64 address, Double value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<Double>(address.ToIntPtr(), value);
        }

        public void WriteBytes(UInt64 address, Byte[] values)
        {
            this.PrintDebugTag(address.ToString("x"));

            EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(address.ToIntPtr(), values);
        }

        private String ResolveKeywords(String assembly)
        {
            if (assembly == null)
            {
                return String.Empty;
            }

            assembly = assembly.Replace("\t", String.Empty);

            // Resolve keywords
            foreach (KeyValuePair<String, String> keyword in this.Keywords)
            {
                assembly = assembly.Replace(keyword.Key, keyword.Value);
            }

            foreach (KeyValuePair<String, String> globalKeyword in LuaMemoryCore.GlobalKeywords)
            {
                assembly = assembly.Replace(globalKeyword.Key, globalKeyword.Value);
            }

            return assembly;
        }

        private struct CodeCave
        {
            public CodeCave(UInt64 remoteAllocation, Byte[] originalBytes, UInt64 entry)
            {
                this.RemoteAllocation = remoteAllocation;
                this.OriginalBytes = originalBytes;
                this.Entry = entry;
            }

            public Byte[] OriginalBytes { get; set; }

            public UInt64 RemoteAllocation { get; set; }

            public UInt64 Entry { get; set; }
        }
    }
    //// End interface
}
//// End namespace