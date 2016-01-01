// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\BitOps.cs)
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

namespace SharpDisasm.Udis86
{
    internal static class BitOps
    {
        #region Instruction Table prefix bits

        public const UInt32 P_none = (0);

        public const UInt32 P_inv64 = (1 << 0);
        public static UInt32 P_INV64(UInt32 n) { return (n >> 0) & 1; }
        public const UInt32 P_def64 = (1 << 1);
        public static UInt32 P_DEF64(UInt32 n) { return (n >> 1) & 1; }

        public const UInt32 P_oso = (1 << 2);
        public static UInt32 P_OSO(UInt32 n) { return (n >> 2) & 1; }
        public const UInt32 P_aso = (1 << 3);
        public static UInt32 P_ASO(UInt32 n) { return (n >> 3) & 1; }
        public const UInt32 P_rexb = (1 << 4);
        public static UInt32 P_REXB(UInt32 n) { return (n >> 4) & 1; }
        public const UInt32 P_rexw = (1 << 5);
        public static UInt32 P_REXW(UInt32 n) { return (n >> 5) & 1; }
        public const UInt32 P_rexr = (1 << 6);
        public static UInt32 P_REXR(UInt32 n) { return (n >> 6) & 1; }
        public const UInt32 P_rexx = (1 << 7);
        public static UInt32 P_REXX(UInt32 n) { return (n >> 7) & 1; }

        public const UInt32 P_seg = (1 << 8);
        public static UInt32 P_SEG(UInt32 n) { return (n >> 8) & 1; }

        public const UInt32 P_vexl = (1 << 9);
        public static UInt32 P_VEXL(UInt32 n) { return (n >> 9) & 1; }
        public const UInt32 P_vexw = (1 << 10);
        public static UInt32 P_VEXW(UInt32 n) { return (n >> 10) & 1; }

        public const UInt32 P_str = (1 << 11);
        public static UInt32 P_STR(UInt32 n) { return (n >> 11) & 1; }
        public const UInt32 P_strz = (1 << 12);
        public static UInt32 P_STR_ZF(UInt32 n) { return (n >> 12) & 1; }

        #endregion

        #region REX prefix bits

        public static byte REX_W(byte r) { return (byte)((0xF & r) >> 3); }
        public static byte REX_R(byte r) { return (byte)((0x7 & r) >> 2); }
        public static byte REX_X(byte r) { return (byte)((0x3 & r) >> 1); }
        public static byte REX_B(byte r) { return (byte)((0x1 & r) >> 0); }
        public static uint REX_PFX_MASK(uint n)
        {
            return       ((P_REXW(n) << 3) |
                          (P_REXR(n) << 2) |
                          (P_REXX(n) << 1) |
                          (P_REXB(n) << 0));
        }

        #endregion

        #region Scale Index Base bits

        public static byte SIB_S(byte b) { return (byte)(b >> 6); }
        public static byte SIB_I(byte b) { return (byte)((b >> 3) & 7); }
        public static byte SIB_B(byte b) { return (byte)(b & 7); }

        #endregion

        #region MODRM bits

        public static byte MODRM_REG(byte b) { return (byte)((b >> 3) & 7); }
        public static byte MODRM_NNN(byte b) { return (byte)((b >> 3) & 7); }
        public static byte MODRM_MOD(byte b) { return (byte)((b >> 6) & 3); }
        public static byte MODRM_RM(byte b) { return (byte)(b & 7); }

        #endregion
    }
}
