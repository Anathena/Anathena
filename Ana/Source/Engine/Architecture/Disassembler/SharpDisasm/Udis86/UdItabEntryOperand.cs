namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    /// <summary>
    /// A single operand of an entry within the instruction table
    /// </summary>
    public struct UdItabEntryOperand
    {
        public UdItabEntryOperand(UdOperandCode operandType, UdOperandSize operandSize)
        {
            this.OperandType = operandType;
            this.Size = operandSize;
        }

        public UdOperandCode OperandType { get; set; }

        public UdOperandSize Size { get; set; }
    }
    //// End struct
}
//// End namespace