// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\ud_operand_code.cs)
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
    /* operand type constants -- order is important! */

    /// <summary>
    /// Operand codes
    /// </summary>
    public enum ud_operand_code {
        OP_NONE,

        OP_A,      OP_E,      OP_M,       OP_G,       
        OP_I,      OP_F,

        OP_R0,     OP_R1,     OP_R2,      OP_R3,
        OP_R4,     OP_R5,     OP_R6,      OP_R7,

        OP_AL,     OP_CL,     OP_DL,
        OP_AX,     OP_CX,     OP_DX,
        OP_eAX,    OP_eCX,    OP_eDX,
        OP_rAX,    OP_rCX,    OP_rDX,

        OP_ES,     OP_CS,     OP_SS,      OP_DS,  
        OP_FS,     OP_GS,

        OP_ST0,    OP_ST1,    OP_ST2,     OP_ST3,
        OP_ST4,    OP_ST5,    OP_ST6,     OP_ST7,

        OP_J,      OP_S,      OP_O,          
        OP_I1,     OP_I3,     OP_sI,

        OP_V,      OP_W,      OP_Q,       OP_P, 
        OP_U,      OP_N,      OP_MU,      OP_H,
        OP_L,

        OP_R,      OP_C,      OP_D,       

        OP_MR
    }
    #pragma warning restore 1591
}
