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

        // Determines if a string contains all zeros
        private static Boolean IsZeros(String Value)
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
            Boolean CanParse = false;

            var @switch = new Dictionary<Type, Action> {
                    { typeof(Byte), () => CanParse = ByteValue(Value) },
                    { typeof(SByte), () => CanParse = ByteValue(Value) },
                    { typeof(Int16), () => CanParse = Int16Value(Value) },
                    { typeof(Int32), () => CanParse = Int32Value(Value) },
                    { typeof(Int64), () => CanParse = Int64Value(Value) },
                    { typeof(UInt16), () => CanParse = Int16Value(Value) },
                    { typeof(UInt32), () => CanParse = Int32Value(Value) },
                    { typeof(UInt64), () => CanParse = Int64Value(Value) },
                    { typeof(Single), () => CanParse = SingleValue(Value) },
                    { typeof(Double), () => CanParse = DoubleValue(Value) },
                };

            if (@switch.ContainsKey(ValueType))
                @switch[ValueType]();

            return CanParse;
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

        // Checks if passed value is a valid value for a byte
        public static Boolean ByteValue(String Value, Boolean IsHex = false)
        {
            if (IsHex && Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Value = Value.Substring(2);

            // Check if string is empty
            if (Value == String.Empty)
                return false;

            // Try to read a signed value
            SByte SignedResult;
            if (IsHex)
            {
                // Try to read value from hex string
                if (SByte.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out SignedResult))
                    return true;
            }
            else
                if (SByte.TryParse(Value, out SignedResult))
                return true;

            // Try to read an unsigned value
            Byte UnsignedResult;
            if (IsHex)
            {
                // Try to read value from hex string
                if (Byte.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out UnsignedResult))
                    return true;
            }
            else
                if (Byte.TryParse(Value, out UnsignedResult))
                return true;


            return false; // Invalid
        }

        // Checks if passed value is a valid value for a short
        public static Boolean Int16Value(String Value, Boolean IsHex = false)
        {
            if (IsHex && Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Value = Value.Substring(2);

            if (Value == String.Empty)
                return false;

            // Try to read a signed value
            Int16 SignedResult;
            if (IsHex)
            {
                if (Int16.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out SignedResult))
                    return true;
            }
            else
                if (Int16.TryParse(Value, out SignedResult))
                return true;

            // Try to read unsigned value
            UInt16 UnsignedResult;
            if (IsHex)
            {
                if (UInt16.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out UnsignedResult))
                    return true;
            }
            else
                if (UInt16.TryParse(Value, out UnsignedResult))
                return true;

            return false; // Invalid
        }

        // Checks if passed value is a valid value for a int
        public static Boolean Int32Value(String Value, Boolean IsHex = false)
        {
            if (IsHex && Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Value = Value.Substring(2);

            if (Value == String.Empty)
                return false;

            // Try to read signed value
            Int32 SignedResult;
            if (IsHex)
            {
                if (Int32.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out SignedResult))
                    return true;
            }
            else
                if (Int32.TryParse(Value, out SignedResult))
                return true;

            // Try to read unsigned value
            UInt32 UnsignedResult;
            if (IsHex)
            {
                if (UInt32.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out UnsignedResult))
                    return true;
            }
            else
                if (UInt32.TryParse(Value, out UnsignedResult))
                return true;

            return false; // Invalid
        }

        // Checks if passed value is a valid value for a long
        public static Boolean Int64Value(String Value, Boolean IsHex = false)
        {
            if (IsHex && Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Value = Value.Substring(2);

            if (Value == String.Empty)
                return false;

            // Try to read signed value
            Int64 SignedResult;
            if (IsHex)
            {
                if (Int64.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out SignedResult))
                    return true;
            }
            else
                if (Int64.TryParse(Value, out SignedResult))
                return true;

            // Try to read unsigned value
            UInt64 UnsignedResult;
            if (IsHex)
            {
                if (UInt64.TryParse(Value, System.Globalization.NumberStyles.HexNumber, null, out UnsignedResult))
                    return true;
            }
            else
                if (UInt64.TryParse(Value, out UnsignedResult))
                return true;

            return false; // Invalid
        }

        // Checks if passed value is a valid value for a single
        public static Boolean SingleValue(String Value, Boolean IsHex = false)
        {
            if (IsHex && Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Value = Value.Substring(2);

            if (Value == String.Empty)
                return false;

            Single ResultSingle = 0;

            if (IsHex)
            {
                // If single is in hex, validify as Int32 since they use the same number of bytes
                return Int32Value(Value, IsHex);
            }
            else
            {
                if (Single.TryParse(Value, out ResultSingle))
                    return true;
            }

            return false; //Invalid
        }

        // Checks if passed value is a valid value for a double
        public static Boolean DoubleValue(String Value, Boolean IsHex = false)
        {
            if (IsHex && Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                Value = Value.Substring(2);

            if (Value == String.Empty)
                return false;

            // Try to read value from hex string
            Double ResultDouble = 0;

            if (IsHex)
            {
                // If single is in hex, validify as Int64 since they use the same number of bytes
                return Int64Value(Value, IsHex);
            }
            else
            {
                if (Double.TryParse(Value, out ResultDouble))
                    return true;
            }

            return false;
        }

    } // End class

} // End namespace