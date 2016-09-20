namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct UdLval
    {
        [FieldOffset(0)]
        public SByte SByte;
        [FieldOffset(0)]
        public Byte UByte;
        [FieldOffset(0)]
        public Int16 SWord;
        [FieldOffset(0)]
        public UInt16 UWord;
        [FieldOffset(0)]
        public Int32 SdWord;
        [FieldOffset(0)]
        public UInt32 UdWord;
        [FieldOffset(0)]
        public Int64 SqWord;
        [FieldOffset(0)]
        public UInt64 UqWord;
        [FieldOffset(0)]
        public UInt16 PtrSeg;
        [FieldOffset(2)]
        public UInt32 PtrOff;
    }
    //// End struct
}
//// End namespace