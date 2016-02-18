using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Anathema.MemoryManagement.Assembly;
using Anathema.MemoryManagement.Helpers;
using Anathema.MemoryManagement.Internals;
using Anathema.MemoryManagement.Memory;
using Anathema.MemoryManagement.Modules;
using Anathema.MemoryManagement.Native;
using System.Runtime.InteropServices;

namespace Anathema.MemoryManagement
{
    /// <summary>
    /// Class for memory editing a remote process.
    /// </summary>
    public class WindowsOSInterface : IOperatingSystemInterface, IDisposable, IEquatable<WindowsOSInterface>
    {
        /// <summary>
        /// Raises when the <see cref="WindowsOSInterface"/> object is disposed.
        /// </summary>
        public event EventHandler OnDispose;

        /// <summary>
        /// The factories embedded inside the library.
        /// </summary>
        protected List<IFactory> Factories;

        /// <summary>
        /// State if the process is running.
        /// </summary>
        public bool IsRunning
        {
            get { return !Handle.IsInvalid && !Handle.IsClosed && !Native.HasExited; }
        }
        /// <summary>
        /// The remote process handle opened with all rights.
        /// </summary>
        public SafeMemoryHandle Handle { get; private set; }
        /// <summary>
        /// Factory for manipulating memory space.
        /// </summary>
        public MemoryFactory Memory { get; protected set; }
        /// <summary>
        /// Factory for manipulating modules and libraries.
        /// </summary>
        public ModuleFactory Modules { get; protected set; }
        /// <summary>
        /// Provide access to the opened process.
        /// </summary>
        public Process Native { get; private set; }
        /// <summary>
        /// Gets the unique identifier for the remote process.
        /// </summary>
        public int Pid { get { return Native.Id; } }
        /// <summary>
        /// Gets the specified module in the remote process.
        /// </summary>
        /// <param name="ModuleName">The name of module (not case sensitive).</param>
        /// <returns>A new instance of a <see cref="RemoteModule"/> class.</returns>
        public RemoteModule this[String ModuleName]
        {
            get { return Modules[ModuleName]; }
        }

        /// <summary>
        /// Gets a pointer to the specified address in the remote process.
        /// </summary>
        /// <param name="Address">The address pointed.</param>
        /// <returns>A new instance of a <see cref="RemotePointer"/> class.</returns>
        public RemotePointer this[IntPtr Address]
        {
            get { return new RemotePointer(this, Address); }
        }

