using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class Conversions
    {

        public static string ToAddress(string Value)
        {
            if (CheckSyntax.Int32Value(Value))
                return String.Format("{0:X8}", Convert.ToUInt32(Value));
            else if (CheckSyntax.Int64Value(Value))
                return String.Format("{0:X16}", Convert.ToUInt64(Value));
            else
                return "??";
        }

        public static String ByteToMetricSize(UInt64 byteCount)
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
