namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    using SharpDX.D3DCompiler;
    using SharpDX.Direct3D11;
    using SharpDX.Mathematics.Interop;
    using System;
    using Buffer = SharpDX.Direct3D11.Buffer;

    internal class ScreenAlignedQuadRenderer : RendererBase
    {
        private String shaderCodeVertexIn = @"Texture2D<float4> Texture0 : register(t0);
SamplerState Sampler : register(s0);

struct VertexIn
{
    float4 Position : SV_Position;// Position - xyzw
};

struct PixelIn
{
    float4 Position : SV_Position;
    float2 UV : TEXCOORD0;
};

// Screen-Aligned Quad: vertex shader main function
PixelIn VSMain(VertexIn vertex)
{
    PixelIn result = (PixelIn)0;
    
    // The input quad is expected in device coordinates 
    // (i.e. 0,0 is center of screen, -1,1 top left, 1,-1 bottom right)
    // Therefore no transformation!
    result.Position = vertex.Position;
    result.Position.w = 1.0f;

    // The UV coordinates are top-left 0,0 bottom-right 1,1
    result.UV.x = result.Position.x * 0.5 + 0.5;
    result.UV.y = result.Position.y * -0.5 + 0.5;

    return result;
}

float4 PSMain(PixelIn input) : SV_Target
{
    return Texture0.Sample(Sampler, input.UV);
}
";

        private String shaderCode = @"Texture2D<float4> Texture0 : register(t0);
SamplerState Sampler : register(s0);

struct PixelIn
{
    float4 Position : SV_POSITION;
    float2 UV : TEXCOORD0;
};

// Vertex shader outputs a full screen quad with UV coords without vertex buffer
PixelIn VSMain(uint vertexId: SV_VertexID)
{
    PixelIn result = (PixelIn)0;
    
    // The input quad is expected in device coordinates 
    // (i.e. 0,0 is center of screen, -1,1 top left, 1,-1 bottom right)
    // Therefore no transformation!

    // The UV coordinates are top-left 0,0, bottom-right 1,1
    result.UV = float2((vertexId << 1) & 2, vertexId & 2 );
    result.Position = float4( result.UV * float2( 2.0f, -2.0f ) + float2( -1.0f, 1.0f), 0.0f, 1.0f );

    return result;
}

float4 PSMain(PixelIn input) : SV_Target
{
    return Texture0.Sample(Sampler, input.UV);
}
";

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenAlignedQuadRenderer" /> class
        /// </summary>
        public ScreenAlignedQuadRenderer()
        {
        }

        public Boolean UseLinearSampling { get; set; }

        public ShaderResourceView ShaderResource { get; set; }

        public RenderTargetView RenderTargetView { get; set; }

        public Texture2D RenderTarget { get; set; }

        /// <summary>
        /// Gets or sets the vertex shader
        /// </summary>
        private VertexShader VertexShader { get; set; }

        /// <summary>
        /// Gets or sets the vertex buffer binding
        /// </summary>
        private VertexBufferBinding VertexBinding { get; set; }

        /// <summary>
        /// Gets or sets the vertex layout for the IA
        /// </summary>
        private InputLayout VertexLayout { get; set; }

        /// <summary>
        /// Gets or sets the vertex buffer
        /// </summary>
        private Buffer VertexBuffer { get; set; }

        /// <summary>
        /// Gets or sets the pixel shader
        /// </summary>
        private PixelShader PixelShader { get; set; }

        private SamplerState PointSamplerState { get; set; }

        private SamplerState LinearSampleState { get; set; }

        /// <summary>
        /// Create any device dependent resources here.
        /// This method will be called when the device is first
        /// initialized or recreated after being removed or reset.
        /// </summary>
        protected override void CreateDeviceDependentResources()
        {
            // Ensure that if already set the device resources
            // are correctly disposed of before recreating
            //// RemoveAndDispose(ref VertexShader);
            //// RemoveAndDispose(ref PixelShader);
            //// RemoveAndDispose(ref PointSamplerState);
            //// RemoveAndDispose(ref indexBuffer);

            // Retrieve our SharpDX.Direct3D11.Device1 instance
            // Get a reference to the Device1 instance and immediate context
            Device device = DeviceManager.Direct3DDevice;
            DeviceContext context = DeviceManager.Direct3DContext;
            ShaderFlags shaderFlags = ShaderFlags.None;

#if DEBUG
            shaderFlags = ShaderFlags.Debug | ShaderFlags.SkipOptimization;
#endif
            // Use our HLSL file include handler to resolve #include directives in the HLSL source
            // var IncludeHandler = new HLSLFileIncludeHandler(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Shaders"));

            // Compile and create the vertex shader
            using (CompilationResult vertexShaderBytecode = ShaderBytecode.Compile(this.shaderCode, "VSMain", "vs_4_0", shaderFlags, EffectFlags.None, null, null))
            {
                this.VertexShader = new VertexShader(device, vertexShaderBytecode);

                // Layout from VertexShader input signature
                //// vertexLayout = ToDispose(new InputLayout(device,
                ////     ShaderSignature.GetInputSignature(vertexShaderBytecode),
                //// ShaderSignature.GetInputSignature(vertexShaderBytecode),
                //// new[]
                //// {
                ////     // "SV_Position" = vertex coordinate
                ////     new InputElement("SV_Position", 0, Format.R32G32B32_Float, 0, 0),
                //// }));

                // Create vertex buffer
                //// vertexBuffer = ToDispose(Buffer.Create(device, BindFlags.VertexBuffer, new Vector3[] {
                ////     /*  Position: float x 3 */
                ////     new Vector3(-1.0f, -1.0f, -1.0f),
                ////     new Vector3(-1.0f, 1.0f, -1.0f),
                ////     new Vector3(1.0f, -1.0f, -1.0f),
                ////     new Vector3(1.0f, 1.0f, -1.0f),
                //// }));
                //// vertexBinding = new VertexBufferBinding(vertexBuffer, Utilities.SizeOf<Vector3>(), 0);
                //// Triangle strip:
                //// v1   v3
                //// |\   |
                //// | \ B|
                //// | A\ |
                //// |   \|
                //// v0   v2
            }

            // Compile and create the pixel shader
            using (CompilationResult bytecode = ShaderBytecode.Compile(this.shaderCode, "PSMain", "ps_5_0", shaderFlags, EffectFlags.None, null, null))
            {
                this.PixelShader = new PixelShader(device, bytecode);
            }

            this.LinearSampleState = new SamplerState(
                   device,
                   new SamplerStateDescription
                   {
                       Filter = Filter.MinMagMipLinear,
                       AddressU = TextureAddressMode.Wrap,
                       AddressV = TextureAddressMode.Wrap,
                       AddressW = TextureAddressMode.Wrap,
                       ComparisonFunction = Comparison.Never,
                       MinimumLod = 0,
                       MaximumLod = Single.MaxValue
                   });

            this.PointSamplerState = new SamplerState(
                device,
                new SamplerStateDescription
                {
                    Filter = Filter.MinMagMipPoint,
                    AddressU = TextureAddressMode.Wrap,
                    AddressV = TextureAddressMode.Wrap,
                    AddressW = TextureAddressMode.Wrap,
                    ComparisonFunction = Comparison.Never,
                    MinimumLod = 0,
                    MaximumLod = Single.MaxValue
                });

            context.Rasterizer.State = new RasterizerState(
                device,
                new RasterizerStateDescription()
                {
                    CullMode = CullMode.None,
                    FillMode = FillMode.Solid,
                });

            // Configure the depth buffer to discard pixels that are
            // further than the current pixel.
            //// context.OutputMerger.SetDepthStencilState(ToDispose(new DepthStencilState(device,
            ////     new DepthStencilStateDescription()
            ////     {
            ////         IsDepthEnabled = false, // enable depth?
            ////         DepthComparison = Comparison.Less,
            ////         DepthWriteMask = SharpDX.Direct3D11.DepthWriteMask.All,
            ////         IsStencilEnabled = false,// enable stencil?
            ////         StencilReadMask = 0xff, // 0xff (no mask)
            ////         StencilWriteMask = 0xff,// 0xff (no mask)
            ////         // Configure FrontFace depth/stencil operations
            ////         FrontFace = new DepthStencilOperationDescription()
            ////         {
            ////             Comparison = Comparison.Always,
            ////             PassOperation = StencilOperation.Keep,
            ////             FailOperation = StencilOperation.Keep,
            ////             DepthFailOperation = StencilOperation.Increment
            ////         },
            ////         // Configure BackFace depth/stencil operations
            ////         BackFace = new DepthStencilOperationDescription()
            ////         {
            ////             Comparison = Comparison.Always,
            ////             PassOperation = StencilOperation.Keep,
            ////             FailOperation = StencilOperation.Keep,
            ////             DepthFailOperation = StencilOperation.Decrement
            ////         },
            ////     })));
        }

        protected override void DoRender()
        {
            DeviceContext context = this.DeviceManager.Direct3DContext;

            //// Context.InputAssembler.InputLayout = vertexLayout;
            //// Context.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleStrip;
            //// Context.InputAssembler.SetVertexBuffers(0, vertexBinding);

            //// Retrieve the existing shader and IA settings
            //// using (var OldVertexLayout = Context.InputAssembler.InputLayout)
            //// using (var OldSampler = Context.PixelShader.GetSamplers(0, 1).FirstOrDefault())
            //// using (var OldPixelShader = Context.PixelShader.Get())
            //// using (var OldVertexShader = Context.VertexShader.Get())
            //// using (var OldRenderTarget = Context.OutputMerger.GetRenderTargets(1).FirstOrDefault())
            {
                context.ClearRenderTargetView(this.RenderTargetView, new RawColor4());

                // Set sampler
                RawViewportF[] viewportF = { new RawViewportF() }; // (0, 0, RenderTarget.Description.Width, RenderTarget.Description.Height, 0, 1)
                context.Rasterizer.SetViewports(viewportF);
                context.PixelShader.SetSampler(0, this.UseLinearSampling ? this.LinearSampleState : this.PointSamplerState);

                // Set shader resource
                //// Boolean isMultisampledSRV = false;
                if (this.ShaderResource != null && !this.ShaderResource.IsDisposed)
                {
                    context.PixelShader.SetShaderResource(0, this.ShaderResource);

                    //// if (this.ShaderResource.Description.Dimension == SharpDX.Direct3D.ShaderResourceViewDimension.Texture2DMultisampled)
                    //// {
                    ////     isMultisampledSRV = true;
                    //// }
                }

                // Set pixel shader
                //// if (isMultisampledSRV)
                ////     context.PixelShader.Set(pixelShaderMS);
                //// else
                context.PixelShader.Set(this.PixelShader);

                // Set vertex shader
                context.VertexShader.Set(this.VertexShader);

                // Update vertex layout to use
                context.InputAssembler.InputLayout = null;

                // Tell the IA we are using a triangle strip
                context.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleStrip;

                // No vertices to pass (note: null as we will use SV_VertexId)
                //// context.InputAssembler.SetVertexBuffers(0, vertexBuffer);

                // Set the render target
                context.OutputMerger.SetTargets(RenderTargetView);

                // Draw the 4 vertices that make up the triangle strip
                context.Draw(4, 0);

                // Remove the render target from the pipeline so that we can read from it if necessary
                context.OutputMerger.SetTargets((RenderTargetView)null);

                // Restore previous shader and IA settings
                //// context.PixelShader.SetSampler(0, oldSampler);
                //// context.PixelShader.Set(oldPixelShader);
                //// context.VertexShader.Set(oldVertexShader);
                //// context.InputAssembler.InputLayout = oldVertexLayout;
                //// context.OutputMerger.SetTargets(oldRenderTarget);
            }
        }
    }
    //// End class
}
//// End namespace