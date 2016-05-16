// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\udis86.cs)
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
using System.Text;

namespace SharpDisasm.Udis86
{
    /// <summary>
    /// <para>The C# udis86 library ported from C.</para>
    /// <para>For more information about how to use the C-based library see the udis86 project on GitHub https://github.com/vmt/udis86 </para>
    /// <para>This static class is thread safe ONLY WHEN using separate <see cref="ud"/> instances.</para>
    /// </summary>
    /// <remarks>This class is deliberately written to match as closely as possible to the original C-library.</remarks>
    public unsafe static class udis86
    {
        static Decode decode = new Decode();

        /* =============================================================================
         * ud_init
         *    Initializes ud_t object.
         * =============================================================================
         */
        /// <summary>
        /// Initializes ud_t object.
        /// </summary>
        /// <param name="u"></param>
        public static void
        ud_init(ref ud u)
        {
            u = new ud();
            ud_set_mode(ref u, 16);
            u.mnemonic = ud_mnemonic_code.UD_Iinvalid;
            ud_set_pc(ref u, 0);
            //#ifndef __UD_STANDALONE__
            //  ud_set_input_file(u, stdin);
            //#endif /* __UD_STANDALONE__ */

            ud_set_asm_buffer(ref u, u.asm_buf_int, u.asm_buf_int.Length);
        }


        /* =============================================================================
         * ud_disassemble
         *    Disassembles one instruction and returns the number of 
         *    bytes disassembled. A zero means end of disassembly.
         * =============================================================================
         */
        /// <summary>
        /// Disassembles one instruction and returns the number of bytes disassembled. A zero means end of disassembly.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int
        ud_disassemble(ref ud u)
        {
            int len;
            if (u.inp_end > 0)
            {
                return 0;
            }
            if ((len = decode.ud_decode(ref u)) > 0)
            {
                if (u.translator != null)
                {
                    for (var i = 0; i < u.asm_buf.Length; i++)
                        u.asm_buf[i] = '\0';
                    u.translator(ref u);
                }
            }
            return len;
        }


        /* =============================================================================
         * ud_set_mode() - Set Disassemly Mode.
         * =============================================================================
         */
        /// <summary>
        /// Set Disassembly mode
        /// </summary>
        /// <param name="u"></param>
        /// <param name="m"></param>
        public static void ud_set_mode(ref ud u, byte m)
        {
            switch (m)
            {
                case 16:
                case 32:
                case 64: u.dis_mode = m; return;
                default: u.dis_mode = 16; return;
            }
        }

        /* =============================================================================
         * ud_set_vendor() - Set vendor.
         * =============================================================================
         */
        /// <summary>
        /// Set vendor.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        public static void
        ud_set_vendor(ref ud u, int v)
        {
            switch (v)
            {
                case Decode.UD_VENDOR_INTEL:
                    u.vendor = Decode.UD_VENDOR_INTEL;
                    break;
                case Decode.UD_VENDOR_ANY:
                    u.vendor = Decode.UD_VENDOR_ANY;
                    break;
                default:
                    u.vendor = Decode.UD_VENDOR_AMD;
                    break;
            }
        }

        /* =============================================================================
         * ud_set_pc() - Sets code origin. 
         * =============================================================================
         */
        /// <summary>
        /// Set code origin address
        /// </summary>
        /// <param name="u"></param>
        /// <param name="o"></param>
        public static void
        ud_set_pc(ref ud u, ulong o)
        {
            u.pc = o;
        }

        /* =============================================================================
         * ud_set_syntax() - Sets the output syntax.
         * =============================================================================
         */
        /// <summary>
        /// Sets the output syntax
        /// </summary>
        /// <param name="u"></param>
        /// <param name="t"></param>
        public static void
        ud_set_syntax(ref ud u, UdTranslatorDelegate t)
        {
            u.translator = t;
        }

        /* =============================================================================
         * ud_insn() - returns the disassembled instruction
         * =============================================================================
         */
        /// <summary>
        /// returns the disassembled instruction
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static string ud_insn_asm(ref ud u)
        {
            if (u.asm_buf == null || u.asm_buf.Length == 0)
                return String.Empty;

            int count = Array.IndexOf<char>(u.asm_buf, '\0', 0);
            if (count < 0) count = u.asm_buf.Length;
            
            char[] c = new char[count];
            Array.Copy(u.asm_buf, c, count);

            return new String(c);
        }

        /* =============================================================================
         * ud_insn_offset() - Returns the offset.
         * =============================================================================
         */
        static ulong
        ud_insn_off(ref ud u)
        {
            return u.insn_offset;
        }


