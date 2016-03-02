using System;

namespace Anathema
{
    /// <summary>
    /// Defines an OS independent module region and attributes
    /// </summary>
    public class NormalizedModule : NormalizedRegion
    {
        public String Name;

        public NormalizedModule(String Name, IntPtr Base, Int32 Size) : base(Base, Size)
        {
            this.Name = Name;
        }

    } // End interface

} // End namespace