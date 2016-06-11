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

#include "PatchInfo.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool TrianglePatchInfo::operator == ( TrianglePatchInfo left, TrianglePatchInfo right )
	{
		return TrianglePatchInfo::Equals( left, right );
	}

	bool TrianglePatchInfo::operator != ( TrianglePatchInfo left, TrianglePatchInfo right )
	{
		return !TrianglePatchInfo::Equals( left, right );
	}

	int TrianglePatchInfo::GetHashCode()
	{
		return StartVertexOffset.GetHashCode() + VertexCount.GetHashCode() + Basis.GetHashCode()
			 + Degree.GetHashCode();
	}

	bool TrianglePatchInfo::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<TrianglePatchInfo>( value ) );
	}

	bool TrianglePatchInfo::Equals( TrianglePatchInfo value )
	{
		return ( StartVertexOffset == value.StartVertexOffset && VertexCount == value.VertexCount && Basis == value.Basis
			 && Degree == value.Degree );
	}

	bool TrianglePatchInfo::Equals( TrianglePatchInfo% value1, TrianglePatchInfo% value2 )
	{
		return ( value1.StartVertexOffset == value2.StartVertexOffset && value1.VertexCount == value2.VertexCount && value1.Basis == value2.Basis
			 && value1.Degree == value2.Degree );
	}

	bool RectanglePatchInfo::operator == ( RectanglePatchInfo left, RectanglePatchInfo right )
	{
		return RectanglePatchInfo::Equals( left, right );
	}

	bool RectanglePatchInfo::operator != ( RectanglePatchInfo left, RectanglePatchInfo right )
	{
		return !RectanglePatchInfo::Equals( left, right );
	}

	int RectanglePatchInfo::GetHashCode()
	{
		return StartVertexOffsetWidth.GetHashCode() + StartVertexOffsetHeight.GetHashCode() + Width.GetHashCode()
			 + Height.GetHashCode() + Stride.GetHashCode() + Basis.GetHashCode()
			 + Degree.GetHashCode();
	}

	bool RectanglePatchInfo::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RectanglePatchInfo>( value ) );
	}

	bool RectanglePatchInfo::Equals( RectanglePatchInfo value )
	{
		return ( StartVertexOffsetWidth == value.StartVertexOffsetWidth && StartVertexOffsetHeight == value.StartVertexOffsetHeight && Width == value.Width
			 && Height == value.Height && Stride == value.Stride && Basis == value.Basis
			 && Degree == value.Degree );
	}

	bool RectanglePatchInfo::Equals( RectanglePatchInfo% value1, RectanglePatchInfo% value2 )
	{
		return ( value1.StartVertexOffsetWidth == value2.StartVertexOffsetWidth && value1.StartVertexOffsetHeight == value2.StartVertexOffsetHeight && value1.Width == value2.Width
			 && value1.Height == value2.Height && value1.Stride == value2.Stride && value1.Basis == value2.Basis
			 && value1.Degree == value2.Degree );
	}
}
}