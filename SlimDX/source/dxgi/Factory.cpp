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

#include "DXGIException.h"

#include "Adapter.h"
#include "Factory.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace DXGI
{
	bool Factory::AdapterEnumerator::MoveNext()
	{
		IDXGIAdapter* adapter = 0;
		HRESULT hr = m_factory->InternalPointer->EnumAdapters(m_index++, &adapter);
		if (FAILED(hr))
			return false;

		m_current = Adapter::FromPointer(adapter, m_factory);
		return true;
	}

	Factory::Factory()
	{
		IDXGIFactory* factory = 0;
		RECORD_DXGI( CreateDXGIFactory( __uuidof( IDXGIFactory ), reinterpret_cast<void**>( &factory ) ) );
		if( Result::Last.IsFailure )
			throw gcnew DXGIException( Result::Last );

		Construct( factory );
	}

	Factory::AdapterEnumerator Factory::EnumerateAdapters()
	{
		return Factory::AdapterEnumerator(this);
	}

	int Factory::GetAdapterCount()
	{
		int count = 0;
		IDXGIAdapter* adapter = 0;
		while( InternalPointer->EnumAdapters( count, &adapter) != DXGI_ERROR_NOT_FOUND )
		{
			adapter->Release();
			++count;
		}

		return count;
	}

	Adapter^ Factory::GetAdapter( int index )
	{
		IDXGIAdapter* adapter = 0;
		RECORD_DXGI( InternalPointer->EnumAdapters( index, &adapter) );
		if( Result::Last.IsFailure )
			return nullptr;
		return Adapter::FromPointer( adapter, this );
	}
	
	Adapter^ Factory::CreateSoftwareAdapter( IntPtr softwareModule )
	{
		if( softwareModule == IntPtr::Zero )
			throw gcnew ArgumentNullException( "softwareModule" );
			
		HINSTANCE instance = reinterpret_cast<HINSTANCE>( softwareModule.ToPointer() );
		IDXGIAdapter* adapter = 0;
		RECORD_DXGI( InternalPointer->CreateSoftwareAdapter( instance, &adapter ) );
		
		if( adapter == 0 )
			return nullptr;
		return Adapter::FromPointer( adapter );
	}
	
	Adapter^ Factory::CreateSoftwareAdapter( Module^ module )
	{
		return CreateSoftwareAdapter( Marshal::GetHINSTANCE( module ) );
	}
	
	IntPtr Factory::GetWindowAssociation()
	{
		HWND window = 0;
		RECORD_DXGI( InternalPointer->GetWindowAssociation( &window ) );
		return IntPtr( window );
	}
	
	Result Factory::SetWindowAssociation( IntPtr handle, WindowAssociationFlags flags )
	{
		return RECORD_DXGI( InternalPointer->MakeWindowAssociation( reinterpret_cast<HWND>( handle.ToPointer() ), static_cast<UINT>( flags ) ) );
	}
}
}
