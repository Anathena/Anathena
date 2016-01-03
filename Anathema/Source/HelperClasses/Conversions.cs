using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Converts a value from one format to another format. No validation checking is done.
    /// </summary>
    class Conversions
    {
        public static Type StringToPrimitiveType(String Value)
        {
            return PrimitiveTypes.GetPrimitiveTypes().Where(x => x.Name == Value).First();
        }

        public static dynamic ParseValue(Type ValueType, String Value)
        {
            dynamic ParsedValue = null;

            var @switch = new Dictionary<Type, Action> {
                    { typeof(Byte), () => ParsedValue = Byte.Parse(Value) },
                    { typeof(SByte), () => ParsedValue = SByte.Parse(Value) },
                    { typeof(Int16), () => ParsedValue = Int16.Parse(Value) },
                    { typeof(Int32), () => ParsedValue = Int32.Parse(Value) },
                    { typeof(Int64), () => ParsedValue = Int64.Parse(Value) },
                    { typeof(UInt16), () => ParsedValue = UInt16.Parse(Value) },
                    { typeof(UInt32), () => ParsedValue = UInt32.Parse(Value) },
                    { typeof(UInt64), () => ParsedValue = UInt64.Parse(Value) },
                    { typeof(Single), () => ParsedValue = Single.Parse(Value) },
                    { typeof(Double), () => ParsedValue = Double.Parse(Value) },
                };

            if (@switch.ContainsKey(ValueType))
                @switch[ValueType]();

            return ParsedValue;
        }

        public static String ToAddress(String Value)
        {
            if (CheckSyntax.Int32Value(Value))
                return String.Format("{0:X8}", Convert.ToUInt32(Value));
            else if (CheckSyntax.Int64Value(Value))
                return String.Format("{0:X16}", Convert.ToUInt64(Value));
            else
                return "!!";
        }

        public static UInt64 AddressToValue(String Address)
        {
            return UInt64.Parse(Address, System.Globalization.NumberStyles.HexNumber);
        }
        public static Int32 HexToInt(String Address)
        {
            return Int32.Parse(Address, System.Globalization.NumberStyles.HexNumber);
        }

        public static String BytesToMetric(UInt64 byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; // Longs run out around EB

            if (byteCount == 0)
                return "0" + suf[0];

            int place = Convert.ToInt32(Math.Floor(Math.Log(byteCount, 1024)));

            double num = Math.Round(byteCount / Math.Pow(1024, place), 1);

            return (num.ToString() + suf[place]);
        }

        public static UIntPtr IntPtrToUIntPtr(IntPtr Value)
        {
            if (IntPtr.Size == 4)
            {
                return unchecked((UIntPtr)(uint)(int)Value);
            }

            return unchecked((UIntPtr)(ulong)(long)Value);
        }

        public static IntPtr UIntPtrToIntPtr(UIntPtr Value)
        {
            if (IntPtr.Size == 4)
            {
                return unchecked((IntPtr)(int)(uint)Value);
            }

            return unchecked((IntPtr)(long)(ulong)Value);
        }
    }
}
