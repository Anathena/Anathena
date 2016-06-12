using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Capture.Interface
{
    public static class ScreenshotExtensions
    {
        public static Bitmap ToBitmap(this byte[] data, int width, int height, int stride, System.Drawing.Imaging.PixelFormat pixelFormat)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                var img = new Bitmap(width, height, stride, pixelFormat, handle.AddrOfPinnedObject());
                return img;
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();
            }
        }

        public static Bitmap ToBitmap(this Screenshot screenshot)
        {
            if (screenshot.Format == ImageFormat.PixelData)
            {
                return screenshot.Data.ToBitmap(screenshot.Width, screenshot.Height, screenshot.Stride, screenshot.PixelFormat);
            }
            else
            {
                return screenshot.Data.ToBitmap();
            }
        }

        public static Bitmap ToBitmap(this byte[] imageBytes)
        {
            // Note: deliberately not disposing of MemoryStream, it doesn't have any unmanaged resources anyway and the GC 
            //       will deal with it. This fixes GitHub issue #19 (https://github.com/spazzarama/Direct3DHook/issues/19).
            MemoryStream ms = new MemoryStream(imageBytes);
            try
            {
                Bitmap image = (Bitmap)Image.FromStream(ms);
                return image;
            }
            catch
            {
                return null;
            }
        }

        public static byte[] ToByteArray(this Image img, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, format);
                stream.Close();
                return stream.ToArray();
            }
        }
    }
}
