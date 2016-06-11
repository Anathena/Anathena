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
#include <d3dx11.h>

#include "../DataStream.h"

#include "Direct3D11Exception.h"

#include "Buffer11.h"
#include "BufferDescription11.h"
#include "Device11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	Buffer::Buffer( SlimDX::Direct3D11::Device^ device, BufferDescription description )
	{
		Construct( Build( device, nullptr, description.SizeInBytes, description.Usage, description.BindFlags, description.CpuAccessFlags, description.OptionFlags, description.StructureByteStride ) );
	}

	Buffer::Buffer( SlimDX::Direct3D11::Device^ device, DataStream^ data, BufferDescription description )
	{
		Construct( Build( device, data, description.SizeInBytes, description.Usage, description.BindFlags, description.CpuAccessFlags, description.OptionFlags, description.StructureByteStride ) );
	}
	
	Buffer::Buffer( SlimDX::Direct3D11::Device^ device, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D11::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags, int structureByteStride )
	{
		Construct( Build( device, nullptr, sizeInBytes, usage, bindFlags, accessFlags, optionFlags, structureByteStride ) );
	}
	
	Buffer::Buffer( SlimDX::Direct3D11::Device^ device, DataStream^ data, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D11::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags, int structureByteStride )
	{		
		Construct( Build( device, data, sizeInBytes, usage, bindFlags, accessFlags, optionFlags, structureByteStride ) );
	}

	ID3D11Buffer* Buffer::Build( SlimDX::Direct3D11::Device^ device, DataStream^ data, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D11::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags, int structureByteStride )
	{
		D3D11_BUFFER_DESC description;
		ZeroMemory( &description, sizeof( description ) );
		description.ByteWidth = sizeInBytes;
		description.Usage = static_cast<D3D11_USAGE>( usage );
		description.BindFlags = static_cast<UINT>( bindFlags );
		description.CPUAccessFlags = static_cast<UINT>( accessFlags );
		description.MiscFlags = static_cast<UINT>( optionFlags );
		description.StructureByteStride = structureByteStride;
		
		ID3D11Buffer* buffer = 0;
		if( data == nullptr )
		{
			if( RECORD_D3D11( device->InternalPointer->CreateBuffer( &description, 0, &buffer ) ).IsFailure )
				throw gcnew Direct3D11Exception( Result::Last );
		}
		else
		{
			D3D11_SUBRESOURCE_DATA initialData;
			ZeroMemory( &initialData, sizeof( initialData ) );
			initialData.pSysMem = data->PositionPointer;
			data->Position += sizeInBytes;
			
			if( RECORD_D3D11( device->InternalPointer->CreateBuffer( &description, &initialData, &buffer ) ).IsFailure )
				throw gcnew Direct3D11Exception( Result::Last );
		}
		
		return buffer;
	}
	
	BufferDescription Buffer::Description::get()
	{
		D3D11_BUFFER_DESC description;
		InternalPointer->GetDesc( &description );
		return BufferDescription( description );
	}
}
}
