/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using D3D = SlimDX.Direct3D10;
using SlimDX.D3DCompiler;

namespace SlimDX.SampleFramework
{
    /// <summary>
    /// Provides functionality to render a user interface using Direct3D10.
    /// </summary>6
    public class UserInterfaceRenderer10 : UserInterfaceRenderer
    {
        #region Public Interface

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInterfaceRenderer10"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="width">The width of the renderable area.</param>
        /// <param name="height">The height of the renderable area.</param>
        public UserInterfaceRenderer10(D3D.Device device, int width, int height)
        {
            if (device == null)
                throw new ArgumentNullException("device");
            if (width < 0)
                throw new ArgumentException("Value must be positive.", "width");
            if (height < 0)
                throw new ArgumentException("Value must be positive.", "height");

            this.device = device;
            halfWidth = width / 2;
            halfHeight = height / 2;

            font = new D3D.Font(device, 18, 0, D3D.FontWeight.Bold, 0, false, D3D.FontCharacterSet.Default, D3D.FontPrecision.Default, D3D.FontQuality.Antialiased, D3D.FontPitchAndFamily.Default, "Arial");
            sprite = new SlimDX.Direct3D10.Sprite(device, 0);
            lineBuffer = new DynamicPrimitiveBuffer10<ColoredVertex>(device);

            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("SampleFramework.Resources.UserInterface10.fx"))
            using (StreamReader reader = new StreamReader(stream))
            {
                effect = D3D.Effect.FromString(device, reader.ReadToEnd(), "fx_4_0");
            }

            technique = effect.GetTechniqueByIndex(0);
            pass = technique.GetPassByIndex(0);

            ShaderSignature signature = pass.Description.Signature;
            inputLayout = new D3D.InputLayout(device, signature, new[] {
				new D3D.InputElement("POSITION", 0, SlimDX.DXGI.Format.R32G32B32_Float, 0, 0),
				new D3D.InputElement("COLOR", 0, SlimDX.DXGI.Format.R8G8B8A8_UNorm, D3D.InputElement.AppendAligned, 0 )
			});
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected override void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                effect.Dispose();
                inputLayout.Dispose();
                lineBuffer.Dispose();
                sprite.Dispose();
                font.Dispose();
            }

            base.Dispose(disposeManagedResources);
        }

        /// <summary>
        /// In a derived class, implements logic to flush all pending rendering commands.
        /// </summary>
        protected override void Flush()
        {
            Matrix offset = Matrix.Translation(-1.0f, 1.0f, 0.0f);
            Matrix scale = Matrix.Scaling(1.0f / halfWidth, -1.0f / halfHeight, 1.0f);

            D3D.EffectVariable transform = effect.GetVariableBySemantic("MatrixWVP");
            transform.AsMatrix().SetMatrix(scale * offset);
            pass.Apply();

            lineBuffer.Commit();
            device.InputAssembler.SetInputLayout(inputLayout);
            device.InputAssembler.SetVertexBuffers(0, new D3D.VertexBufferBinding(lineBuffer.UnderlyingBuffer, lineBuffer.ElementSize, 0));
            device.InputAssembler.SetPrimitiveTopology(D3D.PrimitiveTopology.LineList);
            device.Draw(lineBuffer.Count, 0);
            lineBuffer.Clear();

            sprite.Begin(SlimDX.Direct3D10.SpriteFlags.SaveState);
            foreach (Text text in textBuffer)
            {
                font.Draw(sprite, text.String, new System.Drawing.Rectangle(text.X, text.Y, 100, 100), D3D.FontDrawFlags.SingleLine, text.Color);
            }
            sprite.End();

            textBuffer.Clear();
        }

        /// <summary>
        /// Computes the metrics for a string if it were to be rendered with this renderer.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <returns>The size metrics for the string.</returns>
        internal override Vector2 MeasureString(string text)
        {
            System.Drawing.Rectangle bounds = font.Measure(null, text, new System.Drawing.Rectangle(0, 0, halfWidth * 2, halfHeight * 2), D3D.FontDrawFlags.SingleLine);
            return new Vector2(bounds.Width, bounds.Height);
        }

        /// <summary>
        /// Renders a string.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <param name="x">The X coordinate of the upper left corner of the text.</param>
        /// <param name="y">The Y coordinate of the upper left corner of the text.</param>
        /// <param name="color">The color of the text.</param>
        internal override void RenderString(string text, int x, int y, Color4 color)
        {
            textBuffer.Add(new Text(x, y, text, color));
        }

        /// <summary>
        /// Renders a line.
        /// </summary>
        /// <param name="x0">The X coordinate of the first point.</param>
        /// <param name="y0">The Y coordinate of the first point.</param>
        /// <param name="color0">The color of the first point.</param>
        /// <param name="x1">The X coordinate of the second point.</param>
        /// <param name="y1">The Y coordinate of the second point.</param>
        /// <param name="color1">The color of the second point.</param>
        internal override void RenderLine(int x0, int y0, Color4 color0, int x1, int y1, Color4 color1)
        {
            lineBuffer.Add(new ColoredVertex(
                    new Vector3(x0, y0, 0.0f),
                    color0.ToArgb()
            ));

            lineBuffer.Add(new ColoredVertex(
                    new Vector3(x1, y1, 0.0f),
                    color1.ToArgb()
            ));
        }

        #endregion
        #region Implementation Detail

        D3D.Device device;
        int halfWidth;
        int halfHeight;

        D3D.Font font;
        D3D.Sprite sprite;
        DynamicPrimitiveBuffer10<ColoredVertex> lineBuffer;
        List<Text> textBuffer = new List<Text>();

        D3D.Effect effect;
        D3D.EffectTechnique technique;
        D3D.EffectPass pass;
        D3D.InputLayout inputLayout;

        #endregion
    }
}
