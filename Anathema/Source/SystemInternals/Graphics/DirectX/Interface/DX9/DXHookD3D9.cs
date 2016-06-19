using SharpDX.Direct3D9;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface.DX9
{
    internal class DXHookD3D9 : BaseDXHook
    {
        private Hook<Direct3D9DeviceEndSceneDelegate> Direct3DDeviceEndSceneHook;
        private Hook<Direct3D9DeviceResetDelegate> Direct3DDeviceResetHook;
        private Hook<Direct3D9DevicePresentDelegate> Direct3DDevicePresentHook;
        private Hook<Direct3D9DeviceExPresentExDelegate> Direct3DDeviceExPresentExHook;
        private Object LockRenderObject;

        private Boolean ResourcesInitialized;
        private Query Query;
        private Font Font;
        private Surface RenderTargetCopy;
        private Surface ResolvedTarget;

        private List<IntPtr> ID3DDeviceFunctionAddresses = new List<IntPtr>();
        private Boolean SupportsDirect3D9Ex;

        private DXOverlayEngine OverlayEngine;

        private Boolean IsUsingPresent;

        /// <summary>
        /// The IDirect3DDevice9.EndScene function definition
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate int Direct3D9DeviceEndSceneDelegate(IntPtr device);

        /// <summary>
        /// The IDirect3DDevice9.Reset function definition
        /// </summary>
        /// <param name="Device"></param>
        /// <param name="PresentParameters"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate Int32 Direct3D9DeviceResetDelegate(IntPtr Device, ref PresentParameters PresentParameters);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe delegate Int32 Direct3D9DevicePresentDelegate(IntPtr DevicePtr, RawRectangle* PSourceRect, RawRectangle* PDestRect, IntPtr hDestWindowOverride, IntPtr PDirtyRegion);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe delegate Int32 Direct3D9DeviceExPresentExDelegate(IntPtr DevicePtr, RawRectangle* PSourceRect, RawRectangle* PDestRect, IntPtr hDestWindowOverride, IntPtr PDirtyRegion, Present DWFlags);


        public DXHookD3D9(DirextXGraphicsInterface GraphicsInterface) : base(GraphicsInterface)
        {
            LockRenderObject = new Object();

            SupportsDirect3D9Ex = false;
            IsUsingPresent = false;

            Direct3DDeviceEndSceneHook = null;
            Direct3DDeviceResetHook = null;
            Direct3DDevicePresentHook = null;
            Direct3DDeviceExPresentExHook = null;
        }

        protected override String HookName { get { return "DXHookD3D9"; } }

        public override void Hook()
        {
            // First we need to determine the function address for IDirect3DDevice9
            Device Device;
            ID3DDeviceFunctionAddresses = new List<IntPtr>();

            using (Direct3D D3D = new Direct3D())
            {
                using (Form renderForm = new Form())
                {
                    using (Device = new Device(D3D, 0, DeviceType.NullReference, IntPtr.Zero, CreateFlags.HardwareVertexProcessing, new PresentParameters() { BackBufferWidth = 1, BackBufferHeight = 1, DeviceWindowHandle = renderForm.Handle }))
                    {
                        ID3DDeviceFunctionAddresses.AddRange(GetVirtualTableAddresses(Device.NativePointer, DirectXFlags.D3D9DeviceMethodCount));
                    }
                }
            }

            try
            {
                using (Direct3DEx D3DEx = new Direct3DEx())
                {
                    using (Form RenderForm = new Form())
                    {
                        using (DeviceEx DeviceEx = new DeviceEx(D3DEx, 0, DeviceType.NullReference, IntPtr.Zero, CreateFlags.HardwareVertexProcessing, new PresentParameters() { BackBufferWidth = 1, BackBufferHeight = 1, DeviceWindowHandle = RenderForm.Handle }, new DisplayModeEx() { Width = 800, Height = 600 }))
                        {
                            ID3DDeviceFunctionAddresses.AddRange(GetVirtualTableAddresses(DeviceEx.NativePointer, DirectXFlags.D3D9DeviceMethodCount, DirectXFlags.D3D9ExDeviceMethodCount));
                            SupportsDirect3D9Ex = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                SupportsDirect3D9Ex = false;
            }

            // We want to hook each method of the IDirect3DDevice9 interface that we are interested in

            // 42 - EndScene (we will retrieve the back buffer here)
            // On Windows 7 64-bit w/ 32-bit app and d3d9 dll version 6.1.7600.16385, the address is equiv to:
            // (IntPtr)(GetModuleHandle("d3d9").ToInt32() + 0x1ce09),
            // A 64-bit app would use 0xff18
            // Note: GetD3D9DeviceFunctionAddress will output these addresses to a log file
            Direct3DDeviceEndSceneHook = new Hook<Direct3D9DeviceEndSceneDelegate>(
                ID3DDeviceFunctionAddresses[(Int32)DirectXFlags.Direct3DDevice9FunctionOrdinalsEnum.EndScene],
                new Direct3D9DeviceEndSceneDelegate(EndSceneHook), this);

            unsafe
            {
                // If Direct3D9Ex is available - hook the PresentEx
                if (SupportsDirect3D9Ex)
                {
                    Direct3DDeviceExPresentExHook = new Hook<Direct3D9DeviceExPresentExDelegate>(
                        ID3DDeviceFunctionAddresses[(Int32)DirectXFlags.Direct3DDevice9ExFunctionOrdinalsEnum.PresentEx],
                        new Direct3D9DeviceExPresentExDelegate(PresentExHook), this);
                }

                // Always hook Present also (device will only call Present or PresentEx not both)
                Direct3DDevicePresentHook = new Hook<Direct3D9DevicePresentDelegate>(
                    ID3DDeviceFunctionAddresses[(Int32)DirectXFlags.Direct3DDevice9FunctionOrdinalsEnum.Present],
                    new Direct3D9DevicePresentDelegate(PresentHook), this);
            }

            // 16 - Reset (called on resolution change or windowed/fullscreen change - we will reset some things as well)
            // On Windows 7 64-bit w/ 32-bit app and d3d9 dll version 6.1.7600.16385, the address is equiv to:
            //(IntPtr)(GetModuleHandle("d3d9").ToInt32() + 0x58dda),
            // A 64-bit app would use 0x3b3a0
            // Note: GetD3D9DeviceFunctionAddress will output these addresses to a log file
            Direct3DDeviceResetHook = new Hook<Direct3D9DeviceResetDelegate>(
                ID3DDeviceFunctionAddresses[(Int32)DirectXFlags.Direct3DDevice9FunctionOrdinalsEnum.Reset],
                new Direct3D9DeviceResetDelegate(ResetHook), this);

            // The following ensures that all threads are intercepted (Note: must be done for each hook)
            Direct3DDeviceEndSceneHook.Activate();
            Hooks.Add(Direct3DDeviceEndSceneHook);

            Direct3DDevicePresentHook.Activate();
            Hooks.Add(Direct3DDevicePresentHook);

            if (SupportsDirect3D9Ex)
            {
                Direct3DDeviceExPresentExHook.Activate();
                Hooks.Add(Direct3DDeviceExPresentExHook);
            }

            Direct3DDeviceResetHook.Activate();
            Hooks.Add(Direct3DDeviceResetHook);
        }

        /// <summary>
        /// Just ensures that the surface we created is cleaned up.
        /// </summary>
        public override void Cleanup()
        {
            lock (LockRenderObject)
            {
                ResourcesInitialized = false;
                /*
                RemoveAndDispose(ref RenderTargetCopy);
                RemoveAndDispose(ref ResolvedTarget);
                RemoveAndDispose(ref Query);
                RemoveAndDispose(ref Font);
                RemoveAndDispose(ref OverlayEngine);*/
            }
        }

        /// <summary>
        /// Reset the _renderTarget so that we are sure it will have the correct presentation parameters (required to support working across changes to windowed/fullscreen or resolution changes)
        /// </summary>
        /// <param name="DevicePtr"></param>
        /// <param name="PresentParameters"></param>
        /// <returns></returns>
        Int32 ResetHook(IntPtr DevicePtr, ref PresentParameters PresentParameters)
        {
            // Ensure certain overlay resources have performed necessary pre-reset tasks
            if (OverlayEngine != null)
                OverlayEngine.BeforeDeviceReset();

            Cleanup();

            return Direct3DDeviceResetHook.Original(DevicePtr, ref PresentParameters);
        }

        // Used in the overlay
        unsafe Int32 PresentExHook(IntPtr DevicePtr, RawRectangle* PSourceRect, RawRectangle* PDestRect, IntPtr HDestWindowOverride, IntPtr PDirtyRegion, Present DWFlags)
        {
            IsUsingPresent = true;
            DeviceEx Device = (DeviceEx)DevicePtr;

            DoCaptureRenderTarget(Device, "PresentEx");

            return Direct3DDeviceExPresentExHook.Original(DevicePtr, PSourceRect, PDestRect, HDestWindowOverride, PDirtyRegion, DWFlags);
        }

        unsafe Int32 PresentHook(IntPtr DevicePtr, RawRectangle* PSourceRect, RawRectangle* PDestRect, IntPtr HDestWindowOverride, IntPtr PDirtyRegion)
        {
            IsUsingPresent = true;
            Device Device = (Device)DevicePtr;

            DoCaptureRenderTarget(Device, "PresentHook");

            return Direct3DDevicePresentHook.Original(DevicePtr, PSourceRect, PDestRect, HDestWindowOverride, PDirtyRegion);
        }

        /// <summary>
        /// Hook for IDirect3DDevice9.EndScene
        /// </summary>
        /// <param name="DevicePtr">Pointer to the IDirect3DDevice9 instance. Note: object member functions always pass "this" as the first parameter.</param>
        /// <returns>The HRESULT of the original EndScene</returns>
        /// <remarks>Remember that this is called many times a second by the Direct3D application - be mindful of memory and performance!</remarks>
        Int32 EndSceneHook(IntPtr DevicePtr)
        {
            Device Device = (Device)DevicePtr;

            if (!IsUsingPresent)
                DoCaptureRenderTarget(Device, "EndSceneHook");

            return Direct3DDeviceEndSceneHook.Original(DevicePtr);
        }


        /// <summary>
        /// Implementation of capturing from the render target of the Direct3D9 Device (or DeviceEx)
        /// </summary>
        /// <param name="Device"></param>
        void DoCaptureRenderTarget(Device Device, String Hook)
        {
            Frame();

            try
            {
                // Draw FPS
                if (OverlayEngine == null || OverlayEngine.Device.NativePointer != Device.NativePointer)
                {
                    // Cleanup if necessary
                    // if (OverlayEngine != null)
                    // RemoveAndDispose(ref OverlayEngine);

                    OverlayEngine = new DXOverlayEngine(GraphicsInterface); // ToDispose()

                    OverlayEngine.Initialize(Device);
                }
                // Draw Overlay(s)
                else if (OverlayEngine != null)
                {
                    OverlayEngine.Draw();
                }
            }
            catch
            {

            }
        }

        private void CreateResources(Device Device, Int32 Width, Int32 Height, Format D3DFormat)
        {
            if (ResourcesInitialized)
                return;

            ResourcesInitialized = true;

            // Create offscreen surface to use as copy of render target data
            RenderTargetCopy = Surface.CreateOffscreenPlain(Device, Width, Height, D3DFormat, Pool.SystemMemory); //ToDispose()

            // Create our resolved surface (resizing if necessary and to resolve any multi-sampling)
            ResolvedTarget = Surface.CreateRenderTarget(Device, Width, Height, D3DFormat, MultisampleType.None, 0, false); //ToDispose(

            Query = new Query(Device, QueryType.Event); //ToDispose()
        }

    } // End class

} // End namespace