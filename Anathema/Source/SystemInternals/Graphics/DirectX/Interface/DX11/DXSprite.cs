using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface.DX11
{
    public class DXSprite : Component
    {
        private Device Device;
        private DeviceContext DeviceContext;

        private Boolean Initialized;
        private BlendState TransparentBS;
        private EffectTechnique SpriteTech;
        private EffectShaderResourceVariable SpriteMap;
        private ShaderResourceView BatchTexSRV;
        private InputLayout InputLayout;
        private SharpDX.Direct3D11.Buffer VBuffer;
        private SharpDX.Direct3D11.Buffer IBuffer;
        private Int32 TexWidth;
        private Int32 TexHeight;
        private List<Sprite> SpriteList;
        private Single ScreenWidth;
        private Single ScreenHeight;
        private CompilationResult CompiledFX;
        private Effect Effect;
        private SafeHGlobal IndexBuffer;

        public DXSprite(Device Device, DeviceContext DeviceContext)
        {
            this.Device = Device;
            this.DeviceContext = DeviceContext;

            SpriteList = new List<Sprite>(128);
            IndexBuffer = null;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SpriteVertex
        {
            public Vector3 Position;
            public Vector2 Tex;
            public Color4 Color;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Sprite
        {
            public Rectangle SourceRectangle;
            public Rectangle DestRectangle;
            public Color4 Color;

            public Single Z;
            public Single Angle;
            public Single Scale;

            public Sprite(Rectangle SourceRect, Rectangle DestRect, Color4 Color)
            {
                this.SourceRectangle = SourceRect;
                this.DestRectangle = DestRect;
                this.Color = Color;

                Z = 0.0f;
                Angle = 0.0f;
                Scale = 1.0f;
            }
        }

        public Boolean Initialize()
        {
            Debug.Assert(!Initialized);

            #region Shaders
            String SpriteFX = @"Texture2D SpriteTex;
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
            #endregion

            CompiledFX = ToDispose(ShaderBytecode.Compile(SpriteFX, "SpriteTech", "fx_5_0"));
            {

                if (CompiledFX.HasErrors)
                    return false;

                Effect = ToDispose(new Effect(Device, CompiledFX));
                {
                    SpriteTech = ToDispose(Effect.GetTechniqueByName("SpriteTech"));
                    SpriteMap = ToDispose(Effect.GetVariableByName("SpriteTex").AsShaderResource());

                    using (EffectPass EffectPas = SpriteTech.GetPassByIndex(0))
                    {
                        InputElement[] layoutDesc =
                        {
                            new InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0),
                            new InputElement("TEXCOORD", 0, SharpDX.DXGI.Format.R32G32_Float, 12, 0, InputClassification.PerVertexData, 0),
                            new InputElement("COLOR", 0, SharpDX.DXGI.Format.R32G32B32A32_Float, 20, 0, InputClassification.PerVertexData, 0)
                        };

                        InputLayout = ToDispose(new InputLayout(Device, EffectPas.Description.Signature, layoutDesc));
                    }
                    // Create Vertex Buffer
                    BufferDescription VBufferDescription = new BufferDescription
                    {
                        SizeInBytes = 2048 * Marshal.SizeOf(typeof(SpriteVertex)),
                        Usage = ResourceUsage.Dynamic,
                        BindFlags = BindFlags.VertexBuffer,
                        CpuAccessFlags = CpuAccessFlags.Write,
                        OptionFlags = ResourceOptionFlags.None,
                        StructureByteStride = 0
                    };

                    VBuffer = ToDispose(new SharpDX.Direct3D11.Buffer(Device, VBufferDescription));

                    // Create and initialise Index Buffer
                    Int16[] Indicies = new Int16[3072];

                    for (UInt16 Index = 0; Index < 512; ++Index)
                    {
                        Indicies[Index * 6] = (Int16)(Index * 4);
                        Indicies[Index * 6 + 1] = (Int16)(Index * 4 + 1);
                        Indicies[Index * 6 + 2] = (Int16)(Index * 4 + 2);
                        Indicies[Index * 6 + 3] = (Int16)(Index * 4);
                        Indicies[Index * 6 + 4] = (Int16)(Index * 4 + 2);
                        Indicies[Index * 6 + 5] = (Int16)(Index * 4 + 3);
                    }

                    IndexBuffer = ToDispose(new SafeHGlobal(Indicies.Length * Marshal.SizeOf(Indicies[0])));
                    Marshal.Copy(Indicies, 0, IndexBuffer.DangerousGetHandle(), Indicies.Length);

                    BufferDescription IBufferDescription = new BufferDescription
                    {
                        SizeInBytes = 3072 * Marshal.SizeOf(typeof(Int16)),
                        Usage = ResourceUsage.Immutable,
                        BindFlags = BindFlags.IndexBuffer,
                        CpuAccessFlags = CpuAccessFlags.None,
                        OptionFlags = ResourceOptionFlags.None,
                        StructureByteStride = 0
                    };

                    IBuffer = ToDispose(new SharpDX.Direct3D11.Buffer(Device, IndexBuffer.DangerousGetHandle(), IBufferDescription));

                    BlendStateDescription TransparentDescription = new BlendStateDescription()
                    {
                        AlphaToCoverageEnable = false,
                        IndependentBlendEnable = false,
                    };

                    TransparentDescription.RenderTarget[0].IsBlendEnabled = true;
                    TransparentDescription.RenderTarget[0].SourceBlend = BlendOption.SourceAlpha;
                    TransparentDescription.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
                    TransparentDescription.RenderTarget[0].BlendOperation = BlendOperation.Add;
                    TransparentDescription.RenderTarget[0].SourceAlphaBlend = BlendOption.One;
                    TransparentDescription.RenderTarget[0].DestinationAlphaBlend = BlendOption.Zero;
                    TransparentDescription.RenderTarget[0].AlphaBlendOperation = BlendOperation.Add;
                    TransparentDescription.RenderTarget[0].RenderTargetWriteMask = ColorWriteMaskFlags.All;

                    TransparentBS = ToDispose(new BlendState(Device, TransparentDescription));
                }
            }

            Initialized = true;

            return true;
        }

        internal static Color4 ToColor4(System.Drawing.Color color)
        {
            Vector4 Vec = new Vector4(color.R > 0 ? (Single)(color.R / 255.0f) : 0.0f, color.G > 0 ? (Single)(color.G / 255.0f) : 0.0f, color.B > 0 ? (Single)(color.B / 255.0f) : 0.0f, color.A > 0 ? (Single)(color.A / 255.0f) : 0.0f);
            return new Color4(Vec);
        }

        public void DrawImage(Int32 X, Int32 Y, Single Scale, Single Angle, System.Drawing.Color? color, DXImage Image)
        {
            Debug.Assert(Initialized);

            Color4 BlendFactor = new Color4(1.0f);
            Color4 BackupBlendFactor;
            Int32 BackupMask;

            using (BlendState backupBlendState = DeviceContext.OutputMerger.GetBlendState(out BackupBlendFactor, out BackupMask))
            {
                DeviceContext.OutputMerger.SetBlendState(TransparentBS, BlendFactor);
                BeginBatch(Image.GetSRV());
                Draw(new Rectangle(X, Y, (Int32)(Scale * Image.Width), (Int32)(Scale * Image.Height)), new Rectangle(0, 0, Image.Width, Image.Height), color.HasValue ? ToColor4(color.Value) : Color4.White, 1.0f, Angle);

                EndBatch();
                DeviceContext.OutputMerger.SetBlendState(backupBlendState, BackupBlendFactor, BackupMask);
            }
        }

        public void DrawString(Int32 X, Int32 Y, String Text, System.Drawing.Color Color, DXFont F)
        {
            Color4 BlendFactor = new Color4(1.0f);
            Color4 BackupBlendFactor;
            Int32 BackupMask;

            using (BlendState BackupBlendState = DeviceContext.OutputMerger.GetBlendState(out BackupBlendFactor, out BackupMask))
            {
                DeviceContext.OutputMerger.SetBlendState(TransparentBS, BlendFactor);

                BeginBatch(F.GetFontSheetSRV());

                Int32 Length = Text.Length;

                Int32 PositionX = X;
                Int32 PositionY = Y;

                Color4 Color4 = ToColor4(Color);

                for (Int32 Index = 0; Index < Length; ++Index)
                {
                    Char Character = Text[Index];

                    if (Character == ' ')
                        PositionX += F.GetSpaceWidth();

                    else if (Character == '\n')
                    {
                        PositionX = X;
                        PositionY += F.GetCharHeight();
                    }
                    else
                    {
                        Rectangle CharRectangle = F.GetCharRect(Character);

                        Int32 Width = CharRectangle.Right - CharRectangle.Left;
                        Int32 Height = CharRectangle.Bottom - CharRectangle.Top;

                        Draw(new Rectangle(PositionX, PositionY, Width, Height), CharRectangle, Color4);

                        PositionX += Width + 1;
                    }
                }

                EndBatch();
                DeviceContext.OutputMerger.SetBlendState(BackupBlendState, BackupBlendFactor, BackupMask);
            }
        }

        public void BeginBatch(ShaderResourceView TexSRV)
        {
            Debug.Assert(Initialized);

            BatchTexSRV = TexSRV;

            Texture2D Tex = BatchTexSRV.ResourceAs<Texture2D>();
            {
                Texture2DDescription TexDesc = Tex.Description;
                TexWidth = TexDesc.Width;
                TexHeight = TexDesc.Height;
            }

            SpriteList.Clear();
        }

        public void EndBatch()
        {
            Debug.Assert(Initialized);

            ViewportF[] ViewportF = DeviceContext.Rasterizer.GetViewports();
            Int32 Stride = Marshal.SizeOf(typeof(SpriteVertex));
            Int32 Offset = 0;

            ScreenWidth = ViewportF[0].Width;
            ScreenHeight = ViewportF[0].Height;


            DeviceContext.InputAssembler.InputLayout = InputLayout;
            DeviceContext.InputAssembler.SetIndexBuffer(IBuffer, SharpDX.DXGI.Format.R16_UInt, 0);
            DeviceContext.InputAssembler.SetVertexBuffers(0, new[] { VBuffer }, new[] { Stride }, new[] { Offset });
            DeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            SpriteMap.SetResource(BatchTexSRV);

            using (EffectPass EffectPass = SpriteTech.GetPassByIndex(0))
            {
                EffectPass.Apply(DeviceContext);
                Int32 SpritesToDraw = SpriteList.Count;
                Int32 StartIndex = 0;

                while (SpritesToDraw > 0)
                {
                    if (SpritesToDraw <= 512)
                    {
                        DrawBatch(StartIndex, SpritesToDraw);
                        SpritesToDraw = 0;
                    }
                    else
                    {
                        DrawBatch(StartIndex, 512);
                        StartIndex += 512;
                        SpritesToDraw -= 512;
                    }
                }
            }

            BatchTexSRV = null;
        }

        public void Draw(Rectangle DestinationRectangle, Rectangle SourceRectangle, Color4 Color, Single Scale = 1.0f, Single Angle = 0f, Single Z = 0f)
        {
            Sprite Sprite = new Sprite(SourceRectangle, DestinationRectangle, Color)
            {
                Scale = Scale,
                Angle = Angle,
                Z = Z
            };

            SpriteList.Add(Sprite);
        }

        void DrawBatch(Int32 StartSpriteIndex, Int32 SpriteCount)
        {
            DataBox MappedData = DeviceContext.MapSubresource(VBuffer, 0, MapMode.WriteDiscard, MapFlags.None);

            // Update the vertices
            unsafe
            {
                SpriteVertex* SpriteVertex = (SpriteVertex*)MappedData.DataPointer.ToPointer();

                for (Int32 Index = 0; Index < SpriteCount; ++Index)
                {
                    Sprite Sprite = SpriteList[StartSpriteIndex + Index];
                    SpriteVertex[] Quad = new SpriteVertex[4];

                    BuildSpriteQuad(Sprite, ref Quad);

                    SpriteVertex[Index * 4] = Quad[0];
                    SpriteVertex[Index * 4 + 1] = Quad[1];
                    SpriteVertex[Index * 4 + 2] = Quad[2];
                    SpriteVertex[Index * 4 + 3] = Quad[3];
                }
            }

            DeviceContext.UnmapSubresource(VBuffer, 0);
            DeviceContext.DrawIndexed(SpriteCount * 6, 0, 0);
        }

        Vector3 PointToNdc(Int32 X, Int32 Y, Single Z)
        {
            Vector3 P;

            P.X = 2.0f * (Single)X / ScreenWidth - 1.0f;
            P.Y = 1.0f - 2.0f * (Single)Y / ScreenHeight;
            P.Z = Z;

            return P;
        }

        void BuildSpriteQuad(Sprite Sprite, ref SpriteVertex[] Vertex)
        {
            if (Vertex.Length < 4)
                throw new ArgumentException("Must have 4 sprite vertices", "v");

            Rectangle DestinationRectangle = Sprite.DestRectangle;
            Rectangle SourceRectangle = Sprite.SourceRectangle;

            Vertex[0].Position = PointToNdc(DestinationRectangle.Left, DestinationRectangle.Bottom, Sprite.Z);
            Vertex[1].Position = PointToNdc(DestinationRectangle.Left, DestinationRectangle.Top, Sprite.Z);
            Vertex[2].Position = PointToNdc(DestinationRectangle.Right, DestinationRectangle.Top, Sprite.Z);
            Vertex[3].Position = PointToNdc(DestinationRectangle.Right, DestinationRectangle.Bottom, Sprite.Z);

            Vertex[0].Tex = new Vector2((Single)SourceRectangle.Left / TexWidth, (Single)SourceRectangle.Bottom / TexHeight);
            Vertex[1].Tex = new Vector2((Single)SourceRectangle.Left / TexWidth, (Single)SourceRectangle.Top / TexHeight);
            Vertex[2].Tex = new Vector2((Single)SourceRectangle.Right / TexWidth, (Single)SourceRectangle.Top / TexHeight);
            Vertex[3].Tex = new Vector2((Single)SourceRectangle.Right / TexWidth, (Single)SourceRectangle.Bottom / TexHeight);

            Vertex[0].Color = Sprite.Color;
            Vertex[1].Color = Sprite.Color;
            Vertex[2].Color = Sprite.Color;
            Vertex[3].Color = Sprite.Color;

            Single TX = 0.5f * (Vertex[0].Position.X + Vertex[3].Position.X);
            Single TY = 0.5f * (Vertex[0].Position.Y + Vertex[1].Position.Y);

            Vector2 Origin = new Vector2(TX, TY);
            Vector2 Translation = new Vector2(0.0f, 0.0f);
            Matrix Transformation = Matrix.AffineTransformation2D(Sprite.Scale, Origin, Sprite.Angle, Translation);

            for (Int32 Index = 0; Index < 4; ++Index)
            {
                Vector3 Position = Vertex[Index].Position;
                Position = Vector3.TransformCoordinate(Position, Transformation);
                Vertex[Index].Position = Position;
            }
        }

    } // End class

} // End namespace