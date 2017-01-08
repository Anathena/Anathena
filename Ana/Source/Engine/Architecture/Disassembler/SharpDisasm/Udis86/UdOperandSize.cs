namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    /// <summary>
    /// Operand size constants.
    /// </summary>
    public enum UdOperandSize
    {
        // Symbolic constants for various operand sizes. Some of these constants are given a value equal to the width of the data (SZ_B == 8)

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_NA = 0,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_Z = 1,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_V = 2,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_Y = 3,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_X = 4,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_RDQ = 7,

        // The following values are used as is, and thus hard-coded

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_B = 8,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_W = 16,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_D = 32,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_Q = 64,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_T = 80,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_O = 12,

        /// <summary>
        /// Double quad
        /// </summary>
        SZ_DQ = 128,

        /// <summary>
        /// Quad Quad
        /// </summary>
        SZ_QQ = 256,

        // Complex size types, that encode sizes for operands of type MR (memory or register), for internal use only. Id space 256 and above

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_BD = (SZ_B << 8) | SZ_D,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_BV = (SZ_B << 8) | SZ_V,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_WD = (SZ_W << 8) | SZ_D,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_WV = (SZ_W << 8) | SZ_V,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_WY = (SZ_W << 8) | SZ_Y,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_DY = (SZ_D << 8) | SZ_Y,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_WO = (SZ_W << 8) | SZ_O,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_DO = (SZ_D << 8) | SZ_O,

        /// <summary>
        /// TODO TODO.
        /// </summary>
        SZ_QO = (SZ_Q << 8) | SZ_O,
    }

    /// <summary>
    /// TODO TODO.
    /// </summary>
    internal static class UdOperandSizeExtensions
    {
        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="size">TODO size.</param>
        /// <returns>TODO TODO.</returns>
        public static UdOperandSize Mx_mem_size(this UdOperandSize size)
        {
            return (UdOperandSize)((Int32)size >> 8 & 0xff);
        }

        /// <summary>
        /// TODO summary.
        /// </summary>
        /// <param name="size">TODO size.</param>
        /// <returns>TODO TODO.</returns>
        public static UdOperandSize Mx_reg_size(this UdOperandSize size)
        {
            return (UdOperandSize)((Int32)size & 0xff);
        }
    }
    //// End class
}
//// End namespace