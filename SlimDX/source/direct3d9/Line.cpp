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
#include <windows.h>
#include <d3d9.h>
#include <d3dx9.h>

#include "../ComObject.h"

#include "../math/Color4.h"
#include "../math/Matrix.h"
#include "../math/Vector2.h"
#include "../math/Vector3.h"

#include "Direct3D9Exception.h"

#include "Device.h"
#include "Line.h"

using namespace System;
using namespace System::Drawing;

namespace SlimDX
{
namespace Direct3D9
{
	Line::Line( SlimDX::Direct3D9::Device^ device )
	{
		ID3DXLine* line;

		HRESULT hr = D3DXCreateLine( device->InternalPointer, &line );
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(line);
	}

	Result Line::Begin()
	{
		HRESULT hr = InternalPointer->Begin();
		return RECORD_D3D9( hr );
	}

	Result Line::End()
	{
		HRESULT hr = InternalPointer->End();
		return RECORD_D3D9( hr );
	}

	Result Line::Draw( array<Vector2>^ vertexList, Color4 color )
	{
		pin_ptr<Vector2> pinnedVerts = &vertexList[0];

		HRESULT hr = InternalPointer->Draw( reinterpret_cast<D3DXVECTOR2*>( pinnedVerts ), vertexList->Length, color.ToArgb() );
		return RECORD_D3D9( hr );
	}
	
	Result Line::DrawTransformed( array<Vector3>^ vertexList, Matrix transform, Color4 color )
	{
		pin_ptr<Vector3> pinnedVerts = &vertexList[0];

		HRESULT hr = InternalPointer->DrawTransform( reinterpret_cast<D3DXVECTOR3*>( pinnedVerts ), vertexList->Length, reinterpret_cast<const D3DXMATRIX*>( &transform ), color.ToArgb() );
		return RECORD_D3D9( hr );
	}
	
	Result Line::OnLostDevice()
	{
		HRESULT hr = InternalPointer->OnLostDevice();
		return RECORD_D3D9( hr );
	}

	Result Line::OnResetDevice()
	{
		HRESULT hr = InternalPointer->OnResetDevice();
		return RECORD_D3D9( hr );
	}

	SlimDX::Direct3D9::Device^ Line::Device::get()
	{
		IDirect3DDevice9* device;
		HRESULT hr = InternalPointer->GetDevice( &device );
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return SlimDX::Direct3D9::Device::FromPointer( device );
	}

	bool Line::Antialias::get()
	{
		return InternalPointer->GetAntialias() > 0;
	}

	void Line::Antialias::set( bool value )
	{
		HRESULT hr = InternalPointer->SetAntialias( value );
		RECORD_D3D9( hr );
	}

	bool Line::GLLines::get()
	{
		return InternalPointer->GetGLLines() > 0;
	}

	void Line::GLLines::set( bool value )
	{
		HRESULT hr = InternalPointer->SetGLLines( value );
		RECORD_D3D9( hr );
	}

	int Line::Pattern::get()
	{
		return InternalPointer->GetPattern();
	}

	void Line::Pattern::set( int value )
	{
		HRESULT hr = InternalPointer->SetPattern( value );
		RECORD_D3D9( hr );
	}

	float Line::PatternScale::get()
	{
		return InternalPointer->GetPatternScale();
	}

	void Line::PatternScale::set( float value )
	{
		HRESULT hr = InternalPointer->SetPatternScale( value );
		RECORD_D3D9( hr );
	}

	float Line::Width::get()
	{
		return InternalPointer->GetWidth();
	}

	void Line::Width::set( float value )
	{
		HRESULT hr = InternalPointer->SetWidth( value );
		RECORD_D3D9( hr );
	}

}
}