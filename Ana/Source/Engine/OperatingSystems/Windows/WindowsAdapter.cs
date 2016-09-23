namespace Ana.Source.Engine.OperatingSystems.Windows
{
    using Native;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using Utils.Extensions;
    using Utils.Validation;

    /// <summary>
    /// Class for memory editing a remote process.
    /// </summary>
    internal class WindowsAdapter : IOperatingSystemAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsAdapter"/> class
        /// </summary>
        public WindowsAdapter()
        {
            this.Handle = Memory.OpenProcess(ProcessAccessFlags.AllAccess, EngineCore.GetInstance().Processes.GetOpenedProcess());
        }

        /// <summary>
        /// Gets the remote process handle opened with all rights.
        /// </summary>
        public IntPtr Handle { get; private set; }

        #region Read
        /// <summary>
        /// Reads the value of a specified type in the remote process
        /// </summary>
        /// <param name="valueType">Type of value being read</param>
        /// <param name="address">The address where the value is read</param>
        /// <param name="success">Whether or not the read succeeded</param>
        /// <returns>A value.</returns>
        [Obfuscation(Exclude = true)]
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
            return Memory.ReadBytes(this.Handle, address, count, out success);
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
        [Obfuscation(Exclude = true)]
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
                    bytes = BitConverter.GetBytes((Boolean)(Object)value);
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
            Memory.WriteBytes(this.Handle, address, byteArray);
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
            return Memory.Allocate(this.Handle, size);
        }

        /// <summary>
        /// Deallocates memory in the opened process
        /// </summary>
        /// <param name="address">The address to perform the region wide deallocation</param>
        public void DeallocateMemory(IntPtr address)
        {
            Memory.Free(this.Handle, address);
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

            List<IntPtr> pages = new List<IntPtr>(Memory.VirtualPages(this.Handle, startAddress, endAddress, requiredFlags, excludedFlags, allowedTypes));
            List<NormalizedRegion> regions = new List<NormalizedRegion>();
            pages.ForEach(page => regions.Add(new NormalizedRegion(page, (Int32)Memory.Query(this.Handle, page).RegionSize)));

            return regions;
        }

        /// <summary>
        /// Gets all virtual pages in the opened process
        /// </summary>
        /// <returns>A collection of regions in the process</returns>
        public IEnumerable<NormalizedRegion> GetAllVirtualPages()
        {
            return this.GetVirtualPages(0, 0, MemoryTypeEnum.None | MemoryTypeEnum.Private | MemoryTypeEnum.Image | MemoryTypeEnum.Mapped, IntPtr.Zero, IntPtr.Zero.MaxValue());
        }

        /// <summary>
        /// Gets all modules in the opened process
        /// </summary>
        /// <returns>A collection of modules in the process</returns>
        public IEnumerable<NormalizedModule> GetModules()
        {
            List<NormalizedModule> normalizedModules = new List<NormalizedModule>();

            // Just doing this so I dont get any warnings. Fix the process design...
            if (normalizedModules.Count <= 0)
            {
                throw new NotImplementedException();
            }

            //// Process?.Modules?.Cast<ProcessModule>().ForEach(X => normalizedModules.Add(new NormalizedModule(X.ModuleName, X.BaseAddress, X.ModuleMemorySize)));

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
        public Boolean IsOS32Bit()
        {
            return !Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines if the operating system is 64 bit
        /// </summary>
        /// <returns>A boolean indicating if the OS is 64 bit or not</returns>
        public Boolean IsOS64Bit()
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
    }
    //// End class
}
//// End namespace