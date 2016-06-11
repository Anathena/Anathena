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

#include <d3d11.h>

#include "ShaderResourceViewDescription11.h"

namespace SlimDX
{
namespace Direct3D11
{
	ShaderResourceViewDescription::ShaderResourceViewDescription( const D3D11_SHADER_RESOURCE_VIEW_DESC& native )
	{
		Format = static_cast<DXGI::Format>( native.Format );
		Dimension = static_cast<ShaderResourceViewDimension>( native.ViewDimension );

		switch( Dimension )
		{
			case ShaderResourceViewDimension::Buffer:
				ElementOffset = native.Buffer.ElementOffset;
				ElementWidth = native.Buffer.ElementWidth;
				break;

			case ShaderResourceViewDimension::Texture1D:
				MipLevels = native.Texture1D.MipLevels;
				MostDetailedMip = native.Texture1D.MostDetailedMip;
				break;

			case ShaderResourceViewDimension::Texture1DArray:
				ArraySize = native.Texture1DArray.ArraySize;
				FirstArraySlice = native.Texture1DArray.FirstArraySlice;
				MipLevels = native.Texture1DArray.MipLevels;
				MostDetailedMip = native.Texture1DArray.MostDetailedMip;
				break;

			case ShaderResourceViewDimension::Texture2D:
				MipLevels = native.Texture2D.MipLevels;
				MostDetailedMip = native.Texture2D.MostDetailedMip;
				break;

			case ShaderResourceViewDimension::Texture2DArray:
				ArraySize = native.Texture2DArray.ArraySize;
				FirstArraySlice = native.Texture2DArray.FirstArraySlice;
				MipLevels = native.Texture2DArray.MipLevels;
				MostDetailedMip = native.Texture2DArray.MostDetailedMip;
				break;

			case ShaderResourceViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;

			case ShaderResourceViewDimension::Texture2DMultisampledArray:
				ArraySize = native.Texture2DMSArray.ArraySize;
				FirstArraySlice = native.Texture2DMSArray.FirstArraySlice;
				break;

			case ShaderResourceViewDimension::Texture3D:
				MipLevels = native.Texture3D.MipLevels;
				MostDetailedMip = native.Texture3D.MostDetailedMip;
				break;

			case ShaderResourceViewDimension::TextureCube:
				MipLevels = native.TextureCube.MipLevels;
				MostDetailedMip = native.TextureCube.MostDetailedMip;
				break;

			case ShaderResourceViewDimension::TextureCubeArray:
				MostDetailedMip = native.TextureCubeArray.MostDetailedMip;
				MipLevels = native.TextureCubeArray.MipLevels;
				First2DArrayFace = native.TextureCubeArray.First2DArrayFace;
				CubeCount = native.TextureCubeArray.NumCubes;
				break;

			case ShaderResourceViewDimension::ExtendedBuffer:
				FirstElement = native.BufferEx.FirstElement;
				ElementCount = native.BufferEx.NumElements;
				Flags = static_cast<ShaderResourceViewExtendedBufferFlags>( native.BufferEx.Flags );
				break;

			default:
				break;
		}
	}
	