        #region Constructors/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsOSInterface"/> class.
        /// </summary>
        /// <param name="Process">Process to open.</param>
        public WindowsOSInterface(Process Process)
        {
            // Save the reference of the process
            Native = Process;

            // Open the process with all rights
            Handle = MemoryCore.OpenProcess(ProcessAccessFlags.AllAccess, Process.Id);

            // Create instances of the factories
            Factories = new List<IFactory>();
            Factories.AddRange(new IFactory[]
            {
                Memory = new MemoryFactory(this),
                Modules = new ModuleFactory(this),
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsOSInterface"/> class.
        /// </summary>
        /// <param name="ProcessId">Process id of the process to open.</param>
        public WindowsOSInterface(Int32 ProcessId) : this(ApplicationFinder.FromProcessId(ProcessId))
        {

        }

        /// <summary>
        /// Frees resources and perform other cleanup operations before it is reclaimed by garbage collection. 
        /// </summary>
        ~WindowsOSInterface()
        {
            Dispose();
        }

        #endregion

        #region Methods
        #region Dispose (implementation of IDisposable)
        /// <summary>
        /// Releases all resources used by the <see cref="WindowsOSInterface"/> object.
        /// </summary>
        public virtual void Dispose()
        {
            // Raise the event OnDispose
            if (OnDispose != null)
                OnDispose(this, new EventArgs());

            // Dispose all factories
            Factories.ForEach(factory => factory.Dispose());

            // Close the process handle
            Handle.Close();

            // Avoid the finalizer
            GC.SuppressFinalize(this);
        }

        #endregion
        #region Equals (override)
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override Boolean Equals(Object Object)
        {
            if (ReferenceEquals(null, Object)) return false;
            if (ReferenceEquals(this, Object)) return true;
            return Object.GetType() == GetType() && Equals((WindowsOSInterface)Object);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        public Boolean Equals(WindowsOSInterface Other)
        {
            if (ReferenceEquals(null, Other)) return false;
            return ReferenceEquals(this, Other) || Handle.Equals(Other.Handle);
        }

        #endregion
        #region GetHashCode (override)
        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        public override Int32 GetHashCode()
        {
            return Handle.GetHashCode();
        }

        #endregion
        #region Operator (override)
        public static Boolean operator ==(WindowsOSInterface Left, WindowsOSInterface Right)
        {
            return Equals(Left, Right);
        }

        public static Boolean operator !=(WindowsOSInterface Left, WindowsOSInterface Right)
        {
            return !Equals(Left, Right);
        }

        #endregion
        #region Read

        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="ValueType">Type of value being read.</param>
        /// <param name="Address">The address where the value is read.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A value.</returns>
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
            return MarshalType<T>.ByteArrayToObject(ReadBytes(Address, MarshalType<T>.Size, out Success));
        }

        /// <summary>
        /// Reads an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="Address">The address where the values is read.</param>
        /// <param name="Count">The number of cells in the array.</param>
        /// <returns>An array.</returns>
        public T[] Read<T>(IntPtr Address, Int32 Count, out Boolean Success)
        {
            // Allocate an array to store the results
            T[] Array = new T[Count];
            Success = true;

            // Read the array in the remote process
            for (Int32 Index = 0; Index < Count; Index++)
            {
                Boolean Result;
                Array[Index] = Read<T>(Address + MarshalType<T>.Size * Index, out Result);
                Success &= Result;
            }

            return Array;
        }

        /// <summary>
        /// Reads an array of bytes in the remote process.
        /// </summary>
        /// <param name="Address">The address where the array is read.</param>
        /// <param name="Count">The number of cells.</param>
        /// <returns>The array of bytes.</returns>
        public byte[] ReadBytes(IntPtr Address, Int32 Count, out Boolean Success)
        {
            return MemoryCore.ReadBytes(Handle, Address, Count, out Success);
        }

        #endregion
        #region ReadString
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

        /// <summary>
        /// Reads a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is read.</param>
        /// <param name="MaxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(IntPtr Address, out Boolean Success, Int32 MaxLength = 512)
        {
            return ReadString(Address, Encoding.UTF8, out Success, MaxLength);
        }

        #endregion
        #region Write

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
        public void Write<T>(IntPtr Address, T Value)
        {
            WriteBytes(Address, MarshalType<T>.ObjectToByteArray(Value));
        }

        /// <summary>
        /// Writes an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="Address">The address where the values is written.</param>
        /// <param name="Array">The array to write.</param>
        public void Write<T>(IntPtr Address, T[] Array)
        {
            // Write the array in the remote process
            for (Int32 Index = 0; Index < Array.Length; Index++)
            {
                Write(Address + MarshalType<T>.Size * Index, Array[Index]);
            }
        }

        /// <summary>
        /// Write an array of bytes in the remote process.
        /// </summary>
        /// <param name="Address">The address where the array is written.</param>
        /// <param name="ByteArray">The array of bytes to write.</param>
        public void WriteBytes(IntPtr Address, Byte[] ByteArray)
        {
            // Change the protection of the memory to allow writable
            using (new MemoryProtection(this, Address, MarshalType<byte>.Size * ByteArray.Length))
            {
                // Write the byte array
                MemoryCore.WriteBytes(Handle, Address, ByteArray);
            }
        }

        #endregion
        #region WriteString
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
        /// Writes a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is written.</param>
        /// <param name="Text">The text to write.</param>
        public void WriteString(IntPtr Address, String Text)
        {
            WriteString(Address, Text, Encoding.UTF8);
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

        public String GetProcessName()
        {
            return Native.ProcessName;
        }

        public Boolean Is32Bit()
        {
            // First do the simple check if seeing if the OS is 32 bit, in which case the process wont be 64 bit
            if (!Environment.Is64BitOperatingSystem)
                return true;

            Boolean IsWow64;
            if (!IsWow64Process(Native.Handle, out IsWow64))
                return false; // Error
            return IsWow64;
        }

        public Boolean Is64Bit()
        {
            return !Is32Bit();
        }

        public IntPtr AllocateMemory(Int32 Size)
        {
            return MemoryCore.Allocate(Handle, Size);
        }

        public void DeallocateMemory(IntPtr Address)
        {
            MemoryCore.Free(Handle, Address);
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

            List<RemoteVirtualPage> Pages = new List<RemoteVirtualPage>(Memory.VirtualPages(StartAddress, EndAddress, RequiredFlags, ExcludedFlags, AllowedTypes));
            List<NormalizedRegion> Regions = new List<NormalizedRegion>();
            Pages.ForEach(x => Regions.Add(new NormalizedRegion(x.BaseAddress, (Int32)x.Information.RegionSize)));

            return Regions;
        }

        public IEnumerable<NormalizedRegion> GetAllVirtualPages()
        {
            return GetVirtualPages(0, 0, MemoryTypeEnum.None | MemoryTypeEnum.Private | MemoryTypeEnum.Image | MemoryTypeEnum.Mapped, IntPtr.Zero, IntPtr.Zero.MaxValue());
        }
        
        public IEnumerable<NormalizedModule> GetModules()
        {
            List<NormalizedModule> NormalizedModules = new List<NormalizedModule>();

            foreach (RemoteModule Module in Modules.RemoteModules)
                NormalizedModules.Add(new NormalizedModule(Module.Name, Module.BaseAddress, Module.Size));

            return NormalizedModules;
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr ProcessHandle, [Out, MarshalAs(UnmanagedType.Bool)] out bool Wow64Process);

        public IntPtr SearchAOB(byte[] Bytes)
        {
            throw new NotImplementedException();
        }

        public IntPtr SearchAOB(string Pattern)
        {
            throw new NotImplementedException();
        }

        public IntPtr[] SearchllAOB(string Pattern)
        {
            throw new NotImplementedException();
        }


        #endregion
        #endregion

    } // End class

} // End namespace