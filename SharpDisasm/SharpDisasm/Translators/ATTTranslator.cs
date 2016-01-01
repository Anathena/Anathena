using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDisasm.Translators
{
    /// <summary>
    /// Translates to AT&amp;T syntax
    /// </summary>
    public class ATTTranslator : Translator
    {
        /// <summary>
        /// Translate a list of instructions separated by <see cref="Environment.NewLine"/>.
        /// </summary>
        /// <param name="insns"></param>
        /// <returns></returns>
        public override string Translate(IEnumerable<Instruction> insns)
        {
            Content = new StringBuilder();
            bool first = true;
            foreach (var insn in insns)
            {
                if (first)
                    first = false;
                else
                    Content.Append(Environment.NewLine);

                if (IncludeAddress)
                    WriteAddress(insn);
                if (IncludeBinary)
                    WriteBinary(insn);

                ud_translate_att(insn);
            }

            return Content.ToString();
        }

        /// <summary>
        /// Translate a single instruction
        /// </summary>
        /// <param name="insn"></param>
        /// <returns></returns>
        public override string Translate(Instruction insn)
        {
            Content = new StringBuilder();
            
            if (IncludeAddress)
                WriteAddress(insn);
            if (IncludeBinary)
                WriteBinary(insn);
            ud_translate_att(insn);

            return Content.ToString();
        }


        /* -----------------------------------------------------------------------------
         * opr_cast() - Prints an operand cast.
         * -----------------------------------------------------------------------------
         */
        /// <summary>
        /// Prints an operand cast.
        /// </summary>
        /// <param name="insn"></param>
        /// <param name="op"></param>
        void
        opr_cast(Instruction insn, Operand op)
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

        /* -----------------------------------------------------------------------------
         * gen_operand() - Generates assembly output for each operand.
         * -----------------------------------------------------------------------------
         */
        /// <summary>
        /// Generates assembly output for each operand
        /// </summary>
        /// <param name="u"></param>
        /// <param name="op"></param>
        void
        gen_operand(Instruction u, Operand op)
        {
            switch (op.Type)
            {
                case Udis86.ud_type.UD_OP_CONST:
                    Content.AppendFormat("$0x{0:x4}", op.LvalUDWord);
                    break;

                case Udis86.ud_type.UD_OP_REG:
                    Content.AppendFormat("%{0}", RegisterForType(op.Base));
                    break;

                case Udis86.ud_type.UD_OP_MEM:
                    if (u.br_far != 0)
                    {
                        opr_cast(u, op);
                    }
                    if (u.pfx_seg != 0)
                    {
                        Content.AppendFormat("%{0}:", RegisterForType((Udis86.ud_type)u.pfx_seg));
                    }
                    if (op.Offset != 0)
                    {
                        ud_syn_print_mem_disp(u, op, 0);
                    }
                    if (op.Base != Udis86.ud_type.UD_NONE)
                    {
                        Content.AppendFormat("(%{0}", RegisterForType(op.Base));
                    }
                    if (op.Index != Udis86.ud_type.UD_NONE)
                    {
                        if (op.Base != Udis86.ud_type.UD_NONE)
                        {
                            Content.AppendFormat(",");
                        }
                        else
                        {
                            Content.AppendFormat("(");
                        }
                        Content.AppendFormat("%{0}", RegisterForType(op.Index));
                    }
                    if (op.Scale != 0)
                    {
                        Content.AppendFormat(",{0}", op.Scale);
                    }
                    if (op.Base != Udis86.ud_type.UD_NONE || op.Index != Udis86.ud_type.UD_NONE)
                    {
                        Content.AppendFormat(")");
                    }
                    break;

                case Udis86.ud_type.UD_OP_IMM:
                    Content.AppendFormat("$");
                    ud_syn_print_imm(u, op);
                    break;

                case Udis86.ud_type.UD_OP_JIMM:
                    ud_syn_print_addr(u, (long)ud_syn_rel_target(u, op));
                    break;

                case Udis86.ud_type.UD_OP_PTR:
                    switch (op.Size)
                    {
                        case 32:
                            Content.AppendFormat("$0x{0:x}, $0x{1:x}", op.PtrSegment,
                              op.PtrOffset & 0xFFFF);
                            break;
                        case 48:
                            Content.AppendFormat("$0x{0:x}, $0x{1:x}", op.PtrSegment,
                              op.PtrOffset);
                            break;
                    }
                    break;

                default: return;
            }
        }

        /* =============================================================================
         * translates to AT&T syntax 
         * =============================================================================
         */
        /// <summary>
        /// Translates to AT&amp;T syntax 
        /// </summary>
        /// <param name="u"></param>
        void
        ud_translate_att(Instruction u)
        {
            int size = 0;
            bool star = false;

            /* check if P_OSO prefix is used */
            if (SharpDisasm.Udis86.BitOps.P_OSO(u.itab_entry.Prefix) == 0 && u.pfx_opr != 0)
            {
                switch (u.dis_mode)
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

            /* check if P_ASO prefix was used */
            if (SharpDisasm.Udis86.BitOps.P_ASO(u.itab_entry.Prefix) == 0 && u.pfx_adr != 0)
            {
                switch (u.dis_mode)
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

            if (u.pfx_lock != 0)
                Content.AppendFormat("lock ");
            if (u.pfx_rep != 0)
            {
                Content.AppendFormat("rep ");
            }
            else if (u.pfx_repe != 0)
            {
                Content.AppendFormat("repe ");
            }
            else if (u.pfx_repne != 0)
            {
                Content.AppendFormat("repne ");
            }

            /* special instructions */
            switch (u.Mnemonic)
            {
                case Udis86.ud_mnemonic_code.UD_Iretf:
                    Content.AppendFormat("lret ");
                    size = -1;
                    break;
                case Udis86.ud_mnemonic_code.UD_Idb:
                    Content.AppendFormat(".byte 0x{0:x2}", u.Operands[0].LvalByte);
                    return;
                case Udis86.ud_mnemonic_code.UD_Ijmp:
                case Udis86.ud_mnemonic_code.UD_Icall:
                    if (u.br_far != 0)
                    {
                        Content.AppendFormat("l");
                        size = -1;
                    }
                    if (u.Operands[0].Type == Udis86.ud_type.UD_OP_REG)
                    {
                        star = true;
                    }
                    Content.AppendFormat("{0}", Udis86.udis86.ud_lookup_mnemonic(u.Mnemonic));
                    break;
                case Udis86.ud_mnemonic_code.UD_Ibound:
                case Udis86.ud_mnemonic_code.UD_Ienter:
                    if (u.Operands.Length > 0 && u.Operands[0].Type != Udis86.ud_type.UD_NONE)
                        gen_operand(u, u.Operands[0]);
                    if (u.Operands.Length > 1 && u.Operands[1].Type != Udis86.ud_type.UD_NONE)
                    {
                        Content.AppendFormat(",");
                        gen_operand(u, u.Operands[1]);
                    }
                    return;
                default:
                    Content.AppendFormat("{0}", Udis86.udis86.ud_lookup_mnemonic(u.Mnemonic));
                    break;
            }

            if (size != -1 && u.Operands.Length > 0 && u.Operands.Any(o => o.Type == Udis86.ud_type.UD_OP_MEM))
                size = u.Operands[0].Size;

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

            if (u.Operands.Length > 3 && u.Operands[3].Type != Udis86.ud_type.UD_NONE)
            {
                gen_operand(u, u.Operands[3]);
                Content.AppendFormat(", ");
            }
            if (u.Operands.Length > 2 && u.Operands[2].Type != Udis86.ud_type.UD_NONE)
            {
                gen_operand(u, u.Operands[2]);
                Content.AppendFormat(", ");
            }
            if (u.Operands.Length > 1 && u.Operands[1].Type != Udis86.ud_type.UD_NONE)
            {
                gen_operand(u, u.Operands[1]);
                Content.AppendFormat(", ");
            }
            if (u.Operands.Length > 0 && u.Operands[0].Type != Udis86.ud_type.UD_NONE)
            {
                gen_operand(u, u.Operands[0]);
            }
        }
    }
}
