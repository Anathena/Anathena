namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    /// <summary>
    /// TODO TODO.
    /// </summary>
    internal class SynIntel : Syn
    {
        /// <summary>
        /// Prints an operand cast.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="op">TODO op.</param>
        public void OperandCast(ref Ud u, ref UdOperand op)
        {
            if (u.BrFar > 0)
            {
                Syn.UdAsmPrintf(ref u, "far ");
            }

            switch (op.Size)
            {
                case 8:
                    Syn.UdAsmPrintf(ref u, "byte ");
                    break;
                case 16:
                    Syn.UdAsmPrintf(ref u, "word ");
                    break;
                case 32:
                    Syn.UdAsmPrintf(ref u, "dword ");
                    break;
                case 64:
                    Syn.UdAsmPrintf(ref u, "qword ");
                    break;
                case 80:
                    Syn.UdAsmPrintf(ref u, "tword ");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// translates to intel syntax.
        /// </summary>
        /// <param name="u">TODO u.</param>
        public void UdTranslateIntel(ref Ud u)
        {
            // Check if P_OSO prefix is used
            if (BitOps.P_OSO(u.ItabEntry.Prefix) == 0 && u.PfxOpr > 0)
            {
                switch (u.DisMode)
                {
                    case 16:
                        Syn.UdAsmPrintf(ref u, "o32 ");
                        break;
                    case 32:
                    case 64:
                        Syn.UdAsmPrintf(ref u, "o16 ");
                        break;
                }
            }

            // Check if P_ASO prefix was used
            if (BitOps.P_ASO(u.ItabEntry.Prefix) == 0 && u.PfxAdr > 0)
            {
                switch (u.DisMode)
                {
                    case 16:
                        Syn.UdAsmPrintf(ref u, "a32 ");
                        break;
                    case 32:
                        Syn.UdAsmPrintf(ref u, "a16 ");
                        break;
                    case 64:
                        Syn.UdAsmPrintf(ref u, "a32 ");
                        break;
                }
            }

            if (u.PfxSeg > 0 && u.Operand[0].UdType != UdType.UD_OP_MEM && u.Operand[1].UdType != UdType.UD_OP_MEM)
            {
                Syn.UdAsmPrintf(ref u, "{0} ", Syn.UdRegTab[u.PfxSeg - (byte)UdType.UD_R_AL]);
            }

            if (u.PfxLock > 0)
            {
                Syn.UdAsmPrintf(ref u, "lock ");
            }

            if (u.PfxRep > 0)
            {
                Syn.UdAsmPrintf(ref u, "rep ");
            }
            else if (u.PfxRepe > 0)
            {
                Syn.UdAsmPrintf(ref u, "repe ");
            }
            else if (u.PfxRepne > 0)
            {
                Syn.UdAsmPrintf(ref u, "repne ");
            }

            // Print the instruction mnemonic
            Syn.UdAsmPrintf(ref u, "{0}", Udis86.UdLookupMnemonic(u.Mnemonic));

            if (u.Operand[0].UdType != UdType.UD_NONE)
            {
                Int32 cast = 0;
                Syn.UdAsmPrintf(ref u, " ");

                if (u.Operand[0].UdType == UdType.UD_OP_MEM)
                {
                    if (u.Operand[1].UdType == UdType.UD_OP_IMM ||
                        u.Operand[1].UdType == UdType.UD_OP_CONST ||
                        u.Operand[1].UdType == UdType.UD_NONE ||
                        (u.Operand[0].Size != u.Operand[1].Size &&
                         u.Operand[1].UdType != UdType.UD_OP_REG))
                    {
                        cast = 1;
                    }
                    else if (u.Operand[1].UdType == UdType.UD_OP_REG &&
                             u.Operand[1].Base == UdType.UD_R_CL)
                    {
                        switch (u.Mnemonic)
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
                            default: break;
                        }
                    }
                }

                this.GenOperand(ref u, ref u.Operand[0], cast);
            }

            if (u.Operand[1].UdType != UdType.UD_NONE)
            {
                Int32 cast = 0;
                Syn.UdAsmPrintf(ref u, ", ");

                if (u.Operand[1].UdType == UdType.UD_OP_MEM &&
                    u.Operand[0].Size != u.Operand[1].Size &&
                    !Udis86.UdOprIsSeg(u.Operand[0]))
                {
                    cast = 1;
                }

                this.GenOperand(ref u, ref u.Operand[1], cast);
            }

            if (u.Operand[2].UdType != UdType.UD_NONE)
            {
                Int32 cast = 0;
                Syn.UdAsmPrintf(ref u, ", ");

                if (u.Operand[2].UdType == UdType.UD_OP_MEM &&
                    u.Operand[2].Size != u.Operand[1].Size)
                {
                    cast = 1;
                }

                this.GenOperand(ref u, ref u.Operand[2], cast);
            }

            if (u.Operand[3].UdType != UdType.UD_NONE)
            {
                Syn.UdAsmPrintf(ref u, ", ");
                this.GenOperand(ref u, ref u.Operand[3], 0);
            }
        }

        /// <summary>
        /// Generates assembly output for each operand.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="op">TODO op.</param>
        /// <param name="synCast">TODO synCast.</param>
        private void GenOperand(ref Ud u, ref UdOperand op, Int32 synCast)
        {
            switch (op.UdType)
            {
                case UdType.UD_OP_REG:
                    Syn.UdAsmPrintf(ref u, "{0}", Syn.UdRegTab[op.Base - UdType.UD_R_AL]);
                    break;
                case UdType.UD_OP_MEM:
                    if (synCast > 0)
                    {
                        this.OperandCast(ref u, ref op);
                    }

                    Syn.UdAsmPrintf(ref u, "[");

                    if (u.PfxSeg > 0)
                    {
                        Syn.UdAsmPrintf(ref u, "{0}:", Syn.UdRegTab[u.PfxSeg - (int)UdType.UD_R_AL]);
                    }

                    if (op.Base > 0)
                    {
                        Syn.UdAsmPrintf(ref u, "{0}", Syn.UdRegTab[op.Base - UdType.UD_R_AL]);
                    }

                    if (op.Index > 0)
                    {
                        Syn.UdAsmPrintf(ref u, "{0}{1}", op.Base != UdType.UD_NONE ? "+" : String.Empty, Syn.UdRegTab[op.Index - UdType.UD_R_AL]);
                        if (op.Scale > 0)
                        {
                            Syn.UdAsmPrintf(ref u, "*{0}", op.Scale);
                        }
                    }

                    if (op.Offset != 0)
                    {
                        this.UdSynPrintMemDisp(ref u, ref op, (op.Base != UdType.UD_NONE || op.Index != UdType.UD_NONE) ? 1 : 0);
                    }

                    Syn.UdAsmPrintf(ref u, "]");
                    break;
                case UdType.UD_OP_IMM:
                    this.UdSynPrintImm(ref u, ref op);
                    break;
                case UdType.UD_OP_JIMM:
                    this.UdSynPrintAddr(ref u, (Int64)this.UdSynRelTarget(ref u, ref op));
                    break;
                case UdType.UD_OP_PTR:
                    switch (op.Size)
                    {
                        case 32:
                            Syn.UdAsmPrintf(ref u, "word 0x{0:x}:0x{1:x}", op.Lval.PtrSeg, op.Lval.PtrOff & 0xFFFF);
                            break;
                        case 48:
                            Syn.UdAsmPrintf(ref u, "dword 0x{0:x}:0x{0:x}", op.Lval.PtrSeg, op.Lval.PtrOff);
                            break;
                    }

                    break;
                case UdType.UD_OP_CONST:
                    if (synCast > 0)
                    {
                        this.OperandCast(ref u, ref op);
                    }

                    Syn.UdAsmPrintf(ref u, "{0}", op.Lval.UdWord);
                    break;
                default:
                    return;
            }
        }
    }
    //// End class
}
//// End namespace