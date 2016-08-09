// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\instruction.cs)
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
using System.Runtime.InteropServices;

namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm
{
    /// <summary>
    /// Represents a decoded instruction.
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Instruction Offset
        /// </summary>
        public ulong Offset { get; private set; }

        /// <summary>
        /// Program counter
        /// </summary>
        public ulong PC { get; private set; }

        /// <summary>
        /// Will contain a copy of the original binary instruction if <see cref="Disassembler.CopyBinaryToInstruction"/> is true.
        /// </summary>
        public byte[] Bytes { get; private set; }

        /// <summary>
        /// Mnemonic
        /// </summary>
        public SharpDisasm.Udis86.ud_mnemonic_code Mnemonic { get; private set; }

        /// <summary>
        /// The instruction operands (maximum 3)
        /// </summary>
        public Operand[] Operands { get; private set; }

        /// <summary>
        /// The length of the instruction in bytes
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Indicates whether the instruction was successfully decoded.
        /// </summary>
        public bool Error { get; private set; }

        /// <summary>
        /// The reason an instruction was not successfully decoded.
        /// </summary>
        public string ErrorMessage { get; private set; }

        #region Low-level instruction information

        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte pfx_rex;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte pfx_seg;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte pfx_opr;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte pfx_adr;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte pfx_lock;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte pfx_str;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte pfx_rep;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte pfx_repe;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte pfx_repne;

        /// <summary>
        /// The operand mode (16-,32-, or 64-bit), i.e. we could be reading a 16-bit value from a 32-bit address, in which case opr_mode would be 16, while adr_mode would be 32.
        /// </summary>
        public byte opr_mode;

        /// <summary>
        /// The memory addressing mode of the instruction (16-,32-, or 64-bit)
        /// </summary>
        public byte adr_mode;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte br_far;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte br_near;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte have_modrm;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte modrm;
        /// <summary>
        /// Low-level decode information
        /// </summary>
        public byte primary_opcode;

        #endregion

        /// <summary>
        /// The instruction architecture as configured within the constructor of <see cref="Disassembler"/>
        /// </summary>
        public ArchitectureMode dis_mode;

        /// <summary>
        /// The instruction table entry that applies to this instruction
        /// </summary>
        public SharpDisasm.Udis86.ud_itab_entry itab_entry;

        internal Instruction(ref Udis86.ud u, bool keepBinary)
        {
            this.Offset = u.insn_offset;
            this.PC = u.pc;
            this.Mnemonic = u.mnemonic;

            // Add operands
            List<Operand> operands = new List<Operand>(4);
            if (u.operand[0].type != Udis86.ud_type.UD_NONE)
            {
                operands.Add(new Operand(u.operand[0]));
                if (u.operand[1].type != Udis86.ud_type.UD_NONE)
                {
                    operands.Add(new Operand(u.operand[1]));
                    if (u.operand[2].type != Udis86.ud_type.UD_NONE)
                    {
                        operands.Add(new Operand(u.operand[2]));
                        if (u.operand[3].type != Udis86.ud_type.UD_NONE) operands.Add(new Operand(u.operand[3]));
                    }
                }
            }
            this.Operands = operands.ToArray();

            this.Length = u.inp_ctr;

            // Copy the instruction bytes
            if (keepBinary)
            {
                this.Bytes = new byte[this.Length];
                Marshal.Copy((IntPtr)(u.inp_bufPtr.ToInt64() + u.inp_buf_index - this.Length), this.Bytes, 0, this.Length);
            }

            if (u.error > 0)
            {
                this.Error = true;
                this.ErrorMessage = u.errorMessage;
            }
            else if (this.Mnemonic == Udis86.ud_mnemonic_code.UD_Iinvalid)
            {
                this.Error = true;
                this.ErrorMessage = "Invalid instruction";
            }

            this.itab_entry = u.itab_entry;
            this.dis_mode = (ArchitectureMode)u.dis_mode;
            this.pfx_rex = u.pfx_rex;
            this.pfx_seg = u.pfx_seg;
            this.pfx_opr = u.pfx_opr;
            this.pfx_adr = u.pfx_adr;
            this.pfx_lock = u.pfx_lock;
            this.pfx_str = u.pfx_str;
            this.pfx_rep = u.pfx_rep;
            this.pfx_repe = u.pfx_repe;
            this.pfx_repne = u.pfx_repne;
            this.opr_mode = u.opr_mode;
            this.adr_mode = u.adr_mode;
            this.br_far = u.br_far;
            this.br_near = u.br_near;
            this.have_modrm = u.have_modrm;
            this.modrm = u.modrm;
            this.primary_opcode = u.primary_opcode;
        }

        /// <summary>
        /// Output the instruction using the <see cref="Translators.Translator"/> assigned to <see cref="Disassembler.Translator"/>.
        /// </summary>
        /// <returns>The translated instruction (e.g. Intel ASM syntax)</returns>
        public override string ToString()
        {
            System.Diagnostics.Debug.Assert(Disassembler.Translator != null, "Disassembler.Translator must be configured to use Instruction.ToString");
            return Disassembler.Translator.Translate(this);
        }

    } // End class

} // End namespace