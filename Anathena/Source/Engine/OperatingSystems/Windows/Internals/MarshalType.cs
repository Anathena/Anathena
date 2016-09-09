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
        /// <summary>
        /// The real type.
        /// </summary>
        public static Type RealType { get; private set; }

        /// <summary>
        /// The size of the type.
        /// </summary>
        public static Int32 Size { get; private set; }

        /// <summary>
        /// The typecode of the type.
        /// </summary>
        public static TypeCode TypeCode { get; private set; }

        /// <summary>
        /// Initializes static information related to the specified type.
        /// </summary>
        static MarshalType()
        {
            RealType = typeof(T);
            Size = TypeCode == TypeCode.Boolean ? 1 : Marshal.SizeOf(RealType);
            TypeCode = Type.GetTypeCode(RealType);
        }

        /// <summary>
        /// Marshals a managed object to an array of bytes.
        /// </summary>
        /// <param name="Object">The object to marshal.</param>
        /// <returns>A array of bytes corresponding to the managed object.</returns>
        public static Byte[] ObjectToByteArray(T Object)
        {
            // We'll tried to avoid marshalling as it really slows the process
            // First, check if the type can be converted without marhsalling
            switch (TypeCode)
            {
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
                case TypeCode.UInt16:
                    return BitConverter.GetBytes((UInt16)(Object)Object);
                case TypeCode.UInt32:
                    return BitConverter.GetBytes((UInt32)(Object)Object);
                case TypeCode.UInt64:
                    return BitConverter.GetBytes((UInt64)(Object)Object);
                default:
                    throw new ArgumentException("Invalid type provided");

            }
        }

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
                case TypeCode.UInt16:
                    return (T)(Object)BitConverter.ToUInt16(ByteArray, 0);
                case TypeCode.UInt32:
                    return (T)(Object)BitConverter.ToUInt32(ByteArray, 0);
                case TypeCode.UInt64:
                    return (T)(Object)BitConverter.ToUInt64(ByteArray, 0);
                default:
                    throw new ArgumentException("Invalid type provided");
            }
        }

    } // End class

} // End namespace