        /* =============================================================================
         * ud_insn_hex() - Returns hex form of disassembled instruction.
         * =============================================================================
         */
        static string
        ud_insn_hex(ref ud u)
        {
            //u.insn_hexcode[0] = '\0';
            StringBuilder src_hex = new StringBuilder();
            if (u.error == 0)
            {
                uint i;
                IntPtr src_ptr = ud_insn_ptr(ref u);
                unsafe 
                {
                    byte* src = (byte*)src_ptr.ToPointer();
                    for (i = 0; i < ud_insn_len(ref u); i++)
                    {
                        src_hex.AppendFormat("{0:2X", src[i]);
                    }
                }
                //byte[] src_ptr = ud_insn_ptr(ref u);
                // //char[] src_hex;
                // /* for each byte used to decode instruction */
                //for (i = 0; i < src_ptr.Length; // && i < u.insn_hexcode.Length / 2;
                //     ++i)
                //{
                //    src_hex.Append(String.Format("{0:2x}", src_ptr[i] & 0xFF));
                //}
            }
            return src_hex.ToString();
        }


        /* =============================================================================
         * ud_insn_ptr
         *    Returns a pointer to buffer containing the bytes that were
         *    disassembled.
         * =============================================================================
         */
        /// <summary>
        /// Returns a pointer to buffer containing the bytes that were disassembled.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        unsafe static IntPtr
        ud_insn_ptr(ref ud u)
        {
            return (u.inp_buf == null) ? u._inputSessionPinner : new IntPtr(u.inp_buf + u.inp_buf_index - u.inp_ctr);
        }


        /* =============================================================================
         * ud_insn_len
         *    Returns the count of bytes disassembled.
         * =============================================================================
         */
        /// <summary>
        /// Returns the count of bytes disassembled.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int ud_insn_len(ref ud u)
        {
            return u.inp_ctr;
        }


        /* =============================================================================
         * ud_insn_get_opr
         *    Return the operand struct representing the nth operand of
         *    the currently disassembled instruction. Returns NULL if
         *    there's no such operand.
         * =============================================================================
         */
        /// <summary>
        /// Return the operand struct representing the nth operand of
        /// the currently disassembled instruction. Returns NULL if
        /// there's no such operand.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="n"></param>
        /// <param name="op"></param>
        public static void
        ud_insn_opr(ref ud u, int n, out ud_operand? op)
        {
            if (n > 3 || u.operand[n].type == ud_type.UD_NONE)
            {
                op = null;
            }
            else
            {
                op = u.operand[n];
            }
        }


        /* =============================================================================
         * ud_opr_is_sreg
         *    Returns non-zero if the given operand is of a segment register type.
         * =============================================================================
         */
        /// <summary>
        /// Returns true if the given operand is of a segment register type.
        /// </summary>
        /// <param name="opr"></param>
        /// <returns></returns>
        public static bool
        ud_opr_is_sreg(ref ud_operand opr)
        {
            return opr.type == ud_type.UD_OP_REG &&
                   opr.@base >= ud_type.UD_R_ES &&
                   opr.@base <= ud_type.UD_R_GS;
        }


        /* =============================================================================
         * ud_opr_is_sreg
         *    Returns non-zero if the given operand is of a general purpose
         *    register type.
         * =============================================================================
         */
        /// <summary>
        /// Returns true if the given operand is of a general purpose
        /// register type.
        /// </summary>
        /// <param name="opr"></param>
        /// <returns></returns>
        public static bool
        ud_opr_is_gpr(ref ud_operand opr)
        {
            return opr.type == ud_type.UD_OP_REG &&
                   opr.@base >= ud_type.UD_R_AL &&
                   opr.@base <= ud_type.UD_R_R15;
        }


        /* =============================================================================
         * ud_set_user_opaque_data
         * ud_get_user_opaque_data
         *    Get/set user opaqute data pointer
         * =============================================================================
         */
        static void
        ud_set_user_opaque_data(ref ud u, IntPtr opaque)
        {
            u.user_opaque_data = opaque;
        }

        static IntPtr
        ud_get_user_opaque_data(ref ud u)
        {
            return u.user_opaque_data;
        }


        /* =============================================================================
         * ud_set_asm_buffer
         *    Allow the user to set an assembler output buffer. If `buf` is NULL,
         *    we switch back to the internal buffer.
         * =============================================================================
         */
        static void ud_set_asm_buffer(ref ud u, char[] buf, int size)
        {
            if (buf == null)
            {
                ud_set_asm_buffer(ref u, u.asm_buf_int, u.asm_buf_int.Length);
            }
            else
            {
                u.asm_buf = buf;
                u.asm_buf_size = size;
            }
        }


