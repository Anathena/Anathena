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
#include "Direct3D.h"
#include "DisplayMode.h"

using namespace System;
using namespace System::Collections::Generic;

namespace SlimDX
{
namespace Direct3D9
{
	bool DisplayMode::operator == ( DisplayMode left, DisplayMode right )
	{
		return DisplayMode::Equals( left, right );
	}

	bool DisplayMode::operator != ( DisplayMode left, DisplayMode right )
	{
		return !DisplayMode::Equals( left, right );
	}

	int DisplayMode::GetHashCode()
	{
		return Width.GetHashCode() + Height.GetHashCode() + RefreshRate.GetHashCode()
			 + Format.GetHashCode();
	}

	bool DisplayMode::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<DisplayMode>( value ) );
	}

	bool DisplayMode::Equals( DisplayMode value )
	{
		return ( Width == value.Width && Height == value.Height && RefreshRate == value.RefreshRate
			 && Format == value.Format );
	}

	bool DisplayMode::Equals( DisplayMode% value1, DisplayMode% value2 )
	{
		return ( value1.Width == value2.Width && value1.Height == value2.Height && value1.RefreshRate == value2.RefreshRate
			 && value1.Format == value2.Format );
	}

	DisplayModeCollection::DisplayModeCollection( IDirect3D9 *direct3D, unsigned int adapter, Format format )
		: ReadOnlyCollection( gcnew List<DisplayMode>() )
	{
		int count = direct3D->GetAdapterModeCount( adapter, static_cast<D3DFORMAT>( format ) );

		for( int i = 0; i < count; ++i )
		{
			DisplayMode displayMode;
			HRESULT hr = direct3D->EnumAdapterModes( adapter, static_cast<D3DFORMAT>( format ), i, reinterpret_cast<D3DDISPLAYMODE*>( &displayMode ) );
			if( RECORD_D3D9( hr ).IsFailure )
				continue;

			Items->Add( displayMode );
		}
	}
}
}