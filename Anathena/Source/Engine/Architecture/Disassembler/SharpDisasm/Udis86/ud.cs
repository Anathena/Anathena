namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;
    using System.IO;

    public delegate void UdTranslatorDelegate(ref Ud ud);

    public delegate String UdSymbolResolverDelegate(ref Ud ud, Int64 addr, ref Int64 offset);

    public delegate Int32 UdInputCallback(ref Ud ud);

    public sealed unsafe class Ud : IDisposable
    {
        public Ud()
        {
            this.InputBuffer = null;
            this.InputFile = null;
            this.InputSession = new Byte[64];
            this.AsmBufferInt = new Char[128];
            this.Operand = new UdOperand[4];
            this.InputSessionPinner = new AutoPinner(this.InputSession);
        }

        ~Ud()
        {
            this.Dispose();
        }

        public UdInputCallback InputHook { get; set; }

        /// <summary>
        /// Gets a pointer to the source buffer
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

        public FileStream InputFile { get; set; }

        public Int32 InputBufferSize { get; set; }

        public Int32 InputBufferIndex { get; set; }

        public Byte InputCur { get; set; }

        public Int32 InputCtr { get; set; }

        public Byte[] InputSession { get; set; }

        public Int32 InputEnd { get; set; }

        public Int32 InputPeek { get; set; }

        public UdTranslatorDelegate Translator { get; set; }

        public UInt64 InstructionOffset { get; set; }

        /// <summary>
        /// Gets or sets the assembly output buffer
        /// </summary>
        public Char[] AsmBuffer { get; set; }

        public Int32 AsmBufferSize { get; set; }

        public Int32 AsmBufferFill { get; set; }

        public Char[] AsmBufferInt { get; set; }

        /// <summary>
        /// Gets or sets the symbol resolver for use in the translation phase
        /// </summary>
        public UdSymbolResolverDelegate SymResolver { get; set; }

        public Byte DisMode { get; set; }

        public UInt64 Pc { get; set; }

        public Byte Vendor { get; set; }

        public UdMnemonicCode Mnemonic { get; set; }

        public UdOperand[] Operand { get; set; }

        public Byte Error { get; set; }

        public String ErrorMessage { get; set; }

        public Byte Rex { get; set; }

        public Byte PfxRex { get; set; }

        public Byte PfxSeg { get; set; }

        public Byte PfxOpr { get; set; }

        public Byte PfxAdr { get; set; }

        public Byte PfxLock { get; set; }

        public Byte PfxStr { get; set; }

        public Byte PfxRep { get; set; }

        public Byte PfxRepe { get; set; }

        public Byte PfxRepne { get; set; }

        public Byte OprMode { get; set; }

        public Byte AddressMode { get; set; }

        public Byte BrFar { get; set; }

        public Byte BrNear { get; set; }

        public Byte HaveModrm { get; set; }

        public Byte Modrm { get; set; }

        public Byte ModrmOffset { get; set; }

        public Byte VexOp { get; set; }

        public Byte VexB1 { get; set; }

        public Byte VexB2 { get; set; }

        public Byte PrimaryOpcode { get; set; }

        public IntPtr UserOpaqueData { get; set; }

        public UdItabEntry ItabEntry { get; set; }

        public UdLookupTableListEntry Le { get; set; }

        internal Byte* InputBuffer { get; set; }

        /// <summary>
        /// Gets or sets a reference to the input session array
        /// </summary>
        internal AutoPinner InputSessionPinner { get; set; }

        /// <summary>
        /// Cleanup unmanaged resources
        /// </summary>
        public void Dispose()
        {
            this.CleanupPinners();
        }

        /// <summary>
        /// Frees the pinned buffer
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