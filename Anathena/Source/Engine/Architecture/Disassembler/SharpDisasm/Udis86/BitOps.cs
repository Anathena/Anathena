namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    internal static class BitOps
    {
        public const UInt32 PNone = 0;
        public const UInt32 PInv64 = 1 << 0;
        public const UInt32 PDef64 = 1 << 1;
        public const UInt32 POso = 1 << 2;
        public const UInt32 PAso = 1 << 3;
        public const UInt32 PRexb = 1 << 4;
        public const UInt32 PRexw = 1 << 5;
        public const UInt32 PRexr = 1 << 6;
        public const UInt32 PRexx = 1 << 7;
        public const UInt32 PSeg = 1 << 8;
        public const UInt32 PVexl = 1 << 9;
        public const UInt32 PVexw = 1 << 10;
        public const UInt32 PStr = 1 << 11;
        public const UInt32 PStrz = 1 << 12;

        public static UInt32 P_INV64(UInt32 n)
        {
            return (n >> 0) & 1;
        }

        public static UInt32 P_DEF64(UInt32 n)
        {
            return (n >> 1) & 1;
        }

        public static UInt32 P_OSO(UInt32 n)
        {
            return (n >> 2) & 1;
        }

        public static UInt32 P_ASO(UInt32 n)
        {
            return (n >> 3) & 1;
        }

        public static UInt32 P_REXB(UInt32 n)
        {
            return (n >> 4) & 1;
        }

        public static UInt32 P_REXW(UInt32 n)
        {
            return (n >> 5) & 1;
        }

        public static UInt32 P_REXR(UInt32 n)
        {
            return (n >> 6) & 1;
        }

        public static UInt32 P_REXX(UInt32 n)
        {
            return (n >> 7) & 1;
        }

        public static UInt32 P_SEG(UInt32 n)
        {
            return (n >> 8) & 1;
        }

        public static UInt32 P_VEXL(UInt32 n)
        {
            return (n >> 9) & 1;
        }

        public static UInt32 P_VEXW(UInt32 n)
        {
            return (n >> 10) & 1;
        }

        public static UInt32 P_STR(UInt32 n)
        {
            return (n >> 11) & 1;
        }

        public static UInt32 P_STR_ZF(UInt32 n)
        {
            return (n >> 12) & 1;
        }

        public static Byte REX_W(Byte r)
        {
            return (Byte)((0xF & r) >> 3);
        }

        public static Byte REX_R(Byte r)
        {
            return (Byte)((0x7 & r) >> 2);
        }

        public static Byte REX_X(Byte r)
        {
            return (Byte)((0x3 & r) >> 1);
        }

        public static Byte REX_B(Byte r)
        {
            return (Byte)((0x1 & r) >> 0);
        }

        public static UInt32 REX_PFX_MASK(UInt32 n)
        {
            return (P_REXW(n) << 3) |
                          (P_REXR(n) << 2) |
                          (P_REXX(n) << 1) |
                          (P_REXB(n) << 0);
        }

        public static Byte SIB_S(Byte b)
        {
            return (Byte)(b >> 6);
        }

        public static Byte SIB_I(Byte b)
        {
            return (Byte)((b >> 3) & 7);
        }

        public static Byte SIB_B(Byte b)
        {
            return (Byte)(b & 7);
        }

        public static Byte MODRM_REG(Byte b)
        {
            return (Byte)((b >> 3) & 7);
        }

        public static Byte MODRM_NNN(Byte b)
        {
            return (Byte)((b >> 3) & 7);
        }

        public static Byte MODRM_MOD(Byte b)
        {
            return (Byte)((b >> 6) & 3);
        }

        public static Byte MODRM_RM(Byte b)
        {
            return (Byte)(b & 7);
        }
    }
    //// End class
}
//// End namespace