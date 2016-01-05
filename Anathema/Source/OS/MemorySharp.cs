/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Binarysharp.MemoryManagement.Assembly;
using Binarysharp.MemoryManagement.Helpers;
using Binarysharp.MemoryManagement.Internals;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Modules;
using Binarysharp.MemoryManagement.Native;
using Binarysharp.MemoryManagement.Threading;

namespace Binarysharp.MemoryManagement
{
    /// <summary>
    /// Class for memory editing a remote process.
    /// </summary>
    public class MemorySharp : IDisposable, IEquatable<MemorySharp>
    {
        #region Events
        /// <summary>
        /// Raises when the <see cref="MemorySharp"/> object is disposed.
        /// </summary>
        public event EventHandler OnDispose;
        #endregion

        #region Fields
        /// <summary>
        /// The factories embedded inside the library.
        /// </summary>
        protected List<IFactory> Factories;
        #endregion

        #region Properties
        #region Assembly
        /// <summary>
        /// Factory for generating assembly code.
        /// </summary>
        public AssemblyFactory Assembly { get; protected set; }
        #endregion
        #region IsDebugged
        /// <summary>
        /// Gets whether the process is being debugged.
        /// </summary>
        public bool IsDebugged
        {
            get { return Peb.BeingDebugged; }
            set { Peb.BeingDebugged = value; }
        }
        #endregion
        #region IsRunning
        /// <summary>
        /// State if the process is running.
        /// </summary>
        public bool IsRunning
        {
            get { return !Handle.IsInvalid && !Handle.IsClosed && !Native.HasExited; }
        }
        #endregion
        #region Handle
        /// <summary>
        /// The remote process handle opened with all rights.
        /// </summary>
        public SafeMemoryHandle Handle { get; private set; }
        #endregion
        #region Memory
        /// <summary>
        /// Factory for manipulating memory space.
        /// </summary>
        public MemoryFactory Memory { get; protected set; }
        #endregion
        #region Modules
        /// <summary>
        /// Factory for manipulating modules and libraries.
        /// </summary>
        public ModuleFactory Modules { get; protected set; }
        #endregion
        #region Native
        /// <summary>
        /// Provide access to the opened process.
        /// </summary>
        public Process Native { get; private set; }
        #endregion
        #region Peb
        /// <summary>
        /// The Process Environment Block of the process.
        /// </summary>
        public ManagedPeb Peb { get; private set; }
        #endregion
        #region Pid
        /// <summary>
        /// Gets the unique identifier for the remote process.
        /// </summary>
        public int Pid
        {
            get { return Native.Id; }
        }
        #endregion
        #region This
        /// <summary>
        /// Gets the specified module in the remote process.
        /// </summary>
        /// <param name="moduleName">The name of module (not case sensitive).</param>
        /// <returns>A new instance of a <see cref="RemoteModule"/> class.</returns>
        public RemoteModule this[string moduleName]
        {
            get { return Modules[moduleName]; }
        }
        /// <summary>
        /// Gets a pointer to the specified address in the remote process.
        /// </summary>
        /// <param name="address">The address pointed.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A new instance of a <see cref="RemotePointer"/> class.</returns>
        public RemotePointer this[IntPtr address, bool isRelative = true]
        {
            get { return new RemotePointer(this, isRelative ? MakeAbsolute(address) : address); }
        }
        #endregion
        #region Threads
        /// <summary>
        /// Factory for manipulating threads.
        /// </summary>
        public ThreadFactory Threads { get; protected set; }
        #endregion
        #endregion

