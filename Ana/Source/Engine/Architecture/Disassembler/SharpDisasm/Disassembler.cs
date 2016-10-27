namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm
{
    using System;
    using System.Collections.Generic;
    using Translators;
    using Udis86;

    /// <summary>
    /// Provides a simple wrapper around the C# ported udis86 library.
    /// </summary>
    internal sealed class Disassembler : IDisposable
    {
        /// <summary>
        /// The address offset for the <see cref="code"/>.
        /// </summary>
        public readonly UInt64 Address;

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

        /// <summary>
        /// The binary code to be disassembled provided as a byte array.
        /// </summary>
        private readonly Byte[] code;

        /// <summary>
        /// A pointer to the code to be disassembled (e.g. a pointer to a function in memory)
        /// </summary>
        private readonly IntPtr codePtr;

        /// <summary>
        /// Gets or sets the maximum length of the code in memory to be disassembled <see cref="codePtr"/>
        /// </summary>
        private readonly Int32 codeLength;

        /// <summary>
        /// The udis86 ud structure used during disassembly.
        /// </summary>
        private Ud ud = new Ud();

        /// <summary>
        /// Used to pin the <see cref="code"/> byte array (if provided).
        /// </summary>
        private AutoPinner pinnedCodeArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="Disassembler" /> class. Prepares a new disassembler instance for the code provided. The instructions can then be disassembled with a call to <see cref="Disassemble"/>. The base address used to resolve relative addresses should be provided in <paramref name="address"/>.
        /// </summary>
        /// <param name="code">The code to be disassembled</param>
        /// <param name="architecture">The target x86 instruction set architecture of the code (e.g. 64-bit, 32-bit or 16-bit).</param>
        /// <param name="address">The address of the first byte of code. This value is used to resolve relative addresses into absolute addresses while disassembling.</param>
        /// <param name="copyBinaryToInstruction">Keeps a copy of the binary code for the instruction. This will increase the memory usage for each instruction. This is necessary if planning on using the <see cref="Translators.Translator.IncludeBinary"/> option.</param>
        /// <param name="vendor">What vendor instructions to support during disassembly, default is Any. Other options are AMD or Intel.</param>
        public Disassembler(Byte[] code, ArchitectureMode architecture, ulong address = 0x0, bool copyBinaryToInstruction = false, Vendor vendor = Vendor.Any)
        {
            this.code = code;
            this.Architecture = architecture;
            this.Address = address;
            this.CopyBinaryToInstruction = copyBinaryToInstruction;
            this.Vendor = vendor;

            if (code != null)
            {
                this.pinnedCodeArray = new AutoPinner(this.code);
                this.codePtr = this.pinnedCodeArray;
                this.codeLength = this.code.Length;
            }

            Translator = new IntelTranslator();
            this.InitUdis86();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disassembler" /> class. The instructions can then be disassembled with a call to <see cref="Disassemble"/>. The base address used to resolve relative addresses should be provided in <paramref name="address"/>.
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
            {
                throw new ArgumentOutOfRangeException("codePtr");
            }

            if (codeLength <= 0)
            {
                throw new ArgumentOutOfRangeException("codeLength", "Code length must be larger than 0.");
            }

            this.codePtr = codePtr;
            this.codeLength = codeLength;

            Translator = new IntelTranslator();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Disassembler" /> class
        /// </summary>
        ~Disassembler()
        {
            this.Dispose();
        }

        /// <summary>
        /// Gets or sets the translator that will be used when calling <see cref="Instruction.ToString"/>.
        /// </summary>
        public static Translator Translator { get; set; }

        /// <summary>
        /// Gets the number of bytes successfully decoded into instructions. This excludes invalid instructions.
        /// </summary>
        public Int32 BytesDecoded { get; private set; }

        /// <summary>
        /// Disassemble instructions and yield the result. Breaking out of the enumerator will prevent further instructions being disassembled.
        /// </summary>
        /// <returns>An IEnumerable collection of disassembled instructions</returns>
        public IEnumerable<Instruction> Disassemble()
        {
            this.Reset();
            Instruction instruction = null;
            while ((instruction = this.NextInstruction()) != null)
            {
                yield return instruction;
            }
        }

        /// <summary>
        /// Reset to beginning of the buffer
        /// </summary>
        public void Reset()
        {
            this.InitUdis86();
            this.BytesDecoded = 0;
            this.ud.InputBufferIndex = 0;
        }

        /// <summary>
        /// Decodes a single instruction and increments buffer position.
        /// </summary>
        /// <returns>TODO TODO</returns>
        public Instruction NextInstruction()
        {
            Int32 length = 0;
            if ((length = Udis86.Udis86.UdDisassemble(ref this.ud)) > 0)
            {
                Instruction instruction = new Instruction(ref this.ud, this.CopyBinaryToInstruction);
                if (!instruction.Error)
                {
                    this.BytesDecoded += length;
                }

                return instruction;
            }

            return null;
        }

        /// <summary>
        /// Dispose managed objects
        /// </summary>
        public void Dispose()
        {
            if (this.pinnedCodeArray != null)
            {
                this.pinnedCodeArray.Dispose();
                this.pinnedCodeArray = null;
            }
        }

        /// <summary>
        /// (Re)Initialize the udis86 disassembler
        /// </summary>
        private void InitUdis86()
        {
            // reset _u and initialise
            Udis86.Udis86.UdInit(ref this.ud);

            // set input buffer
            Udis86.Udis86.UdSetInputBuffer(ref this.ud, this.codePtr, this.codeLength);

            // set architecture
            Udis86.Udis86.UdSetMode(ref this.ud, (Byte)this.Architecture);

            // set program counter
            Udis86.Udis86.UdSetPc(ref this.ud, this.Address);

            // set the vendor
            Udis86.Udis86.UdSetVendor(ref this.ud, (Int32)this.Vendor);
        }
    }
    //// End class
}
//// End namespace