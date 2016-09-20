using Ana.Source.Engine.Architecture.Assembler;
using Ana.Source.Engine.Architecture.Disassembler;
using Ana.Source.Engine.OperatingSystems;
using System.Diagnostics;

namespace Ana.Source.Engine
{
    /// <summary>
    /// Abstraction of the OS, providing access to assembly functions and target process functions
    /// </summary>
    public class EngineCore
    {
        public IOperatingSystem Memory { get; private set; }

        public IAssembler Assembler { get; private set; }

        public IDisassembler Disassembler { get; private set; }

        public EngineCore(Process TargetProcess)
        {
            Memory = OperatingSystemFactory.GetOperatingSystem(TargetProcess);
            Assembler = AssemblerFactory.GetAssembler();
            Disassembler = DisassemblerFactory.GetDisassembler();
        }

    } // End interface

} // End namespace