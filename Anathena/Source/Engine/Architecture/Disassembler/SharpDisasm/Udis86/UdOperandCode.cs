namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    /// <summary>
    /// Operand codes. Order is important.
    /// </summary>
    public enum UdOperandCode
    {
        OP_NONE,

        OP_A, OP_E, OP_M, OP_G,
        OP_I, OP_F,

        OP_R0, OP_R1, OP_R2, OP_R3,
        OP_R4, OP_R5, OP_R6, OP_R7,

        OP_AL, OP_CL, OP_DL,
        OP_AX, OP_CX, OP_DX,
        OP_eAX, OP_eCX, OP_eDX,
        OP_rAX, OP_rCX, OP_rDX,

        OP_ES, OP_CS, OP_SS, OP_DS,
        OP_FS, OP_GS,

        OP_ST0, OP_ST1, OP_ST2, OP_ST3,
        OP_ST4, OP_ST5, OP_ST6, OP_ST7,

        OP_J, OP_S, OP_O,
        OP_I1, OP_I3, OP_sI,

        OP_V, OP_W, OP_Q, OP_P,
        OP_U, OP_N, OP_MU, OP_H,
        OP_L,

        OP_R, OP_C, OP_D,

        OP_MR
    }
    //// End class
}
//// End namespace