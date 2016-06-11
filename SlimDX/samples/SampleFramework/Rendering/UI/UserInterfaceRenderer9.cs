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
using System.Reflection;

using SlimDX;
using SlimDX.Direct3D9;

namespace SlimDX.SampleFramework {
    /// <summary>
    /// Provides functionality to render a user interface using Direct3D9.
    /// </summary>
    public class UserInterfaceRenderer9 : UserInterfaceRenderer {
        #region Public Interface

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInterfaceRenderer9"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="width">The width of the renderable area.</param>
        /// <param name="height">The height of the renderable area.</param>
        public UserInterfaceRenderer9(Device device, int width, int height) {
            if (device == null)
                throw new ArgumentNullException("device");
            if (width < 0)
                throw new ArgumentException("Value must be positive.", "width");
            if (height < 0)
                throw new ArgumentException("Value must be positive.", "height");

            this.device = device;
            halfWidth = width / 2;
            halfHeight = height / 2;

            font = new Font(device, 18, 0, FontWeight.Bold, 0, false, CharacterSet.Default, Precision.Default, FontQuality.Antialiased, PitchAndFamily.Default, "Arial");
            lineBuffer = new DynamicPrimitiveBuffer9<ColoredVertex>(device);

            stateBlock = new StateBlock(device, StateBlockType.All);
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected override void Dispose(bool disposeManagedResources) {
            if (disposeManagedResources) {
                stateBlock.Dispose();
                lineBuffer.Dispose();
                font.Dispose();
            }

            base.Dispose(disposeManagedResources);
        }

        /// <summary>
        /// In a derived class, implements logic to flush all pending rendering commands.
        /// </summary>
        protected override void Flush() {
            try {
                stateBlock.Capture();

                Matrix offset = Matrix.Translation(-1.0f, 1.0f, 0.0f);
                Matrix scale = Matrix.Scaling(1.0f / halfWidth, -1.0f / halfHeight, 1.0f);

                device.SetTransform(TransformState.World, scale * offset);
                device.SetTransform(TransformState.View, Matrix.Identity);
                device.SetTransform(TransformState.Projection, Matrix.Identity);

                device.SetRenderState(RenderState.Lighting, false);

                lineBuffer.Commit();
                device.VertexFormat = VertexFormat.Position | VertexFormat.Diffuse;
                device.SetStreamSource(0, lineBuffer.UnderlyingBuffer, 0, lineBuffer.ElementSize);
                device.DrawPrimitives(PrimitiveType.LineList, 0, lineBuffer.Count / 2);
                lineBuffer.Clear();

                foreach (Text text in textBuffer)
                    font.DrawString(null, text.String, text.X, text.Y, text.Color);
                textBuffer.Clear();
            } finally {
                stateBlock.Apply();
            }
        }

        /// <summary>
        /// Computes the metrics for a string if it were to be rendered with this renderer.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <returns>The size metrics for the string.</returns>
        internal override Vector2 MeasureString(string text) {
            System.Drawing.Rectangle bounds = font.MeasureString(null, text, DrawTextFormat.SingleLine);
            return new Vector2(bounds.Width, bounds.Height);
        }

        /// <summary>
        /// Renders a string.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <param name="x">The X coordinate of the upper left corner of the text.</param>
        /// <param name="y">The Y coordinate of the upper left corner of the text.</param>
        /// <param name="color">The color of the text.</param>
        internal override void RenderString(string text, int x, int y, Color4 color) {
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
        internal override void RenderLine(int x0, int y0, Color4 color0, int x1, int y1, Color4 color1) {
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

        Device device;
        int halfWidth;
        int halfHeight;

        Font font;
        DynamicPrimitiveBuffer9<ColoredVertex> lineBuffer;
        List<Text> textBuffer = new List<Text>();

        StateBlock stateBlock;

        #endregion
    }
}
