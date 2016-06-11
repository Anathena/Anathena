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
using System.Runtime.InteropServices;

using D3D = SlimDX.Direct3D10;

namespace SlimDX.SampleFramework {


    /// <summary>
    /// The base class for an automatically-resizing buffer of primitive data.
    /// </summary>
    public abstract class DynamicPrimitiveBuffer<T> : IDisposable where T : struct {
        #region Public Interface

        /// <summary>
        /// Gets the number of vertices in the buffer.
        /// </summary>
        public int Count {
            get {
                return vertices.Count;
            }
        }

        /// <summary>
        /// Gets the size (in bytes) of a single buffer element.
        /// </summary>
        public int ElementSize {
            get {
                return Marshal.SizeOf(typeof(T));
            }
        }

        /// <summary>
        /// Performs object finalization.
        /// </summary>
        ~DynamicPrimitiveBuffer() {
            Dispose(false);
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected virtual void Dispose(bool disposeManagedResources) {
        }

        /// <summary>
        /// Adds a vertex to the buffer.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        public void Add(T vertex) {
            vertices.Add(vertex);
            if (vertices.Count > bufferSize) {
                bufferSize = bufferSize == 0 ? initialBufferSize : bufferSize * 2;
                ResizeBuffer(bufferSize * ElementSize);
            }

            needsCommit = true;
        }

        /// <summary>
        /// Clears the buffer of all primitive data.
        /// </summary>
        public void Clear() {
            // Note that we do not require a recommit here, since trying to render an
            // empty buffer will just no-op. It doesn't matter what's in the real buffer
            // on the card at this point, so there's no sense in locking it.
            vertices.Clear();
        }

        /// <summary>
        /// Commits the buffer changes in preparation for rendering.
        /// </summary>
        public void Commit() {
            if (needsCommit) {
                FillBuffer(vertices);
                needsCommit = false;
            }
        }

        /// <summary>
        /// In a derived class, implements logic to resize the buffer.
        /// During resize, the existing buffer contents need not be preserved.
        /// </summary>
        /// <param name="sizeInBytes">The new size, in bytes.</param>
        protected abstract void ResizeBuffer(int sizeInBytes);

        /// <summary>
        /// In a derived class, implements logic to fill the buffer with vertex data.
        /// </summary>
        /// <param name="vertices">The vertex data.</param>
        protected abstract void FillBuffer(List<T> vertices);

        #endregion
        #region Implementation Detail

        const int initialBufferSize = 32;
        int bufferSize;

        List<T> vertices = new List<T>();
        bool needsCommit = false;

        #endregion
    }



    [StructLayout(LayoutKind.Sequential)]
    public struct TransformedVertex : IEquatable<TransformedVertex> {
        /// <summary>
        /// Gets or sets the pre-transformed position of the vertex.
        /// </summary>
        public Vector4 Position {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformedVertex"/> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public TransformedVertex(Vector4 position)
            : this() {
            Position = position;
        }

        /// <summary>
        /// Implements operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(TransformedVertex left, TransformedVertex right) {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements operator !=.
        /// </summary>
        /// <param name="left">The left side of the operator.</param>
        /// <param name="right">The right side of the operator.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TransformedVertex left, TransformedVertex right) {
            return !(left == right);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode() {
            return Position.GetHashCode();
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj) {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            return Equals((TransformedVertex)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TransformedVertex other) {
            return (Position == other.Position);
        }
    }

}
