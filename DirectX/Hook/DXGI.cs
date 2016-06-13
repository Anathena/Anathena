using SharpDX.DXGI;
using System;

namespace DirectXShell.Hook
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

        /*
         * 
        typedef enum DXGI_MODE_SCANLINE_ORDER
        {
        DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED        = 0,
        DXGI_MODE_SCANLINE_ORDER_PROGRESSIVE        = 1,
        DXGI_MODE_SCANLINE_ORDER_UPPER_FIELD_FIRST  = 2,
        DXGI_MODE_SCANLINE_ORDER_LOWER_FIELD_FIRST  = 3
        } DXGI_MODE_SCANLINE_ORDER;

        typedef enum DXGI_MODE_SCALING
        {
        DXGI_MODE_SCALING_UNSPECIFIED   = 0,
        DXGI_MODE_SCALING_CENTERED      = 1,
        DXGI_MODE_SCALING_STRETCHED     = 2
        } DXGI_MODE_SCALING;

        typedef enum DXGI_MODE_ROTATION
        {
        DXGI_MODE_ROTATION_UNSPECIFIED  = 0,
        DXGI_MODE_ROTATION_IDENTITY     = 1,
        DXGI_MODE_ROTATION_ROTATE90     = 2,
        DXGI_MODE_ROTATION_ROTATE180    = 3,
        DXGI_MODE_ROTATION_ROTATE270    = 4
        } DXGI_MODE_ROTATION;

        typedef struct DXGI_MODE_DESC
        {
        UINT Width;
        UINT Height;
        DXGI_RATIONAL RefreshRate;
        DXGI_FORMAT Format;
        DXGI_MODE_SCANLINE_ORDER ScanlineOrdering;
        DXGI_MODE_SCALING Scaling;
        } DXGI_MODE_DESC;
         * */

    } // End class

} // End namespace