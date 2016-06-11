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

#include "TessellationSink.h"

const IID IID_ID2D1TessellationSink = __uuidof(ID2D1TessellationSink);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	Result TessellationSink::Close()
	{
		return RECORD_D2D( InternalPointer->Close() );
	}

	void TessellationSink::AddTriangle( Triangle triangle )
	{
		InternalPointer->AddTriangles( reinterpret_cast<D2D1_TRIANGLE*>( &triangle ), 1 );
	}

	void TessellationSink::AddTriangles( array<Triangle>^ triangles )
	{
		AddTriangles( triangles, 0, triangles->Length );
	}

	void TessellationSink::AddTriangles( array<Triangle>^ triangles, int startOffset, int count )
	{
		pin_ptr<Triangle> pinnedTris = &triangles[startOffset];

		InternalPointer->AddTriangles( reinterpret_cast<D2D1_TRIANGLE*>( pinnedTris ), count );
	}
}
}