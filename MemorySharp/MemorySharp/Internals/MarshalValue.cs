/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using Binarysharp.MemoryManagement.Memory;

namespace Binarysharp.MemoryManagement.Internals
{
    /// <summary>
    /// The factory to create instance of the <see cref="MarshalledValue{T}"/> class.
    /// </summary>
    /// <remarks>
    /// A factory pattern is used because C# 5.0 constructor doesn't support type inference.
    /// More info from Eric Lippert here : http://stackoverflow.com/questions/3570167/why-cant-the-c-sharp-constructor-infer-type
    /// </remarks>
    public static class MarshalValue
    {
        /// <summary>
        /// Marshals a given value into the remote process.
        /// </summary>
        /// <typeparam name="T">The type of the value. It can be a primitive or reference data type.</typeparam>
        /// <param name="memorySharp">The concerned process.</param>
        /// <param name="value">The value to marshal.</param>
        /// <returns>The return value is an new instance of the <see cref="MarshalledValue{T}"/> class.</returns>
        public static MarshalledValue<T> Marshal<T>(MemorySharp memorySharp, T value)
        {
            return new MarshalledValue<T>(memorySharp, value);
        }
    }

    /// <summary>
    /// Class marshalling a value into the remote process.
    /// </summary>
    /// <typeparam name="T">The type of the value. It can be a primitive or reference data type.</typeparam>
    public class MarshalledValue<T> : IMarshalledValue
    {
        #region Fields
        /// <summary>
        /// The reference of the <see cref="MemorySharp"/> object.
        /// </summary>
        protected readonly MemorySharp MemorySharp;
        #endregion

        #region Properties
        /// <summary>
        /// The memory allocated where the value is fully written if needed. It can be unused.
        /// </summary>
        public RemoteAllocation Allocated { get; private set; }
        /// <summary>
        /// The reference of the value. It can be directly the value or a pointer.
        /// </summary>
        public IntPtr Reference { get; private set; }
        /// <summary>
        /// The initial value.
        /// </summary>
        public T Value { get; private set; }
        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MarshalledValue{T}"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="value">The value to marshal.</param>
        public MarshalledValue(MemorySharp memorySharp, T value)
        {
            // Save the parameters
            MemorySharp = memorySharp;
            Value = value;
            // Marshal the value
            Marshal();
        }
        /// <summary>
        /// Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~MarshalledValue()
        {
            Dispose();
        }
        #endregion

        #region Methods
        #region Dispose (implementation of IDisposable)
        /// <summary>
        /// Releases all resources used by the <see cref="RemoteAllocation"/> object.
        /// </summary>
        public void Dispose()
        {
            // Free the allocated memory
            if(Allocated != null)
                Allocated.Dispose();
            // Set the pointer to zero
            Reference = IntPtr.Zero;
            // Avoid the finalizer
            GC.SuppressFinalize(this);
        }
        #endregion
        #region Marshal (private)
        /// <summary>
        /// Marshals the value into the remote process.
        /// </summary>
        private void Marshal()
        {
            // If the type is string, it's a special case
            if (typeof(T) == typeof(string))
            {
                var text = Value.ToString();
                // Allocate memory in the remote process (string + '\0')
                Allocated = MemorySharp.Memory.Allocate(text.Length + 1);
                // Write the value
                Allocated.WriteString(0, text);
                // Get the pointer
                Reference = Allocated.BaseAddress;
            }
            else
            {
                // For all other types
                // Convert the value into a byte array
                var byteArray = MarshalType<T>.ObjectToByteArray(Value);

                // If the value can be stored directly in registers
                if (MarshalType<T>.CanBeStoredInRegisters)
                {
                    // Convert the byte array into a pointer
                    Reference = MarshalType<IntPtr>.ByteArrayToObject(byteArray);
                }
                else
                {
                    // It's a bit more complicated, we must allocate some space into
                    // the remote process to store the value and get its pointer

                    // Allocate memory in the remote process
                    Allocated = MemorySharp.Memory.Allocate(MarshalType<T>.Size);
                    // Write the value
                    Allocated.Write(0, Value);
                    // Get the pointer
                    Reference = Allocated.BaseAddress;
                }
            }
        }
        #endregion
        #endregion
    }
}
