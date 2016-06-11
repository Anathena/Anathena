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

#include "RenderToEnvMapDescription.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool RenderToEnvironmentMapDescription::operator == ( RenderToEnvironmentMapDescription left, RenderToEnvironmentMapDescription right )
	{
		return RenderToEnvironmentMapDescription::Equals( left, right );
	}

	bool RenderToEnvironmentMapDescription::operator != ( RenderToEnvironmentMapDescription left, RenderToEnvironmentMapDescription right )
	{
		return !RenderToEnvironmentMapDescription::Equals( left, right );
	}

	int RenderToEnvironmentMapDescription::GetHashCode()
	{
		return Size.GetHashCode() + MipLevels.GetHashCode() + Format.GetHashCode()
			 + DepthStencil.GetHashCode() + DepthStencilFormat.GetHashCode();
	}

	bool RenderToEnvironmentMapDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RenderToEnvironmentMapDescription>( value ) );
	}

	bool RenderToEnvironmentMapDescription::Equals( RenderToEnvironmentMapDescription value )
	{
		return ( Size == value.Size && MipLevels == value.MipLevels && Format == value.Format
			 && DepthStencil == value.DepthStencil && DepthStencilFormat == value.DepthStencilFormat );
	}

	bool RenderToEnvironmentMapDescription::Equals( RenderToEnvironmentMapDescription% value1, RenderToEnvironmentMapDescription% value2 )
	{
		return ( value1.Size == value2.Size && value1.MipLevels == value2.MipLevels && value1.Format == value2.Format
			 && value1.DepthStencil == value2.DepthStencil && value1.DepthStencilFormat == value2.DepthStencilFormat );
	}
}
}