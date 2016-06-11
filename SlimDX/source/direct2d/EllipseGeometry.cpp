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

#include <d2d1.h>
#include <d2d1helper.h>

#include "Direct2DException.h"

#include "RenderTarget.h"
#include "EllipseGeometry.h"

const IID IID_ID2D1EllipseGeometry = __uuidof(ID2D1EllipseGeometry);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	EllipseGeometry::EllipseGeometry( SlimDX::Direct2D::Factory^ factory, SlimDX::Direct2D::Ellipse ellipse )
	{
		ID2D1EllipseGeometry *geometry = NULL;

		HRESULT hr = factory->InternalPointer->CreateEllipseGeometry( reinterpret_cast<D2D1_ELLIPSE*>( &ellipse ), &geometry );
		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( geometry );
	}

	SlimDX::Direct2D::Ellipse EllipseGeometry::Ellipse::get()
	{
		SlimDX::Direct2D::Ellipse result;

		InternalPointer->GetEllipse( reinterpret_cast<D2D1_ELLIPSE*>( &result ) );
		return result;
	}
}
}