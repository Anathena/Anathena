using Anathema.Source.SystemInternals.Graphics.DirectXHook.Interface;
using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook
{
    internal interface IDXHook : IDisposable
    {
        ClientInterface CaptureInterface { get; set; }
        void Hook();
        void Cleanup();

    } // End class

} // End namespace