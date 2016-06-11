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
#include "../Utilities.h"

#include "Direct3D9Exception.h"
#include "Direct3D9NotFoundException.h"
#include "Direct3DX9NotFoundException.h"

#include "Enums.h"

#include "Direct3DEx.h"

using namespace System;
using namespace System::Collections::Generic;

namespace SlimDX
{
namespace Direct3D9
{
	Direct3DEx::Direct3DEx()
		: Direct3D( true )
	{
		HRESULT hr;
		IDirect3D9Ex *direct3D = NULL;

        try
        {
		    hr = Direct3DCreate9Ex( D3D_SDK_VERSION, &direct3D );
        }
        catch( System::Runtime::InteropServices::SEHException^ ex )
        {
            throw gcnew Direct3D9NotFoundException( "Direct3D 9 was not found. Reinstalling DirectX may fix the problem.", ex );
        }

		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		if( direct3D == NULL )
			throw gcnew Direct3D9Exception( "Could not create Direct3DEx instance." );

		Adapters = gcnew AdapterCollection( direct3D, false );
		adaptersEx = gcnew AdapterExCollection( direct3D, false );

        try
        {
            D3DXCheckVersion( D3D_SDK_VERSION, D3DX_SDK_VERSION );
        }
        catch( System::Runtime::InteropServices::SEHException^ ex )
        {
            throw gcnew Direct3DX9NotFoundException( "Direct3DX 9 was not found. Please install "
                "the latest DirectX end-user redistributable package from Microsoft.", ex );
        }

		Construct( direct3D );
	}
	
	Direct3DEx::Direct3DEx( IDirect3D9Ex* direct3d, ComObject^ owner )
		: Direct3D( true )
	{
		Construct( direct3d, owner );
	}

	Direct3DEx::Direct3DEx( IntPtr pointer )
		: Direct3D( true )
	{
		Construct( pointer, NativeInterface );
	}
	
	Direct3DEx^ Direct3DEx::FromPointer( IDirect3D9Ex* pointer, ComObject^ owner, ComObjectFlags flags ) 
	{
		return ComObject::ConstructFromPointer<Direct3DEx,IDirect3D9Ex>( pointer, owner, flags );
	}
	
	Direct3DEx^ Direct3DEx::FromPointer( IntPtr pointer )
	{
		return ComObject::ConstructFromUserPointer<Direct3DEx>( pointer );
	}
	
    DisplayModeEx Direct3DEx::GetAdapterDisplayModeEx( int adapter )
	{
		D3DDISPLAYMODEEX nativeMode = {0};
		nativeMode.Size = sizeof(D3DDISPLAYMODEEX);
		HRESULT hr = InternalPointer->GetAdapterDisplayModeEx( adapter, &nativeMode, NULL );
		RECORD_D3D9( hr );

		DisplayModeEx mode = DisplayModeEx::FromUnmanaged( nativeMode );
		return mode;
	}

    DisplayModeEx Direct3DEx::GetAdapterDisplayModeEx( int adapter, [Out] DisplayRotation% rotation )
	{
		D3DDISPLAYMODEEX nativeMode = {0};
		nativeMode.Size = sizeof(D3DDISPLAYMODEEX);
		D3DDISPLAYROTATION nativeRotation = D3DDISPLAYROTATION_IDENTITY;
		HRESULT hr = InternalPointer->GetAdapterDisplayModeEx( adapter, &nativeMode, &nativeRotation );
		RECORD_D3D9( hr );

		DisplayModeEx mode = DisplayModeEx::FromUnmanaged( nativeMode );
		rotation = static_cast<DisplayRotation>( nativeRotation );

		return mode;
	}

	System::Int64 Direct3DEx::GetAdapterLuid( int adapter )
	{
		Int64 luid = 0;
		InternalPointer->GetAdapterLUID( adapter, reinterpret_cast<LUID*>( &luid ) );
		return luid;
	}

    int Direct3DEx::GetAdapterModeCountEx( int adapter, DisplayModeFilter filter )
	{
		D3DDISPLAYMODEFILTER nativeFilter;
		filter.ToUnmanaged( nativeFilter );

		return InternalPointer->GetAdapterModeCountEx( adapter, &nativeFilter );
	}

    DisplayModeEx Direct3DEx::EnumerateAdapterModesEx( int adapter, DisplayModeFilter filter, int modeIndex )
	{
		D3DDISPLAYMODEEX nativeMode = {0};
		nativeMode.Size = sizeof(D3DDISPLAYMODEEX);
		D3DDISPLAYMODEFILTER nativeFilter;
		filter.ToUnmanaged( nativeFilter );

		HRESULT hr = InternalPointer->EnumAdapterModesEx( adapter, &nativeFilter, modeIndex, &nativeMode );
		RECORD_D3D9( hr );

		DisplayModeEx mode = mode.FromUnmanaged( nativeMode );
		return mode;
	}
}
}
