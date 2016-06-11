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
#include "AdapterDescription.h"
#include "Output.h"

using namespace System;

namespace SlimDX
{
namespace DXGI
{
	bool Adapter::OutputEnumerator::MoveNext()
	{
		IDXGIOutput* output = 0;
		HRESULT hr = m_adapter->InternalPointer->EnumOutputs(m_index++, &output);
		if (FAILED(hr))
			return false;

		m_current = Output::FromPointer(output, m_adapter);
		return true;
	}

	AdapterDescription Adapter::Description::get()
	{
		DXGI_ADAPTER_DESC nativeDescription;
		if (RECORD_DXGI( InternalPointer->GetDesc( &nativeDescription ) ).IsFailure)
			return AdapterDescription();

		return AdapterDescription( nativeDescription );
	}

	Adapter::OutputEnumerator Adapter::EnumerateOutputs()
	{
		return Adapter::OutputEnumerator(this);
	}
		
	int Adapter::GetOutputCount()
	{
		int count = 0;
		IDXGIOutput* output = 0;
		while( InternalPointer->EnumOutputs( count, &output) != DXGI_ERROR_NOT_FOUND )
		{
			output->Release();
			++count;
		}

		return count;
	}
	
	Output^ Adapter::GetOutput( int index )
	{
		IDXGIOutput* output = 0;
		RECORD_DXGI( InternalPointer->EnumOutputs( index, &output) );
		if( Result::Last.IsFailure )
			return nullptr;
		return Output::FromPointer( output, this );
	}

	bool Adapter::IsInterfaceSupported( Type^ type )
	{
		Int64 version = 0;
		return IsInterfaceSupported( type, version );
	}

	bool Adapter::IsInterfaceSupported( Type^ type, [Out] Int64% userModeVersion )
	{
		GUID guid = Utilities::GetNativeGuidForType( type );
		LARGE_INTEGER version = { };

		// We intentionally do not record this HRESULT right away; if we did,
		// we might throw out of this method, which make it rather pointless.
		HRESULT hr = InternalPointer->CheckInterfaceSupport( guid, &version );
		RECORD_DXGI( S_OK );
		if( FAILED( hr ) )
		{
			userModeVersion = 0;
			return false;
		}

		userModeVersion = version.QuadPart;
		return true;
	}
	
	String^ Adapter::ToString()
	{
		return Description.Description;
	}
}
}