        /* =============================================================================
         * ud_set_sym_resolver
         *    Set symbol resolver for relative targets used in the translation
         *    phase.
         *
         *    The resolver is a function that takes a ulong address and returns a
         *    symbolic name for the that address. The function also takes a second
         *    argument pointing to an integer that the client can optionally set to a
         *    non-zero value for offsetted targets. (symbol+offset) The function may
         *    also return NULL, in which case the translator only prints the target
         *    address.
         *
         *    The function pointer maybe NULL which resets symbol resolution.
         * =============================================================================
         */
        static void
        ud_set_sym_resolver(ref ud u, UdSymbolResolverDelegate resolver)
        {
            u.sym_resolver = resolver;
        }


        /* =============================================================================
         * ud_insn_mnemonic
         *    Return the current instruction mnemonic.
         * =============================================================================
         */
        static ud_mnemonic_code
        ud_insn_mnemonic(ref ud u)
        {
            return u.mnemonic;
        }


        /* =============================================================================
         * ud_lookup_mnemonic
         *    Looks up mnemonic code in the mnemonic string table.
         *    Returns NULL if the mnemonic code is invalid.
         * =============================================================================
         */
        /// <summary>
        /// Looks up mnemonic code in the mnemonic string table.
        /// Returns NULL if the mnemonic code is invalid.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string
        ud_lookup_mnemonic(ud_mnemonic_code c)
        {
            if (c < ud_mnemonic_code.UD_MAX_MNEMONIC_CODE)
            {
                return InstructionTables.ud_mnemonics_str[(int)c];
            }
            else
            {
                return null;
            }
        }


        /* 
         * ud_inp_init
         *    Initializes the input system.
         */
        static void
        ud_inp_init(ref ud u)
        {
            u.inp_hook = null;
            u.inp_buf = null;
            u.inp_buf_size = 0;
            u.inp_buf_index = 0;
            u.inp_curr = 0;
            u.inp_ctr = 0;
            u.inp_end = 0;
            u.inp_peek = Decode.UD_EOI;
            //UD_NON_STANDALONE(u.inp_file = NULL);
        }


        /* =============================================================================
         * ud_inp_set_hook
         *    Sets input hook.
         * =============================================================================
         */
        static void
        ud_set_input_hook(ref ud u, UdInputCallback hook)
        {
            ud_inp_init(ref u);
            u.inp_hook = hook;
        }

        /* =============================================================================
         * ud_inp_set_buffer
         *    Set buffer as input.
         * =============================================================================
         */
        /// <summary>
        /// Set the buffer as input
        /// </summary>
        /// <param name="u"></param>
        /// <param name="buf">Pointer to memory to be read from</param>
        /// <param name="len">The maximum amount of memory to be read</param>
        public static unsafe void ud_set_input_buffer(ref ud u, IntPtr buf, int len)
        {
            ud_inp_init(ref u);
            u.inp_buf = (byte*)buf.ToPointer();
            u.inp_buf_size = len;
            u.inp_buf_index = 0;
        }

        //#ifndef __UD_STANDALONE__
        /* =============================================================================
         * ud_input_set_file
         *    Set FILE as input.
         * =============================================================================
         */
        static int 
        inp_file_hook(ref ud u)
        {
          return u.inp_file.ReadByte();// fgetc(u.inp_file);
        }

        /// <summary>
        /// Set file as input for disassembly.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="file">File stream that will be read from. The stream must support reading.</param>
        public static void 
        ud_set_input_file(ref ud u, System.IO.FileStream file)
        {
          ud_inp_init(ref u);
          u.inp_hook = inp_file_hook;
          u.inp_file = file;
        }
        //#endif /* __UD_STANDALONE__ */


        /* =============================================================================
         * ud_input_skip
         *    Skip n input bytes.
         * ============================================================================
         */
        static void ud_input_skip(ref ud u, int n)
        {
            if (u.inp_end > 0)
            {
                return;
            }
            if (u.inp_buf == null)
            {
                while (n-- > 0)
                {
                    int c = u.inp_hook(ref u);
                    if (c == Decode.UD_EOI)
                    {
                        goto eoi;
                    }
                }
                return;
            }
            else
            {
                if (n > u.inp_buf_size ||
                    u.inp_buf_index > u.inp_buf_size - n)
                {
                    u.inp_buf_index = u.inp_buf_size;
                    goto eoi;
                }
                u.inp_buf_index += n;
                return;
            }
        eoi:
            u.inp_end = 1;
            u.error = 1;
            u.errorMessage = "cannot skip, eoi received\b";
            return;
        }


        /* =============================================================================
         * ud_input_end
         *    Returns non-zero on end-of-input.
         * =============================================================================
         */
        static int ud_input_end(ref ud u)
        {
            return u.inp_end;
        }
    }
}
