using Anathema.Source.Graphics;
using Capture;
using Capture.Hook;
using Capture.Interface;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Anathema.Source.SystemInternals.Graphics.DirectX
{
    class GraphicsDirextX : IGraphicsInterface
    {
        CaptureProcess DirectXShell;

        public GraphicsDirextX()
        {

        }

        public void Inject(Process Process)
        {
            if (DirectXShell == null)
            {
                // Must be running as Administrator to allow dynamic registration in GAC
                // Config.Register("Capture", "Capture.dll");

                // Skip if the process is already hooked (and we want to hook multiple applications)
                if (Process.MainWindowHandle == IntPtr.Zero || HookManager.IsHooked(Process.Id))
                {
                    return;
                }

                Direct3DVersionEnum Direct3DVersion = Direct3DVersionEnum.AutoDetect;

                CaptureConfig CaptureConfig = new CaptureConfig()
                {
                    Direct3DVersion = Direct3DVersion,
                    ShowOverlay = true
                };

                CaptureInterface CaptureInterface = new CaptureInterface();
                CaptureInterface.RemoteMessage += new MessageReceivedEvent(CaptureInterface_RemoteMessage);
                DirectXShell = new CaptureProcess(Process, CaptureConfig, CaptureInterface);
            }
            else
            {
                HookManager.RemoveHookedProcess(DirectXShell.Process.Id);
                DirectXShell.CaptureInterface.Disconnect();
                DirectXShell = null;
            }

        }

        public void DrawLine(int StartX, int StartY, int EndX, int EndY)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Display messages from the target process
        /// </summary>
        /// <param name="Message"></param>
        void CaptureInterface_RemoteMessage(MessageReceivedEventArgs Message)
        {
            // k
        }

        /// <summary>
        /// Display debug messages from the target process
        /// </summary>
        /// <param name="ClientPID"></param>
        /// <param name="message"></param>
        void ScreenshotManager_OnScreenshotDebugMessage(Int32 ClientPID, String Message)
        {
            // k
        }

        /// <summary>
        /// Create the screen shot request
        /// </summary>
        void DoRequest()
        {
            Size? resize = new Size(1000, 1000);

            DirectXShell.BringProcessWindowToFront();
            DirectXShell.CaptureInterface.BeginGetScreenshot(new Rectangle(0, 0, 1000, 100),
                new TimeSpan(0, 0, 2), Callback, resize, ImageFormat.Bitmap);
        }

        /// <summary>
        /// The callback for when the screenshot has been taken
        /// </summary>
        /// <param name="clientPID"></param>
        /// <param name="status"></param>
        /// <param name="screenshotResponse"></param>
        void Callback(IAsyncResult result)
        {
            using (Screenshot Screenshot = DirectXShell.CaptureInterface.EndGetScreenshot(result))

                try
                {
                    DirectXShell.CaptureInterface.DisplayInGameText("Screenshot captured...");

                    if (Screenshot != null && Screenshot.Data != null)
                    {

                    }

                    Thread Thread = new Thread(new ThreadStart(DoRequest));
                    Thread.Start();
                }
                catch
                {

                }
        }

    } // End class

} // End namespace