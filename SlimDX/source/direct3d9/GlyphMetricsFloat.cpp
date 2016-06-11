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

#include "GlyphMetricsFloat.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool GlyphMetricsFloat::operator == ( GlyphMetricsFloat left, GlyphMetricsFloat right )
	{
		return GlyphMetricsFloat::Equals( left, right );
	}

	bool GlyphMetricsFloat::operator != ( GlyphMetricsFloat left, GlyphMetricsFloat right )
	{
		return !GlyphMetricsFloat::Equals( left, right );
	}

	int GlyphMetricsFloat::GetHashCode()
	{
		return BlackBoxX.GetHashCode() + BlackBoxY.GetHashCode() + GlyphOrigin.GetHashCode()
			 + CellIncX.GetHashCode() + CellIncY.GetHashCode();
	}

	bool GlyphMetricsFloat::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<GlyphMetricsFloat>( value ) );
	}

	bool GlyphMetricsFloat::Equals( GlyphMetricsFloat value )
	{
		return ( BlackBoxX == value.BlackBoxX && BlackBoxY == value.BlackBoxY && GlyphOrigin == value.GlyphOrigin
			 && CellIncX == value.CellIncX && CellIncY == value.CellIncY );
	}

	bool GlyphMetricsFloat::Equals( GlyphMetricsFloat% value1, GlyphMetricsFloat% value2 )
	{
		return ( value1.BlackBoxX == value2.BlackBoxX && value1.BlackBoxY == value2.BlackBoxY && value1.GlyphOrigin == value2.GlyphOrigin
			 && value1.CellIncX == value2.CellIncX && value1.CellIncY == value2.CellIncY );
	}
}
}