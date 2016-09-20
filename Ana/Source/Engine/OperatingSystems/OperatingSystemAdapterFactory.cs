using Ana.Source.Engine.OperatingSystems.Windows;
using System.Diagnostics;

namespace Ana.Source.Engine.OperatingSystems
{
    class OperatingSystemAdapterFactory
    {
        public static IOperatingSystemAdapter GetOperatingSystemAdapter(Process Target)
        {
            return new WindowsAdapter(Target);
        }

    } // End class

} // End namespace