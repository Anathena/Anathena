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

#include <d3d10.h>

#include "Direct3D10Exception.h"

#include "StateBlock10.h"
#include "Device10.h"
#include "StateBlockMask.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	StateBlock::StateBlock( SlimDX::Direct3D10::Device^ device, StateBlockMask^ mask )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
	
		ID3D10StateBlock* stateBlock = 0;
		D3D10_STATE_BLOCK_MASK nativeMask = mask->CreateNativeVersion();
		
		if( RECORD_D3D10( D3D10CreateStateBlock( device->InternalPointer, &nativeMask, &stateBlock ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		Construct( stateBlock );
	}

	Device^ StateBlock::Device::get()
	{
		ID3D10Device* device = 0;
		InternalPointer->GetDevice( &device );
		return SlimDX::Direct3D10::Device::FromPointer( device );
	}
	
	Result StateBlock::Apply() {
		return RECORD_D3D10( InternalPointer->Apply() );
	}
	
	Result StateBlock::Capture() {
		return RECORD_D3D10( InternalPointer->Capture() );
	}
	
	Result StateBlock::ReleaseAllDeviceObjects() {
		return RECORD_D3D10( InternalPointer->ReleaseAllDeviceObjects() );
	}
}
}
