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
#pragma once

#include "../math/Color4.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>
		/// Callback function used for animations.
		/// </summary>
		/// <param name="track">The track for which the callback is occuring.</param>
		/// <param name="data">Application defined data.</param>
		public delegate void AnimationCallback( int track, System::Object^ data );

		/// <summary>
		/// Callback function used by 2D texture fill functions.
		/// </summary>
		/// <param name="coordinate">Texture coordinate being sampled.</param>
		/// <param name="texelSize">Dimensions of the texel.</param>
		/// <returns>The desired color of the specified texel.</returns>
		/// <unmanaged>LPD3DXFILL2D</unmanaged>
		public delegate Color4 Fill2DCallback(Vector2 coordinate, Vector2 texelSize);

		/// <summary>
		/// Callback function used by 3D texture fill functions.
		/// </summary>
		/// <param name="coordinate">Texture coordinate being sampled.</param>
		/// <param name="texelSize">Dimensions of the texel.</param>
		/// <returns>The desired color of the specified texel.</returns>
		/// <unmanaged>LPD3DXFILL3D</unmanaged>
		public delegate Color4 Fill3DCallback(Vector3 coordinate, Vector3 texelSize);
	}
}
