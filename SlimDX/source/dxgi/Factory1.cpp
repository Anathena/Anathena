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

#include "Factory1.h"
#include "Adapter1.h"

using namespace System;

namespace SlimDX
{
namespace DXGI
{
	bool Factory1::AdapterEnumerator1::MoveNext()
	{
		IDXGIAdapter1* adapter = 0;
		HRESULT hr = m_factory->InternalPointer->EnumAdapters1(m_index++, &adapter);
		if (FAILED(hr))
			return false;

		m_current = Adapter1::FromPointer(adapter, m_factory);
		return true;
	}

	Factory1::Factory1( IDXGIFactory1* pointer, ComObject^ owner )
		: Factory( true )
	{
		Construct( pointer, owner );
	}
	
	Factory1::Factory1( IntPtr pointer )
		: Factory( true )
	{
		Construct( pointer, NativeInterface );
	}
	
	Factory1^ Factory1::FromPointer( IDXGIFactory1* pointer, ComObject^ owner, ComObjectFlags flags )
	{
		return ComObject::ConstructFromPointer<Factory1, IDXGIFactory1>( pointer, owner, flags );
	}
	
	Factory1^ Factory1::FromPointer( IntPtr pointer )
	{
		return ComObject::ConstructFromUserPointer<Factory1>( pointer );
	}

	Factory1::Factory1()
		: Factory(true)
	{
		IDXGIFactory1* factory = 0;
		RECORD_DXGI( CreateDXGIFactory1( __uuidof( IDXGIFactory1 ), reinterpret_cast<void**>( &factory ) ) );
		if( Result::Last.IsFailure )
			throw gcnew DXGIException( Result::Last );

		Construct( factory );
	}

	Factory1::AdapterEnumerator1 Factory1::EnumerateAdapters1()
	{
		return Factory1::AdapterEnumerator1(this);
	}

	int Factory1::GetAdapterCount1()
	{
		int count = 0;
		IDXGIAdapter1* adapter = 0;
		while( InternalPointer->EnumAdapters1( count, &adapter ) != DXGI_ERROR_NOT_FOUND )
		{
			adapter->Release();
			++count;
		}

		return count;
	}

	Adapter1^ Factory1::GetAdapter1(int index)
	{
		IDXGIAdapter1* adapter = 0;
		RECORD_DXGI( InternalPointer->EnumAdapters1( index, &adapter) );
		if( Result::Last.IsFailure )
			return nullptr;

		return Adapter1::FromPointer( adapter, this );
	}

	bool Factory1::IsCurrent::get()
	{
		return InternalPointer->IsCurrent() == TRUE;
	}
}
}
