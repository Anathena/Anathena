using Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook.Common;
using Anathema.Source.SystemInternals.Graphics.DirectXHook.Interface;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook.DX9
{
    /// <summary>
    /// The full list of IDirect3DDevice9 functions with the correct index
    /// </summary>
    public enum Direct3DDevice9FunctionOrdinalsEnum : Int16
    {
        QueryInterface = 0,
        AddRef = 1,
        Release = 2,
        TestCooperativeLevel = 3,
        GetAvailableTextureMem = 4,
        EvictManagedResources = 5,
        GetDirect3D = 6,
        GetDeviceCaps = 7,
        GetDisplayMode = 8,
        GetCreationParameters = 9,
        SetCursorProperties = 10,
        SetCursorPosition = 11,
        ShowCursor = 12,
        CreateAdditionalSwapChain = 13,
        GetSwapChain = 14,
        GetNumberOfSwapChains = 15,
        Reset = 16,
        Present = 17,
        GetBackBuffer = 18,
        GetRasterStatus = 19,
        SetDialogBoxMode = 20,
        SetGammaRamp = 21,
        GetGammaRamp = 22,
        CreateTexture = 23,
        CreateVolumeTexture = 24,
        CreateCubeTexture = 25,
        CreateVertexBuffer = 26,
        CreateIndexBuffer = 27,
        CreateRenderTarget = 28,
        CreateDepthStencilSurface = 29,
        UpdateSurface = 30,
        UpdateTexture = 31,
        GetRenderTargetData = 32,
        GetFrontBufferData = 33,
        StretchRect = 34,
        ColorFill = 35,
        CreateOffscreenPlainSurface = 36,
        SetRenderTarget = 37,
        GetRenderTarget = 38,
        SetDepthStencilSurface = 39,
        GetDepthStencilSurface = 40,
        BeginScene = 41,
        EndScene = 42,
        Clear = 43,
        SetTransform = 44,
        GetTransform = 45,
        MultiplyTransform = 46,
        SetViewport = 47,
        GetViewport = 48,
        SetMaterial = 49,
        GetMaterial = 50,
        SetLight = 51,
        GetLight = 52,
        LightEnable = 53,
        GetLightEnable = 54,
        SetClipPlane = 55,
        GetClipPlane = 56,
        SetRenderState = 57,
        GetRenderState = 58,
        CreateStateBlock = 59,
        BeginStateBlock = 60,
        EndStateBlock = 61,
        SetClipStatus = 62,
        GetClipStatus = 63,
        GetTexture = 64,
        SetTexture = 65,
        GetTextureStageState = 66,
        SetTextureStageState = 67,
        GetSamplerState = 68,
        SetSamplerState = 69,
        ValidateDevice = 70,
        SetPaletteEntries = 71,
        GetPaletteEntries = 72,
        SetCurrentTexturePalette = 73,
        GetCurrentTexturePalette = 74,
        SetScissorRect = 75,
        GetScissorRect = 76,
        SetSoftwareVertexProcessing = 77,
        GetSoftwareVertexProcessing = 78,
        SetNPatchMode = 79,
        GetNPatchMode = 80,
        DrawPrimitive = 81,
        DrawIndexedPrimitive = 82,
        DrawPrimitiveUP = 83,
        DrawIndexedPrimitiveUP = 84,
        ProcessVertices = 85,
        CreateVertexDeclaration = 86,
        SetVertexDeclaration = 87,
        GetVertexDeclaration = 88,
        SetFVF = 89,
        GetFVF = 90,
        CreateVertexShader = 91,
        SetVertexShader = 92,
        GetVertexShader = 93,
        SetVertexShaderConstantF = 94,
        GetVertexShaderConstantF = 95,
        SetVertexShaderConstantI = 96,
        GetVertexShaderConstantI = 97,
        SetVertexShaderConstantB = 98,
        GetVertexShaderConstantB = 99,
        SetStreamSource = 100,
        GetStreamSource = 101,
        SetStreamSourceFreq = 102,
        GetStreamSourceFreq = 103,
        SetIndices = 104,
        GetIndices = 105,
        CreatePixelShader = 106,
        SetPixelShader = 107,
        GetPixelShader = 108,
        SetPixelShaderConstantF = 109,
        GetPixelShaderConstantF = 110,
        SetPixelShaderConstantI = 111,
        GetPixelShaderConstantI = 112,
        SetPixelShaderConstantB = 113,
        GetPixelShaderConstantB = 114,
        DrawRectPatch = 115,
        DrawTriPatch = 116,
        DeletePatch = 117,
        CreateQuery = 118,
    }

    public enum Direct3DDevice9ExFunctionOrdinalsEnum : Int16
    {
        SetConvolutionMonoKernel = 119,
        ComposeRects = 120,
        PresentEx = 121,
        GetGPUThreadPriority = 122,
        SetGPUThreadPriority = 123,
        WaitForVBlank = 124,
        CheckResourceResidency = 125,
        SetMaximumFrameLatency = 126,
        GetMaximumFrameLatency = 127,
        CheckDeviceState_ = 128,
        CreateRenderTargetEx = 129,
        CreateOffscreenPlainSurfaceEx = 130,
        CreateDepthStencilSurfaceEx = 131,
        ResetEx = 132,
        GetDisplayModeEx = 133,

    }

    internal class DXHookD3D9 : BaseDXHook
    {
        private static Int32 D3D9_DEVICE_METHOD_COUNT = Enum.GetNames(typeof(Direct3DDevice9FunctionOrdinalsEnum)).Length;
        private static Int32 D3D9Ex_DEVICE_METHOD_COUNT = Enum.GetNames(typeof(Direct3DDevice9ExFunctionOrdinalsEnum)).Length;

        private Hook<Direct3D9Device_EndSceneDelegate> Direct3DDeviceEndSceneHook;
        private Hook<Direct3D9Device_ResetDelegate> Direct3DDeviceResetHook;
        private Hook<Direct3D9Device_PresentDelegate> Direct3DDevicePresentHook;
        private Hook<Direct3D9DeviceEx_PresentExDelegate> Direct3DDeviceExPresentExHook;
        private Object LockRenderObject;

        private Boolean ResourcesInitialized;
        private Query Query;
        private SharpDX.Direct3D9.Font Font;
        private Boolean QueryIssued;
        private Boolean RenderTargetCopyLocked;
        private Surface RenderTargetCopy;
        private Surface ResolvedTarget;

        private List<IntPtr> ID3DDeviceFunctionAddresses = new List<IntPtr>();
        // private List<IntPtr> id3dDeviceExFunctionAddresses = new List<IntPtr>();
        private Boolean SupportsDirect3D9Ex;

        private DX9.DXOverlayEngine OverlayEngine;

        private Boolean IsUsingPresent;

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
        /// <param name="Device"></param>
        /// <param name="PresentParameters"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate Int32 Direct3D9Device_ResetDelegate(IntPtr Device, ref PresentParameters PresentParameters);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe delegate Int32 Direct3D9Device_PresentDelegate(IntPtr DevicePtr, SharpDX.Rectangle* PSourceRect, SharpDX.Rectangle* PDestRect, IntPtr hDestWindowOverride, IntPtr PDirtyRegion);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe delegate Int32 Direct3D9DeviceEx_PresentExDelegate(IntPtr DevicePtr, SharpDX.Rectangle* PSourceRect, SharpDX.Rectangle* PDestRect, IntPtr hDestWindowOverride, IntPtr PDirtyRegion, Present DWFlags);


        public DXHookD3D9(ClientInterface CaptureInterface) : base(CaptureInterface)
        {
            LockRenderObject = new Object();

            RenderTargetCopyLocked = false;
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
            DebugMessage("Hook: Begin");

            // First we need to determine the function address for IDirect3DDevice9
            Device Device;
            ID3DDeviceFunctionAddresses = new List<IntPtr>();
            // ID3DDeviceExFunctionAddresses = new List<IntPtr>();

            DebugMessage("Hook: Before device creation");

            using (Direct3D D3D = new Direct3D())
            {
                using (Form renderForm = new Form())
                {
                    using (Device = new Device(D3D, 0, DeviceType.NullReference, IntPtr.Zero, CreateFlags.HardwareVertexProcessing, new PresentParameters() { BackBufferWidth = 1, BackBufferHeight = 1, DeviceWindowHandle = renderForm.Handle }))
                    {
                        DebugMessage("Hook: Device created");

                        ID3DDeviceFunctionAddresses.AddRange(GetVirtualTableAddresses(Device.NativePointer, D3D9_DEVICE_METHOD_COUNT));
                    }
                }
            }

            try
            {
                using (Direct3DEx D3DEx = new Direct3DEx())
                {
                    DebugMessage("Hook: Direct3DEx...");

                    using (Form RenderForm = new Form())
                    {
                        using (DeviceEx DeviceEx = new DeviceEx(D3DEx, 0, DeviceType.NullReference, IntPtr.Zero, CreateFlags.HardwareVertexProcessing, new PresentParameters() { BackBufferWidth = 1, BackBufferHeight = 1, DeviceWindowHandle = RenderForm.Handle }, new DisplayModeEx() { Width = 800, Height = 600 }))
                        {
                            DebugMessage("Hook: DeviceEx created - PresentEx supported");

                            ID3DDeviceFunctionAddresses.AddRange(GetVirtualTableAddresses(DeviceEx.NativePointer, D3D9_DEVICE_METHOD_COUNT, D3D9Ex_DEVICE_METHOD_COUNT));
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
            Direct3DDeviceEndSceneHook = new Hook<Direct3D9Device_EndSceneDelegate>(
                ID3DDeviceFunctionAddresses[(Int32)Direct3DDevice9FunctionOrdinalsEnum.EndScene],
                new Direct3D9Device_EndSceneDelegate(EndSceneHook), this);

            unsafe
            {
                // If Direct3D9Ex is available - hook the PresentEx
                if (SupportsDirect3D9Ex)
                {
                    Direct3DDeviceExPresentExHook = new Hook<Direct3D9DeviceEx_PresentExDelegate>(
                        ID3DDeviceFunctionAddresses[(Int32)Direct3DDevice9ExFunctionOrdinalsEnum.PresentEx],
                        new Direct3D9DeviceEx_PresentExDelegate(PresentExHook), this);
                }

                // Always hook Present also (device will only call Present or PresentEx not both)
                Direct3DDevicePresentHook = new Hook<Direct3D9Device_PresentDelegate>(
                    ID3DDeviceFunctionAddresses[(Int32)Direct3DDevice9FunctionOrdinalsEnum.Present],
                    new Direct3D9Device_PresentDelegate(PresentHook), this);
            }

            // 16 - Reset (called on resolution change or windowed/fullscreen change - we will reset some things as well)
            // On Windows 7 64-bit w/ 32-bit app and d3d9 dll version 6.1.7600.16385, the address is equiv to:
            //(IntPtr)(GetModuleHandle("d3d9").ToInt32() + 0x58dda),
            // A 64-bit app would use 0x3b3a0
            // Note: GetD3D9DeviceFunctionAddress will output these addresses to a log file
            Direct3DDeviceResetHook = new Hook<Direct3D9Device_ResetDelegate>(
                ID3DDeviceFunctionAddresses[(Int32)Direct3DDevice9FunctionOrdinalsEnum.Reset],
                new Direct3D9Device_ResetDelegate(ResetHook), this);

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

            DebugMessage("Hook: End");
        }

        /// <summary>
        /// Just ensures that the surface we created is cleaned up.
        /// </summary>
        public override void Cleanup()
        {
            lock (LockRenderObject)
            {
                ResourcesInitialized = false;

                RemoveAndDispose(ref RenderTargetCopy);
                RenderTargetCopyLocked = false;

                RemoveAndDispose(ref ResolvedTarget);
                RemoveAndDispose(ref Query);
                QueryIssued = false;

                RemoveAndDispose(ref Font);

                RemoveAndDispose(ref OverlayEngine);
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
        unsafe Int32 PresentExHook(IntPtr DevicePtr, SharpDX.Rectangle* PSourceRect, SharpDX.Rectangle* PDestRect, IntPtr HDestWindowOverride, IntPtr PDirtyRegion, Present DWFlags)
        {
            IsUsingPresent = true;
            DeviceEx Device = (DeviceEx)DevicePtr;

            DoCaptureRenderTarget(Device, "PresentEx");

            return Direct3DDeviceExPresentExHook.Original(DevicePtr, PSourceRect, PDestRect, HDestWindowOverride, PDirtyRegion, DWFlags);
        }

        unsafe Int32 PresentHook(IntPtr DevicePtr, SharpDX.Rectangle* PSourceRect, SharpDX.Rectangle* PDestRect, IntPtr HDestWindowOverride, IntPtr PDirtyRegion)
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
                    if (OverlayEngine != null)
                        RemoveAndDispose(ref OverlayEngine);

                    OverlayEngine = ToDispose(new DX9.DXOverlayEngine());

                    // Create Overlay
                    OverlayEngine.Overlays.Add(new Overlay
                    {
                        Elements =
                            {
                                // Add frame rate
                                new Common.FramesPerSecond(new System.Drawing.Font("Arial", 16, FontStyle.Bold)) { Location = new Point(5,5), Color = Color.Red, AntiAliased = true },
                                // Example of adding an image to overlay (can implement semi transparency with Tint, e.g. Ting = Color.FromArgb(127, 255, 255, 255))
                                // new Common.ImageElement(@"C:\Temp\test.bmp") { Location = new Point(20, 20) }
                            }
                    });

                    OverlayEngine.Initialize(Device);
                }
                // Draw Overlay(s)
                else if (OverlayEngine != null)
                {
                    foreach (IOverlay Overlay in OverlayEngine.Overlays)
                        Overlay.Frame();
                    OverlayEngine.Draw();
                }
            }
            catch (Exception Ex)
            {
                DebugMessage(Ex.ToString());
            }
        }

        private void CreateResources(Device Device, Int32 Width, Int32 Height, Format D3DFormat)
        {
            if (ResourcesInitialized)
                return;

            ResourcesInitialized = true;

            // Create offscreen surface to use as copy of render target data
            RenderTargetCopy = ToDispose(Surface.CreateOffscreenPlain(Device, Width, Height, D3DFormat, Pool.SystemMemory));

            // Create our resolved surface (resizing if necessary and to resolve any multi-sampling)
            ResolvedTarget = ToDispose(Surface.CreateRenderTarget(Device, Width, Height, D3DFormat, MultisampleType.None, 0, false));

            Query = ToDispose(new Query(Device, QueryType.Event));
        }

    } // End class

} // End namespace