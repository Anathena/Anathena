namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX9
{
    using SharpDX.Direct3D9;
    using SharpDX.Mathematics.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class DXHookD3D9 : BaseDXHook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DXHookD3D9" /> class
        /// </summary>
        /// <param name="graphicsInterface">An object providing access to control of the DirectX graphics library</param>
        public DXHookD3D9(DirextXGraphicsInterface graphicsInterface) : base(graphicsInterface)
        {
            this.ID3DDeviceFunctionAddresses = new List<IntPtr>();
            this.SupportsDirect3D9Ex = false;
            this.IsUsingPresent = false;

            this.Direct3DDeviceEndSceneHook = null;
            this.Direct3DDeviceResetHook = null;
            this.Direct3DDevicePresentHook = null;
            this.Direct3DDeviceExPresentExHook = null;
        }

        /// <summary>
        /// The IDirect3DDevice9.EndScene function definition
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate int Direct3D9DeviceEndSceneDelegate(IntPtr device);

        /// <summary>
        /// The IDirect3DDevice9.Reset function definition
        /// </summary>
        /// <param name="device"></param>
        /// <param name="presentParameters"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Int32 Direct3D9DeviceResetDelegate(IntPtr device, ref PresentParameters presentParameters);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private unsafe delegate Int32 Direct3D9DevicePresentDelegate(IntPtr device, RawRectangle* sourceRect, RawRectangle* destRect, IntPtr destWindowOverride, IntPtr dirtyRegion);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private unsafe delegate Int32 Direct3D9DeviceExPresentExDelegate(IntPtr device, RawRectangle* sourceRect, RawRectangle* destRect, IntPtr destWindowOverride, IntPtr dirtyRegion, Present presentFlags);

        private Hook<Direct3D9DeviceEndSceneDelegate> Direct3DDeviceEndSceneHook { get; set; }

        private Hook<Direct3D9DeviceResetDelegate> Direct3DDeviceResetHook { get; set; }

        private Hook<Direct3D9DevicePresentDelegate> Direct3DDevicePresentHook { get; set; }

        private Hook<Direct3D9DeviceExPresentExDelegate> Direct3DDeviceExPresentExHook { get; set; }

        private List<IntPtr> ID3DDeviceFunctionAddresses { get; set; }

        private Boolean SupportsDirect3D9Ex { get; set; }

        private DXOverlayEngine OverlayEngine { get; set; }

        private Boolean IsUsingPresent { get; set; }

        public override void Hook()
        {
            // First we need to determine the function address for IDirect3DDevice9
            Device device;
            this.ID3DDeviceFunctionAddresses = new List<IntPtr>();

            using (Direct3D d3d = new Direct3D())
            {
                using (Form renderForm = new Form())
                {
                    using (device = new Device(d3d, 0, DeviceType.NullReference, IntPtr.Zero, CreateFlags.HardwareVertexProcessing, new PresentParameters() { BackBufferWidth = 1, BackBufferHeight = 1, DeviceWindowHandle = renderForm.Handle }))
                    {
                        this.ID3DDeviceFunctionAddresses.AddRange(this.GetVirtualTableAddresses(device.NativePointer, DirectXFlags.D3D9DeviceMethodCount));
                    }
                }
            }

            try
            {
                using (Direct3DEx d3dEx = new Direct3DEx())
                {
                    using (Form renderForm = new Form())
                    {
                        using (DeviceEx deviceEx = new DeviceEx(d3dEx, 0, DeviceType.NullReference, IntPtr.Zero, CreateFlags.HardwareVertexProcessing, new PresentParameters() { BackBufferWidth = 1, BackBufferHeight = 1, DeviceWindowHandle = renderForm.Handle }, new DisplayModeEx() { Width = 800, Height = 600 }))
                        {
                            this.ID3DDeviceFunctionAddresses.AddRange(this.GetVirtualTableAddresses(deviceEx.NativePointer, DirectXFlags.D3D9DeviceMethodCount, DirectXFlags.D3D9ExDeviceMethodCount));
                            this.SupportsDirect3D9Ex = true;
                        }
                    }
                }
            }
            catch
            {
                this.SupportsDirect3D9Ex = false;
            }

            // We want to hook each method of the IDirect3DDevice9 interface that we are interested in

            // 42 - EndScene (we will retrieve the back buffer here)
            // On Windows 7 64-bit w/ 32-bit app and d3d9 dll version 6.1.7600.16385, the address is equiv to:
            // (IntPtr)(GetModuleHandle("d3d9").ToInt32() + 0x1ce09),
            // A 64-bit app would use 0xff18
            // Note: GetD3D9DeviceFunctionAddress will output these addresses to a log file
            this.Direct3DDeviceEndSceneHook = new Hook<Direct3D9DeviceEndSceneDelegate>(
                  this.ID3DDeviceFunctionAddresses[(Int32)DirectXFlags.Direct3DDevice9FunctionOrdinalsEnum.EndScene],
                 new Direct3D9DeviceEndSceneDelegate(this.EndSceneHook),
                 this);

            unsafe
            {
                // If Direct3D9Ex is available - hook the PresentEx
                if (this.SupportsDirect3D9Ex)
                {
                    this.Direct3DDeviceExPresentExHook = new Hook<Direct3D9DeviceExPresentExDelegate>(
                        this.ID3DDeviceFunctionAddresses[(Int32)DirectXFlags.Direct3DDevice9ExFunctionOrdinalsEnum.PresentEx],
                        new Direct3D9DeviceExPresentExDelegate(this.PresentExHook),
                        this);
                }

                // Always hook Present also (device will only call Present or PresentEx not both)
                this.Direct3DDevicePresentHook = new Hook<Direct3D9DevicePresentDelegate>(
                    this.ID3DDeviceFunctionAddresses[(Int32)DirectXFlags.Direct3DDevice9FunctionOrdinalsEnum.Present],
                    new Direct3D9DevicePresentDelegate(this.PresentHook),
                    this);
            }

            // 16 - Reset (called on resolution change or windowed/fullscreen change - we will reset some things as well)
            // On Windows 7 64-bit w/ 32-bit app and d3d9 dll version 6.1.7600.16385, the address is equiv to:
            // (IntPtr)(GetModuleHandle("d3d9").ToInt32() + 0x58dda),
            // A 64-bit app would use 0x3b3a0
            // Note: GetD3D9DeviceFunctionAddress will output these addresses to a log file
            this.Direct3DDeviceResetHook = new Hook<Direct3D9DeviceResetDelegate>(
                 this.ID3DDeviceFunctionAddresses[(Int32)DirectXFlags.Direct3DDevice9FunctionOrdinalsEnum.Reset],
                new Direct3D9DeviceResetDelegate(this.ResetHook),
                this);

            // The following ensures that all threads are intercepted (Note: must be done for each hook)
            this.Direct3DDeviceEndSceneHook.Activate();
            this.Hooks.Add(this.Direct3DDeviceEndSceneHook);

            this.Direct3DDevicePresentHook.Activate();
            this.Hooks.Add(this.Direct3DDevicePresentHook);

            if (this.SupportsDirect3D9Ex)
            {
                this.Direct3DDeviceExPresentExHook.Activate();
                this.Hooks.Add(this.Direct3DDeviceExPresentExHook);
            }

            this.Direct3DDeviceResetHook.Activate();
            this.Hooks.Add(this.Direct3DDeviceResetHook);
        }

        /// <summary>
        /// Just ensures that the surface we created is cleaned up.
        /// </summary>
        public override void Cleanup()
        {
            this.OverlayEngine?.Dispose();
        }

        /// <summary>
        /// Reset the _renderTarget so that we are sure it will have the correct presentation parameters (required to support working across changes to windowed/fullscreen or resolution changes)
        /// </summary>
        /// <param name="devicePtr"></param>
        /// <param name="presentParameters"></param>
        /// <returns></returns>
        private Int32 ResetHook(IntPtr devicePtr, ref PresentParameters presentParameters)
        {
            // Ensure certain overlay resources have performed necessary pre-reset tasks
            if (this.OverlayEngine != null)
            {
                this.OverlayEngine.BeforeDeviceReset();
            }

            this.Cleanup();

            return this.Direct3DDeviceResetHook.Original(devicePtr, ref presentParameters);
        }

        // Used in the overlay
        private unsafe Int32 PresentExHook(IntPtr devicePtr, RawRectangle* sourceRect, RawRectangle* destRect, IntPtr destWindowOverride, IntPtr dirtyRegion, Present presentFlags)
        {
            this.IsUsingPresent = true;
            DeviceEx device = (DeviceEx)devicePtr;

            this.DoCaptureRenderTarget(device, "PresentEx");

            return this.Direct3DDeviceExPresentExHook.Original(devicePtr, sourceRect, destRect, destWindowOverride, dirtyRegion, presentFlags);
        }

        private unsafe Int32 PresentHook(IntPtr devicePtr, RawRectangle* sourceRect, RawRectangle* destRect, IntPtr destWindowOverride, IntPtr dirtyRegion)
        {
            this.IsUsingPresent = true;
            Device device = (Device)devicePtr;

            this.DoCaptureRenderTarget(device, "PresentHook");

            return this.Direct3DDevicePresentHook.Original(devicePtr, sourceRect, destRect, destWindowOverride, dirtyRegion);
        }

        /// <summary>
        /// Hook for IDirect3DDevice9.EndScene
        /// </summary>
        /// <param name="devicePtr">Pointer to the IDirect3DDevice9 instance. Note: object member functions always pass "this" as the first parameter.</param>
        /// <returns>The HRESULT of the original EndScene</returns>
        /// <remarks>Remember that this is called many times a second by the Direct3D application - be mindful of memory and performance!</remarks>
        private Int32 EndSceneHook(IntPtr devicePtr)
        {
            Device device = (Device)devicePtr;

            if (!this.IsUsingPresent)
            {
                this.DoCaptureRenderTarget(device, "EndSceneHook");
            }

            return this.Direct3DDeviceEndSceneHook.Original(devicePtr);
        }

        /// <summary>
        /// Implementation of capturing from the render target of the Direct3D9 Device (or DeviceEx)
        /// </summary>
        /// <param name="device"></param>
        private void DoCaptureRenderTarget(Device device, String hook)
        {
            this.Frame();

            try
            {
                if (this.OverlayEngine == null || this.OverlayEngine.Device.NativePointer != device.NativePointer)
                {
                    // Draw FPS
                    //// Cleanup if necessary
                    //// if ( this.OverlayEngine != null)
                    //// {
                    ////  this.RemoveAndDispose(ref OverlayEngine);
                    //// }

                    if (this.OverlayEngine != null)
                    {
                        this.OverlayEngine.Dispose();
                    }

                    this.OverlayEngine = new DXOverlayEngine(this.GraphicsInterface); // ToDispose()

                    this.OverlayEngine.Initialize(device);
                }
                else if (this.OverlayEngine != null)
                {
                    // Draw Overlay(s)
                    this.OverlayEngine.Draw();
                }
            }
            catch
            {
            }
        }
    }
    //// End class
}
//// End namespace