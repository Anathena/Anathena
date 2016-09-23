namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    /// <summary>
    /// Disassembled instruction Operand.
    /// </summary>
    public struct UdOperand
    {
        /// <summary>
        /// Gets or sets TODO TODO
        /// </summary>
        public UdLval Lval;

        /// <summary>
        /// Gets or sets TODO TODO
        /// </summary>
        public UdType UdType { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO
        /// </summary>
        public UInt16 Size { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO
        /// </summary>
        public UdType Base { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO
        /// </summary>
        public UdType Index { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO
        /// </summary>
        public Byte Scale { get; set; }

        /// <summary>
        /// Gets or sets offset size (8, 16, 32, 64)
        /// </summary>
        public Byte Offset { get; set; }

        /// <summary>
        /// Gets or sets the operand code
        /// </summary>
        internal UdOperandCode OperandCode { get; set; }
    }
    //// End struct
}
//// End namespace