namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface
{
    using System;

    /// <summary>
    /// A collection of virtual table flags for supported DirectX versions
    /// </summary>
    internal static class DirectXFlags
    {
        public static readonly Int32 D3D9DeviceMethodCount = Enum.GetNames(typeof(Direct3DDevice9FunctionOrdinalsEnum)).Length;

        public static readonly Int32 D3D9ExDeviceMethodCount = Enum.GetNames(typeof(Direct3DDevice9ExFunctionOrdinalsEnum)).Length;

        public static readonly Int32 D3D10DeviceMethodCount = Enum.GetNames(typeof(D3D10DeviceVirtualTableEnum)).Length;

        public static readonly Int32 D3D101DeviceMethodCount = Enum.GetNames(typeof(D3D101DeviceVirtualTableEnum)).Length;

        public static readonly Int32 D3D11DeviceMethodCount = Enum.GetNames(typeof(D3D11DeviceVirtualTableEnum)).Length;

        public static readonly Int32 DXGISwapChainMethodCount = Enum.GetNames(typeof(DXGISwapChainVirtualTableEnum)).Length;

        /// <summary>
        /// Versions of DirectX the graphic hook supports
        /// </summary>
        public enum Direct3DVersionEnum
        {
            Unknown,

            Direct3D9,

            Direct3D10,

            Direct3D10_1,

            Direct3D11,

            Direct3D11_1,
        }

        /// <summary>
        /// The full list of DXGI functions, sorted by virtual table index
        /// </summary>
        internal enum DXGISwapChainVirtualTableEnum : Int16
        {
            //// IUnknown
            QueryInterface = 0,

            AddRef = 1,

            Release = 2,

            //// IDXGIObject
            SetPrivateData = 3,

            SetPrivateDataInterface = 4,

            GetPrivateData = 5,

            GetParent = 6,

            //// IDXGIDeviceSubObject
            GetDevice = 7,

            //// IDXGISwapChain
            Present = 8,

            GetBuffer = 9,

            SetFullscreenState = 10,

            GetFullscreenState = 11,

            GetDesc = 12,

            ResizeBuffers = 13,

            ResizeTarget = 14,

            GetContainingOutput = 15,

            GetFrameStatistics = 16,

            GetLastPresentCount = 17,
        }
        //// End DXGISwapChainVirtualTableEnum

        /// <summary>
        /// IDirect3DDevice9 interface to perform DrawPrimitive-based rendering, create resources, work with system-level variables,
        /// adjust gamma ramp levels, work with palettes, and create shaders.
        /// </summary>
        internal enum Direct3DDevice9FunctionOrdinalsEnum : Int16
        {
            /// <summary>
            /// Retrieves pointers to the supported interfaces on an object.
            /// </summary>
            QueryInterface = 0,

            /// <summary>
            /// Increments the reference count for an interface on an object. This method should be called for every new copy of a pointer to an interface on an object.
            /// </summary>
            AddRef = 1,

            /// <summary>
            /// Decrements the reference count for an interface on an object.
            /// </summary>
            Release = 2,

            /// <summary>
            /// Reports the current cooperative-level status of the Direct3D device for a windowed or full-screen application.
            /// </summary>
            TestCooperativeLevel = 3,

            /// <summary>
            /// Returns an estimate of the amount of available texture memory.
            /// </summary>
            GetAvailableTextureMem = 4,

            /// <summary>
            /// Evicts all managed resources, including both Direct3D and driver-managed resources.
            /// </summary>
            EvictManagedResources = 5,

            /// <summary>
            /// Returns an interface to the instance of the Direct3D object that created the device.
            /// </summary>
            GetDirect3D = 6,

            /// <summary>
            /// The GetDeviceCaps function retrieves device-specific information for the specified device.
            /// </summary>
            GetDeviceCaps = 7,

            /// <summary>
            /// Retrieves the display mode's spatial resolution, color resolution, and refresh frequency.
            /// </summary>
            GetDisplayMode = 8,

            /// <summary>
            /// Retrieves the creation parameters of the device.
            /// </summary>
            GetCreationParameters = 9,

            /// <summary>
            /// Sets properties for the cursor.
            /// </summary>
            SetCursorProperties = 10,

            /// <summary>
            /// Sets the cursor position and update options.
            /// </summary>
            SetCursorPosition = 11,

            /// <summary>
            /// Displays or hides the cursor.
            /// </summary>
            ShowCursor = 12,

            /// <summary>
            /// Creates an additional swap chain for rendering multiple views.
            /// </summary>
            CreateAdditionalSwapChain = 13,

            /// <summary>
            /// Gets a pointer to a swap chain.
            /// </summary>
            GetSwapChain = 14,

            /// <summary>
            /// Gets the number of implicit swap chains.
            /// </summary>
            GetNumberOfSwapChains = 15,

            /// <summary>
            /// Resets the type, size, and format of the swap chain.
            /// </summary>
            Reset = 16,

            /// <summary>
            /// Presents the contents of the next buffer in the sequence of back buffers owned by the device.
            /// </summary>
            Present = 17,

            /// <summary>
            /// Retrieves a back buffer from the device's swap chain.
            /// </summary>
            GetBackBuffer = 18,

            /// <summary>
            /// Returns information describing the raster of the monitor on which the swap chain is presented.
            /// </summary>
            GetRasterStatus = 19,

            /// <summary>
            /// This method allows the use of GDI dialog boxes in full-screen mode applications.
            /// </summary>
            SetDialogBoxMode = 20,

            /// <summary>
            /// Sets the gamma correction ramp for the implicit swap chain.
            /// This method will affect the entire screen (not just the active window if you are running in windowed mode).
            /// </summary>
            SetGammaRamp = 21,

            /// <summary>
            /// Retrieves the gamma correction ramp for the swap chain.
            /// </summary>
            GetGammaRamp = 22,

            /// <summary>
            /// Creates a texture resource.
            /// </summary>
            CreateTexture = 23,

            /// <summary>
            /// Creates a volume texture resource.
            /// </summary>
            CreateVolumeTexture = 24,

            /// <summary>
            /// Creates a cube texture resource.
            /// </summary>
            CreateCubeTexture = 25,

            /// <summary>
            /// Creates a vertex buffer.
            /// </summary>
            CreateVertexBuffer = 26,

            /// <summary>
            /// Creates an index buffer.
            /// </summary>
            CreateIndexBuffer = 27,

            /// <summary>
            /// Creates a render-target surface.
            /// </summary>
            CreateRenderTarget = 28,

            /// <summary>
            /// Creates a depth-stencil resource.
            /// </summary>
            CreateDepthStencilSurface = 29,

            /// <summary>
            /// Copies rectangular subsets of pixels from one surface to another.
            /// </summary>
            UpdateSurface = 30,

            /// <summary>
            /// Updates the dirty portions of a texture.
            /// </summary>
            UpdateTexture = 31,

            /// <summary>
            /// Copies the render-target data from device memory to system memory.
            /// </summary>
            GetRenderTargetData = 32,

            /// <summary>
            /// Generates a copy of the device's front buffer and places that copy in a system memory buffer provided by the application.
            /// </summary>
            GetFrontBufferData = 33,

            /// <summary>
            /// Copy the contents of the source rectangle to the destination rectangle.
            /// The source rectangle can be stretched and filtered by the copy.
            /// This function is often used to change the aspect ratio of a video stream.
            /// </summary>
            StretchRect = 34,

            /// <summary>
            /// Allows an application to fill a rectangular area of a D3DPOOL_DEFAULT surface with a specified color.
            /// </summary>
            ColorFill = 35,

            /// <summary>
            /// Create an off-screen surface.
            /// </summary>
            CreateOffscreenPlainSurface = 36,

            /// <summary>
            /// Sets a new color buffer for the device.
            /// </summary>
            SetRenderTarget = 37,

            /// <summary>
            /// Retrieves a render-target surface.
            /// </summary>
            GetRenderTarget = 38,

            /// <summary>
            /// Sets the depth stencil surface.
            /// </summary>
            SetDepthStencilSurface = 39,

            /// <summary>
            /// Gets the depth-stencil surface owned by the Direct3DDevice object.
            /// </summary>
            GetDepthStencilSurface = 40,

            /// <summary>
            /// Begins a scene.
            /// </summary>
            BeginScene = 41,

            /// <summary>
            /// Ends a scene that was begun by calling IDirect3DDevice9::BeginScene.
            /// </summary>
            EndScene = 42,

            /// <summary>
            /// Clears one or more surfaces such as a render target, multiple render targets, a stencil buffer, and a depth buffer.
            /// </summary>
            Clear = 43,

            /// <summary>
            /// Sets a single device transformation-related state.
            /// </summary>
            SetTransform = 44,

            /// <summary>
            /// Retrieves a matrix describing a transformation state.
            /// </summary>
            GetTransform = 45,

            /// <summary>
            /// Multiplies a device's world, view, or projection matrices by a specified matrix. 
            /// </summary>
            MultiplyTransform = 46,

            /// <summary>
            /// Sets the viewport parameters for the device.
            /// </summary>
            SetViewport = 47,

            /// <summary>
            /// Retrieves the viewport parameters currently set for the device.
            /// </summary>
            GetViewport = 48,

            /// <summary>
            /// Sets the material properties for the device.
            /// </summary>
            SetMaterial = 49,

            /// <summary>
            /// Retrieves the current material properties for the device.
            /// </summary>
            GetMaterial = 50,

            /// <summary>
            /// Assigns a set of lighting properties for this device.
            /// </summary>
            SetLight = 51,

            /// <summary>
            /// Retrieves a set of lighting properties that this device uses.
            /// </summary>
            GetLight = 52,

            /// <summary>
            /// Enables or disables a set of lighting parameters within a device.
            /// </summary>
            LightEnable = 53,

            /// <summary>
            /// Retrieves the activity status - enabled or disabled - for a set of lighting parameters within a device.
            /// </summary>
            GetLightEnable = 54,

            /// <summary>
            /// Sets the coefficients of a user-defined clipping plane for the device.
            /// </summary>
            SetClipPlane = 55,

            /// <summary>
            /// Retrieves the coefficients of a user-defined clipping plane for the device.
            /// </summary>
            GetClipPlane = 56,

            /// <summary>
            /// Sets a single device render-state parameter.
            /// </summary>
            SetRenderState = 57,

            /// <summary>
            /// Retrieves a render-state value for a device.
            /// </summary>
            GetRenderState = 58,

            /// <summary>
            /// Creates a new state block that contains the values for all device states, vertex-related states, or pixel-related states.
            /// </summary>
            CreateStateBlock = 59,

            /// <summary>
            /// Signals Direct3D to begin recording a device-state block.
            /// </summary>
            BeginStateBlock = 60,

            /// <summary>
            /// Signals Direct3D to stop recording a device-state block and retrieve a pointer to the state block interface.
            /// </summary>
            EndStateBlock = 61,

            /// <summary>
            /// Sets the clip status.
            /// </summary>
            SetClipStatus = 62,

            /// <summary>
            /// Retrieves the clip status.
            /// </summary>
            GetClipStatus = 63,

            /// <summary>
            /// Retrieves a texture assigned to a stage for a device.
            /// </summary>
            GetTexture = 64,

            /// <summary>
            /// Assigns a texture to a stage for a device.
            /// </summary>
            SetTexture = 65,

            /// <summary>
            /// Retrieves a state value for an assigned texture.
            /// </summary>
            GetTextureStageState = 66,

            /// <summary>
            /// Sets the state value for the currently assigned texture.
            /// </summary>
            SetTextureStageState = 67,

            /// <summary>
            /// Gets the sampler state value.
            /// </summary>
            GetSamplerState = 68,

            /// <summary>
            /// Sets the sampler state value.
            /// </summary>
            SetSamplerState = 69,

            /// <summary>
            /// Reports the device's ability to render the current texture-blending operations and arguments in a single pass.
            /// </summary>
            ValidateDevice = 70,

            /// <summary>
            /// Sets palette entries.
            /// </summary>
            SetPaletteEntries = 71,

            /// <summary>
            /// Retrieves palette entries.
            /// </summary>
            GetPaletteEntries = 72,

            /// <summary>
            /// Sets the current texture palette.
            /// </summary>
            SetCurrentTexturePalette = 73,

            /// <summary>
            /// Retrieves the current texture palette.
            /// </summary>
            GetCurrentTexturePalette = 74,

            /// <summary>
            /// Sets the scissor rectangle.
            /// </summary>
            SetScissorRect = 75,

            /// <summary>
            /// Gets the scissor rectangle.
            /// </summary>
            GetScissorRect = 76,

            /// <summary>
            /// Use this method to switch between software and hardware vertex processing.
            /// </summary>
            SetSoftwareVertexProcessing = 77,

            /// <summary>
            /// Gets the vertex processing (hardware or software) mode.
            /// </summary>
            GetSoftwareVertexProcessing = 78,

            /// <summary>
            /// Enable or disable N-patches.
            /// </summary>
            SetNPatchMode = 79,

            /// <summary>
            /// Gets the N-patch mode segments.
            /// </summary>
            GetNPatchMode = 80,

            /// <summary>
            /// Renders a sequence of nonindexed, geometric primitives of the specified type from the current set of data input streams.
            /// </summary>
            DrawPrimitive = 81,

            /// <summary>
            /// Based on indexing, renders the specified geometric primitive into an array of vertices.
            /// </summary>
            DrawIndexedPrimitive = 82,

            /// <summary>
            /// Renders data specified by a user memory pointer as a sequence of geometric primitives of the specified type.
            /// </summary>
            DrawPrimitiveUP = 83,

            /// <summary>
            /// Renders the specified geometric primitive with data specified by a user memory pointer.
            /// </summary>
            DrawIndexedPrimitiveUP = 84,

            /// <summary>
            /// Applies the vertex processing defined by the vertex shader to the set of input data streams,
            /// generating a single stream of interleaved vertex data to the destination vertex buffer. 
            /// </summary>
            ProcessVertices = 85,

            /// <summary>
            /// Create a vertex shader declaration from the device and the vertex elements.
            /// </summary>
            CreateVertexDeclaration = 86,

            /// <summary>
            /// Sets a Vertex Declaration (Direct3D 9).
            /// </summary>
            SetVertexDeclaration = 87,

            /// <summary>
            /// Gets a vertex shader declaration.
            /// </summary>
            GetVertexDeclaration = 88,

            /// <summary>
            /// Sets the current vertex stream declaration.
            /// </summary>
            SetFVF = 89,

            /// <summary>
            /// Gets the fixed vertex function declaration.
            /// </summary>
            GetFVF = 90,

            /// <summary>
            /// Creates a vertex shader.
            /// </summary>
            CreateVertexShader = 91,

            /// <summary>
            /// Sets the vertex shader.
            /// </summary>
            SetVertexShader = 92,

            /// <summary>
            /// Retrieves the currently set vertex shader.
            /// </summary>
            GetVertexShader = 93,

            /// <summary>
            /// Sets a floating-point vertex shader constant.
            /// </summary>
            SetVertexShaderConstantF = 94,

            /// <summary>
            /// Gets a floating-point vertex shader constant.
            /// </summary>
            GetVertexShaderConstantF = 95,

            /// <summary>
            /// Sets an integer vertex shader constant.
            /// </summary>
            SetVertexShaderConstantI = 96,

            /// <summary>
            /// Gets an integer vertex shader constant.
            /// </summary>
            GetVertexShaderConstantI = 97,

            /// <summary>
            /// Sets a Boolean vertex shader constant.
            /// </summary>
            SetVertexShaderConstantB = 98,

            /// <summary>
            /// Gets a Boolean vertex shader constant.
            /// </summary>
            GetVertexShaderConstantB = 99,

            /// <summary>
            /// Binds a vertex buffer to a device data stream. For more information, see Setting the Stream Source (Direct3D 9).
            /// </summary>
            SetStreamSource = 100,

            /// <summary>
            /// Retrieves a vertex buffer bound to the specified data stream.
            /// </summary>
            GetStreamSource = 101,

            /// <summary>
            /// Sets the stream source frequency divider value. This may be used to draw several instances of geometry.
            /// </summary>
            SetStreamSourceFreq = 102,

            /// <summary>
            /// Gets the stream source frequency divider value.
            /// </summary>
            GetStreamSourceFreq = 103,

            /// <summary>
            /// Sets index data.
            /// </summary>
            SetIndices = 104,

            /// <summary>
            /// Retrieves index data.
            /// </summary>
            GetIndices = 105,

            /// <summary>
            /// Creates a pixel shader.
            /// </summary>
            CreatePixelShader = 106,

            /// <summary>
            /// Sets the current pixel shader to a previously created pixel shader.
            /// </summary>
            SetPixelShader = 107,

            /// <summary>
            /// Retrieves the currently set pixel shader.
            /// </summary>
            GetPixelShader = 108,

            /// <summary>
            /// Sets a floating-point shader constant.
            /// </summary>
            SetPixelShaderConstantF = 109,

            /// <summary>
            /// Gets a floating-point shader constant.
            /// </summary>
            GetPixelShaderConstantF = 110,

            /// <summary>
            /// Sets an integer shader constant.
            /// </summary>
            SetPixelShaderConstantI = 111,

            /// <summary>
            /// Gets an integer shader constant.
            /// </summary>
            GetPixelShaderConstantI = 112,

            /// <summary>
            /// Sets a Boolean shader constant.
            /// </summary>
            SetPixelShaderConstantB = 113,

            /// <summary>
            /// Gets a Boolean shader constant.
            /// </summary>
            GetPixelShaderConstantB = 114,

            /// <summary>
            /// Draws a rectangular patch using the currently set streams.
            /// </summary>
            DrawRectPatch = 115,

            /// <summary>
            /// Draws a triangular patch using the currently set streams.
            /// </summary>
            DrawTriPatch = 116,

            /// <summary>
            /// Frees a cached high-order patch.
            /// </summary>
            DeletePatch = 117,

            /// <summary>
            /// Creates a status query.
            /// </summary>
            CreateQuery = 118,
        }
        //// End Direct3DDevice9FunctionOrdinalsEnum

        /// <summary>
        /// IDirect3DDevice9Ex interface to render primitives, create resources, work with system-level variables, adjust gamma ramp levels,
        /// with work palettes, and create shaders. The IDirect3DDevice9Ex interface derives from the IDirect3DDevice9 interface.
        /// </summary>
        internal enum Direct3DDevice9ExFunctionOrdinalsEnum : Int16
        {
            /// <summary>
            /// Prepare the texture sampler for monochrome convolution filtering on a single-color texture.
            /// </summary>
            SetConvolutionMonoKernel = 119,

            /// <summary>
            /// Copy a text string to one surface using an alphabet of glyphs on another surface. Composition is done by the GPU using bitwise operations.
            /// </summary>
            ComposeRects = 120,

            /// <summary>
            /// Swap the swapchain's next buffer with the front buffer.
            /// </summary>
            PresentEx = 121,

            /// <summary>
            /// Get the priority of the GPU thread.
            /// </summary>
            GetGPUThreadPriority = 122,

            /// <summary>
            /// Set the priority on the GPU thread.
            /// </summary>
            SetGPUThreadPriority = 123,

            /// <summary>
            /// Suspend execution of the calling thread until the next vertical blank signal.
            /// </summary>
            WaitForVBlank = 124,

            /// <summary>
            /// Checks an array of resources to determine if it is likely that they will cause a large stall at Draw time because the system must make the resources GPU-accessible.
            /// </summary>
            CheckResourceResidency = 125,

            /// <summary>
            /// Set the number of frames that the system is allowed to queue for rendering.
            /// </summary>
            SetMaximumFrameLatency = 126,

            /// <summary>
            /// Retrieves the number of frames of data that the system is allowed to queue.
            /// </summary>
            GetMaximumFrameLatency = 127,

            /// <summary>
            /// Reports the current cooperative-level status of the Direct3D device for a windowed or full-screen application.
            /// </summary>
            CheckDeviceState = 128,

            /// <summary>
            /// Creates a render-target surface.
            /// </summary>
            CreateRenderTargetEx = 129,

            /// <summary>
            /// Create an off-screen surface.
            /// </summary>
            CreateOffscreenPlainSurfaceEx = 130,

            /// <summary>
            /// Creates a depth-stencil surface.
            /// </summary>
            CreateDepthStencilSurfaceEx = 131,

            /// <summary>
            /// Resets the type, size, and format of the swap chain with all other surfaces persistent.
            /// </summary>
            ResetEx = 132,

            /// <summary>
            /// Retrieves the display mode's spatial resolution, color resolution, refresh frequency, and rotation settings.
            /// </summary>
            GetDisplayModeEx = 133,
        }
        //// End Direct3DDevice9ExFunctionOrdinalsEnum

        internal enum D3D10DeviceVirtualTableEnum : Int16
        {
            //// IUnknown
            QueryInterface = 0,
            AddRef = 1,
            Release = 2,

            //// ID3D10Device
            VSSetConstantBuffers = 3,
            PSSetShaderResources = 4,
            PSSetShader = 5,
            PSSetSamplers = 6,
            VSSetShader = 7,
            DrawIndexed = 8,
            Draw = 9,
            PSSetConstantBuffers = 10,
            IASetInputLayout = 11,
            IASetVertexBuffers = 12,
            IASetIndexBuffer = 13,
            DrawIndexedInstanced = 14,
            DrawInstanced = 15,
            GSSetConstantBuffers = 16,
            GSSetShader = 17,
            IASetPrimitiveTopology = 18,
            VSSetShaderResources = 19,
            VSSetSamplers = 20,
            SetPredication = 21,
            GSSetShaderResources = 22,
            GSSetSamplers = 23,
            OMSetRenderTargets = 24,
            OMSetBlendState = 25,
            OMSetDepthStencilState = 26,
            SOSetTargets = 27,
            DrawAuto = 28,
            RSSetState = 29,
            RSSetViewports = 30,
            RSSetScissorRects = 31,
            CopySubresourceRegion = 32,
            CopyResource = 33,
            UpdateSubresource = 34,
            ClearRenderTargetView = 35,
            ClearDepthStencilView = 36,
            GenerateMips = 37,
            ResolveSubresource = 38,
            VSGetConstantBuffers = 39,
            PSGetShaderResources = 40,
            PSGetShader = 41,
            PSGetSamplers = 42,
            VSGetShader = 43,
            PSGetConstantBuffers = 44,
            IAGetInputLayout = 45,
            IAGetVertexBuffers = 46,
            IAGetIndexBuffer = 47,
            GSGetConstantBuffers = 48,
            GSGetShader = 49,
            IAGetPrimitiveTopology = 50,
            VSGetShaderResources = 51,
            VSGetSamplers = 52,
            GetPredication = 53,
            GSGetShaderResources = 54,
            GSGetSamplers = 55,
            OMGetRenderTargets = 56,
            OMGetBlendState = 57,
            OMGetDepthStencilState = 58,
            SOGetTargets = 59,
            RSGetState = 60,
            RSGetViewports = 61,
            RSGetScissorRects = 62,
            GetDeviceRemovedReason = 63,
            SetExceptionMode = 64,
            GetExceptionMode = 65,
            GetPrivateData = 66,
            SetPrivateData = 67,
            SetPrivateDataInterface = 68,
            ClearState = 69,
            Flush = 70,
            CreateBuffer = 71,
            CreateTexture1D = 72,
            CreateTexture2D = 73,
            CreateTexture3D = 74,
            CreateShaderResourceView = 75,
            CreateRenderTargetView = 76,
            CreateDepthStencilView = 77,
            CreateInputLayout = 78,
            CreateVertexShader = 79,
            CreateGeometryShader = 80,
            CreateGemoetryShaderWithStreamOutput = 81,
            CreatePixelShader = 82,
            CreateBlendState = 83,
            CreateDepthStencilState = 84,
            CreateRasterizerState = 85,
            CreateSamplerState = 86,
            CreateQuery = 87,
            CreatePredicate = 88,
            CreateCounter = 89,
            CheckFormatSupport = 90,
            CheckMultisampleQualityLevels = 91,
            CheckCounterInfo = 92,
            CheckCounter = 93,
            GetCreationFlags = 94,
            OpenSharedResource = 95,
            SetTextFilterSize = 96,
            GetTextFilterSize = 97,
        }
        //// End D3D10DeviceVirtualTableEnum

        internal enum D3D101DeviceVirtualTableEnum : Int16
        {
            //// IUnknown
            QueryInterface = 0,

            AddRef = 1,

            Release = 2,

            //// ID3D10Device
            VSSetConstantBuffers = 3,

            PSSetShaderResources = 4,

            PSSetShader = 5,

            PSSetSamplers = 6,

            VSSetShader = 7,

            DrawIndexed = 8,

            Draw = 9,

            PSSetConstantBuffers = 10,

            IASetInputLayout = 11,

            IASetVertexBuffers = 12,

            IASetIndexBuffer = 13,

            DrawIndexedInstanced = 14,

            DrawInstanced = 15,

            GSSetConstantBuffers = 16,

            GSSetShader = 17,

            IASetPrimitiveTopology = 18,

            VSSetShaderResources = 19,

            VSSetSamplers = 20,

            SetPredication = 21,

            GSSetShaderResources = 22,

            GSSetSamplers = 23,

            OMSetRenderTargets = 24,

            OMSetBlendState = 25,

            OMSetDepthStencilState = 26,

            SOSetTargets = 27,

            DrawAuto = 28,

            RSSetState = 29,

            RSSetViewports = 30,

            RSSetScissorRects = 31,

            CopySubresourceRegion = 32,

            CopyResource = 33,

            UpdateSubresource = 34,

            ClearRenderTargetView = 35,

            ClearDepthStencilView = 36,

            GenerateMips = 37,

            ResolveSubresource = 38,

            VSGetConstantBuffers = 39,

            PSGetShaderResources = 40,

            PSGetShader = 41,

            PSGetSamplers = 42,

            VSGetShader = 43,

            PSGetConstantBuffers = 44,

            IAGetInputLayout = 45,

            IAGetVertexBuffers = 46,

            IAGetIndexBuffer = 47,

            GSGetConstantBuffers = 48,

            GSGetShader = 49,

            IAGetPrimitiveTopology = 50,

            VSGetShaderResources = 51,

            VSGetSamplers = 52,

            GetPredication = 53,

            GSGetShaderResources = 54,

            GSGetSamplers = 55,

            OMGetRenderTargets = 56,

            OMGetBlendState = 57,

            OMGetDepthStencilState = 58,

            SOGetTargets = 59,

            RSGetState = 60,

            RSGetViewports = 61,

            RSGetScissorRects = 62,

            GetDeviceRemovedReason = 63,

            SetExceptionMode = 64,

            GetExceptionMode = 65,

            GetPrivateData = 66,

            SetPrivateData = 67,

            SetPrivateDataInterface = 68,

            ClearState = 69,

            Flush = 70,

            CreateBuffer = 71,

            CreateTexture1D = 72,

            CreateTexture2D = 73,

            CreateTexture3D = 74,

            CreateShaderResourceView = 75,

            CreateRenderTargetView = 76,

            CreateDepthStencilView = 77,

            CreateInputLayout = 78,

            CreateVertexShader = 79,

            CreateGeometryShader = 80,

            CreateGemoetryShaderWithStreamOutput = 81,

            CreatePixelShader = 82,

            CreateBlendState = 83,

            CreateDepthStencilState = 84,

            CreateRasterizerState = 85,

            CreateSamplerState = 86,

            CreateQuery = 87,

            CreatePredicate = 88,

            CreateCounter = 89,

            CheckFormatSupport = 90,

            CheckMultisampleQualityLevels = 91,

            CheckCounterInfo = 92,

            CheckCounter = 93,

            GetCreationFlags = 94,

            OpenSharedResource = 95,

            SetTextFilterSize = 96,

            GetTextFilterSize = 97,

            //// ID3D10Device1
            CreateShaderResourceView1 = 98,

            CreateBlendState1 = 99,

            GetFeatureLevel = 100,
        }
        //// End D3D101DeviceVirtualTableEnum

        internal enum D3D11DeviceVirtualTableEnum : Int16
        {
            //// IUnknown
            QueryInterface = 0,

            AddRef = 1,

            Release = 2,

            //// ID3D11Device
            CreateBuffer = 3,

            CreateTexture1D = 4,

            CreateTexture2D = 5,

            CreateTexture3D = 6,

            CreateShaderResourceView = 7,

            CreateUnorderedAccessView = 8,

            CreateRenderTargetView = 9,

            CreateDepthStencilView = 10,

            CreateInputLayout = 11,

            CreateVertexShader = 12,

            CreateGeometryShader = 13,

            CreateGeometryShaderWithStreamOutput = 14,

            CreatePixelShader = 15,

            CreateHullShader = 16,

            CreateDomainShader = 17,

            CreateComputeShader = 18,

            CreateClassLinkage = 19,

            CreateBlendState = 20,

            CreateDepthStencilState = 21,

            CreateRasterizerState = 22,

            CreateSamplerState = 23,

            CreateQuery = 24,

            CreatePredicate = 25,

            CreateCounter = 26,

            CreateDeferredContext = 27,

            OpenSharedResource = 28,

            CheckFormatSupport = 29,

            CheckMultisampleQualityLevels = 30,

            CheckCounterInfo = 31,

            CheckCounter = 32,

            CheckFeatureSupport = 33,

            GetPrivateData = 34,

            SetPrivateData = 35,

            SetPrivateDataInterface = 36,

            GetFeatureLevel = 37,

            GetCreationFlags = 38,

            GetDeviceRemovedReason = 39,

            GetImmediateContext = 40,

            SetExceptionMode = 41,

            GetExceptionMode = 42,
        }
        //// End D3D11DeviceVirtualTableEnum
    }
    //// End class
}
//// End namespace