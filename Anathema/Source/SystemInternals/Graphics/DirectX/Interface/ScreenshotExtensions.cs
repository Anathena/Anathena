using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook.Interface
{
    public static class ScreenshotExtensions
    {
        public static Bitmap ToBitmap(this Byte[] Data, Int32 Width, Int32 Height, Int32 Stride, PixelFormat PixelFormat)
        {
            GCHandle Handle = GCHandle.Alloc(Data, GCHandleType.Pinned);

            try
            {
                return new Bitmap(Width, Height, Stride, PixelFormat, Handle.AddrOfPinnedObject());
            }
            finally
            {
                if (Handle.IsAllocated)
                    Handle.Free();
            }
        }

        public static Bitmap ToBitmap(this Screenshot Screenshot)
        {
            if (Screenshot.Format == ImageFormatEnum.PixelData)
            {
                return Screenshot.Data.ToBitmap(Screenshot.Width, Screenshot.Height, Screenshot.Stride, Screenshot.PixelFormat);
            }
            else
            {
                return Screenshot.Data.ToBitmap();
            }
        }

        public static Bitmap ToBitmap(this Byte[] ImageBytes)
        {
            // Note: deliberately not disposing of MemoryStream, it doesn't have any unmanaged resources anyway and the GC 
            //       will deal with it. This fixes GitHub issue #19 (https://github.com/spazzarama/Direct3DHook/issues/19).
            MemoryStream Stream = new MemoryStream(ImageBytes);

            try
            {
                return (Bitmap)Image.FromStream(Stream);
            }
            catch
            {
                return null;
            }
        }

        public static byte[] ToByteArray(this Image Image, ImageFormat Format)
        {
            using (MemoryStream Stream = new MemoryStream())
            {
                Image.Save(Stream, Format);
                Stream.Close();
                return Stream.ToArray();
            }
        }

    } // End class

} // End namespace