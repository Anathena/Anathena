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

#include <windows.h>
#include <d3d10.h>
#include <d3dx10.h>

namespace SlimDX
{
	namespace Direct3D10_1
	{
		/// <summary>
		/// Specifies possible levels of hardware acceleration features.
		/// </summary>
		/// <unmanaged>D3D10_FEATURE_LEVEL1</unmanaged>
		public enum class FeatureLevel
		{
			Level_10_0 = D3D10_FEATURE_LEVEL_10_0,
			Level_10_1 = D3D10_FEATURE_LEVEL_10_1,
			Level_9_1 = D3D10_FEATURE_LEVEL_9_1,
			Level_9_2 = D3D10_FEATURE_LEVEL_9_2,
			Level_9_3 = D3D10_FEATURE_LEVEL_9_3,
		};

		/// <summary>Identifies the type of the resource that will be viewed.</summary>
		/// <unmanaged>D3D10_1_SRV_DIMENSION</unmanaged>
		public enum class ShaderResourceViewDimension1 : System::Int32
		{
			Unknown = D3D10_1_SRV_DIMENSION_UNKNOWN,
			Buffer = D3D10_1_SRV_DIMENSION_BUFFER,
			Texture1D = D3D10_1_SRV_DIMENSION_TEXTURE1D,
			Texture1DArray = D3D10_1_SRV_DIMENSION_TEXTURE1DARRAY,
			Texture2D = D3D10_1_SRV_DIMENSION_TEXTURE2D,
			Texture2DArray = D3D10_1_SRV_DIMENSION_TEXTURE2DARRAY,
			Texture2DMultisampled = D3D10_1_SRV_DIMENSION_TEXTURE2DMS,
			Texture2DMultisampledArray = D3D10_1_SRV_DIMENSION_TEXTURE2DMSARRAY,
			Texture3D = D3D10_1_SRV_DIMENSION_TEXTURE3D,
			TextureCube = D3D10_1_SRV_DIMENSION_TEXTURECUBE,
			TextureCubeArray = D3D10_1_SRV_DIMENSION_TEXTURECUBEARRAY
		};
	}
}
