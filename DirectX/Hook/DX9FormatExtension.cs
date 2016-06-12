using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capture.Hook
{
    public static class DX9FormatExtension
    {

        public static int ToPixelDepth(this SharpDX.Direct3D9.Format format)
        {
            // Only support the DX9 BackBuffer formats: http://msdn.microsoft.com/en-us/library/windows/desktop/bb172558(v=vs.85).aspx
            switch (format)
            {
                case SharpDX.Direct3D9.Format.A2R10G10B10:
                case SharpDX.Direct3D9.Format.A8R8G8B8:
                case SharpDX.Direct3D9.Format.X8R8G8B8:
                    return 32;
                case SharpDX.Direct3D9.Format.R5G6B5:
                case SharpDX.Direct3D9.Format.A1R5G5B5:
                case SharpDX.Direct3D9.Format.X1R5G5B5:
                    return 16;
                default:
                    return -1;
            }
        }
        
        public static System.Drawing.Imaging.PixelFormat ToPixelFormat(this SharpDX.Direct3D9.Format format)
        {
            // Only support the BackBuffer formats: http://msdn.microsoft.com/en-us/library/windows/desktop/bb172558(v=vs.85).aspx
            // and of these only those that have a direct mapping to supported PixelFormat's
            switch (format)
            {
                case SharpDX.Direct3D9.Format.A8R8G8B8:
                case SharpDX.Direct3D9.Format.X8R8G8B8:
                    return System.Drawing.Imaging.PixelFormat.Format32bppArgb;
                case SharpDX.Direct3D9.Format.R5G6B5:
                    return System.Drawing.Imaging.PixelFormat.Format16bppRgb565;
                case SharpDX.Direct3D9.Format.A1R5G5B5:
                case SharpDX.Direct3D9.Format.X1R5G5B5:
                    return System.Drawing.Imaging.PixelFormat.Format16bppArgb1555;
                default:
                    return System.Drawing.Imaging.PixelFormat.Undefined;
            }
        }
    }
}
