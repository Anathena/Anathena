using Anathena.Source.Engine.OperatingSystems.Windows.Memory;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Internals
{
    /// <summary>
    /// Static class providing tools for extracting information related to types.
    /// </summary>
    /// <typeparam name="T">Type to analyse.</typeparam>
    public static class MarshalType<T>
    {
        #region Properties
        /// <summary>
        /// Gets if the type can be stored in a registers (for example ACX, ECX, ...).
        /// </summary>
        public static bool CanBeStoredInRegisters { get; private set; }
        /// <summary>
        /// State if the type is <see cref="IntPtr"/>.
        /// </summary>
        public static bool IsIntPtr { get; private set; }
        /// <summary>
        /// The real type.
        /// </summary>
        public static Type RealType { get; private set; }
        /// <summary>
        /// The size of the type.
        /// </summary>
        public static int Size { get; private set; }
        /// <summary>
        /// The typecode of the type.
        /// </summary>
        public static TypeCode TypeCode { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes static information related to the specified type.
        /// </summary>
        static MarshalType()
        {
            // Gather information related to the provided type
            IsIntPtr = typeof(T) == typeof(IntPtr);
            RealType = typeof(T);
            Size = TypeCode == TypeCode.Boolean ? 1 : Marshal.SizeOf(RealType);
            TypeCode = Type.GetTypeCode(RealType);
            // Check if the type can be stored in registers
            CanBeStoredInRegisters =
                IsIntPtr ||
#if x64
                TypeCode == TypeCode.Int64 ||
                TypeCode == TypeCode.UInt64 ||
#endif
 TypeCode == TypeCode.Boolean ||
                TypeCode == TypeCode.Byte ||
                TypeCode == TypeCode.Char ||
                TypeCode == TypeCode.Int16 ||
                TypeCode == TypeCode.Int32 ||
                TypeCode == TypeCode.Int64 ||
                TypeCode == TypeCode.SByte ||
                TypeCode == TypeCode.Single ||
                TypeCode == TypeCode.UInt16 ||
                TypeCode == TypeCode.UInt32;
        }

        #endregion

        #region Methods
        #region ObjectToByteArray
        /// <summary>
        /// Marshals a managed object to an array of bytes.
        /// </summary>
        /// <param name="Object">The object to marshal.</param>
        /// <returns>A array of bytes corresponding to the managed object.</returns>
        public static byte[] ObjectToByteArray(T Object)
        {
            // We'll tried to avoid marshalling as it really slows the process
            // First, check if the type can be converted without marhsalling
            switch (TypeCode)
            {
                case TypeCode.Object:
                    if (IsIntPtr)
                    {
                        switch (Size)
                        {
                            case 4:
                                return BitConverter.GetBytes(((IntPtr)(Object)Object).ToInt32());
                            case 8:
                                return BitConverter.GetBytes(((IntPtr)(Object)Object).ToInt64());
                        }
                    }
                    break;
                case TypeCode.Boolean:
                    return BitConverter.GetBytes((Boolean)(Object)Object);
                case TypeCode.Char:
                    return Encoding.UTF8.GetBytes(new[] { (Char)(Object)Object });
                case TypeCode.Double:
                    return BitConverter.GetBytes((Double)(Object)Object);
                case TypeCode.Int16:
                    return BitConverter.GetBytes((Int16)(Object)Object);
                case TypeCode.Int32:
                    return BitConverter.GetBytes((Int32)(Object)Object);
                case TypeCode.Int64:
                    return BitConverter.GetBytes((Int64)(Object)Object);
                case TypeCode.Single:
                    return BitConverter.GetBytes((Single)(Object)Object);
                case TypeCode.String:
                    throw new InvalidCastException("This method doesn't support string conversion.");
                case TypeCode.UInt16:
                    return BitConverter.GetBytes((UInt16)(Object)Object);
                case TypeCode.UInt32:
                    return BitConverter.GetBytes((UInt32)(Object)Object);
                case TypeCode.UInt64:
                    return BitConverter.GetBytes((UInt64)(Object)Object);

            }
            // Check if it's not a common type
            // Allocate a block of unmanaged memory
            using (LocalUnmanagedMemory Unmanaged = new LocalUnmanagedMemory(Size))
            {
                // Write the object inside the unmanaged memory
                Unmanaged.Write(Object);
                // Return the content of the block of unmanaged memory
                return Unmanaged.Read();
            }
        }

        #endregion

        #region ByteArrayToObject
        /// <summary>
        /// Marshals an array of byte to a managed object.
        /// </summary>
        /// <param name="ByteArray">The array of bytes corresponding to a managed object.</param>
        /// <returns>A managed object.</returns>
        public static T ByteArrayToObject(Byte[] ByteArray)
        {
            // We'll tried to avoid marshalling as it really slows the process
            // First, check if the type can be converted without marhsalling
            switch (TypeCode)
            {
                case TypeCode.Object:
                    if (IsIntPtr)
                    {
                        switch (ByteArray.Length)
                        {
                            case 1:
                                return (T)(Object)new IntPtr(BitConverter.ToInt32(new Byte[] { ByteArray[0], 0x0, 0x0, 0x0 }, 0));
                            case 2:
                                return (T)(Object)new IntPtr(BitConverter.ToInt32(new Byte[] { ByteArray[0], ByteArray[1], 0x0, 0x0 }, 0));
                            case 4:
                                return (T)(Object)new IntPtr(BitConverter.ToInt32(ByteArray, 0));
                            case 8:
                                return (T)(Object)new IntPtr(BitConverter.ToInt64(ByteArray, 0));
                        }
                    }
                    break;
                case TypeCode.Boolean:
                    return (T)(Object)BitConverter.ToBoolean(ByteArray, 0);
                case TypeCode.Byte:
                    return (T)(Object)ByteArray[0];
                case TypeCode.Char:
                    return (T)(Object)Encoding.UTF8.GetChars(ByteArray)[0]; // BitConverter.ToChar(ByteArray, 0);
                case TypeCode.Double:
                    return (T)(Object)BitConverter.ToDouble(ByteArray, 0);
                case TypeCode.Int16:
                    return (T)(Object)BitConverter.ToInt16(ByteArray, 0);
                case TypeCode.Int32:
                    return (T)(Object)BitConverter.ToInt32(ByteArray, 0);
                case TypeCode.Int64:
                    return (T)(Object)BitConverter.ToInt64(ByteArray, 0);
                case TypeCode.Single:
                    return (T)(Object)BitConverter.ToSingle(ByteArray, 0);
                case TypeCode.String:
                    throw new InvalidCastException("This method doesn't support string conversion.");
                case TypeCode.UInt16:
                    return (T)(Object)BitConverter.ToUInt16(ByteArray, 0);
                case TypeCode.UInt32:
                    return (T)(Object)BitConverter.ToUInt32(ByteArray, 0);
                case TypeCode.UInt64:
                    return (T)(Object)BitConverter.ToUInt64(ByteArray, 0);
            }

            // Check if it's not a common type
            // Allocate a block of unmanaged memory
            using (LocalUnmanagedMemory Unmanaged = new LocalUnmanagedMemory(Size))
            {
                // Write the array of bytes inside the unmanaged memory
                Unmanaged.Write(ByteArray);
                // Return a managed object created from the block of unmanaged memory
                return Unmanaged.Read<T>();
            }
        }

        #endregion
        #region PtrToObject
        /// <summary>
        /// Converts a pointer to a given type. This function converts the value of the pointer or the pointed value,
        /// according if the data type is primitive or reference.
        /// </summary>
        /// <param name="WindowsOperatingSystem">The concerned process.</param>
        /// <param name="Pointer">The pointer to convert.</param>
        /// <returns>The return value is the pointer converted to the given data type.</returns>
        public static T PtrToObject(WindowsOperatingSystem WindowsOperatingSystem, IntPtr Pointer)
        {
            Boolean ReadSuccess;
            return ByteArrayToObject(CanBeStoredInRegisters ? BitConverter.GetBytes(Pointer.ToInt64()) : WindowsOperatingSystem.Read<Byte>(Pointer, Size, out ReadSuccess));
        }

        #endregion
        #endregion

    } // End class

} // End namespace