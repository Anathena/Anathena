namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    /// <summary>
    /// Operand size constants
    /// </summary>
    public enum UdOperandSize
    {
        /*
         * Operand size constants
         *
         *  Symbolic constants for various operand sizes. Some of these constants
         *  are given a value equal to the width of the data (SZ_B == 8), such
         *  that they maybe used interchangeably in the internals. Modifying them
         *  will most certainly break things!
         */

        SZ_NA = 0,
        SZ_Z = 1,
        SZ_V = 2,
        SZ_Y = 3,
        SZ_X = 4,
        SZ_RDQ = 7,

        /* the following values are used as is,
         * and thus hard-coded. changing them 
         * will break internals 
         */
        SZ_B = 8,
        SZ_W = 16,
        SZ_D = 32,
        SZ_Q = 64,
        SZ_T = 80,
        SZ_O = 12,
        SZ_DQ = 128, // double quad
        SZ_QQ = 256, // quad quad

        /*
         * complex size types, that encode sizes for operands
         * of type MR (memory or register), for internal use
         * only. Id space 256 and above.
         */
        SZ_BD = (SZ_B << 8) | SZ_D,
        SZ_BV = (SZ_B << 8) | SZ_V,
        SZ_WD = (SZ_W << 8) | SZ_D,
        SZ_WV = (SZ_W << 8) | SZ_V,
        SZ_WY = (SZ_W << 8) | SZ_Y,
        SZ_DY = (SZ_D << 8) | SZ_Y,
        SZ_WO = (SZ_W << 8) | SZ_O,
        SZ_DO = (SZ_D << 8) | SZ_O,
        SZ_QO = (SZ_Q << 8) | SZ_O,
    }

    internal static class UdOperandSizeExtensions
    {
        public static UdOperandSize Mx_mem_size(this UdOperandSize size)
        {
            return (UdOperandSize)((Int32)size >> 8 & 0xff);
        }

        public static UdOperandSize Mx_reg_size(this UdOperandSize size)
        {
            return (UdOperandSize)((Int32)size & 0xff);
        }
    }
    //// End class
}
//// End namespace