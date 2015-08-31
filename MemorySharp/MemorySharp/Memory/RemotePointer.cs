/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Text;
using System.Threading.Tasks;
using Binarysharp.MemoryManagement.Assembly.CallingConvention;
using Binarysharp.MemoryManagement.Native;

namespace Binarysharp.MemoryManagement.Memory
{
    /// <summary>
    /// Class representing a pointer in the memory of the remote process.
    /// </summary>
    public class RemotePointer : IEquatable<RemotePointer>
    {
        #region Properties
        #region BaseAddress
        /// <summary>
        /// The address of the pointer in the remote process.
        /// </summary>
        public IntPtr BaseAddress { get; set; }
        #endregion
        #region IsValid
        /// <summary>
        /// Gets if the <see cref="RemotePointer"/> is valid.
        /// </summary>
        public virtual bool IsValid
        {
            get { return MemorySharp.IsRunning && BaseAddress != IntPtr.Zero; }
        }
        #endregion
        #region MemorySharp
        /// <summary>
        /// The reference of the <see cref="MemoryManagement.MemorySharp"/> object.
        /// </summary>
        public MemorySharp MemorySharp { get; protected set; }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemotePointer"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemoryManagement.MemorySharp"/> object.</param>
        /// <param name="address">The location where the pointer points in the remote process.</param>
        public RemotePointer(MemorySharp memorySharp, IntPtr address)
        {
            // Save the parameters
            MemorySharp = memorySharp;
            BaseAddress = address;
        }
        #endregion

