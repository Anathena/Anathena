using Anathema.Source.Utils.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace Anathema.Source.Utils.Validation
{
    /// <summary>
    /// Converts a value from one format to another format. No validation checking is done, see CheckSyntax class for this
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    class Conversions
    {
        public static Type StringToPrimitiveType(String Value)
        {
            return PrimitiveTypes.GetPrimitiveTypes().Where(X => X.Name == Value).First();
        }

        public static dynamic ParseDecStringAsValue(Type ValueType, String Value)
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

        public static dynamic ParseHexStringAsValue(Type ValueType, String Value)
        {
            return ParseDecStringAsValue(ValueType, ParseHexStringAsDecString(ValueType, Value));
        }

        public static String ParseValueAsDec(Type ValueType, dynamic Value)
        {
            return ParseDecStringAsValue(ValueType, Value?.ToString()).ToString();
        }

        public static String ParseValueAsHex(Type ValueType, dynamic Value)
        {
            return ParseDecStringAsValue(ValueType, Value?.ToString()).ToString("X");
        }

        public static String ParseDecStringAsHexString(Type ValueType, String Value)
        {
            dynamic RealValue = ParseDecStringAsValue(ValueType, Value);

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
                case TypeCode.Single: return BitConverter.ToUInt32(BitConverter.GetBytes(RealValue), 0).ToString("X");
                case TypeCode.Double: return BitConverter.ToUInt64(BitConverter.GetBytes(RealValue), 0).ToString("X");
                default: return null;
            }
        }

        public static String ParseHexStringAsDecString(Type ValueType, String Value)
        {
            UInt64 RealValue = AddressToValue(Value);

            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: return RealValue.ToString();
                case TypeCode.SByte: return unchecked((SByte)(RealValue)).ToString();
                case TypeCode.Int16: return unchecked((Int16)(RealValue)).ToString();
                case TypeCode.Int32: return unchecked((Int32)(RealValue)).ToString();
                case TypeCode.Int64: return unchecked((Int64)(RealValue)).ToString();
                case TypeCode.UInt16: return RealValue.ToString();
                case TypeCode.UInt32: return RealValue.ToString();
                case TypeCode.UInt64: return RealValue.ToString();
                case TypeCode.Single: return BitConverter.ToSingle(BitConverter.GetBytes(unchecked((UInt32)RealValue)), 0).ToString();
                case TypeCode.Double: return BitConverter.ToDouble(BitConverter.GetBytes(RealValue), 0).ToString();
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
                return String.Empty;
        }

        public static String ToAddress(Int64 Value)
        {
            return ToAddress(unchecked(Value));
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
                return String.Empty;
        }

        public static String ToAddress(IntPtr Value)
        {
            return ToAddress(Value.ToUInt64());
        }

        public static String ToAddress(UIntPtr Value)
        {
            return ToAddress(Value.ToUInt64());
        }

        public static UInt64 AddressToValue(String Address)
        {
            if (Address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Address = Address.Substring(2);

            while (Address.StartsWith("0") && Address.Length > 1)
                Address = Address.Substring(1);

            return UInt64.Parse(Address, System.Globalization.NumberStyles.HexNumber);
        }

        public static String BytesToMetric<T>(T ByteCount)
        {
            // Note: UInt64s run out around EB
            String[] Suffix = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

            UInt64 RealByteCount = (UInt64)Convert.ChangeType(ByteCount, typeof(UInt64));

            if (RealByteCount == 0)
                return "0" + Suffix[0];

            Int32 Place = Convert.ToInt32(Math.Floor(Math.Log(RealByteCount, 1024)));
            Double Number = Math.Round(RealByteCount / Math.Pow(1024, Place), 1);
            return (Number.ToString() + Suffix[Place]);
        }

    } // End class

} // End namespace