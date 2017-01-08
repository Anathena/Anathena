namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    /// <summary>
    /// All possible "types" of objects in udis86. Order is Important.
    /// </summary>
    public enum UdType : Int32
    {
        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_NONE,

        // 8 bit GPRs -----------------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_AL,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CL,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DL,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_BL,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_AH,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CH,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DH,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_BH,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_SPL,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_BPL,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_SIL,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DIL,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R8B,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R9B,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R10B,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R11B,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R12B,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R13B,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R14B,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R15B,

        // 16 bit GPRs ----------------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_AX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_BX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_SP,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_BP,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_SI,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DI,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R8W,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R9W,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R10W,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R11W,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R12W,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R13W,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R14W,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R15W,

        // 32 bit GPRs ----------------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_EAX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ECX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_EDX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_EBX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ESP,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_EBP,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ESI,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_EDI,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R8D,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R9D,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R10D,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R11D,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R12D,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R13D,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R14D,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R15D,

        // 64 bit GPRs ----------------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_RAX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_RCX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_RDX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_RBX,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_RSP,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_RBP,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_RSI,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_RDI,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R8,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R9,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R10,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R11,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R12,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R13,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R14,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_R15,

        // Segment registers ----------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ES,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CS,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_SS,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DS,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_FS,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_GS,

        // Control registers ----------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR0,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR1,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR2,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR3,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR4,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR5,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR6,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR7,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR8,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR9,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR10,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR11,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR12,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR13,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR14,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_CR15,

        // Debug registers ------------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR0,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR1,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR2,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR3,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR4,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR5,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR6,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR7,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR8,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR9,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR10,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR11,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR12,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR13,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR14,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_DR15,

        // Mmx registers --------------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_MM0,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_MM1,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_MM2,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_MM3,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_MM4,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_MM5,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_MM6,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_MM7,

        // x87 registers --------------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ST0,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ST1,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ST2,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ST3,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ST4,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ST5,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ST6,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_ST7,

        // Extended multimedia registers ----------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM0,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM1,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM2,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM3,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM4,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM5,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM6,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM7,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM8,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM9,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM10,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM11,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM12,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM13,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM14,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_XMM15,

        // 256B multimedia registers --------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM0,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM1,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM2,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM3,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM4,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM5,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM6,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM7,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM8,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM9,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM10,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM11,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM12,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM13,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM14,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_YMM15,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_R_RIP,

        // Operand Types --------------------------------------------------------

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_OP_REG,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_OP_MEM,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_OP_PTR,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_OP_IMM,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_OP_JIMM,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        UD_OP_CONST
    }
    //// End enum
}
//// End namespace