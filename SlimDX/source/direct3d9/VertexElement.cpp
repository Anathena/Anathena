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

#include "VertexElement.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	VertexElement::VertexElement( short stream, short offset, DeclarationType declarationType, DeclarationMethod declarationMethod, DeclarationUsage declarationUsage, System::Byte usageIndex )
	{
		Stream = stream;
		Offset = offset;
		Type = declarationType;
		Method = declarationMethod;
		Usage = declarationUsage;
		UsageIndex = usageIndex;
	}

	bool VertexElement::operator == ( VertexElement left, VertexElement right )
	{
		return VertexElement::Equals( left, right );
	}

	bool VertexElement::operator != ( VertexElement left, VertexElement right )
	{
		return !VertexElement::Equals( left, right );
	}

	int VertexElement::GetHashCode()
	{
		return Stream.GetHashCode() + Offset.GetHashCode() + Type.GetHashCode()
			 + Method.GetHashCode() + Usage.GetHashCode() + UsageIndex.GetHashCode();
	}

	bool VertexElement::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<VertexElement>( value ) );
	}

	bool VertexElement::Equals( VertexElement value )
	{
		return ( Stream == value.Stream && Offset == value.Offset && Type == value.Type
			 && Method == value.Method && Usage == value.Usage && UsageIndex == value.UsageIndex );
	}

	bool VertexElement::Equals( VertexElement% value1, VertexElement% value2 )
	{
		return ( value1.Stream == value2.Stream && value1.Offset == value2.Offset && value1.Type == value2.Type
			 && value1.Method == value2.Method && value1.Usage == value2.Usage && value1.UsageIndex == value2.UsageIndex );
	}
}
}