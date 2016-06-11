#include "stdafx.h"
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

#include <d3d10.h>

#include "ShaderResourceViewDescription1.h"

namespace SlimDX
{
namespace Direct3D10_1
{
	ShaderResourceViewDescription1::ShaderResourceViewDescription1( const D3D10_SHADER_RESOURCE_VIEW_DESC1& native )
	{
		Format = static_cast<DXGI::Format>( native.Format );
		Dimension = static_cast<ShaderResourceViewDimension1>( native.ViewDimension );
		switch( Dimension )
		{
			case ShaderResourceViewDimension1::Buffer:
				ElementOffset = native.Buffer.ElementOffset;
				ElementWidth = native.Buffer.ElementWidth;
				break;

			case ShaderResourceViewDimension1::Texture1D:
				MipLevels = native.Texture1D.MipLevels;
				MostDetailedMip = native.Texture1D.MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::Texture1DArray:
				ArraySize = native.Texture1DArray.ArraySize;
				FirstArraySlice = native.Texture1DArray.FirstArraySlice;
				MipLevels = native.Texture1DArray.MipLevels;
				MostDetailedMip = native.Texture1DArray.MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::Texture2D:
				MipLevels = native.Texture2D.MipLevels;
				MostDetailedMip = native.Texture2D.MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::Texture2DArray:
				ArraySize = native.Texture2DArray.ArraySize;
				FirstArraySlice = native.Texture2DArray.FirstArraySlice;
				MipLevels = native.Texture2DArray.MipLevels;
				MostDetailedMip = native.Texture2DArray.MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::Texture2DMultisampled:
				// Nothing to do here.
				break;

			case ShaderResourceViewDimension1::Texture2DMultisampledArray:
				ArraySize = native.Texture2DMSArray.ArraySize;
				FirstArraySlice = native.Texture2DMSArray.FirstArraySlice;
				break;

			case ShaderResourceViewDimension1::Texture3D:
				MipLevels = native.Texture3D.MipLevels;
				MostDetailedMip = native.Texture3D.MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::TextureCube:
				MipLevels = native.TextureCube.MipLevels;
				MostDetailedMip = native.TextureCube.MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::TextureCubeArray:
				MostDetailedMip = native.TextureCubeArray.MostDetailedMip;
				MipLevels = native.TextureCubeArray.MipLevels;
				First2DArrayFace = native.TextureCubeArray.First2DArrayFace;
				CubeCount = native.TextureCubeArray.NumCubes;
				break;

			default:
				break;
		}
	}
	
	D3D10_SHADER_RESOURCE_VIEW_DESC1 ShaderResourceViewDescription1::CreateNativeVersion()
	{
		D3D10_SHADER_RESOURCE_VIEW_DESC1 native;
		native.Format = static_cast<DXGI_FORMAT>( Format );
		native.ViewDimension = static_cast<D3D10_SRV_DIMENSION1>( Dimension );
		switch( Dimension )
		{
			case ShaderResourceViewDimension1::Buffer:
				native.Buffer.ElementOffset = ElementOffset;
				native.Buffer.ElementWidth = ElementWidth;
				break;

			case ShaderResourceViewDimension1::Texture1D:
				native.Texture1D.MipLevels = MipLevels;
				native.Texture1D.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::Texture1DArray:
				native.Texture1DArray.ArraySize = ArraySize;
				native.Texture1DArray.FirstArraySlice = FirstArraySlice;
				native.Texture1DArray.MipLevels = MipLevels;
				native.Texture1DArray.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::Texture2D:
				native.Texture2D.MipLevels = MipLevels;
				native.Texture2D.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::Texture2DArray:
				native.Texture2DArray.ArraySize = ArraySize;
				native.Texture2DArray.FirstArraySlice = FirstArraySlice;
				native.Texture2DArray.MipLevels = MipLevels;
				native.Texture2DArray.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::Texture2DMultisampled:
				// Nothing to do here.
				break;

			case ShaderResourceViewDimension1::Texture2DMultisampledArray:
				native.Texture2DMSArray.ArraySize = ArraySize;
				native.Texture2DMSArray.FirstArraySlice = FirstArraySlice;
				break;

			case ShaderResourceViewDimension1::Texture3D:
				native.Texture3D.MipLevels = MipLevels;
				native.Texture3D.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::TextureCube:
				native.TextureCube.MipLevels = MipLevels;
				native.TextureCube.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension1::TextureCubeArray:
				native.TextureCubeArray.MostDetailedMip = MostDetailedMip;
				native.TextureCubeArray.MipLevels = MipLevels;
				native.TextureCubeArray.First2DArrayFace = First2DArrayFace;
				native.TextureCubeArray.NumCubes = CubeCount;
				break;

			default:
				break;
		}
		
		return native;
	}
}
}
