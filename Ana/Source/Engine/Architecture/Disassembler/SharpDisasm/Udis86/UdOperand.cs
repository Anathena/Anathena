namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    /// <summary>
    /// Disassembled instruction Operand.
    /// </summary>
    public struct UdOperand
    {
        public UdLval Lval;

        public UdType UdType { get; set; }

        public UInt16 Size { get; set; }

        public UdType Base { get; set; }

        public UdType Index { get; set; }

        public Byte Scale { get; set; }

        /// <summary>
        /// Gets or sets offset size (8, 16, 32, 64)
        /// </summary>
        public Byte Offset { get; set; }

        internal UdOperandCode OperandCode { get; set; }
    }
    //// End struct
}
//// End namespace