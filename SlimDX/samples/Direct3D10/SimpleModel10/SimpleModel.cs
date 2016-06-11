using System;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using SlimDX.SampleFramework;
using SlimDX.D3DCompiler;
using Buffer = SlimDX.Direct3D10.Buffer;
using Device = SlimDX.Direct3D10.Device;
using PrimitiveTopology = SlimDX.Direct3D10.PrimitiveTopology;

namespace SimpleModel10
{
    class SimpleModel : IDisposable
    {
        public SimpleModel(Device device, string effectName, string meshName, string textureName)
        {
            this.device = device;
            effect = Effect.FromFile(device, effectName, "fx_4_0"); ;

            texture = Texture2D.FromFile(device, textureName);

            LoadMesh(meshName);

            elements = new[] {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0), 
                new InputElement("NORMAL", 0, Format.R32G32B32_Float, 0, 1), 
                new InputElement("TEXCOORD", 0, Format.R32G32_Float, 0, 2)
            };

            layout = new InputLayout(device, effect.GetTechniqueByIndex(0).GetPassByIndex(0).Description.Signature, elements);
            textureView = new ShaderResourceView(device, texture);

            binding = new[] {
                new VertexBufferBinding(vertices, 12, 0),
                new VertexBufferBinding(normals, 12, 0), 
                new VertexBufferBinding(texCoords, 8, 0)
            };
        }

        private void LoadMesh(string meshName)
        {
            var meshData = Smd.FromFile(meshName);
            using (var data = new DataStream(meshData.Indices.ToArray(), true, false))
            {
                indices = new Buffer(device, data, new BufferDescription(meshData.Indices.Count * 4, ResourceUsage.Default, BindFlags.IndexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None));
                indexCount = meshData.Indices.Count;
            }

            using (var data = new DataStream(meshData.Positions.ToArray(), true, false))
            {
                vertices = new Buffer(device, data, new BufferDescription(meshData.Positions.Count * 4 * 3, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None));
            }
            using (var data = new DataStream(meshData.Normals.ToArray(), true, false))
            {
                normals = new Buffer(device, data, new BufferDescription(meshData.Normals.Count * 4 * 3, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None));
            }

            using (var data = new DataStream(meshData.TextureCoordinates.ToArray(), true, false))
            {
                texCoords = new Buffer(device, data, new BufferDescription(meshData.TextureCoordinates.Count * 4 * 2, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None));
            }
        }

        public void Draw()
        {
            effect.GetVariableByName("model_texture").AsResource().SetResource(textureView);
            device.InputAssembler.SetInputLayout(layout);
            device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            device.InputAssembler.SetIndexBuffer(indices, Format.R32_UInt, 0);
            device.InputAssembler.SetVertexBuffers(0, binding);

            effect.GetTechniqueByIndex(0).GetPassByIndex(0).Apply();
            device.DrawIndexed(indexCount, 0, 0);

            device.InputAssembler.SetIndexBuffer(null, Format.Unknown, 0);
            device.InputAssembler.SetVertexBuffers(0, nullBinding);
        }

        public void Dispose()
        {
            indices.Dispose();
            normals.Dispose();
            vertices.Dispose();
            texCoords.Dispose();
            texture.Dispose();
            effect.Dispose();
            textureView.Dispose();
            layout.Dispose();
        }

        public Effect Effect { get { return effect; } }

        private ShaderResourceView textureView;
        private Effect effect;
        private InputLayout layout;
        private readonly VertexBufferBinding[] nullBinding = new VertexBufferBinding[3];
        private VertexBufferBinding[] binding;
        private InputElement[] elements;
        private int indexCount;
        private Device device;
        private Buffer indices;
        private Buffer normals;
        private Buffer vertices;
        private Buffer texCoords;

        private Texture2D texture;
    }
}
