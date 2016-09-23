namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    /// <summary>
    /// TODO TODO
    /// </summary>
    public struct UdLookupTableListEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UdLookupTableListEntry" /> struct
        /// </summary>
        /// <param name="table">TODO table</param>
        /// <param name="tableType">TODO tableType</param>
        /// <param name="meta">TODO meta</param>
        public UdLookupTableListEntry(UInt16[] table, UdTableType tableType, String meta)
        {
            this.Table = table;
            this.TableType = tableType;
            this.Meta = meta;
        }

        /// <summary>
        /// Gets or sets TODO TODO
        /// </summary>
        public UInt16[] Table { get; set; }

        /// <summary>
        /// Gets or sets the table type
        /// </summary>
        public UdTableType TableType { get; set; }

        /// <summary>
        /// Gets or sets TODO TODO
        /// </summary>
        public String Meta { get; set; }
    }
    //// End struct
}
//// End namespace