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

#include "Buffer11.h"
#include "SamplerState11.h"
#include "ShaderResourceView11.h"
#include "VertexShaderWrapper11.h"
#include "VertexShader11.h"
#include "ClassInstance11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	VertexShaderWrapper::VertexShaderWrapper( ID3D11DeviceContext* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "device" );
		deviceContext = device;
	}

	void VertexShaderWrapper::Set( VertexShader^ shader )
	{
		Set( shader, nullptr );
	}

	void VertexShaderWrapper::Set( VertexShader^ shader, array<ClassInstance^>^ classInstances )
	{
		ID3D11VertexShader *nativeShader = shader == nullptr ? NULL : shader->InternalPointer;
		ID3D11ClassInstance** instancePtr = NULL;
		stack_array<ID3D11ClassInstance*> instances;
		UINT count = 0;

		if( classInstances != nullptr && classInstances->Length > 0 )
		{
			instances = stack_array<ID3D11ClassInstance*>( classInstances->Length );
			instancePtr = &instances[0];
			count = classInstances->Length;

			for( int i = 0; i < classInstances->Length; i++ )
				instances[i] = classInstances[i]->InternalPointer;
		}

		deviceContext->VSSetShader( nativeShader, instancePtr, count );
	}

	VertexShader^ VertexShaderWrapper::Get()
	{
		array<ClassInstance^>^ dummy;
		return Get( 0, dummy );
	}

	VertexShader^ VertexShaderWrapper::Get( int count, array<ClassInstance^>^ %classInstances )
	{
		ID3D11VertexShader *shader = NULL;
		stack_array<ID3D11ClassInstance*> instances = stackalloc(ID3D11ClassInstance*, count);
		UINT uCount = count;

		deviceContext->VSGetShader( &shader, &instances[0], &uCount );

		classInstances = gcnew array<ClassInstance^>(uCount);
		for( UINT i = 0; i < uCount; i++ )
			classInstances[i] = ClassInstance::FromPointer( instances[i] );

		return shader == NULL ? nullptr : VertexShader::FromPointer( shader );
	}

	array<Buffer^>^ VertexShaderWrapper::GetConstantBuffers( int startSlot, int count )
	{
		array<Buffer^>^ buffers = gcnew array<Buffer^>( count );
		stack_array<ID3D11Buffer*> results = stackalloc( ID3D11Buffer*, count );

		deviceContext->VSGetConstantBuffers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			buffers[i] = Buffer::FromPointer( results[i] );

		return buffers;
	}

	array<SamplerState^>^ VertexShaderWrapper::GetSamplers( int startSlot, int count )
	{
		array<SamplerState^>^ samplers = gcnew array<SamplerState^>( count );
		stack_array<ID3D11SamplerState*> results = stackalloc( ID3D11SamplerState*, count );

		deviceContext->VSGetSamplers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			samplers[i] = SamplerState::FromPointer( results[i] );

		return samplers;
	}

	array<ShaderResourceView^>^ VertexShaderWrapper::GetShaderResources( int startSlot, int count )
	{
		array<ShaderResourceView^>^ resources = gcnew array<ShaderResourceView^>( count );
		stack_array<ID3D11ShaderResourceView*> results = stackalloc( ID3D11ShaderResourceView*, count );

		deviceContext->VSGetShaderResources( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			resources[i] = ShaderResourceView::FromPointer( results[i] );

		return resources;
	}

	void VertexShaderWrapper::SetConstantBuffer( Buffer^ constantBuffer, int slot )
	{
		ID3D11Buffer *buffer = constantBuffer == nullptr ? NULL : constantBuffer->InternalPointer;
		deviceContext->VSSetConstantBuffers( slot, 1, &buffer );
	}

	void VertexShaderWrapper::SetConstantBuffers( array<Buffer^>^ constantBuffers, int startSlot, int count )
	{
		if( count > constantBuffers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11Buffer*> input = stackalloc( ID3D11Buffer*, count );
		for( int i = 0; i < count; i++ )
			input[i] = constantBuffers[i] == nullptr ? NULL : constantBuffers[i]->InternalPointer;

		deviceContext->VSSetConstantBuffers( startSlot, count, &input[0] );
	}

	void VertexShaderWrapper::SetSampler( SamplerState^ sampler, int slot )
	{
		ID3D11SamplerState *pointer = sampler == nullptr ? NULL : sampler->InternalPointer;
		deviceContext->VSSetSamplers( slot, 1, &pointer );
	}

	void VertexShaderWrapper::SetSamplers( array<SamplerState^>^ samplers, int startSlot, int count )
	{
		if( count > samplers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11SamplerState*> input = stackalloc( ID3D11SamplerState*, count );
		for( int i = 0; i < count; i++ )
			input[i] = samplers[i] == nullptr ? NULL : samplers[i]->InternalPointer;

		deviceContext->VSSetSamplers( startSlot, count, &input[0] );
	}

	void VertexShaderWrapper::SetShaderResource( ShaderResourceView^ resourceView, int slot )
	{
		ID3D11ShaderResourceView *resource = resourceView == nullptr ? NULL : resourceView->InternalPointer;
		deviceContext->VSSetShaderResources( slot, 1, &resource );
	}

	void VertexShaderWrapper::SetShaderResources( array<ShaderResourceView^>^ resourceViews, int startSlot, int count )
	{
		if( count > resourceViews->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11ShaderResourceView*> input = stackalloc( ID3D11ShaderResourceView*, count );
		for( int i = 0; i < count; i++ )
			input[i] = resourceViews[i] == nullptr ? NULL : resourceViews[i]->InternalPointer;

		deviceContext->VSSetShaderResources( startSlot, count, &input[0] );
	}
}
}
