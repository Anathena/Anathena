// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\disassembler.cs)
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

namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm
{
    /// <summary>
    /// Provides a simple wrapper around the C# ported udis86 library.
    /// </summary>
    public sealed class Disassembler : IDisposable
    {
        #region Public field members
        /// <summary>
        /// The address offset for the <see cref="Code"/>.
        /// </summary>
        public readonly ulong Address;

        /// <summary>
        /// The x86 CPU architecture to use: 16-bit, 32-bit or 64-bit.
        /// </summary>
        public readonly ArchitectureMode Architecture;

        /// <summary>
        /// Copies the source binary to the decoded instructions. When true this option increases the memory required for each decoded instruction.
        /// </summary>
        public readonly bool CopyBinaryToInstruction = false;

        /// <summary>
        /// Which vendor instructions to support for disassembly. Options are AMD, Intel or Any.
        /// </summary>
        public readonly Vendor Vendor;
        #endregion

        #region Private field members
        /// <summary>
        /// The udis86 ud structure used during disassembly.
        /// </summary>
        private Udis86.ud _u = new Udis86.ud();

        /// <summary>
        /// The binary code to be disassembled provided as a byte array.
        /// </summary>
        private readonly byte[] Code;

        /// <summary>
        /// Used to pin the <see cref="Code"/> byte array (if provided).
        /// </summary>
        private AutoPinner _pinnedCodeArray;

        /// <summary>
        /// A pointer to the code to be disassembled (e.g. a pointer to a function in memory)
        /// </summary>
        private readonly IntPtr CodePtr;

        /// <summary>
        /// The maximum length of the code in memory to be disassembled <see cref="CodePtr"/>
        /// </summary>
        private readonly int CodeLength;

        #endregion

        /// <summary>
        /// The translator that will be used when calling <see cref="Instruction.ToString"/>.
        /// </summary>
        public static SharpDisasm.Translators.Translator Translator = new SharpDisasm.Translators.IntelTranslator();

        /// <summary>
        /// The number of bytes successfully decoded into instructions. This excludes invalid instructions.
        /// </summary>
        public int BytesDecoded { get; private set; }

        /// <summary>
        /// Prepares a new disassembler instance for the code provided. The instructions can then be disassembled with a call to <see cref="Disassemble"/>. The base address used to resolve relative addresses should be provided in <paramref name="address"/>.
        /// </summary>
        /// <param name="code">The code to be disassembled</param>
        /// <param name="architecture">The target x86 instruction set architecture of the code (e.g. 64-bit, 32-bit or 16-bit).</param>
        /// <param name="address">The address of the first byte of code. This value is used to resolve relative addresses into absolute addresses while disassembling.</param>
        /// <param name="copyBinaryToInstruction">Keeps a copy of the binary code for the instruction. This will increase the memory usage for each instruction. This is necessary if planning on using the <see cref="Translators.Translator.IncludeBinary"/> option.</param>
        /// <param name="vendor">What vendor instructions to support during disassembly, default is Any. Other options are AMD or Intel.</param>
        public Disassembler(byte[] code, ArchitectureMode architecture, ulong address = 0x0, bool copyBinaryToInstruction = false, Vendor vendor = Vendor.Any)
        {
            this.Code = code;
            if (code != null)
            {
                _pinnedCodeArray = new AutoPinner(Code);
                this.CodePtr = _pinnedCodeArray;
                this.CodeLength = Code.Length;
            }
            this.Architecture = architecture;
            this.Address = address;
            this.CopyBinaryToInstruction = copyBinaryToInstruction;
            this.Vendor = vendor;

            InitUdis86();
        }

        /// <summary>
        /// Prepares a new disassembler instance for the code located at the memory address provided. The instructions can then be disassembled with a call to <see cref="Disassemble"/>. The base address used to resolve relative addresses should be provided in <paramref name="address"/>.
        /// </summary>
        /// <param name="codePtr">A pointer to memory to be disassembled.</param>
        /// <param name="codeLength">The maximum length to be disassembled.</param>
        /// <param name="architecture">The architecture of the code (e.g. 64-bit, 32-bit or 16-bit).</param>
        /// <param name="address">The address of the first byte of code. This value is used to resolve relative addresses into absolute addresses while disassembling.</param>
        /// <param name="copyBinaryToInstruction">Keeps a copy of the binary code for the instruction. This will increase the memory usage for each instruction. This is necessary if planning on using the <see cref="Translators.Translator.IncludeBinary"/> option.</param>
        /// <param name="vendor">What vendors to support for disassembly, default is Any. Other options are AMD or Intel.</param>
        public Disassembler(IntPtr codePtr, int codeLength, ArchitectureMode architecture, ulong address = 0x0, bool copyBinaryToInstruction = false, Vendor vendor = Vendor.Any)
            : this(null, architecture, address, copyBinaryToInstruction, vendor)
        {
            if (codePtr == IntPtr.Zero)
                throw new ArgumentOutOfRangeException("codePtr");
            if (codeLength <= 0)
                throw new ArgumentOutOfRangeException("codeLength", "Code length must be larger than 0.");

            this.CodePtr = codePtr;
            this.CodeLength = codeLength;
        }

        /// <summary>
        /// (Re)Initialise the udis86 disassembler
        /// </summary>
        private void InitUdis86()
        {
            // reset _u and initialise
            Udis86.udis86.ud_init(ref _u);
            // set input buffer
            Udis86.udis86.ud_set_input_buffer(ref _u, this.CodePtr, this.CodeLength);
            // set architecture
            Udis86.udis86.ud_set_mode(ref _u, (byte)this.Architecture);
            // set program counter
            Udis86.udis86.ud_set_pc(ref _u, Address);
            // set the vendor
            Udis86.udis86.ud_set_vendor(ref _u, (int)Vendor);
        }

        /// <summary>
        /// Disassemble instructions and yield the result. Breaking out of the enumerator will prevent further instructions being disassembled.
        /// </summary>
        /// <returns>An IEnumerable collection of disassembled instructions</returns>
        public IEnumerable<Instruction> Disassemble()
        {
            Reset();
            Instruction instruction = null;
            while ((instruction = NextInstruction()) != null)
            {
                yield return instruction;
            }
        }

        /// <summary>
        /// Reset to beginning of the buffer
        /// </summary>
        public void Reset()
        {
            InitUdis86();
            BytesDecoded = 0;
            _u.inp_buf_index = 0;
        }

        /// <summary>
        /// Decodes a single instruction and increments buffer position.
        /// </summary>
        /// <returns></returns>
        public Instruction NextInstruction()
        {
            int length = 0;
            if ((length = Udis86.udis86.ud_disassemble(ref _u)) > 0)
            {
                var instruction = new Instruction(ref _u, CopyBinaryToInstruction);
                if (!instruction.Error)
                {
                    BytesDecoded += length;
                }
                return instruction;
            }
            return null;
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~Disassembler()
        {
            Dispose();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_pinnedCodeArray != null)
            {
                _pinnedCodeArray.Dispose();
                _pinnedCodeArray = null;
            }
        }

    } // End class

} // End namespace