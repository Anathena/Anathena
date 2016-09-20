using Ana.Source.Engine.Architecture.Assembler;
using Ana.Source.Engine.Architecture.Disassembler;

namespace Ana.Source.Engine.Architecture
{
    public interface IArchitecture
    {
        IDisassembler GetDisassembler();

        IAssembler GetAssembler();
    }
    //// End architecture
}
//// End namespace