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
#include "stdafx.h"

#include "../stack_array.h"

#include "Buffer.h"
#include "SamplerState.h"
#include "ShaderResourceView.h"
#include "VertexShaderWrapper.h"
#include "VertexShader10.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	VertexShaderWrapper::VertexShaderWrapper( ID3D10Device* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "device" );
		m_Device = device;
	}

	void VertexShaderWrapper::Set( VertexShader^ shader )
	{
		m_Device->VSSetShader( shader == nullptr ? 0 : shader->InternalPointer );
	}

	VertexShader^ VertexShaderWrapper::Get()
	{
		ID3D10VertexShader* shader = 0;
		m_Device->VSGetShader( &shader );

		return shader == 0 ? nullptr : VertexShader::FromPointer( shader );
	}

	array<Buffer^>^ VertexShaderWrapper::GetConstantBuffers( int startSlot, int count )
	{
		array<Buffer^>^ buffers = gcnew array<Buffer^>( count );
		stack_array<ID3D10Buffer*> results = stackalloc( ID3D10Buffer*, count );

		m_Device->VSGetConstantBuffers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			buffers[i] = Buffer::FromPointer( results[i] );

		return buffers;
	}

	array<SamplerState^>^ VertexShaderWrapper::GetSamplers( int startSlot, int count )
	{
		array<SamplerState^>^ samplers = gcnew array<SamplerState^>( count );
		stack_array<ID3D10SamplerState*> results = stackalloc( ID3D10SamplerState*, count );

		m_Device->VSGetSamplers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			samplers[i] = SamplerState::FromPointer( results[i] );

		return samplers;
	}

	array<ShaderResourceView^>^ VertexShaderWrapper::GetShaderResources( int startSlot, int count )
	{
		array<ShaderResourceView^>^ resources = gcnew array<ShaderResourceView^>( count );
		stack_array<ID3D10ShaderResourceView*> results = stackalloc( ID3D10ShaderResourceView*, count );

		m_Device->VSGetShaderResources( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			resources[i] = ShaderResourceView::FromPointer( results[i] );

		return resources;
	}

	void VertexShaderWrapper::SetConstantBuffer( Buffer^ constantBuffer, int slot )
	{
		ID3D10Buffer *buffer = constantBuffer == nullptr ? NULL : constantBuffer->InternalPointer;
		m_Device->VSSetConstantBuffers( slot, 1, &buffer );
	}

	void VertexShaderWrapper::SetConstantBuffers( array<Buffer^>^ constantBuffers, int startSlot, int count )
	{
		if( count > constantBuffers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D10Buffer*> input = stackalloc( ID3D10Buffer*, count );
		for( int i = 0; i < count; i++ )
			input[i] = constantBuffers[i] == nullptr ? NULL : constantBuffers[i]->InternalPointer;

		m_Device->VSSetConstantBuffers( startSlot, count, &input[0] );
	}

	void VertexShaderWrapper::SetSampler( SamplerState^ sampler, int slot )
	{
		ID3D10SamplerState *pointer = sampler == nullptr ? NULL : sampler->InternalPointer;
		m_Device->VSSetSamplers( slot, 1, &pointer );
	}

	void VertexShaderWrapper::SetSamplers( array<SamplerState^>^ samplers, int startSlot, int count )
	{
		if( count > samplers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D10SamplerState*> input = stackalloc( ID3D10SamplerState*, count );
		for( int i = 0; i < count; i++ )
			input[i] = samplers[i] == nullptr ? NULL : samplers[i]->InternalPointer;

		m_Device->VSSetSamplers( startSlot, count, &input[0] );
	}

	void VertexShaderWrapper::SetShaderResource( ShaderResourceView^ resourceView, int slot )
	{
		ID3D10ShaderResourceView *resource = resourceView == nullptr ? NULL : resourceView->InternalPointer;
		m_Device->VSSetShaderResources( slot, 1, &resource );
	}

	void VertexShaderWrapper::SetShaderResources( array<ShaderResourceView^>^ resourceViews, int startSlot, int count )
	{
		if( count > resourceViews->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D10ShaderResourceView*> input = stackalloc( ID3D10ShaderResourceView*, count );
		for( int i = 0; i < count; i++ )
			input[i] = resourceViews[i] == nullptr ? NULL : resourceViews[i]->InternalPointer;

		m_Device->VSSetShaderResources( startSlot, count, &input[0] );
	}
}
}
