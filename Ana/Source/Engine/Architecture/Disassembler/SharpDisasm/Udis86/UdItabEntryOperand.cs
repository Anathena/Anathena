namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    /// <summary>
    /// A single operand of an entry within the instruction table
    /// </summary>
    public struct UdItabEntryOperand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UdItabEntryOperand" /> struct
        /// </summary>
        /// <param name="operandType">TODO operandType</param>
        /// <param name="operandSize">TODO operandSize</param>
        public UdItabEntryOperand(UdOperandCode operandType, UdOperandSize operandSize)
        {
            this.OperandType = operandType;
            this.Size = operandSize;
        }

        /// <summary>
        /// Gets or sets the operand type
        /// </summary>
        public UdOperandCode OperandType { get; set; }

        /// <summary>
        /// Gets or sets the operand size
        /// </summary>
        public UdOperandSize Size { get; set; }
    }
    //// End struct
}
//// End namespace