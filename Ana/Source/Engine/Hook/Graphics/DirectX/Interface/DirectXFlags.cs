namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface
{
    using System;

    /// <summary>
    /// A collection of virtual table flags for supported DirectX versions
    /// </summary>
    internal static class DirectXFlags
    {
        /// <summary>
        /// Number of methods in the DirectX 9 Device table
        /// </summary>
        public static readonly Int32 D3D9DeviceMethodCount = Enum.GetNames(typeof(Direct3DDevice9FunctionOrdinalsEnum)).Length;

        /// <summary>
        /// Number of methods in the DirectX 9 DeviceEx table
        /// </summary>
        public static readonly Int32 D3D9ExDeviceMethodCount = Enum.GetNames(typeof(Direct3DDevice9ExFunctionOrdinalsEnum)).Length;

        /// <summary>
        /// Number of methods in the DirectX 10 Device virtual table
        /// </summary>
        public static readonly Int32 D3D10DeviceMethodCount = Enum.GetNames(typeof(D3D10DeviceVirtualTableEnum)).Length;

        /// <summary>
        /// Number of methods in the DirectX 10.1 Device virtual table
        /// </summary>
        public static readonly Int32 D3D101DeviceMethodCount = Enum.GetNames(typeof(D3D101DeviceVirtualTableEnum)).Length;

        /// <summary>
        /// Number of methods in the DirectX 11 Device virtual table
        /// </summary>
        public static readonly Int32 D3D11DeviceMethodCount = Enum.GetNames(typeof(D3D11DeviceVirtualTableEnum)).Length;

        /// <summary>
        /// Number of methods in the DirectX 11.1 Device virtual table
        /// </summary>
        public static readonly Int32 DXGISwapChainMethodCount = Enum.GetNames(typeof(DXGISwapChainVirtualTableEnum)).Length;

        /// <summary>
        /// Versions of DirectX the graphic hook supports
        /// </summary>
        public enum Direct3DVersionEnum
        {
            /// <summary>
            /// Unknown DirectX version
            /// </summary>
            Unknown,

            /// <summary>
            /// DirectX 9
            /// </summary>
            Direct3D9,

            /// <summary>
            /// DirectX 10
            /// </summary>
            Direct3D10,

            /// <summary>
            /// DirectX 10.1
            /// </summary>
            Direct3D10_1,

            /// <summary>
            /// DirectX 11
            /// </summary>
            Direct3D11,

            /// <summary>
            /// DirectX 11.1
            /// </summary>
            Direct3D11_1,
        }

        /// <summary>
        /// The full list of DXGI functions, sorted by virtual table index
        /// </summary>
        internal enum DXGISwapChainVirtualTableEnum : Int16
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
            /// Sets application-defined data to the object and associates that data with a GUID.
            /// </summary>
            SetPrivateData = 3,

            /// <summary>
            /// Set an interface in the object's private data.
            /// </summary>
            SetPrivateDataInterface = 4,

            /// <summary>
            /// Get a pointer to the object's data.
            /// </summary>
            GetPrivateData = 5,

            /// <summary>
            /// Gets the parent of the object.
            /// </summary>
            GetParent = 6,

            /// <summary>
            /// Retrieves the device.
            /// </summary>
            GetDevice = 7,

            /// <summary>
            /// Presents a rendered image to the user.
            /// </summary>
            Present = 8,

            /// <summary>
            /// Accesses one of the swap-chain's back buffers.
            /// </summary>
            GetBuffer = 9,

            /// <summary>
            /// Sets the display state to windowed or full screen.
            /// </summary>
            SetFullscreenState = 10,

            /// <summary>
            /// Get the state associated with full-screen mode.
            /// </summary>
            GetFullscreenState = 11,

            /// <summary>
            /// Get a description of the swap chain.
            /// </summary>
            GetDesc = 12,

            /// <summary>
            /// Changes the swap chain's back buffer size, format, and number of buffers. This should be called when the application window is resized. 
            /// </summary>
            ResizeBuffers = 13,

            /// <summary>
            /// Resizes the output target.
            /// </summary>
            ResizeTarget = 14,

            /// <summary>
            /// Get the output (the display monitor) that contains the majority of the client area of the target window.
            /// </summary>
            GetContainingOutput = 15,

            /// <summary>
            /// Gets performance statistics about the last render frame.
            /// </summary>
            GetFrameStatistics = 16,

            /// <summary>
            /// Gets the number of times that IDXGISwapChain::Present or IDXGISwapChain1::Present1 has been called.
            /// </summary>
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

        /// <summary>
        /// The device interface represents a virtual adapter for Direct3D 10.0; it is used to perform rendering and create Direct3D resources.
        /// </summary>
        internal enum D3D10DeviceVirtualTableEnum : Int16
        {
            /// <summary>
            /// Retrieves pointers to the supported interfaces on an object.
            /// </summary>
            QueryInterface = 0,

            /// <summary>
            /// Increments the reference count for an interface on an object.
            /// </summary>
            AddRef = 1,

            /// <summary>
            /// Decrements the reference count for an interface on an object.
            /// </summary>
            Release = 2,

            /// <summary>
            /// Sets the constant buffers used by the vertex shader pipeline stage.
            /// </summary>
            VSSetConstantBuffers = 3,

            /// <summary>
            /// Bind an array of shader resources to the pixel shader stage.
            /// </summary>
            PSSetShaderResources = 4,

            /// <summary>
            /// Sets a pixel shader to the device.
            /// </summary>
            PSSetShader = 5,

            /// <summary>
            /// Set an array of sampler states to the pixel shader pipeline stage.
            /// </summary>
            PSSetSamplers = 6,

            /// <summary>
            /// Set a vertex shader to the device.
            /// </summary>
            VSSetShader = 7,

            /// <summary>
            /// Draw indexed, non-instanced primitives.
            /// </summary>
            DrawIndexed = 8,

            /// <summary>
            /// Draw non-indexed, non-instanced primitives.
            /// </summary>
            Draw = 9,

            /// <summary>
            /// Set the constant buffers used by the pixel shader pipeline stage.
            /// </summary>
            PSSetConstantBuffers = 10,

            /// <summary>
            /// Bind an input-layout object to the input-assembler stage.
            /// </summary>
            IASetInputLayout = 11,

            /// <summary>
            /// Bind an array of vertex buffers to the input-assembler stage.
            /// </summary>
            IASetVertexBuffers = 12,

            /// <summary>
            /// Bind an index buffer to the input-assembler stage.
            /// </summary>
            IASetIndexBuffer = 13,

            /// <summary>
            /// Draw indexed, instanced primitives.
            /// </summary>
            DrawIndexedInstanced = 14,

            /// <summary>
            /// Draw non-indexed, instanced primitives.
            /// </summary>
            DrawInstanced = 15,

            /// <summary>
            /// Set the constant buffers used by the geometry shader pipeline stage.
            /// </summary>
            GSSetConstantBuffers = 16,

            /// <summary>
            /// Set a geometry shader to the device.
            /// </summary>
            GSSetShader = 17,

            /// <summary>
            /// Bind information about the primitive type, and data order that describes input data for the input assembler stage.
            /// </summary>
            IASetPrimitiveTopology = 18,

            /// <summary>
            /// Bind an array of shader resources to the vertex shader stage.
            /// </summary>
            VSSetShaderResources = 19,

            /// <summary>
            /// Set an array of sampler states to the vertex shader pipeline stage.
            /// </summary>
            VSSetSamplers = 20,

            /// <summary>
            /// Set a rendering predicate.
            /// </summary>
            SetPredication = 21,

            /// <summary>
            /// Bind an array of shader resources to the geometry shader stage.
            /// </summary>
            GSSetShaderResources = 22,

            /// <summary>
            /// Set an array of sampler states to the geometry shader pipeline stage.
            /// </summary>
            GSSetSamplers = 23,

            /// <summary>
            /// Bind one or more render targets and the depth-stencil buffer to the output-merger stage.
            /// </summary>
            OMSetRenderTargets = 24,

            /// <summary>
            /// Set the blend state of the output-merger stage.
            /// </summary>
            OMSetBlendState = 25,

            /// <summary>
            /// Sets the depth-stencil state of the output-merger stage.
            /// </summary>
            OMSetDepthStencilState = 26,

            /// <summary>
            /// Set the target output buffers for the StreamOutput stage, which enables/disables the pipeline to stream-out data.
            /// </summary>
            SOSetTargets = 27,

            /// <summary>
            /// Draw geometry of an unknown size that was created by the geometry shader stage. See remarks.
            /// </summary>
            DrawAuto = 28,

            /// <summary>
            /// Set the rasterizer state for the rasterizer stage of the pipeline.
            /// </summary>
            RSSetState = 29,

            /// <summary>
            /// Bind an array of viewports to the rasterizer stage of the pipeline.
            /// </summary>
            RSSetViewports = 30,

            /// <summary>
            /// Bind an array of scissor rectangles to the rasterizer stage.
            /// </summary>
            RSSetScissorRects = 31,

            /// <summary>
            /// Copy a region from a source resource to a destination resource.
            /// </summary>
            CopySubresourceRegion = 32,

            /// <summary>
            /// Copy the entire contents of the source resource to the destination resource using the GPU. 
            /// </summary>
            CopyResource = 33,

            /// <summary>
            /// The CPU copies data from memory to a subresource created in non-mappable memory. See remarks.
            /// </summary>
            UpdateSubresource = 34,

            /// <summary>
            /// Set all the elements in a render target to one value.
            /// </summary>
            ClearRenderTargetView = 35,

            /// <summary>
            /// Clears the depth-stencil resource.
            /// </summary>
            ClearDepthStencilView = 36,

            /// <summary>
            /// Generates mipmaps for the given shader resource.
            /// </summary>
            GenerateMips = 37,

            /// <summary>
            /// Copy a multisampled resource into a non-multisampled resource.
            /// This API is most useful when re-using the resulting rendertarget of one render pass as an input to a second render pass.
            /// </summary>
            ResolveSubresource = 38,

            /// <summary>
            /// Get the constant buffers used by the vertex shader pipeline stage.
            /// </summary>
            VSGetConstantBuffers = 39,

            /// <summary>
            /// Get the pixel shader resources.
            /// </summary>
            PSGetShaderResources = 40,

            /// <summary>
            /// Get the pixel shader currently set on the device.
            /// </summary>
            PSGetShader = 41,

            /// <summary>
            /// Get an array of sampler states from the pixel shader pipeline stage.
            /// </summary>
            PSGetSamplers = 42,

            /// <summary>
            /// Get the vertex shader currently set on the device.
            /// </summary>
            VSGetShader = 43,

            /// <summary>
            /// Get the constant buffers used by the pixel shader pipeline stage.
            /// </summary>
            PSGetConstantBuffers = 44,

            /// <summary>
            /// Get a pointer to the input-layout object that is bound to the input-assembler stage.
            /// </summary>
            IAGetInputLayout = 45,

            /// <summary>
            /// Get the vertex buffers bound to the input-assembler stage.
            /// </summary>
            IAGetVertexBuffers = 46,

            /// <summary>
            /// Get a pointer to the index buffer that is bound to the input-assembler stage.
            /// </summary>
            IAGetIndexBuffer = 47,

            /// <summary>
            /// Get the constant buffers used by the geometry shader pipeline stage.
            /// </summary>
            GSGetConstantBuffers = 48,

            /// <summary>
            /// Get the geometry shader currently set on the device.
            /// </summary>
            GSGetShader = 49,

            /// <summary>
            /// Get information about the primitive type, and data order that describes input data for the input assembler stage.
            /// </summary>
            IAGetPrimitiveTopology = 50,

            /// <summary>
            /// Get the vertex shader resources.
            /// </summary>
            VSGetShaderResources = 51,

            /// <summary>
            /// Get an array of sampler states from the vertex shader pipeline stage.
            /// </summary>
            VSGetSamplers = 52,

            /// <summary>
            /// Get the rendering predicate state.
            /// </summary>
            GetPredication = 53,

            /// <summary>
            /// Get the geometry shader resources.
            /// </summary>
            GSGetShaderResources = 54,

            /// <summary>
            /// Get an array of sampler states from the geometry shader pipeline stage.
            /// </summary>
            GSGetSamplers = 55,

            /// <summary>
            /// Get pointers to the render targets and the depth-stencil buffer that are available to the output-merger stage.
            /// </summary>
            OMGetRenderTargets = 56,

            /// <summary>
            /// Get the blend state of the output-merger stage.
            /// </summary>
            OMGetBlendState = 57,

            /// <summary>
            /// Gets the depth-stencil state of the output-merger stage.
            /// </summary>
            OMGetDepthStencilState = 58,

            /// <summary>
            /// Get the target output buffers for the StreamOutput stage of the pipeline.
            /// </summary>
            SOGetTargets = 59,

            /// <summary>
            /// Get the rasterizer state from the rasterizer stage of the pipeline.
            /// </summary>
            RSGetState = 60,

            /// <summary>
            /// Get the array of viewports bound to the rasterizer stage
            /// </summary>
            RSGetViewports = 61,

            /// <summary>
            /// Get the array of scissor rectangles bound to the rasterizer stage.
            /// </summary>
            RSGetScissorRects = 62,

            /// <summary>
            /// Get the reason why the device was removed.
            /// </summary>
            GetDeviceRemovedReason = 63,

            /// <summary>
            /// Get the exception-mode flags.
            /// </summary>
            SetExceptionMode = 64,

            /// <summary>
            /// Get the exception-mode flags.
            /// </summary>
            GetExceptionMode = 65,

            /// <summary>
            /// Get data from a device that is associated with a guid.
            /// </summary>
            GetPrivateData = 66,

            /// <summary>
            /// Set data to a device and associate that data with a guid.
            /// </summary>
            SetPrivateData = 67,

            /// <summary>
            /// Associate an IUnknown-derived interface with this device and associate that interface with an application-defined guid.
            /// </summary>
            SetPrivateDataInterface = 68,

            /// <summary>
            /// Restore all default device settings; return the device to the state it was in when it was created.
            /// This will set all set all input/output resource slots, shaders, input layouts, predications, scissor rectangles,
            /// depth-stencil state, rasterizer state, blend state, sampler state, and viewports to NULL. The primitive topology will be set to UNDEFINED.
            /// </summary>
            ClearState = 69,

            /// <summary>
            /// Send queued-up commands in the command buffer to the GPU.
            /// </summary>
            Flush = 70,

            /// <summary>
            /// Create a buffer (vertex buffer, index buffer, or shader-constant buffer).
            /// </summary>
            CreateBuffer = 71,

            /// <summary>
            /// Create an array of 1D textures (see Texture1D).
            /// </summary>
            CreateTexture1D = 72,

            /// <summary>
            /// Create an array of 2D textures (see Texture2D).
            /// </summary>
            CreateTexture2D = 73,

            /// <summary>
            /// Create a single 3D texture (see Texture3D).
            /// </summary>
            CreateTexture3D = 74,

            /// <summary>
            /// Create a shader-resource view for accessing data in a resource.
            /// </summary>
            CreateShaderResourceView = 75,

            /// <summary>
            /// Create a render-target view for accessing resource data.
            /// </summary>
            CreateRenderTargetView = 76,

            /// <summary>
            /// Create a depth-stencil view for accessing resource data.
            /// </summary>
            CreateDepthStencilView = 77,

            /// <summary>
            /// Create an input-layout object to describe the input-buffer data for the input-assembler stage.
            /// </summary>
            CreateInputLayout = 78,

            /// <summary>
            /// Create a vertex-shader object from a compiled shader.
            /// </summary>
            CreateVertexShader = 79,

            /// <summary>
            /// Create a geometry shader.
            /// </summary>
            CreateGeometryShader = 80,

            /// <summary>
            /// Creates a geometry shader that can write to streaming output buffers.
            /// </summary>
            CreateGeometryShaderWithStreamOutput = 81,

            /// <summary>
            /// Create a pixel shader.
            /// </summary>
            CreatePixelShader = 82,

            /// <summary>
            /// Create a blend-state object that encapsules blend state for the output-merger stage.
            /// </summary>
            CreateBlendState = 83,

            /// <summary>
            /// Create a depth-stencil state object that encapsulates depth-stencil test information for the output-merger stage.
            /// </summary>
            CreateDepthStencilState = 84,

            /// <summary>
            /// Create a rasterizer state object that tells the rasterizer stage how to behave.
            /// </summary>
            CreateRasterizerState = 85,

            /// <summary>
            /// Create a sampler-state object that encapsulates sampling information for a texture.
            /// </summary>
            CreateSamplerState = 86,

            /// <summary>
            /// This interface encapsulates methods for querying information from the GPU.
            /// </summary>
            CreateQuery = 87,

            /// <summary>
            /// Creates a predicate.
            /// </summary>
            CreatePredicate = 88,

            /// <summary>
            /// Create a counter object for measuring GPU performance.
            /// </summary>
            CreateCounter = 89,

            /// <summary>
            /// Get the support of a given format on the installed video device.
            /// </summary>
            CheckFormatSupport = 90,

            /// <summary>
            /// Get the number of quality levels available during multisampling.
            /// </summary>
            CheckMultisampleQualityLevels = 91,

            /// <summary>
            /// Get a counter's information.
            /// </summary>
            CheckCounterInfo = 92,

            /// <summary>
            /// Get the type, name, units of measure, and a description of an existing counter.
            /// </summary>
            CheckCounter = 93,

            /// <summary>
            /// Get the flags used during the call to create the device with D3D10CreateDevice.
            /// </summary>
            GetCreationFlags = 94,

            /// <summary>
            /// Give a device access to a shared resource created on a different Direct3d device. 
            /// </summary>
            OpenSharedResource = 95,

            /// <summary>
            /// This method is not implemented.
            /// </summary>
            SetTextFilterSize = 96,

            /// <summary>
            /// This method is not implemented.
            /// </summary>
            GetTextFilterSize = 97,
        }
        //// End D3D10DeviceVirtualTableEnum

        /// <summary>
        /// The device interface represents a virtual adapter for Direct3D 10.1; it is used to perform rendering and create Direct3D resources.
        /// </summary>
        internal enum D3D101DeviceVirtualTableEnum : Int16
        {
            /// <summary>
            /// Retrieves pointers to the supported interfaces on an object.
            /// </summary>
            QueryInterface = 0,

            /// <summary>
            /// Increments the reference count for an interface on an object.
            /// </summary>
            AddRef = 1,

            /// <summary>
            /// Decrements the reference count for an interface on an object.
            /// </summary>
            Release = 2,

            /// <summary>
            /// Sets the constant buffers used by the vertex shader pipeline stage.
            /// </summary>
            VSSetConstantBuffers = 3,

            /// <summary>
            /// Bind an array of shader resources to the pixel shader stage.
            /// </summary>
            PSSetShaderResources = 4,

            /// <summary>
            /// Sets a pixel shader to the device.
            /// </summary>
            PSSetShader = 5,

            /// <summary>
            /// Set an array of sampler states to the pixel shader pipeline stage.
            /// </summary>
            PSSetSamplers = 6,

            /// <summary>
            /// Set a vertex shader to the device.
            /// </summary>
            VSSetShader = 7,

            /// <summary>
            /// Draw indexed, non-instanced primitives.
            /// </summary>
            DrawIndexed = 8,

            /// <summary>
            /// Draw non-indexed, non-instanced primitives.
            /// </summary>
            Draw = 9,

            /// <summary>
            /// Set the constant buffers used by the pixel shader pipeline stage.
            /// </summary>
            PSSetConstantBuffers = 10,

            /// <summary>
            /// Bind an input-layout object to the input-assembler stage.
            /// </summary>
            IASetInputLayout = 11,

            /// <summary>
            /// Bind an array of vertex buffers to the input-assembler stage.
            /// </summary>
            IASetVertexBuffers = 12,

            /// <summary>
            /// Bind an index buffer to the input-assembler stage.
            /// </summary>
            IASetIndexBuffer = 13,

            /// <summary>
            /// Draw indexed, instanced primitives.
            /// </summary>
            DrawIndexedInstanced = 14,

            /// <summary>
            /// Draw non-indexed, instanced primitives.
            /// </summary>
            DrawInstanced = 15,

            /// <summary>
            /// Set the constant buffers used by the geometry shader pipeline stage.
            /// </summary>
            GSSetConstantBuffers = 16,

            /// <summary>
            /// Set a geometry shader to the device.
            /// </summary>
            GSSetShader = 17,

            /// <summary>
            /// Bind information about the primitive type, and data order that describes input data for the input assembler stage.
            /// </summary>
            IASetPrimitiveTopology = 18,

            /// <summary>
            /// Bind an array of shader resources to the vertex shader stage.
            /// </summary>
            VSSetShaderResources = 19,

            /// <summary>
            /// Set an array of sampler states to the vertex shader pipeline stage.
            /// </summary>
            VSSetSamplers = 20,

            /// <summary>
            /// Set a rendering predicate.
            /// </summary>
            SetPredication = 21,

            /// <summary>
            /// Bind an array of shader resources to the geometry shader stage.
            /// </summary>
            GSSetShaderResources = 22,

            /// <summary>
            /// Set an array of sampler states to the geometry shader pipeline stage.
            /// </summary>
            GSSetSamplers = 23,

            /// <summary>
            /// Bind one or more render targets and the depth-stencil buffer to the output-merger stage.
            /// </summary>
            OMSetRenderTargets = 24,

            /// <summary>
            /// Set the blend state of the output-merger stage.
            /// </summary>
            OMSetBlendState = 25,

            /// <summary>
            /// Sets the depth-stencil state of the output-merger stage.
            /// </summary>
            OMSetDepthStencilState = 26,

            /// <summary>
            /// Set the target output buffers for the StreamOutput stage, which enables/disables the pipeline to stream-out data.
            /// </summary>
            SOSetTargets = 27,

            /// <summary>
            /// Draw geometry of an unknown size that was created by the geometry shader stage. See remarks.
            /// </summary>
            DrawAuto = 28,

            /// <summary>
            /// Set the rasterizer state for the rasterizer stage of the pipeline.
            /// </summary>
            RSSetState = 29,

            /// <summary>
            /// Bind an array of viewports to the rasterizer stage of the pipeline.
            /// </summary>
            RSSetViewports = 30,

            /// <summary>
            /// Bind an array of scissor rectangles to the rasterizer stage.
            /// </summary>
            RSSetScissorRects = 31,

            /// <summary>
            /// Copy a region from a source resource to a destination resource.
            /// </summary>
            CopySubresourceRegion = 32,

            /// <summary>
            /// Copy the entire contents of the source resource to the destination resource using the GPU. 
            /// </summary>
            CopyResource = 33,

            /// <summary>
            /// The CPU copies data from memory to a subresource created in non-mappable memory. See remarks.
            /// </summary>
            UpdateSubresource = 34,

            /// <summary>
            /// Set all the elements in a render target to one value.
            /// </summary>
            ClearRenderTargetView = 35,

            /// <summary>
            /// Clears the depth-stencil resource.
            /// </summary>
            ClearDepthStencilView = 36,

            /// <summary>
            /// Generates mipmaps for the given shader resource.
            /// </summary>
            GenerateMips = 37,

            /// <summary>
            /// Copy a multisampled resource into a non-multisampled resource.
            /// This API is most useful when re-using the resulting rendertarget of one render pass as an input to a second render pass.
            /// </summary>
            ResolveSubresource = 38,

            /// <summary>
            /// Get the constant buffers used by the vertex shader pipeline stage.
            /// </summary>
            VSGetConstantBuffers = 39,

            /// <summary>
            /// Get the pixel shader resources.
            /// </summary>
            PSGetShaderResources = 40,

            /// <summary>
            /// Get the pixel shader currently set on the device.
            /// </summary>
            PSGetShader = 41,

            /// <summary>
            /// Get an array of sampler states from the pixel shader pipeline stage.
            /// </summary>
            PSGetSamplers = 42,

            /// <summary>
            /// Get the vertex shader currently set on the device.
            /// </summary>
            VSGetShader = 43,

            /// <summary>
            /// Get the constant buffers used by the pixel shader pipeline stage.
            /// </summary>
            PSGetConstantBuffers = 44,

            /// <summary>
            /// Get a pointer to the input-layout object that is bound to the input-assembler stage.
            /// </summary>
            IAGetInputLayout = 45,

            /// <summary>
            /// Get the vertex buffers bound to the input-assembler stage.
            /// </summary>
            IAGetVertexBuffers = 46,

            /// <summary>
            /// Get a pointer to the index buffer that is bound to the input-assembler stage.
            /// </summary>
            IAGetIndexBuffer = 47,

            /// <summary>
            /// Get the constant buffers used by the geometry shader pipeline stage.
            /// </summary>
            GSGetConstantBuffers = 48,

            /// <summary>
            /// Get the geometry shader currently set on the device.
            /// </summary>
            GSGetShader = 49,

            /// <summary>
            /// Get information about the primitive type, and data order that describes input data for the input assembler stage.
            /// </summary>
            IAGetPrimitiveTopology = 50,

            /// <summary>
            /// Get the vertex shader resources.
            /// </summary>
            VSGetShaderResources = 51,

            /// <summary>
            /// Get an array of sampler states from the vertex shader pipeline stage.
            /// </summary>
            VSGetSamplers = 52,

            /// <summary>
            /// Get the rendering predicate state.
            /// </summary>
            GetPredication = 53,

            /// <summary>
            /// Get the geometry shader resources.
            /// </summary>
            GSGetShaderResources = 54,

            /// <summary>
            /// Get an array of sampler states from the geometry shader pipeline stage.
            /// </summary>
            GSGetSamplers = 55,

            /// <summary>
            /// Get pointers to the render targets and the depth-stencil buffer that are available to the output-merger stage.
            /// </summary>
            OMGetRenderTargets = 56,

            /// <summary>
            /// Get the blend state of the output-merger stage.
            /// </summary>
            OMGetBlendState = 57,

            /// <summary>
            /// Gets the depth-stencil state of the output-merger stage.
            /// </summary>
            OMGetDepthStencilState = 58,

            /// <summary>
            /// Get the target output buffers for the StreamOutput stage of the pipeline.
            /// </summary>
            SOGetTargets = 59,

            /// <summary>
            /// Get the rasterizer state from the rasterizer stage of the pipeline.
            /// </summary>
            RSGetState = 60,

            /// <summary>
            /// Get the array of viewports bound to the rasterizer stage
            /// </summary>
            RSGetViewports = 61,

            /// <summary>
            /// Get the array of scissor rectangles bound to the rasterizer stage.
            /// </summary>
            RSGetScissorRects = 62,

            /// <summary>
            /// Get the reason why the device was removed.
            /// </summary>
            GetDeviceRemovedReason = 63,

            /// <summary>
            /// Get the exception-mode flags.
            /// </summary>
            SetExceptionMode = 64,

            /// <summary>
            /// Get the exception-mode flags.
            /// </summary>
            GetExceptionMode = 65,

            /// <summary>
            /// Get data from a device that is associated with a guid.
            /// </summary>
            GetPrivateData = 66,

            /// <summary>
            /// Set data to a device and associate that data with a guid.
            /// </summary>
            SetPrivateData = 67,

            /// <summary>
            /// Associate an IUnknown-derived interface with this device and associate that interface with an application-defined guid.
            /// </summary>
            SetPrivateDataInterface = 68,

            /// <summary>
            /// Restore all default device settings; return the device to the state it was in when it was created.
            /// This will set all set all input/output resource slots, shaders, input layouts, predications, scissor rectangles,
            /// depth-stencil state, rasterizer state, blend state, sampler state, and viewports to NULL. The primitive topology will be set to UNDEFINED.
            /// </summary>
            ClearState = 69,

            /// <summary>
            /// Send queued-up commands in the command buffer to the GPU.
            /// </summary>
            Flush = 70,

            /// <summary>
            /// Create a buffer (vertex buffer, index buffer, or shader-constant buffer).
            /// </summary>
            CreateBuffer = 71,

            /// <summary>
            /// Create an array of 1D textures (see Texture1D).
            /// </summary>
            CreateTexture1D = 72,

            /// <summary>
            /// Create an array of 2D textures (see Texture2D).
            /// </summary>
            CreateTexture2D = 73,

            /// <summary>
            /// Create a single 3D texture (see Texture3D).
            /// </summary>
            CreateTexture3D = 74,

            /// <summary>
            /// Create a shader-resource view for accessing data in a resource.
            /// </summary>
            CreateShaderResourceView = 75,

            /// <summary>
            /// Create a render-target view for accessing resource data.
            /// </summary>
            CreateRenderTargetView = 76,

            /// <summary>
            /// Create a depth-stencil view for accessing resource data.
            /// </summary>
            CreateDepthStencilView = 77,

            /// <summary>
            /// Create an input-layout object to describe the input-buffer data for the input-assembler stage.
            /// </summary>
            CreateInputLayout = 78,

            /// <summary>
            /// Create a vertex-shader object from a compiled shader.
            /// </summary>
            CreateVertexShader = 79,

            /// <summary>
            /// Create a geometry shader.
            /// </summary>
            CreateGeometryShader = 80,

            /// <summary>
            /// Creates a geometry shader that can write to streaming output buffers.
            /// </summary>
            CreateGeometryShaderWithStreamOutput = 81,

            /// <summary>
            /// Create a pixel shader.
            /// </summary>
            CreatePixelShader = 82,

            /// <summary>
            /// Create a blend-state object that encapsules blend state for the output-merger stage.
            /// </summary>
            CreateBlendState = 83,

            /// <summary>
            /// Create a depth-stencil state object that encapsulates depth-stencil test information for the output-merger stage.
            /// </summary>
            CreateDepthStencilState = 84,

            /// <summary>
            /// Create a rasterizer state object that tells the rasterizer stage how to behave.
            /// </summary>
            CreateRasterizerState = 85,

            /// <summary>
            /// Create a sampler-state object that encapsulates sampling information for a texture.
            /// </summary>
            CreateSamplerState = 86,

            /// <summary>
            /// This interface encapsulates methods for querying information from the GPU.
            /// </summary>
            CreateQuery = 87,

            /// <summary>
            /// Creates a predicate.
            /// </summary>
            CreatePredicate = 88,

            /// <summary>
            /// Create a counter object for measuring GPU performance.
            /// </summary>
            CreateCounter = 89,

            /// <summary>
            /// Get the support of a given format on the installed video device.
            /// </summary>
            CheckFormatSupport = 90,

            /// <summary>
            /// Get the number of quality levels available during multisampling.
            /// </summary>
            CheckMultisampleQualityLevels = 91,

            /// <summary>
            /// Get a counter's information.
            /// </summary>
            CheckCounterInfo = 92,

            /// <summary>
            /// Get the type, name, units of measure, and a description of an existing counter.
            /// </summary>
            CheckCounter = 93,

            /// <summary>
            /// Get the flags used during the call to create the device with D3D10CreateDevice.
            /// </summary>
            GetCreationFlags = 94,

            /// <summary>
            /// Give a device access to a shared resource created on a different Direct3d device. 
            /// </summary>
            OpenSharedResource = 95,

            /// <summary>
            /// This method is not implemented.
            /// </summary>
            SetTextFilterSize = 96,

            /// <summary>
            /// This method is not implemented.
            /// </summary>
            GetTextFilterSize = 97,

            /// <summary>
            /// Create a shader-resource view for accessing data in a resource.
            /// </summary>
            CreateShaderResourceView1 = 98,

            /// <summary>
            /// Create a blend-state object that encapsules blend state for the output-merger stage.
            /// </summary>
            CreateBlendState1 = 99,

            /// <summary>
            /// Gets the feature level of the hardware device.
            /// </summary>
            GetFeatureLevel = 100,
        }
        //// End D3D101DeviceVirtualTableEnum

        /// <summary>
        /// The device interface represents a virtual adapter; it is used to create resources.
        /// </summary>
        internal enum D3D11DeviceVirtualTableEnum : Int16
        {
            /// <summary>
            /// Retrieves pointers to the supported interfaces on an object.
            /// </summary>
            QueryInterface = 0,

            /// <summary>
            /// Increments the reference count for an interface on an object.
            /// </summary>
            AddRef = 1,

            /// <summary>
            /// Decrements the reference count for an interface on an object.
            /// </summary>
            Release = 2,

            /// <summary>
            /// Creates a buffer (vertex buffer, index buffer, or shader-constant buffer).
            /// </summary>
            CreateBuffer = 3,

            /// <summary>
            /// Creates an array of 1D textures.
            /// </summary>
            CreateTexture1D = 4,

            /// <summary>
            /// Create an array of 2D textures.
            /// </summary>
            CreateTexture2D = 5,

            /// <summary>
            /// Create a single 3D texture.
            /// </summary>
            CreateTexture3D = 6,

            /// <summary>
            /// Create a shader-resource view for accessing data in a resource.
            /// </summary>
            CreateShaderResourceView = 7,

            /// <summary>
            /// Creates a view for accessing an unordered access resource.
            /// </summary>
            CreateUnorderedAccessView = 8,

            /// <summary>
            /// Creates a render-target view for accessing resource data.
            /// </summary>
            CreateRenderTargetView = 9,

            /// <summary>
            /// Create a depth-stencil view for accessing resource data.
            /// </summary>
            CreateDepthStencilView = 10,

            /// <summary>
            /// Create an input-layout object to describe the input-buffer data for the input-assembler stage.
            /// </summary>
            CreateInputLayout = 11,

            /// <summary>
            /// Create a vertex-shader object from a compiled shader.
            /// </summary>
            CreateVertexShader = 12,

            /// <summary>
            /// Create a geometry shader.
            /// </summary>
            CreateGeometryShader = 13,

            /// <summary>
            /// Creates a geometry shader that can write to streaming output buffers.
            /// </summary>
            CreateGeometryShaderWithStreamOutput = 14,

            /// <summary>
            /// Create a pixel shader.
            /// </summary>
            CreatePixelShader = 15,

            /// <summary>
            /// Create a hull shader.
            /// </summary>
            CreateHullShader = 16,

            /// <summary>
            /// Create a domain shader.
            /// </summary>
            CreateDomainShader = 17,

            /// <summary>
            /// Create a compute shader.
            /// </summary>
            CreateComputeShader = 18,

            /// <summary>
            /// Creates class linkage libraries to enable dynamic shader linkage.
            /// </summary>
            CreateClassLinkage = 19,

            /// <summary>
            /// Create a blend-state object that encapsules blend state for the output-merger stage.
            /// </summary>
            CreateBlendState = 20,

            /// <summary>
            /// Create a depth-stencil state object that encapsulates depth-stencil test information for the output-merger stage.
            /// </summary>
            CreateDepthStencilState = 21,

            /// <summary>
            /// Create a rasterizer state object that tells the rasterizer stage how to behave.
            /// </summary>
            CreateRasterizerState = 22,

            /// <summary>
            /// Create a sampler-state object that encapsulates sampling information for a texture.
            /// </summary>
            CreateSamplerState = 23,

            /// <summary>
            /// This interface encapsulates methods for querying information from the GPU.
            /// </summary>
            CreateQuery = 24,

            /// <summary>
            /// Creates a predicate.
            /// </summary>
            CreatePredicate = 25,

            /// <summary>
            /// Create a counter object for measuring GPU performance.
            /// </summary>
            CreateCounter = 26,

            /// <summary>
            /// Creates a deferred context, which can record command lists. 
            /// </summary>
            CreateDeferredContext = 27,

            /// <summary>
            /// Give a device access to a shared resource created on a different device.
            /// </summary>
            OpenSharedResource = 28,

            /// <summary>
            /// Get the support of a given format on the installed video device.
            /// </summary>
            CheckFormatSupport = 29,

            /// <summary>
            /// Get the number of quality levels available during multisampling.
            /// </summary>
            CheckMultisampleQualityLevels = 30,

            /// <summary>
            /// Get a counter's information.
            /// </summary>
            CheckCounterInfo = 31,

            /// <summary>
            /// Get the type, name, units of measure, and a description of an existing counter.
            /// </summary>
            CheckCounter = 32,

            /// <summary>
            /// Gets information about the features that are supported by the current graphics driver.
            /// </summary>
            CheckFeatureSupport = 33,

            /// <summary>
            /// Get application-defined data from a device.
            /// </summary>
            GetPrivateData = 34,

            /// <summary>
            /// Set data to a device and associate that data with a guid.
            /// </summary>
            SetPrivateData = 35,

            /// <summary>
            /// Associate an IUnknown-derived interface with this device child and associate that interface with an application-defined guid.
            /// </summary>
            SetPrivateDataInterface = 36,

            /// <summary>
            /// Gets the feature level of the hardware device.
            /// </summary>
            GetFeatureLevel = 37,

            /// <summary>
            /// Get the flags used during the call to create the device with D3D11CreateDevice.
            /// </summary>
            GetCreationFlags = 38,

            /// <summary>
            /// Get the reason why the device was removed.
            /// </summary>
            GetDeviceRemovedReason = 39,

            /// <summary>
            /// Gets an immediate context, which can play back command lists.
            /// </summary>
            GetImmediateContext = 40,

            /// <summary>
            /// Get the exception-mode flags.
            /// </summary>
            SetExceptionMode = 41,

            /// <summary>
            /// Get the exception-mode flags.
            /// </summary>
            GetExceptionMode = 42,
        }
        //// End D3D11DeviceVirtualTableEnum
    }
    //// End class
}
//// End namespace