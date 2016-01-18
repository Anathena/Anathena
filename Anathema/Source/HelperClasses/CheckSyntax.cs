using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public static class CheckSyntax
    {
        private static unsafe int MaxHexAddressLength = sizeof(IntPtr) * 2;

        // Checks if passed value is a valid address
        public static Boolean Address(String Address)
        {
            if (Address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Address = Address.Substring(2);

            // Out of bounds
            if (Address == String.Empty || Address.Length > MaxHexAddressLength)
                return false;

            // Too short: assume preceding 0s are intended
            while (Address.Length < MaxHexAddressLength)
                Address = "0" + Address;

            UInt64 Result;
            if (UInt64.TryParse(Address, System.Globalization.NumberStyles.HexNumber, null, out Result))
                return true; // Valid
            return false;
        }

        // Checks if passed value is a valid hex value
        public static Boolean HexValue(String Value, Int32 Bytes)
        {
            if (Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Value = Value.Substring(2);

            // Check if string is empty
            if (Value == String.Empty)
                return false;

            // Remove leading 0s and check if out of bounds
            while (Value.Length > Bytes)
            {
                if (Value.Substring(0, 1) == "0")
                    Value = Value.Substring(1);
                else   // Value too large (there were no leading 0s that caused it)
                    return false;
            }

            // Try to read value from hex string
            Int32 result;
            if (Int32.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out result))
                return true;

            return false; // Invalid
        }

        public static Boolean CanParseValue(Type ValueType, String Value)
        {
            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: return IsByte(Value);
                case TypeCode.SByte: return IsSByte(Value);
                case TypeCode.Int16: return IsInt16(Value);
                case TypeCode.Int32: return IsInt32(Value);
                case TypeCode.Int64: return IsInt64(Value);
                case TypeCode.UInt16: return IsUInt16(Value);
                case TypeCode.UInt32: return IsUInt32(Value);
                case TypeCode.UInt64: return IsUInt64(Value);
                case TypeCode.Single: return IsSingle(Value);
                case TypeCode.Double: return IsDouble(Value);
                default: return false;
            }
        }

        // Checks if passed value is a valid binary value
        public static Boolean BinaryValue(String Value)
        {
            for (int Index = 0; Index < Value.Length; Index++)
            {
                // Check each character for a value of 0
                if (Value.Substring(Index, 1) != "0" || Value.Substring(Index, 1) != "1")
                    break;

                // Returns true if all characters were 0 or 1
                if (Index == Value.Length - 1)
                    return true;
            }
            return false;
        }

        public static Boolean IsByte(String Value)
        {
            Byte Temp;
            return Byte.TryParse(Value, out Temp);
        }

        public static Boolean IsSByte(String Value)
        {
            SByte Temp;
            return SByte.TryParse(Value, out Temp);
        }

        public static Boolean IsInt16(String Value)
        {
            Int16 Temp;
            return Int16.TryParse(Value, out Temp);
        }

        public static Boolean IsUInt16(String Value)
        {
            UInt16 Temp;
            return UInt16.TryParse(Value, out Temp);
        }

        public static Boolean IsInt32(String Value)
        {
            Int32 Temp;
            return Int32.TryParse(Value, out Temp);
        }

        public static Boolean IsUInt32(String Value)
        {
            UInt32 Temp;
            return UInt32.TryParse(Value, out Temp);
        }

        public static Boolean IsInt64(String Value)
        {
            Int64 Temp;
            return Int64.TryParse(Value, out Temp);
        }

        public static Boolean IsUInt64(String Value)
        {
            UInt64 Temp;
            return UInt64.TryParse(Value, out Temp);
        }

        public static Boolean IsSingle(String Value)
        {
            Single Temp;
            return Single.TryParse(Value, out Temp);
        }

        public static Boolean IsDouble(String Value)
        {
            Double Temp;
            return Double.TryParse(Value, out Temp);
        }

    } // End class

} // End namespace