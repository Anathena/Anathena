// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\ud_operand_size.cs)
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


namespace SharpDisasm.Udis86
{
    #pragma warning disable 1591
    /// <summary>
    /// Operand size constants
    /// </summary>
    public enum ud_operand_size
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

    internal static class ud_operand_size_Extensions
    {
        public static ud_operand_size Mx_mem_size(this ud_operand_size size)
        {
            return (ud_operand_size)((int)size >> 8 & 0xff);
        }

        public static ud_operand_size Mx_reg_size(this ud_operand_size size)
        {
            return (ud_operand_size)((int)size & 0xff);
        }
    }
    #pragma warning restore 1591
}
