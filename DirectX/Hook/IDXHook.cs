using DirectXShell.Interface;
using System;

namespace DirectXShell.Hook
{
    internal interface IDXHook : IDisposable
    {
        CaptureInterface CaptureInterface { get; set; }
        CaptureConfig Config { get; set; }
        ScreenshotRequest Request { get; set; }

        void Hook();
        void Cleanup();

    } // End class

} // End namespace