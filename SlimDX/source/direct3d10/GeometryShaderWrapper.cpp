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
#include "GeometryShaderWrapper.h"
#include "GeometryShader.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	GeometryShaderWrapper::GeometryShaderWrapper( ID3D10Device* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "device" );
		m_Device = device;
	}

	void GeometryShaderWrapper::Set( GeometryShader^ shader )
	{
		m_Device->GSSetShader( shader == nullptr ? 0 : shader->InternalPointer );
	}

	GeometryShader^ GeometryShaderWrapper::Get()
	{
		ID3D10GeometryShader* shader = 0;
		m_Device->GSGetShader( &shader );

		return shader == 0 ? nullptr : GeometryShader::FromPointer( shader );
	}

	array<Buffer^>^ GeometryShaderWrapper::GetConstantBuffers( int startSlot, int count )
	{
		array<Buffer^>^ buffers = gcnew array<Buffer^>( count );
		stack_array<ID3D10Buffer*> results = stackalloc( ID3D10Buffer*, count );

		m_Device->GSGetConstantBuffers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			buffers[i] = Buffer::FromPointer( results[i] );

		return buffers;
	}

	array<SamplerState^>^ GeometryShaderWrapper::GetSamplers( int startSlot, int count )
	{
		array<SamplerState^>^ samplers = gcnew array<SamplerState^>( count );
		stack_array<ID3D10SamplerState*> results = stackalloc( ID3D10SamplerState*, count );

		m_Device->GSGetSamplers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			samplers[i] = SamplerState::FromPointer( results[i] );

		return samplers;
	}

	array<ShaderResourceView^>^ GeometryShaderWrapper::GetShaderResources( int startSlot, int count )
	{
		array<ShaderResourceView^>^ resources = gcnew array<ShaderResourceView^>( count );
		stack_array<ID3D10ShaderResourceView*> results = stackalloc( ID3D10ShaderResourceView*, count );

		m_Device->GSGetShaderResources( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			resources[i] = ShaderResourceView::FromPointer( results[i] );

		return resources;
	}

	void GeometryShaderWrapper::SetConstantBuffer( Buffer^ constantBuffer, int slot )
	{
		ID3D10Buffer *buffer = constantBuffer == nullptr ? NULL : constantBuffer->InternalPointer;
		m_Device->GSSetConstantBuffers( slot, 1, &buffer );
	}

	void GeometryShaderWrapper::SetConstantBuffers( array<Buffer^>^ constantBuffers, int startSlot, int count )
	{
		if( count > constantBuffers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D10Buffer*> input = stackalloc( ID3D10Buffer*, count );
		for( int i = 0; i < count; i++ )
			input[i] = constantBuffers[i] == nullptr ? NULL : constantBuffers[i]->InternalPointer;

		m_Device->GSSetConstantBuffers( startSlot, count, &input[0] );
	}

	void GeometryShaderWrapper::SetSampler( SamplerState^ sampler, int slot )
	{
		ID3D10SamplerState *pointer = sampler == nullptr ? NULL : sampler->InternalPointer;
		m_Device->GSSetSamplers( slot, 1, &pointer );
	}

	void GeometryShaderWrapper::SetSamplers( array<SamplerState^>^ samplers, int startSlot, int count )
	{
		if( count > samplers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D10SamplerState*> input = stackalloc( ID3D10SamplerState*, count );
		for( int i = 0; i < count; i++ )
			input[i] = samplers[i] == nullptr ? NULL : samplers[i]->InternalPointer;

		m_Device->GSSetSamplers( startSlot, count, &input[0] );
	}

	void GeometryShaderWrapper::SetShaderResource( ShaderResourceView^ resourceView, int slot )
	{
		ID3D10ShaderResourceView *resource = resourceView == nullptr ? NULL : resourceView->InternalPointer;
		m_Device->GSSetShaderResources( slot, 1, &resource );
	}

	void GeometryShaderWrapper::SetShaderResources( array<ShaderResourceView^>^ resourceViews, int startSlot, int count )
	{
		if( count > resourceViews->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D10ShaderResourceView*> input = stackalloc( ID3D10ShaderResourceView*, count );
		for( int i = 0; i < count; i++ )
			input[i] = resourceViews[i] == nullptr ? NULL : resourceViews[i]->InternalPointer;

		m_Device->GSSetShaderResources( startSlot, count, &input[0] );
	}
}
}
