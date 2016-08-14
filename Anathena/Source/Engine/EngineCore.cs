using Anathena.Source.Engine.Architecture.Assembler;
using Anathena.Source.Engine.Architecture.Disassembler;
using Anathena.Source.Engine.Hook.Client;
using Anathena.Source.Engine.InputCapture;
using Anathena.Source.Engine.OperatingSystems;
using System.Diagnostics;

namespace Anathena.Source.Engine
{
    /// <summary>
    /// Abstraction of the OS, providing access to assembly functions and target process functions
    /// </summary>
    public class EngineCore
    {
        public IOperatingSystem Memory { get; private set; }

        public IAssembler Assembler { get; private set; }

        public IDisassembler Disassembler { get; private set; }

        public IInputManager InputManager { get; private set; }

        /// <summary>
        /// TODO: Replace this with IGraphicsInterface, ISpeedHack, IUnrandomizer, etc instead of the
        /// hook creator that initializes them. Instead the hook creator can be lazy initialized when any
        /// of the child processes are accessed
        /// </summary>
        public IHookClient HookCreator { get; private set; }

        public EngineCore(Process TargetProcess)
        {
            Memory = OperatingSystemFactory.GetOperatingSystem(TargetProcess);
            Assembler = AssemblerFactory.GetAssembler();
            Disassembler = DisassemblerFactory.GetDisassembler();
            InputManager = new InputManager();
            HookCreator = new HookClient();
        }

    } // End interface

} // End namespace