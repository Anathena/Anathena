using Anathema.Source.SystemInternals.OperatingSystems.Windows;
using System.Diagnostics;

namespace Anathema.Source.SystemInternals.OperatingSystems
{
    class OperatingSystemFactory
    {

        public static IOperatingSystem GetOperatingSystem(Process Target)
        {
            return new WindowsOperatingSystem(Target);
        }

    } // End class

} // End namespace