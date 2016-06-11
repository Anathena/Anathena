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

#include <dxgi.h>

#include "../stack_array.h"

#include "DXGIException.h"

#include "Adapter.h"
#include "DeviceDxgi.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Collections::ObjectModel;

namespace SlimDX
{
namespace DXGI
{
	Device::Device()
	{
	}

	Device::Device( IComObject^ device ) 
	{
		IDXGIDevice* result = 0;

		IUnknown *ptr = reinterpret_cast<IUnknown*>(device->ComPointer.ToPointer());
		if( RECORD_DXGI( ptr->QueryInterface( IID_IDXGIDevice, reinterpret_cast<void**>( &result ) ) ).IsFailure )
			throw gcnew DXGIException( Result::Last );

		ComObject^ other = ObjectTable::Find(IntPtr(result));
		Construct(result, other, other == nullptr);
	}

	int Device::GpuThreadPriority::get()
	{
		int result = 0;
		RECORD_DXGI( InternalPointer->GetGPUThreadPriority( &result ) );

		return result;
	}
	
	void Device::GpuThreadPriority::set( int value )
	{
		RECORD_DXGI( InternalPointer->SetGPUThreadPriority( value ) );
	}

	Adapter^ Device::Adapter::get()
	{
		IDXGIAdapter* adapter = 0;
		if (RECORD_DXGI( InternalPointer->GetAdapter( &adapter ) ).IsFailure)
			return nullptr;

		return DXGI::Adapter::FromPointer( adapter );
	}

	ReadOnlyCollection<Residency>^ Device::QueryResourceResidency( IList<IComObject^>^ resources )
	{
		stack_array<DXGI_RESIDENCY> nativeResidency = stackalloc( DXGI_RESIDENCY, resources->Count );
		stack_array<IUnknown*> nativeResources = stackalloc( IUnknown*, resources->Count );
		
		for( int resourceIndex = 0; resourceIndex < resources->Count; ++resourceIndex )
			nativeResources[resourceIndex] = reinterpret_cast<IUnknown*>(resources[resourceIndex]->ComPointer.ToPointer());
		
		RECORD_DXGI( InternalPointer->QueryResourceResidency( &nativeResources[0], &nativeResidency[0], resources->Count ) );
		if( Result::Last.IsFailure )
			return nullptr;
		
		List<Residency>^ result = gcnew List<Residency>( static_cast<int>( nativeResidency.size() ) );
		for( size_t resourceIndex = 0; resourceIndex < nativeResidency.size(); ++resourceIndex )
			result->Add( static_cast<Residency>( nativeResidency[resourceIndex] ) );

		return gcnew ReadOnlyCollection<Residency>( result );
	}
}
}
