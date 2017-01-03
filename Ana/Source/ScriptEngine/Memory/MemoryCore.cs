﻿namespace Ana.Source.ScriptEngine.Memory
{
    using Source.Engine;
    using Source.Engine.Architecture.Disassembler.SharpDisasm;
    using Source.Engine.OperatingSystems;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Utils.Extensions;
    using Utils.Validation;

    /// <summary>
    /// Provides access to memory manipulations in an external process for scripts.
    /// </summary>
    internal class MemoryCore : IMemoryCore
    {
        /// <summary>
        /// The size of a jump instruction. TODO: this may not always be the case, and sure as hell isn't true for all architectures.
        /// </summary>
        private const Int32 JumpSize = 5;

        /// <summary>
        /// The largest possible instruction size. TODO: Abstract this out to the architecture factory. This can vary by architecture.
        /// </summary>
        private const Int32 Largestx86InstructionSize = 15;

        /// <summary>
        /// Gets or sets the keywords associated with all running scripts.
        /// </summary>
        private static Lazy<ConcurrentDictionary<String, String>> globalKeywords = new Lazy<ConcurrentDictionary<String, String>>(
            () => { return new ConcurrentDictionary<String, String>(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCore" /> class.
        /// </summary>
        public MemoryCore()
        {
            this.RemoteAllocations = new List<UInt64>();
            this.Keywords = new ConcurrentDictionary<String, String>();
            this.CodeCaves = new List<CodeCave>();
        }

        /// <summary>
        /// Gets or sets the keywords associated with the calling script.
        /// </summary>
        private ConcurrentDictionary<String, String> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the collection of allocations created in the external process;
        /// </summary>
        private List<UInt64> RemoteAllocations { get; set; }

        /// <summary>
        /// Gets or sets the collection of code caves active in the external process.
        /// </summary>
        private List<CodeCave> CodeCaves { get; set; }

        /// <summary>
        /// Returns the address of the specified module name. Returns 0 on failure.
        /// </summary>
        /// <param name="moduleName">The name of the module to calculate the address of.</param>
        /// <returns>The read address.</returns>
        public UInt64 GetModuleAddress(String moduleName)
        {
            this.PrintDebugTag();

            moduleName = moduleName?.RemoveSuffixes(".exe", ".dll");

            UInt64 address = 0;
            foreach (NormalizedModule module in EngineCore.GetInstance().OperatingSystemAdapter.GetModules())
            {
                String targetModuleName = module?.Name?.RemoveSuffixes(".exe", ".dll");
                if (targetModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    address = module.BaseAddress.ToUInt64();
                    break;
                }
            }

            return address;
        }

        /// <summary>
        /// Determines the size of the first instruction at the given address.
        /// </summary>
        /// <param name="assembly">The assembly code to measure.</param>
        /// <param name="address">The base address of the assembly code.</param>
        /// <returns>The size, in number of bytes, of the assembly code.</returns>
        public Int32 GetAssemblySize(String assembly, UInt64 address)
        {
            this.PrintDebugTag();

            assembly = this.ResolveKeywords(assembly);

            Byte[] bytes = EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), assembly, address.ToIntPtr());

            return bytes == null ? 0 : bytes.Length;
        }

        /// <summary>
        /// Converts the instruction with a frame of reference at a specific address to raw bytes.
        /// </summary>
        /// <param name="assembly">The assembly code to disassemble.</param>
        /// <param name="address">The base address of the assembly code.</param>
        /// <returns>The disassembled bytes of the assembly code.</returns>
        public Byte[] GetAssemblyBytes(String assembly, UInt64 address)
        {
            this.PrintDebugTag();

            assembly = this.ResolveKeywords(assembly);

            return EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), assembly, address.ToIntPtr());
        }

        /// <summary>
        /// Returns the bytes of multiple instructions, as long as they are greater than the specified minimum.
        /// ie with a minimum of 16 bytes, if we get three 5 byte instructions, we will need to read the entire
        /// next instruction.
        /// </summary>
        /// <param name="address">The base address to begin reading instructions.</param>
        /// <param name="injectedCodeSize">The size of the code being injected.</param>
        /// <returns>The bytes read from memory</returns>
        public Byte[] CollectOriginalBytes(UInt64 address, Int32 injectedCodeSize)
        {
            this.PrintDebugTag();

            // Read original bytes at code cave jump
            Boolean readSuccess;

            Byte[] originalBytes = EngineCore.GetInstance().OperatingSystemAdapter.ReadBytes(address.ToIntPtr(), injectedCodeSize + MemoryCore.Largestx86InstructionSize, out readSuccess);

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
                if (replacedInstructionSize >= injectedCodeSize)
                {
                    break;
                }
            }

            if (replacedInstructionSize < injectedCodeSize)
            {
                return null;
            }

            // Truncate to only the bytes we will need to save
            originalBytes = originalBytes.LargestSubArray(0, replacedInstructionSize);

            return originalBytes;
        }

        /// <summary>
        /// Allocates memory in the target process, and returns the address of the new memory.
        /// </summary>
        /// <param name="size">The size of the allocation.</param>
        /// <returns>The address of the allocated memory.</returns>
        public UInt64 AllocateMemory(Int32 size)
        {
            this.PrintDebugTag();

            UInt64 address = EngineCore.GetInstance().OperatingSystemAdapter.AllocateMemory(size).ToUInt64();
            this.RemoteAllocations.Add(address);

            return address;
        }

        /// <summary>
        /// Deallocates memory previously allocated at a specified address.
        /// </summary>
        /// <param name="address">The address to perform the deallocation.</param>
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

        /// <summary>
        /// Deallocates all allocated memory for the parent script.
        /// </summary>
        public void DeallocateAllMemory()
        {
            this.PrintDebugTag();

            foreach (UInt64 address in this.RemoteAllocations)
            {
                EngineCore.GetInstance().OperatingSystemAdapter.DeallocateMemory(address.ToIntPtr());
            }

            this.RemoteAllocations.Clear();
        }

        /// <summary>
        /// Creates a code cave that jumps from a given entry address and executes the given assembly. This will nop-fill.
        /// If the injected assmebly code fits in one instruction, no cave will be created.
        /// </summary>
        /// <param name="address">The injection address.</param>
        /// <param name="assembly">The assembly code to disassemble and inject into the code cave.</param>
        /// <returns>The address of the code cave. Returns zero if no allocation was necessary.</returns>
        public UInt64 CreateCodeCave(UInt64 address, String assembly)
        {
            this.PrintDebugTag();

            assembly = this.ResolveKeywords(assembly);

            Int32 assemblySize = this.GetAssemblySize(assembly, address);

            // Handle case where allocation is not needed
            if (assemblySize <= MemoryCore.JumpSize)
            {
                Byte[] originalBytes = this.CollectOriginalBytes(address, assemblySize);

                if (originalBytes == null)
                {
                    throw new Exception("Could not gather original bytes");
                }

                // Determine number of no-ops to fill dangling bytes
                String noOps = (originalBytes.Length - assemblySize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", originalBytes.Length - assemblySize)).TrimEnd(',');

                Byte[] injectionBytes = EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), assembly + "\n" + noOps, address.ToIntPtr());
                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(address.ToIntPtr(), injectionBytes);

                CodeCave codeCave = new CodeCave(address, 0, originalBytes);
                this.CodeCaves.Add(codeCave);

                return address;
            }
            else
            {
                Byte[] originalBytes = this.CollectOriginalBytes(address, MemoryCore.JumpSize);

                if (originalBytes == null)
                {
                    throw new Exception("Could not gather original bytes");
                }

                // Not able to collect enough bytes to even place a jump!
                if (originalBytes.Length < MemoryCore.JumpSize)
                {
                    throw new Exception("Not enough bytes at address to jump");
                }

                // Determine number of no-ops to fill dangling bytes
                String noOps = (originalBytes.Length - MemoryCore.JumpSize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", originalBytes.Length - JumpSize)).TrimEnd(',');

                // Allocate memory
                UInt64 remoteAllocation = EngineCore.GetInstance().OperatingSystemAdapter.AllocateMemory(assemblySize).ToUInt64();
                this.RemoteAllocations.Add(remoteAllocation);

                // Write injected code to new page
                Byte[] injectionBytes = EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), assembly, remoteAllocation.ToIntPtr());
                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(remoteAllocation.ToIntPtr(), injectionBytes);

                // Write in the jump to the code cave
                String codeCaveJump = "jmp " + "0x" + Conversions.ToAddress(remoteAllocation) + "\n" + noOps;
                Byte[] jumpBytes = EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), codeCaveJump, address.ToIntPtr());
                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(address.ToIntPtr(), jumpBytes);

                // Save this code cave for later deallocation
                CodeCave codeCave = new CodeCave(address, remoteAllocation, originalBytes);
                this.CodeCaves.Add(codeCave);

                return remoteAllocation;
            }
        }

        /// <summary>
        /// Injects instructions at the specified location, overwriting following instructions. This will nop-fill.
        /// </summary>
        /// <param name="address">The injection address.</param>
        /// <param name="assembly">The assembly code to disassemble and inject into the code cave.</param>
        /// <returns>The address of the code cave.</returns>
        public UInt64 InjectCode(UInt64 address, String assembly)
        {
            this.PrintDebugTag();

            assembly = this.ResolveKeywords(assembly);

            Int32 assemblySize = this.GetAssemblySize(assembly, address);

            Byte[] originalBytes = this.CollectOriginalBytes(address, assemblySize);

            if (originalBytes == null)
            {
                throw new Exception("Could not gather original bytes");
            }

            // Determine number of no-ops to fill dangling bytes
            String noOps = (originalBytes.Length - assemblySize > 0 ? "db " : String.Empty) + String.Join(" ", Enumerable.Repeat("0x90,", originalBytes.Length - assemblySize)).TrimEnd(',');

            Byte[] injectionBytes = EngineCore.GetInstance().Architecture.GetAssembler().Assemble(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit(), assembly + "\n" + noOps, address.ToIntPtr());
            EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(address.ToIntPtr(), injectionBytes);

            CodeCave codeCave = new CodeCave(address, 0, originalBytes);
            this.CodeCaves.Add(codeCave);

            return address;
        }

        /// <summary>
        /// Determines the address that a code cave would need to return to, if one were to be created at the specified address.
        /// </summary>
        /// <param name="address">The address of the code cave.</param>
        /// <returns>The address to which the code cave will return upon completion.</returns>
        public UInt64 GetCaveExitAddress(UInt64 address)
        {
            this.PrintDebugTag();

            Byte[] originalBytes = this.CollectOriginalBytes(address, MemoryCore.JumpSize);
            Int32 originalByteSize;

            if (originalBytes != null && originalBytes.Length < MemoryCore.JumpSize)
            {
                // Determine the size of the minimum number of instructions we will be overwriting
                originalByteSize = originalBytes.Length;
            }
            else
            {
                // Fall back if something goes wrong
                originalByteSize = MemoryCore.JumpSize;
            }

            address = address.ToIntPtr().Add(originalByteSize).ToUInt64();

            return address;
        }

        /// <summary>
        /// Removes a created code cave at the specified address.
        /// </summary>
        /// <param name="codeCaveAddress">The address of the code cave.</param>
        public void RemoveCodeCave(UInt64 codeCaveAddress)
        {
            this.PrintDebugTag();

            foreach (CodeCave codeCave in this.CodeCaves)
            {
                // If remote allocation address is unset, then it was not allocated. Also, if the addresses do not match, ignore it.
                if (codeCave.RemoteAllocationAddress == 0 || codeCave.Address != codeCaveAddress)
                {
                    continue;
                }

                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(codeCave.Address.ToIntPtr(), codeCave.OriginalBytes);

                EngineCore.GetInstance().OperatingSystemAdapter.DeallocateMemory(codeCave.RemoteAllocationAddress.ToIntPtr());
            }
        }

        /// <summary>
        /// Removes all created code caves by the parent script.
        /// </summary>
        public void RemoveAllCodeCaves()
        {
            this.PrintDebugTag();

            foreach (CodeCave codeCave in this.CodeCaves)
            {
                EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(codeCave.Address.ToIntPtr(), codeCave.OriginalBytes);

                // If remote allocation address is unset, then it was not allocated.
                if (codeCave.RemoteAllocationAddress == 0)
                {
                    continue;
                }

                EngineCore.GetInstance().OperatingSystemAdapter.DeallocateMemory(codeCave.RemoteAllocationAddress.ToIntPtr());
            }

            this.CodeCaves.Clear();
        }

        /// <summary>
        /// Binds a keyword to a given value for use in the script.
        /// </summary>
        /// <param name="keyword">The local keyword to bind.</param>
        /// <param name="address">The address to which the keyword is bound.</param>
        public void SetKeyword(String keyword, UInt64 address)
        {
            this.PrintDebugTag(keyword?.ToLower(), address.ToString("x"));

            String mapping = "0x" + Conversions.ToAddress(address);
            this.Keywords[keyword?.ToLower()] = mapping;
        }

        /// <summary>
        /// Binds a keyword to a given value for use in all scripts.
        /// </summary>
        /// <param name="globalKeyword">The global keyword to bind.</param>
        /// <param name="address">The address to which the keyword is bound.</param>
        public void SetGlobalKeyword(String globalKeyword, UInt64 address)
        {
            this.PrintDebugTag(globalKeyword?.ToLower(), address.ToString("x"));

            String mapping = "0x" + Conversions.ToAddress(address);
            MemoryCore.globalKeywords.Value[globalKeyword?.ToLower()] = mapping;
        }

        /// <summary>
        /// Gets the value of a keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns>The value of the keyword. If not found, returns 0.</returns>
        public UInt64 GetKeywordValue(String keyword)
        {
            if (String.IsNullOrWhiteSpace(keyword))
            {
                return 0;
            }

            String result = null;
            this.Keywords.TryGetValue(keyword?.ToLower(), out result);

            if (!CheckSyntax.CanParseAddress(result))
            {
                return 0;
            }

            return Conversions.ParseHexStringAsValue(typeof(UInt64), result);
        }

        /// <summary>
        /// Gets the value of a global keyword.
        /// </summary>
        /// <param name="keyword">The global keyword.</param>
        /// <returns>The value of the global keyword. If not found, returns 0.</returns>
        public UInt64 GetGlobalKeywordValue(String globalKeyword)
        {
            if (String.IsNullOrWhiteSpace(globalKeyword))
            {
                return 0;
            }

            String result = null;
            MemoryCore.globalKeywords.Value.TryGetValue(globalKeyword?.ToLower(), out result);

            if (!CheckSyntax.CanParseAddress(result))
            {
                return 0;
            }

            return Conversions.ParseHexStringAsValue(typeof(UInt64), result);
        }

        /// <summary>
        /// Clears the specified keyword created by the parent script.
        /// </summary>
        /// <param name="keyword">The local keyword to clear.</param>
        public void ClearKeyword(String keyword)
        {
            this.PrintDebugTag(keyword);

            String result;
            if (this.Keywords.ContainsKey(keyword))
            {
                this.Keywords.TryRemove(keyword, out result);
            }
        }

        /// <summary>
        /// Clears the specified global keyword created by any script.
        /// </summary>
        /// <param name="globalKeyword">The global keyword to clear.</param>
        public void ClearGlobalKeyword(String globalKeyword)
        {
            this.PrintDebugTag(globalKeyword);

            String valueRemoved;
            if (MemoryCore.globalKeywords.Value.ContainsKey(globalKeyword))
            {
                MemoryCore.globalKeywords.Value.TryRemove(globalKeyword, out valueRemoved);
            }
        }

        /// <summary>
        /// Clears all keywords created by the parent script.
        /// </summary>
        public void ClearAllKeywords()
        {
            this.PrintDebugTag();

            this.Keywords.Clear();
        }

        /// <summary>
        /// Clears all global keywords created by any script.
        /// </summary>
        public void ClearAllGlobalKeywords()
        {
            this.PrintDebugTag();

            MemoryCore.globalKeywords.Value.Clear();
        }

        /// <summary>
        /// Searches for the first address that matches the array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to search for.</param>
        /// <returns>The address of the first first array of byte match.</returns>
        public UInt64 SearchAob(Byte[] bytes)
        {
            this.PrintDebugTag();

            UInt64 address = EngineCore.GetInstance().OperatingSystemAdapter.SearchAob(bytes).ToUInt64();
            return address;
        }

        /// <summary>
        /// Searches for the first address that matches the given array of byte pattern.
        /// </summary>
        /// <param name="pattern">The pattern string for which to search.</param>
        /// <returns>The address of the first first pattern match.</returns>
        public UInt64 SearchAob(String pattern)
        {
            this.PrintDebugTag(pattern);

            return EngineCore.GetInstance().OperatingSystemAdapter.SearchAob(pattern).ToUInt64();
        }

        /// <summary>
        /// Searches for all addresses that match the given array of byte pattern.
        /// </summary>
        /// <param name="pattern">The array of bytes to search for.</param>
        /// <returns>The addresses of all matches.</returns>
        public UInt64[] SearchAllAob(String pattern)
        {
            this.PrintDebugTag(pattern);
            List<IntPtr> aobResults = new List<IntPtr>(EngineCore.GetInstance().OperatingSystemAdapter.SearchllAob(pattern));
            List<UInt64> convertedAobs = new List<UInt64>();
            aobResults.ForEach(x => convertedAobs.Add(x.ToUInt64()));
            return convertedAobs.ToArray();
        }

        /// <summary>
        /// Reads the value at the given address.
        /// </summary>
        /// <param name="address">The address of the read.</param>
        /// <returns>The value read from memory.</returns>
        public T ReadMemory<T>(UInt64 address)
        {
            this.PrintDebugTag(address.ToString("x"));

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.Read<T>(address.ToIntPtr(), out readSuccess);
        }

        /// <summary>
        /// Reads the array of bytes of the specified count at the given address.
        /// </summary>
        /// <param name="address">The address of the read.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The bytes read at the address.</returns>
        public Byte[] ReadMemory(UInt64 address, Int32 count)
        {
            this.PrintDebugTag(address.ToString("x"), count.ToString());

            Boolean readSuccess;
            return EngineCore.GetInstance().OperatingSystemAdapter.ReadBytes(address.ToIntPtr(), count, out readSuccess);
        }

        /// <summary>
        /// Writes the value at the specified address.
        /// </summary>
        /// <param name="address">The address of the write.</param>
        /// <param name="value">The value of the write.</param>
        public void WriteMemory<T>(UInt64 address, T value)
        {
            this.PrintDebugTag(address.ToString("x"), value.ToString());

            EngineCore.GetInstance().OperatingSystemAdapter.Write<T>(address.ToIntPtr(), value);
        }

        /// <summary>
        /// Writes the Byte array to the specified address
        /// </summary>
        /// <param name="address">The address of the write.</param>
        /// <param name="values">The values of the write.</param>
        public void WriteMemory(UInt64 address, Byte[] values)
        {
            this.PrintDebugTag(address.ToString("x"));

            EngineCore.GetInstance().OperatingSystemAdapter.WriteBytes(address.ToIntPtr(), values);
        }

        /// <summary>
        /// Replaces user provided keywords with their associated value.
        /// </summary>
        /// <param name="assembly">The assembly script.</param>
        /// <returns>The assembly script with all keywords replaced with their values.</returns>
        private String ResolveKeywords(String assembly)
        {
            if (assembly == null)
            {
                return String.Empty;
            }

            // Clear out any whitespace that may cause issues in the assembly script
            assembly = assembly.Replace("\t", String.Empty);

            // Resolve keywords
            foreach (KeyValuePair<String, String> keyword in this.Keywords)
            {
                assembly = assembly.Replace("<" + keyword.Key + ">", keyword.Value);
            }

            foreach (KeyValuePair<String, String> globalKeyword in MemoryCore.globalKeywords.Value.ToArray())
            {
                assembly = assembly.Replace("<" + globalKeyword.Key + ">", globalKeyword.Value);
            }

            return assembly;
        }

        /// <summary>
        /// Defines instructions replaced by a jump to a newly allocated region of memory, which will execute and return control.
        /// </summary>
        private struct CodeCave
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CodeCave" /> struct.
            /// </summary>
            /// <param name="codeCaveAddress">The address of the code cave allocation.</param>
            /// <param name="originalBytes">The original bytes being overwritten.</param>
            /// <param name="address">The entry address of the code cave.</param>
            public CodeCave(UInt64 address, UInt64 codeCaveAddress, Byte[] originalBytes)
            {
                this.RemoteAllocationAddress = codeCaveAddress;
                this.OriginalBytes = originalBytes;
                this.Address = address;
            }

            /// <summary>
            /// Gets or sets the original instruction bytes at the cave entry.
            /// </summary>
            public Byte[] OriginalBytes { get; set; }

            /// <summary>
            /// Gets or sets the address of the allocated code cave.
            /// </summary>
            public UInt64 RemoteAllocationAddress { get; set; }

            /// <summary>
            /// Gets or sets the entry address of the code cave.
            /// </summary>
            public UInt64 Address { get; set; }
        }
    }
    //// End interface
}
//// End namespace