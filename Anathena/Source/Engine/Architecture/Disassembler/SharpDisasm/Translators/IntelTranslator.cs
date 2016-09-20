namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Translators
{
    using System;
    using System.Collections.Generic;
    using Udis86;

    /// <summary>
    /// Translates instructions to Intel ASM syntax
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public class IntelTranslator : Translator
    {
        /// <summary>
        /// Translate a list of instructions separated by <see cref="Environment.NewLine"/>.
        /// </summary>
        /// <param name="insns">The instructions to translate</param>
        /// <returns>Each instruction as Intel ASM syntax separated by <see cref="Environment.NewLine"/></returns>
        public override String Translate(IEnumerable<Instruction> insns)
        {
            Boolean first = true;
            this.Content.Length = 0;

            foreach (Instruction insn in insns)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    this.Content.Append(Environment.NewLine);
                }

                if (this.IncludeAddress)
                {
                    this.WriteAddress(insn);
                }

                if (this.IncludeBinary)
                {
                    this.WriteBinary(insn);
                }

                this.Ud_translate_intel(insn);
            }

            String result = Content.ToString();
            Content.Length = 0;

            return result;
        }

        /// <summary>
        /// Translate a single instruction
        /// </summary>
        /// <param name="insn"></param>
        /// <returns></returns>
        public override String Translate(Instruction insn)
        {
            Content.Length = 0;
            if (this.IncludeAddress)
            {
                this.WriteAddress(insn);
            }

            if (this.IncludeBinary)
            {
                this.WriteBinary(insn);
            }

            this.Ud_translate_intel(insn);

            String result = Content.ToString();
            Content.Length = 0;

            return result;
        }

        /// <summary>
        /// Prints an operand cast.
        /// </summary>
        /// <param name="insn"></param>
        /// <param name="op"></param>
        private void Opr_cast(Instruction insn, Operand op)
        {
            if (insn.BrFar > 0)
            {
                Content.AppendFormat("far ");
            }

            switch (op.Size)
            {
                case 8:
                    Content.AppendFormat("byte ");
                    break;
                case 16:
                    Content.AppendFormat("word ");
                    break;
                case 32:
                    Content.AppendFormat("dword ");
                    break;
                case 64:
                    Content.AppendFormat("qword ");
                    break;
                case 80:
                    Content.AppendFormat("tword ");
                    break;
                default: break;
            }
        }

        /// <summary>
        /// Generates assembly output for each operand.
        /// </summary>
        /// <param name="insn"></param>
        /// <param name="op"></param>
        /// <param name="syn_cast"></param>
        private void Gen_operand(Instruction insn, Operand op, Int32 syn_cast)
        {
            switch (op.Type)
            {
                case UdType.UD_OP_REG:
                    this.Content.AppendFormat("{0}", this.RegisterForType(op.Base));
                    break;

                case UdType.UD_OP_MEM:
                    if (syn_cast > 0)
                    {
                        this.Opr_cast(insn, op);
                    }

                    this.Content.AppendFormat("[");
                    if (insn.PfxSeg > 0)
                    {
                        this.Content.AppendFormat("{0}:", this.RegisterForType((UdType)insn.PfxSeg));
                    }

                    if (op.Base > 0)
                    {
                        this.Content.AppendFormat("{0}", this.RegisterForType(op.Base));
                    }

                    if (op.Index > 0)
                    {
                        this.Content.AppendFormat("{0}{1}", op.Base != UdType.UD_NONE ? "+" : String.Empty, this.RegisterForType(op.Index));
                        if (op.Scale > 0)
                        {
                            this.Content.AppendFormat("*{0}", op.Scale);
                        }
                    }

                    if (op.Offset != 0)
                    {
                        this.Ud_syn_print_mem_disp(insn, op, (op.Base != UdType.UD_NONE || op.Index != UdType.UD_NONE) ? 1 : 0);
                    }

                    this.Content.AppendFormat("]");
                    break;
                case UdType.UD_OP_IMM:
                    this.Ud_syn_print_imm(insn, op);
                    break;
                case UdType.UD_OP_JIMM:
                    this.Ud_syn_print_addr(insn, (long)Ud_syn_rel_target(insn, op));
                    break;
                case UdType.UD_OP_PTR:
                    switch (op.Size)
                    {
                        case 32:
                            this.Content.AppendFormat("word 0x{0:x}:0x{1:x}", op.PtrSegment, op.PtrOffset & 0xFFFF);
                            break;
                        case 48:
                            this.Content.AppendFormat("dword 0x{0:x}:0x{1:x}", op.PtrSegment, op.PtrOffset);
                            break;
                    }

                    break;
                case UdType.UD_OP_CONST:
                    if (syn_cast > 0)
                    {
                        this.Opr_cast(insn, op);
                    }

                    this.Content.AppendFormat("{0}", op.LvalUDWord);
                    break;

                default: return;
            }
        }

        /// <summary>
        /// translates to intel syntax
        /// </summary>
        /// <param name="insn"></param>
        private void Ud_translate_intel(Instruction insn)
        {
            /* check if P_OSO prefix is used */
            if (BitOps.P_OSO(insn.ItabEntry.Prefix) == 0 && insn.PfxOpr > 0)
            {
                switch (insn.DisMode)
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
            if (BitOps.P_ASO(insn.ItabEntry.Prefix) == 0 && insn.PfxAdr > 0)
            {
                switch (insn.DisMode)
                {
                    case ArchitectureMode.x86_16:
                        this.Content.AppendFormat("a32 ");
                        break;
                    case ArchitectureMode.x86_32:
                        this.Content.AppendFormat("a16 ");
                        break;
                    case ArchitectureMode.x86_64:
                        this.Content.AppendFormat("a32 ");
                        break;
                }
            }

            if (insn.PfxSeg > 0 &&
                insn.Operands[0].Type != UdType.UD_OP_MEM &&
                insn.Operands[1].Type != UdType.UD_OP_MEM)
            {
                this.Content.AppendFormat("{0} ", this.RegisterForType((UdType)insn.PfxSeg));
            }

            if (insn.PfxLock > 0)
            {
                this.Content.AppendFormat("lock ");
            }

            if (insn.PfxRep > 0)
            {
                this.Content.AppendFormat("rep ");
            }
            else if (insn.PfxRepe > 0)
            {
                this.Content.AppendFormat("repe ");
            }
            else if (insn.PfxRepne > 0)
            {
                this.Content.AppendFormat("repne ");
            }

            // Print the instruction mnemonic
            this.Content.AppendFormat("{0}", SharpDisasm.Udis86.Udis86.UdLookupMnemonic(insn.Mnemonic));

            if (insn.Operands.Length > 0 && insn.Operands[0].Type != UdType.UD_NONE)
            {
                Int32 cast = 0;
                Content.AppendFormat(" ");
                if (insn.Operands[0].Type == UdType.UD_OP_MEM)
                {
                    if ((insn.Operands.Length > 1 &&
                        (insn.Operands[1].Type == UdType.UD_OP_IMM ||
                        insn.Operands[1].Type == UdType.UD_OP_CONST)) ||
                        insn.Operands.Length < 2 || ////insn.Operands[1].Type == ud_type.UD_NONE) ||
                        (insn.Operands.Length > 1 &&
                         insn.Operands[0].Size != insn.Operands[1].Size &&
                         insn.Operands[1].Type != UdType.UD_OP_REG))
                    {
                        cast = 1;
                    }
                    else if (insn.Operands[1].Type == UdType.UD_OP_REG &&
                             insn.Operands[1].Base == UdType.UD_R_CL)
                    {
                        switch (insn.Mnemonic)
                        {
                            case UdMnemonicCode.UD_Ircl:
                            case UdMnemonicCode.UD_Irol:
                            case UdMnemonicCode.UD_Iror:
                            case UdMnemonicCode.UD_Ircr:
                            case UdMnemonicCode.UD_Ishl:
                            case UdMnemonicCode.UD_Ishr:
                            case UdMnemonicCode.UD_Isar:
                                cast = 1;
                                break;
                            default:
                                break;
                        }
                    }
                }

                this.Gen_operand(insn, insn.Operands[0], cast);
            }

            if (insn.Operands.Length > 1 && insn.Operands[1].Type != UdType.UD_NONE)
            {
                Int32 cast = 0;
                Content.AppendFormat(", ");
                if (insn.Operands[1].Type == UdType.UD_OP_MEM &&
                    insn.Operands[0].Size != insn.Operands[1].Size &&
                    !Udis86.UdOprIsSeg(insn.Operands[0].UdOperand))
                {
                    cast = 1;
                }

                this.Gen_operand(insn, insn.Operands[1], cast);
            }

            if (insn.Operands.Length > 2 && insn.Operands[1].Type != UdType.UD_NONE)
            {
                Int32 cast = 0;
                Content.AppendFormat(", ");
                if (insn.Operands[2].Type == UdType.UD_OP_MEM && insn.Operands[2].Size != insn.Operands[1].Size)
                {
                    cast = 1;
                }

                this.Gen_operand(insn, insn.Operands[2], cast);
            }

            if (insn.Operands.Length > 3 && insn.Operands[3].Type != UdType.UD_NONE)
            {
                Content.AppendFormat(", ");
                this.Gen_operand(insn, insn.Operands[3], 0);
            }
        }
    }
    //// End class
}
//// End namespace