// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\ud.cs)
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
using System.Linq;
using System.Text;

#pragma warning disable 1591
namespace SharpDisasm.Udis86
{
    public delegate void UdTranslatorDelegate(ref ud ud);
    public delegate string UdSymbolResolverDelegate(ref ud ud, long addr, ref long offset);
    public delegate int UdInputCallback(ref ud ud);

    public sealed unsafe class ud : IDisposable
    {
  /*
   * input buffering
   */
        //public int (*inp_hook) (struct ud*);
        public UdInputCallback inp_hook;

        /// <summary>
        /// Returns a pointer to the source buffer (either inp_buf or inp_sess)
        /// </summary>
        public IntPtr inp_bufPtr
        {
            get
            {
                if (inp_buf != null)
                {
                    return new IntPtr(inp_buf);
                }
                else
                {
                    return _inputSessionPinner;
                }
            }
        }
        internal byte * inp_buf = null;
        public System.IO.FileStream inp_file = null;
        public int    inp_buf_size;
        public int    inp_buf_index;
        public byte   inp_curr;
        public int    inp_ctr;
        public byte[] inp_sess = new byte[64];
        public int    inp_end;
        public int    inp_peek;

        //void      (*translator)(struct ud*);
        public UdTranslatorDelegate translator;

        public ulong insn_offset;
        //public char[] insn_hexcode = new char[64];

        /*
        * Assembly output buffer
        */
        public char[] asm_buf;
        public int    asm_buf_size;
        public int    asm_buf_fill;
        public char[] asm_buf_int = new char[128];

        /*
        * Symbol resolver for use in the translation phase.
        */
        //const char* (*sym_resolver)(struct ud*, uint64_t addr, int64_t *offset);
        public UdSymbolResolverDelegate sym_resolver;

        public byte   dis_mode;
        public UInt64 pc;
        public byte   vendor;
        public ud_mnemonic_code mnemonic;
        public ud_operand[] operand = new ud_operand[4];
        public byte   error;
        public string errorMessage;
        public byte _rex;
        public byte pfx_rex;
        public byte pfx_seg;
        public byte pfx_opr;
        public byte pfx_adr;
        public byte pfx_lock;
        public byte pfx_str;
        public byte pfx_rep;
        public byte pfx_repe;
        public byte pfx_repne;
        public byte opr_mode;
        public byte adr_mode;
        public byte br_far;
        public byte br_near;
        public byte have_modrm;
        public byte modrm;
        public byte modrm_offset;
        public byte vex_op;
        public byte vex_b1;
        public byte vex_b2;
        public byte primary_opcode;
        public IntPtr user_opaque_data;
        public ud_itab_entry itab_entry;
        public ud_lookup_table_list_entry le;

        public ud()
        {
            _inputSessionPinner = new AutoPinner(inp_sess);
        }

        /// <summary>
        /// Keeps a reference to the input session array
        /// </summary>
        internal AutoPinner _inputSessionPinner;

        /// <summary>
        /// Frees the pinned buffer
        /// </summary>
        void CleanupPinners()
        {
            if (_inputSessionPinner != null)
            {
                _inputSessionPinner.Dispose();
                _inputSessionPinner = null;
            }
        }

        ~ud()
        {
            Dispose();
        }

        /// <summary>
        /// Cleanup unmanaged resources
        /// </summary>
        public void Dispose()
        {
            CleanupPinners();
        }
    }
}
#pragma warning restore 1591