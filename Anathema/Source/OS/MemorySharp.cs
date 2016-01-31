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
        /// <summary>
        /// Raises when the <see cref="MemorySharp"/> object is disposed.
        /// </summary>
        public event EventHandler OnDispose;

        /// <summary>
        /// The factories embedded inside the library.
        /// </summary>
        protected List<IFactory> Factories;

        /// <summary>
        /// Factory for generating assembly code.
        /// </summary>
        public AssemblyFactory Assembly { get; protected set; }

        /// <summary>
        /// Gets whether the process is being debugged.
        /// </summary>
        public bool IsDebugged
        {
            get { return Peb.BeingDebugged; }
            set { Peb.BeingDebugged = value; }
        }
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
        /// The Process Environment Block of the process.
        /// </summary>
        public ManagedPeb Peb { get; private set; }
        /// <summary>
        /// Gets the unique identifier for the remote process.
        /// </summary>
        public int Pid { get { return Native.Id; } }
        /// <summary>
        /// Factory for manipulating threads.
        /// </summary>
        public ThreadFactory Threads { get; protected set; }
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
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A new instance of a <see cref="RemotePointer"/> class.</returns>
        public RemotePointer this[IntPtr Address, Boolean IsRelative = false]
        {
            get { return new RemotePointer(this, IsRelative ? MakeAbsolute(Address) : Address); }
        }

        #region Constructors/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MemorySharp"/> class.
        /// </summary>
        /// <param name="Process">Process to open.</param>
        public MemorySharp(Process Process)
        {
            // Save the reference of the process
            Native = Process;

            // Open the process with all rights
            Handle = MemoryCore.OpenProcess(ProcessAccessFlags.AllAccess, Process.Id);

            // Initialize the PEB
            Peb = new ManagedPeb(this, ManagedPeb.FindPeb(Handle));

            // Create instances of the factories
            Factories = new List<IFactory>();
            Factories.AddRange(new IFactory[]
            {
                Assembly = new AssemblyFactory(this),
                Memory = new MemoryFactory(this),
                Modules = new ModuleFactory(this),
                Threads = new ThreadFactory(this),
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemorySharp"/> class.
        /// </summary>
        /// <param name="ProcessId">Process id of the process to open.</param>
        public MemorySharp(Int32 ProcessId) : this(ApplicationFinder.FromProcessId(ProcessId))
        {

        }

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
        public override Boolean Equals(Object Object)
        {
            if (ReferenceEquals(null, Object)) return false;
            if (ReferenceEquals(this, Object)) return true;
            return Object.GetType() == GetType() && Equals((MemorySharp)Object);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        public Boolean Equals(MemorySharp Other)
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
        #region MakeAbsolute
        /// <summary>
        /// Makes an absolute address from a relative one based on the main module.
        /// </summary>
        /// <param name="Address">The relative address.</param>
        /// <returns>The absolute address.</returns>
        public IntPtr MakeAbsolute(IntPtr Address)
        {
            // Check if the relative address is not greater than the main module size
            //if (address.ToInt64() > Modules.MainModule.Size)
            //    throw new ArgumentOutOfRangeException("address", "The relative address cannot be greater than the main module size.");
            // Compute the absolute address
            return new IntPtr(Modules.MainModule.BaseAddress.ToInt64() + Address.ToInt64());
        }

        #endregion
        #region MakeRelative
        /// <summary>
        /// Makes a relative address from an absolute one based on the main module.
        /// </summary>
        /// <param name="Address">The absolute address.</param>
        /// <returns>The relative address.</returns>
        public IntPtr MakeRelative(IntPtr Address)
        {
            // Check if the absolute address is smaller than the main module base address
            if (Address.ToInt64() < Modules.MainModule.BaseAddress.ToInt64())
                throw new ArgumentOutOfRangeException("address", "The absolute address cannot be smaller than the main module base address.");

            // Compute the relative address
            return new IntPtr(Address.ToInt64() - Modules.MainModule.BaseAddress.ToInt64());
        }

        #endregion
        #region Operator (override)
        public static Boolean operator ==(MemorySharp Left, MemorySharp Right)
        {
            return Equals(Left, Right);
        }

        public static Boolean operator !=(MemorySharp Left, MemorySharp Right)
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
        public dynamic Read(Type ValueType, IntPtr Address, out Boolean Success, Boolean IsRelative = false)
        {
            dynamic Value;

            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: Value = Read<Byte>(Address, out Success, IsRelative); break;
                case TypeCode.SByte: Value = Read<SByte>(Address, out Success, IsRelative); break;
                case TypeCode.Int16: Value = Read<Int16>(Address, out Success, IsRelative); break;
                case TypeCode.Int32: Value = Read<Int32>(Address, out Success, IsRelative); break;
                case TypeCode.Int64: Value = Read<Int64>(Address, out Success, IsRelative); break;
                case TypeCode.UInt16: Value = Read<UInt16>(Address, out Success, IsRelative); break;
                case TypeCode.UInt32: Value = Read<UInt32>(Address, out Success, IsRelative); break;
                case TypeCode.UInt64: Value = Read<UInt64>(Address, out Success, IsRelative); break;
                case TypeCode.Single: Value = Read<Single>(Address, out Success, IsRelative); break;
                case TypeCode.Double: Value = Read<Double>(Address, out Success, IsRelative); break;
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
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A value.</returns>
        public T Read<T>(IntPtr Address, out Boolean Success, Boolean IsRelative = false)
        {
            return MarshalType<T>.ByteArrayToObject(ReadBytes(Address, MarshalType<T>.Size, out Success, IsRelative));
        }

        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="Address">The address where the value is read.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A value.</returns>
        public T Read<T>(Enum Address, out Boolean Success, Boolean IsRelative = false)
        {
            return Read<T>(new IntPtr(Convert.ToInt64(Address)), out Success, IsRelative);
        }

        /// <summary>
        /// Reads an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="Address">The address where the values is read.</param>
        /// <param name="Count">The number of cells in the array.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>An array.</returns>
        public T[] Read<T>(IntPtr Address, Int32 Count, out Boolean Success, Boolean IsRelative = false)
        {
            // Allocate an array to store the results
            T[] Array = new T[Count];
            Success = true;

            // Read the array in the remote process
            for (Int32 Index = 0; Index < Count; Index++)
            {
                Boolean result;
                Array[Index] = Read<T>(Address + MarshalType<T>.Size * Index, out result, IsRelative);
                Success &= result;
            }

            return Array;
        }

        /// <summary>
        /// Reads an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="Address">The address where the values is read.</param>
        /// <param name="Count">The number of cells in the array.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>An array.</returns>
        public T[] Read<T>(Enum Address, Int32 Count, out Boolean Success, Boolean IsRelative = false)
        {
            return Read<T>(new IntPtr(Convert.ToInt64(Address)), Count, out Success, IsRelative);
        }

        /// <summary>
        /// Reads an array of bytes in the remote process.
        /// </summary>
        /// <param name="Address">The address where the array is read.</param>
        /// <param name="Count">The number of cells.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>The array of bytes.</returns>
        public byte[] ReadBytes(IntPtr Address, Int32 Count, out Boolean Success, Boolean IsRelative = false)
        {
            return MemoryCore.ReadBytes(Handle, IsRelative ? MakeAbsolute(Address) : Address, Count, out Success);
        }

        #endregion
        #region ReadString
        /// <summary>
        /// Reads a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is read.</param>
        /// <param name="Encoding">The encoding used.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <param name="MaxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public String ReadString(IntPtr Address, Encoding Encoding, out Boolean Success, Boolean IsRelative = false, Int32 MaxLength = 512)
        {
            // Read the string
            String Data = Encoding.GetString(ReadBytes(Address, MaxLength, out Success, IsRelative));

            // Search the end of the string
            Int32 End = Data.IndexOf('\0');

            // Crop the string with this end
            return Data.Substring(0, End);
        }

        /// <summary>
        /// Reads a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is read.</param>
        /// <param name="Encoding">The encoding used.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <param name="MaxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(Enum Address, Encoding Encoding, out Boolean Success, Boolean IsRelative = false, Int32 MaxLength = 512)
        {
            return ReadString(new IntPtr(Convert.ToInt64(Address)), Encoding, out Success, IsRelative, MaxLength);
        }

        /// <summary>
        /// Reads a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is read.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <param name="MaxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(IntPtr Address, out Boolean Success, Boolean IsRelative = false, Int32 MaxLength = 512)
        {
            return ReadString(Address, Encoding.UTF8, out Success, IsRelative, MaxLength);
        }

        /// <summary>
        /// Reads a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is read.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        /// <param name="MaxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(Enum Address, out Boolean Success, Boolean IsRelative = false, Int32 MaxLength = 512)
        {
            return ReadString(new IntPtr(Convert.ToInt64(Address)), out Success, IsRelative, MaxLength);
        }

        #endregion
        #region Write

        public void Write(Type ValueType, IntPtr Address, dynamic Value, Boolean IsRelative = false)
        {
            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: Write<Byte>(Address, Value, IsRelative); break;
                case TypeCode.SByte: Write<SByte>(Address, Value, IsRelative); break;
                case TypeCode.Int16: Write<Int16>(Address, Value, IsRelative); break;
                case TypeCode.Int32: Write<Int32>(Address, Value, IsRelative); break;
                case TypeCode.Int64: Write<Int64>(Address, Value, IsRelative); break;
                case TypeCode.UInt16: Write<UInt16>(Address, Value, IsRelative); break;
                case TypeCode.UInt32: Write<UInt32>(Address, Value, IsRelative); break;
                case TypeCode.UInt64: Write<UInt64>(Address, Value, IsRelative); break;
                case TypeCode.Single: Write<Single>(Address, Value, IsRelative); break;
                case TypeCode.Double: Write<Double>(Address, Value, IsRelative); break;
                default: return;
            }
        }

        /// <summary>
        /// Writes the values of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="Address">The address where the value is written.</param>
        /// <param name="Value">The value to write.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        public void Write<T>(IntPtr Address, T Value, Boolean IsRelative = false)
        {
            WriteBytes(Address, MarshalType<T>.ObjectToByteArray(Value), IsRelative);
        }

        /// <summary>
        /// Writes the values of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="Address">The address where the value is written.</param>
        /// <param name="Value">The value to write.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        public void Write<T>(Enum Address, T Value, Boolean IsRelative = false)
        {
            Write(new IntPtr(Convert.ToInt64(Address)), Value, IsRelative);
        }

        /// <summary>
        /// Writes an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="Address">The address where the values is written.</param>
        /// <param name="Array">The array to write.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        public void Write<T>(IntPtr Address, T[] Array, Boolean IsRelative = false)
        {
            // Write the array in the remote process
            for (Int32 Index = 0; Index < Array.Length; Index++)
            {
                Write(Address + MarshalType<T>.Size * Index, Array[Index], IsRelative);
            }
        }

        /// <summary>
        /// Writes an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="Address">The address where the values is written.</param>
        /// <param name="Array">The array to write.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        public void Write<T>(Enum Address, T[] Array, Boolean IsRelative = false)
        {
            Write(new IntPtr(Convert.ToInt64(Address)), Array, IsRelative);
        }

        /// <summary>
        /// Write an array of bytes in the remote process.
        /// </summary>
        /// <param name="Address">The address where the array is written.</param>
        /// <param name="ByteArray">The array of bytes to write.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        public void WriteBytes(IntPtr Address, Byte[] ByteArray, Boolean IsRelative = false)
        {
            // Change the protection of the memory to allow writable
            using (new MemoryProtection(this, IsRelative ? MakeAbsolute(Address) : Address, MarshalType<byte>.Size * ByteArray.Length))
            {
                // Write the byte array
                MemoryCore.WriteBytes(Handle, IsRelative ? MakeAbsolute(Address) : Address, ByteArray);
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
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        public void WriteString(IntPtr Address, String Text, Encoding Encoding, Boolean IsRelative = false)
        {
            // Write the text
            WriteBytes(Address, Encoding.GetBytes(Text + '\0'), IsRelative);
        }

        /// <summary>
        /// Writes a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="address">The address where the string is written.</param>
        /// <param name="Text">The text to write.</param>
        /// <param name="Encoding">The encoding used.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        public void WriteString(Enum Address, String Text, Encoding Encoding, Boolean IsRelative = false)
        {
            WriteString(new IntPtr(Convert.ToInt64(Address)), Text, Encoding, IsRelative);
        }

        /// <summary>
        /// Writes a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is written.</param>
        /// <param name="Text">The text to write.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        public void WriteString(IntPtr Address, String Text, Boolean IsRelative = false)
        {
            WriteString(Address, Text, Encoding.UTF8, IsRelative);
        }

        /// <summary>
        /// Writes a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="Address">The address where the string is written.</param>
        /// <param name="Text">The text to write.</param>
        /// <param name="IsRelative">[Optional] State if the address is relative to the main module.</param>
        public void WriteString(Enum Address, String Text, Boolean IsRelative = false)
        {
            WriteString(new IntPtr(Convert.ToInt64(Address)), Text, IsRelative);
        }

        #endregion
        #endregion

    } // End class

} // End namespace