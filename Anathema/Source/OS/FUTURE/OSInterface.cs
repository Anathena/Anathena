using System;

namespace Anathema
{
    /// <summary>
    /// Specifies the interface requir
    /// </summary>
    public class OSInterface
    {
        public Architecture Architecture { get; private set; }

        public IOperatingSystemInterface Process { get; private set; }

        public OSInterface()
        {
            Architecture = new Architecture();
            
        }

    } // End interface

} // End namespace