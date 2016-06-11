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

#include "DepthStencilViewDescription11.h"

namespace SlimDX
{
namespace Direct3D11
{
	DepthStencilViewDescription::DepthStencilViewDescription( const D3D11_DEPTH_STENCIL_VIEW_DESC& native )
	{
		Format = static_cast<DXGI::Format>( native.Format );
		Dimension = static_cast<DepthStencilViewDimension>( native.ViewDimension );
		Flags = static_cast<DepthStencilViewFlags>( native.Flags );

		switch( Dimension )
		{
			case DepthStencilViewDimension::Texture1D:
				MipSlice = native.Texture1D.MipSlice;
				break;

			case DepthStencilViewDimension::Texture1DArray:
				ArraySize = native.Texture1DArray.ArraySize;
				FirstArraySlice = native.Texture1DArray.FirstArraySlice;
				MipSlice = native.Texture1DArray.MipSlice;
				break;

			case DepthStencilViewDimension::Texture2D:
				MipSlice = native.Texture2D.MipSlice;
				break;

			case DepthStencilViewDimension::Texture2DArray:
				ArraySize = native.Texture2DArray.ArraySize;
				FirstArraySlice = native.Texture2DArray.FirstArraySlice;
				MipSlice = native.Texture2DArray.MipSlice;
				break;

			case DepthStencilViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;

			case DepthStencilViewDimension::Texture2DMultisampledArray:
				ArraySize = native.Texture2DMSArray.ArraySize;
				FirstArraySlice = native.Texture2DMSArray.FirstArraySlice;
				break;

			default:
				break;
		}
	}
	
	D3D11_DEPTH_STENCIL_VIEW_DESC DepthStencilViewDescription::CreateNativeVersion()
	{
		D3D11_DEPTH_STENCIL_VIEW_DESC native;
		native.Format = static_cast<DXGI_FORMAT>( Format );
		native.ViewDimension = static_cast<D3D11_DSV_DIMENSION>( Dimension );
		native.Flags = static_cast<UINT>( Flags );

		switch( Dimension )
		{
			case DepthStencilViewDimension::Texture1D:
				native.Texture1D.MipSlice = MipSlice;
				break;

			case DepthStencilViewDimension::Texture1DArray:
				native.Texture1DArray.ArraySize = ArraySize;
				native.Texture1DArray.FirstArraySlice = FirstArraySlice;
				native.Texture1DArray.MipSlice = MipSlice;
				break;

			case DepthStencilViewDimension::Texture2D:
				native.Texture2D.MipSlice = MipSlice;
				break;

			case DepthStencilViewDimension::Texture2DArray:
				native.Texture2DArray.ArraySize = ArraySize;
				native.Texture2DArray.FirstArraySlice = FirstArraySlice;
				native.Texture2DArray.MipSlice = MipSlice;
				break;

			case DepthStencilViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;

			case DepthStencilViewDimension::Texture2DMultisampledArray:
				native.Texture2DMSArray.ArraySize = ArraySize;
				native.Texture2DMSArray.FirstArraySlice = FirstArraySlice;
				break;

			default:
				break;
		}
		
		return native;
	}

	bool DepthStencilViewDescription::operator == ( DepthStencilViewDescription left, DepthStencilViewDescription right )
	{
		return DepthStencilViewDescription::Equals( left, right );
	}

	bool DepthStencilViewDescription::operator != ( DepthStencilViewDescription left, DepthStencilViewDescription right )
	{
		return !DepthStencilViewDescription::Equals( left, right );
	}

	int DepthStencilViewDescription::GetHashCode()
	{
		return Format.GetHashCode() + Dimension.GetHashCode() + MipSlice.GetHashCode()
			 + FirstArraySlice.GetHashCode() + ArraySize.GetHashCode() + Flags.GetHashCode();
	}

	bool DepthStencilViewDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<DepthStencilViewDescription>( value ) );
	}

	bool DepthStencilViewDescription::Equals( DepthStencilViewDescription value )
	{
		return ( Format == value.Format && Dimension == value.Dimension && MipSlice == value.MipSlice
			 && FirstArraySlice == value.FirstArraySlice && ArraySize == value.ArraySize && Flags == value.Flags );
	}

	bool DepthStencilViewDescription::Equals( DepthStencilViewDescription% value1, DepthStencilViewDescription% value2 )
	{
		return ( value1.Format == value2.Format && value1.Dimension == value2.Dimension && value1.MipSlice == value2.MipSlice
			 && value1.FirstArraySlice == value2.FirstArraySlice && value1.ArraySize == value2.ArraySize && value1.Flags == value2.Flags );
	}
}
}
