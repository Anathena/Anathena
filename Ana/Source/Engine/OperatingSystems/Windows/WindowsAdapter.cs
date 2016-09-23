namespace Ana.Source.Engine.OperatingSystems.Windows
{
    using Native;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
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
        /// The remote process handle opened with all rights.
        /// </summary>
        public IntPtr Handle { get; private set; }
        /// <summary>
        /// Provide access to the opened process.
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsAdapter"/> class.
        /// </summary>
        /// <param name="Process">Process to open.</param>
        public WindowsAdapter(Process Process)
        {
            // Save the reference of the process
            this.Process = Process;

            Handle = Memory.OpenProcess(ProcessAccessFlags.AllAccess, Process);
        }

        #region Read
        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="ValueType">Type of value being read.</param>
        /// <param name="Address">The address where the value is read.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A value.</returns>
        [Obfuscation(Exclude = true)]
        public dynamic Read(Type ValueType, IntPtr Address, out Boolean Success)
        {
            dynamic Value;

            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: Value = Read<Byte>(Address, out Success); break;
                case TypeCode.SByte: Value = Read<SByte>(Address, out Success); break;
                case TypeCode.Int16: Value = Read<Int16>(Address, out Success); break;
                case TypeCode.Int32: Value = Read<Int32>(Address, out Success); break;
                case TypeCode.Int64: Value = Read<Int64>(Address, out Success); break;
                case TypeCode.UInt16: Value = Read<UInt16>(Address, out Success); break;
                case TypeCode.UInt32: Value = Read<UInt32>(Address, out Success); break;
                case TypeCode.UInt64: Value = Read<UInt64>(Address, out Success); break;
                case TypeCode.Single: Value = Read<Single>(Address, out Success); break;
                case TypeCode.Double: Value = Read<Double>(Address, out Success); break;
                default: Value = "?"; Success = false; break;
            }

            if (!Success)
                Value = "?";

            return Value;
        }

        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="Address">The address where the value is read.</param>
        /// <returns>A value.</returns>
        public T Read<T>(IntPtr Address, out Boolean Success)
        {
            Byte[] ByteArray = ReadBytes(Address, Conversions.GetTypeSize<T>(), out Success);
            return Conversions.BytesToObject<T>(ByteArray);
        }

        /// <summary>
        /// Reads an array of bytes in the remote process.
        /// </summary>
        /// <param name="Address">The address where the array is read.</param>
        /// <param name="Count">The number of cells.</param>
        /// <returns>The array of bytes.</returns>
        public Byte[] ReadBytes(IntPtr Address, Int32 Count, out Boolean Success)
        {
            return Memory.ReadBytes(Handle, Address, Count, out Success);
        }

        /// <summary>
        /// Reads a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is read.</param>
        /// <param name="Encoding">The encoding used.</param>
        /// <param name="MaxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public String ReadString(IntPtr Address, Encoding Encoding, out Boolean Success, Int32 MaxLength = 512)
        {
            // Read the string
            String Data = Encoding.GetString(ReadBytes(Address, MaxLength, out Success));

            // Search the end of the string
            Int32 End = Data.IndexOf('\0');

            // Crop the string with this end
            return Data.Substring(0, End);
        }

        #endregion

        #region Write
        [Obfuscation(Exclude = true)]
        public void Write(Type ValueType, IntPtr Address, dynamic Value)
        {
            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: Write<Byte>(Address, Value); break;
                case TypeCode.SByte: Write<SByte>(Address, Value); break;
                case TypeCode.Int16: Write<Int16>(Address, Value); break;
                case TypeCode.Int32: Write<Int32>(Address, Value); break;
                case TypeCode.Int64: Write<Int64>(Address, Value); break;
                case TypeCode.UInt16: Write<UInt16>(Address, Value); break;
                case TypeCode.UInt32: Write<UInt32>(Address, Value); break;
                case TypeCode.UInt64: Write<UInt64>(Address, Value); break;
                case TypeCode.Single: Write<Single>(Address, Value); break;
                case TypeCode.Double: Write<Double>(Address, Value); break;
                default: return;
            }
        }

        /// <summary>
        /// Writes the values of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="Address">The address where the value is written.</param>
        /// <param name="Value">The value to write.</param>
        public unsafe void Write<T>(IntPtr Address, T Value)
        {
            Byte[] Bytes;
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean: Bytes = BitConverter.GetBytes((Boolean)(Object)Value); break;
                case TypeCode.Char: Bytes = Encoding.UTF8.GetBytes(new[] { (Char)(Object)Value }); break;
                case TypeCode.Double: Bytes = BitConverter.GetBytes((Double)(Object)Value); break;
                case TypeCode.Int16: Bytes = BitConverter.GetBytes((Int16)(Object)Value); break;
                case TypeCode.Int32: Bytes = BitConverter.GetBytes((Int32)(Object)Value); break;
                case TypeCode.Int64: Bytes = BitConverter.GetBytes((Int64)(Object)Value); break;
                case TypeCode.Single: Bytes = BitConverter.GetBytes((Single)(Object)Value); break;
                case TypeCode.UInt16: Bytes = BitConverter.GetBytes((UInt16)(Object)Value); break;
                case TypeCode.UInt32: Bytes = BitConverter.GetBytes((UInt32)(Object)Value); break;
                case TypeCode.UInt64: Bytes = BitConverter.GetBytes((UInt64)(Object)Value); break;
                default: throw new ArgumentException("Invalid type provided");
            }
            WriteBytes(Address, Bytes);
        }

        /// <summary>
        /// Write an array of bytes in the remote process.
        /// </summary>
        /// <param name="Address">The address where the array is written.</param>
        /// <param name="ByteArray">The array of bytes to write.</param>
        public void WriteBytes(IntPtr Address, Byte[] ByteArray)
        {
            // Write the byte array
            Memory.WriteBytes(Handle, Address, ByteArray);
        }

        /// <summary>
        /// Writes a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is written.</param>
        /// <param name="Text">The text to write.</param>
        /// <param name="Encoding">The encoding used.</param>
        public void WriteString(IntPtr Address, String Text, Encoding Encoding)
        {
            // Write the text
            WriteBytes(Address, Encoding.GetBytes(Text + '\0'));
        }

        /// <summary>
        /// Gets the address of the stack in the windows process
        /// </summary>
        /// <returns></returns>
        public IntPtr GetStackAddress()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the address(es) of the heap in the windows process
        /// </summary>
        /// <returns></returns>
        public IntPtr[] GetHeapAddresses()
        {
            throw new NotImplementedException();
        }

        public Process GetProcess()
        {
            return Process;
        }

        public Boolean IsProcess32Bit()
        {
            // First do the simple check if seeing if the OS is 32 bit, in which case the process wont be 64 bit
            if (!Environment.Is64BitOperatingSystem)
                return true;

            Boolean IsWow64;
            if (!NativeMethods.IsWow64Process(Process == null ? IntPtr.Zero : Process.Handle, out IsWow64))
                return true; // Error, assume 32 bit

            return IsWow64;
        }

        public Boolean IsProcess64Bit()
        {
            return !IsProcess32Bit();
        }

        public IntPtr AllocateMemory(Int32 Size)
        {
            return Memory.Allocate(Handle, Size);
        }

        public void DeallocateMemory(IntPtr Address)
        {
            Memory.Free(Handle, Address);
        }

        public IEnumerable<NormalizedRegion> GetVirtualPages(MemoryProtectionEnum RequiredProtection, MemoryProtectionEnum ExcludedProtection,
            MemoryTypeEnum AllowedTypes, IntPtr StartAddress, IntPtr EndAddress)
        {
            MemoryProtectionFlags RequiredFlags = 0;
            MemoryProtectionFlags ExcludedFlags = 0;

            if ((RequiredProtection & MemoryProtectionEnum.Write) != 0)
            {
                RequiredFlags |= MemoryProtectionFlags.ExecuteReadWrite;
                RequiredFlags |= MemoryProtectionFlags.ReadWrite;
            }

            if ((RequiredProtection & MemoryProtectionEnum.Execute) != 0)
            {
                RequiredFlags |= MemoryProtectionFlags.Execute;
                RequiredFlags |= MemoryProtectionFlags.ExecuteRead;
                RequiredFlags |= MemoryProtectionFlags.ExecuteReadWrite;
                RequiredFlags |= MemoryProtectionFlags.ExecuteWriteCopy;
            }

            if ((RequiredProtection & MemoryProtectionEnum.CopyOnWrite) != 0)
            {
                RequiredFlags |= MemoryProtectionFlags.WriteCopy;
                RequiredFlags |= MemoryProtectionFlags.ExecuteWriteCopy;
            }

            if ((ExcludedProtection & MemoryProtectionEnum.Write) != 0)
            {
                ExcludedFlags |= MemoryProtectionFlags.ExecuteReadWrite;
                ExcludedFlags |= MemoryProtectionFlags.ReadWrite;
            }

            if ((ExcludedProtection & MemoryProtectionEnum.Execute) != 0)
            {
                ExcludedFlags |= MemoryProtectionFlags.Execute;
                ExcludedFlags |= MemoryProtectionFlags.ExecuteRead;
                ExcludedFlags |= MemoryProtectionFlags.ExecuteReadWrite;
                ExcludedFlags |= MemoryProtectionFlags.ExecuteWriteCopy;
            }

            if ((ExcludedProtection & MemoryProtectionEnum.CopyOnWrite) != 0)
            {
                ExcludedFlags |= MemoryProtectionFlags.WriteCopy;
                ExcludedFlags |= MemoryProtectionFlags.ExecuteWriteCopy;
            }

            List<IntPtr> Pages = new List<IntPtr>(Memory.VirtualPages(Handle, StartAddress, EndAddress, RequiredFlags, ExcludedFlags, AllowedTypes));
            List<NormalizedRegion> Regions = new List<NormalizedRegion>();
            Pages.ForEach(X => Regions.Add(new NormalizedRegion(X, (Int32)Memory.Query(Handle, X).RegionSize)));

            return Regions;
        }

        public IEnumerable<NormalizedRegion> GetAllVirtualPages()
        {
            return GetVirtualPages(0, 0, MemoryTypeEnum.None | MemoryTypeEnum.Private | MemoryTypeEnum.Image | MemoryTypeEnum.Mapped, IntPtr.Zero, IntPtr.Zero.MaxValue());
        }

        public IEnumerable<NormalizedModule> GetModules()
        {
            List<NormalizedModule> NormalizedModules = new List<NormalizedModule>();
            Process?.Modules?.Cast<ProcessModule>().ForEach(X => NormalizedModules.Add(new NormalizedModule(X.ModuleName, X.BaseAddress, X.ModuleMemorySize)));

            return NormalizedModules;
        }

        public IntPtr SearchAOB(Byte[] Bytes)
        {
            throw new NotImplementedException();
        }

        public IntPtr SearchAOB(String Pattern)
        {
            throw new NotImplementedException();
        }

        public IntPtr[] SearchllAOB(String Pattern)
        {
            throw new NotImplementedException();
        }

        #endregion


        public Boolean IsOS32Bit()
        {
            return !Environment.Is64BitOperatingSystem;
        }

        public Boolean IsOS64Bit()
        {
            return Environment.Is64BitOperatingSystem;
        }

        public Boolean IsAnathena32Bit()
        {
            return !Environment.Is64BitProcess;
        }

        public Boolean IsAnathena64Bit()
        {
            return Environment.Is64BitProcess;
        }

        public Boolean IsProcess32Bit(Process Process)
        {
            // First do the simple check if seeing if the OS is 32 bit, in which case the process wont be 64 bit
            if (IsOS32Bit())
                return true;

            Boolean IsWow64;
            if (!NativeMethods.IsWow64Process(Process.Handle, out IsWow64))
                return false; // Error

            return IsWow64;
        }

        public Boolean IsProcess64Bit(Process Process)
        {
            return !IsProcess32Bit(Process);
        }
    }
    //// End class
}
//// End namespace