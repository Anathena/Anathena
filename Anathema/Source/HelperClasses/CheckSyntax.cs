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
        public static Boolean CanParseAddress(String Address, Boolean MustBe32Bit = false)
        {
            // Remove 0x
            if (Address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Address = Address.Substring(2);

            // Remove trailing 0s
            while (Address.StartsWith("0"))
                Address = Address.Substring(1);

            // Bounds checking
            if (Address == String.Empty || Address.Length > MaxHexAddressLength)
                return false;

            if (MustBe32Bit)
            {
                UInt32 Result;
                if (UInt32.TryParse(Address, System.Globalization.NumberStyles.HexNumber, null, out Result))
                    return true;
            }
            else
            {
                UInt64 Result;
                if (UInt64.TryParse(Address, System.Globalization.NumberStyles.HexNumber, null, out Result))
                    return true;
            }

            return false;
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