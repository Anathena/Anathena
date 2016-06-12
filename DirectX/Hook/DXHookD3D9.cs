using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SlimDX.Direct3D9;
using EasyHook;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Drawing;
using Capture.Interface;
using SharpDX.Direct3D9;

namespace Capture.Hook
{
    internal class DXHookD3D9: BaseDXHook
    {
        public DXHookD3D9(CaptureInterface ssInterface)
            : base(ssInterface)
        {
        }

        Hook<Direct3D9Device_EndSceneDelegate> Direct3DDevice_EndSceneHook = null;
        Hook<Direct3D9Device_ResetDelegate> Direct3DDevice_ResetHook = null;
        Hook<Direct3D9Device_PresentDelegate> Direct3DDevice_PresentHook = null;
        Hook<Direct3D9DeviceEx_PresentExDelegate> Direct3DDeviceEx_PresentExHook = null;
        object _lockRenderTarget = new object();

        bool _resourcesInitialised;
        Query _query;
        SharpDX.Direct3D9.Font _font;
        bool _queryIssued;
        ScreenshotRequest _requestCopy;
        bool _renderTargetCopyLocked = false;
        Surface _renderTargetCopy;
        Surface _resolvedTarget;

        protected override string HookName
        {
            get
            {
                return "DXHookD3D9";
            }
        }

