namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;
    using System.IO;

    /// <summary>
    /// TODO summary.
    /// </summary>
    /// <param name="ud">TODO ud.</param>
    internal delegate void UdTranslatorDelegate(ref Ud ud);

    /// <summary>
    /// TODO summary.
    /// </summary>
    /// <param name="ud">TODO ud.</param>
    /// <param name="addr">TODO addr.</param>
    /// <param name="offset">TODO offset.</param>
    /// <returns>TODO TODO.</returns>
    internal delegate String UdSymbolResolverDelegate(ref Ud ud, Int64 addr, ref Int64 offset);

    /// <summary>
    /// TODO summary.
    /// </summary>
    /// <param name="ud">TODO ud.</param>
    /// <returns>TODO TODO.</returns>
    internal delegate Int32 UdInputCallback(ref Ud ud);

    /// <summary>
    /// TODO TODO.
    /// </summary>
    internal sealed unsafe class Ud : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ud" /> class.
        /// </summary>
        public Ud()
        {
            this.InputBuffer = null;
            this.InputFile = null;
            this.InputSession = new Byte[64];
            this.AsmBufferInt = new Char[128];
            this.Operand = new UdOperand[4];
            this.InputSessionPinner = new AutoPinner(this.InputSession);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Ud" /> class.
        /// </summary>
        ~Ud()
        {
            this.Dispose();
        }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public UdInputCallback InputHook { get; set; }

        /// <summary>
        /// Gets a pointer to the source buffer.
        /// </summary>
        public IntPtr InputBufferPointer
        {
            get
            {
                if (this.InputBuffer != null)
                {
                    return new IntPtr(this.InputBuffer);
                }
                else
                {
                    return this.InputSessionPinner;
                }
            }
        }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public FileStream InputFile { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Int32 InputBufferSize { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Int32 InputBufferIndex { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte InputCur { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Int32 InputCtr { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte[] InputSession { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Int32 InputEnd { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Int32 InputPeek { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public UdTranslatorDelegate Translator { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public UInt64 InstructionOffset { get; set; }

        /// <summary>
        /// Gets or sets the assembly output buffer.
        /// </summary>
        public Char[] AsmBuffer { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Int32 AsmBufferSize { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Int32 AsmBufferFill { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Char[] AsmBufferInt { get; set; }

        /// <summary>
        /// Gets or sets the symbol resolver for use in the translation phase.
        /// </summary>
        public UdSymbolResolverDelegate SymResolver { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte DisMode { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public UInt64 Pc { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte Vendor { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public UdMnemonicCode Mnemonic { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public UdOperand[] Operand { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte Error { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public String ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte Rex { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PfxRex { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PfxSeg { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PfxOpr { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PfxAdr { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PfxLock { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PfxStr { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PfxRep { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PfxRepe { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PfxRepne { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte OprMode { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte AddressMode { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte BrFar { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte BrNear { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte HaveModrm { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte Modrm { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte ModrmOffset { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte VexOp { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte VexB1 { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte VexB2 { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public Byte PrimaryOpcode { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public IntPtr UserOpaqueData { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public UdItabEntry ItabEntry { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        public UdLookupTableListEntry Le { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO.
        /// </summary>
        internal Byte* InputBuffer { get; set; }

        /// <summary>
        /// Gets or sets a reference to the input session array.
        /// </summary>
        internal AutoPinner InputSessionPinner { get; set; }

        /// <summary>
        /// Cleanup unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.CleanupPinners();
        }

        /// <summary>
        /// Frees the pinned buffer.
        /// </summary>
        private void CleanupPinners()
        {
            if (this.InputSessionPinner != null)
            {
                this.InputSessionPinner.Dispose();
                this.InputSessionPinner = null;
            }
        }
    }
    //// End class
}
//// End namespace