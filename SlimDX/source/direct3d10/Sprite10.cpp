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
#include <d3d10.h>
#include <d3dx10.h>

#include "../stack_array.h"

#include "Direct3D10Exception.h"

#include "Device10.h"
#include "Sprite10.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{
	Sprite::Sprite( SlimDX::Direct3D10::Device^ device, int bufferSize )
	{
		ID3DX10Sprite* sprite = 0;
		if( RECORD_D3D10( D3DX10CreateSprite( device->InternalPointer, bufferSize, &sprite ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
			
		Construct( sprite );
	}

	Matrix Sprite::ViewTransform::get()
	{
		D3DXMATRIX matrix;
		if( RECORD_D3D10( InternalPointer->GetViewTransform( &matrix ) ).IsFailure )
			return Matrix::Identity;
			
		return Matrix::FromD3DXMATRIX( matrix );
	}

	void Sprite::ViewTransform::set( Matrix value )
	{
		RECORD_D3D10( InternalPointer->SetViewTransform( reinterpret_cast<D3DXMATRIX*>( &value ) ) );
	}

	Matrix Sprite::ProjectionTransform::get()
	{
		D3DXMATRIX matrix;
		if( RECORD_D3D10( InternalPointer->GetProjectionTransform( &matrix ) ).IsFailure )
			return Matrix::Identity;
			
		return Matrix::FromD3DXMATRIX( matrix );
	}

	void Sprite::ProjectionTransform::set( Matrix value )
	{
		RECORD_D3D10( InternalPointer->SetProjectionTransform( reinterpret_cast<D3DXMATRIX*>( &value ) ) );
	}

	SlimDX::Direct3D10::Device^ Sprite::Device::get()
	{
		ID3D10Device* device = 0;
		if( RECORD_D3D10( InternalPointer->GetDevice( &device ) ).IsFailure )
			return nullptr;
		return SlimDX::Direct3D10::Device::FromPointer( device );
	}

	Result Sprite::Begin( SpriteFlags flags )
	{
		return RECORD_D3D10( InternalPointer->Begin( static_cast<DWORD>( flags ) ) );
	}

	Result Sprite::End()
	{
		return RECORD_D3D10( InternalPointer->End() );
	}

	Result Sprite::Flush()
	{
		return RECORD_D3D10( InternalPointer->Flush() );
	}

	Result Sprite::DrawBuffered( array<SpriteInstance^>^ instances )
	{
		stack_array<D3DX10_SPRITE> nativeInstances = stackalloc( D3DX10_SPRITE, instances->Length );
		for( int instanceIndex = 0; instanceIndex < instances->Length; ++instanceIndex )
			instances[instanceIndex]->ToNativeObject( nativeInstances[instanceIndex] );

		return RECORD_D3D10( InternalPointer->DrawSpritesBuffered( &nativeInstances[0], instances->Length ) );
	}

	Result Sprite::DrawImmediate( array<SpriteInstance^>^ instances )
	{
		stack_array<D3DX10_SPRITE> nativeInstances = stackalloc( D3DX10_SPRITE, instances->Length );
		for( int instanceIndex = 0; instanceIndex < instances->Length; ++instanceIndex )
			instances[instanceIndex]->ToNativeObject( nativeInstances[instanceIndex] );

		return RECORD_D3D10( InternalPointer->DrawSpritesImmediate( &nativeInstances[0], instances->Length, sizeof(D3DX10_SPRITE), 0 ) );
	}
}
}
