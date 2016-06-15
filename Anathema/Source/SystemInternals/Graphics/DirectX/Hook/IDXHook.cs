using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Hook
{
    internal interface IDXHook : IDisposable
    {
        ClientInterface CaptureInterface { get; set; }
        void Hook();
        void Cleanup();

    } // End class

} // End namespace