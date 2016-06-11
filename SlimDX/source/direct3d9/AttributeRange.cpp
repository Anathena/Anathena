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

#include "AttributeRange.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool AttributeRange::operator == ( AttributeRange left, AttributeRange right )
	{
		return AttributeRange::Equals( left, right );
	}

	bool AttributeRange::operator != ( AttributeRange left, AttributeRange right )
	{
		return !AttributeRange::Equals( left, right );
	}

	int AttributeRange::GetHashCode()
	{
		return AttribId.GetHashCode() + FaceStart.GetHashCode() + FaceCount.GetHashCode()
			 + VertexStart.GetHashCode() + VertexCount.GetHashCode();
	}

	bool AttributeRange::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<AttributeRange>( value ) );
	}

	bool AttributeRange::Equals( AttributeRange value )
	{
		return ( AttribId == value.AttribId && FaceStart == value.FaceStart && FaceCount == value.FaceCount
			 && VertexStart == value.VertexStart && VertexCount == value.VertexCount );
	}

	bool AttributeRange::Equals( AttributeRange% value1, AttributeRange% value2 )
	{
		return ( value1.AttribId == value2.AttribId && value1.FaceStart == value2.FaceStart && value1.FaceCount == value2.FaceCount
			 && value1.VertexStart == value2.VertexStart && value1.VertexCount == value2.VertexCount );
	}
}
}