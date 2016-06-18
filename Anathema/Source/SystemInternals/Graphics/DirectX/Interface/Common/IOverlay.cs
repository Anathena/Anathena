using System.Collections.Generic;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface.Common
{
    internal interface IOverlay : IOverlayElement
    {
        List<IOverlayElement> Elements { get; set; }

    } // End class

} // End namespace