using SharpDX.Direct3D9;
using System;
using System.Drawing.Imaging;

namespace DirectXHook.Hook
{
    public static class DX9FormatExtension
    {
        public static Int32 ToPixelDepth(this Format Format)
        {
            // Only support the DX9 BackBuffer formats: http://msdn.microsoft.com/en-us/library/windows/desktop/bb172558(v=vs.85).aspx
            switch (Format)
            {
                case Format.A2R10G10B10:
                case Format.A8R8G8B8:
                case Format.X8R8G8B8:
                    return 32;
                case Format.R5G6B5:
                case Format.A1R5G5B5:
                case Format.X1R5G5B5:
                    return 16;
                default:
                    return -1;
            }
        }

        public static PixelFormat ToPixelFormat(this Format Format)
        {
            // Only support the BackBuffer formats: http://msdn.microsoft.com/en-us/library/windows/desktop/bb172558(v=vs.85).aspx
            // and of these only those that have a direct mapping to supported PixelFormat's
            switch (Format)
            {
                case Format.A8R8G8B8:
                case Format.X8R8G8B8:
                    return PixelFormat.Format32bppArgb;
                case Format.R5G6B5:
                    return PixelFormat.Format16bppRgb565;
                case Format.A1R5G5B5:
                case Format.X1R5G5B5:
                    return PixelFormat.Format16bppArgb1555;
                default:
                    return PixelFormat.Undefined;
            }
        }

    } // End class

} // End namespace