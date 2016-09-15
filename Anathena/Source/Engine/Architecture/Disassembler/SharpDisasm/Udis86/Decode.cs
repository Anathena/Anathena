namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;
    using System.Diagnostics;

    internal class Decode
    {
        public const Int32 MAX_INSN_LENGTH = 15;
        public const Int32 MAX_PREFIXES = 15;
        public const Int32 UD_EOI = -1;
        public const Int32 UD_INP_CACHE_SZ = 32;
        public const Int32 UD_VENDOR_AMD = 0;
        public const Int32 UD_VENDOR_INTEL = 1;
        public const Int32 UD_VENDOR_ANY = 2;

        /* Boolean ud_opcode_field_sext(byte primary_opcode)
        * {
        *     return (primary_opcode & 0x02) != 0;
        * }
        */

        /* 
        * inp_start
        *    Should be called before each de-code operation.
        */
        void inp_start(ref ud u)
        {
            u.inp_ctr = 0;
        }

        unsafe int inp_peek(ref ud u)
        {
            if (u.inp_end == 0)
            {
                if (u.inp_buf != null)
                {
                    if (u.inp_buf_index < u.inp_buf_size)
                    {
                        return u.inp_buf[u.inp_buf_index];
                    }
                }
                else if (u.inp_peek != UD_EOI)
                {
                    return u.inp_peek;
                }
                else
                {
                    int c;
                    if ((c = u.inp_hook(ref u)) != UD_EOI)
                    {
                        u.inp_peek = c;
                        return u.inp_peek;
                    }
                }
            }
            u.inp_end = 1;
            u.error = 1;
            u.errorMessage = "byte expected, eoi received";
            return 0;
        }

        unsafe byte inp_next(ref ud u)
        {
            if (u.inp_end == 0)
            {
                if (u.inp_buf != null)
                {
                    if (u.inp_buf_index < u.inp_buf_size)
                    {
                        u.inp_ctr++;
                        return (u.inp_curr = u.inp_buf[u.inp_buf_index++]);
                    }
                }
                else
                {
                    int c = u.inp_peek;
                    if ((c = u.inp_hook(ref u)) != UD_EOI)
                    {
                        u.inp_peek = UD_EOI;
                        u.inp_curr = (byte)c;
                        u.inp_sess[u.inp_ctr++] = u.inp_curr;
                        return u.inp_curr;
                    }
                }
            }
            u.inp_end = 1;
            u.error = 1;
            u.errorMessage = "byte expected, eoi received\n";
            return 0;
        }

        byte inp_curr(ref ud ud)
        {
            return ud.inp_curr;
        }

        /*
         * inp_uint8
         * inp_uint16
         * inp_uint32
         * inp_uint64
         *    Load little-endian values from input
         */
        byte inp_uint8(ref ud u)
        {
            return inp_next(ref u);
        }

        ushort inp_uint16(ref ud u)
        {
            ushort r, ret;

            ret = (ushort)inp_next(ref u);
            r = (ushort)inp_next(ref u);
            return (ushort)(ret | (r << 8));
        }

        UInt32
        inp_uint32(ref ud u)
        {
            UInt32 r, ret;

            ret = (UInt32)inp_next(ref u);
            r = (UInt32)inp_next(ref u);
            ret = ret | (r << 8);
            r = (UInt32)inp_next(ref u);
            ret = ret | (r << 16);
            r = (UInt32)inp_next(ref u);
            return ret | (r << 24);
        }

        ulong
        inp_uint64(ref ud u)
        {
            ulong r, ret;

            ret = inp_next(ref u);
            r = inp_next(ref u);
            ret = ret | (r << 8);
            r = inp_next(ref u);
            ret = ret | (r << 16);
            r = inp_next(ref u);
            ret = ret | (r << 24);
            r = inp_next(ref u);
            ret = ret | (r << 32);
            r = inp_next(ref u);
            ret = ret | (r << 40);
            r = inp_next(ref u);
            ret = ret | (r << 48);
            r = inp_next(ref u);
            return ret | (r << 56);
        }

        int eff_opr_mode(int dis_mode, int rex_w, int pfx_opr)
        {
            if (dis_mode == 64)
            {
                return rex_w > 0 ? 64 : (pfx_opr > 0 ? 16 : 32);
            }
            else if (dis_mode == 32)
            {
                return pfx_opr > 0 ? 16 : 32;
            }
            else
            {
                Debug.Assert(dis_mode == 16);
                return pfx_opr > 0 ? 32 : 16;
            }
        }

        int eff_adr_mode(int dis_mode, int pfx_adr)
        {
            if (dis_mode == 64)
            {
                return pfx_adr > 0 ? 32 : 64;
            }
            else if (dis_mode == 32)
            {
                return pfx_adr > 0 ? 16 : 32;
            }
            else
            {
                Debug.Assert(dis_mode == 16);
                return pfx_adr > 0 ? 32 : 16;
            }
        }


        /* 
         * decode_prefixes
         *
         *  Extracts instruction prefixes.
         */
        int
        decode_prefixes(ref ud u)
        {
            bool done = false;
            byte curr = 0;
            byte last = 0;
            if (u.error != 0) return u.error;

            do
            {
                last = curr;
                curr = inp_next(ref u);
                if (u.error != 0) return u.error;
                if (u.inp_ctr == MAX_INSN_LENGTH)
                {
                    u.error = 1;
                    u.errorMessage = "max instruction length";
                    return u.error;
                }

                switch (curr)
                {
                    case 0x2E:
                        u.pfx_seg = (byte)ud_type.UD_R_CS;
                        break;
                    case 0x36:
                        u.pfx_seg = (byte)ud_type.UD_R_SS;
                        break;
                    case 0x3E:
                        u.pfx_seg = (byte)ud_type.UD_R_DS;
                        break;
                    case 0x26:
                        u.pfx_seg = (byte)ud_type.UD_R_ES;
                        break;
                    case 0x64:
                        u.pfx_seg = (byte)ud_type.UD_R_FS;
                        break;
                    case 0x65:
                        u.pfx_seg = (byte)ud_type.UD_R_GS;
                        break;
                    case 0x67: /* adress-size override prefix */
                        u.pfx_adr = 0x67;
                        break;
                    case 0xF0:
                        u.pfx_lock = 0xF0;
                        break;
                    case 0x66:
                        u.pfx_opr = 0x66;
                        break;
                    case 0xF2:
                        u.pfx_str = 0xf2;
                        break;
                    case 0xF3:
                        u.pfx_str = 0xf3;
                        break;
                    default:
                        /* consume if rex */
                        done = (u.dis_mode == 64 && (curr & 0xF0) == 0x40) ? false : true;
                        break;
                }
            } while (!done);
            /* rex prefixes in 64bit mode, must be the last prefix */
            if (u.dis_mode == 64 && (last & 0xF0) == 0x40)
            {
                u.pfx_rex = last;
            }
            return 0;
        }

        byte vex_l(ref ud u)
        {
            Debug.Assert(u.vex_op != 0);
            return (byte)(((u.vex_op == 0xc4 ? u.vex_b2 : u.vex_b1) >> 2) & 1);
        }
        byte vex_w(ref ud u)
        {
            Debug.Assert(u.vex_op != 0);
            return (byte)(u.vex_op == 0xc4 ? ((u.vex_b2 >> 7) & 1) : 0);
        }

        byte modrm(ref ud u)
        {
            if (u.have_modrm == 0)
            {
                u.modrm = inp_next(ref u);
                u.modrm_offset = (byte)(u.inp_ctr - 1);
                u.have_modrm = 1;
            }
            return u.modrm;
        }

        ud_operand_size resolve_operand_size(ref ud u, ud_operand_size oSize)
        {
            switch (oSize)
            {
                case ud_operand_size.SZ_V:
                    return (ud_operand_size)u.opr_mode;
                case ud_operand_size.SZ_Z:
                    return (ud_operand_size)((u.opr_mode == 16) ? 16 : 32);
                case ud_operand_size.SZ_Y:
                    return (ud_operand_size)((u.opr_mode == 16) ? 32 : u.opr_mode);
                case ud_operand_size.SZ_RDQ:
                    return (ud_operand_size)((u.dis_mode == 64) ? 64 : 32);
                case ud_operand_size.SZ_X:
                    Debug.Assert(u.vex_op != 0);
                    return (BitOps.P_VEXL(u.itab_entry.Prefix) > 0 && vex_l(ref u) > 0) ? ud_operand_size.SZ_QQ : ud_operand_size.SZ_DQ;
                default:
                    return oSize;
            }
        }

        int resolve_mnemonic(ref ud u)
        {
            /* resolve 3dnow weirdness. */
            if (u.mnemonic == ud_mnemonic_code.UD_I3dnow)
            {
                u.mnemonic = InstructionTables.ud_itab[u.le.Table[inp_curr(ref u)]].Mnemonic;
            }
            /* SWAPGS is only valid in 64bits mode */
            if (u.mnemonic == ud_mnemonic_code.UD_Iswapgs && u.dis_mode != 64)
            {
                u.error = 1;
                u.errorMessage = "swapgs invalid in 64bits mode\n";
                return -1;
            }

            if (u.mnemonic == ud_mnemonic_code.UD_Ixchg)
            {
                if ((u.operand[0].type == ud_type.UD_OP_REG && u.operand[0].@base == ud_type.UD_R_AX &&
                     u.operand[1].type == ud_type.UD_OP_REG && u.operand[1].@base == ud_type.UD_R_AX) ||
                    (u.operand[0].type == ud_type.UD_OP_REG && u.operand[0].@base == ud_type.UD_R_EAX &&
                     u.operand[1].type == ud_type.UD_OP_REG && u.operand[1].@base == ud_type.UD_R_EAX))
                {
                    u.operand[0].type = ud_type.UD_NONE;
                    u.operand[1].type = ud_type.UD_NONE;
                    u.mnemonic = ud_mnemonic_code.UD_Inop;
                }
            }

            if (u.mnemonic == ud_mnemonic_code.UD_Inop && u.pfx_repe != 0)
            {
                u.pfx_repe = 0;
                u.mnemonic = ud_mnemonic_code.UD_Ipause;
            }
            return 0;
        }


        /* -----------------------------------------------------------------------------
         * decode_a()- Decodes operands of the type seg:offset
         * -----------------------------------------------------------------------------
         */
        void decode_a(ref ud u, ref ud_operand op)
        {
            if (u.opr_mode == 16)
            {
                /* seg16:off16 */
                op.type = ud_type.UD_OP_PTR;
                op.size = 32;
                op.lval.ptr_off = inp_uint16(ref u);
                op.lval.ptr_seg = inp_uint16(ref u);
            }
            else
            {
                /* seg16:off32 */
                op.type = ud_type.UD_OP_PTR;
                op.size = 48;
                op.lval.ptr_off = inp_uint32(ref u);
                op.lval.ptr_seg = inp_uint16(ref u);
            }
        }

        /* -----------------------------------------------------------------------------
         * decode_gpr() - Returns decoded General Purpose Register 
         * -----------------------------------------------------------------------------
         */
        ud_type decode_gpr(ref ud u, ud_operand_size s, byte rm)
        {
            switch ((int)s)
            {
                case 64:
                    return ud_type.UD_R_RAX + rm;
                case 32:
                    return ud_type.UD_R_EAX + rm;
                case 16:
                    return ud_type.UD_R_AX + rm;
                case 8:
                    if (u.dis_mode == 64 && u.pfx_rex != 0)
                    {
                        if (rm >= 4)
                            return ud_type.UD_R_SPL + (rm - 4);
                        return ud_type.UD_R_AL + rm;
                    }
                    else return ud_type.UD_R_AL + rm;
                case 0:
                    /* invalid size in case of a decode error */
                    Debug.Assert(u.error == 0, "invalid operand size");
                    return ud_type.UD_NONE;
                default:
                    Debug.Assert(false, "invalid operand size");
                    return ud_type.UD_NONE;
            }
        }

        void
        decode_reg(ref ud u,
                   ref ud_operand opr,
                   reg_class type,
                   byte num,
                   ud_operand_size size)
        {
            int reg;
            size = resolve_operand_size(ref u, size);
            switch (type)
            {
                case reg_class.REGCLASS_GPR: reg = (int)decode_gpr(ref u, size, (byte)num); break;
                case reg_class.REGCLASS_MMX: reg = (int)ud_type.UD_R_MM0 + (num & 7); break;
                case reg_class.REGCLASS_XMM: reg = num + (int)(size == ud_operand_size.SZ_QQ ? ud_type.UD_R_YMM0 : ud_type.UD_R_XMM0); break;
                case reg_class.REGCLASS_CR: reg = (int)ud_type.UD_R_CR0 + num; break;
                case reg_class.REGCLASS_DB: reg = (int)ud_type.UD_R_DR0 + num; break;
                case reg_class.REGCLASS_SEG:
                    {
                        /*
                         * Only 6 segment registers, anything else is an error.
                         */
                        if ((num & 7) > 5)
                        {
                            u.error = 1;
                            u.errorMessage = "invalid segment register value\n";
                            return;
                        }
                        else
                        {
                            reg = (int)ud_type.UD_R_ES + (num & 7);
                        }
                        break;
                    }
                default:
                    Debug.Assert(false, "invalid register type");
                    return;
            }
            opr.type = ud_type.UD_OP_REG;
            opr.@base = (ud_type)reg;
            opr.size = (byte)size;
        }


        /*
         * decode_imm 
         *
         *    Decode Immediate values.
         */
        void
        decode_imm(ref ud u, ud_operand_size size, ref ud_operand op)
        {
            op.size = (byte)resolve_operand_size(ref u, size);
            op.type = ud_type.UD_OP_IMM;

            switch (op.size)
            {
                case 8: op.lval.@sbyte = (sbyte)inp_uint8(ref u); break;
                case 16: op.lval.uword = inp_uint16(ref u); break;
                case 32: op.lval.udword = inp_uint32(ref u); break;
                case 64: op.lval.uqword = inp_uint64(ref u); break;
                default: return;
            }
        }


        /* 
         * decode_mem_disp
         *
         *    Decode mem address displacement.
         */
        void
        decode_mem_disp(ref ud u, int size, ref ud_operand op)
        {
            switch (size)
            {
                case 8:
                    op.offset = 8;
                    op.lval.ubyte = inp_uint8(ref u);
                    break;
                case 16:
                    op.offset = 16;
                    op.lval.uword = inp_uint16(ref u);
                    break;
                case 32:
                    op.offset = 32;
                    op.lval.udword = inp_uint32(ref u);
                    break;
                case 64:
                    op.offset = 64;
                    op.lval.uqword = inp_uint64(ref u);
                    break;
                default:
                    return;
            }
        }


        /*
         * decode_modrm_reg
         *
         *    Decodes reg field of mod/rm byte
         * 
         */
        void decode_modrm_reg(ref ud u,
                         ref ud_operand operand,
                         reg_class type,
                         ud_operand_size size)
        {
            byte reg = (byte)((BitOps.REX_R(u._rex) << 3) | BitOps.MODRM_REG(modrm(ref u)));
            decode_reg(ref u, ref operand, type, reg, size);
        }


        /*
         * decode_modrm_rm
         *
         *    Decodes rm field of mod/rm byte
         * 
         */
        void
        decode_modrm_rm(ref ud u,
                        ref ud_operand op,
                        reg_class type,    /* register type */
                        ud_operand_size size)    /* operand size */
        {
            int offset = 0;
            byte mod, rm;

            /* get mod, r/m and reg fields */
            mod = BitOps.MODRM_MOD(modrm(ref u));
            rm = (byte)((BitOps.REX_B(u._rex) << 3) | BitOps.MODRM_RM(modrm(ref u)));

            /* 
             * If mod is 11b, then the modrm.rm specifies a register.
             *
             */
            if (mod == 3)
            {
                decode_reg(ref u, ref op, type, rm, size);
                return;
            }

            /* 
             * !11b => Memory Address
             */
            op.type = ud_type.UD_OP_MEM;
            op.size = (byte)resolve_operand_size(ref u, size);

            if (u.adr_mode == 64)
            {
                op.@base = ud_type.UD_R_RAX + rm;
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
                    op.@base = ud_type.UD_R_RIP;
                    offset = 32;
                }
                else
                {
                    offset = 0;
                }
                /* 
                 * Scale-Index-Base (SIB) 
                 */
                if ((rm & 7) == 4)
                {
                    inp_next(ref u);

                    op.@base = ud_type.UD_R_RAX + (BitOps.SIB_B(inp_curr(ref u)) | (BitOps.REX_B(u._rex) << 3));
                    op.index = ud_type.UD_R_RAX + (BitOps.SIB_I(inp_curr(ref u)) | (BitOps.REX_X(u._rex) << 3));

                    /* special conditions for base reference */
                    if (op.index == ud_type.UD_R_RSP)
                    {
                        op.index = ud_type.UD_NONE;
                        op.scale = (byte)ud_type.UD_NONE;
                    }
                    else
                    {
                        op.scale = (byte)((1 << BitOps.SIB_S(inp_curr(ref u))) & ~1);
                    }

                    if (op.@base == ud_type.UD_R_RBP || op.@base == ud_type.UD_R_R13)
                    {
                        if (mod == 0)
                        {
                            op.@base = ud_type.UD_NONE;
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
                    op.scale = 0;
                    op.index = ud_type.UD_NONE;
                }
            }
            else if (u.adr_mode == 32)
            {
                op.@base = ud_type.UD_R_EAX + rm;
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
                    op.@base = ud_type.UD_NONE;
                    offset = 32;
                }
                else
                {
                    offset = 0;
                }

                /* Scale-Index-Base (SIB) */
                if ((rm & 7) == 4)
                {
                    inp_next(ref u);

                    op.scale = (byte)((1 << BitOps.SIB_S(inp_curr(ref u))) & ~1);
                    op.index = ud_type.UD_R_EAX + (BitOps.SIB_I(inp_curr(ref u)) | (BitOps.REX_X(u.pfx_rex) << 3));
                    op.@base = ud_type.UD_R_EAX + (BitOps.SIB_B(inp_curr(ref u)) | (BitOps.REX_B(u.pfx_rex) << 3));

                    if (op.index == ud_type.UD_R_ESP)
                    {
                        op.index = ud_type.UD_NONE;
                        op.scale = (byte)ud_type.UD_NONE;
                    }

                    /* special condition for base reference */
                    if (op.@base == ud_type.UD_R_EBP)
                    {
                        if (mod == 0)
                        {
                            op.@base = ud_type.UD_NONE;
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
                    op.scale = 0;
                    op.index = ud_type.UD_NONE;
                }
            }
            else
            {
                ud_type[] bases = { ud_type.UD_R_BX, ud_type.UD_R_BX, ud_type.UD_R_BP, ud_type.UD_R_BP,
                                             ud_type.UD_R_SI, ud_type.UD_R_DI, ud_type.UD_R_BP, ud_type.UD_R_BX };
                ud_type[] indices = { ud_type.UD_R_SI, ud_type.UD_R_DI, ud_type.UD_R_SI, ud_type.UD_R_DI,
                                             ud_type.UD_NONE, ud_type.UD_NONE, ud_type.UD_NONE, ud_type.UD_NONE };
                op.@base = bases[rm & 7];
                op.index = indices[rm & 7];
                op.scale = 0;
                if (mod == 0 && rm == 6)
                {
                    offset = 16;
                    op.@base = ud_type.UD_NONE;
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
                decode_mem_disp(ref u, offset, ref op);
            }
            else
            {
                op.offset = 0;
            }
        }


        /* 
         * decode_moffset
         *    Decode offset-only memory operand
         */
        void
        decode_moffset(ref ud u, ud_operand_size size, ref ud_operand opr)
        {
            opr.type = ud_type.UD_OP_MEM;
            opr.@base = ud_type.UD_NONE;
            opr.index = ud_type.UD_NONE;
            opr.scale = 0;
            opr.size = (byte)resolve_operand_size(ref u, size);
            decode_mem_disp(ref u, u.adr_mode, ref opr);
        }

        void
        decode_vex_vvvv(ref ud u, ref ud_operand opr, ud_operand_size size)
        {
            byte vvvv;
            Debug.Assert(u.vex_op != 0);
            vvvv = (byte)(((u.vex_op == 0xc4 ? u.vex_b2 : u.vex_b1) >> 3) & 0xf);
            decode_reg(ref u, ref opr, reg_class.REGCLASS_XMM, (byte)(0xf & ~vvvv), size);
        }

        /* 
         * decode_vex_immreg
         *    Decode source operand encoded in immediate byte [7:4]
         */
        int
        decode_vex_immreg(ref ud u, ref ud_operand opr, ud_operand_size size)
        {
            byte imm = (byte)inp_next(ref u);
            byte mask = (byte)(u.dis_mode == 64 ? 0xf : 0x7);
            if (u.error != 0) return u.error;
            Debug.Assert(u.vex_op != 0);
            decode_reg(ref u, ref opr, reg_class.REGCLASS_XMM, (byte)(mask & (imm >> 4)), size);
            return 0;
        }

        ud_operand_size Mx_mem_size(int size)
        {
            return (ud_operand_size)((size >> 8) & 0xff);
        }

        ud_operand_size Mx_reg_size(int size)
        {
            return (ud_operand_size)(size & 0xff);
        }

        /* -----------------------------------------------------------------------------
         * decode_operands() - Disassembles Operands.
         * -----------------------------------------------------------------------------
         */
        ud_type
        decode_operand(ref ud u,
                       ref ud_operand operand,
                       ud_operand_code type,
                       ud_operand_size size)
        {
            operand.type = ud_type.UD_NONE;
            operand._oprcode = type;

            switch (type)
            {
                case ud_operand_code.OP_A:
                    decode_a(ref u, ref operand);
                    break;
                case ud_operand_code.OP_MR:
                    decode_modrm_rm(ref u, ref operand, (byte)reg_class.REGCLASS_GPR,
                                    BitOps.MODRM_MOD(modrm(ref u)) == 3 ?
                                      size.Mx_reg_size() : size.Mx_mem_size());
                    break;
                case ud_operand_code.OP_F:
                    u.br_far = 1;
                    if (BitOps.MODRM_MOD(modrm(ref u)) == 3)
                    {
                        u.error = 1;
                        u.errorMessage = "expected modrm.mod != 3\n";
                    }
                    decode_modrm_rm(ref u, ref operand, reg_class.REGCLASS_GPR, size);
                    break;
                case ud_operand_code.OP_M:
                    if (BitOps.MODRM_MOD(modrm(ref u)) == 3)
                    {
                        u.error = 1;
                        u.errorMessage = "expected modrm.mod != 3\n";
                    }
                    decode_modrm_rm(ref u, ref operand, reg_class.REGCLASS_GPR, size);
                    break;
                case ud_operand_code.OP_E:
                    decode_modrm_rm(ref u, ref operand, reg_class.REGCLASS_GPR, size);
                    break;
                case ud_operand_code.OP_G:
                    decode_modrm_reg(ref u, ref operand, reg_class.REGCLASS_GPR, size);
                    break;
                case ud_operand_code.OP_sI:
                case ud_operand_code.OP_I:
                    decode_imm(ref u, size, ref operand);
                    break;
                case ud_operand_code.OP_I1:
                    operand.type = ud_type.UD_OP_CONST;
                    operand.lval.udword = 1;
                    break;
                case ud_operand_code.OP_N:
                    if (BitOps.MODRM_MOD(modrm(ref u)) != 3)
                    {
                        u.error = 1;
                        u.errorMessage = "expected modrm.mod == 3\n";
                    }
                    decode_modrm_rm(ref u, ref operand, reg_class.REGCLASS_MMX, size);
                    break;
                case ud_operand_code.OP_Q:
                    decode_modrm_rm(ref u, ref operand, reg_class.REGCLASS_MMX, size);
                    break;
                case ud_operand_code.OP_P:
                    decode_modrm_reg(ref u, ref operand, reg_class.REGCLASS_MMX, size);
                    break;
                case ud_operand_code.OP_U:
                    if (BitOps.MODRM_MOD(modrm(ref u)) != 3)
                    {
                        u.error = 1;
                        u.errorMessage = "expected modrm.mod == 3\n";
                    }
                    decode_modrm_rm(ref u, ref operand, reg_class.REGCLASS_XMM, size);
                    break;
                case ud_operand_code.OP_W:
                    decode_modrm_rm(ref u, ref operand, reg_class.REGCLASS_XMM, size);
                    break;
                case ud_operand_code.OP_V:
                    decode_modrm_reg(ref u, ref operand, reg_class.REGCLASS_XMM, size);
                    break;
                case ud_operand_code.OP_H:
                    decode_vex_vvvv(ref u, ref operand, size);
                    break;
                case ud_operand_code.OP_MU:
                    decode_modrm_rm(ref u, ref operand, reg_class.REGCLASS_XMM,
                                    BitOps.MODRM_MOD(modrm(ref u)) == 3 ?
                                      size.Mx_reg_size() : size.Mx_mem_size());
                    break;
                case ud_operand_code.OP_S:
                    decode_modrm_reg(ref u, ref operand, reg_class.REGCLASS_SEG, size);
                    break;
                case ud_operand_code.OP_O:
                    decode_moffset(ref u, size, ref operand);
                    break;
                case ud_operand_code.OP_R0:
                case ud_operand_code.OP_R1:
                case ud_operand_code.OP_R2:
                case ud_operand_code.OP_R3:
                case ud_operand_code.OP_R4:
                case ud_operand_code.OP_R5:
                case ud_operand_code.OP_R6:
                case ud_operand_code.OP_R7:
                    decode_reg(ref u, ref operand, reg_class.REGCLASS_GPR,
                               (byte)((BitOps.REX_B(u._rex) << 3) | (type - ud_operand_code.OP_R0)), size);
                    break;
                case ud_operand_code.OP_AL:
                case ud_operand_code.OP_AX:
                case ud_operand_code.OP_eAX:
                case ud_operand_code.OP_rAX:
                    decode_reg(ref u, ref operand, reg_class.REGCLASS_GPR, 0, size);
                    break;
                case ud_operand_code.OP_CL:
                case ud_operand_code.OP_CX:
                case ud_operand_code.OP_eCX:
                    decode_reg(ref u, ref operand, reg_class.REGCLASS_GPR, 1, size);
                    break;
                case ud_operand_code.OP_DL:
                case ud_operand_code.OP_DX:
                case ud_operand_code.OP_eDX:
                    decode_reg(ref u, ref operand, reg_class.REGCLASS_GPR, 2, size);
                    break;
                case ud_operand_code.OP_ES:
                case ud_operand_code.OP_CS:
                case ud_operand_code.OP_DS:
                case ud_operand_code.OP_SS:
                case ud_operand_code.OP_FS:
                case ud_operand_code.OP_GS:
                    /* in 64bits mode, only fs and gs are allowed */
                    if (u.dis_mode == 64)
                    {
                        if (type != ud_operand_code.OP_FS && type != ud_operand_code.OP_GS)
                        {
                            u.error = 1;
                            u.errorMessage = "invalid segment register in 64bits\n";
                        }
                    }
                    operand.type = ud_type.UD_OP_REG;
                    operand.@base = (type - ud_operand_code.OP_ES) + ud_type.UD_R_ES;
                    operand.size = 16;
                    break;
                case ud_operand_code.OP_J:
                    decode_imm(ref u, size, ref operand);
                    operand.type = ud_type.UD_OP_JIMM;
                    break;
                case ud_operand_code.OP_R:
                    if (BitOps.MODRM_MOD(modrm(ref u)) != 3)
                    {
                        u.error = 1;
                        u.errorMessage = "expected modrm.mod == 3\n";
                    }
                    decode_modrm_rm(ref u, ref operand, reg_class.REGCLASS_GPR, size);
                    break;
                case ud_operand_code.OP_C:
                    decode_modrm_reg(ref u, ref operand, reg_class.REGCLASS_CR, size);
                    break;
                case ud_operand_code.OP_D:
                    decode_modrm_reg(ref u, ref operand, reg_class.REGCLASS_DB, size);
                    break;
                case ud_operand_code.OP_I3:
                    operand.type = ud_type.UD_OP_CONST;
                    operand.lval.@sbyte = 3;
                    break;
                case ud_operand_code.OP_ST0:
                case ud_operand_code.OP_ST1:
                case ud_operand_code.OP_ST2:
                case ud_operand_code.OP_ST3:
                case ud_operand_code.OP_ST4:
                case ud_operand_code.OP_ST5:
                case ud_operand_code.OP_ST6:
                case ud_operand_code.OP_ST7:
                    operand.type = ud_type.UD_OP_REG;
                    operand.@base = (type - ud_operand_code.OP_ST0) + ud_type.UD_R_ST0;
                    operand.size = 80;
                    break;
                case ud_operand_code.OP_L:
                    decode_vex_immreg(ref u, ref operand, size);
                    break;
                default:
                    operand.type = ud_type.UD_NONE;
                    break;
            }
            return operand.type;
        }


        /* 
         * decode_operands
         *
         *    Disassemble upto 4 operands of the current instruction being
         *    disassembled. By the end of the function, the operand fields
         *    of the ud structure will have been filled.
         */
        int
        decode_operands(ref ud u)
        {
            if (decode_operand(ref u, ref u.operand[0],
                              u.itab_entry.Operand1.type,
                              u.itab_entry.Operand1.size) != ud_type.UD_NONE)
            {
                if (decode_operand(ref u, ref u.operand[1],
                                  u.itab_entry.Operand2.type,
                                  u.itab_entry.Operand2.size) != ud_type.UD_NONE)
                {
                    if (decode_operand(ref u, ref u.operand[2],
                                      u.itab_entry.Operand3.type,
                                      u.itab_entry.Operand3.size) != ud_type.UD_NONE)
                    {
                        decode_operand(ref u, ref u.operand[3],
                            u.itab_entry.Operand4.type,
                            u.itab_entry.Operand4.size);
                    }
                }
            }
            return 0;
        }

        /* -----------------------------------------------------------------------------
         * clear_insn() - clear instruction structure
         * -----------------------------------------------------------------------------
         */
        void clear_insn(ref ud u)
        {
            u.error = 0;
            u.pfx_seg = 0;
            u.pfx_opr = 0;
            u.pfx_adr = 0;
            u.pfx_lock = 0;
            u.pfx_repne = 0;
            u.pfx_rep = 0;
            u.pfx_repe = 0;
            u.pfx_rex = 0;
            u.pfx_str = 0;
            u.mnemonic = ud_mnemonic_code.UD_Inone;
            u.itab_entry = new ud_itab_entry();
            u.have_modrm = 0;
            u.br_far = 0;
            u.vex_op = 0;
            u._rex = 0;
            u.operand[0] = new ud_operand();
            u.operand[1] = new ud_operand();
            u.operand[2] = new ud_operand();
            u.operand[3] = new ud_operand();
            //memset( &u.operand[ 0 ], 0, sizeof( ud_operand ) );
            //memset( &u.operand[ 1 ], 0, sizeof( ud_operand ) );
            //memset( &u.operand[ 2 ], 0, sizeof( ud_operand ) );
        }


        int
        resolve_pfx_str(ref ud u)
        {
            if (u.pfx_str == 0xf3)
            {
                if (BitOps.P_STR(u.itab_entry.Prefix) > 0)
                {
                    u.pfx_rep = 0xf3;
                }
                else
                {
                    u.pfx_repe = 0xf3;
                }
            }
            else if (u.pfx_str == 0xf2)
            {
                u.pfx_repne = 0xf3;
            }
            return 0;
        }


        int
        resolve_mode(ref ud u)
        {
            int default64;
            /* if in error state, bail out */
            if (u.error == 1) return -1;

            /* propagate prefix effects */
            if (u.dis_mode == 64)
            {  /* set 64bit-mode flags */

                /* Check validity of  instruction m64 */
                if (BitOps.P_INV64(u.itab_entry.Prefix) > 0)
                {
                    u.error = 1;
                    u.errorMessage = "instruction invalid in 64bits\n";
                    return -1;
                }

                /* compute effective rex based on,
                 *  - vex prefix (if any)
                 *  - rex prefix (if any, and not vex)
                 *  - allowed prefixes specified by the opcode map
                 */
                if (u.vex_op == 0xc4)
                {
                    /* vex has rex.rxb in 1's complement */
                    u._rex = (byte)((~(u.vex_b1 >> 5) & 0x7) /* rex.0rxb */ |
                               ((u.vex_b2 >> 4) & 0x8) /* rex.w000 */);
                }
                else if (u.vex_op == 0xc5)
                {
                    /* vex has rex.r in 1's complement */
                    u._rex = (byte)((~(u.vex_b1 >> 5)) & 4);
                }
                else
                {
                    Debug.Assert(u.vex_op == 0);
                    u._rex = u.pfx_rex;
                }
                u._rex &= (byte)BitOps.REX_PFX_MASK(u.itab_entry.Prefix);

                /* whether this instruction has a default operand size of 
                 * 64bit, also hardcoded into the opcode map.
                 */
                default64 = (int)BitOps.P_DEF64(u.itab_entry.Prefix);
                /* calculate effective operand size */
                if (BitOps.REX_W(u._rex) > 0)
                {
                    u.opr_mode = 64;
                }
                else if (u.pfx_opr > 0)
                {
                    u.opr_mode = 16;
                }
                else
                {
                    /* unless the default opr size of instruction is 64,
                     * the effective operand size in the absence of rex.w
                     * prefix is 32.
                     */
                    u.opr_mode = (byte)(default64 > 0 ? 64 : 32);
                }

                /* calculate effective address size */
                u.adr_mode = (byte)((u.pfx_adr > 0) ? 32 : 64);
            }
            else if (u.dis_mode == 32)
            { /* set 32bit-mode flags */
                u.opr_mode = (byte)((u.pfx_opr > 0) ? 16 : 32);
                u.adr_mode = (byte)((u.pfx_adr > 0) ? 16 : 32);
            }
            else if (u.dis_mode == 16)
            { /* set 16bit-mode flags */
                u.opr_mode = (byte)((u.pfx_opr > 0) ? 32 : 16);
                u.adr_mode = (byte)((u.pfx_adr > 0) ? 32 : 16);
            }

            return 0;
        }


        int decode_insn(ref ud u, ushort ptr)
        {
            Debug.Assert((ptr & 0x8000) == 0);
            u.itab_entry = InstructionTables.ud_itab[ptr];
            u.mnemonic = u.itab_entry.Mnemonic;
            return (resolve_pfx_str(ref u) == 0 &&
                    resolve_mode(ref u) == 0 &&
                    decode_operands(ref u) == 0 &&
                    resolve_mnemonic(ref u) == 0) ? 0 : -1;
        }


        /*
         * decode_3dnow()
         *
         *    Decoding 3dnow is a little tricky because of its strange opcode
         *    structure. The final opcode disambiguation depends on the last
         *    byte that comes after the operands have been decoded. Fortunately,
         *    all 3dnow instructions have the same set of operand types. So we
         *    go ahead and decode the instruction by picking an arbitrarily chosen
         *    valid entry in the table, decode the operands, and read the final
         *    byte to resolve the menmonic.
         */
        int
        decode_3dnow(ref ud u)
        {
            ushort ptr;
            Debug.Assert(u.le.Type == ud_table_type.UD_TAB__OPC_3DNOW);
            Debug.Assert(u.le.Table[0xc] != 0);
            decode_insn(ref u, u.le.Table[0xc]);
            inp_next(ref u);
            if (u.error > 0)
            {
                return -1;
            }
            ptr = u.le.Table[inp_curr(ref u)];
            Debug.Assert((ptr & 0x8000) == 0);
            u.mnemonic = InstructionTables.ud_itab[ptr].Mnemonic;
            return 0;
        }


        int
        decode_ssepfx(ref ud u)
        {
            byte idx;
            byte pfx;

            /*
             * String prefixes (f2, f3) take precedence over operand
             * size prefix (66).
             */
            pfx = u.pfx_str;
            if (pfx == 0)
            {
                pfx = u.pfx_opr;
            }
            idx = (byte)(((pfx & 0xf) + 1) / 2);
            if (u.le.Table[idx] == 0)
            {
                idx = 0;
            }
            if (idx > 0 && u.le.Table[idx] != 0)
            {
                /*
                 * "Consume" the prefix as a part of the opcode, so it is no
                 * longer exported as an instruction prefix.
                 */
                u.pfx_str = 0;
                if (pfx == 0x66)
                {
                    /* 
                     * consume "66" only if it was used for decoding, leaving
                     * it to be used as an operands size override for some
                     * simd instructions.
                     */
                    u.pfx_opr = 0;
                }
            }
            return decode_ext(ref u, u.le.Table[idx]);
        }


        int
        decode_vex(ref ud u)
        {
            byte index;
            if (u.dis_mode != 64 && BitOps.MODRM_MOD((byte)inp_peek(ref u)) != 0x3)
            {
                index = 0;
            }
            else
            {
                u.vex_op = (byte)inp_curr(ref u);
                u.vex_b1 = (byte)inp_next(ref u);
                if (u.vex_op == 0xc4)
                {
                    byte pp, m;
                    /* 3-byte vex */
                    u.vex_b2 = inp_next(ref u);
                    if (u.error != 0) return u.error;

                    m = (byte)(u.vex_b1 & 0x1f);
                    if (m == 0 || m > 3)
                    {
                        u.error = 1;
                        u.errorMessage = "decode-error: reserved vex.m-mmmm value";
                    }
                    pp = (byte)(u.vex_b2 & 0x3);
                    index = (byte)((pp << 2) | m);
                }
                else
                {
                    /* 2-byte vex */
                    Debug.Assert(u.vex_op == 0xc5);
                    index = (byte)(0x1 | ((u.vex_b1 & 0x3) << 2));
                }
            }
            return decode_ext(ref u, u.le.Table[index]);
        }


        /*
         * decode_ext()
         *
         *    Decode opcode extensions (if any)
         */
        int decode_ext(ref ud u, ushort ptr)
        {
            byte idx = 0;
            if ((ptr & 0x8000) == 0)
            {
                return decode_insn(ref u, ptr);
            }
            u.le = InstructionTables.ud_lookup_table_list[(~0x8000 & ptr)];
            if (u.le.Type == ud_table_type.UD_TAB__OPC_3DNOW)
            {
                return decode_3dnow(ref u);
            }

            switch (u.le.Type)
            {
                case ud_table_type.UD_TAB__OPC_MOD:
                    /* !11 = 0, 11 = 1 */
                    idx = (byte)((BitOps.MODRM_MOD(modrm(ref u)) + 1) / 4);
                    break;
                /* disassembly mode/operand size/address size based tables.
                 * 16 = 0,, 32 = 1, 64 = 2
                 */
                case ud_table_type.UD_TAB__OPC_MODE:
                    idx = (byte)(u.dis_mode != 64 ? 0 : 1);
                    break;
                case ud_table_type.UD_TAB__OPC_OSIZE:
                    idx = (byte)(eff_opr_mode(u.dis_mode, BitOps.REX_W(u.pfx_rex), u.pfx_opr) / 32);
                    break;
                case ud_table_type.UD_TAB__OPC_ASIZE:
                    idx = (byte)(eff_adr_mode(u.dis_mode, u.pfx_adr) / 32);
                    break;
                case ud_table_type.UD_TAB__OPC_X87:
                    idx = (byte)(modrm(ref u) - 0xC0);
                    break;
                case ud_table_type.UD_TAB__OPC_VENDOR:
                    if (u.vendor == UD_VENDOR_ANY)
                    {
                        /* choose a valid entry */
                        idx = (byte)((u.le.Table[idx] != 0) ? 0 : 1);
                    }
                    else if (u.vendor == UD_VENDOR_AMD)
                    {
                        idx = 0;
                    }
                    else
                    {
                        idx = 1;
                    }
                    break;
                case ud_table_type.UD_TAB__OPC_RM:
                    idx = BitOps.MODRM_RM(modrm(ref u));
                    break;
                case ud_table_type.UD_TAB__OPC_REG:
                    idx = BitOps.MODRM_REG(modrm(ref u));
                    break;
                case ud_table_type.UD_TAB__OPC_SSE:
                    return decode_ssepfx(ref u);
                case ud_table_type.UD_TAB__OPC_VEX:
                    return decode_vex(ref u);
                case ud_table_type.UD_TAB__OPC_VEX_W:
                    idx = vex_w(ref u);
                    break;
                case ud_table_type.UD_TAB__OPC_VEX_L:
                    idx = vex_l(ref u);
                    break;
                case ud_table_type.UD_TAB__OPC_TABLE:
                    inp_next(ref u);
                    return decode_opcode(ref u);
                default:
                    Debug.Assert(false, "not reached");
                    break;
            }

            return decode_ext(ref u, u.le.Table[idx]);
        }


        int
        decode_opcode(ref ud u)
        {
            ushort ptr;
            Debug.Assert(u.le.Type == ud_table_type.UD_TAB__OPC_TABLE);
            if (u.error != 0) return u.error;
            ptr = u.le.Table[inp_curr(ref u)];
            return decode_ext(ref u, ptr);
        }


        /* =============================================================================
         * ud_decode() - Instruction decoder. Returns the number of bytes decoded.
         * =============================================================================
         */
        public int ud_decode(ref ud u)
        {
            inp_start(ref u);
            clear_insn(ref u);
            u.le = InstructionTables.ud_lookup_table_list[0];
            u.error = (byte)((decode_prefixes(ref u) == -1 ||
                       decode_opcode(ref u) == -1 ||
                       u.error == 1) ? 1 : 0);
            /* Handle decode error. */
            if (u.error == 1)
            {
                /* clear out the decode data. */
                clear_insn(ref u);
                /* mark the sequence of bytes as invalid. */
                u.itab_entry = InstructionTables.ud_itab[0]; /* entry 0 is invalid */
                u.mnemonic = u.itab_entry.Mnemonic;
            }

            /* maybe this stray segment override byte
             * should be spewed out?
             */
            if (BitOps.P_SEG(u.itab_entry.Prefix) == 0 &&
                    u.operand[0].type != ud_type.UD_OP_MEM &&
                    u.operand[1].type != ud_type.UD_OP_MEM)
                u.pfx_seg = 0;

            u.insn_offset = u.pc; /* set offset of instruction */
            u.asm_buf_fill = 0;   /* set translation buffer index to 0 */
            u.pc += (uint)u.inp_ctr;    /* move program counter by bytes decoded */

            /* return number of bytes disassembled. */
            return u.inp_ctr;
        }
    }
    //// End class
}
//// End namespace