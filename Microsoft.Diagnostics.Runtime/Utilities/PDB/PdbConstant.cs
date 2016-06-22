// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    /// <summary>
    /// This class represents a constant value in source code, such as:
    ///     const int Foo = 3;
    /// </summary>
    public class PdbConstant
    {
        /// <summary>
        /// The variable name of the constant.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The metadata token of this constant.
        /// </summary>
        public uint Token { get; private set; }

        /// <summary>
        /// The value of this constant.
        /// </summary>
        public object Value { get; private set; }

        internal PdbConstant(BitAccess bits)
        {
            uint token;
            bits.ReadUInt32(out token);
            this.Token = token;
            byte tag1;
            bits.ReadUInt8(out tag1);
            byte tag2;
            bits.ReadUInt8(out tag2);
            if (tag2 == 0)
            {
                this.Value = tag1;
            }
            else if (tag2 == 0x80)
            {
                switch (tag1)
                {
                    case 0x00: //sbyte
                        sbyte sb;
                        bits.ReadInt8(out sb);
                        this.Value = sb;
                        break;
                    case 0x01: //short
                        short s;
                        bits.ReadInt16(out s);
                        this.Value = s;
                        break;
                    case 0x02: //ushort
                        ushort us;
                        bits.ReadUInt16(out us);
                        this.Value = us;
                        break;
                    case 0x03: //int
                        int i;
                        bits.ReadInt32(out i);
                        this.Value = i;
                        break;
                    case 0x04: //uint
                        uint ui;
                        bits.ReadUInt32(out ui);
                        this.Value = ui;
                        break;
                    case 0x05: //float
                        this.Value = bits.ReadFloat();
                        break;
                    case 0x06: //double
                        this.Value = bits.ReadDouble();
                        break;
                    case 0x09: //long
                        long sl;
                        bits.ReadInt64(out sl);
                        this.Value = sl;
                        break;
                    case 0x0a: //ulong
                        ulong ul;
                        bits.ReadUInt64(out ul);
                        this.Value = ul;
                        break;
                    case 0x10: //string
                        string str;
                        bits.ReadBString(out str);
                        this.Value = str;
                        break;
                    case 0x19: //decimal
                        this.Value = bits.ReadDecimal();
                        break;
                    default:
                        //TODO: error
                        break;
                }
            }
            else
            {
                //TODO: error
            }

            string name;
            bits.ReadCString(out name);
            Name = name;
        }
    }
}