        #region Constructors/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MemorySharp"/> class.
        /// </summary>
        /// <param name="process">Process to open.</param>
        public MemorySharp(Process process)
        {
            // Save the reference of the process
            Native = process;
            // Open the process with all rights
            Handle = MemoryCore.OpenProcess(ProcessAccessFlags.AllAccess, process.Id);
            // Initialize the PEB
            Peb = new ManagedPeb(this, ManagedPeb.FindPeb(Handle));
            // Create instances of the factories
            Factories = new List<IFactory>();
            Factories.AddRange(
                new IFactory[] {
                    Assembly = new AssemblyFactory(this),
                    Memory = new MemoryFactory(this),
                    Modules = new ModuleFactory(this),
                    Threads = new ThreadFactory(this),
                });
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MemorySharp"/> class.
        /// </summary>
        /// <param name="processId">Process id of the process to open.</param>
        public MemorySharp(int processId)
            : this(ApplicationFinder.FromProcessId(processId))
        { }
        /// <summary>
        /// Frees resources and perform other cleanup operations before it is reclaimed by garbage collection. 
        /// </summary>
        ~MemorySharp()
        {
            Dispose();
        }
        #endregion

        #region Methods
        #region Dispose (implementation of IDisposable)
        /// <summary>
        /// Releases all resources used by the <see cref="MemorySharp"/> object.
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
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((MemorySharp)obj);
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        public bool Equals(MemorySharp other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Handle.Equals(other.Handle);
        }
        #endregion
        #region GetHashCode (override)
        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        #endregion
        #region MakeAbsolute
        /// <summary>
        /// Makes an absolute address from a relative one based on the main module.
        /// </summary>
        /// <param name="address">The relative address.</param>
        /// <returns>The absolute address.</returns>
        public IntPtr MakeAbsolute(IntPtr address)
        {
            // Check if the relative address is not greater than the main module size
            //if (address.ToInt64() > Modules.MainModule.Size)
            //    throw new ArgumentOutOfRangeException("address", "The relative address cannot be greater than the main module size.");
            // Compute the absolute address
            return new IntPtr(Modules.MainModule.BaseAddress.ToInt64() + address.ToInt64());
        }
        #endregion
        #region MakeRelative
        /// <summary>
        /// Makes a relative address from an absolute one based on the main module.
        /// </summary>
        /// <param name="address">The absolute address.</param>
        /// <returns>The relative address.</returns>
        public IntPtr MakeRelative(IntPtr address)
        {
            // Check if the absolute address is smaller than the main module base address
            if (address.ToInt64() < Modules.MainModule.BaseAddress.ToInt64())
                throw new ArgumentOutOfRangeException("address", "The absolute address cannot be smaller than the main module base address.");
            // Compute the relative address
            return new IntPtr(address.ToInt64() - Modules.MainModule.BaseAddress.ToInt64());
        }
        #endregion
        #region Operator (override)
        public static bool operator ==(MemorySharp left, MemorySharp right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MemorySharp left, MemorySharp right)
        {
            return !Equals(left, right);
        }
        #endregion
        #region Read

        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="ValueType">Type of value being read.</param>
        /// <param name="address">The address where the value is read.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A value.</returns>
        public dynamic Read(Type ValueType, IntPtr address, out bool success, bool isRelative = true)
        {
            dynamic Value = "-";

            bool readSuccess = false;
            var @switch = new Dictionary<Type, Action> {
                    { typeof(Byte), () => Value = Read<Byte>(address, out readSuccess, isRelative) },
                    { typeof(SByte), () => Value = Read<SByte>(address, out readSuccess, isRelative) },
                    { typeof(Int16), () => Value = Read<Int16>(address, out readSuccess, isRelative) },
                    { typeof(Int32), () => Value = Read<Int32>(address, out readSuccess, isRelative) },
                    { typeof(Int64), () => Value = Read<Int64>(address, out readSuccess, isRelative) },
                    { typeof(UInt16), () => Value = Read<UInt16>(address, out readSuccess, isRelative) },
                    { typeof(UInt32), () => Value = Read<UInt32>(address, out readSuccess, isRelative) },
                    { typeof(UInt64), () => Value = Read<UInt64>(address, out readSuccess, isRelative) },
                    { typeof(Single), () => Value = Read<Single>(address, out readSuccess, isRelative) },
                    { typeof(Double), () => Value = Read<Double>(address, out readSuccess, isRelative) },
                };

            if (@switch.ContainsKey(ValueType))
                @switch[ValueType]();

            success = readSuccess;

            if (!success)
                Value = "?";

            return Value;
        }

        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="address">The address where the value is read.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A value.</returns>
        public T Read<T>(IntPtr address, out bool success, bool isRelative = true)
        {
            return MarshalType<T>.ByteArrayToObject(ReadBytes(address, MarshalType<T>.Size, out success, isRelative));
        }
        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="address">The address where the value is read.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A value.</returns>
        public T Read<T>(Enum address, out bool success, bool isRelative = true)
        {
            return Read<T>(new IntPtr(Convert.ToInt64(address)), out success, isRelative);
        }
        /// <summary>
        /// Reads an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="address">The address where the values is read.</param>
        /// <param name="count">The number of cells in the array.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>An array.</returns>
        public T[] Read<T>(IntPtr address, int count, out bool success, bool isRelative = true)
        {
            // Allocate an array to store the results
            var array = new T[count];
            success = true;
            // Read the array in the remote process
            for (var i = 0; i < count; i++)
            {
                bool result;
                array[i] = Read<T>(address + MarshalType<T>.Size * i, out result, isRelative);
                success &= result;
            }
            return array;
        }
        /// <summary>
        /// Reads an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="address">The address where the values is read.</param>
        /// <param name="count">The number of cells in the array.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>An array.</returns>
        public T[] Read<T>(Enum address, int count, out bool success, bool isRelative = true)
        {
            return Read<T>(new IntPtr(Convert.ToInt64(address)), count, out success, isRelative);
        }
        #endregion
        #region ReadBytes (protected)
        /// <summary>
        /// Reads an array of bytes in the remote process.
        /// </summary>
        /// <param name="address">The address where the array is read.</param>
        /// <param name="count">The number of cells.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>The array of bytes.</returns>
        public byte[] ReadBytes(IntPtr address, int count, out bool success, bool isRelative = true)
        {
            return MemoryCore.ReadBytes(Handle, isRelative ? MakeAbsolute(address) : address, count, out success);
        }
        #endregion
        #region ReadString
        /// <summary>
        /// Reads a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is read.</param>
        /// <param name="encoding">The encoding used.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(IntPtr address, Encoding encoding, out bool success, bool isRelative = true, int maxLength = 512)
        {
            // Read the string
            var data = encoding.GetString(ReadBytes(address, maxLength, out success, isRelative));
            // Search the end of the string
            var end = data.IndexOf('\0');
            // Crop the string with this end
            return data.Substring(0, end);
        }
        /// <summary>
        /// Reads a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is read.</param>
        /// <param name="encoding">The encoding used.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(Enum address, Encoding encoding, out bool success, bool isRelative = true, int maxLength = 512)
        {
            return ReadString(new IntPtr(Convert.ToInt64(address)), encoding, out success, isRelative, maxLength);
        }
        /// <summary>
        /// Reads a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is read.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(IntPtr address, out bool success, bool isRelative = true, int maxLength = 512)
        {
            return ReadString(address, Encoding.UTF8, out success, isRelative, maxLength);
        }
        /// <summary>
        /// Reads a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is read.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(Enum address, out bool success, bool isRelative = true, int maxLength = 512)
        {
            return ReadString(new IntPtr(Convert.ToInt64(address)), out success, isRelative, maxLength);
        }
        #endregion
        #region Write

        public void Write(Type ValueType, IntPtr address, dynamic value, bool isRelative = true)
        {
            var @switch = new Dictionary<Type, Action> {
                    { typeof(Byte), () => Write<Byte>(address, value, isRelative) },
                    { typeof(SByte), () => Write<SByte>(address, value, isRelative) },
                    { typeof(Int16), () => Write<Int16>(address, value, isRelative) },
                    { typeof(Int32), () => Write<Int32>(address, value, isRelative) },
                    { typeof(Int64), () => Write<Int64>(address, value, isRelative) },
                    { typeof(UInt16), () => Write<UInt16>(address, value, isRelative) },
                    { typeof(UInt32), () => Write<UInt32>(address, value, isRelative) },
                    { typeof(UInt64), () => Write<UInt64>(address, value, isRelative) },
                    { typeof(Single), () => Write<Single>(address, value, isRelative) },
                    { typeof(Double), () => Write<Double>(address, value, isRelative) },
                };

            if (@switch.ContainsKey(ValueType))
                @switch[ValueType]();
        }

        /// <summary>
        /// Writes the values of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="address">The address where the value is written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        public void Write<T>(IntPtr address, T value, bool isRelative = true)
        {
            WriteBytes(address, MarshalType<T>.ObjectToByteArray(value), isRelative);
        }
        /// <summary>
        /// Writes the values of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="address">The address where the value is written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        public void Write<T>(Enum address, T value, bool isRelative = true)
        {
            Write(new IntPtr(Convert.ToInt64(address)), value, isRelative);
        }
        /// <summary>
        /// Writes an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="address">The address where the values is written.</param>
        /// <param name="array">The array to write.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        public void Write<T>(IntPtr address, T[] array, bool isRelative = true)
        {
            // Write the array in the remote process
            for (var i = 0; i < array.Length; i++)
            {
                Write(address + MarshalType<T>.Size * i, array[i], isRelative);
            }
        }
        /// <summary>
        /// Writes an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="address">The address where the values is written.</param>
        /// <param name="array">The array to write.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        public void Write<T>(Enum address, T[] array, bool isRelative = true)
        {
            Write(new IntPtr(Convert.ToInt64(address)), array, isRelative);
        }
        #endregion
        #region WriteBytes (protected)
        /// <summary>
        /// Write an array of bytes in the remote process.
        /// </summary>
        /// <param name="address">The address where the array is written.</param>
        /// <param name="byteArray">The array of bytes to write.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        protected void WriteBytes(IntPtr address, byte[] byteArray, bool isRelative = true)
        {
            // Change the protection of the memory to allow writable
            using (new MemoryProtection(this, isRelative ? MakeAbsolute(address) : address, MarshalType<byte>.Size * byteArray.Length))
            {
                // Write the byte array
                MemoryCore.WriteBytes(Handle, isRelative ? MakeAbsolute(address) : address, byteArray);

            }
        }
        #endregion
        #region WriteString
        /// <summary>
        /// Writes a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is written.</param>
        /// <param name="text">The text to write.</param>
        /// <param name="encoding">The encoding used.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        public void WriteString(IntPtr address, string text, Encoding encoding, bool isRelative = true)
        {
            // Write the text
            WriteBytes(address, encoding.GetBytes(text + '\0'), isRelative);
        }
        /// <summary>
        /// Writes a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is written.</param>
        /// <param name="text">The text to write.</param>
        /// <param name="encoding">The encoding used.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        public void WriteString(Enum address, string text, Encoding encoding, bool isRelative = true)
        {
            WriteString(new IntPtr(Convert.ToInt64(address)), text, encoding, isRelative);
        }
        /// <summary>
        /// Writes a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is written.</param>
        /// <param name="text">The text to write.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        public void WriteString(IntPtr address, string text, bool isRelative = true)
        {
            WriteString(address, text, Encoding.UTF8, isRelative);
        }
        /// <summary>
        /// Writes a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is written.</param>
        /// <param name="text">The text to write.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        public void WriteString(Enum address, string text, bool isRelative = true)
        {
            WriteString(new IntPtr(Convert.ToInt64(address)), text, isRelative);
        }
        #endregion
        #endregion
    }
}
