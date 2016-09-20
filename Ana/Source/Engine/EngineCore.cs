using Ana.Source.Engine.Architecture;
using Ana.Source.Engine.OperatingSystems;
using Ana.Source.Engine.SpeedManipulator;
using Ana.Source.Engine.Unrandomizer;
using System.Diagnostics;

namespace Ana.Source.Engine
{
    /// <summary>
    /// Abstraction of the OS, providing access to assembly functions and target process functions
    /// </summary>
    public class EngineCore
    {
        public IOperatingSystemAdapter Memory { get; private set; }

        public IArchitecture Architecture { get; private set; }

        public ISpeedManipulator SpeedManipulator { get; private set; }

        public IUnrandomizer Unrandomizer { get; private set; }

        // public IGraphics Graphics { get; private set; }

        // public IInput Input { get; private set; }

        public EngineCore(Process TargetProcess)
        {
            Architecture = ArchitectureFactory.GetArchitecture();
            Memory = OperatingSystemAdapterFactory.GetOperatingSystemAdapter(TargetProcess);
        }

    } // End interface

} // End namespace