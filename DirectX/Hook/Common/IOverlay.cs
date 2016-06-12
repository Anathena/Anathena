using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;

namespace Capture.Hook.Common
{
    internal interface IOverlay: IOverlayElement
    {
        List<IOverlayElement> Elements { get; set; }
    }
}
