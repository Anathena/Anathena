namespace Ana.Source.Utils
{
    using System;

    /// <summary>
    /// A static class used to check syntax for various values and types
    /// </summary>
    public static class CheckSyntax
    {
        /// <summary>
        /// Checks if the provided string is a valid address
        /// </summary>
        /// <param name="address">The address as a hex string</param>
        /// <param name="mustBe32Bit">Whether or not the address must strictly be containable in 32 bits</param>
        /// <returns>A boolean indicating if the address is parseable</returns>
        public static Boolean CanParseAddress(String address, Boolean mustBe32Bit = false)
        {
            if (address == null)
            {
                return false;
            }

            // Remove 0x hex specifier
            if (address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                address = address.Substring(2);
            }

            // Remove trailing 0s
            while (address.StartsWith("0") && address.Length > 1)
            {
                address = address.Substring(1);
            }

            if (mustBe32Bit)
            {
                return IsUInt32(address, true);
            }
            else
            {
                return IsUInt64(address, true);
            }
        }

        /// <summary>
        /// Determines if a value of the given type can be parsed from the given string
        /// </summary>
        /// <param name="valueType">The type of the given value</param>
        /// <param name="value">The value to be parsed</param>
        /// <returns>A boolean indicating if the value is parseable</returns>
        public static Boolean CanParseValue(Type valueType, String value)
        {
            if (valueType == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Byte:
                    return CheckSyntax.IsByte(value);
                case TypeCode.SByte:
                    return CheckSyntax.IsSByte(value);
                case TypeCode.Int16:
                    return CheckSyntax.IsInt16(value);
                case TypeCode.Int32:
                    return CheckSyntax.IsInt32(value);
                case TypeCode.Int64:
                    return CheckSyntax.IsInt64(value);
                case TypeCode.UInt16:
                    return CheckSyntax.IsUInt16(value);
                case TypeCode.UInt32:
                    return CheckSyntax.IsUInt32(value);
                case TypeCode.UInt64:
                    return CheckSyntax.IsUInt64(value);
                case TypeCode.Single:
                    return CheckSyntax.IsSingle(value);
                case TypeCode.Double:
                    return CheckSyntax.IsDouble(value);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines if a hex value can be parsed from the given string
        /// </summary>
        /// <param name="valueType">The type of the given value</param>
        /// <param name="value">The value to be parsed</param>
        /// <returns>A boolean indicating if the value is parseable as hex</returns>
        public static Boolean CanParseHex(Type valueType, String value)
        {
            if (value == null)
            {
                return false;
            }

            // Remove 0x hex specifier
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                value = value.Substring(2);
            }

            // Remove trailing 0s
            while (value.StartsWith("0") && value.Length > 1)
            {
                value = value.Substring(1);
            }

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Byte:
                    return IsByte(value, true);
                case TypeCode.SByte:
                    return IsSByte(value, true);
                case TypeCode.Int16:
                    return IsInt16(value, true);
                case TypeCode.Int32:
                    return IsInt32(value, true);
                case TypeCode.Int64:
                    return IsInt64(value, true);
                case TypeCode.UInt16:
                    return IsUInt16(value, true);
                case TypeCode.UInt32:
                    return IsUInt32(value, true);
                case TypeCode.UInt64:
                    return IsUInt64(value, true);
                case TypeCode.Single:
                    return IsSingle(value, true);
                case TypeCode.Double:
                    return IsDouble(value, true);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a byte
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsByte(String value, Boolean isHex = false)
        {
            Byte temp;

            if (isHex)
            {
                return Byte.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out temp);
            }
            else
            {
                return Byte.TryParse(value, out temp);
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a signed byte
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsSByte(String value, Boolean isHex = false)
        {
            SByte temp;

            if (isHex)
            {
                return SByte.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out temp);
            }
            else
            {
                return SByte.TryParse(value, out temp);
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a 16 bit integer
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsInt16(String value, Boolean isHex = false)
        {
            Int16 temp;

            if (isHex)
            {
                return Int16.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out temp);
            }
            else
            {
                return Int16.TryParse(value, out temp);
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a 16 bit signed integer
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsUInt16(String value, Boolean isHex = false)
        {
            UInt16 temp;

            if (isHex)
            {
                return UInt16.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out temp);
            }
            else
            {
                return UInt16.TryParse(value, out temp);
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a 32 bit integer
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsInt32(String value, Boolean isHex = false)
        {
            Int32 temp;

            if (isHex)
            {
                return Int32.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out temp);
            }
            else
            {
                return Int32.TryParse(value, out temp);
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a 32 bit signed integer
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsUInt32(String value, Boolean isHex = false)
        {
            UInt32 temp;

            if (isHex)
            {
                return UInt32.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out temp);
            }
            else
            {
                return UInt32.TryParse(value, out temp);
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a 64 bit integer
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsInt64(String value, Boolean isHex = false)
        {
            Int64 temp;

            if (isHex)
            {
                return Int64.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out temp);
            }
            else
            {
                return Int64.TryParse(value, out temp);
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a 64 bit signed integer
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsUInt64(String value, Boolean isHex = false)
        {
            UInt64 temp;

            if (isHex)
            {
                return UInt64.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out temp);
            }
            else
            {
                return UInt64.TryParse(value, out temp);
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a single precision floating point number
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsSingle(String value, Boolean isHex = false)
        {
            Single temp;

            if (isHex && IsUInt32(value, isHex))
            {
                return Single.TryParse(Conversions.ParseHexStringAsPrimitiveString(typeof(Single), value), out temp);
            }
            else
            {
                return Single.TryParse(value, out temp);
            }
        }

        /// <summary>
        /// Determines if the given string can be parsed as a double precision floating point number
        /// </summary>
        /// <param name="value">The value as a string</param>
        /// <param name="isHex">Whether or not the value is encoded in hex</param>
        /// <returns>A boolean indicating if the value could be parsed</returns>
        public static Boolean IsDouble(String value, Boolean isHex = false)
        {
            Double temp;

            if (isHex && IsUInt64(value, isHex))
            {
                return Double.TryParse(Conversions.ParseHexStringAsPrimitiveString(typeof(Double), value), out temp);
            }
            else
            {
                return Double.TryParse(value, out temp);
            }
        }
    }
    //// End class
}
//// End namespace