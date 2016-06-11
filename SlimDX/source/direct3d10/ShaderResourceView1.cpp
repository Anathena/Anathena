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

#include "Enums.h"
#include "Device10_1.h"
#include "Resource10.h"
#include "ShaderResourceView1.h"

using namespace System;
using namespace SlimDX::Direct3D10;

namespace SlimDX
{
namespace Direct3D10_1
{
	ShaderResourceView1::ShaderResourceView1( ID3D10ShaderResourceView1* pointer, ComObject^ owner )
		: ShaderResourceView( pointer, owner )
	{
	}
	
	ShaderResourceView1::ShaderResourceView1( IntPtr pointer )
		: ShaderResourceView( pointer )
	{
	}
	
	ShaderResourceView1^ ShaderResourceView1::FromPointer( ID3D10ShaderResourceView1* pointer, ComObject^ owner, ComObjectFlags flags )
	{
		return ComObject::ConstructFromPointer<ShaderResourceView1, ID3D10ShaderResourceView1>( pointer, owner, flags );
	}
	
	ShaderResourceView1^ ShaderResourceView1::FromPointer( IntPtr pointer )
	{
		return ComObject::ConstructFromUserPointer<ShaderResourceView1>( pointer );
	}

	ShaderResourceView1::ShaderResourceView1( Device1^ device, SlimDX::Direct3D10::Resource^ resource )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
		if( resource == nullptr )
			throw gcnew ArgumentNullException( "resource" );
		
		ID3D10ShaderResourceView1 *view = 0;
		if( RECORD_D3D10( device->InternalPointer->CreateShaderResourceView1( resource->InternalPointer, 0, &view ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
			
		Construct( view );
	}
	
	ShaderResourceView1::ShaderResourceView1( Device1^ device, SlimDX::Direct3D10::Resource^ resource, ShaderResourceViewDescription1 description )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
		if( resource == nullptr )
			throw gcnew ArgumentNullException( "resource" );
		
		ID3D10ShaderResourceView1 *view = 0;
		D3D10_SHADER_RESOURCE_VIEW_DESC1 nativeDescription = description.CreateNativeVersion();
		if( RECORD_D3D10( device->InternalPointer->CreateShaderResourceView1( resource->InternalPointer, &nativeDescription, &view ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
			
		Construct( view );
	}

	ShaderResourceViewDescription1 ShaderResourceView1::Description::get()
	{
		D3D10_SHADER_RESOURCE_VIEW_DESC1 nativeDescription;
		InternalPointer->GetDesc1( &nativeDescription );
		return ShaderResourceViewDescription1( nativeDescription );
	}
}
}
