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

#include "UnorderedAccessViewDescription11.h"

namespace SlimDX
{
namespace Direct3D11
{
	UnorderedAccessViewDescription::UnorderedAccessViewDescription( const D3D11_UNORDERED_ACCESS_VIEW_DESC& native )
	{
		Format = static_cast<DXGI::Format>( native.Format );
		Dimension = static_cast<UnorderedAccessViewDimension>( native.ViewDimension );

		switch( Dimension )
		{
			case UnorderedAccessViewDimension::Buffer:
				FirstElement = native.Buffer.FirstElement;
				ElementCount = native.Buffer.NumElements;
				Flags = static_cast<UnorderedAccessViewBufferFlags>( native.Buffer.Flags );
				break;

			case UnorderedAccessViewDimension::Texture1D:
				MipSlice = native.Texture1D.MipSlice;
				break;

			case UnorderedAccessViewDimension::Texture1DArray:
				ArraySize = native.Texture1DArray.ArraySize;
				FirstArraySlice = native.Texture1DArray.FirstArraySlice;
				MipSlice = native.Texture1DArray.MipSlice;
				break;

			case UnorderedAccessViewDimension::Texture2D:
				MipSlice = native.Texture2D.MipSlice;
				break;

			case UnorderedAccessViewDimension::Texture2DArray:
				ArraySize = native.Texture2DArray.ArraySize;
				FirstArraySlice = native.Texture2DArray.FirstArraySlice;
				MipSlice = native.Texture2DArray.MipSlice;
				break;

			case UnorderedAccessViewDimension::Texture3D:
				MipSlice = native.Texture3D.MipSlice;
				FirstDepthSlice = native.Texture3D.FirstWSlice;
				DepthSliceCount = native.Texture3D.WSize;
				break;

			default:
				break;
		}
	}
	
	D3D11_UNORDERED_ACCESS_VIEW_DESC UnorderedAccessViewDescription::CreateNativeVersion()
	{
		D3D11_UNORDERED_ACCESS_VIEW_DESC native;
		native.Format = static_cast<DXGI_FORMAT>( Format );
		native.ViewDimension = static_cast<D3D11_UAV_DIMENSION>( Dimension );

		switch( Dimension )
		{
			case UnorderedAccessViewDimension::Buffer:
				native.Buffer.FirstElement = FirstElement;
				native.Buffer.NumElements = ElementCount;
				native.Buffer.Flags = static_cast<UINT>( Flags );
				break;

			case UnorderedAccessViewDimension::Texture1D:
				native.Texture1D.MipSlice = MipSlice;
				break;

			case UnorderedAccessViewDimension::Texture1DArray:
				native.Texture1DArray.ArraySize = ArraySize;
				native.Texture1DArray.FirstArraySlice = FirstArraySlice;
				native.Texture1DArray.MipSlice = MipSlice;
				break;

			case UnorderedAccessViewDimension::Texture2D:
				native.Texture2D.MipSlice = MipSlice;
				break;

			case UnorderedAccessViewDimension::Texture2DArray:
				native.Texture2DArray.ArraySize = ArraySize;
				native.Texture2DArray.FirstArraySlice = FirstArraySlice;
				native.Texture2DArray.MipSlice = MipSlice;
				break;

			case UnorderedAccessViewDimension::Texture3D:
				native.Texture3D.MipSlice = MipSlice;
				native.Texture3D.FirstWSlice = FirstDepthSlice;
				native.Texture3D.WSize = DepthSliceCount;
				break;

			default:
				break;
		}
		
		return native;
	}

	bool UnorderedAccessViewDescription::operator == ( UnorderedAccessViewDescription left, UnorderedAccessViewDescription right )
	{
		return UnorderedAccessViewDescription::Equals( left, right );
	}

	bool UnorderedAccessViewDescription::operator != ( UnorderedAccessViewDescription left, UnorderedAccessViewDescription right )
	{
		return !UnorderedAccessViewDescription::Equals( left, right );
	}

	int UnorderedAccessViewDescription::GetHashCode()
	{
		return Format.GetHashCode() + Dimension.GetHashCode() + FirstElement.GetHashCode()
			 + ElementCount.GetHashCode() + MipSlice.GetHashCode() + FirstArraySlice.GetHashCode()
			 + ArraySize.GetHashCode() + FirstDepthSlice.GetHashCode() + DepthSliceCount.GetHashCode() + Flags.GetHashCode();
	}

	bool UnorderedAccessViewDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<UnorderedAccessViewDescription>( value ) );
	}

	bool UnorderedAccessViewDescription::Equals( UnorderedAccessViewDescription value )
	{
		return ( Format == value.Format && Dimension == value.Dimension && FirstElement == value.FirstElement && Flags == value.Flags
			 && ElementCount == value.ElementCount && MipSlice == value.MipSlice && FirstArraySlice == value.FirstArraySlice
			 && ArraySize == value.ArraySize && FirstDepthSlice == value.FirstDepthSlice && DepthSliceCount == value.DepthSliceCount );
	}

	bool UnorderedAccessViewDescription::Equals( UnorderedAccessViewDescription% value1, UnorderedAccessViewDescription% value2 )
	{
		return ( value1.Format == value2.Format && value1.Dimension == value2.Dimension && value1.FirstElement == value2.FirstElement && value1.Flags == value2.Flags
			 && value1.ElementCount == value2.ElementCount && value1.MipSlice == value2.MipSlice && value1.FirstArraySlice == value2.FirstArraySlice
			 && value1.ArraySize == value2.ArraySize && value1.FirstDepthSlice == value2.FirstDepthSlice && value1.DepthSliceCount == value2.DepthSliceCount );
	}
}
}
