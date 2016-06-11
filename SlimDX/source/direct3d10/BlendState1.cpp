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
#include <d3dx10.h>

#include "Direct3D10Exception.h"

#include "BlendState1.h"
#include "BlendStateDescription1.h"
#include "Device10_1.h"

using namespace System;
using namespace SlimDX::Direct3D10;

namespace SlimDX
{
namespace Direct3D10_1
{
	BlendState1::BlendState1( ID3D10BlendState1* pointer, ComObject^ owner )
		: BlendState( pointer, owner )
	{
	}
	
	BlendState1::BlendState1( IntPtr pointer )
		: BlendState( pointer )
	{
	}
	
	BlendState1^ BlendState1::FromPointer( ID3D10BlendState1* pointer, ComObject^ owner, ComObjectFlags flags )
	{
		return ComObject::ConstructFromPointer<BlendState1, ID3D10BlendState1>( pointer, owner, flags );
	}
	
	BlendState1^ BlendState1::FromPointer( IntPtr pointer )
	{
		return ComObject::ConstructFromUserPointer<BlendState1>( pointer );
	}

	BlendState1^ BlendState1::FromDescription( Device1^ device, BlendStateDescription1 description )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
	
		ID3D10BlendState1* state = 0;
		D3D10_BLEND_DESC1 nativeDescription = description.CreateNativeVersion();
		
		if( RECORD_D3D10( device->InternalPointer->CreateBlendState1( &nativeDescription, &state ) ).IsFailure )
			return nullptr;
		
		return FromPointer( state );
	}

	BlendStateDescription1 BlendState1::Description::get()
	{
		D3D10_BLEND_DESC1 description;
		InternalPointer->GetDesc1( &description );
		return BlendStateDescription1( description );
	}
}
}
