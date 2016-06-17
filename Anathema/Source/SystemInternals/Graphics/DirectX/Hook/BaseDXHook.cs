using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using EasyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Threading;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Hook
{
    internal abstract class BaseDXHook : SharpDX.Component, IDXHook
    {
        protected readonly ClientCaptureInterfaceEventProxy InterfaceEventProxy = new ClientCaptureInterfaceEventProxy();

        /// <summary>
        /// Frames Per second counter, FPS.Frame() must be called each frame
        /// </summary>
        protected Stopwatch Timer { get; set; }
        protected TextDisplay TextDisplay { get; set; }
        protected List<Hook> Hooks = new List<Hook>();
        public DirextXGraphicsInterface GraphicsInterface { get; set; }

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

        protected virtual String HookName { get { return "BaseDXHook"; } }

        public BaseDXHook(DirextXGraphicsInterface GraphicsInterface)
        {
            this.GraphicsInterface = GraphicsInterface;

            Timer = new Stopwatch();
            Timer.Start();

            this.GraphicsInterface.DisplayText += InterfaceEventProxy.DisplayTextProxyHandler;

            InterfaceEventProxy.DisplayText += new DisplayTextEvent(InterfaceEventProxyDisplayText);
        }

        ~BaseDXHook()
        {
            Dispose(false);
        }

        void InterfaceEventProxyDisplayText(DisplayTextEventArgs Args)
        {
            TextDisplay = new TextDisplay()
            {
                Text = Args.Text,
                Duration = Args.Duration
            };
        }

        protected void Frame()
        {
            if (TextDisplay != null && TextDisplay.Display)
                TextDisplay.Frame();
        }

        protected void DebugMessage(String Message)
        {

#if DEBUG
            try
            {
                GraphicsInterface.Message(MessageType.Debug, HookName + ": " + Message);
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
                            Hook.Deactivate();

                        Thread.Sleep(100);

                        // Now we can dispose of the hooks (which triggers the removal of the hook)
                        foreach (Hook Hook in Hooks)
                            Hook.Dispose();

                        Hooks.Clear();
                    }

                    try
                    {
                        // Remove the event handlers
                        GraphicsInterface.DisplayText -= InterfaceEventProxy.DisplayTextProxyHandler;
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