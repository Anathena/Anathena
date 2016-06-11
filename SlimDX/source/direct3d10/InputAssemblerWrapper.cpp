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

#include "Buffer.h"
#include "InputAssemblerWrapper.h"
#include "InputLayout.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	InputAssemblerWrapper::InputAssemblerWrapper( ID3D10Device* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "device" );
		m_Device = device;
	}

	void InputAssemblerWrapper::SetInputLayout( InputLayout^ value)
	{
		if( value == nullptr )
		{
			m_Device->IASetInputLayout( 0 );
		} else
		{
			m_Device->IASetInputLayout( value->InternalPointer );
		}
	}

	InputLayout^ InputAssemblerWrapper::GetInputLayout()
	{
		ID3D10InputLayout* result = 0;
		m_Device->IAGetInputLayout( &result );
		if( result == 0 )
			return nullptr;
		
		return InputLayout::FromPointer( result );
	}
	
	void InputAssemblerWrapper::SetPrimitiveTopology( PrimitiveTopology value)
	{
		m_Device->IASetPrimitiveTopology( static_cast<D3D10_PRIMITIVE_TOPOLOGY>( value ) );
	}

	PrimitiveTopology InputAssemblerWrapper::GetPrimitiveTopology()
	{
		D3D10_PRIMITIVE_TOPOLOGY result = D3D_PRIMITIVE_TOPOLOGY_UNDEFINED;
		m_Device->IAGetPrimitiveTopology( &result );
		return static_cast<PrimitiveTopology>( result );
	}
	
	void InputAssemblerWrapper::SetIndexBuffer( Buffer^ indexBuffer, DXGI::Format format, int offset )
	{
		if( indexBuffer == nullptr )
		{
			m_Device->IASetIndexBuffer( 0, DXGI_FORMAT_UNKNOWN, 0 );
		}
		else
		{
			m_Device->IASetIndexBuffer( static_cast<ID3D10Buffer*>( indexBuffer->InternalPointer ), static_cast<DXGI_FORMAT>( format ), offset );
		}
	}

	void InputAssemblerWrapper::GetIndexBuffer( [Out] Buffer^ %indexBuffer, [Out] DXGI::Format %format, [Out] int %offset )
	{
		ID3D10Buffer *buffer;
		DXGI_FORMAT nativeFormat;
		UINT nativeOffset;

		m_Device->IAGetIndexBuffer( &buffer, &nativeFormat, &nativeOffset );

		indexBuffer = Buffer::FromPointer( buffer );
		format = static_cast<DXGI::Format>( nativeFormat );
		offset = nativeOffset;
	}
	
	void InputAssemblerWrapper::SetVertexBuffers( int slot, VertexBufferBinding vertexBufferBinding )
	{
		ID3D10Buffer* buffers[] = { static_cast<ID3D10Buffer*>( vertexBufferBinding.Buffer == nullptr ? 0 : vertexBufferBinding.Buffer->InternalPointer ) };
		UINT strides[] = { vertexBufferBinding.Stride };
		UINT offsets[] = { vertexBufferBinding.Offset };
		
		m_Device->IASetVertexBuffers( slot, 1, buffers, strides, offsets );
	}
	
	void InputAssemblerWrapper::SetVertexBuffers( int firstSlot, ... array<VertexBufferBinding>^ vertexBufferBinding )
	{
		ID3D10Buffer* buffers[D3D10_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT];
		UINT strides[D3D10_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT];
		UINT offsets[D3D10_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT];
		
		for( int i = 0; i < vertexBufferBinding->Length; ++i )
		{
			buffers[i] = vertexBufferBinding[ i ].Buffer == nullptr ? 0 : static_cast<ID3D10Buffer*>( vertexBufferBinding[ i ].Buffer->InternalPointer );
			strides[i] = vertexBufferBinding[ i ].Stride;
			offsets[i] = vertexBufferBinding[ i ].Offset;
		}
		
		m_Device->IASetVertexBuffers( firstSlot, vertexBufferBinding->Length, buffers, strides, offsets );
	}

	array<VertexBufferBinding>^ InputAssemblerWrapper::GetVertexBuffers( int firstSlot, int count )
	{
		ID3D10Buffer* buffers[D3D10_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT];
		UINT strides[D3D10_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT];
		UINT offsets[D3D10_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT];
		array<VertexBufferBinding>^ results = gcnew array<VertexBufferBinding>( count );
		
		m_Device->IAGetVertexBuffers( firstSlot, count, buffers, strides, offsets );

		for( int i = 0; i < count; ++i )
		{
			results[i].Buffer = buffers[i] == NULL ? nullptr : Buffer::FromPointer( buffers[i] );
			results[i].Stride = strides[i];
			results[i].Offset = offsets[i];
		}

		return results;
	}
}
}
