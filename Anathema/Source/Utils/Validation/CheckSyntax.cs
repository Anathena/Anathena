using System;

namespace Anathema.Source.Utils.Validation
{
    public static class CheckSyntax
    {
        // Checks if passed value is a valid address
        public static Boolean CanParseAddress(String Address, Boolean MustBe32Bit = false)
        {
            if (Address == null)
                return false;

            // Remove 0x hex specifier
            if (Address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Address = Address.Substring(2);

            // Remove trailing 0s
            while (Address.StartsWith("0") && Address.Length > 1)
                Address = Address.Substring(1);

            if (MustBe32Bit)
                return IsUInt32(Address, true);
            else
                return IsUInt64(Address, true);
        }

        public static Boolean CanParseValue(Type ValueType, String Value)
        {
            if (ValueType == null)
                return false;

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

        public static Boolean CanParseHex(Type ValueType, String Value)
        {
            if (Value == null)
                return false;

            // Remove 0x hex specifier
            if (Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Value = Value.Substring(2);

            // Remove trailing 0s
            while (Value.StartsWith("0") && Value.Length > 1)
                Value = Value.Substring(1);

            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: return IsByte(Value, true);
                case TypeCode.SByte: return IsSByte(Value, true);
                case TypeCode.Int16: return IsInt16(Value, true);
                case TypeCode.Int32: return IsInt32(Value, true);
                case TypeCode.Int64: return IsInt64(Value, true);
                case TypeCode.UInt16: return IsUInt16(Value, true);
                case TypeCode.UInt32: return IsUInt32(Value, true);
                case TypeCode.UInt64: return IsUInt64(Value, true);
                case TypeCode.Single: return IsSingle(Value, true);
                case TypeCode.Double: return IsDouble(Value, true);
                default: return false;
            }
        }

        public static Boolean IsByte(String Value, Boolean IsHex = false)
        {
            Byte Temp;
            if (IsHex)
                return Byte.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out Temp);
            else
                return Byte.TryParse(Value, out Temp);
        }

        public static Boolean IsSByte(String Value, Boolean IsHex = false)
        {
            SByte Temp;
            if (IsHex)
                return SByte.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out Temp);
            else
                return SByte.TryParse(Value, out Temp);
        }

        public static Boolean IsInt16(String Value, Boolean IsHex = false)
        {
            Int16 Temp;
            if (IsHex)
                return Int16.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out Temp);
            else
                return Int16.TryParse(Value, out Temp);
        }

        public static Boolean IsUInt16(String Value, Boolean IsHex = false)
        {
            UInt16 Temp;
            if (IsHex)
                return UInt16.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out Temp);
            else
                return UInt16.TryParse(Value, out Temp);
        }

        public static Boolean IsInt32(String Value, Boolean IsHex = false)
        {
            Int32 Temp;
            if (IsHex)
                return Int32.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out Temp);
            else
                return Int32.TryParse(Value, out Temp);
        }

        public static Boolean IsUInt32(String Value, Boolean IsHex = false)
        {
            UInt32 Temp;
            if (IsHex)
                return UInt32.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out Temp);
            else
                return UInt32.TryParse(Value, out Temp);
        }

        public static Boolean IsInt64(String Value, Boolean IsHex = false)
        {
            Int64 Temp;
            if (IsHex)
                return Int64.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out Temp);
            else
                return Int64.TryParse(Value, out Temp);
        }

        public static Boolean IsUInt64(String Value, Boolean IsHex = false)
        {
            UInt64 Temp;
            if (IsHex)
                return UInt64.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out Temp);
            else
                return UInt64.TryParse(Value, out Temp);
        }

        public static Boolean IsSingle(String Value, Boolean IsHex = false)
        {
            Single Temp;
            if (IsHex && IsUInt32(Value, IsHex))
                return Single.TryParse(Conversions.ParseHexAsDec(typeof(Single), Value), out Temp);
            else
                return Single.TryParse(Value, out Temp);
        }

        public static Boolean IsDouble(String Value, Boolean IsHex = false)
        {
            Double Temp;
            if (IsHex && IsUInt64(Value, IsHex))
                return Double.TryParse(Conversions.ParseHexAsDec(typeof(Double), Value), out Temp);
            else
                return Double.TryParse(Value, out Temp);
        }

    } // End class

} // End namespace