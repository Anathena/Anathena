namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    /// <summary>
    /// A single entry within an instruction table
    /// </summary>
    public class UdItabEntry
    {
        public readonly UdMnemonicCode Mnemonic;
        public readonly UdItabEntryOperand Operand1;
        public readonly UdItabEntryOperand Operand2;
        public readonly UdItabEntryOperand Operand3;
        public readonly UdItabEntryOperand Operand4;
        public readonly UInt32 Prefix;

        internal UdItabEntry()
        {
        }

        internal UdItabEntry(
            UdMnemonicCode mnemonic,
            UdItabEntryOperand operand1,
            UdItabEntryOperand operand2,
            UdItabEntryOperand operand3,
            UdItabEntryOperand operand4,
            UInt32 prefix)
        {
            this.Mnemonic = mnemonic;
            this.Operand1 = operand1;
            this.Operand2 = operand2;
            this.Operand3 = operand3;
            this.Operand4 = operand4;
            this.Prefix = prefix;
        }
    }
    //// End class
}
//// End namespace