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
#include <d3d9.h>
#include <d3dx9.h>

#include "../ComObject.h"

#include "../math/Matrix.h"
#include "../math/Vector3.h"

#include "Direct3D9Exception.h"

#include "MatrixStack.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	MatrixStack::MatrixStack()
	{
		ID3DXMatrixStack* matrixStack;
		HRESULT hr = D3DXCreateMatrixStack( 0, &matrixStack );
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( "Failed to create MatrixStack." );

		Construct(matrixStack);
	}

	Result MatrixStack::Push()
	{
		HRESULT hr = InternalPointer->Push();
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::Pop()
	{
		HRESULT hr = InternalPointer->Pop();
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::LoadIdentity()
	{
		HRESULT hr = InternalPointer->LoadIdentity();
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::LoadMatrix( Matrix matrix )
	{
		HRESULT hr = InternalPointer->LoadMatrix( reinterpret_cast<D3DXMATRIX*>( &matrix ) );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::MultiplyMatrix( Matrix matrix )
	{
		HRESULT hr = InternalPointer->MultMatrix( reinterpret_cast<D3DXMATRIX*>( &matrix ) );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::MultiplyMatrixLocal( Matrix matrix )
	{
		HRESULT hr = InternalPointer->MultMatrixLocal( reinterpret_cast<D3DXMATRIX*>( &matrix ) );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::RotateAxis( Vector3 axis, float angle )
	{
		HRESULT hr = InternalPointer->RotateAxis( reinterpret_cast<D3DXVECTOR3*>( &axis ), angle );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::RotateAxisLocal( Vector3 axis, float angle )
	{
		HRESULT hr = InternalPointer->RotateAxisLocal( reinterpret_cast<D3DXVECTOR3*>( &axis ), angle );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::RotateYawPitchRoll( float yaw, float pitch, float roll )
	{
		HRESULT hr = InternalPointer->RotateYawPitchRoll( yaw, pitch, roll );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::RotateYawPitchRollLocal( float yaw, float pitch, float roll )
	{
		HRESULT hr = InternalPointer->RotateYawPitchRollLocal( yaw, pitch, roll );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::Scale( float x, float y, float z )
	{
		HRESULT hr = InternalPointer->Scale( x, y, z );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::ScaleLocal( float x, float y, float z )
	{
		HRESULT hr = InternalPointer->ScaleLocal( x, y, z );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::Translate( float x, float y, float z )
	{
		HRESULT hr = InternalPointer->Translate( x, y, z );
		return RECORD_D3D9( hr );
	}

	Result MatrixStack::TranslateLocal( float x, float y, float z )
	{
		HRESULT hr = InternalPointer->TranslateLocal( x, y, z );
		return RECORD_D3D9( hr );
	}

	Matrix MatrixStack::Top::get()
	{
		D3DXMATRIX* top = InternalPointer->GetTop();
		Matrix result;
		*reinterpret_cast<D3DXMATRIX*>( &result ) = *top;

		return result;
	}
}
}
