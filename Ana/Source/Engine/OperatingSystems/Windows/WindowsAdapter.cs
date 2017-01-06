﻿namespace Ana.Source.Engine.OperatingSystems.Windows
{
    using Native;
    using Pe;
    using Pe.Structures;
    using Processes;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Utils.Extensions;
    using Utils.Validation;
    using static Native.Enumerations;
    using static Native.Structures;

    /// <summary>
    /// Class for memory editing a remote process.
    /// </summary>
    internal class WindowsAdapter : IOperatingSystemAdapter
    {
        private Process systemProcess;

        private PeFile peFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsAdapter"/> class
        /// </summary>
        public WindowsAdapter()
        {
            // Subscribe to process events (async call as to avoid locking on GetInstance() if engine is being constructed)
            Task.Run(() => { EngineCore.GetInstance().Processes.Subscribe(this); });
        }

        /// <summary>
        /// Gets a reference to the process running. This is an optimization to minimize accesses
        /// to the Processes component of the Engine
        /// </summary>
        public Process SystemProcess
        {
            get
            {
                try
                {
                    if (this.systemProcess != null && this.systemProcess.HasExited)
                    {
                        this.systemProcess = null;
                    }
                }
                catch
                {
                }

                return this.systemProcess;
            }

            private set
            {
                this.systemProcess = value;
                this.peFile = new PeFile(this.SystemProcess?.MainModule?.FileName);
            }
        }
        /// <summary>
        /// Recieve a process update. This is an optimization over grabbing the process from the <see cref="IProcesses"/> component
        /// of the <see cref="EngineCore"/> every time we need it, which would be cumbersome when doing hundreds of thousands of memory read/writes
        /// </summary>
        /// <param name="process">The newly selected process</param>
        public void Update(NormalizedProcess process)
        {
            try
            {
                this.SystemProcess = Process.GetProcessById(process.ProcessId);
            }
            catch
            {
                this.SystemProcess = null;
            }
        }

        #region Read
        /// <summary>
        /// Reads the value of a specified type in the remote process
        /// </summary>
        /// <param name="valueType">Type of value being read</param>
        /// <param name="address">The address where the value is read</param>
        /// <param name="success">Whether or not the read succeeded</param>
        /// <returns>A value.</returns>
        public dynamic Read(Type valueType, IntPtr address, out Boolean success)
        {
            dynamic value;

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Byte:
                    value = this.Read<Byte>(address, out success);
                    break;
                case TypeCode.SByte:
                    value = this.Read<SByte>(address, out success);
                    break;
                case TypeCode.Int16:
                    value = this.Read<Int16>(address, out success);
                    break;
                case TypeCode.Int32:
                    value = this.Read<Int32>(address, out success);
                    break;
                case TypeCode.Int64:
                    value = this.Read<Int64>(address, out success);
                    break;
                case TypeCode.UInt16:
                    value = this.Read<UInt16>(address, out success);
                    break;
                case TypeCode.UInt32:
                    value = this.Read<UInt32>(address, out success);
                    break;
                case TypeCode.UInt64:
                    value = this.Read<UInt64>(address, out success);
                    break;
                case TypeCode.Single:
                    value = this.Read<Single>(address, out success);
                    break;
                case TypeCode.Double:
                    value = this.Read<Double>(address, out success);
                    break;
                default:
                    value = "?";
                    success = false;
                    break;
            }

            if (!success)
            {
                value = "?";
            }

            return value;
        }

        /// <summary>
        /// Reads the value of a specified type in the remote process
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="address">The address where the value is read</param>
        /// <param name="success">Whether or not the read succeeded</param>
        /// <returns>A value.</returns>
        public T Read<T>(IntPtr address, out Boolean success)
        {
            Byte[] byteArray = this.ReadBytes(address, Conversions.GetTypeSize<T>(), out success);
            return Conversions.BytesToObject<T>(byteArray);
        }

        /// <summary>
        /// Reads an array of bytes in the remote process
        /// </summary>
        /// <param name="address">The address where the array is read</param>
        /// <param name="count">The number of cells</param>
        /// <param name="success">Whether or not the read succeeded</param>
        /// <returns>The array of bytes</returns>
        public Byte[] ReadBytes(IntPtr address, Int32 count, out Boolean success)
        {
            return Memory.ReadBytes(this.SystemProcess == null ? IntPtr.Zero : this.SystemProcess.Handle, address, count, out success);
        }

        /// <summary>
        /// Reads a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is read.</param>
        /// <param name="encoding">The encoding used.</param>
        /// <param name="success">Whether or not the read succeeded</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public String ReadString(IntPtr address, Encoding encoding, out Boolean success, Int32 maxLength = 512)
        {
            // Read the string
            String data = encoding.GetString(this.ReadBytes(address, maxLength, out success));

            // Search the end of the string
            Int32 end = data.IndexOf('\0');

            // Crop the string with this end
            return data.Substring(0, end);
        }

        #endregion

        #region Write
        /// <summary>
        /// Writes a value to memory in the opened process
        /// </summary>
        /// <param name="elementType">The data type to write</param>
        /// <param name="address">The address to write to</param>
        /// <param name="value">The value to write</param>
        public void Write(Type elementType, IntPtr address, dynamic value)
        {
            switch (Type.GetTypeCode(elementType))
            {
                case TypeCode.Byte:
                    this.Write<Byte>(address, value);
                    break;
                case TypeCode.SByte:
                    this.Write<SByte>(address, value);
                    break;
                case TypeCode.Int16:
                    this.Write<Int16>(address, value);
                    break;
                case TypeCode.Int32:
                    this.Write<Int32>(address, value);
                    break;
                case TypeCode.Int64:
                    this.Write<Int64>(address, value);
                    break;
                case TypeCode.UInt16:
                    this.Write<UInt16>(address, value);
                    break;
                case TypeCode.UInt32:
                    this.Write<UInt32>(address, value);
                    break;
                case TypeCode.UInt64:
                    this.Write<UInt64>(address, value);
                    break;
                case TypeCode.Single:
                    this.Write<Single>(address, value);
                    break;
                case TypeCode.Double:
                    this.Write<Double>(address, value);
                    break;
                default: return;
            }
        }

        /// <summary>
        /// Writes the values of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="address">The address where the value is written.</param>
        /// <param name="value">The value to write.</param>
        public unsafe void Write<T>(IntPtr address, T value)
        {
            Byte[] bytes;
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                    bytes = BitConverter.GetBytes((Byte)(Object)value);
                    break;
                case TypeCode.SByte:
                    bytes = BitConverter.GetBytes((SByte)(Object)value);
                    break;
                case TypeCode.Char:
                    bytes = Encoding.UTF8.GetBytes(new[] { (Char)(Object)value });
                    break;
                case TypeCode.Double:
                    bytes = BitConverter.GetBytes((Double)(Object)value);
                    break;
                case TypeCode.Int16:
                    bytes = BitConverter.GetBytes((Int16)(Object)value);
                    break;
                case TypeCode.Int32:
                    bytes = BitConverter.GetBytes((Int32)(Object)value);
                    break;
                case TypeCode.Int64:
                    bytes = BitConverter.GetBytes((Int64)(Object)value);
                    break;
                case TypeCode.Single:
                    bytes = BitConverter.GetBytes((Single)(Object)value);
                    break;
                case TypeCode.UInt16:
                    bytes = BitConverter.GetBytes((UInt16)(Object)value);
                    break;
                case TypeCode.UInt32:
                    bytes = BitConverter.GetBytes((UInt32)(Object)value);
                    break;
                case TypeCode.UInt64:
                    bytes = BitConverter.GetBytes((UInt64)(Object)value);
                    break;
                default:
                    throw new ArgumentException("Invalid type provided");
            }

            this.WriteBytes(address, bytes);
        }

        /// <summary>
        /// Write an array of bytes in the remote process
        /// </summary>
        /// <param name="address">The address where the array is written</param>
        /// <param name="byteArray">The array of bytes to write</param>
        public void WriteBytes(IntPtr address, Byte[] byteArray)
        {
            // Write the byte array
            Memory.WriteBytes(this.SystemProcess == null ? IntPtr.Zero : this.SystemProcess.Handle, address, byteArray);
        }

        /// <summary>
        /// Writes a string with a specified encoding in the remote process
        /// </summary>
        /// <param name="address">The address where the string is written</param>
        /// <param name="text">The text to write</param>
        /// <param name="encoding">The encoding used</param>
        public void WriteString(IntPtr address, String text, Encoding encoding)
        {
            // Write the text
            this.WriteBytes(address, encoding.GetBytes(text + '\0'));
        }

        /// <summary>
        /// Gets the address of the stack in the target process
        /// </summary>
        /// <returns>The stack address in the target process</returns>
        public IntPtr GetStackAddress()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the address(es) of the heap in the target process
        /// </summary>
        /// <returns>The heap addresses in the target process</returns>
        public IEnumerable<IntPtr> GetHeapAddresses()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allocates memory in the opened process
        /// </summary>
        /// <param name="size">The size of the memory allocation</param>
        /// <returns>A pointer to the location of the allocated memory</returns>
        public IntPtr AllocateMemory(Int32 size)
        {
            return Memory.Allocate(this.SystemProcess == null ? IntPtr.Zero : this.SystemProcess.Handle, IntPtr.Zero, size);
        }

        /// <summary>
        /// Allocates memory in the opened process
        /// </summary>
        /// <param name="size">The size of the memory allocation</param>
        /// <param name="allocAddress">The rough address of where the allocation should take place.</param>
        /// <returns>A pointer to the location of the allocated memory</returns>
        public IntPtr AllocateMemory(Int32 size, IntPtr allocAddress)
        {
            return Memory.Allocate(this.SystemProcess == null ? IntPtr.Zero : this.SystemProcess.Handle, allocAddress, size);
        }

        /// <summary>
        /// Deallocates memory in the opened process
        /// </summary>
        /// <param name="address">The address to perform the region wide deallocation</param>
        public void DeallocateMemory(IntPtr address)
        {
            Memory.Free(this.SystemProcess == null ? IntPtr.Zero : this.SystemProcess.Handle, address);
        }

        /// <summary>
        /// Gets regions of memory allocated in the remote process based on provided parameters
        /// </summary>
        /// <param name="requiredProtection">Protection flags required to be present</param>
        /// <param name="excludedProtection">Protection flags that must not be present</param>
        /// <param name="allowedTypes">Memory types that can be present</param>
        /// <param name="startAddress">The start address of the query range</param>
        /// <param name="endAddress">The end address of the query range</param>
        /// <returns>A collection of pointers to virtual pages in the target process</returns>
        public IEnumerable<NormalizedRegion> GetVirtualPages(
            MemoryProtectionEnum requiredProtection,
            MemoryProtectionEnum excludedProtection,
            MemoryTypeEnum allowedTypes,
            IntPtr startAddress,
            IntPtr endAddress)
        {
            MemoryProtectionFlags requiredFlags = 0;
            MemoryProtectionFlags excludedFlags = 0;

            if ((requiredProtection & MemoryProtectionEnum.Write) != 0)
            {
                requiredFlags |= MemoryProtectionFlags.ExecuteReadWrite;
                requiredFlags |= MemoryProtectionFlags.ReadWrite;
            }

            if ((requiredProtection & MemoryProtectionEnum.Execute) != 0)
            {
                requiredFlags |= MemoryProtectionFlags.Execute;
                requiredFlags |= MemoryProtectionFlags.ExecuteRead;
                requiredFlags |= MemoryProtectionFlags.ExecuteReadWrite;
                requiredFlags |= MemoryProtectionFlags.ExecuteWriteCopy;
            }

            if ((requiredProtection & MemoryProtectionEnum.CopyOnWrite) != 0)
            {
                requiredFlags |= MemoryProtectionFlags.WriteCopy;
                requiredFlags |= MemoryProtectionFlags.ExecuteWriteCopy;
            }

            if ((excludedProtection & MemoryProtectionEnum.Write) != 0)
            {
                excludedFlags |= MemoryProtectionFlags.ExecuteReadWrite;
                excludedFlags |= MemoryProtectionFlags.ReadWrite;
            }

            if ((excludedProtection & MemoryProtectionEnum.Execute) != 0)
            {
                excludedFlags |= MemoryProtectionFlags.Execute;
                excludedFlags |= MemoryProtectionFlags.ExecuteRead;
                excludedFlags |= MemoryProtectionFlags.ExecuteReadWrite;
                excludedFlags |= MemoryProtectionFlags.ExecuteWriteCopy;
            }

            if ((excludedProtection & MemoryProtectionEnum.CopyOnWrite) != 0)
            {
                excludedFlags |= MemoryProtectionFlags.WriteCopy;
                excludedFlags |= MemoryProtectionFlags.ExecuteWriteCopy;
            }

            return Memory.VirtualPages(this.SystemProcess == null ? IntPtr.Zero : this.SystemProcess.Handle, startAddress, endAddress, requiredFlags, excludedFlags, allowedTypes)
                .Select(x => new NormalizedRegion(x.BaseAddress, (Int32)x.RegionSize));
        }

        /// <summary>
        /// Gets all virtual pages in the opened process
        /// </summary>
        /// <returns>A collection of regions in the process</returns>
        public IEnumerable<NormalizedRegion> GetAllVirtualPages()
        {
            MemoryTypeEnum flags = MemoryTypeEnum.None | MemoryTypeEnum.Private | MemoryTypeEnum.Image | MemoryTypeEnum.Mapped;
            return this.GetVirtualPages(0, 0, flags, IntPtr.Zero, this.GetMaximumAddress());
        }

        /// <summary>
        /// Gets the maximum address possible in the target process
        /// </summary>
        /// <returns>The maximum address possible in the target process</returns>
        public IntPtr GetMaximumAddress()
        {
            if (IntPtr.Size == sizeof(Int32))
            {
                return unchecked(UInt32.MaxValue.ToIntPtr());
            }
            else if (IntPtr.Size == sizeof(Int64))
            {
                return unchecked(UInt64.MaxValue.ToIntPtr());
            }

            throw new Exception("Unable to determine maximum address");
        }

        /// <summary>
        /// Gets the maximum usermode address possible in the target process
        /// </summary>
        /// <returns>The maximum usermode address possible in the target process</returns>
        public IntPtr GetMaximumUserModeAddress()
        {
            if (EngineCore.GetInstance().Processes.IsOpenedProcess32Bit())
            {
                return unchecked((IntPtr)Int32.MaxValue);
            }
            else
            {
                return unchecked((IntPtr)Int64.MaxValue);
            }
        }

        /// <summary>
        /// Gets all modules in the opened process
        /// </summary>
        /// <returns>A collection of modules in the process</returns>
        public IEnumerable<NormalizedModule> GetModules()
        {
            List<NormalizedModule> normalizedModules = new List<NormalizedModule>();

            if (this.SystemProcess == null)
            {
                return normalizedModules;
            }

            // Query all modules in the target process
            IntPtr[] modulePointers = new IntPtr[0];
            Int32 bytesNeeded = 0;

            try
            {
                // Determine number of modules
                if (!Native.NativeMethods.EnumProcessModulesEx(this.SystemProcess.Handle, modulePointers, 0, out bytesNeeded, (UInt32)Enumerations.ModuleFilter.ListModulesAll))
                {
                    return normalizedModules;
                }

                Int32 totalNumberofModules = bytesNeeded / IntPtr.Size;
                modulePointers = new IntPtr[totalNumberofModules];

                if (Native.NativeMethods.EnumProcessModulesEx(this.SystemProcess.Handle, modulePointers, bytesNeeded, out bytesNeeded, (UInt32)Enumerations.ModuleFilter.ListModulesAll))
                {
                    for (Int32 index = 0; index < totalNumberofModules; index++)
                    {
                        StringBuilder moduleFilePath = new StringBuilder(1024);
                        Native.NativeMethods.GetModuleFileNameEx(this.SystemProcess.Handle, modulePointers[index], moduleFilePath, (UInt32)(moduleFilePath.Capacity));

                        String moduleName = Path.GetFileName(moduleFilePath.ToString());
                        ModuleInformation moduleInformation = new ModuleInformation();
                        Native.NativeMethods.GetModuleInformation(this.SystemProcess.Handle, modulePointers[index], out moduleInformation, (UInt32)(IntPtr.Size * (modulePointers.Length)));

                        // Convert to a normalized module and add it to our list
                        NormalizedModule module = new NormalizedModule(moduleName, moduleInformation.lpBaseOfDll, unchecked((Int32)moduleInformation.SizeOfImage));
                        normalizedModules.Add(module);
                    }
                }
            }
            catch
            {
                // TODO: Log to user
            }

            return normalizedModules;
        }

        /// <summary>
        /// Searches for an array of bytes in the opened process
        /// </summary>
        /// <param name="bytes">The byte array to search for</param>
        /// <returns>The address of the first match</returns>
        public IntPtr SearchAob(Byte[] bytes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches for an array of bytes in the opened process
        /// </summary>
        /// <param name="pattern">The string pattern to search for</param>
        /// <returns>The address of the first match</returns>
        public IntPtr SearchAob(String pattern)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches for an array of bytes in the opened process
        /// </summary>
        /// <param name="pattern">The string pattern to search for</param>
        /// <returns>The address of all matches</returns>
        public IEnumerable<IntPtr> SearchllAob(String pattern)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Determines if the operating system is 32 bit
        /// </summary>
        /// <returns>A boolean indicating if the OS is 32 bit or not</returns>
        public Boolean IsOperatingSystem32Bit()
        {
            return !Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines if the operating system is 64 bit
        /// </summary>
        /// <returns>A boolean indicating if the OS is 64 bit or not</returns>
        public Boolean IsOperatingSystem64Bit()
        {
            return Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines if this program is 32 bit
        /// </summary>
        /// <returns>A boolean indicating if this program is 32 bit or not</returns>
        public Boolean IsAnathena32Bit()
        {
            return !Environment.Is64BitProcess;
        }

        /// <summary>
        /// Determines if this program is 64 bit
        /// </summary>
        /// <returns>A boolean indicating if this program is 64 bit or not</returns>
        public Boolean IsAnathena64Bit()
        {
            return Environment.Is64BitProcess;
        }

        /// <summary>
        /// Determines if a process is 32 bit
        /// </summary>
        /// <param name="process">The process to check</param>
        /// <returns>Returns true if the process is 32 bit, otherwise false</returns>
        public Boolean IsProcess32Bit(NormalizedProcess process)
        {
            Boolean isWow64;

            // First do the simple check if seeing if the OS is 32 bit, in which case the process wont be 64 bit
            if (EngineCore.GetInstance().OperatingSystemAdapter.IsOperatingSystem32Bit())
            {
                return true;
            }

            // No process provided, assume 32 bit
            if (process == null)
            {
                return true;
            }

            try
            {
                Process systemProcess = Process.GetProcessById(process.ProcessId);

                if (systemProcess == null || !Native.NativeMethods.IsWow64Process(systemProcess.Handle, out isWow64))
                {
                    // Error, assume 32 bit
                    return true;
                }
            }
            catch
            {
                // Error, assume 32 bit
                return true;
            }

            return isWow64;
        }

        /// <summary>
        /// Determines if a process is 64 bit
        /// </summary>
        /// <param name="process">The process to check</param>
        /// <returns>Returns true if the process is 64 bit, otherwise false</returns>
        public Boolean IsProcess64Bit(NormalizedProcess process)
        {
            return !this.IsProcess32Bit(process);
        }

        /// <summary>
        /// Collects the window title of the running process.
        /// </summary>
        /// <returns>The window title of the running process.</returns>
        public String CollectWindowTitle()
        {
            return this.SystemProcess?.MainWindowTitle ?? String.Empty;
        }

        /// <summary>
        /// Collects the version from the binary. This information should be stored directly in the binary.
        /// </summary>
        /// <returns>The version of the target process, taken from the binary.</returns>
        public String CollectBinaryVersion()
        {
            String binaryVersion = String.Empty;

            try
            {
                binaryVersion = FileVersionInfo.GetVersionInfo(SystemProcess?.MainModule?.FileName)?.ProductVersion;
            }
            catch
            {
            }

            return binaryVersion ?? String.Empty;
        }

        /// <summary>
        /// Collects the binary header (such as the PE header) and creates a SHA-256 hash based on the header information.
        /// </summary>
        /// <returns>A SHA-256 hash computed from the header of the binary.</returns>
        public String CollectBinaryHeaderHash()
        {
            String binaryHeaderHash = String.Empty;

            try
            {
                binaryHeaderHash = peFile.SHA256;
            }
            catch
            {
            }

            return binaryHeaderHash ?? String.Empty;
        }

        /// <summary>
        /// Collects the binary import hash.
        /// </summary>
        /// <returns>A hash computed from the imports of the binary.</returns>
        public String CollectBinaryImportHash()
        {
            String binaryImportHash = String.Empty;

            try
            {
                binaryImportHash = peFile.ImpHash;
            }
            catch
            {
            }

            return binaryImportHash ?? String.Empty;
        }

        /// <summary>
        /// Collects a "hash", which is simply the first 128 bytes of the main module encoded in base-64.
        /// </summary>
        /// <returns>A hash based on the first 128 bytes of the main module.</returns>
        public String CollectMainModuleHash()
        {
            const Int32 BytesToRead = 128;

            String mainModuleHash = String.Empty;

            try
            {
                IMAGE_SECTION_HEADER textHeader = peFile.ImageSectionHeaders.Where(x => Encoding.UTF8.GetString(x?.Name).TrimEnd('\0') == ".text")?.First();
                UInt32 entryPointAddress = peFile.ImageNtHeaders.OptionalHeader.AddressOfEntryPoint;
                UInt32 pointerToRawData = textHeader.PointerToRawData;
                UInt32 virtualAddress = textHeader.VirtualAddress;

                // Formula: AddressOfRawEntryPoint (in exe file) = AddressOfEntryPoint + .text[PointerToRawData] - .text[VirtualAddress]
                Int32 entryPoint = unchecked((Int32)(entryPointAddress + pointerToRawData - virtualAddress));

                mainModuleHash = Convert.ToBase64String(File.ReadAllBytes(SystemProcess?.MainModule?.FileName).Skip(entryPoint).Take(BytesToRead).ToArray());
            }
            catch
            {
            }

            return mainModuleHash ?? String.Empty;
        }

        /// <summary>
        /// Collects a hash code to identify the game loaded in the running emulator.
        /// </summary>
        /// <returns>A hash identifying the game running in the target process emulator.</returns>
        public String CollectEmulatorHash()
        {
            String emulatorHash = String.Empty;

            // TODO: Here we could write some really advanced logic to determine what game is loaded in EVERY single possible emulator
            // It is currently unclear if this is a valid possibility. This functionality may be abandoned if determined to be a wasted effort.
            return emulatorHash;
        }
    }
    //// End class
}
//// End namespace