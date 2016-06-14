using System.Collections.Generic;

namespace DirectXHook.Hook.Common
{
    internal interface IOverlay : IOverlayElement
    {
        List<IOverlayElement> Elements { get; set; }

    } // End class

} // End namespace