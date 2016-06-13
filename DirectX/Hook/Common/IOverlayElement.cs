using System;

namespace DirectXShell.Hook.Common
{
    public interface IOverlayElement : ICloneable
    {
        Boolean Hidden { get; set; }

        void Frame();

    } // End class

} // End namespace