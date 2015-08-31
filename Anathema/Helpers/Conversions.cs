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
