using SharpDX.DXGI;
using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook
{
    internal static class DXGI
    {
        public enum DXGISwapChainVirtualTableEnum : Int16
        {
            // IUnknown
            QueryInterface = 0,
            AddRef = 1,
            Release = 2,

            // IDXGIObject
            SetPrivateData = 3,
            SetPrivateDataInterface = 4,
            GetPrivateData = 5,
            GetParent = 6,

            // IDXGIDeviceSubObject
            GetDevice = 7,

            // IDXGISwapChain
            Present = 8,
            GetBuffer = 9,
            SetFullscreenState = 10,
            GetFullscreenState = 11,
            GetDesc = 12,
            ResizeBuffers = 13,
            ResizeTarget = 14,
            GetContainingOutput = 15,
            GetFrameStatistics = 16,
            GetLastPresentCount = 17,
        }

        public static Int32 DXGI_SWAPCHAIN_METHOD_COUNT = Enum.GetNames(typeof(DXGISwapChainVirtualTableEnum)).Length;

        public static SwapChainDescription CreateSwapChainDescription(IntPtr WindowHandle)
        {
            return new SwapChainDescription
            {
                BufferCount = 1,
                Flags = SwapChainFlags.None,
                IsWindowed = true,
                ModeDescription = new ModeDescription(100, 100, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                OutputHandle = WindowHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
        }

    } // End class

} // End namespace