using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public static class CheckSyntax
    {
        private static unsafe int MaxAddressLength = sizeof(IntPtr) * 2;

        // Determines if a string contains all zeros
        private static bool IsZeroX(string Value)
        {
            for (int Index = 0; Index < Value.Length; Index++)
            {
                // Check each char for a value of 0 if not break & return false
                if (Value.Substring(Index, 1) != "0")
                    break;

                // Check if all characters were 0 and return true if so
                if (Index == Value.Length - 1)
                    return true;
            }

            return false;
        }

        // Determines if a string is empty (easy check, but cleaner looking as a method)
        private static bool IsBlank(string Value)
        {
            if (Value.Length == 0)
                return true;
            return false;
        }

        // Checks if passed value is a valid address
        public static bool Address(string Address)
        {
            if (Address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Address = Address.Substring(2);

            // Out of bounds
            if (Address.Length > MaxAddressLength || IsBlank(Address))
                return false;

            // Too short: assume preceding 0s are intended
            while (Address.Length < MaxAddressLength)
                Address = "0" + Address;

            int Result;
            if (Int32.TryParse(Address, System.Globalization.NumberStyles.HexNumber, null, out Result))
                return true; // Valid
            return false;
        }

        // Checks if passed value is a valid hex value
        public static bool HexValue(string Value, int Bytes)
        {
            if (Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Value = Value.Substring(2);

            // Remove leading 0s and check if out of bounds
            while (Value.Length > Bytes)
            {
                if (Value.Substring(0, 1) == "0")
                    Value = Value.Substring(1);
                else   // Value too large (there were no leading 0s that caused it)
                    return false;
            }

            // Check if string is empty
            if (IsBlank(Value))
                return false;

            // Try to read value from hex string
            int result;
            if (Int32.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out result))
                return true;

            return false; // Invalid
        }

        // Checks if passed value is a valid binary value
        public static bool BinaryValue(string Value)
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

        // Checks if passed value is a valid value for a byte
        public static bool ByteValue(string Value, bool IsHex = false)
        {
            // Remove 0x notation
            if (IsHex)
                if (Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    Value = Value.Substring(2);

            // Check if string is empty
            if (IsBlank(Value))
                return false;

            // Try to read a signed value
            SByte result;
            if (IsHex)
            {
                // Try to read value from hex string
                if (SByte.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out result))
                    return true;
            }
            else
                if (SByte.TryParse(Value, out result))
                return true;

            // Try to read an unsigned value
            Byte _result;
            if (IsHex)
            {
                // Try to read value from hex string
                if (Byte.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out _result))
                    return true;
            }
            else
                if (Byte.TryParse(Value, out _result))
                return true;


            return false; // Invalid
        }

        // Checks if passed value is a valid value for a short
        public static bool Int16Value(string Value, bool IsHex = false)
        {
            if (IsHex)
                if (Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    Value = Value.Substring(2);

            if (IsBlank(Value))
                return false;

            // Try to read a signed value
            Int16 result;
            if (IsHex)
            {
                if (Int16.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out result))
                    return true;
            }
            else
                if (Int16.TryParse(Value, out result))
                return true;

            // Try to read unsigned value
            UInt16 _result;
            if (IsHex)
            {
                if (UInt16.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out _result))
                    return true;
            }
            else
                if (UInt16.TryParse(Value, out _result))
                return true;

            return false; // Invalid
        }

        // Checks if passed value is a valid value for a int
        // TODO: There is likely repetition between this and other methods, can probably condense these
        public static bool Int32Value(string Value, bool IsHex = false)
        {
            if (IsHex)
                if (Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    Value = Value.Substring(2);

            if (IsBlank(Value))
                return false;

            // Try to read signed value
            Int32 result;
            if (IsHex)
            {
                if (Int32.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out result))
                    return true;
            }
            else
                if (Int32.TryParse(Value, out result))
                return true;

            // Try to read unsigned value
            UInt32 _result;
            if (IsHex)
            {
                if (UInt32.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out _result))
                    return true;
            }
            else
                if (UInt32.TryParse(Value, out _result))
                return true;

            return false; // Invalid
        }

        // Checks if passed value is a valid value for a long
        public static bool Int64Value(string Value, bool IsHex = false)
        {
            if (IsHex)
                if (Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    Value = Value.Substring(2);

            if (IsBlank(Value))
                return false;

            // Try to read signed value
            Int64 result;
            if (IsHex)
            {
                if (Int64.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out result))
                    return true;
            }
            else
                if (Int64.TryParse(Value, out result))
                return true;

            // Try to read unsigned value
            UInt64 _result;
            if (IsHex)
            {
                if (UInt64.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out _result))
                    return true;
            }
            else
                if (UInt64.TryParse(Value, out _result))
                return true;

            return false; // Invalid
        }

        // Checks if passed value is a valid value for a single
        public static bool SingleValue(string Value, bool IsHex = false)
        {
            if (IsHex)
                if (Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    Value = Value.Substring(2);

            if (IsBlank(Value))
                return false;

            Single resultS = 0;
            Int32 result32 = 0;

            if (IsHex)
            {
                // Grab int32 value (int32 has same bytes as a single), and since this
                // is checking to see if there is any data, we don't care how those bytes are read
                if (Int32.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out result32))
                    return true;
            }
            else
                if (Single.TryParse(Value, out resultS))
                return true;

            return false; //Invalid
        }

        // Checks if passed value is a valid value for a double
        public static bool DoubleValue(string Value, bool IsHex = false)
        {
            if (IsHex)
                if (Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    Value = Value.Substring(2);

            if (IsBlank(Value))
                return false;

            // Try to read value from hex string
            Double ResultDouble = 0;
            Int64 ResultInt64 = 0;
            if (IsHex)
            {
                // Grab int64 value (int64 has same bytes as a double), and since this
                // is checking to see if there is any data, we don't care how those bytes are interpreted
                if (Int64.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out ResultInt64))
                    return true;
            }
            else
                if (Double.TryParse(Value, out ResultDouble))
                return true;

            return false;
        }

    } // End class

} // End namespace
