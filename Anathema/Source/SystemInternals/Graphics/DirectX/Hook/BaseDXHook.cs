using Anathema.Source.SystemInternals.Graphics.DirectXHook.Interface;
using EasyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook
{
    internal abstract class BaseDXHook : SharpDX.Component, IDXHook
    {
        protected readonly ClientCaptureInterfaceEventProxy InterfaceEventProxy = new ClientCaptureInterfaceEventProxy();

        /// <summary>
        /// Frames Per second counter, FPS.Frame() must be called each frame
        /// </summary>
        protected FramesPerSecond FPS { get; set; }
        protected Stopwatch Timer { get; set; }
        protected TextDisplay TextDisplay { get; set; }
        protected TimeSpan LastCaptureTime { get; set; }

        protected List<Hook> Hooks = new List<Hook>();


        protected TimeSpan CaptureDelay { get; set; }
        public ClientInterface CaptureInterface { get; set; }
        private CaptureConfig _Config;
        public CaptureConfig Config
        {
            get { return _Config; }
            set
            {
                _Config = value;
                CaptureDelay = new TimeSpan(0, 0, 0, 0, (Int32)((1.0 / (Double)_Config.TargetFramesPerSecond) * 1000.0));
            }
        }

        private ScreenshotRequest _Request;
        public ScreenshotRequest Request
        {
            get { return _Request; }
            set { Interlocked.Exchange(ref _Request, value); }
        }


        private Int32 _ProcessId = 0;
        protected Int32 ProcessId
        {
            get
            {
                if (_ProcessId == 0)
                    _ProcessId = RemoteHooking.GetCurrentProcessId();
                return _ProcessId;
            }
        }

        protected virtual string HookName { get { return "BaseDXHook"; } }

        public BaseDXHook(ClientInterface CaptureInterface)
        {
            this.CaptureInterface = CaptureInterface;

            Timer = new Stopwatch();
            Timer.Start();
            FPS = new FramesPerSecond();

            this.CaptureInterface.ScreenshotRequested += InterfaceEventProxy.ScreenshotRequestedProxyHandler;
            this.CaptureInterface.DisplayText += InterfaceEventProxy.DisplayTextProxyHandler;

            InterfaceEventProxy.ScreenshotRequested += new ScreenshotRequestedEvent(InterfaceEventProxy_ScreenshotRequested);
            InterfaceEventProxy.DisplayText += new DisplayTextEvent(InterfaceEventProxy_DisplayText);
        }

        ~BaseDXHook()
        {
            Dispose(false);
        }

        void InterfaceEventProxy_DisplayText(DisplayTextEventArgs Args)
        {
            TextDisplay = new TextDisplay()
            {
                Text = Args.Text,
                Duration = Args.Duration
            };
        }

        protected virtual void InterfaceEventProxy_ScreenshotRequested(ScreenshotRequest Request)
        {

            this.Request = Request;
        }

        protected void Frame()
        {
            FPS.Frame();

            if (TextDisplay != null && TextDisplay.Display)
                TextDisplay.Frame();
        }

        protected void DebugMessage(String Message)
        {

#if DEBUG
            try
            {
                CaptureInterface.Message(MessageType.Debug, HookName + ": " + Message);
            }
            catch (RemotingException)
            {
                // Ignore remoting exceptions
            }
#endif
        }

        protected IntPtr[] GetVirtualTableAddresses(IntPtr Pointer, Int32 NumberOfMethods)
        {
            return GetVirtualTableAddresses(Pointer, 0, NumberOfMethods);
        }

        protected IntPtr[] GetVirtualTableAddresses(IntPtr Pointer, Int32 StartIndex, Int32 NumberOfMethods)
        {
            List<IntPtr> VirtualTableAddresses = new List<IntPtr>();
            IntPtr VirtualTablePtr = Marshal.ReadIntPtr(Pointer);

            for (Int32 Index = StartIndex; Index < StartIndex + NumberOfMethods; Index++)
                VirtualTableAddresses.Add(Marshal.ReadIntPtr(VirtualTablePtr, Index * IntPtr.Size)); // using IntPtr.Size allows us to support both 32 and 64-bit processes

            return VirtualTableAddresses.ToArray();
        }

        protected static void CopyStream(Stream InputStream, Stream OutputStream)
        {
            Int32 BufferSize = 32768;
            Byte[] Buffer = new Byte[BufferSize];

            while (true)
            {
                Int32 BytesRead = InputStream.Read(Buffer, 0, Buffer.Length);

                if (BytesRead <= 0)
                    return;

                OutputStream.Write(Buffer, 0, BytesRead);
            }
        }

        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="Stream">The stream to read data from</param>
        protected static Byte[] ReadFullStream(Stream Stream)
        {
            if (Stream is MemoryStream)
            {
                return ((MemoryStream)Stream).ToArray();
            }

            Byte[] buffer = new Byte[32768];

            using (MemoryStream MemoryStream = new MemoryStream())
            {
                while (true)
                {
                    Int32 BytesRead = Stream.Read(buffer, 0, buffer.Length);

                    if (BytesRead > 0)
                        MemoryStream.Write(buffer, 0, BytesRead);

                    if (BytesRead < buffer.Length)
                        return MemoryStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Process the capture based on the requested format.
        /// </summary>
        /// <param name="Width">image width</param>
        /// <param name="Height">image height</param>
        /// <param name="Pitch">data pitch (bytes per row)</param>
        /// <param name="Format">target format</param>
        /// <param name="PBits">IntPtr to the image data</param>
        /// <param name="Request">The original requets</param>
        protected void ProcessCapture(Int32 Width, Int32 Height, Int32 Pitch, PixelFormat Format, IntPtr PBits, ScreenshotRequest Request)
        {
            if (Request == null)
                return;

            if (Format == PixelFormat.Undefined)
            {
                DebugMessage("Unsupported render target format");
                return;
            }

            // Copy the image data from the buffer
            Int32 Size = Height * Pitch;
            Byte[] Data = new Byte[Size];
            Marshal.Copy(PBits, Data, 0, Size);

            // Prepare the response
            Screenshot Response = null;

            if (Request.Format == ImageFormatEnum.PixelData)
            {
                // Return the raw data
                Response = new Screenshot(Request.RequestId, Data)
                {
                    Format = Request.Format,
                    PixelFormat = Format,
                    Height = Height,
                    Width = Width,
                    Stride = Pitch
                };
            }
            else
            {
                // Return an image
                using (Bitmap Bitmap = Data.ToBitmap(Width, Height, Pitch, Format))
                {
                    ImageFormat ImageFormat = ImageFormat.Bmp;
                    switch (Request.Format)
                    {
                        case ImageFormatEnum.Jpeg:
                            ImageFormat = ImageFormat.Jpeg;
                            break;

                        case ImageFormatEnum.Png:
                            ImageFormat = ImageFormat.Png;
                            break;
                    }

                    Response = new Screenshot(Request.RequestId, Bitmap.ToByteArray(ImageFormat))
                    {
                        Format = Request.Format,
                        Height = Bitmap.Height,
                        Width = Bitmap.Width
                    };
                }
            }

            // Send the response
            SendResponse(Response);
        }

        protected void SendResponse(Screenshot Response)
        {
            Task.Run(() =>
            {
                try
                {
                    CaptureInterface.SendScreenshotResponse(Response);
                    LastCaptureTime = Timer.Elapsed;
                }
                catch (RemotingException)
                {
                    // Ignore remoting exceptions
                    // .NET Remoting will throw an exception if the host application is unreachable
                }
                catch (Exception Ex)
                {
                    DebugMessage(Ex.ToString());
                }
            });
        }

        protected void ProcessCapture(Stream Stream, ScreenshotRequest Request)
        {
            ProcessCapture(ReadFullStream(Stream), Request);
        }

        protected void ProcessCapture(Byte[] BitmapData, ScreenshotRequest Request)
        {
            try
            {
                if (Request != null)
                {
                    CaptureInterface.SendScreenshotResponse(new Screenshot(Request.RequestId, BitmapData)
                    {
                        Format = Request.Format,
                    });
                }

                LastCaptureTime = Timer.Elapsed;
            }
            catch (RemotingException)
            {
                // Ignore remoting exceptions
                // .NET Remoting will throw an exception if the host application is unreachable
            }
            catch (Exception Ex)
            {
                DebugMessage(Ex.ToString());
            }
        }


        private ImageCodecInfo GetEncoder(ImageFormat Format)
        {
            ImageCodecInfo[] Codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo Codec in Codecs)
            {
                if (Codec.FormatID == Format.Guid)
                    return Codec;
            }

            return null;
        }

        private Bitmap BitmapFromBytes(Byte[] BitmapData)
        {
            using (MemoryStream MemoryStream = new MemoryStream(BitmapData))
            {
                return (Bitmap)Image.FromStream(MemoryStream);
            }
        }

        protected Boolean CaptureThisFrame
        {
            get
            {
                return ((Timer.Elapsed - LastCaptureTime) > CaptureDelay) || Request != null;
            }
        }

        public abstract void Hook();

        public abstract void Cleanup();

        #region IDispose Implementation

        protected override void Dispose(Boolean DisposeManagedResources)
        {
            // Only clean up managed objects if disposing (i.e. not called from destructor)
            if (DisposeManagedResources)
            {
                try
                {
                    Cleanup();
                }
                catch { }

                try
                {
                    // Uninstall Hooks
                    if (Hooks.Count > 0)
                    {
                        // First disable the hook (by excluding all threads) and wait long enough to ensure that all hooks are not active
                        foreach (Hook Hook in Hooks)
                        {
                            // Lets ensure that no threads will be intercepted again
                            Hook.Deactivate();
                        }

                        Thread.Sleep(100);

                        // Now we can dispose of the hooks (which triggers the removal of the hook)
                        foreach (Hook Hook in Hooks)
                        {
                            Hook.Dispose();
                        }

                        Hooks.Clear();
                    }

                    try
                    {
                        // Remove the event handlers
                        CaptureInterface.ScreenshotRequested -= InterfaceEventProxy.ScreenshotRequestedProxyHandler;
                        CaptureInterface.DisplayText -= InterfaceEventProxy.DisplayTextProxyHandler;
                    }
                    catch (RemotingException) { } // Ignore remoting exceptions (host process may have been closed)
                }
                catch
                {

                }
            }

            base.Dispose(DisposeManagedResources);
        }

        #endregion

    } // End class

} // End namespace