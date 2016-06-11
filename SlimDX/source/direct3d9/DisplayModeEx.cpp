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
#include <d3d9.h>
#include <d3dx9.h>

#include "../ComObject.h"
#include "../InternalHelpers.h"
#include "../Result.h"

#include "Direct3D9Exception.h"
#include "Direct3DEx.h"
#include "DisplayModeEx.h"

using namespace System;
using namespace System::Collections::Generic;

namespace SlimDX
{
namespace Direct3D9
{
	DisplayModeEx DisplayModeEx::FromUnmanaged( const D3DDISPLAYMODEEX& nativeMode )
	{
		DisplayModeEx mode;

		mode.Width = nativeMode.Width;
		mode.Height = nativeMode.Height;
		mode.RefreshRate = nativeMode.RefreshRate;
		mode.Format = static_cast<SlimDX::Direct3D9::Format>( nativeMode.Format );
		mode.ScanlineOrdering = static_cast<SlimDX::Direct3D9::ScanlineOrdering>( nativeMode.ScanLineOrdering );

		return mode;
	}

	D3DDISPLAYMODEEX DisplayModeEx::ToUnmanaged()
	{
		D3DDISPLAYMODEEX mode;

		mode.Size = sizeof(D3DDISPLAYMODEEX);
		mode.Width = Width;
		mode.Height = Height;
		mode.RefreshRate = RefreshRate;
		mode.Format = static_cast<D3DFORMAT>( Format );
		mode.ScanLineOrdering = static_cast<D3DSCANLINEORDERING>( ScanlineOrdering );

		return mode;
	}

	bool DisplayModeEx::operator == ( DisplayModeEx left, DisplayModeEx right )
	{
		return DisplayModeEx::Equals( left, right );
	}

	bool DisplayModeEx::operator != ( DisplayModeEx left, DisplayModeEx right )
	{
		return !DisplayModeEx::Equals( left, right );
	}

	int DisplayModeEx::GetHashCode()
	{
		return Width.GetHashCode() + Height.GetHashCode() + RefreshRate.GetHashCode()
			 + Format.GetHashCode() + ScanlineOrdering.GetHashCode();
	}

	bool DisplayModeEx::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<DisplayModeEx>( value ) );
	}

	bool DisplayModeEx::Equals( DisplayModeEx value )
	{
		return ( Width == value.Width && Height == value.Height && RefreshRate == value.RefreshRate
			 && Format == value.Format && ScanlineOrdering == value.ScanlineOrdering );
	}

	bool DisplayModeEx::Equals( DisplayModeEx% value1, DisplayModeEx% value2 )
	{
		return ( value1.Width == value2.Width && value1.Height == value2.Height && value1.RefreshRate == value2.RefreshRate
			 && value1.Format == value2.Format && value1.ScanlineOrdering == value2.ScanlineOrdering );
	}

	DisplayModeExCollection::DisplayModeExCollection( IDirect3D9Ex *direct3D, unsigned int adapter, DisplayModeFilter filter )
		: ReadOnlyCollection( gcnew List<DisplayModeEx>() )
	{
		D3DDISPLAYMODEFILTER nativeFilter;
		filter.ToUnmanaged( nativeFilter );

		int count = direct3D->GetAdapterModeCountEx( adapter, &nativeFilter );

		for( int i = 0; i < count; ++i )
		{
			D3DDISPLAYMODEEX nativeDisplayMode = {0};
			nativeDisplayMode.Size = sizeof(D3DDISPLAYMODEEX);
			HRESULT hr = direct3D->EnumAdapterModesEx( adapter, &nativeFilter, i, &nativeDisplayMode );
			if( RECORD_D3D9( hr ).IsFailure )
				continue;

			DisplayModeEx displayMode = DisplayModeEx::FromUnmanaged( nativeDisplayMode );
			Items->Add( displayMode );
		}
	}
}
}