	D3D11_SHADER_RESOURCE_VIEW_DESC ShaderResourceViewDescription::CreateNativeVersion()
	{
		D3D11_SHADER_RESOURCE_VIEW_DESC native;
		native.Format = static_cast<DXGI_FORMAT>( Format );
		native.ViewDimension = static_cast<D3D11_SRV_DIMENSION>( Dimension );

		switch( Dimension )
		{
			case ShaderResourceViewDimension::Buffer:
				native.Buffer.ElementOffset = ElementOffset;
				native.Buffer.ElementWidth = ElementWidth;
				break;

			case ShaderResourceViewDimension::Texture1D:
				native.Texture1D.MipLevels = MipLevels;
				native.Texture1D.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension::Texture1DArray:
				native.Texture1DArray.ArraySize = ArraySize;
				native.Texture1DArray.FirstArraySlice = FirstArraySlice;
				native.Texture1DArray.MipLevels = MipLevels;
				native.Texture1DArray.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension::Texture2D:
				native.Texture2D.MipLevels = MipLevels;
				native.Texture2D.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension::Texture2DArray:
				native.Texture2DArray.ArraySize = ArraySize;
				native.Texture2DArray.FirstArraySlice = FirstArraySlice;
				native.Texture2DArray.MipLevels = MipLevels;
				native.Texture2DArray.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;

			case ShaderResourceViewDimension::Texture2DMultisampledArray:
				native.Texture2DMSArray.ArraySize = ArraySize;
				native.Texture2DMSArray.FirstArraySlice = FirstArraySlice;
				break;

			case ShaderResourceViewDimension::Texture3D:
				native.Texture3D.MipLevels = MipLevels;
				native.Texture3D.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension::TextureCube:
				native.TextureCube.MipLevels = MipLevels;
				native.TextureCube.MostDetailedMip = MostDetailedMip;
				break;

			case ShaderResourceViewDimension::TextureCubeArray:
				native.TextureCubeArray.MostDetailedMip = MostDetailedMip;
				native.TextureCubeArray.MipLevels = MipLevels;
				native.TextureCubeArray.First2DArrayFace = First2DArrayFace;
				native.TextureCubeArray.NumCubes = CubeCount;
				break;

			case ShaderResourceViewDimension::ExtendedBuffer:
				native.BufferEx.FirstElement = FirstElement;
				native.BufferEx.NumElements = ElementCount;
				native.BufferEx.Flags = static_cast<UINT>( Flags );
				break;

			default:
				break;
		}
		
		return native;
	}

	bool ShaderResourceViewDescription::operator == ( ShaderResourceViewDescription left, ShaderResourceViewDescription right )
	{
		return ShaderResourceViewDescription::Equals( left, right );
	}

	bool ShaderResourceViewDescription::operator != ( ShaderResourceViewDescription left, ShaderResourceViewDescription right )
	{
		return !ShaderResourceViewDescription::Equals( left, right );
	}

	int ShaderResourceViewDescription::GetHashCode()
	{
		return Format.GetHashCode() + Dimension.GetHashCode() + ElementOffset.GetHashCode()
			 + ElementWidth.GetHashCode() + MostDetailedMip.GetHashCode() + MipLevels.GetHashCode()
			 + FirstArraySlice.GetHashCode() + ArraySize.GetHashCode() + FirstElement.GetHashCode()
			 + ElementCount.GetHashCode() + Flags.GetHashCode() + CubeCount.GetHashCode() + First2DArrayFace.GetHashCode();
	}

	bool ShaderResourceViewDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ShaderResourceViewDescription>( value ) );
	}

	bool ShaderResourceViewDescription::Equals( ShaderResourceViewDescription value )
	{
		return ( Format == value.Format && Dimension == value.Dimension && ElementOffset == value.ElementOffset && FirstElement == value.FirstElement && Flags == value.Flags
			 && ElementCount == value.ElementCount && ElementWidth == value.ElementWidth && MostDetailedMip == value.MostDetailedMip && MipLevels == value.MipLevels
			 && FirstArraySlice == value.FirstArraySlice && ArraySize == value.ArraySize && CubeCount == value.CubeCount && First2DArrayFace == value.First2DArrayFace );
	}

	bool ShaderResourceViewDescription::Equals( ShaderResourceViewDescription% value1, ShaderResourceViewDescription% value2 )
	{
		return ( value1.Format == value2.Format && value1.Dimension == value2.Dimension && value1.ElementOffset == value2.ElementOffset
			 && value1.FirstElement == value2.FirstElement && value1.Flags == value2.Flags && value1.ElementCount == value2.ElementCount 
			 && value1.ElementWidth == value2.ElementWidth && value1.MostDetailedMip == value2.MostDetailedMip && value1.MipLevels == value2.MipLevels
			 && value1.FirstArraySlice == value2.FirstArraySlice && value1.ArraySize == value2.ArraySize
			 && value1.CubeCount == value2.CubeCount && value1.First2DArrayFace == value2.First2DArrayFace );
	}
}
}
