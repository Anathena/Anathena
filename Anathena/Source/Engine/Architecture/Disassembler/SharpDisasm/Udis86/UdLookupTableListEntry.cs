namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;

    public struct UdLookupTableListEntry
    {
        public UdLookupTableListEntry(UInt16[] table, UdTableType tableType, String meta)
        {
            this.Table = table;
            this.TableType = tableType;
            this.Meta = meta;
        }

        public UInt16[] Table { get; set; }

        public UdTableType TableType { get; set; }

        public String Meta { get; set; }
    }
    //// End struct
}
//// End namespace