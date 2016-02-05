using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Converts a value from one format to another format. No validation checking is done, see CheckSyntax class for this
    /// </summary>
    class Conversions
    {
        public static Type StringToPrimitiveType(String Value)
        {
            return PrimitiveTypes.GetPrimitiveTypes().Where(x => x.Name == Value).First();
        }

        public static dynamic ParseValue(Type ValueType, String Value)
        {
            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: return Byte.Parse(Value);
                case TypeCode.SByte: return SByte.Parse(Value);
                case TypeCode.Int16: return Int16.Parse(Value);
                case TypeCode.Int32: return Int32.Parse(Value);
                case TypeCode.Int64: return Int64.Parse(Value);
                case TypeCode.UInt16: return UInt16.Parse(Value);
                case TypeCode.UInt32: return UInt32.Parse(Value);
                case TypeCode.UInt64: return UInt64.Parse(Value);
                case TypeCode.Single: return Single.Parse(Value);
                case TypeCode.Double: return Double.Parse(Value);
                default: return null;
            }
        }

        public static String ParseAsHex(Type ValueType, String Value)
        {
            dynamic RealValue = ParseValue(ValueType, Value);

            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: return RealValue.ToString("X");
                case TypeCode.SByte: return RealValue.ToString("X");
                case TypeCode.Int16: return RealValue.ToString("X");
                case TypeCode.Int32: return RealValue.ToString("X");
                case TypeCode.Int64: return RealValue.ToString("X");
                case TypeCode.UInt16: return RealValue.ToString("X");
                case TypeCode.UInt32: return RealValue.ToString("X");
                case TypeCode.UInt64: return RealValue.ToString("X");
                case TypeCode.Single: return BitConverter.ToSingle(BitConverter.GetBytes(Single.Parse(RealValue)), 0).ToString("X");
                case TypeCode.Double: return BitConverter.ToSingle(BitConverter.GetBytes(Double.Parse(RealValue)), 0).ToString("X");
                default: return null;
            }
        }

        public static String ToAddress(Int32 Value)
        {
            return ToAddress(unchecked((UInt32)Value));
        }

        public static String ToAddress(UInt32 Value)
        {
            String ValueString = Value.ToString();

            if (CheckSyntax.IsUInt32(ValueString))
                return String.Format("{0:X8}", Convert.ToUInt32(Value));
            else if (CheckSyntax.IsInt32(ValueString))
                return String.Format("{0:X8}", unchecked((UInt32)(Convert.ToInt32(Value))));
            else
                return "!!";
        }

        public static String ToAddress(Int64 Value)
        {
            return ToAddress(unchecked((UInt64)Value));
        }

        public static String ToAddress(UInt64 Value)
        {
            String ValueString = Value.ToString();

            if (CheckSyntax.IsUInt32(ValueString))
                return String.Format("{0:X8}", Convert.ToUInt32(Value));
            else if (CheckSyntax.IsInt32(ValueString))
                return String.Format("{0:X8}", unchecked((UInt32)(Convert.ToInt32(Value))));
            else if (CheckSyntax.IsUInt64(ValueString))
                return String.Format("{0:X16}", Convert.ToUInt64(Value));
            else if (CheckSyntax.IsInt64(ValueString))
                return String.Format("{0:X16}", unchecked((UInt64)(Convert.ToInt64(Value))));
            else
                return "!!";
        }

        public static UInt64 AddressToValue(String Address)
        {
            if (Address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Address = Address.Substring(2);

            while (Address.StartsWith("0"))
                Address = Address.Substring(1);

            return UInt64.Parse(Address, System.Globalization.NumberStyles.HexNumber);
        }

        public static String BytesToMetric(UInt64 ByteCount)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; // Longs run out around EB

            if (ByteCount == 0)
                return "0" + Suffix[0];

            int Place = Convert.ToInt32(Math.Floor(Math.Log(ByteCount, 1024)));
            double Number = Math.Round(ByteCount / Math.Pow(1024, Place), 1);
            return (Number.ToString() + Suffix[Place]);
        }

    } // End class

} // End namespace