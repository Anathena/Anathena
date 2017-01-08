namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// TODO TODO.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct UdLval
    {
        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(0)]
        public SByte SByte;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(0)]
        public Byte UByte;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(0)]
        public Int16 SWord;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(0)]
        public UInt16 UWord;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(0)]
        public Int32 SdWord;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(0)]
        public UInt32 UdWord;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(0)]
        public Int64 SqWord;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(0)]
        public UInt64 UqWord;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(0)]
        public UInt16 PtrSeg;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        [FieldOffset(2)]
        public UInt32 PtrOff;
    }
    //// End struct
}
//// End namespace