        #region Methods
        #region ChangeProtection
        /// <summary>
        /// Changes the protection of the n next bytes in remote process.
        /// </summary>
        /// <param name="size">The size of the memory to change.</param>
        /// <param name="protection">The new protection to apply.</param>
        /// <param name="mustBeDisposed">The resource will be automatically disposed when the finalizer collects the object.</param>
        /// <returns>A new instance of the <see cref="MemoryProtection"/> class.</returns>
        public MemoryProtection ChangeProtection(int size, MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite, bool mustBeDisposed = true)
        {
            return new MemoryProtection(MemorySharp, BaseAddress, size, protection, mustBeDisposed);
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
            return obj.GetType() == GetType() && Equals((RemotePointer)obj);
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        public bool Equals(RemotePointer other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || (BaseAddress.Equals(other.BaseAddress) && MemorySharp.Equals(other.MemorySharp));
        }
        #endregion
        #region Execute
        /// <summary>
        /// Executes the assembly code in the remote process.
        /// </summary>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T Execute<T>()
        {
            return MemorySharp.Assembly.Execute<T>(BaseAddress);
        }
        /// <summary>
        /// Executes the assembly code in the remote process.
        /// </summary>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr Execute()
        {
            return Execute<IntPtr>();
        }
        /// <summary>
        /// Executes the assembly code in the remote process.
        /// </summary>
        /// <param name="parameter">The parameter used to execute the assembly code.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T Execute<T>(dynamic parameter)
        {
            return MemorySharp.Assembly.Execute<T>(BaseAddress, parameter);
        }
        /// <summary>
        /// Executes the assembly code in the remote process.
        /// </summary>
        /// <param name="parameter">The parameter used to execute the assembly code.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr Execute(dynamic parameter)
        {
            return Execute<IntPtr>(parameter);
        }
        /// <summary>
        /// Executes the assembly code in the remote process.
        /// </summary>
        /// <param name="callingConvention">The calling convention used to execute the assembly code with the parameters.</param>
        /// <param name="parameters">An array of parameters used to execute the assembly code.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T Execute<T>(CallingConventions callingConvention, params dynamic[] parameters)
        {
            return MemorySharp.Assembly.Execute<T>(BaseAddress, callingConvention, parameters);
        }
        /// <summary>
        /// Executes the assembly code in the remote process.
        /// </summary>
        /// <param name="callingConvention">The calling convention used to execute the assembly code with the parameters.</param>
        /// <param name="parameters">An array of parameters used to execute the assembly code.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr Execute(CallingConventions callingConvention, params dynamic[] parameters)
        {
            return Execute<IntPtr>(callingConvention, parameters);
        }
        #endregion
        #region ExecuteAsync
        /// <summary>
        /// Executes asynchronously the assembly code in the remote process.
        /// </summary>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> ExecuteAsync<T>()
        {
            return MemorySharp.Assembly.ExecuteAsync<T>(BaseAddress);
        }
        /// <summary>
        /// Executes asynchronously the assembly code in the remote process.
        /// </summary>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> ExecuteAsync()
        {
            return ExecuteAsync<IntPtr>();
        }
        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="parameter">The parameter used to execute the assembly code.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> ExecuteAsync<T>(dynamic parameter)
        {
            return MemorySharp.Assembly.ExecuteAsync<T>(BaseAddress, parameter);
        }
        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="parameter">The parameter used to execute the assembly code.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> ExecuteAsync(dynamic parameter)
        {
            return ExecuteAsync<IntPtr>(parameter);
        }
        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="callingConvention">The calling convention used to execute the assembly code with the parameters.</param>
        /// <param name="parameters">An array of parameters used to execute the assembly code.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> ExecuteAsync<T>(CallingConventions callingConvention, params dynamic[] parameters)
        {
            return MemorySharp.Assembly.ExecuteAsync<T>(BaseAddress, callingConvention, parameters);
        }
        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="callingConvention">The calling convention used to execute the assembly code with the parameters.</param>
        /// <param name="parameters">An array of parameters used to execute the assembly code.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> ExecuteAsync(CallingConventions callingConvention, params dynamic[] parameters)
        {
            return ExecuteAsync<IntPtr>(callingConvention, parameters);
        }
        #endregion
        #region GetHashCode (override)
        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        public override int GetHashCode()
        {
            return BaseAddress.GetHashCode() ^ MemorySharp.GetHashCode();
        }
        #endregion
        #region Operator (override)
        public static bool operator ==(RemotePointer left, RemotePointer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RemotePointer left, RemotePointer right)
        {
            return !Equals(left, right);
        }
        #endregion
        #region Read
        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="offset">The offset where the value is read from the pointer.</param>
        /// <returns>A value.</returns>
        public T Read<T>(int offset)
        {
            return MemorySharp.Read<T>(BaseAddress + offset, false);
        }
        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="offset">The offset where the value is read from the pointer.</param>
        /// <returns>A value.</returns>
        public T Read<T>(Enum offset)
        {
            return Read<T>(Convert.ToInt32(offset));
        }
        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>A value.</returns>
        public T Read<T>()
        {
            return Read<T>(0);
        }
        /// <summary>
        /// Reads an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="offset">The offset where the values is read from the pointer.</param>
        /// <param name="count">The number of cells in the array.</param>
        /// <returns>An array.</returns>
        public T[] Read<T>(int offset, int count)
        {
            return MemorySharp.Read<T>(BaseAddress + offset, count, false);
        }
        /// <summary>
        /// Reads an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="offset">The offset where the values is read from the pointer.</param>
        /// <param name="count">The number of cells in the array.</param>
        /// <returns>An array.</returns>
        public T[] Read<T>(Enum offset, int count)
        {
            return Read<T>(Convert.ToInt32(offset), count);
        }
        #endregion
        #region ReadString
        /// <summary>
        /// Reads a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="offset">The offset where the string is read from the pointer.</param>
        /// <param name="encoding">The encoding used.</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(int offset, Encoding encoding, int maxLength = 512)
        {
            return MemorySharp.ReadString(BaseAddress + offset, encoding, false, maxLength);
        }
        /// <summary>
        /// Reads a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="offset">The offset where the string is read from the pointer.</param>
        /// <param name="encoding">The encoding used.</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(Enum offset, Encoding encoding, int maxLength = 512)
        {
            return ReadString(Convert.ToInt32(offset), encoding, maxLength);
        }
        /// <summary>
        /// Reads a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="encoding">The encoding used.</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(Encoding encoding, int maxLength = 512)
        {
            return ReadString(0, encoding, maxLength);
        }
        /// <summary>
        /// Reads a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="offset">The offset where the string is read from the pointer.</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(int offset, int maxLength = 512)
        {
            return MemorySharp.ReadString(BaseAddress + offset, false, maxLength);
        }
        /// <summary>
        /// Reads a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="offset">The offset where the string is read from the pointer.</param>
        /// <param name="maxLength">[Optional] The number of maximum bytes to read. The string is automatically cropped at this end ('\0' char).</param>
        /// <returns>The string.</returns>
        public string ReadString(Enum offset, int maxLength = 512)
        {
            return ReadString(Convert.ToInt32(offset), maxLength);
        }
        #endregion
        #region ToString (override)
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return string.Format("BaseAddress = 0x{0:X}", BaseAddress.ToInt64());
        }
        #endregion
        #region Write
        /// <summary>
        /// Writes the values of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="offset">The offset where the value is written from the pointer.</param>
        /// <param name="value">The value to write.</param>
        public void Write<T>(int offset, T value)
        {
            MemorySharp.Write(BaseAddress + offset, value, false);
        }
        /// <summary>
        /// Writes the values of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="offset">The offset where the value is written from the pointer.</param>
        /// <param name="value">The value to write.</param>
        public void Write<T>(Enum offset, T value)
        {
            Write(Convert.ToInt32(offset), value);
        }
        /// <summary>
        /// Writes the values of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to write.</param>
        public void Write<T>(T value)
        {
            Write(0, value);
        }
        /// <summary>
        /// Writes an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="offset">The offset where the values is written from the pointer.</param>
        /// <param name="array">The array to write.</param>
        public void Write<T>(int offset, T[] array)
        {
            MemorySharp.Write(BaseAddress + offset, array, false);
        }
        /// <summary>
        /// Writes an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="offset">The offset where the values is written from the pointer.</param>
        /// <param name="array">The array to write.</param>
        public void Write<T>(Enum offset, T[] array)
        {
            Write(Convert.ToInt32(offset), array);
        }
        /// <summary>
        /// Writes an array of a specified type in the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="array">The array to write.</param>
        public void Write<T>(T[] array)
        {
            Write(0, array);
        }
        #endregion
        #region WriteString
        /// <summary>
        /// Writes a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="offset">The offset where the string is written from the pointer.</param>
        /// <param name="text">The text to write.</param>
        /// <param name="encoding">The encoding used.</param>
        public void WriteString(int offset, string text, Encoding encoding)
        {
            MemorySharp.WriteString(BaseAddress + offset, text, encoding, false);
        }
        /// <summary>
        /// Writes a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="offset">The offset where the string is written from the pointer.</param>
        /// <param name="text">The text to write.</param>
        /// <param name="encoding">The encoding used.</param>
        public void WriteString(Enum offset, string text, Encoding encoding)
        {
            WriteString(Convert.ToInt32(offset), text, encoding);
        }
        /// <summary>
        /// Writes a string with a specified encoding in the remote process.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="encoding">The encoding used.</param>
        public void WriteString(string text, Encoding encoding)
        {
            WriteString(0, text, encoding);
        }
        /// <summary>
        /// Writes a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="offset">The offset where the string is written from the pointer.</param>
        /// <param name="text">The text to write.</param>
        public void WriteString(int offset, string text)
        {
            MemorySharp.WriteString(BaseAddress + offset, text, false);
        }
        /// <summary>
        /// Writes a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="offset">The offset where the string is written from the pointer.</param>
        /// <param name="text">The text to write.</param>
        public void WriteString(Enum offset, string text)
        {
            WriteString(Convert.ToInt32(offset), text);
        }
        /// <summary>
        /// Writes a string using the encoding UTF8 in the remote process.
        /// </summary>
        /// <param name="text">The text to write.</param>
        public void WriteString(string text)
        {
            WriteString(0, text);
        }
        #endregion
        #endregion
    }
}
