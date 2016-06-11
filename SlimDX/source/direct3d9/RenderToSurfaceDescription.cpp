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

#include "RenderToSurfaceDescription.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool RenderToSurfaceDescription::operator == ( RenderToSurfaceDescription left, RenderToSurfaceDescription right )
	{
		return RenderToSurfaceDescription::Equals( left, right );
	}

	bool RenderToSurfaceDescription::operator != ( RenderToSurfaceDescription left, RenderToSurfaceDescription right )
	{
		return !RenderToSurfaceDescription::Equals( left, right );
	}

	int RenderToSurfaceDescription::GetHashCode()
	{
		return Width.GetHashCode() + Height.GetHashCode() + Format.GetHashCode()
			 + DepthStencil.GetHashCode() + DepthStencilFormat.GetHashCode();
	}

	bool RenderToSurfaceDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RenderToSurfaceDescription>( value ) );
	}

	bool RenderToSurfaceDescription::Equals( RenderToSurfaceDescription value )
	{
		return ( Width == value.Width && Height == value.Height && Format == value.Format
			 && DepthStencil == value.DepthStencil && DepthStencilFormat == value.DepthStencilFormat );
	}

	bool RenderToSurfaceDescription::Equals( RenderToSurfaceDescription% value1, RenderToSurfaceDescription% value2 )
	{
		return ( value1.Width == value2.Width && value1.Height == value2.Height && value1.Format == value2.Format
			 && value1.DepthStencil == value2.DepthStencil && value1.DepthStencilFormat == value2.DepthStencilFormat );
	}
}
}