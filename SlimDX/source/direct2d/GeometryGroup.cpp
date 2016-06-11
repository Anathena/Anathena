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
#include <vector>

#include "Direct2DException.h"

#include "RenderTarget.h"
#include "GeometryGroup.h"

const IID IID_ID2D1GeometryGroup = __uuidof(ID2D1GeometryGroup);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	GeometryGroup::GeometryGroup( SlimDX::Direct2D::Factory^ factory, SlimDX::Direct2D::FillMode fillMode, array<Geometry^>^ geometries )
	{
		ID2D1GeometryGroup *group = NULL;

		std::vector<ID2D1Geometry*> geos( geometries->Length );
		for( int i = 0; i < geometries->Length; i++ )
			geos[i] = geometries[i]->InternalPointer;

		HRESULT hr = factory->InternalPointer->CreateGeometryGroup( static_cast<D2D1_FILL_MODE>( fillMode ), &geos[0], geometries->Length, &group );
		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( group );
	}

	array<Geometry^>^ GeometryGroup::GetSourceGeometry()
	{
		int count = InternalPointer->GetSourceGeometryCount();
		std::vector<ID2D1Geometry*> geos( count );

		InternalPointer->GetSourceGeometries( &geos[0], count );

		array<Geometry^>^ results = gcnew array<Geometry^>( count );
		for( int i = 0; i < count; i++ )
			results[i] = Geometry::FromPointer( geos[i] );

		return results;
	}

	SlimDX::Direct2D::FillMode GeometryGroup::FillMode::get()
	{
		return static_cast<SlimDX::Direct2D::FillMode>( InternalPointer->GetFillMode() );
	}
}
}