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

using System.Collections.Generic;

namespace SlimDX.SampleFramework {
	/// <summary>
	/// A container for mesh data.
	/// </summary>
	public class MeshData {
		/// <summary>
		/// Gets position data.
		/// </summary>
		public List<Vector3> Positions {
			get {
				return positions;
			}	
		}

		/// <summary>
		/// Gets normal data.
		/// </summary>
		public List<Vector3> Normals {
			get {
				return normals;
			}
		}

		/// <summary>
		/// Gets texture coordinate data.
		/// </summary>
		public List<Vector2> TextureCoordinates {
			get {
				return textureCoordinates;
			}
		}
		
		/// <summary>
		/// Gets mesh index data.
		/// </summary>
		public List<int> Indices {
			get {
				return indices;
			}
		}
		
		List<Vector3> positions = new List<Vector3>();
		List<Vector3> normals = new List<Vector3>();
		List<Vector2> textureCoordinates = new List<Vector2>();
		List<int> indices = new List<int>();
	}
}
