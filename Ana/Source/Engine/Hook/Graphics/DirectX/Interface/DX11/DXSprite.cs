namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    using SharpDX;
    using SharpDX.D3DCompiler;
    using SharpDX.Direct3D11;
    using SharpDX.Mathematics.Interop;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A DirectX 11 Sprite
    /// </summary>
    internal class DXSprite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DXSprite" /> class
        /// </summary>
        /// <param name="device">The DirectX device</param>
        /// <param name="deviceContext">The contect of the DirectX device</param>
        public DXSprite(Device device, DeviceContext deviceContext)
        {
            this.Device = device;
            this.DeviceContext = deviceContext;

            this.SpriteList = new List<Sprite>(128);
            this.IndexBuffer = null;
        }

        private Device Device { get; set; }

        private DeviceContext DeviceContext { get; set; }

        private Boolean Initialized { get; set; }

        private BlendState TransparentBlendState { get; set; }

        private EffectTechnique SpriteTech { get; set; }

        private EffectShaderResourceVariable SpriteMap { get; set; }

        private ShaderResourceView BatchTexSRV { get; set; }

        private InputLayout InputLayout { get; set; }

        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }

        private SharpDX.Direct3D11.Buffer Buffer { get; set; }

        private Int32 TexWidth { get; set; }

        private Int32 TexHeight { get; set; }

        private List<Sprite> SpriteList { get; set; }

        private Single ScreenWidth { get; set; }

        private Single ScreenHeight { get; set; }

        private CompilationResult CompiledFX { get; set; }

        private Effect Effect { get; set; }

        private SafeHGlobal IndexBuffer { get; set; }

        public Boolean Initialize()
        {
            Debug.Assert(!this.Initialized, "Ensure not initialized");

            String spriteFX = @"Texture2D SpriteTex;
SamplerState samLinear {
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = WRAP;
    AddressV = WRAP;
};
struct VertexIn {
    float3 PosNdc : POSITION;
    float2 Tex    : TEXCOORD;
    float4 Color  : COLOR;
};
struct VertexOut {
    float4 PosNdc : SV_POSITION;
    float2 Tex    : TEXCOORD;
    float4 Color  : COLOR;
};
VertexOut VS(VertexIn vin) {
    VertexOut vout;
    vout.PosNdc = float4(vin.PosNdc, 1.0f);
    vout.Tex    = vin.Tex;
    vout.Color  = vin.Color;
    return vout;
};
float4 PS(VertexOut pin) : SV_Target {
    return pin.Color*SpriteTex.Sample(samLinear, pin.Tex);
};
technique11 SpriteTech {
    pass P0 {
        SetVertexShader( CompileShader( vs_5_0, VS() ) );
        SetHullShader( NULL );
        SetDomainShader( NULL );
        SetGeometryShader( NULL );
        SetPixelShader( CompileShader( ps_5_0, PS() ) );
    }
};";

            this.CompiledFX = ShaderBytecode.Compile(spriteFX, "SpriteTech", "fx_5_0");
            {
                if (this.CompiledFX.HasErrors)
                {
                    return false;
                }

                this.Effect = new Effect(this.Device, this.CompiledFX);
                {
                    this.SpriteTech = Effect.GetTechniqueByName("SpriteTech");
                    this.SpriteMap = Effect.GetVariableByName("SpriteTex").AsShaderResource();

                    using (EffectPass effectPas = this.SpriteTech.GetPassByIndex(0))
                    {
                        InputElement[] layoutDesc =
                        {
                            new InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0),
                            new InputElement("TEXCOORD", 0, SharpDX.DXGI.Format.R32G32_Float, 12, 0, InputClassification.PerVertexData, 0),
                            new InputElement("COLOR", 0, SharpDX.DXGI.Format.R32G32B32A32_Float, 20, 0, InputClassification.PerVertexData, 0)
                        };

                        this.InputLayout = new InputLayout(this.Device, effectPas.Description.Signature, layoutDesc);
                    }

                    // Create Vertex Buffer
                    BufferDescription vertexBufferDescription = new BufferDescription
                    {
                        SizeInBytes = 2048 * Marshal.SizeOf(typeof(SpriteVertex)),
                        Usage = ResourceUsage.Dynamic,
                        BindFlags = BindFlags.VertexBuffer,
                        CpuAccessFlags = CpuAccessFlags.Write,
                        OptionFlags = ResourceOptionFlags.None,
                        StructureByteStride = 0
                    };

                    this.VertexBuffer = new SharpDX.Direct3D11.Buffer(Device, vertexBufferDescription); // ToDispose()

                    // Create and initialise Index Buffer
                    Int16[] indicies = new Int16[3072];

                    for (UInt16 index = 0; index < 512; ++index)
                    {
                        indicies[index * 6] = (Int16)(index * 4);
                        indicies[(index * 6) + 1] = (Int16)((index * 4) + 1);
                        indicies[(index * 6) + 2] = (Int16)((index * 4) + 2);
                        indicies[(index * 6) + 3] = (Int16)(index * 4);
                        indicies[(index * 6) + 4] = (Int16)((index * 4) + 2);
                        indicies[(index * 6) + 5] = (Int16)((index * 4) + 3);
                    }

                    this.IndexBuffer = new SafeHGlobal(indicies.Length * Marshal.SizeOf(indicies[0]));
                    Marshal.Copy(indicies, 0, this.IndexBuffer.DangerousGetHandle(), indicies.Length);

                    BufferDescription bufferDescription = new BufferDescription
                    {
                        SizeInBytes = 3072 * Marshal.SizeOf(typeof(Int16)),
                        Usage = ResourceUsage.Immutable,
                        BindFlags = BindFlags.IndexBuffer,
                        CpuAccessFlags = CpuAccessFlags.None,
                        OptionFlags = ResourceOptionFlags.None,
                        StructureByteStride = 0
                    };

                    this.Buffer = new SharpDX.Direct3D11.Buffer(this.Device, this.IndexBuffer.DangerousGetHandle(), bufferDescription);

                    BlendStateDescription transparentDescription = new BlendStateDescription()
                    {
                        AlphaToCoverageEnable = false,
                        IndependentBlendEnable = false,
                    };

                    transparentDescription.RenderTarget[0].IsBlendEnabled = true;
                    transparentDescription.RenderTarget[0].SourceBlend = BlendOption.SourceAlpha;
                    transparentDescription.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
                    transparentDescription.RenderTarget[0].BlendOperation = BlendOperation.Add;
                    transparentDescription.RenderTarget[0].SourceAlphaBlend = BlendOption.One;
                    transparentDescription.RenderTarget[0].DestinationAlphaBlend = BlendOption.Zero;
                    transparentDescription.RenderTarget[0].AlphaBlendOperation = BlendOperation.Add;
                    transparentDescription.RenderTarget[0].RenderTargetWriteMask = ColorWriteMaskFlags.All;

                    this.TransparentBlendState = new BlendState(Device, transparentDescription); // ToDispose()
                }
            }

            this.Initialized = true;

            return true;
        }

        public void DrawImage(Int32 x, Int32 y, Single scale, Single angle, System.Drawing.Color? color, DXImage image)
        {
            Debug.Assert(this.Initialized, "Ensure initalized");

            RawColor4 blendFactor = new RawColor4(1.0f, 1.0f, 1.0f, 1.0f);
            RawColor4 backupBlendFactor;
            Int32 backupMask;

            using (BlendState backupBlendState = DeviceContext.OutputMerger.GetBlendState(out backupBlendFactor, out backupMask))
            {
                this.DeviceContext.OutputMerger.SetBlendState(this.TransparentBlendState, blendFactor);
                this.BeginBatch(image.GetSRV());
                this.Draw(new RawRectangle(x, y, (Int32)(scale * image.Width), (Int32)(scale * image.Height)), new RawRectangle(0, 0, image.Width, image.Height), color.HasValue ? ToColor4(color.Value) : new RawColor4(), 1.0f, angle);

                this.EndBatch();
                this.DeviceContext.OutputMerger.SetBlendState(backupBlendState, backupBlendFactor, backupMask);
            }
        }

        public void DrawString(Int32 x, Int32 y, String text, System.Drawing.Color color, DXFont font)
        {
            RawColor4 blendFactor = new RawColor4(1.0f, 1.0f, 1.0f, 1.0f);
            RawColor4 backupBlendFactor;
            Int32 backupMask;

            using (BlendState backupBlendState = DeviceContext.OutputMerger.GetBlendState(out backupBlendFactor, out backupMask))
            {
                this.DeviceContext.OutputMerger.SetBlendState(this.TransparentBlendState, blendFactor);

                this.BeginBatch(font.GetFontSheetSRV());

                Int32 length = text.Length;
                Int32 positionX = x;
                Int32 positionY = y;
                RawColor4 color4 = ToColor4(color);

                for (Int32 index = 0; index < length; ++index)
                {
                    Char character = text[index];

                    if (character == ' ')
                    {
                        positionX += font.GetSpaceWidth();
                    }
                    else if (character == '\n')
                    {
                        positionX = x;
                        positionY += font.GetCharHeight();
                    }
                    else
                    {
                        RawRectangle charRectangle = font.GetCharRect(character);

                        Int32 width = charRectangle.Right - charRectangle.Left;
                        Int32 height = charRectangle.Bottom - charRectangle.Top;

                        this.Draw(new RawRectangle(positionX, positionY, width, height), charRectangle, color4);

                        positionX += width + 1;
                    }
                }

                this.EndBatch();
                this.DeviceContext.OutputMerger.SetBlendState(backupBlendState, backupBlendFactor, backupMask);
            }
        }

        public void BeginBatch(ShaderResourceView texSRV)
        {
            Debug.Assert(this.Initialized, "Ensure initalized");

            this.BatchTexSRV = texSRV;

            Texture2D tex = this.BatchTexSRV.ResourceAs<Texture2D>();
            {
                Texture2DDescription texDesc = tex.Description;
                this.TexWidth = texDesc.Width;
                this.TexHeight = texDesc.Height;
            }

            this.SpriteList.Clear();
        }

        public void EndBatch()
        {
            Debug.Assert(this.Initialized, "Ensure initalized");

            RawViewportF[] viewportF = DeviceContext.Rasterizer.GetViewports<RawViewportF>();
            Int32 stride = Marshal.SizeOf(typeof(SpriteVertex));
            Int32 offset = 0;

            this.ScreenWidth = viewportF[0].Width;
            this.ScreenHeight = viewportF[0].Height;

            this.DeviceContext.InputAssembler.InputLayout = InputLayout;
            this.DeviceContext.InputAssembler.SetIndexBuffer(this.Buffer, SharpDX.DXGI.Format.R16_UInt, 0);
            this.DeviceContext.InputAssembler.SetVertexBuffers(0, new[] { this.VertexBuffer }, new[] { stride }, new[] { offset });
            this.DeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            this.SpriteMap.SetResource(this.BatchTexSRV);

            using (EffectPass effectPass = this.SpriteTech.GetPassByIndex(0))
            {
                effectPass.Apply(this.DeviceContext);
                Int32 spritesToDraw = this.SpriteList.Count;
                Int32 startIndex = 0;

                while (spritesToDraw > 0)
                {
                    if (spritesToDraw <= 512)
                    {
                        this.DrawBatch(startIndex, spritesToDraw);
                        spritesToDraw = 0;
                    }
                    else
                    {
                        this.DrawBatch(startIndex, 512);
                        startIndex += 512;
                        spritesToDraw -= 512;
                    }
                }
            }

            this.BatchTexSRV = null;
        }

        public void Draw(RawRectangle destinationRectangle, RawRectangle sourceRectangle, RawColor4 color, Single scale = 1.0f, Single angle = 0f, Single z = 0f)
        {
            Sprite sprite = new Sprite(sourceRectangle, destinationRectangle, color)
            {
                Scale = scale,
                Angle = angle,
                Z = z
            };

            this.SpriteList.Add(sprite);
        }

        internal static RawColor4 ToColor4(System.Drawing.Color color)
        {
            RawVector4 vec = new RawVector4(color.R > 0 ? (Single)(color.R / 255.0f) : 0.0f, color.G > 0 ? (Single)(color.G / 255.0f) : 0.0f, color.B > 0 ? (Single)(color.B / 255.0f) : 0.0f, color.A > 0 ? (Single)(color.A / 255.0f) : 0.0f);
            return new RawColor4(vec.W, vec.X, vec.Y, vec.Z);
        }

        private void DrawBatch(Int32 startSpriteIndex, Int32 spriteCount)
        {
            DataBox mappedData = DeviceContext.MapSubresource(this.VertexBuffer, 0, MapMode.WriteDiscard, MapFlags.None);

            // Update the vertices
            unsafe
            {
                SpriteVertex* spriteVertex = (SpriteVertex*)mappedData.DataPointer.ToPointer();

                for (Int32 index = 0; index < spriteCount; index++)
                {
                    Sprite sprite = this.SpriteList[startSpriteIndex + index];
                    SpriteVertex[] quad = new SpriteVertex[4];

                    this.BuildSpriteQuad(sprite, ref quad);

                    spriteVertex[index * 4] = quad[0];
                    spriteVertex[(index * 4) + 1] = quad[1];
                    spriteVertex[(index * 4) + 2] = quad[2];
                    spriteVertex[(index * 4) + 3] = quad[3];
                }
            }

            DeviceContext.UnmapSubresource(this.VertexBuffer, 0);
            DeviceContext.DrawIndexed(spriteCount * 6, 0, 0);
        }

        private RawVector3 PointToNdc(Int32 x, Int32 y, Single z)
        {
            RawVector3 p;

            p.X = ((2.0f * (Single)x) / this.ScreenWidth) - 1.0f;
            p.Y = (1.0f - (2.0f * (Single)y)) / this.ScreenHeight;
            p.Z = z;

            return p;
        }

        private void BuildSpriteQuad(Sprite sprite, ref SpriteVertex[] vertex)
        {
            if (vertex.Length < 4)
            {
                throw new ArgumentException("Must have 4 sprite vertices", "v");
            }

            RawRectangle destinationRectangle = sprite.DestRectangle;
            RawRectangle sourceRectangle = sprite.SourceRectangle;

            vertex[0].Position = this.PointToNdc(destinationRectangle.Left, destinationRectangle.Bottom, sprite.Z);
            vertex[1].Position = this.PointToNdc(destinationRectangle.Left, destinationRectangle.Top, sprite.Z);
            vertex[2].Position = this.PointToNdc(destinationRectangle.Right, destinationRectangle.Top, sprite.Z);
            vertex[3].Position = this.PointToNdc(destinationRectangle.Right, destinationRectangle.Bottom, sprite.Z);

            vertex[0].Tex = new RawVector2((Single)sourceRectangle.Left / this.TexWidth, (Single)sourceRectangle.Bottom / this.TexHeight);
            vertex[1].Tex = new RawVector2((Single)sourceRectangle.Left / this.TexWidth, (Single)sourceRectangle.Top / this.TexHeight);
            vertex[2].Tex = new RawVector2((Single)sourceRectangle.Right / this.TexWidth, (Single)sourceRectangle.Top / this.TexHeight);
            vertex[3].Tex = new RawVector2((Single)sourceRectangle.Right / this.TexWidth, (Single)sourceRectangle.Bottom / this.TexHeight);

            vertex[0].Color = sprite.Color;
            vertex[1].Color = sprite.Color;
            vertex[2].Color = sprite.Color;
            vertex[3].Color = sprite.Color;

            Single tx = 0.5f * (vertex[0].Position.X + vertex[3].Position.X);
            Single ty = 0.5f * (vertex[0].Position.Y + vertex[1].Position.Y);

            RawVector2 origin = new RawVector2(tx, ty);
            RawVector2 translation = new RawVector2(0.0f, 0.0f);
            //// RawMatrix Transformation = RawMatrix.AffineTransformation2D(sprite.Scale, origin, Sprite.Angle, translation);

            for (Int32 index = 0; index < 4; ++index)
            {
                RawVector3 position = vertex[index].Position;
                //// RawVector3 position = RawVector3.TransformCoordinate(position, Transformation);
                vertex[index].Position = position;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SpriteVertex
        {
            public RawVector3 Position { get; set; }

            public RawVector2 Tex { get; set; }

            public RawColor4 Color { get; set; }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Sprite
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Sprite" /> struct
            /// </summary>
            /// <param name="sourceRect"></param>
            /// <param name="destRect"></param>
            /// <param name="color"></param>
            public Sprite(RawRectangle sourceRect, RawRectangle destRect, RawColor4 color)
            {
                this.SourceRectangle = sourceRect;
                this.DestRectangle = destRect;
                this.Color = color;

                this.Z = 0.0f;
                this.Angle = 0.0f;
                this.Scale = 1.0f;
            }

            public RawRectangle SourceRectangle { get; set; }

            public RawRectangle DestRectangle { get; set; }

            public RawColor4 Color { get; set; }

            public Single Z { get; set; }

            public Single Angle { get; set; }

            public Single Scale { get; set; }
        }
    }
    //// End class
}
//// End namespace