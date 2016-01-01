// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\syn.cs)
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace SharpDisasm.Udis86
{
    #pragma warning disable 1591
    public class syn
    {
        public static readonly string[] ud_reg_tab = new string[] {
            "al",   "cl",   "dl",   "bl",
            "ah",   "ch",   "dh",   "bh",
            "spl",  "bpl",    "sil",    "dil",
            "r8b",  "r9b",    "r10b",   "r11b",
            "r12b", "r13b",   "r14b",   "r15b",

            "ax",   "cx",   "dx",   "bx",
            "sp",   "bp",   "si",   "di",
            "r8w",  "r9w",    "r10w",   "r11w",
            "r12w", "r13w"  , "r14w",   "r15w",
  
            "eax",  "ecx",    "edx",    "ebx",
            "esp",  "ebp",    "esi",    "edi",
            "r8d",  "r9d",    "r10d",   "r11d",
            "r12d", "r13d",   "r14d",   "r15d",
  
            "rax",  "rcx",    "rdx",    "rbx",
            "rsp",  "rbp",    "rsi",    "rdi",
            "r8",   "r9",   "r10",    "r11",
            "r12",  "r13",    "r14",    "r15",

            "es",   "cs",   "ss",   "ds",
            "fs",   "gs", 

            "cr0",  "cr1",    "cr2",    "cr3",
            "cr4",  "cr5",    "cr6",    "cr7",
            "cr8",  "cr9",    "cr10",   "cr11",
            "cr12", "cr13",   "cr14",   "cr15",
  
            "dr0",  "dr1",    "dr2",    "dr3",
            "dr4",  "dr5",    "dr6",    "dr7",
            "dr8",  "dr9",    "dr10",   "dr11",
            "dr12", "dr13",   "dr14",   "dr15",

            "mm0",  "mm1",    "mm2",    "mm3",
            "mm4",  "mm5",    "mm6",    "mm7",

            "st0",  "st1",    "st2",    "st3",
            "st4",  "st5",    "st6",    "st7", 

            "xmm0", "xmm1",   "xmm2",   "xmm3",
            "xmm4", "xmm5",   "xmm6",   "xmm7",
            "xmm8", "xmm9",   "xmm10",  "xmm11",
            "xmm12",  "xmm13",  "xmm14",  "xmm15",

            "ymm0", "ymm1", "ymm2",   "ymm3",
            "ymm4", "ymm5", "ymm6",   "ymm7",
            "ymm8", "ymm9", "ymm10",  "ymm11",
            "ymm12", "ymm13", "ymm14", "ymm15",

            "rip"
        };

        public ulong ud_syn_rel_target(ref ud u, ref ud_operand opr)
        {
            ulong trunc_mask = 0xffffffffffffffff >> (64 - u.opr_mode);
            switch (opr.size)
            {
                case 8: return (u.pc + (ulong)opr.lval.@sbyte) & trunc_mask;
                case 16: return (u.pc + (ulong)opr.lval.sword) & trunc_mask;
                case 32: return (u.pc + (ulong)opr.lval.sdword) & trunc_mask;
                default: Debug.Assert(false, "invalid relative offset size.");
                    return 0;
            }
        }

        /*
         * asmprintf
         *    Printf style function for printing translated assembly
         *    output. Returns the number of characters written and
         *    moves the buffer pointer forward. On an overflow,
         *    returns a negative number and truncates the output.
         */
        public static int ud_asmprintf(ref ud u, string fmt, params object[] args)
        {
            int ret;
            int avail;

            avail = u.asm_buf_size - u.asm_buf_fill - 1 /* nullchar */;
            var str = String.Format(fmt, args).ToArray();
            Array.Copy(str, 0, u.asm_buf, u.asm_buf_fill, Math.Min(str.Length, avail));
            ret = str.Length;
            //ret = vsnprintf((char*)u.asm_buf + u.asm_buf_fill, avail, fmt, ap);
            if (ret < 0 || ret > avail)
            {
                u.asm_buf_fill = u.asm_buf_size - 1;
                ret = -1;
            }
            else
            {
                u.asm_buf_fill += ret;
            }
            return ret;
        }


        public void ud_syn_print_addr(ref ud u, long addr)
        {
            string name = null;
            if (u.sym_resolver != null)
            {
                long offset = 0;
                name = u.sym_resolver(ref u, addr, ref offset);
                if (!String.IsNullOrEmpty(name))
                {
                    if (offset > 0)
                    {
                        ud_asmprintf(ref u, "{0}{1:+#;-#}", name, offset);
                    }
                    else
                    {
                        ud_asmprintf(ref u, "{0}", name);
                    }
                    return;
                }
            }
            ud_asmprintf(ref u, "0x{0:x}", addr);
        }


        public void ud_syn_print_imm(ref ud u, ref ud_operand op)
        {
            ulong v;
            if (op._oprcode == ud_operand_code.OP_sI && op.size != u.opr_mode)
            {
                if (op.size == 8)
                {
                    v = (ulong)op.lval.@sbyte;
                }
                else
                {
                    Debug.Assert(op.size == 32);
                    v = (ulong)op.lval.sdword;
                }
                if (u.opr_mode < 64)
                {
                    v = v & ((1ul << u.opr_mode) - 1ul);
                }
            }
            else
            {
                switch (op.size)
                {
                    case 8: v = op.lval.ubyte; break;
                    case 16: v = op.lval.uword; break;
                    case 32: v = op.lval.udword; break;
                    case 64: v = op.lval.uqword; break;
                    default: Debug.Assert(false, "invalid offset"); v = 0; /* keep cc happy */
                        break;
                }
            }
            ud_asmprintf(ref u, "0x{0:x}", v);
        }


        public void ud_syn_print_mem_disp(ref ud u, ref ud_operand op, int sign)
        {
            Debug.Assert(op.offset != 0);
            if (op.@base == ud_type.UD_NONE && op.index == ud_type.UD_NONE)
            {
                ulong v;
                Debug.Assert(op.scale == 0 && op.offset != 8);
                /* unsigned mem-offset */
                switch (op.offset)
                {
                    case 16: v = op.lval.uword; break;
                    case 32: v = op.lval.udword; break;
                    case 64: v = op.lval.uqword; break;
                    default: Debug.Assert(false, "invalid offset"); v = 0; /* keep cc happy */
                        break;
                }
                ud_asmprintf(ref u, "0x{0:x}", v);
            }
            else
            {
                long v;
                Debug.Assert(op.offset != 64);
                switch (op.offset)
                {
                    case 8: v = op.lval.@sbyte; break;
                    case 16: v = op.lval.sword; break;
                    case 32: v = op.lval.sdword; break;
                    default: Debug.Assert(false, "invalid offset"); v = 0; /* keep cc happy */
                        break;
                }
                if (v < 0)
                {
                    ud_asmprintf(ref u, "-0x{0:x}", -v);
                }
                else if (v > 0)
                {
                    ud_asmprintf(ref u, "{0}0x{1:x}", sign > 0 ? "+" : "", v);
                }
            }
        }
    }
    #pragma warning restore 1591
}
