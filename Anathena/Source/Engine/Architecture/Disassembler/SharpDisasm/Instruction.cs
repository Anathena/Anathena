namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Udis86;

    /// <summary>
    /// Represents a decoded instruction.
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Instruction" /> class
        /// </summary>
        /// <param name="u"></param>
        /// <param name="keepBinary"></param>
        internal Instruction(ref ud u, Boolean keepBinary)
        {
            this.Offset = u.insn_offset;
            this.PC = u.pc;
            this.Mnemonic = u.mnemonic;

            // Add operands
            List<Operand> operands = new List<Operand>(4);
            if (u.operand[0].type != ud_type.UD_NONE)
            {
                operands.Add(new Operand(u.operand[0]));
                if (u.operand[1].type != ud_type.UD_NONE)
                {
                    operands.Add(new Operand(u.operand[1]));
                    if (u.operand[2].type != ud_type.UD_NONE)
                    {
                        operands.Add(new Operand(u.operand[2]));
                        if (u.operand[3].type != ud_type.UD_NONE)
                        {
                            operands.Add(new Operand(u.operand[3]));
                        }
                    }
                }
            }

            this.Operands = operands.ToArray();

            this.Length = u.inp_ctr;

            // Copy the instruction bytes
            if (keepBinary)
            {
                this.Bytes = new Byte[this.Length];
                Marshal.Copy((IntPtr)(u.inp_bufPtr.ToInt64() + u.inp_buf_index - this.Length), this.Bytes, 0, this.Length);
            }

            if (u.error > 0)
            {
                this.Error = true;
                this.ErrorMessage = u.errorMessage;
            }
            else if (this.Mnemonic == ud_mnemonic_code.UD_Iinvalid)
            {
                this.Error = true;
                this.ErrorMessage = "Invalid instruction";
            }

            this.ItabEntry = u.itab_entry;
            this.DisMode = (ArchitectureMode)u.dis_mode;
            this.PfxRex = u.pfx_rex;
            this.PfxSeg = u.pfx_seg;
            this.PfxOpr = u.pfx_opr;
            this.PfxAdr = u.pfx_adr;
            this.PfxLock = u.pfx_lock;
            this.PfxStr = u.pfx_str;
            this.PfxRep = u.pfx_rep;
            this.PfxRepe = u.pfx_repe;
            this.PfxRepne = u.pfx_repne;
            this.OprMode = u.opr_mode;
            this.AdrMode = u.adr_mode;
            this.BrFar = u.br_far;
            this.BrNear = u.br_near;
            this.HaveModrm = u.have_modrm;
            this.Modrm = u.modrm;
            this.PrimaryOpcode = u.primary_opcode;
        }

        /// <summary>
        /// Gets instruction Offset
        /// </summary>
        public UInt64 Offset { get; private set; }

        /// <summary>
        /// Gets program counter
        /// </summary>
        public UInt64 PC { get; private set; }

        /// <summary>
        /// Gets a copy of the original binary instruction if <see cref="Disassembler.CopyBinaryToInstruction"/> is true.
        /// </summary>
        public Byte[] Bytes { get; private set; }

        /// <summary>
        /// Gets the mnemonic
        /// </summary>
        public ud_mnemonic_code Mnemonic { get; private set; }

        /// <summary>
        /// Gets the instruction operands (maximum 3)
        /// </summary>
        public Operand[] Operands { get; private set; }

        /// <summary>
        /// Gets the length of the instruction in bytes
        /// </summary>
        public Int32 Length { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the instruction was successfully decoded
        /// </summary>
        public Boolean Error { get; private set; }

        /// <summary>
        /// Gets the reason an instruction was not successfully decoded
        /// </summary>
        public String ErrorMessage { get; private set; }

        #region Low-level instruction information

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PfxRex { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PfxSeg { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PfxOpr { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PfxAdr { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PfxLock { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PfxStr { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PfxRep { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PfxRepe { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PfxRepne { get; private set; }

        /// <summary>
        /// Gets the operand mode (16-,32-, or 64-bit), i.e. we could be reading a 16-bit value from a 32-bit address, in which case opr_mode would be 16, while adr_mode would be 32.
        /// </summary>
        public Byte OprMode { get; private set; }

        /// <summary>
        /// Gets the memory addressing mode of the instruction (16-,32-, or 64-bit)
        /// </summary>
        public Byte AdrMode { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte BrFar { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte BrNear { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte HaveModrm { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte Modrm { get; private set; }

        /// <summary>
        /// Gets low-level decode information
        /// </summary>
        public Byte PrimaryOpcode { get; private set; }

        #endregion

        /// <summary>
        /// Gets the instruction architecture as configured within the constructor of <see cref="Disassembler"/>
        /// </summary>
        public ArchitectureMode DisMode { get; private set; }

        /// <summary>
        /// Gets the instruction table entry that applies to this instruction
        /// </summary>
        public ud_itab_entry ItabEntry { get; private set; }

        /// <summary>
        /// Output the instruction using the <see cref="Translators.Translator"/> assigned to <see cref="Disassembler.Translator"/>.
        /// </summary>
        /// <returns>The translated instruction (e.g. Intel ASM syntax)</returns>
        public override String ToString()
        {
            Debug.Assert(Disassembler.Translator != null, "Disassembler.Translator must be configured to use Instruction.ToString");
            return Disassembler.Translator.Translate(this);
        }
    }
    //// End class
}
//// End namespace