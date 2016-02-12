using Anathema.MemoryManagement;
using System;
using System.Diagnostics;

namespace Anathema
{
    /// <summary>
    /// Specifies the interface requir
    /// </summary>
    public class FUTUREOSInterface
    {
        public Architecture Architecture { get; private set; }

        public FUTUREIOperatingSystemInterface Process { get; private set; }

        public FUTUREOSInterface(Process TargetProcess)
        {
            Architecture = new Architecture();

            Process = new WindowsOSInterface(TargetProcess);
        }

    } // End interface

} // End namespace