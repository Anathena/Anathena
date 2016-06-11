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

#include "Buffer.h"
#include "BufferDescription.h"
#include "Device10.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	Buffer::Buffer( SlimDX::Direct3D10::Device^ device, BufferDescription description )
	{
		Construct( Build( device, nullptr, description.SizeInBytes, description.Usage, description.BindFlags, description.CpuAccessFlags, description.OptionFlags ) );
	}

	Buffer::Buffer( SlimDX::Direct3D10::Device^ device, DataStream^ data, BufferDescription description )
	{
		Construct( Build( device, data, description.SizeInBytes, description.Usage, description.BindFlags, description.CpuAccessFlags, description.OptionFlags ) );
	}
	
	Buffer::Buffer( SlimDX::Direct3D10::Device^ device, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D10::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags )
	{
		Construct( Build( device, nullptr, sizeInBytes, usage, bindFlags, accessFlags, optionFlags ) );
	}
	
	Buffer::Buffer( SlimDX::Direct3D10::Device^ device, DataStream^ data, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D10::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags )
	{		
		Construct( Build( device, data, sizeInBytes, usage, bindFlags, accessFlags, optionFlags ) );
	}

	ID3D10Buffer* Buffer::Build( SlimDX::Direct3D10::Device^ device, DataStream^ data, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D10::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags )
	{
		D3D10_BUFFER_DESC description;
		ZeroMemory( &description, sizeof( description ) );
		description.ByteWidth = sizeInBytes;
		description.Usage = static_cast<D3D10_USAGE>( usage );
		description.BindFlags = static_cast<UINT>( bindFlags );
		description.CPUAccessFlags = static_cast<UINT>( accessFlags );
		description.MiscFlags = static_cast<UINT>( optionFlags );
		
		ID3D10Buffer* buffer = 0;
		if( data == nullptr )
		{
			if( RECORD_D3D10( device->InternalPointer->CreateBuffer( &description, 0, &buffer ) ).IsFailure )
				throw gcnew Direct3D10Exception( Result::Last );
		}
		else
		{
			D3D10_SUBRESOURCE_DATA initialData;
			ZeroMemory( &initialData, sizeof( initialData ) );
			initialData.pSysMem = data->PositionPointer;
			data->Position += sizeInBytes;
			
			if( RECORD_D3D10( device->InternalPointer->CreateBuffer( &description, &initialData, &buffer ) ).IsFailure )
				throw gcnew Direct3D10Exception( Result::Last );
		}
		
		return buffer;
	}
	
	BufferDescription Buffer::Description::get()
	{
		D3D10_BUFFER_DESC description;
		InternalPointer->GetDesc( &description );
		return BufferDescription( description );
	}
	
	DataStream^ Buffer::Map( MapMode mode, MapFlags flags )
	{
		void* mappedPtr = 0;
		if( RECORD_D3D10( InternalPointer->Map( static_cast<D3D10_MAP>( mode ), static_cast<UINT>( flags ), &mappedPtr ) ).IsFailure )
			return nullptr;
		
		return gcnew DataStream( mappedPtr, Description.SizeInBytes, true, true, false );
	}

	void Buffer::Unmap()
	{
		InternalPointer->Unmap();
	}
}
}
