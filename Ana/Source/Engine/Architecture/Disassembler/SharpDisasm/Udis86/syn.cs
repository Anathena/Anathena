namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// TODO TODO.
    /// </summary>
    internal class Syn
    {
        /// <summary>
        /// TODO TODO.
        /// </summary>
        public static readonly String[] UdRegTab = new String[]
        {
            "al", "cl", "dl", "bl",
            "ah", "ch", "dh", "bh",
            "spl", "bpl", "sil", "dil",
            "r8b", "r9b", "r10b", "r11b",
            "r12b", "r13b", "r14b", "r15b",

            "ax", "cx", "dx", "bx",
            "sp", "bp", "si", "di",
            "r8w", "r9w", "r10w", "r11w",
            "r12w", "r13w", "r14w", "r15w",

            "eax", "ecx", "edx", "ebx",
            "esp", "ebp", "esi", "edi",
            "r8d", "r9d", "r10d", "r11d",
            "r12d", "r13d", "r14d", "r15d",

            "rax", "rcx", "rdx", "rbx",
            "rsp", "rbp", "rsi", "rdi",
            "r8", "r9", "r10", "r11",
            "r12", "r13", "r14", "r15",

            "es", "cs", "ss", "ds",
            "fs", "gs",

            "cr0", "cr1", "cr2", "cr3",
            "cr4", "cr5", "cr6", "cr7",
            "cr8", "cr9", "cr10", "cr11",
            "cr12", "cr13", "cr14", "cr15",

            "dr0", "dr1", "dr2", "dr3",
            "dr4", "dr5", "dr6", "dr7",
            "dr8", "dr9", "dr10", "dr11",
            "dr12", "dr13", "dr14", "dr15",

            "mm0", "mm1", "mm2", "mm3",
            "mm4", "mm5", "mm6", "mm7",

            "st0", "st1", "st2", "st3",
            "st4", "st5", "st6", "st7",

            "xmm0", "xmm1", "xmm2", "xmm3",
            "xmm4", "xmm5", "xmm6", "xmm7",
            "xmm8", "xmm9", "xmm10", "xmm11",
            "xmm12", "xmm13", "xmm14", "xmm15",

            "ymm0", "ymm1", "ymm2", "ymm3",
            "ymm4", "ymm5", "ymm6", "ymm7",
            "ymm8", "ymm9", "ymm10", "ymm11",
            "ymm12", "ymm13", "ymm14", "ymm15",

            "rip"
        };

        /// <summary>
        /// Printf style function for printing translated assembly output. 
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="fmt">TODO fmt.</param>
        /// <param name="args">TODO args.</param>
        /// <returns>
        /// Returns the number of characters written and moves the buffer pointer forward. On an overflow, returns a negative number and truncates the output.
        /// </returns>
        public static Int32 UdAsmPrintf(ref Ud u, String fmt, params Object[] args)
        {
            Int32 ret;
            Int32 avail;

            avail = u.AsmBufferSize - u.AsmBufferFill - 1 /* nullchar */;
            Char[] str = String.Format(fmt, args).ToArray();
            Array.Copy(str, 0, u.AsmBuffer, u.AsmBufferFill, Math.Min(str.Length, avail));
            ret = str.Length;
            //// ret = vsnprintf((char*)u.asm_buf + u.asm_buf_fill, avail, fmt, ap);

            if (ret < 0 || ret > avail)
            {
                u.AsmBufferFill = u.AsmBufferSize - 1;
                ret = -1;
            }
            else
            {
                u.AsmBufferFill += ret;
            }

            return ret;
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="opr">TODO opr.</param>
        /// <returns>TODO TODO.</returns>
        public UInt64 UdSynRelTarget(ref Ud u, ref UdOperand opr)
        {
            UInt64 trunc_mask = 0xffffffffffffffff >> (64 - u.OprMode);
            switch (opr.Size)
            {
                case 8:
                    return (u.Pc + (UInt64)opr.Lval.SByte) & trunc_mask;
                case 16:
                    return (u.Pc + (UInt64)opr.Lval.SWord) & trunc_mask;
                case 32:
                    return (u.Pc + (UInt64)opr.Lval.SdWord) & trunc_mask;
                default:
                    Debug.Assert(false, "invalid relative offset size.");
                    return 0;
            }
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="addr">TODO addr.</param>
        public void UdSynPrintAddr(ref Ud u, Int64 addr)
        {
            String name = null;

            if (u.SymResolver != null)
            {
                Int64 offset = 0;
                name = u.SymResolver(ref u, addr, ref offset);
                if (!String.IsNullOrEmpty(name))
                {
                    if (offset > 0)
                    {
                        UdAsmPrintf(ref u, "{0}{1:+#;-#}", name, offset);
                    }
                    else
                    {
                        UdAsmPrintf(ref u, "{0}", name);
                    }

                    return;
                }
            }

            UdAsmPrintf(ref u, "0x{0:x}", addr);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="op">TODO op.</param>
        public void UdSynPrintImm(ref Ud u, ref UdOperand op)
        {
            UInt64 v;
            if (op.OperandCode == UdOperandCode.OP_sI && op.Size != u.OprMode)
            {
                if (op.Size == 8)
                {
                    v = (UInt64)op.Lval.SByte;
                }
                else
                {
                    Debug.Assert(op.Size == 32, "TODO: REASON");
                    v = (UInt64)op.Lval.SdWord;
                }

                if (u.OprMode < 64)
                {
                    v = v & ((1ul << u.OprMode) - 1ul);
                }
            }
            else
            {
                switch (op.Size)
                {
                    case 8:
                        v = op.Lval.UByte;
                        break;
                    case 16:
                        v = op.Lval.UWord;
                        break;
                    case 32:
                        v = op.Lval.UdWord;
                        break;
                    case 64:
                        v = op.Lval.UqWord;
                        break;
                    default:
                        Debug.Assert(false, "invalid offset");
                        v = 0; // keep cc happy
                        break;
                }
            }

            UdAsmPrintf(ref u, "0x{0:x}", v);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="u">TODO u.</param>
        /// <param name="op">TODO op.</param>
        /// <param name="sign">TODO sign.</param>
        public void UdSynPrintMemDisp(ref Ud u, ref UdOperand op, Int32 sign)
        {
            Debug.Assert(op.Offset != 0, "TODO: REASON");
            if (op.Base == UdType.UD_NONE && op.Index == UdType.UD_NONE)
            {
                UInt64 v;
                Debug.Assert(op.Scale == 0 && op.Offset != 8, "TODO: REASON");

                // unsigned mem-offset
                switch (op.Offset)
                {
                    case 16:
                        v = op.Lval.UWord;
                        break;
                    case 32:
                        v = op.Lval.UdWord;
                        break;
                    case 64:
                        v = op.Lval.UqWord;
                        break;
                    default:
                        Debug.Assert(false, "invalid offset");
                        v = 0; // keep cc happy
                        break;
                }

                UdAsmPrintf(ref u, "0x{0:x}", v);
            }
            else
            {
                Int64 v;
                Debug.Assert(op.Offset != 64, "TODO: REASON");
                switch (op.Offset)
                {
                    case 8:
                        v = op.Lval.SByte;
                        break;
                    case 16:
                        v = op.Lval.SWord;
                        break;
                    case 32:
                        v = op.Lval.SdWord;
                        break;
                    default:
                        Debug.Assert(false, "invalid offset");
                        v = 0; // keep cc happy
                        break;
                }

                if (v < 0)
                {
                    UdAsmPrintf(ref u, "-0x{0:x}", -v);
                }
                else if (v > 0)
                {
                    UdAsmPrintf(ref u, "{0}0x{1:x}", sign > 0 ? "+" : String.Empty, v);
                }
            }
        }
    }
    //// End enum
}
//// End namespace