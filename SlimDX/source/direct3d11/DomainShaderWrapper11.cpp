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
#include "DomainShaderWrapper11.h"
#include "DomainShader11.h"
#include "ClassInstance11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	DomainShaderWrapper::DomainShaderWrapper( ID3D11DeviceContext* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "deviceContext" );
		deviceContext = device;
	}

	void DomainShaderWrapper::Set( DomainShader^ shader )
	{
		Set( shader, nullptr );
	}

	void DomainShaderWrapper::Set( DomainShader^ shader, array<ClassInstance^>^ classInstances )
	{
		ID3D11DomainShader *nativeShader = shader == nullptr ? NULL : shader->InternalPointer;
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

		deviceContext->DSSetShader( nativeShader, instancePtr, count );
	}

	DomainShader^ DomainShaderWrapper::Get()
	{
		array<ClassInstance^>^ dummy;
		return Get( 0, dummy );
	}

	DomainShader^ DomainShaderWrapper::Get( int count, array<ClassInstance^>^ %classInstances )
	{
		ID3D11DomainShader *shader = NULL;
		stack_array<ID3D11ClassInstance*> instances = stackalloc(ID3D11ClassInstance*, count);
		UINT uCount = count;

		deviceContext->DSGetShader( &shader, &instances[0], &uCount );

		classInstances = gcnew array<ClassInstance^>(uCount);
		for( UINT i = 0; i < uCount; i++ )
			classInstances[i] = ClassInstance::FromPointer( instances[i] );

		return shader == NULL ? nullptr : DomainShader::FromPointer( shader );
	}

	array<Buffer^>^ DomainShaderWrapper::GetConstantBuffers( int startSlot, int count )
	{
		array<Buffer^>^ buffers = gcnew array<Buffer^>( count );
		stack_array<ID3D11Buffer*> results = stackalloc( ID3D11Buffer*, count );

		deviceContext->DSGetConstantBuffers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			buffers[i] = Buffer::FromPointer( results[i] );

		return buffers;
	}

	array<SamplerState^>^ DomainShaderWrapper::GetSamplers( int startSlot, int count )
	{
		array<SamplerState^>^ samplers = gcnew array<SamplerState^>( count );
		stack_array<ID3D11SamplerState*> results = stackalloc( ID3D11SamplerState*, count );

		deviceContext->DSGetSamplers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			samplers[i] = SamplerState::FromPointer( results[i] );

		return samplers;
	}

	array<ShaderResourceView^>^ DomainShaderWrapper::GetShaderResources( int startSlot, int count )
	{
		array<ShaderResourceView^>^ resources = gcnew array<ShaderResourceView^>( count );
		stack_array<ID3D11ShaderResourceView*> results = stackalloc( ID3D11ShaderResourceView*, count );

		deviceContext->DSGetShaderResources( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			resources[i] = ShaderResourceView::FromPointer( results[i] );

		return resources;
	}

	void DomainShaderWrapper::SetConstantBuffer( Buffer^ constantBuffer, int slot )
	{
		ID3D11Buffer *buffer = constantBuffer == nullptr ? NULL : constantBuffer->InternalPointer;
		deviceContext->DSSetConstantBuffers( slot, 1, &buffer );
	}

	void DomainShaderWrapper::SetConstantBuffers( array<Buffer^>^ constantBuffers, int startSlot, int count )
	{
		if( count > constantBuffers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11Buffer*> input = stackalloc( ID3D11Buffer*, count );
		for( int i = 0; i < count; i++ )
			input[i] = constantBuffers[i] == nullptr ? NULL : constantBuffers[i]->InternalPointer;

		deviceContext->DSSetConstantBuffers( startSlot, count, &input[0] );
	}

	void DomainShaderWrapper::SetSampler( SamplerState^ sampler, int slot )
	{
		ID3D11SamplerState *pointer = sampler == nullptr ? NULL : sampler->InternalPointer;
		deviceContext->DSSetSamplers( slot, 1, &pointer );
	}

	void DomainShaderWrapper::SetSamplers( array<SamplerState^>^ samplers, int startSlot, int count )
	{
		if( count > samplers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11SamplerState*> input = stackalloc( ID3D11SamplerState*, count );
		for( int i = 0; i < count; i++ )
			input[i] = samplers[i] == nullptr ? NULL : samplers[i]->InternalPointer;

		deviceContext->DSSetSamplers( startSlot, count, &input[0] );
	}

	void DomainShaderWrapper::SetShaderResource( ShaderResourceView^ resourceView, int slot )
	{
		ID3D11ShaderResourceView *resource = resourceView == nullptr ? NULL : resourceView->InternalPointer;
		deviceContext->DSSetShaderResources( slot, 1, &resource );
	}

	void DomainShaderWrapper::SetShaderResources( array<ShaderResourceView^>^ resourceViews, int startSlot, int count )
	{
		if( count > resourceViews->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11ShaderResourceView*> input = stackalloc( ID3D11ShaderResourceView*, count );
		for( int i = 0; i < count; i++ )
			input[i] = resourceViews[i] == nullptr ? NULL : resourceViews[i]->InternalPointer;

		deviceContext->DSSetShaderResources( startSlot, count, &input[0] );
	}
}
}
