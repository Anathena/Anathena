using DirectXHook.Interface;
using System;

namespace DirectXHook.Hook
{
    internal interface IDXHook : IDisposable
    {
        ClientInterface CaptureInterface { get; set; }
        CaptureConfig Config { get; set; }
        ScreenshotRequest Request { get; set; }

        void Hook();
        void Cleanup();

    } // End class

} // End namespace