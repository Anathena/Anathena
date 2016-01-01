// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\operand.cs)
// Copyright (c) 2014-2015 Justin Stenning
// http://spazzarama.com
// https://github.com/spazzarama/SharpDisasm
// https://sharpdisasm.codeplex.com/
//
// SharpDisasm is distributed under the 2-clause "Simplified BSD License".
//
// Portions of SharpDisasm are ported to C# from udis86 a C disassembler project
// also distributed under the terms of the 2-clause "Simplified BSD License" and
// Copyright (c) 2002-2012, Vivek Thampi <vivek.mt@gmail.com>
// All rights reserved.
// UDIS86: https://github.com/vmt/udis86
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
// 
// 1. Redistributions of source code must retain the above copyright notice, 
//    this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice, 
//    this list of conditions and the following disclaimer in the documentation 
//    and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
// ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// --------------------------------------------------------------------------------

using SharpDisasm.Udis86;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace SharpDisasm
{
    /// <summary>
    /// Represents an operand for an <see cref="Instruction"/>
    /// </summary>
    public class Operand
    {
        internal ud_operand UdOperand;
        internal Operand(ud_operand operand)
        {
            UdOperand = operand;
        }

        /// <summary>
        /// The value of the memory displacement portion of the operand (if applicable) converted to Int64. See the Lval* properties for original value.
        /// </summary>
        public long Value
        {
            get
            {
                return Convert.ToInt64(RawValue);
            }
        }

        /// <summary>
        /// <para>Returns the operand displacement value as its raw type (e.g. sbyte, byte, short, ushort, Int32, UInt32, long, ulong) depending on the operand type.</para>
        /// <para>If a memory operand, and no base/index registers, the result will be unsigned and contain <see cref="Offset"/> bits, otherwise if there is a base and/or index register the value is signed with <see cref="Offset"/> bits.</para>
        /// <para>If an immediate mode operand the value will be signed and the contain <see cref="Size"/> bits.</para>
        /// <para>Otherwise the result will be unsigned and if <see cref="Offset"/> is > 0 will contain <see cref="Offset"/> bits otherwise <see cref="Size"/> bits.</para>
        /// </summary>
        public object RawValue
        {
            get
            {
                if (Type == ud_type.UD_OP_MEM) // Accessing memory
                {
                    if (Base == ud_type.UD_NONE && Index == ud_type.UD_NONE)
                    {
                        return GetRawValue(Offset, false);
                    }
                    else
                    {
                        return GetRawValue(Offset, true);
                    }
                }
                else if (Type == ud_type.UD_OP_IMM)  // Immediate Mode (memory is not accessed)
                {
                    return GetRawValue(Size, true);
                }

                return GetRawValue((Offset == 0 ? Size : Offset), false);
            }
        }

        private object GetRawValue(int size, bool signed = true)
        {
            switch (size)
            {
                case 8:
                    return (signed ? (object)UdOperand.lval.@sbyte : (object)UdOperand.lval.ubyte);
                case 16:
                    return (signed ? (object)UdOperand.lval.sword : (object)UdOperand.lval.uword);
                case 32:
                    return (signed ? (object)UdOperand.lval.sdword : (object)UdOperand.lval.udword);
                case 64:
                    return (signed ? (object)UdOperand.lval.sqword : (object)UdOperand.lval.uqword);
                default:
                    return (long)0;
            }
        }

        /// <summary>
        /// The operand code
        /// </summary>
        public ud_operand_code Opcode { get { return UdOperand._oprcode; } }

        /// <summary>
        /// The operand type (UD_OP_REG, UD_OP_MEM, UD_OP_PTR, UD_OP_IMM, UD_OP_JIMM, UD_OP_CONST)
        /// </summary>
        public ud_type Type { get { return UdOperand.type; } }
        
        /// <summary>
        /// Size of the result of the operand
        /// </summary>
        public ushort Size { get { return UdOperand.size; } }
        
        /// <summary>
        /// Base register
        /// </summary>
        public ud_type Base { get { return UdOperand.@base; } }
        
        /// <summary>
        /// Index register
        /// </summary>
        public ud_type Index { get { return UdOperand.index; } }
        
        /// <summary>
        /// Scale applied to index register (2, 4, or 8). 0 == 1 == does nothing
        /// </summary>
        public byte Scale { get { return UdOperand.scale; } }
        
        /// <summary>
        /// For UD_OP_MEM operands, this represents the size of the memory displacement value (e.g. 8-, 16-, 32-, or 64- bits).
        /// This helps determine which "Lval*" value should be read (e.g. if Offset is 8 and operand type is UD_OP_MEM and Base register is not UD_NONE, read LvalSByte)
        /// </summary>
        /// <remarks>
        /// <see cref="RawValue"/> for more detail about the rules governing which value is read.
        /// </remarks>
        public byte Offset { get { return UdOperand.offset; } }
        
        /// <summary>
        /// Segment component of PTR operand
        /// </summary>
        public ushort PtrSegment
        {
            get { return UdOperand.lval.ptr_seg; }
        }

        /// <summary>
        /// Offset component of PTR operand
        /// </summary>
        public uint PtrOffset
        {
            get { return UdOperand.lval.ptr_off; }
        }

        #region Lval - displacement value as various sizes

        private long Lval { get { return UdOperand.lval.sqword; } }

        /// <summary>
        /// The displacement value as <see cref="sbyte"/>
        /// </summary>
        public sbyte LvalSByte
        {
            get { return (sbyte)Lval; }
        }

        /// <summary>
        /// The displacement value as <see cref="byte"/>
        /// </summary>
        public byte LvalByte
        {
            get { return (byte)Lval; }
        }

        /// <summary>
        /// The displacement value as <see cref="short"/>
        /// </summary>
        public short LvalSWord
        {
            get { return (short)Lval; }
        }

        /// <summary>
        /// The displacement value as <see cref="ushort"/>
        /// </summary>
        public ushort LvalUWord
        {
            get { return (ushort)Lval; }
        }

        /// <summary>
        /// The displacement value as <see cref="Int32"/>
        /// </summary>
        public Int32 LvalSDWord
        {
            get { return (int)Lval; }
        }

        /// <summary>
        /// The displacement value as <see cref="UInt32"/>
        /// </summary>
        public UInt32 LvalUDWord
        {
            get { return (uint)Lval; }
        }

        /// <summary>
        /// The displacement value as <see cref="long"/>
        /// </summary>
        public long LvalSQWord
        {
            get { return Lval; }
        }

        /// <summary>
        /// The displacement value as <see cref="ulong"/>
        /// </summary>
        public ulong LvalUQWord
        {
            get { return (ulong)Lval; }
        }

        #endregion

        /// <summary>
        /// Converts the key components of the operand to a string.
        /// </summary>
        /// <returns>The operand in string format suitable for diagnostics.</returns>
        public override string ToString()
        {
            if (Type == ud_type.UD_OP_REG)
            {
                return String.Format("{0,-10}", String.Format("{0},", Base));
            }
            else if (Type == ud_type.UD_OP_MEM)
            {
                string memSize = "";
                switch (Size)
                {
                    case 8:
                        memSize = "BYTE ";
                        break;
                    case 16:
                        memSize = "WORD ";
                        break;
                    case 32:
                        memSize = "DWORD ";
                        break;
                    case 64:
                        memSize = "QWORD ";
                        break;
                }

                return String.Format("{0}{4}[{1}{2}{3:x}],", "", (Base == ud_type.UD_NONE ? "" : String.Format("{0}+", Base)), (Index == ud_type.UD_NONE ? "" : String.Format("({0}*{1})", Index, (Scale == 0 ? 1 : Scale))), PrintDisplacementAddress(), memSize);
            }
            else
                return String.Format("{0}{1}{2}{3:x},", "", (Base == ud_type.UD_NONE ? "" : String.Format("{0}+", Base)), (Index == ud_type.UD_NONE ? "" : String.Format("({0}*{1})", Index, (Scale == 0 ? 1 : Scale))), RawValue);
        }

        private string PrintDisplacementAddress()
        {
            if (Base == ud_type.UD_NONE && Index == ud_type.UD_NONE)
            {
                ulong v;
                Debug.Assert(Scale == 0 && Offset != 8);
                /* unsigned mem-offset */
                switch (Offset)
                {
                    case 16: v = LvalUWord; break;
                    case 32: v = LvalUDWord; break;
                    case 64: v = LvalUQWord; break;
                    default: Debug.Assert(false, "invalid offset"); v = 0; /* keep cc happy */
                        break;
                }
                return String.Format("0x{0:x}", v);
            }
            else
            {
                long v;
                Debug.Assert(Offset != 64);
                switch (Offset)
                {
                    case 8: v = LvalSByte; break;
                    case 16: v = LvalSWord; break;
                    case 32: v = LvalSDWord; break;
                    default: Debug.Assert(false, "invalid offset"); v = 0; /* keep cc happy */
                        break;
                }
                if (v < 0)
                {
                    return String.Format("-0x{0:x}", -v);
                }
                else if (v > 0)
                {
                    return String.Format("{0}0x{1:x}", (Index != ud_type.UD_NONE || Base != ud_type.UD_NONE ? "+" : ""), v);
                }
            }
            return "";
        }
    }
}
