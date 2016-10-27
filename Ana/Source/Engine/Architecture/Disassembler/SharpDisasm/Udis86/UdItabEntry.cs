namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    /// <summary>
    /// A single entry within an instruction table
    /// </summary>
    internal class UdItabEntry
    {
        /// <summary>
        /// TODO TODO
        /// </summary>
        public readonly UdMnemonicCode Mnemonic;

        /// <summary>
        /// TODO TODO
        /// </summary>
        public readonly UdItabEntryOperand Operand1;

        /// <summary>
        /// TODO TODO
        /// </summary>
        public readonly UdItabEntryOperand Operand2;

        /// <summary>
        /// TODO TODO
        /// </summary>
        public readonly UdItabEntryOperand Operand3;

        /// <summary>
        /// TODO TODO
        /// </summary>
        public readonly UdItabEntryOperand Operand4;

        /// <summary>
        /// TODO TODO
        /// </summary>
        public readonly UInt32 Prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="UdItabEntry" /> class
        /// </summary>
        internal UdItabEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdItabEntry" /> class
        /// </summary>
        /// <param name="mnemonic">TODO mnemonic</param>
        /// <param name="operand1">TODO operand1</param>
        /// <param name="operand2">TODO operand2</param>
        /// <param name="operand3">TODO operand3</param>
        /// <param name="operand4">TODO operand4</param>
        /// <param name="prefix">TODO prefix</param>
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