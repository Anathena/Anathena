namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Translators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Udis86;

    /// <summary>
    /// Translates to ATT syntax
    /// </summary>
    public class ATTTranslator : Translator
    {
        /// <summary>
        /// Translate a list of instructions separated by <see cref="Environment.NewLine"/>.
        /// </summary>
        /// <param name="insns"></param>
        /// <returns></returns>
        public override String Translate(IEnumerable<Instruction> insns)
        {
            Boolean first = true;
            this.Content = new StringBuilder();

            foreach (Instruction insn in insns)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Content.Append(Environment.NewLine);
                }

                if (this.IncludeAddress)
                {
                    this.WriteAddress(insn);
                }

                if (this.IncludeBinary)
                {
                    this.WriteBinary(insn);
                }

                this.Ud_translate_att(insn);
            }

            return Content.ToString();
        }

        /// <summary>
        /// Translate a single instruction
        /// </summary>
        /// <param name="insn"></param>
        /// <returns></returns>
        public override String Translate(Instruction insn)
        {
            this.Content = new StringBuilder();

            if (this.IncludeAddress)
            {
                this.WriteAddress(insn);
            }

            if (this.IncludeBinary)
            {
                this.WriteBinary(insn);
            }

            this.Ud_translate_att(insn);

            return Content.ToString();
        }

        /// <summary>
        /// Prints an operand cast.
        /// </summary>
        /// <param name="insn"></param>
        /// <param name="op"></param>
        private void Opr_cast(Instruction insn, Operand op)
        {
            switch (op.Size)
            {
                case 16:
                case 32:
                    Content.Append("*");
                    break;
                default: break;
            }
        }

        /// <summary>
        /// Generates assembly output for each operand
        /// </summary>
        /// <param name="u"></param>
        /// <param name="op"></param>
        private void Gen_operand(Instruction u, Operand op)
        {
            switch (op.Type)
            {
                case UdType.UD_OP_CONST:
                    this.Content.AppendFormat("$0x{0:x4}", op.LvalUDWord);
                    break;
                case UdType.UD_OP_REG:
                    this.Content.AppendFormat("%{0}", this.RegisterForType(op.Base));
                    break;
                case UdType.UD_OP_MEM:
                    if (u.BrFar != 0)
                    {
                        this.Opr_cast(u, op);
                    }

                    if (u.PfxSeg != 0)
                    {
                        this.Content.AppendFormat("%{0}:", this.RegisterForType((UdType)u.PfxSeg));
                    }

                    if (op.Offset != 0)
                    {
                        this.Ud_syn_print_mem_disp(u, op, 0);
                    }

                    if (op.Base != UdType.UD_NONE)
                    {
                        this.Content.AppendFormat("(%{0}", this.RegisterForType(op.Base));
                    }

                    if (op.Index != UdType.UD_NONE)
                    {
                        if (op.Base != UdType.UD_NONE)
                        {
                            this.Content.AppendFormat(",");
                        }
                        else
                        {
                            this.Content.AppendFormat("(");
                        }

                        this.Content.AppendFormat("%{0}", this.RegisterForType(op.Index));
                    }

                    if (op.Scale != 0)
                    {
                        this.Content.AppendFormat(",{0}", op.Scale);
                    }

                    if (op.Base != UdType.UD_NONE || op.Index != UdType.UD_NONE)
                    {
                        this.Content.AppendFormat(")");
                    }

                    break;
                case UdType.UD_OP_IMM:
                    this.Content.AppendFormat("$");
                    this.Ud_syn_print_imm(u, op);
                    break;
                case UdType.UD_OP_JIMM:
                    this.Ud_syn_print_addr(u, (Int64)this.Ud_syn_rel_target(u, op));
                    break;
                case UdType.UD_OP_PTR:
                    switch (op.Size)
                    {
                        case 32:
                            this.Content.AppendFormat("$0x{0:x}, $0x{1:x}", op.PtrSegment, op.PtrOffset & 0xFFFF);
                            break;
                        case 48:
                            this.Content.AppendFormat("$0x{0:x}, $0x{1:x}", op.PtrSegment, op.PtrOffset);
                            break;
                    }

                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Translates to ATT syntax 
        /// </summary>
        /// <param name="u"></param>
        private void Ud_translate_att(Instruction u)
        {
            Int32 size = 0;
            Boolean star = false;

            // check if P_OSO prefix is used
            if (BitOps.P_OSO(u.ItabEntry.Prefix) == 0 && u.PfxOpr != 0)
            {
                switch (u.DisMode)
                {
                    case ArchitectureMode.x86_16:
                        Content.AppendFormat("o32 ");
                        break;
                    case ArchitectureMode.x86_32:
                    case ArchitectureMode.x86_64:
                        Content.AppendFormat("o16 ");
                        break;
                }
            }

            // check if P_ASO prefix was used
            if (BitOps.P_ASO(u.ItabEntry.Prefix) == 0 && u.PfxAdr != 0)
            {
                switch (u.DisMode)
                {
                    case ArchitectureMode.x86_16:
                        Content.AppendFormat("a32 ");
                        break;
                    case ArchitectureMode.x86_32:
                        Content.AppendFormat("a16 ");
                        break;
                    case ArchitectureMode.x86_64:
                        Content.AppendFormat("a32 ");
                        break;
                }
            }

            if (u.PfxLock != 0)
            {
                Content.AppendFormat("lock ");
            }

            if (u.PfxRep != 0)
            {
                Content.AppendFormat("rep ");
            }
            else if (u.PfxRepe != 0)
            {
                Content.AppendFormat("repe ");
            }
            else if (u.PfxRepne != 0)
            {
                Content.AppendFormat("repne ");
            }

            // special instructions
            switch (u.Mnemonic)
            {
                case UdMnemonicCode.UD_Iretf:
                    Content.AppendFormat("lret ");
                    size = -1;
                    break;
                case UdMnemonicCode.UD_Idb:
                    Content.AppendFormat(".byte 0x{0:x2}", u.Operands[0].LvalByte);
                    return;
                case UdMnemonicCode.UD_Ijmp:
                case UdMnemonicCode.UD_Icall:
                    if (u.BrFar != 0)
                    {
                        Content.AppendFormat("l");
                        size = -1;
                    }

                    if (u.Operands[0].Type == UdType.UD_OP_REG)
                    {
                        star = true;
                    }

                    this.Content.AppendFormat("{0}", Udis86.UdLookupMnemonic(u.Mnemonic));
                    break;
                case UdMnemonicCode.UD_Ibound:
                case UdMnemonicCode.UD_Ienter:
                    if (u.Operands.Length > 0 && u.Operands[0].Type != UdType.UD_NONE)
                    {
                        this.Gen_operand(u, u.Operands[0]);
                    }

                    if (u.Operands.Length > 1 && u.Operands[1].Type != UdType.UD_NONE)
                    {
                        this.Content.AppendFormat(",");
                        this.Gen_operand(u, u.Operands[1]);
                    }

                    return;
                default:
                    this.Content.AppendFormat("{0}", Udis86.UdLookupMnemonic(u.Mnemonic));
                    break;
            }

            if (size != -1 && u.Operands.Length > 0 && u.Operands.Any(o => o.Type == UdType.UD_OP_MEM))
            {
                size = u.Operands[0].Size;
            }

            if (size == 8)
            {
                Content.AppendFormat("b");
            }
            else if (size == 16)
            {
                Content.AppendFormat("w");
            }
            else if (size == 32)
            {
                Content.AppendFormat("l");
            }
            else if (size == 64)
            {
                Content.AppendFormat("q");
            }
            else if (size == 80)
            {
                Content.AppendFormat("t");
            }

            if (star)
            {
                Content.AppendFormat(" *");
            }
            else
            {
                Content.AppendFormat(" ");
            }

            if (u.Operands.Length > 3 && u.Operands[3].Type != UdType.UD_NONE)
            {
                this.Gen_operand(u, u.Operands[3]);
                Content.AppendFormat(", ");
            }

            if (u.Operands.Length > 2 && u.Operands[2].Type != UdType.UD_NONE)
            {
                this.Gen_operand(u, u.Operands[2]);
                Content.AppendFormat(", ");
            }

            if (u.Operands.Length > 1 && u.Operands[1].Type != UdType.UD_NONE)
            {
                this.Gen_operand(u, u.Operands[1]);
                this.Content.AppendFormat(", ");
            }

            if (u.Operands.Length > 0 && u.Operands[0].Type != UdType.UD_NONE)
            {
                this.Gen_operand(u, u.Operands[0]);
            }
        }
    }
    //// End class
}
//// End namespace