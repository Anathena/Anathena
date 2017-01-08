namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// TODO TODO.
    /// </summary>
    internal class Decode
    {
        /// <summary>
        /// TODO TODO.
        /// </summary>
        public const Int32 MaxInstructionLength = 15;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        public const Int32 MaxPrefixes = 15;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        public const Int32 UdEoi = -1;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        public const Int32 UdInpCacheSize = 32;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        public const Int32 UdVendorAmd = 0;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        public const Int32 UdVendorIntel = 1;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        public const Int32 UdVendorAny = 2;

        /// <summary>
        /// Instruction decoder. Returns the number of bytes decoded.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        public Int32 UdDecode(ref Ud u)
        {
            this.InpStart(ref u);
            this.ClearInstruction(ref u);
            u.Le = InstructionTables.ud_lookup_table_list[0];
            u.Error = (Byte)((this.DecodePrefixes(ref u) == -1 || this.DecodeOpcode(ref u) == -1 || u.Error == 1) ? 1 : 0);

            // Handle decode error
            if (u.Error == 1)
            {
                // Clear out the decode data
                this.ClearInstruction(ref u);

                // Mark the sequence of bytes as invalid. Entry 0 is invalid
                u.ItabEntry = InstructionTables.ud_itab[0];
                u.Mnemonic = u.ItabEntry.Mnemonic;
            }

            // maybe this stray segment override byte should be spewed out?
            if (BitOps.P_SEG(u.ItabEntry.Prefix) == 0 && u.Operand[0].UdType != UdType.UD_OP_MEM && u.Operand[1].UdType != UdType.UD_OP_MEM)
            {
                u.PfxSeg = 0;
            }

            // Set offset of instruction
            u.InstructionOffset = u.Pc;

            // Set translation buffer index to 0
            u.AsmBufferFill = 0;

            // Move program counter by bytes decoded
            u.Pc += (uint)u.InputCtr;

            // Return number of bytes disassembled
            return u.InputCtr;
        }

        /// <summary>
        /// Should be called before each de-code operation.
        /// </summary>
        /// <param name="u">TODO u.</param>
        private void InpStart(ref Ud u)
        {
            u.InputCtr = 0;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private unsafe Int32 InpPeek(ref Ud u)
        {
            if (u.InputEnd == 0)
            {
                if (u.InputBuffer != null)
                {
                    if (u.InputBufferIndex < u.InputBufferSize)
                    {
                        return u.InputBuffer[u.InputBufferIndex];
                    }
                }
                else if (u.InputPeek != UdEoi)
                {
                    return u.InputPeek;
                }
                else
                {
                    Int32 c;
                    if ((c = u.InputHook(ref u)) != UdEoi)
                    {
                        u.InputPeek = c;
                        return u.InputPeek;
                    }
                }
            }

            u.InputEnd = 1;
            u.Error = 1;
            u.ErrorMessage = "byte expected, eoi received";
            return 0;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private unsafe Byte InpNext(ref Ud u)
        {
            if (u.InputEnd == 0)
            {
                if (u.InputBuffer != null)
                {
                    if (u.InputBufferIndex < u.InputBufferSize)
                    {
                        u.InputCtr++;
                        return u.InputCur = u.InputBuffer[u.InputBufferIndex++];
                    }
                }
                else
                {
                    Int32 c = u.InputPeek;
                    if ((c = u.InputHook(ref u)) != UdEoi)
                    {
                        u.InputPeek = UdEoi;
                        u.InputCur = (Byte)c;
                        u.InputSession[u.InputCtr++] = u.InputCur;
                        return u.InputCur;
                    }
                }
            }

            u.InputEnd = 1;
            u.Error = 1;
            u.ErrorMessage = "byte expected, eoi received\n";
            return 0;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="ud">TODO u.d</param>
        /// <returns>TODO TODO.</returns>
        private Byte InpCurr(ref Ud ud)
        {
            return ud.InputCur;
        }

        /// <summary>
        /// Load little-endian values from input.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Byte InpUInt8(ref Ud u)
        {
            return this.InpNext(ref u);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private UInt16 InpUInt16(ref Ud u)
        {
            UInt16 r, ret;

            ret = (UInt16)this.InpNext(ref u);
            r = (UInt16)this.InpNext(ref u);
            return (UInt16)(ret | (r << 8));
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private UInt32 InpUInt32(ref Ud u)
        {
            UInt32 r, ret;

            ret = (UInt32)this.InpNext(ref u);
            r = (UInt32)this.InpNext(ref u);
            ret = ret | (r << 8);
            r = (UInt32)this.InpNext(ref u);
            ret = ret | (r << 16);
            r = (UInt32)this.InpNext(ref u);
            return ret | (r << 24);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private UInt64 InpUInt64(ref Ud u)
        {
            UInt64 r, ret;

            ret = this.InpNext(ref u);
            r = this.InpNext(ref u);
            ret = ret | (r << 8);
            r = this.InpNext(ref u);
            ret = ret | (r << 16);
            r = this.InpNext(ref u);
            ret = ret | (r << 24);
            r = this.InpNext(ref u);
            ret = ret | (r << 32);
            r = this.InpNext(ref u);
            ret = ret | (r << 40);
            r = this.InpNext(ref u);
            ret = ret | (r << 48);
            r = this.InpNext(ref u);

            return ret | (r << 56);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="disMode">TODO disMode.</param>
        /// <param name="rexW">TODO rexW.</param>
        /// <param name="pfxOpr">TODO pfxOpr.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 EffOprMode(Int32 disMode, Int32 rexW, Int32 pfxOpr)
        {
            if (disMode == 64)
            {
                return rexW > 0 ? 64 : (pfxOpr > 0 ? 16 : 32);
            }
            else if (disMode == 32)
            {
                return pfxOpr > 0 ? 16 : 32;
            }
            else
            {
                Debug.Assert(disMode == 16, "TODO: REASON");
                return pfxOpr > 0 ? 32 : 16;
            }
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="disMode">TODO disMode.</param>
        /// <param name="pfxAdr">TODO pfxAdr.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 EffAdrMode(Int32 disMode, Int32 pfxAdr)
        {
            if (disMode == 64)
            {
                return pfxAdr > 0 ? 32 : 64;
            }
            else if (disMode == 32)
            {
                return pfxAdr > 0 ? 16 : 32;
            }
            else
            {
                Debug.Assert(disMode == 16, "TODO: REASON");
                return pfxAdr > 0 ? 32 : 16;
            }
        }

        /// <summary>
        /// Extracts instruction prefixes.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 DecodePrefixes(ref Ud u)
        {
            Boolean done = false;
            Byte curr = 0;
            Byte last = 0;

            if (u.Error != 0)
            {
                return u.Error;
            }

            do
            {
                last = curr;
                curr = this.InpNext(ref u);
                if (u.Error != 0)
                {
                    return u.Error;
                }

                if (u.InputCtr == MaxInstructionLength)
                {
                    u.Error = 1;
                    u.ErrorMessage = "max instruction length";
                    return u.Error;
                }

                switch (curr)
                {
                    case 0x2E:
                        u.PfxSeg = (Byte)UdType.UD_R_CS;
                        break;
                    case 0x36:
                        u.PfxSeg = (Byte)UdType.UD_R_SS;
                        break;
                    case 0x3E:
                        u.PfxSeg = (Byte)UdType.UD_R_DS;
                        break;
                    case 0x26:
                        u.PfxSeg = (Byte)UdType.UD_R_ES;
                        break;
                    case 0x64:
                        u.PfxSeg = (Byte)UdType.UD_R_FS;
                        break;
                    case 0x65:
                        u.PfxSeg = (Byte)UdType.UD_R_GS;
                        break;
                    case 0x67:
                        // Adress-size override prefix
                        u.PfxAdr = 0x67;
                        break;
                    case 0xF0:
                        u.PfxLock = 0xF0;
                        break;
                    case 0x66:
                        u.PfxOpr = 0x66;
                        break;
                    case 0xF2:
                        u.PfxStr = 0xf2;
                        break;
                    case 0xF3:
                        u.PfxStr = 0xf3;
                        break;
                    default:
                        // Consume if rex
                        done = (u.DisMode == 64 && (curr & 0xF0) == 0x40) ? false : true;
                        break;
                }
            }
            while (!done);

            // Rex prefixes in 64bit mode, must be the last prefix
            if (u.DisMode == 64 && (last & 0xF0) == 0x40)
            {
                u.PfxRex = last;
            }

            return 0;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Byte VexL(ref Ud u)
        {
            Debug.Assert(u.VexOp != 0, "TODO: REASON");
            return (Byte)(((u.VexOp == 0xc4 ? u.VexB2 : u.VexB1) >> 2) & 1);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Byte VexW(ref Ud u)
        {
            Debug.Assert(u.VexOp != 0, "TODO: REASON");
            return (Byte)(u.VexOp == 0xc4 ? ((u.VexB2 >> 7) & 1) : 0);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Byte Modrm(ref Ud u)
        {
            if (u.HaveModrm == 0)
            {
                u.Modrm = this.InpNext(ref u);
                u.ModrmOffset = (Byte)(u.InputCtr - 1);
                u.HaveModrm = 1;
            }

            return u.Modrm;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="operandSize">TODO operandSize.</param>
        /// <returns>TODO TODO.</returns>
        private UdOperandSize ResolveOperandSize(ref Ud u, UdOperandSize operandSize)
        {
            switch (operandSize)
            {
                case UdOperandSize.SZ_V:
                    return (UdOperandSize)u.OprMode;
                case UdOperandSize.SZ_Z:
                    return (UdOperandSize)((u.OprMode == 16) ? 16 : 32);
                case UdOperandSize.SZ_Y:
                    return (UdOperandSize)((u.OprMode == 16) ? 32 : u.OprMode);
                case UdOperandSize.SZ_RDQ:
                    return (UdOperandSize)((u.DisMode == 64) ? 64 : 32);
                case UdOperandSize.SZ_X:
                    Debug.Assert(u.VexOp != 0, "TODO: REASON");
                    return (BitOps.P_VEXL(u.ItabEntry.Prefix) > 0 && this.VexL(ref u) > 0) ? UdOperandSize.SZ_QQ : UdOperandSize.SZ_DQ;
                default:
                    return operandSize;
            }
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 ResolveMnemonic(ref Ud u)
        {
            // Resolve 3dnow weirdness
            if (u.Mnemonic == UdMnemonicCode.UD_I3dnow)
            {
                u.Mnemonic = InstructionTables.ud_itab[u.Le.Table[this.InpCurr(ref u)]].Mnemonic;
            }

            // SWAPGS is only valid in 64bits mode
            if (u.Mnemonic == UdMnemonicCode.UD_Iswapgs && u.DisMode != 64)
            {
                u.Error = 1;
                u.ErrorMessage = "swapgs invalid in 64bits mode\n";
                return -1;
            }

            if (u.Mnemonic == UdMnemonicCode.UD_Ixchg)
            {
                if ((u.Operand[0].UdType == UdType.UD_OP_REG && u.Operand[0].Base == UdType.UD_R_AX &&
                     u.Operand[1].UdType == UdType.UD_OP_REG && u.Operand[1].Base == UdType.UD_R_AX) ||
                    (u.Operand[0].UdType == UdType.UD_OP_REG && u.Operand[0].Base == UdType.UD_R_EAX &&
                     u.Operand[1].UdType == UdType.UD_OP_REG && u.Operand[1].Base == UdType.UD_R_EAX))
                {
                    u.Operand[0].UdType = UdType.UD_NONE;
                    u.Operand[1].UdType = UdType.UD_NONE;
                    u.Mnemonic = UdMnemonicCode.UD_Inop;
                }
            }

            if (u.Mnemonic == UdMnemonicCode.UD_Inop && u.PfxRepe != 0)
            {
                u.PfxRepe = 0;
                u.Mnemonic = UdMnemonicCode.UD_Ipause;
            }

            return 0;
        }

        /// <summary>
        /// Decodes operands of the type seg:offset.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="op">TODO op.</param>
        private void DecodeA(ref Ud u, ref UdOperand op)
        {
            if (u.OprMode == 16)
            {
                // seg16:off16
                op.UdType = UdType.UD_OP_PTR;
                op.Size = 32;
                op.Lval.PtrOff = this.InpUInt16(ref u);
                op.Lval.PtrSeg = this.InpUInt16(ref u);
            }
            else
            {
                // seg16:off32
                op.UdType = UdType.UD_OP_PTR;
                op.Size = 48;
                op.Lval.PtrOff = this.InpUInt32(ref u);
                op.Lval.PtrSeg = this.InpUInt16(ref u);
            }
        }

        /// <summary>
        /// Returns decoded General Purpose Register .
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="s">TODO s.</param>
        /// <param name="rm">TODO rm.</param>
        /// <returns>TODO TODO.</returns>
        private UdType DecodeGeneralPurposeRegister(ref Ud u, UdOperandSize s, Byte rm)
        {
            switch ((Int32)s)
            {
                case 64:
                    return UdType.UD_R_RAX + rm;
                case 32:
                    return UdType.UD_R_EAX + rm;
                case 16:
                    return UdType.UD_R_AX + rm;
                case 8:
                    if (u.DisMode == 64 && u.PfxRex != 0)
                    {
                        if (rm >= 4)
                        {
                            return UdType.UD_R_SPL + (rm - 4);
                        }

                        return UdType.UD_R_AL + rm;
                    }
                    else return UdType.UD_R_AL + rm;
                case 0:
                    // Invalid size in case of a decode error
                    Debug.Assert(u.Error == 0, "invalid operand size");
                    return UdType.UD_NONE;
                default:
                    Debug.Assert(false, "invalid operand size");
                    return UdType.UD_NONE;
            }
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="opr">TODO opr.</param>
        /// <param name="type">TODO type.</param>
        /// <param name="num">TODO num.</param>
        /// <param name="size">TODO size.</param>
        private void DecodeReg(ref Ud u, ref UdOperand opr, RegClass type, Byte num, UdOperandSize size)
        {
            Int32 reg;
            size = this.ResolveOperandSize(ref u, size);

            switch (type)
            {
                case RegClass.REGCLASS_GPR:
                    reg = (Int32)this.DecodeGeneralPurposeRegister(ref u, size, (Byte)num);
                    break;
                case RegClass.REGCLASS_MMX:
                    reg = (Int32)UdType.UD_R_MM0 + (num & 7);
                    break;
                case RegClass.REGCLASS_XMM:
                    reg = num + (Int32)(size == UdOperandSize.SZ_QQ ? UdType.UD_R_YMM0 : UdType.UD_R_XMM0);
                    break;
                case RegClass.REGCLASS_CR:
                    reg = (Int32)UdType.UD_R_CR0 + num;
                    break;
                case RegClass.REGCLASS_DB:
                    reg = (Int32)UdType.UD_R_DR0 + num;
                    break;
                case RegClass.REGCLASS_SEG:
                    {
                        /*
                         * Only 6 segment registers, anything else is an error.
                         */
                        if ((num & 7) > 5)
                        {
                            u.Error = 1;
                            u.ErrorMessage = "invalid segment register value\n";
                            return;
                        }
                        else
                        {
                            reg = (Int32)UdType.UD_R_ES + (num & 7);
                        }

                        break;
                    }

                default:
                    Debug.Assert(false, "invalid register type");
                    return;
            }

            opr.UdType = UdType.UD_OP_REG;
            opr.Base = (UdType)reg;
            opr.Size = (Byte)size;
        }

        /// <summary>
        /// Decode Immediate values.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="size">TODO size.</param>
        /// <param name="op">TODO op.</param>
        private void DecodeImmediate(ref Ud u, UdOperandSize size, ref UdOperand op)
        {
            op.Size = (Byte)this.ResolveOperandSize(ref u, size);
            op.UdType = UdType.UD_OP_IMM;

            switch (op.Size)
            {
                case 8:
                    op.Lval.SByte = (SByte)this.InpUInt8(ref u);
                    break;
                case 16:
                    op.Lval.UWord = this.InpUInt16(ref u);
                    break;
                case 32:
                    op.Lval.UdWord = this.InpUInt32(ref u);
                    break;
                case 64:
                    op.Lval.UqWord = this.InpUInt64(ref u);
                    break;
                default: return;
            }
        }

        /// <summary>
        /// Decode mem address displacement.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="size">TODO size.</param>
        /// <param name="op">TODO op.</param>
        private void DecodeMemoryDisplacement(ref Ud u, int size, ref UdOperand op)
        {
            switch (size)
            {
                case 8:
                    op.Offset = 8;
                    op.Lval.UByte = this.InpUInt8(ref u);
                    break;
                case 16:
                    op.Offset = 16;
                    op.Lval.UWord = this.InpUInt16(ref u);
                    break;
                case 32:
                    op.Offset = 32;
                    op.Lval.UdWord = this.InpUInt32(ref u);
                    break;
                case 64:
                    op.Offset = 64;
                    op.Lval.UqWord = this.InpUInt64(ref u);
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Decodes reg field of mod/rm byte.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="operand">TODO operand.</param>
        /// <param name="type">TODO type.</param>
        /// <param name="size">TODO size.</param>
        private void DecodeModrmReg(ref Ud u, ref UdOperand operand, RegClass type, UdOperandSize size)
        {
            Byte reg = (Byte)((BitOps.REX_R(u.Rex) << 3) | BitOps.MODRM_REG(this.Modrm(ref u)));
            this.DecodeReg(ref u, ref operand, type, reg, size);
        }

        /// <summary>
        /// Decodes rm field of mod/rm byte.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="op">TODO op.</param>
        /// <param name="type">TODO type.</param>
        /// <param name="size">TODO size.</param>
        private void DecodeModrmRm(ref Ud u, ref UdOperand op, RegClass type, UdOperandSize size)
        {
            Int32 offset = 0;
            Byte mod, rm;

            // Get mod, r/m and reg fields
            mod = BitOps.MODRM_MOD(this.Modrm(ref u));
            rm = (Byte)((BitOps.REX_B(u.Rex) << 3) | BitOps.MODRM_RM(this.Modrm(ref u)));

            // If mod is 0b11, then the modrm.rm specifies a register
            if (mod == 3)
            {
                this.DecodeReg(ref u, ref op, type, rm, size);
                return;
            }

            // !0b11 => Memory Address
            op.UdType = UdType.UD_OP_MEM;
            op.Size = (Byte)this.ResolveOperandSize(ref u, size);

            if (u.AddressMode == 64)
            {
                op.Base = UdType.UD_R_RAX + rm;
                if (mod == 1)
                {
                    offset = 8;
                }
                else if (mod == 2)
                {
                    offset = 32;
                }
                else if (mod == 0 && (rm & 7) == 5)
                {
                    op.Base = UdType.UD_R_RIP;
                    offset = 32;
                }
                else
                {
                    offset = 0;
                }

                // Scale-Index-Base (SIB) 
                if ((rm & 7) == 4)
                {
                    this.InpNext(ref u);

                    op.Base = UdType.UD_R_RAX + (BitOps.SIB_B(this.InpCurr(ref u)) | (BitOps.REX_B(u.Rex) << 3));
                    op.Index = UdType.UD_R_RAX + (BitOps.SIB_I(this.InpCurr(ref u)) | (BitOps.REX_X(u.Rex) << 3));

                    // special conditions for base reference
                    if (op.Index == UdType.UD_R_RSP)
                    {
                        op.Index = UdType.UD_NONE;
                        op.Scale = (Byte)UdType.UD_NONE;
                    }
                    else
                    {
                        op.Scale = (Byte)((1 << BitOps.SIB_S(this.InpCurr(ref u))) & ~1);
                    }

                    if (op.Base == UdType.UD_R_RBP || op.Base == UdType.UD_R_R13)
                    {
                        if (mod == 0)
                        {
                            op.Base = UdType.UD_NONE;
                        }

                        if (mod == 1)
                        {
                            offset = 8;
                        }
                        else
                        {
                            offset = 32;
                        }
                    }
                }
                else
                {
                    op.Scale = 0;
                    op.Index = UdType.UD_NONE;
                }
            }
            else if (u.AddressMode == 32)
            {
                op.Base = UdType.UD_R_EAX + rm;
                if (mod == 1)
                {
                    offset = 8;
                }
                else if (mod == 2)
                {
                    offset = 32;
                }
                else if (mod == 0 && rm == 5)
                {
                    op.Base = UdType.UD_NONE;
                    offset = 32;
                }
                else
                {
                    offset = 0;
                }

                // Scale-Index-Base (SIB)
                if ((rm & 7) == 4)
                {
                    this.InpNext(ref u);

                    op.Scale = (Byte)((1 << BitOps.SIB_S(this.InpCurr(ref u))) & ~1);
                    op.Index = UdType.UD_R_EAX + (BitOps.SIB_I(this.InpCurr(ref u)) | (BitOps.REX_X(u.PfxRex) << 3));
                    op.Base = UdType.UD_R_EAX + (BitOps.SIB_B(this.InpCurr(ref u)) | (BitOps.REX_B(u.PfxRex) << 3));

                    if (op.Index == UdType.UD_R_ESP)
                    {
                        op.Index = UdType.UD_NONE;
                        op.Scale = (Byte)UdType.UD_NONE;
                    }

                    // special condition for base reference
                    if (op.Base == UdType.UD_R_EBP)
                    {
                        if (mod == 0)
                        {
                            op.Base = UdType.UD_NONE;
                        }

                        if (mod == 1)
                        {
                            offset = 8;
                        }
                        else
                        {
                            offset = 32;
                        }
                    }
                }
                else
                {
                    op.Scale = 0;
                    op.Index = UdType.UD_NONE;
                }
            }
            else
            {
                UdType[] bases = { UdType.UD_R_BX, UdType.UD_R_BX, UdType.UD_R_BP, UdType.UD_R_BP, UdType.UD_R_SI, UdType.UD_R_DI, UdType.UD_R_BP, UdType.UD_R_BX };
                UdType[] indices = { UdType.UD_R_SI, UdType.UD_R_DI, UdType.UD_R_SI, UdType.UD_R_DI, UdType.UD_NONE, UdType.UD_NONE, UdType.UD_NONE, UdType.UD_NONE };

                op.Base = bases[rm & 7];
                op.Index = indices[rm & 7];
                op.Scale = 0;
                if (mod == 0 && rm == 6)
                {
                    offset = 16;
                    op.Base = UdType.UD_NONE;
                }
                else if (mod == 1)
                {
                    offset = 8;
                }
                else if (mod == 2)
                {
                    offset = 16;
                }
            }

            if (offset > 0)
            {
                this.DecodeMemoryDisplacement(ref u, offset, ref op);
            }
            else
            {
                op.Offset = 0;
            }
        }

        /// <summary>
        /// Decode offset-only memory operand.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="size">TODO size.</param>
        /// <param name="opr">TODO opr.</param>
        private void DecodeMOffset(ref Ud u, UdOperandSize size, ref UdOperand opr)
        {
            opr.UdType = UdType.UD_OP_MEM;
            opr.Base = UdType.UD_NONE;
            opr.Index = UdType.UD_NONE;
            opr.Scale = 0;
            opr.Size = (Byte)this.ResolveOperandSize(ref u, size);
            this.DecodeMemoryDisplacement(ref u, u.AddressMode, ref opr);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="opr">TODO opr.</param>
        /// <param name="size">TODO size.</param>
        private void DecodeVexVvvv(ref Ud u, ref UdOperand opr, UdOperandSize size)
        {
            Byte vvvv;
            Debug.Assert(u.VexOp != 0, "TODO: REASON");

            vvvv = (Byte)(((u.VexOp == 0xc4 ? u.VexB2 : u.VexB1) >> 3) & 0xf);
            this.DecodeReg(ref u, ref opr, RegClass.REGCLASS_XMM, (Byte)(0xf & ~vvvv), size);
        }

        /// <summary>
        /// Decode source operand encoded in immediate byte [7:4].
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="opr">TODO opr.</param>
        /// <param name="size">TODO size.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 DecodeVexImmreg(ref Ud u, ref UdOperand opr, UdOperandSize size)
        {
            Byte imm = (Byte)this.InpNext(ref u);
            Byte mask = (Byte)(u.DisMode == 64 ? 0xf : 0x7);
            if (u.Error != 0)
            {
                return u.Error;
            }

            Debug.Assert(u.VexOp != 0, "TODO: REASON");
            this.DecodeReg(ref u, ref opr, RegClass.REGCLASS_XMM, (Byte)(mask & (imm >> 4)), size);
            return 0;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="size">TODO size.</param>
        /// <returns>TODO TODO.</returns>
        private UdOperandSize MxMemSize(Int32 size)
        {
            return (UdOperandSize)((size >> 8) & 0xff);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="size">TODO size.</param>
        /// <returns>TODO TODO.</returns>
        private UdOperandSize MxRegSize(Int32 size)
        {
            return (UdOperandSize)(size & 0xff);
        }

        /// <summary>
        /// Disassembles Operands.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="operand">TODO operand.</param>
        /// <param name="type">TODO type.</param>
        /// <param name="size">TODO size.</param>
        /// <returns>TODO TODO.</returns>
        private UdType DecodeOperand(ref Ud u, ref UdOperand operand, UdOperandCode type, UdOperandSize size)
        {
            operand.UdType = UdType.UD_NONE;
            operand.OperandCode = type;

            switch (type)
            {
                case UdOperandCode.OP_A:
                    this.DecodeA(ref u, ref operand);
                    break;
                case UdOperandCode.OP_MR:
                    this.DecodeModrmRm(ref u, ref operand, (Byte)RegClass.REGCLASS_GPR, BitOps.MODRM_MOD(this.Modrm(ref u)) == 3 ? size.Mx_reg_size() : size.Mx_mem_size());
                    break;
                case UdOperandCode.OP_F:
                    u.BrFar = 1;
                    if (BitOps.MODRM_MOD(this.Modrm(ref u)) == 3)
                    {
                        u.Error = 1;
                        u.ErrorMessage = "expected modrm.mod != 3\n";
                    }

                    this.DecodeModrmRm(ref u, ref operand, RegClass.REGCLASS_GPR, size);
                    break;
                case UdOperandCode.OP_M:
                    if (BitOps.MODRM_MOD(this.Modrm(ref u)) == 3)
                    {
                        u.Error = 1;
                        u.ErrorMessage = "expected modrm.mod != 3\n";
                    }

                    this.DecodeModrmRm(ref u, ref operand, RegClass.REGCLASS_GPR, size);
                    break;
                case UdOperandCode.OP_E:
                    this.DecodeModrmRm(ref u, ref operand, RegClass.REGCLASS_GPR, size);
                    break;
                case UdOperandCode.OP_G:
                    this.DecodeModrmReg(ref u, ref operand, RegClass.REGCLASS_GPR, size);
                    break;
                case UdOperandCode.OP_sI:
                case UdOperandCode.OP_I:
                    this.DecodeImmediate(ref u, size, ref operand);
                    break;
                case UdOperandCode.OP_I1:
                    operand.UdType = UdType.UD_OP_CONST;
                    operand.Lval.UdWord = 1;
                    break;
                case UdOperandCode.OP_N:
                    if (BitOps.MODRM_MOD(this.Modrm(ref u)) != 3)
                    {
                        u.Error = 1;
                        u.ErrorMessage = "expected modrm.mod == 3\n";
                    }

                    this.DecodeModrmRm(ref u, ref operand, RegClass.REGCLASS_MMX, size);
                    break;
                case UdOperandCode.OP_Q:
                    this.DecodeModrmRm(ref u, ref operand, RegClass.REGCLASS_MMX, size);
                    break;
                case UdOperandCode.OP_P:
                    this.DecodeModrmReg(ref u, ref operand, RegClass.REGCLASS_MMX, size);
                    break;
                case UdOperandCode.OP_U:
                    if (BitOps.MODRM_MOD(this.Modrm(ref u)) != 3)
                    {
                        u.Error = 1;
                        u.ErrorMessage = "expected modrm.mod == 3\n";
                    }

                    this.DecodeModrmRm(ref u, ref operand, RegClass.REGCLASS_XMM, size);
                    break;
                case UdOperandCode.OP_W:
                    this.DecodeModrmRm(ref u, ref operand, RegClass.REGCLASS_XMM, size);
                    break;
                case UdOperandCode.OP_V:
                    this.DecodeModrmReg(ref u, ref operand, RegClass.REGCLASS_XMM, size);
                    break;
                case UdOperandCode.OP_H:
                    this.DecodeVexVvvv(ref u, ref operand, size);
                    break;
                case UdOperandCode.OP_MU:
                    this.DecodeModrmRm(ref u, ref operand, RegClass.REGCLASS_XMM, BitOps.MODRM_MOD(this.Modrm(ref u)) == 3 ? size.Mx_reg_size() : size.Mx_mem_size());
                    break;
                case UdOperandCode.OP_S:
                    this.DecodeModrmReg(ref u, ref operand, RegClass.REGCLASS_SEG, size);
                    break;
                case UdOperandCode.OP_O:
                    this.DecodeMOffset(ref u, size, ref operand);
                    break;
                case UdOperandCode.OP_R0:
                case UdOperandCode.OP_R1:
                case UdOperandCode.OP_R2:
                case UdOperandCode.OP_R3:
                case UdOperandCode.OP_R4:
                case UdOperandCode.OP_R5:
                case UdOperandCode.OP_R6:
                case UdOperandCode.OP_R7:
                    this.DecodeReg(ref u, ref operand, RegClass.REGCLASS_GPR, (Byte)((BitOps.REX_B(u.Rex) << 3) | (type - UdOperandCode.OP_R0)), size);
                    break;
                case UdOperandCode.OP_AL:
                case UdOperandCode.OP_AX:
                case UdOperandCode.OP_eAX:
                case UdOperandCode.OP_rAX:
                    this.DecodeReg(ref u, ref operand, RegClass.REGCLASS_GPR, 0, size);
                    break;
                case UdOperandCode.OP_CL:
                case UdOperandCode.OP_CX:
                case UdOperandCode.OP_eCX:
                    this.DecodeReg(ref u, ref operand, RegClass.REGCLASS_GPR, 1, size);
                    break;
                case UdOperandCode.OP_DL:
                case UdOperandCode.OP_DX:
                case UdOperandCode.OP_eDX:
                    this.DecodeReg(ref u, ref operand, RegClass.REGCLASS_GPR, 2, size);
                    break;
                case UdOperandCode.OP_ES:
                case UdOperandCode.OP_CS:
                case UdOperandCode.OP_DS:
                case UdOperandCode.OP_SS:
                case UdOperandCode.OP_FS:
                case UdOperandCode.OP_GS:
                    /* in 64bits mode, only fs and gs are allowed */
                    if (u.DisMode == 64)
                    {
                        if (type != UdOperandCode.OP_FS && type != UdOperandCode.OP_GS)
                        {
                            u.Error = 1;
                            u.ErrorMessage = "invalid segment register in 64bits\n";
                        }
                    }

                    operand.UdType = UdType.UD_OP_REG;
                    operand.Base = (type - UdOperandCode.OP_ES) + UdType.UD_R_ES;
                    operand.Size = 16;
                    break;
                case UdOperandCode.OP_J:
                    this.DecodeImmediate(ref u, size, ref operand);
                    operand.UdType = UdType.UD_OP_JIMM;
                    break;
                case UdOperandCode.OP_R:
                    if (BitOps.MODRM_MOD(this.Modrm(ref u)) != 3)
                    {
                        u.Error = 1;
                        u.ErrorMessage = "expected modrm.mod == 3\n";
                    }

                    this.DecodeModrmRm(ref u, ref operand, RegClass.REGCLASS_GPR, size);
                    break;
                case UdOperandCode.OP_C:
                    this.DecodeModrmReg(ref u, ref operand, RegClass.REGCLASS_CR, size);
                    break;
                case UdOperandCode.OP_D:
                    this.DecodeModrmReg(ref u, ref operand, RegClass.REGCLASS_DB, size);
                    break;
                case UdOperandCode.OP_I3:
                    operand.UdType = UdType.UD_OP_CONST;
                    operand.Lval.SByte = 3;
                    break;
                case UdOperandCode.OP_ST0:
                case UdOperandCode.OP_ST1:
                case UdOperandCode.OP_ST2:
                case UdOperandCode.OP_ST3:
                case UdOperandCode.OP_ST4:
                case UdOperandCode.OP_ST5:
                case UdOperandCode.OP_ST6:
                case UdOperandCode.OP_ST7:
                    operand.UdType = UdType.UD_OP_REG;
                    operand.Base = (type - UdOperandCode.OP_ST0) + UdType.UD_R_ST0;
                    operand.Size = 80;
                    break;
                case UdOperandCode.OP_L:
                    this.DecodeVexImmreg(ref u, ref operand, size);
                    break;
                default:
                    operand.UdType = UdType.UD_NONE;
                    break;
            }

            return operand.UdType;
        }

        /// <summary>
        /// Disassemble upto 4 operands of the current instruction being disassembled. By the end of the function,
        /// the operand fields of the ud structure will have been filled.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 DecodeOperands(ref Ud u)
        {
            if (this.DecodeOperand(ref u, ref u.Operand[0], u.ItabEntry.Operand1.OperandType, u.ItabEntry.Operand1.Size) != UdType.UD_NONE)
            {
                if (this.DecodeOperand(ref u, ref u.Operand[1], u.ItabEntry.Operand2.OperandType, u.ItabEntry.Operand2.Size) != UdType.UD_NONE)
                {
                    if (this.DecodeOperand(ref u, ref u.Operand[2], u.ItabEntry.Operand3.OperandType, u.ItabEntry.Operand3.Size) != UdType.UD_NONE)
                    {
                        this.DecodeOperand(ref u, ref u.Operand[3], u.ItabEntry.Operand4.OperandType, u.ItabEntry.Operand4.Size);
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Clear instruction structure.
        /// </summary>
        /// <param name="u">TODO u.</param>
        private void ClearInstruction(ref Ud u)
        {
            u.Error = 0;
            u.PfxSeg = 0;
            u.PfxOpr = 0;
            u.PfxAdr = 0;
            u.PfxLock = 0;
            u.PfxRepne = 0;
            u.PfxRep = 0;
            u.PfxRepe = 0;
            u.PfxRex = 0;
            u.PfxStr = 0;
            u.Mnemonic = UdMnemonicCode.UD_Inone;
            u.ItabEntry = new UdItabEntry();
            u.HaveModrm = 0;
            u.BrFar = 0;
            u.VexOp = 0;
            u.Rex = 0;
            u.Operand[0] = new UdOperand();
            u.Operand[1] = new UdOperand();
            u.Operand[2] = new UdOperand();
            u.Operand[3] = new UdOperand();
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 ResolvePrefixStr(ref Ud u)
        {
            if (u.PfxStr == 0xf3)
            {
                if (BitOps.P_STR(u.ItabEntry.Prefix) > 0)
                {
                    u.PfxRep = 0xf3;
                }
                else
                {
                    u.PfxRepe = 0xf3;
                }
            }
            else if (u.PfxStr == 0xf2)
            {
                u.PfxRepne = 0xf3;
            }

            return 0;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 ResolveMode(ref Ud u)
        {
            Int32 default64;

            // If in error state, bail out
            if (u.Error == 1)
            {
                return -1;
            }

            // Propagate prefix effects
            if (u.DisMode == 64)
            {
                // Set 64bit-mode flags

                // Check validity of  instruction m64
                if (BitOps.P_INV64(u.ItabEntry.Prefix) > 0)
                {
                    u.Error = 1;
                    u.ErrorMessage = "instruction invalid in 64bits\n";
                    return -1;
                }

                /* compute effective rex based on,
                 *  - vex prefix (if any)
                 *  - rex prefix (if any, and not vex)
                 *  - allowed prefixes specified by the opcode map
                 */
                if (u.VexOp == 0xc4)
                {
                    // Vex has rex.rxb in 1's complement rex.0rxb / rex.w000
                    u.Rex = (Byte)((~(u.VexB1 >> 5) & 0x7) | ((u.VexB2 >> 4) & 0x8));
                }
                else if (u.VexOp == 0xc5)
                {
                    // Vex has rex.r in 1's complement
                    u.Rex = (Byte)((~(u.VexB1 >> 5)) & 4);
                }
                else
                {
                    Debug.Assert(u.VexOp == 0, "TODO: REASON");
                    u.Rex = u.PfxRex;
                }

                u.Rex &= (Byte)BitOps.REX_PFX_MASK(u.ItabEntry.Prefix);

                // Whether this instruction has a default operand size of 64bit, also hardcoded into the opcode map.
                default64 = (Int32)BitOps.P_DEF64(u.ItabEntry.Prefix);

                // Calculate effective operand size
                if (BitOps.REX_W(u.Rex) > 0)
                {
                    u.OprMode = 64;
                }
                else if (u.PfxOpr > 0)
                {
                    u.OprMode = 16;
                }
                else
                {
                    // Unless the default opr size of instruction is 64, the effective operand size in the absence of rex.w prefix is 32.
                    u.OprMode = (Byte)(default64 > 0 ? 64 : 32);
                }

                // Calculate effective address size
                u.AddressMode = (Byte)((u.PfxAdr > 0) ? 32 : 64);
            }
            else if (u.DisMode == 32)
            {
                // set 32bit-mode flags
                u.OprMode = (Byte)((u.PfxOpr > 0) ? 16 : 32);
                u.AddressMode = (Byte)((u.PfxAdr > 0) ? 16 : 32);
            }
            else if (u.DisMode == 16)
            {
                // set 16bit-mode flags
                u.OprMode = (Byte)((u.PfxOpr > 0) ? 32 : 16);
                u.AddressMode = (Byte)((u.PfxAdr > 0) ? 32 : 16);
            }

            return 0;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="ptr">TODO ptr.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 DecodeInstruction(ref Ud u, UInt16 ptr)
        {
            Debug.Assert((ptr & 0x8000) == 0, "TODO: REASON");
            u.ItabEntry = InstructionTables.ud_itab[ptr];
            u.Mnemonic = u.ItabEntry.Mnemonic;

            return (this.ResolvePrefixStr(ref u) == 0 && this.ResolveMode(ref u) == 0 && this.DecodeOperands(ref u) == 0 && this.ResolveMnemonic(ref u) == 0) ? 0 : -1;
        }

        /// <summary>
        /// Decoding 3dnow is a little tricky because of its strange opcode structure. The final opcode disambiguation depends on the last
        /// byte that comes after the operands have been decoded. Fortunately, all 3dnow instructions have the same set of operand types. So we
        /// go ahead and decode the instruction by picking an arbitrarily chosen valid entry in the table, decode the operands, and read the final byte to resolve the menmonic.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 Decode3dNow(ref Ud u)
        {
            UInt16 ptr;
            Debug.Assert(u.Le.TableType == UdTableType.UD_TAB__OPC_3DNOW, "TODO: REASON");
            Debug.Assert(u.Le.Table[0xc] != 0, "TODO: REASON");

            this.DecodeInstruction(ref u, u.Le.Table[0xc]);
            this.InpNext(ref u);
            if (u.Error > 0)
            {
                return -1;
            }

            ptr = u.Le.Table[this.InpCurr(ref u)];
            Debug.Assert((ptr & 0x8000) == 0, "TODO: REASON");
            u.Mnemonic = InstructionTables.ud_itab[ptr].Mnemonic;

            return 0;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 DecodeSsePrefix(ref Ud u)
        {
            Byte idx;
            Byte pfx;

            // String prefixes (f2, f3) take precedence over operand size prefix (66)
            pfx = u.PfxStr;
            if (pfx == 0)
            {
                pfx = u.PfxOpr;
            }

            idx = (Byte)(((pfx & 0xf) + 1) / 2);
            if (u.Le.Table[idx] == 0)
            {
                idx = 0;
            }

            if (idx > 0 && u.Le.Table[idx] != 0)
            {
                // "Consume" the prefix as a part of the opcode, so it is no longer exported as an instruction prefix
                u.PfxStr = 0;
                if (pfx == 0x66)
                {
                    // Consume "66" only if it was used for decoding, leaving it to be used as an operands size override for some simd instructions
                    u.PfxOpr = 0;
                }
            }

            return this.DecodeExtension(ref u, u.Le.Table[idx]);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 DecodeVex(ref Ud u)
        {
            Byte index;
            if (u.DisMode != 64 && BitOps.MODRM_MOD((Byte)this.InpPeek(ref u)) != 0x3)
            {
                index = 0;
            }
            else
            {
                u.VexOp = (Byte)this.InpCurr(ref u);
                u.VexB1 = (Byte)this.InpNext(ref u);

                if (u.VexOp == 0xc4)
                {
                    Byte pp, m;

                    // 3-byte vex
                    u.VexB2 = this.InpNext(ref u);
                    if (u.Error != 0)
                    {
                        return u.Error;
                    }

                    m = (Byte)(u.VexB1 & 0x1f);
                    if (m == 0 || m > 3)
                    {
                        u.Error = 1;
                        u.ErrorMessage = "decode-error: reserved vex.m-mmmm value";
                    }

                    pp = (Byte)(u.VexB2 & 0x3);
                    index = (Byte)((pp << 2) | m);
                }
                else
                {
                    // 2-byte vex
                    Debug.Assert(u.VexOp == 0xc5, "TODO: REASON");
                    index = (Byte)(0x1 | ((u.VexB1 & 0x3) << 2));
                }
            }

            return this.DecodeExtension(ref u, u.Le.Table[index]);
        }

        /// <summary>
        /// Decode opcode extensions (if any).
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="ptr">TODO ptr.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 DecodeExtension(ref Ud u, UInt16 ptr)
        {
            Byte idx = 0;
            if ((ptr & 0x8000) == 0)
            {
                return this.DecodeInstruction(ref u, ptr);
            }

            u.Le = InstructionTables.ud_lookup_table_list[(~0x8000 & ptr)];
            if (u.Le.TableType == UdTableType.UD_TAB__OPC_3DNOW)
            {
                return this.Decode3dNow(ref u);
            }

            switch (u.Le.TableType)
            {
                case UdTableType.UD_TAB__OPC_MOD:
                    // !11 = 0, 11 = 1
                    idx = (Byte)((BitOps.MODRM_MOD(this.Modrm(ref u)) + 1) / 4);
                    break;
                case UdTableType.UD_TAB__OPC_MODE:
                    // Disassembly mode/operand size/address size based tables. 16 = 0, 32 = 1, 64 = 2
                    idx = (Byte)(u.DisMode != 64 ? 0 : 1);
                    break;
                case UdTableType.UD_TAB__OPC_OSIZE:
                    idx = (Byte)(this.EffOprMode(u.DisMode, BitOps.REX_W(u.PfxRex), u.PfxOpr) / 32);
                    break;
                case UdTableType.UD_TAB__OPC_ASIZE:
                    idx = (Byte)(this.EffAdrMode(u.DisMode, u.PfxAdr) / 32);
                    break;
                case UdTableType.UD_TAB__OPC_X87:
                    idx = (Byte)(this.Modrm(ref u) - 0xC0);
                    break;
                case UdTableType.UD_TAB__OPC_VENDOR:
                    if (u.Vendor == UdVendorAny)
                    {
                        /* choose a valid entry */
                        idx = (Byte)((u.Le.Table[idx] != 0) ? 0 : 1);
                    }
                    else if (u.Vendor == UdVendorAmd)
                    {
                        idx = 0;
                    }
                    else
                    {
                        idx = 1;
                    }

                    break;
                case UdTableType.UD_TAB__OPC_RM:
                    idx = BitOps.MODRM_RM(this.Modrm(ref u));
                    break;
                case UdTableType.UD_TAB__OPC_REG:
                    idx = BitOps.MODRM_REG(this.Modrm(ref u));
                    break;
                case UdTableType.UD_TAB__OPC_SSE:
                    return this.DecodeSsePrefix(ref u);
                case UdTableType.UD_TAB__OPC_VEX:
                    return this.DecodeVex(ref u);
                case UdTableType.UD_TAB__OPC_VEX_W:
                    idx = this.VexW(ref u);
                    break;
                case UdTableType.UD_TAB__OPC_VEX_L:
                    idx = this.VexL(ref u);
                    break;
                case UdTableType.UD_TAB__OPC_TABLE:
                    this.InpNext(ref u);
                    return this.DecodeOpcode(ref u);
                default:
                    Debug.Assert(false, "not reached");
                    break;
            }

            return this.DecodeExtension(ref u, u.Le.Table[idx]);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <returns>TODO TODO.</returns>
        private Int32 DecodeOpcode(ref Ud u)
        {
            UInt16 ptr;
            Debug.Assert(u.Le.TableType == UdTableType.UD_TAB__OPC_TABLE, "TODO: REASON");
            if (u.Error != 0)
            {
                return u.Error;
            }

            ptr = u.Le.Table[this.InpCurr(ref u)];

            return this.DecodeExtension(ref u, ptr);
        }
    }
    //// End class
}
//// End namespace