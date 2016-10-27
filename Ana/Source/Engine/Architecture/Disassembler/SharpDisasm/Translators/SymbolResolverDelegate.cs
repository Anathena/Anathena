namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Translators
{
    using System;

    /// <summary>
    /// Allows an address to be resolved to a symbol name
    /// </summary>
    /// <param name="instruction">The instruction being translated</param>
    /// <param name="addr">The address to resolve to a symbol</param>
    /// <param name="offset">An optional offset for the symbol. E.g. if the symbol is located at 0x100 and <paramref name="addr"/> is 0x101, the offset can be set to 0x001 and the symbol can be output with an offset i.e. MYSYMBOL:0x001</param>
    /// <returns>If a symbol is located at the address return the symbol name, otherwise null or <see cref="String.Empty"/>.</returns>
    internal delegate String SymbolResolverDelegate(Instruction instruction, Int64 addr, ref Int64 offset);
}
//// End namespace