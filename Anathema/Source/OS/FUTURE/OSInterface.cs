using System;

namespace Anathema
{
    /// <summary>
    /// Specifies the interface requir
    /// </summary>
    public class FUTUREOSInterface
    {
        public Architecture Architecture { get; private set; }

        public FUTUREIOperatingSystemInterface Process { get; private set; }

        public FUTUREOSInterface()
        {
            Architecture = new Architecture();
            
        }

    } // End interface

} // End namespace