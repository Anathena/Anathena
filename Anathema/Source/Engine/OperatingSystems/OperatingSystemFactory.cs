using Anathema.Source.Engine.OperatingSystems.Windows;
using System.Diagnostics;

namespace Anathema.Source.Engine.OperatingSystems
{
    class OperatingSystemFactory
    {

        public static IOperatingSystem GetOperatingSystem(Process Target)
        {
            return new WindowsOperatingSystem(Target);
        }

    } // End class

} // End namespace