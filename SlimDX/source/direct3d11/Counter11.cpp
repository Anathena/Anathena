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

#include "Direct3D11Exception.h"

#include "Counter11.h"
#include "Device11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	Counter::Counter( SlimDX::Direct3D11::Device^ device, Direct3D11::CounterKind counterKind )
	{
		ID3D11Counter* counter = 0;
		D3D11_COUNTER_DESC nativeDescription;
		nativeDescription.Counter = static_cast<D3D11_COUNTER>( counterKind );

		if( RECORD_D3D11( device->InternalPointer->CreateCounter( &nativeDescription, &counter ) ).IsFailure )
			throw gcnew Direct3D11Exception( Result::Last );
		
		Construct( counter );
	}

	Direct3D11::CounterKind Counter::CounterKind::get()
	{
		D3D11_COUNTER_DESC description;
		InternalPointer->GetDesc( &description );

		return static_cast<Direct3D11::CounterKind>( description.Counter );
	}
}
}