        List<IntPtr> id3dDeviceFunctionAddresses = new List<IntPtr>();
        //List<IntPtr> id3dDeviceExFunctionAddresses = new List<IntPtr>();
        const int D3D9_DEVICE_METHOD_COUNT = 119;
        const int D3D9Ex_DEVICE_METHOD_COUNT = 15;
        bool _supportsDirect3D9Ex = false;
        public override void Hook()
        {
            this.DebugMessage("Hook: Begin");
            // First we need to determine the function address for IDirect3DDevice9
            Device device;
            id3dDeviceFunctionAddresses = new List<IntPtr>();
            //id3dDeviceExFunctionAddresses = new List<IntPtr>();
            this.DebugMessage("Hook: Before device creation");
            using (Direct3D d3d = new Direct3D())
            {
                using (var renderForm = new System.Windows.Forms.Form())
                {
                    using (device = new Device(d3d, 0, DeviceType.NullReference, IntPtr.Zero, CreateFlags.HardwareVertexProcessing, new PresentParameters() { BackBufferWidth = 1, BackBufferHeight = 1, DeviceWindowHandle = renderForm.Handle }))
                    {
                        this.DebugMessage("Hook: Device created");
                        id3dDeviceFunctionAddresses.AddRange(GetVTblAddresses(device.NativePointer, D3D9_DEVICE_METHOD_COUNT));
                    }
                }
            }

            try
            {
                using (Direct3DEx d3dEx = new Direct3DEx())
                {
                    this.DebugMessage("Hook: Direct3DEx...");
                    using (var renderForm = new System.Windows.Forms.Form())
                    {
                        using (var deviceEx = new DeviceEx(d3dEx, 0, DeviceType.NullReference, IntPtr.Zero, CreateFlags.HardwareVertexProcessing, new PresentParameters() { BackBufferWidth = 1, BackBufferHeight = 1, DeviceWindowHandle = renderForm.Handle }, new DisplayModeEx() { Width = 800, Height = 600 }))
                        {
                            this.DebugMessage("Hook: DeviceEx created - PresentEx supported");
                            id3dDeviceFunctionAddresses.AddRange(GetVTblAddresses(deviceEx.NativePointer, D3D9_DEVICE_METHOD_COUNT, D3D9Ex_DEVICE_METHOD_COUNT));
                            _supportsDirect3D9Ex = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                _supportsDirect3D9Ex = false;
            }

            // We want to hook each method of the IDirect3DDevice9 interface that we are interested in

            // 42 - EndScene (we will retrieve the back buffer here)
            Direct3DDevice_EndSceneHook = new Hook<Direct3D9Device_EndSceneDelegate>(
                id3dDeviceFunctionAddresses[(int)Direct3DDevice9FunctionOrdinals.EndScene],
                // On Windows 7 64-bit w/ 32-bit app and d3d9 dll version 6.1.7600.16385, the address is equiv to:
                // (IntPtr)(GetModuleHandle("d3d9").ToInt32() + 0x1ce09),
                // A 64-bit app would use 0xff18
                // Note: GetD3D9DeviceFunctionAddress will output these addresses to a log file
                new Direct3D9Device_EndSceneDelegate(EndSceneHook),
                this);

            unsafe
            {
                // If Direct3D9Ex is available - hook the PresentEx
                if (_supportsDirect3D9Ex)
                {
                    Direct3DDeviceEx_PresentExHook = new Hook<Direct3D9DeviceEx_PresentExDelegate>(
                        id3dDeviceFunctionAddresses[(int)Direct3DDevice9ExFunctionOrdinals.PresentEx],
                        new Direct3D9DeviceEx_PresentExDelegate(PresentExHook),
                        this);
                }

                // Always hook Present also (device will only call Present or PresentEx not both)
                Direct3DDevice_PresentHook = new Hook<Direct3D9Device_PresentDelegate>(
                    id3dDeviceFunctionAddresses[(int)Direct3DDevice9FunctionOrdinals.Present],
                    new Direct3D9Device_PresentDelegate(PresentHook),
                    this);
            }

            // 16 - Reset (called on resolution change or windowed/fullscreen change - we will reset some things as well)
            Direct3DDevice_ResetHook = new Hook<Direct3D9Device_ResetDelegate>(
                id3dDeviceFunctionAddresses[(int)Direct3DDevice9FunctionOrdinals.Reset],
                // On Windows 7 64-bit w/ 32-bit app and d3d9 dll version 6.1.7600.16385, the address is equiv to:
                //(IntPtr)(GetModuleHandle("d3d9").ToInt32() + 0x58dda),
                // A 64-bit app would use 0x3b3a0
                // Note: GetD3D9DeviceFunctionAddress will output these addresses to a log file
                new Direct3D9Device_ResetDelegate(ResetHook),
                this);

            /*
             * Don't forget that all hooks will start deactivated...
             * The following ensures that all threads are intercepted:
             * Note: you must do this for each hook.
             */
            
            Direct3DDevice_EndSceneHook.Activate();
            Hooks.Add(Direct3DDevice_EndSceneHook);

            Direct3DDevice_PresentHook.Activate();
            Hooks.Add(Direct3DDevice_PresentHook);

            if (_supportsDirect3D9Ex)
            {
                Direct3DDeviceEx_PresentExHook.Activate();
                Hooks.Add(Direct3DDeviceEx_PresentExHook);
            }

            Direct3DDevice_ResetHook.Activate();
            Hooks.Add(Direct3DDevice_ResetHook);

            this.DebugMessage("Hook: End");
        }

        /// <summary>
        /// Just ensures that the surface we created is cleaned up.
        /// </summary>
        public override void Cleanup()
        {
            lock (_lockRenderTarget)
            {
                _resourcesInitialised = false;

                RemoveAndDispose(ref _renderTargetCopy);
                _renderTargetCopyLocked = false;

                RemoveAndDispose(ref _resolvedTarget);
                RemoveAndDispose(ref _query);
                _queryIssued = false;

                RemoveAndDispose(ref _font);

                RemoveAndDispose(ref _overlayEngine);
            }
        }

        /// <summary>
        /// The IDirect3DDevice9.EndScene function definition
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate int Direct3D9Device_EndSceneDelegate(IntPtr device);

        /// <summary>
        /// The IDirect3DDevice9.Reset function definition
        /// </summary>
        /// <param name="device"></param>
        /// <param name="presentParameters"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate int Direct3D9Device_ResetDelegate(IntPtr device, ref PresentParameters presentParameters);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe delegate int Direct3D9Device_PresentDelegate(IntPtr devicePtr, SharpDX.Rectangle* pSourceRect, SharpDX.Rectangle* pDestRect, IntPtr hDestWindowOverride, IntPtr pDirtyRegion);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe delegate int Direct3D9DeviceEx_PresentExDelegate(IntPtr devicePtr, SharpDX.Rectangle* pSourceRect, SharpDX.Rectangle* pDestRect, IntPtr hDestWindowOverride, IntPtr pDirtyRegion, Present dwFlags);
        

        /// <summary>
        /// Reset the _renderTarget so that we are sure it will have the correct presentation parameters (required to support working across changes to windowed/fullscreen or resolution changes)
        /// </summary>
        /// <param name="devicePtr"></param>
        /// <param name="presentParameters"></param>
        /// <returns></returns>
        int ResetHook(IntPtr devicePtr, ref PresentParameters presentParameters)
        {
            // Ensure certain overlay resources have performed necessary pre-reset tasks
            if (_overlayEngine != null)
                _overlayEngine.BeforeDeviceReset();

            Cleanup();

            return Direct3DDevice_ResetHook.Original(devicePtr, ref presentParameters);
        }

        bool _isUsingPresent = false;

        // Used in the overlay
        unsafe int PresentExHook(IntPtr devicePtr, SharpDX.Rectangle* pSourceRect, SharpDX.Rectangle* pDestRect, IntPtr hDestWindowOverride, IntPtr pDirtyRegion, Present dwFlags)
        {
            _isUsingPresent = true;
            DeviceEx device = (DeviceEx)devicePtr;

            DoCaptureRenderTarget(device, "PresentEx");

            return Direct3DDeviceEx_PresentExHook.Original(devicePtr, pSourceRect, pDestRect, hDestWindowOverride, pDirtyRegion, dwFlags);
        }
        
        unsafe int PresentHook(IntPtr devicePtr, SharpDX.Rectangle* pSourceRect, SharpDX.Rectangle* pDestRect, IntPtr hDestWindowOverride, IntPtr pDirtyRegion)
        {
            _isUsingPresent = true;

            Device device = (Device)devicePtr;

            DoCaptureRenderTarget(device, "PresentHook");

            return Direct3DDevice_PresentHook.Original(devicePtr, pSourceRect, pDestRect, hDestWindowOverride, pDirtyRegion);
        }

        /// <summary>
        /// Hook for IDirect3DDevice9.EndScene
        /// </summary>
        /// <param name="devicePtr">Pointer to the IDirect3DDevice9 instance. Note: object member functions always pass "this" as the first parameter.</param>
        /// <returns>The HRESULT of the original EndScene</returns>
        /// <remarks>Remember that this is called many times a second by the Direct3D application - be mindful of memory and performance!</remarks>
        int EndSceneHook(IntPtr devicePtr)
        {
            Device device = (Device)devicePtr;

            if (!_isUsingPresent)
                DoCaptureRenderTarget(device, "EndSceneHook");

            return Direct3DDevice_EndSceneHook.Original(devicePtr);
        }

        Capture.Hook.DX9.DXOverlayEngine _overlayEngine;

        /// <summary>
        /// Implementation of capturing from the render target of the Direct3D9 Device (or DeviceEx)
        /// </summary>
        /// <param name="device"></param>
        void DoCaptureRenderTarget(Device device, string hook)
        {
            this.Frame();
            
            try
            {
                #region Screenshot Request

                // If we have issued the command to copy data to our render target, check if it is complete
                bool qryResult;
                if (_queryIssued && _requestCopy != null && _query.GetData(out qryResult, false))
                {
                    // The GPU has finished copying data to _renderTargetCopy, we can now lock
                    // the data and access it on another thread.

                    _queryIssued = false;
                    
                    // Lock the render target
                    SharpDX.Rectangle rect;
                    SharpDX.DataRectangle lockedRect = LockRenderTarget(_renderTargetCopy, out rect);
                    _renderTargetCopyLocked = true;

                    // Copy the data from the render target
                    System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        lock (_lockRenderTarget)
                        {
                            ProcessCapture(rect.Width, rect.Height, lockedRect.Pitch, _renderTargetCopy.Description.Format.ToPixelFormat(), lockedRect.DataPointer, _requestCopy);
                        }
                    });
                }

                // Single frame capture request
                if (this.Request != null)
                {
                    DateTime start = DateTime.Now;
                    try
                    {
                        using (Surface renderTarget = device.GetRenderTarget(0))
                        {
                            int width, height;

                            // If resizing of the captured image, determine correct dimensions
                            if (Request.Resize != null && (renderTarget.Description.Width > Request.Resize.Value.Width || renderTarget.Description.Height > Request.Resize.Value.Height))
                            {
                                if (renderTarget.Description.Width > Request.Resize.Value.Width)
                                {
                                    width = Request.Resize.Value.Width;
                                    height = (int)Math.Round((renderTarget.Description.Height * ((double)Request.Resize.Value.Width / (double)renderTarget.Description.Width)));
                                }
                                else
                                {
                                    height = Request.Resize.Value.Height;
                                    width = (int)Math.Round((renderTarget.Description.Width * ((double)Request.Resize.Value.Height / (double)renderTarget.Description.Height)));
                                }
                            }
                            else
                            {
                                width = renderTarget.Description.Width;
                                height = renderTarget.Description.Height;
                            }

                            // If existing _renderTargetCopy, ensure that it is the correct size and format
                            if (_renderTargetCopy != null && (_renderTargetCopy.Description.Width != width || _renderTargetCopy.Description.Height != height || _renderTargetCopy.Description.Format != renderTarget.Description.Format))
                            {
                                // Cleanup resources
                                Cleanup();
                            }

                            // Ensure that we have something to put the render target data into
                            if (!_resourcesInitialised || _renderTargetCopy == null)
                            {
                                CreateResources(device, width, height, renderTarget.Description.Format);
                            }

                            // Resize from render target Surface to resolvedSurface (also deals with resolving multi-sampling)
                            device.StretchRectangle(renderTarget, _resolvedTarget, TextureFilter.None);
                        }

                        // If the render target is locked from a previous request unlock it
                        if (_renderTargetCopyLocked)
                        {
                            // Wait for the the ProcessCapture thread to finish with it
                            lock (_lockRenderTarget)
                            {
                                if (_renderTargetCopyLocked)
                                {
                                    _renderTargetCopy.UnlockRectangle();
                                    _renderTargetCopyLocked = false;
                                }
                            }
                        }
                            
                        // Copy data from resolved target to our render target copy
                        device.GetRenderTargetData(_resolvedTarget, _renderTargetCopy);

                        _requestCopy = Request.Clone();
                        _query.Issue(Issue.End);
                        _queryIssued = true;
                        
                    }
                    finally
                    {
                        // We have completed the request - mark it as null so we do not continue to try to capture the same request
                        // Note: If you are after high frame rates, consider implementing buffers here to capture more frequently
                        //         and send back to the host application as needed. The IPC overhead significantly slows down 
                        //         the whole process if sending frame by frame.
                        Request = null;
                    }
                    DateTime end = DateTime.Now;
                    this.DebugMessage(hook + ": Capture time: " + (end - start).ToString());
                }

                #endregion

                if (this.Config.ShowOverlay)
                {
                    #region Draw Overlay

                    // Check if overlay needs to be initialised
                    if (_overlayEngine == null || _overlayEngine.Device.NativePointer != device.NativePointer)
                    {
                        // Cleanup if necessary
                        if (_overlayEngine != null)
                            RemoveAndDispose(ref _overlayEngine);

                        _overlayEngine = ToDispose(new DX9.DXOverlayEngine());
                        // Create Overlay
                        _overlayEngine.Overlays.Add(new Capture.Hook.Common.Overlay
                        {
                            Elements =
                            {
                                // Add frame rate
                                new Capture.Hook.Common.FramesPerSecond(new System.Drawing.Font("Arial", 16, FontStyle.Bold)) { Location = new System.Drawing.Point(5,5), Color = System.Drawing.Color.Red, AntiAliased = true },
                                // Example of adding an image to overlay (can implement semi transparency with Tint, e.g. Ting = Color.FromArgb(127, 255, 255, 255))
                                //new Capture.Hook.Common.ImageElement(@"C:\Temp\test.bmp") { Location = new System.Drawing.Point(20, 20) }
                            }
                        });

                        _overlayEngine.Initialise(device);
                    }
                    // Draw Overlay(s)
                    else if (_overlayEngine != null)
                    {
                        foreach (var overlay in _overlayEngine.Overlays)
                            overlay.Frame();
                        _overlayEngine.Draw();
                    }

                    #endregion
                }
            }
            catch (Exception e)
            {
                DebugMessage(e.ToString());
            }
        }

        private SharpDX.DataRectangle LockRenderTarget(Surface _renderTargetCopy, out SharpDX.Rectangle rect)
        {
            if (_requestCopy.RegionToCapture.Height > 0 && _requestCopy.RegionToCapture.Width > 0)
            {
                rect = new SharpDX.Rectangle(_requestCopy.RegionToCapture.Left, _requestCopy.RegionToCapture.Top, _requestCopy.RegionToCapture.Width, _requestCopy.RegionToCapture.Height);
            }
            else
            {
                rect = new SharpDX.Rectangle(0, 0, _renderTargetCopy.Description.Width, _renderTargetCopy.Description.Height);
            }
            return _renderTargetCopy.LockRectangle(rect, LockFlags.ReadOnly);
        }

        private void CreateResources(Device device, int width, int height, Format format)
        {
            if (_resourcesInitialised) return;
            _resourcesInitialised = true;
            
            // Create offscreen surface to use as copy of render target data
            _renderTargetCopy = ToDispose(Surface.CreateOffscreenPlain(device, width, height, format, Pool.SystemMemory));
            
            // Create our resolved surface (resizing if necessary and to resolve any multi-sampling)
            _resolvedTarget = ToDispose(Surface.CreateRenderTarget(device, width, height, format, MultisampleType.None, 0, false));

            _query = ToDispose(new Query(device, QueryType.Event));
        }
    }
}
