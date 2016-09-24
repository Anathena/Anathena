namespace Ana.Source.Utils.Validation
{
    using Extensions;
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Collection of methods to convert values from one format to another format
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    internal class Conversions
    {
        /// <summary>
        /// Converts a string to the corresponding primitive type
        /// </summary>
        /// <param name="value">The string to convert</param>
        /// <returns>The type that corresponds to the given string</returns>
        public static Type StringToPrimitiveType(String value)
        {
            return PrimitiveTypes.GetPrimitiveTypes().Where(x => x.Name == value).First();
        }

        /// <summary>
        /// Parse a string containing a non-hex value and return the value
        /// </summary>
        /// <param name="valueType">The type the string represents</param>
        /// <param name="value">The string to convert</param>
        /// <returns>The value converted from the given string</returns>
        public static dynamic ParseDecStringAsValue(Type valueType, String value)
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
        /// Converts a string containing hex characters to the given data type
        /// </summary>
        /// <param name="valueType">The type to convert the parsed hex to</param>
        /// <param name="value">The hex string to parse</param>
        /// <returns>The converted value from the hex</returns>
        public static dynamic ParseHexStringAsValue(Type valueType, String value)
        {
            return ParseDecStringAsValue(valueType, ParseHexStringAsDecString(valueType, value));
        }

        public static String ParseValueAsDec(Type valueType, dynamic value)
        {
            return ParseDecStringAsValue(valueType, value?.ToString()).ToString();
        }

        public static String ParseValueAsHex(Type valueType, dynamic value)
        {
            return ParseDecStringAsHexString(valueType, value?.ToString());
        }

        public static String ParseDecStringAsHexString(Type valueType, String value)
        {
            dynamic vealValue = ParseDecStringAsValue(valueType, value);

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Byte:
                    return vealValue.ToString("X");
                case TypeCode.Char:
                    return vealValue.ToString("X");
                case TypeCode.SByte:
                    return vealValue.ToString("X");
                case TypeCode.Int16:
                    return vealValue.ToString("X");
                case TypeCode.Int32:
                    return vealValue.ToString("X");
                case TypeCode.Int64:
                    return vealValue.ToString("X");
                case TypeCode.UInt16:
                    return vealValue.ToString("X");
                case TypeCode.UInt32:
                    return vealValue.ToString("X");
                case TypeCode.UInt64:
                    return vealValue.ToString("X");
                case TypeCode.Single:
                    return BitConverter.ToUInt32(BitConverter.GetBytes(vealValue), 0).ToString("X");
                case TypeCode.Double:
                    return BitConverter.ToUInt64(BitConverter.GetBytes(vealValue), 0).ToString("X");
                default: return null;
            }
        }

        public static String ParseHexStringAsDecString(Type valueType, String value)
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

        public static String ToAddress(Int32 value)
        {
            return ToAddress(unchecked((UInt32)value));
        }

        public static String ToAddress(UInt32 value)
        {
            String valueString = value.ToString();

            if (CheckSyntax.IsUInt32(valueString))
            {
                return String.Format("{0:X8}", Convert.ToUInt32(value));
            }
            else if (CheckSyntax.IsInt32(valueString))
            {
                return String.Format("{0:X8}", unchecked((UInt32)Convert.ToInt32(value)));
            }
            else
            {
                return String.Empty;
            }
        }

        public static String ToAddress(Int64 value)
        {
            return ToAddress(unchecked(value));
        }

        public static String ToAddress(UInt64 value)
        {
            String valueString = value.ToString();

            if (CheckSyntax.IsUInt32(valueString))
            {
                return String.Format("{0:X8}", Convert.ToUInt32(value));
            }
            else if (CheckSyntax.IsInt32(valueString))
            {
                return String.Format("{0:X8}", unchecked((UInt32)Convert.ToInt32(value)));
            }
            else if (CheckSyntax.IsUInt64(valueString))
            {
                return String.Format("{0:X16}", Convert.ToUInt64(value));
            }
            else if (CheckSyntax.IsInt64(valueString))
            {
                return String.Format("{0:X16}", unchecked((UInt64)Convert.ToInt64(value)));
            }
            else
            {
                return String.Empty;
            }
        }

        public static String ToAddress(IntPtr value)
        {
            return ToAddress(value.ToUInt64());
        }

        public static String ToAddress(UIntPtr value)
        {
            return ToAddress(value.ToUInt64());
        }

        public static UInt64 AddressToValue(String address)
        {
            if (address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                address = address.Substring(2);
            }

            while (address.StartsWith("0") && address.Length > 1)
            {
                address = address.Substring(1);
            }

            return UInt64.Parse(address, System.Globalization.NumberStyles.HexNumber);
        }

        public static Type TypeCodeToType(TypeCode? typeCode)
        {
            if (typeCode == null)
            {
                return null;
            }

            switch (typeCode)
            {
                case TypeCode.Boolean: return typeof(Boolean);
                case TypeCode.Byte: return typeof(Byte);
                case TypeCode.Char: return typeof(Char);
                case TypeCode.DateTime: return typeof(DateTime);
                case TypeCode.DBNull: return typeof(DBNull);
                case TypeCode.Decimal: return typeof(Decimal);
                case TypeCode.Double: return typeof(Double);
                case TypeCode.Int16: return typeof(Int16);
                case TypeCode.Int32: return typeof(Int32);
                case TypeCode.Int64: return typeof(Int64);
                case TypeCode.Object: return typeof(Object);
                case TypeCode.SByte: return typeof(SByte);
                case TypeCode.Single: return typeof(Single);
                case TypeCode.String: return typeof(String);
                case TypeCode.UInt16: return typeof(UInt16);
                case TypeCode.UInt32: return typeof(UInt32);
                case TypeCode.UInt64: return typeof(UInt64);
                case TypeCode.Empty: return null;
            }

            return null;
        }

        /// <summary>
        /// Converts a given number of bytes to the most relevant metric. ie 2123 bytes => 2KB
        /// </summary>
        /// <typeparam name="T">The data type expressing the byte count</typeparam>
        /// <param name="byteCount">The number of bytes</param>
        /// <returns>The byte count converted to a string representing the same value as the most relevant metric</returns>
        public static String BytesToMetric<T>(T byteCount) where T : struct
        {
            // Note: UInt64s run out around EB
            String[] suffix = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

            UInt64 realByteCount = (UInt64)Convert.ChangeType(byteCount, typeof(UInt64));

            if (realByteCount == 0)
            {
                return "0" + suffix[0];
            }

            Int32 place = Convert.ToInt32(Math.Floor(Math.Log(realByteCount, 1024)));
            Double number = Math.Round(realByteCount / Math.Pow(1024, place), 1);
            return number.ToString() + suffix[place];
        }

        public static Int32 GetTypeSize<T>()
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean: return sizeof(Boolean);
                case TypeCode.Byte: return sizeof(Byte);
                case TypeCode.Char: return sizeof(Char);
                case TypeCode.Decimal: return sizeof(Decimal);
                case TypeCode.Double: return sizeof(Double);
                case TypeCode.Int16: return sizeof(Int16);
                case TypeCode.Int32: return sizeof(Int32);
                case TypeCode.Int64: return sizeof(Int64);
                case TypeCode.SByte: return sizeof(SByte);
                case TypeCode.Single: return sizeof(Single);
                case TypeCode.UInt16: return sizeof(UInt16);
                case TypeCode.UInt32: return sizeof(UInt32);
                case TypeCode.UInt64: return sizeof(UInt64);
                default: throw new Exception("Type is not a primitive");
            }
        }

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