namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm
{
    using System;
    using System.Diagnostics;
    using Udis86;

    /// <summary>
    /// Represents an operand for an <see cref="Instruction"/>
    /// </summary>
    public class Operand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operand" /> class.
        /// </summary>
        /// <param name="operand">Disassembled operand</param>
        internal Operand(ud_operand operand)
        {
            this.UdOperand = operand;
        }

        /// <summary>
        /// Gets the value of the memory displacement portion of the operand (if applicable) converted to Int64. See the Lval* properties for original value.
        /// </summary>
        public Int64 Value
        {
            get
            {
                return Convert.ToInt64(this.RawValue);
            }
        }

        /// <summary>
        /// <para>Gets the operand displacement value as its raw type (e.g. sbyte, byte, short, ushort, Int32, UInt32, long, ulong) depending on the operand type.</para>
        /// <para>If a memory operand, and no base/index registers, the result will be unsigned and contain <see cref="Offset"/> bits, otherwise if there is a base and/or index register the value is signed with <see cref="Offset"/> bits.</para>
        /// <para>If an immediate mode operand the value will be signed and the contain <see cref="Size"/> bits.</para>
        /// <para>Otherwise the result will be unsigned and if <see cref="Offset"/> is > 0 will contain <see cref="Offset"/> bits otherwise <see cref="Size"/> bits.</para>
        /// </summary>
        public Object RawValue
        {
            get
            {
                if (this.Type == ud_type.UD_OP_MEM)
                {
                    // Accessing memory
                    if (this.Base == ud_type.UD_NONE && this.Index == ud_type.UD_NONE)
                    {
                        return this.GetRawValue(this.Offset, false);
                    }
                    else
                    {
                        return this.GetRawValue(this.Offset, true);
                    }
                }
                else if (this.Type == ud_type.UD_OP_IMM)
                {
                    // Immediate Mode (memory is not accessed)
                    return this.GetRawValue(this.Size, true);
                }

                return this.GetRawValue(this.Offset == 0 ? this.Size : this.Offset, false);
            }
        }

        /// <summary>
        /// Gets the operand code
        /// </summary>
        public ud_operand_code Opcode
        {
            get { return this.UdOperand._oprcode; }
        }

        /// <summary>
        /// Gets the operand type (UD_OP_REG, UD_OP_MEM, UD_OP_PTR, UD_OP_IMM, UD_OP_JIMM, UD_OP_CONST)
        /// </summary>
        public ud_type Type
        {
            get { return this.UdOperand.type; }
        }

        /// <summary>
        /// Gets the size of the result of the operand
        /// </summary>
        public UInt16 Size
        {
            get { return this.UdOperand.size; }
        }

        /// <summary>
        /// Gets the base register
        /// </summary>
        public ud_type Base
        {
            get { return this.UdOperand.@base; }
        }

        /// <summary>
        /// Gets the index register
        /// </summary>
        public ud_type Index
        {
            get { return this.UdOperand.index; }
        }

        /// <summary>
        /// Gets the scale applied to index register (2, 4, or 8). 0 == 1 == does nothing
        /// </summary>
        public Byte Scale
        {
            get { return this.UdOperand.scale; }
        }

        /// <summary>
        /// Gets the size of the memory displacement value for UD_OP_MEM operands (e.g. 8-, 16-, 32-, or 64- bits).
        /// This helps determine which "Lval*" value should be read (e.g. if Offset is 8 and operand type is UD_OP_MEM and Base register is not UD_NONE, read LvalSByte)
        /// </summary>
        /// <remarks>
        /// <see cref="RawValue"/> for more detail about the rules governing which value is read.
        /// </remarks>
        public Byte Offset
        {
            get { return this.UdOperand.offset; }
        }

        /// <summary>
        /// Gets the segment component of PTR operand
        /// </summary>
        public UInt16 PtrSegment
        {
            get { return this.UdOperand.lval.ptr_seg; }
        }

        /// <summary>
        /// Gets the offset component of PTR operand
        /// </summary>
        public UInt32 PtrOffset
        {
            get { return this.UdOperand.lval.ptr_off; }
        }

        /// <summary>
        /// Gets the displacement value as <see cref="SByte"/>
        /// </summary>
        public SByte LvalSByte
        {
            get { return (SByte)this.Lval; }
        }

        /// <summary>
        /// Gets the displacement value as <see cref="Byte"/>
        /// </summary>
        public Byte LvalByte
        {
            get { return (Byte)this.Lval; }
        }

        /// <summary>
        /// Gets the displacement value as <see cref="Int16"/>
        /// </summary>
        public Int16 LvalSWord
        {
            get { return (Int16)this.Lval; }
        }

        /// <summary>
        /// Gets the displacement value as <see cref="UInt16"/>
        /// </summary>
        public UInt16 LvalUWord
        {
            get { return (UInt16)this.Lval; }
        }

        /// <summary>
        /// Gets the displacement value as <see cref="Int32"/>
        /// </summary>
        public Int32 LvalSDWord
        {
            get { return (Int32)this.Lval; }
        }

        /// <summary>
        /// Gets the displacement value as <see cref="UInt32"/>
        /// </summary>
        public UInt32 LvalUDWord
        {
            get { return (UInt32)this.Lval; }
        }

        /// <summary>
        /// Gets the displacement value as <see cref="Int64"/>
        /// </summary>
        public Int64 LvalSQWord
        {
            get { return this.Lval; }
        }

        /// <summary>
        /// Gets the displacement value as <see cref="UInt64"/>
        /// </summary>
        public UInt64 LvalUQWord
        {
            get { return (UInt64)this.Lval; }
        }

        /// <summary>
        /// Gets or sets the disassembled instruction operand
        /// </summary>
        internal ud_operand UdOperand { get; set; }

        /// <summary>
        /// Gets the Lval as <see cref="Int64"/>
        /// </summary>
        private Int64 Lval
        {
            get { return this.UdOperand.lval.sqword; }
        }

        /// <summary>
        /// Converts the key components of the operand to a string.
        /// </summary>
        /// <returns>The operand in string format suitable for diagnostics.</returns>
        public override String ToString()
        {
            if (this.Type == ud_type.UD_OP_REG)
            {
                return String.Format("{0,-10}", String.Format("{0},", this.Base));
            }
            else if (this.Type == ud_type.UD_OP_MEM)
            {
                String memSize = String.Empty;

                switch (this.Size)
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

                return String.Format("{0}{4}[{1}{2}{3:x}],", String.Empty, this.Base == ud_type.UD_NONE ? String.Empty : String.Format("{0}+", this.Base), this.Index == ud_type.UD_NONE ? String.Empty : String.Format("({0}*{1})", this.Index, this.Scale == 0 ? 1 : this.Scale), this.PrintDisplacementAddress(), memSize);
            }
            else
            {
                return String.Format("{0}{1}{2}{3:x},", String.Empty, this.Base == ud_type.UD_NONE ? String.Empty : String.Format("{0}+", this.Base), this.Index == ud_type.UD_NONE ? String.Empty : String.Format("({0}*{1})", this.Index, this.Scale == 0 ? 1 : this.Scale), this.RawValue);
            }
        }

        /// <summary>
        /// Returns a string representing the displacement address
        /// </summary>
        /// <returns>The displacement address as a string</returns>
        private String PrintDisplacementAddress()
        {
            if (this.Base == ud_type.UD_NONE && this.Index == ud_type.UD_NONE)
            {
                UInt64 v;
                Debug.Assert(this.Scale == 0 && this.Offset != 8, "TODO: REASON");

                // unsigned mem-offset
                switch (this.Offset)
                {
                    case 16:
                        v = this.LvalUWord;
                        break;
                    case 32:
                        v = this.LvalUDWord;
                        break;
                    case 64:
                        v = this.LvalUQWord;
                        break;
                    default:
                        Debug.Assert(false, "invalid offset");
                        v = 0; // keep cc happy
                        break;
                }

                return String.Format("0x{0:x}", v);
            }
            else
            {
                Int64 v;
                Debug.Assert(this.Offset != 64, "TODO: REASON");

                switch (this.Offset)
                {
                    case 8:
                        v = this.LvalSByte;
                        break;
                    case 16:
                        v = this.LvalSWord;
                        break;
                    case 32:
                        v = this.LvalSDWord;
                        break;
                    default:
                        Debug.Assert(false, "invalid offset");
                        v = 0; // keep cc happy
                        break;
                }

                if (v < 0)
                {
                    return String.Format("-0x{0:x}", -v);
                }
                else if (v > 0)
                {
                    return String.Format("{0}0x{1:x}", this.Index != ud_type.UD_NONE || this.Base != ud_type.UD_NONE ? "+" : String.Empty, v);
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Converts the stored value to its raw value of the appropriate size and sign
        /// </summary>
        /// <param name="size">Data type size</param>
        /// <param name="signed">Should return value be signed</param>
        /// <returns>A converted operand lval of the appropriate size and sign</returns>
        private Object GetRawValue(Int32 size, Boolean signed = true)
        {
            switch (size)
            {
                case 8:
                    return signed ? (Object)this.UdOperand.lval.@sbyte : (Object)this.UdOperand.lval.ubyte;
                case 16:
                    return signed ? (Object)this.UdOperand.lval.sword : (Object)this.UdOperand.lval.uword;
                case 32:
                    return signed ? (Object)this.UdOperand.lval.sdword : (Object)this.UdOperand.lval.udword;
                case 64:
                    return signed ? (Object)this.UdOperand.lval.sqword : (Object)this.UdOperand.lval.uqword;
                default:
                    return (Int64)0;
            }
        }
    }
    //// End class
}
//// End namespace