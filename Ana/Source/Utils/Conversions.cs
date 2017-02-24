namespace Ana.Source.Utils
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Collection of methods to convert values from one format to another format.
    /// </summary>
    internal class Conversions
    {
        /// <summary>
        /// Parse a string containing a non-hex value and return the value.
        /// </summary>
        /// <param name="valueType">The type the string represents.</param>
        /// <param name="value">The string to convert.</param>
        /// <returns>The value converted from the given string.</returns>
        public static dynamic ParsePrimitiveStringAsDynamic(Type valueType, String value)
        {
            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Byte:
                    return Byte.Parse(value);
                case TypeCode.Char:
                    return Byte.Parse(value);
                case TypeCode.SByte:
                    return SByte.Parse(value);
                case TypeCode.Int16:
                    return Int16.Parse(value);
                case TypeCode.Int32:
                    return Int32.Parse(value);
                case TypeCode.Int64:
                    return Int64.Parse(value);
                case TypeCode.UInt16:
                    return UInt16.Parse(value);
                case TypeCode.UInt32:
                    return UInt32.Parse(value);
                case TypeCode.UInt64:
                    return UInt64.Parse(value);
                case TypeCode.Single:
                    return Single.Parse(value);
                case TypeCode.Double:
                    return Double.Parse(value);
                default: return null;
            }
        }

        /// <summary>
        /// Converts a string containing hex characters to the given data type.
        /// </summary>
        /// <param name="valueType">The type to convert the parsed hex to.</param>
        /// <param name="value">The hex string to parse.</param>
        /// <returns>The converted value from the hex.</returns>
        public static dynamic ParseHexStringAsDynamic(Type valueType, String value)
        {
            return ParsePrimitiveStringAsDynamic(valueType, ParseHexStringAsPrimitiveString(valueType, value));
        }

        /// <summary>
        /// Parses a raw value as a hex string.
        /// </summary>
        /// <param name="valueType">The data type of the value.</param>
        /// <param name="value">The raw value.</param>
        /// <returns>The converted hex string.</returns>
        public static String ParseDynamicAsHexString(Type valueType, dynamic value)
        {
            return ParsePrimitiveStringAsHexString(valueType, value?.ToString());
        }

        /// <summary>
        /// Converts a string containing dec characters to the hex equivalent for the given data type.
        /// </summary>
        /// <param name="valueType">The value type.</param>
        /// <param name="value">The hex string to parse.</param>
        /// <returns>The converted value from the hex.</returns>
        public static String ParsePrimitiveStringAsHexString(Type valueType, String value)
        {
            dynamic realValue = ParsePrimitiveStringAsDynamic(valueType, value);

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return realValue.ToString("X");
                case TypeCode.Single:
                    return BitConverter.ToUInt32(BitConverter.GetBytes(realValue), 0).ToString("X");
                case TypeCode.Double:
                    return BitConverter.ToUInt64(BitConverter.GetBytes(realValue), 0).ToString("X");
                default: return null;
            }
        }

        /// <summary>
        /// Converts a string containing hex characters to the dec equivalent for the given data type.
        /// </summary>
        /// <param name="valueType">The value type.</param>
        /// <param name="value">The dec string to parse.</param>
        /// <returns>The converted value from the dec.</returns>
        public static String ParseHexStringAsPrimitiveString(Type valueType, String value)
        {
            UInt64 realValue = AddressToValue(value);

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Byte:
                    return realValue.ToString();
                case TypeCode.Char:
                    return realValue.ToString();
                case TypeCode.SByte:
                    return unchecked((SByte)realValue).ToString();
                case TypeCode.Int16:
                    return unchecked((Int16)realValue).ToString();
                case TypeCode.Int32:
                    return unchecked((Int32)realValue).ToString();
                case TypeCode.Int64:
                    return unchecked((Int64)realValue).ToString();
                case TypeCode.UInt16:
                    return realValue.ToString();
                case TypeCode.UInt32:
                    return realValue.ToString();
                case TypeCode.UInt64:
                    return realValue.ToString();
                case TypeCode.Single:
                    return BitConverter.ToSingle(BitConverter.GetBytes(unchecked((UInt32)realValue)), 0).ToString();
                case TypeCode.Double:
                    return BitConverter.ToDouble(BitConverter.GetBytes(realValue), 0).ToString();
                default: return null;
            }
        }

        /// <summary>
        /// Converts a given value to hex.
        /// </summary>
        /// <typeparam name="T">The data type of the value being converted.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="formatAsAddress">Whether or not to use a zero padded address format.</param>
        /// <param name="includePrefix">Whether or not to include the '0x' hex prefix.</param>
        /// <returns>The value converted to hex.</returns>
        public static String ToHex<T>(T value, Boolean formatAsAddress = true, Boolean includePrefix = false)
        {
            Type dataType = typeof(T);

            // If a pointer type, parse as a long integer
            if (dataType == typeof(IntPtr))
            {
                dataType = typeof(Int64);
            }
            else if (dataType == typeof(UIntPtr))
            {
                dataType = typeof(UInt64);
            }

            String result = Conversions.ParsePrimitiveStringAsHexString(dataType, value.ToString());

            if (formatAsAddress)
            {
                if (result.Length <= 8)
                {
                    result = result.PadLeft(8, '0');
                }
                else
                {
                    result = result.PadLeft(16, '0');
                }
            }

            if (includePrefix)
            {
                result = "0x" + result;
            }

            return result;
        }

        /// <summary>
        /// Converts an address string to a raw value.
        /// </summary>
        /// <param name="address">The address hex string.</param>
        /// <returns>The raw value as a <see cref="UInt64"/></returns>
        public static UInt64 AddressToValue(String address)
        {
            if (String.IsNullOrEmpty(address))
            {
                return 0;
            }

            if (address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                address = address.Substring("0x".Length);
            }

            address = address.TrimStart('0');

            if (String.IsNullOrEmpty(address))
            {
                return 0;
            }

            return UInt64.Parse(address, System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// Gets the type from the given type code.
        /// </summary>
        /// <param name="typeCode">The type code.</param>
        /// <returns>Returns the data type, or null if the conversion is not possible.</returns>
        public static Type TypeCodeToType(TypeCode? typeCode)
        {
            if (typeCode == null)
            {
                return null;
            }

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return typeof(Boolean);
                case TypeCode.Byte:
                    return typeof(Byte);
                case TypeCode.Char:
                    return typeof(Char);
                case TypeCode.DateTime:
                    return typeof(DateTime);
                case TypeCode.DBNull:
                    return typeof(DBNull);
                case TypeCode.Decimal:
                    return typeof(Decimal);
                case TypeCode.Double:
                    return typeof(Double);
                case TypeCode.Int16:
                    return typeof(Int16);
                case TypeCode.Int32:
                    return typeof(Int32);
                case TypeCode.Int64:
                    return typeof(Int64);
                case TypeCode.Object:
                    return typeof(Object);
                case TypeCode.SByte:
                    return typeof(SByte);
                case TypeCode.Single:
                    return typeof(Single);
                case TypeCode.String:
                    return typeof(String);
                case TypeCode.UInt16:
                    return typeof(UInt16);
                case TypeCode.UInt32:
                    return typeof(UInt32);
                case TypeCode.UInt64:
                    return typeof(UInt64);
                default:
                    break;
            }

            return null;
        }

        /// <summary>
        /// Gets the size of the given data type.
        /// </summary>
        /// <typeparam name="T">The data type.</typeparam>
        /// <returns>The size of the given type.</returns>
        public static Int32 GetTypeSize<T>()
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    return sizeof(Boolean);
                case TypeCode.Byte:
                    return sizeof(Byte);
                case TypeCode.Char:
                    return sizeof(Char);
                case TypeCode.Decimal:
                    return sizeof(Decimal);
                case TypeCode.Double:
                    return sizeof(Double);
                case TypeCode.Int16:
                    return sizeof(Int16);
                case TypeCode.Int32:
                    return sizeof(Int32);
                case TypeCode.Int64:
                    return sizeof(Int64);
                case TypeCode.SByte:
                    return sizeof(SByte);
                case TypeCode.Single:
                    return sizeof(Single);
                case TypeCode.UInt16:
                    return sizeof(UInt16);
                case TypeCode.UInt32:
                    return sizeof(UInt32);
                case TypeCode.UInt64:
                    return sizeof(UInt64);
                default:
                    return Marshal.SizeOf(typeof(T));
            }
        }

        /// <summary>
        /// Gets the size of the given data type.
        /// </summary>
        /// <param name="type">The data type.</param>
        /// <returns>The size of the given type.</returns>
        public static Int32 GetTypeSize(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return sizeof(Boolean);
                case TypeCode.Byte:
                    return sizeof(Byte);
                case TypeCode.Char:
                    return sizeof(Char);
                case TypeCode.Decimal:
                    return sizeof(Decimal);
                case TypeCode.Double:
                    return sizeof(Double);
                case TypeCode.Int16:
                    return sizeof(Int16);
                case TypeCode.Int32:
                    return sizeof(Int32);
                case TypeCode.Int64:
                    return sizeof(Int64);
                case TypeCode.SByte:
                    return sizeof(SByte);
                case TypeCode.Single:
                    return sizeof(Single);
                case TypeCode.UInt16:
                    return sizeof(UInt16);
                case TypeCode.UInt32:
                    return sizeof(UInt32);
                case TypeCode.UInt64:
                    return sizeof(UInt64);
                default:
                    return Marshal.SizeOf(type);
            }
        }

        /// <summary>
        /// Converts an array of bytes to an object.
        /// </summary>
        /// <typeparam name="T">The data type of the object.</typeparam>
        /// <param name="byteArray">The array of bytes.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="ArgumentException">If unable to handle the conversion.</exception>
        public static T BytesToObject<T>(Byte[] byteArray)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    return (T)(Object)BitConverter.ToBoolean(byteArray, 0);
                case TypeCode.Byte:
                    return (T)(Object)byteArray[0];
                case TypeCode.Char:
                    return (T)(Object)BitConverter.ToChar(byteArray, 0);
                case TypeCode.Double:
                    return (T)(Object)BitConverter.ToDouble(byteArray, 0);
                case TypeCode.Int16:
                    return (T)(Object)BitConverter.ToInt16(byteArray, 0);
                case TypeCode.Int32:
                    return (T)(Object)BitConverter.ToInt32(byteArray, 0);
                case TypeCode.Int64:
                    return (T)(Object)BitConverter.ToInt64(byteArray, 0);
                case TypeCode.Single:
                    return (T)(Object)BitConverter.ToSingle(byteArray, 0);
                case TypeCode.UInt16:
                    return (T)(Object)BitConverter.ToUInt16(byteArray, 0);
                case TypeCode.UInt32:
                    return (T)(Object)BitConverter.ToUInt32(byteArray, 0);
                case TypeCode.UInt64:
                    return (T)(Object)BitConverter.ToUInt64(byteArray, 0);
                default:
                    throw new ArgumentException("Invalid type provided");
            }
        }
    }
    //// End class
}
//// End namespace