using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface.Common
{
    public interface IOverlayElement : ICloneable
    {
        Boolean Hidden { get; set; }

        void Frame();

    } // End class

} // End namespace