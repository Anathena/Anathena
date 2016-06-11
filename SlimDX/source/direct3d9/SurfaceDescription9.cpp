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

#include "SurfaceDescription9.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool SurfaceDescription::operator == ( SurfaceDescription left, SurfaceDescription right )
	{
		return SurfaceDescription::Equals( left, right );
	}

	bool SurfaceDescription::operator != ( SurfaceDescription left, SurfaceDescription right )
	{
		return !SurfaceDescription::Equals( left, right );
	}

	int SurfaceDescription::GetHashCode()
	{
		return Format.GetHashCode() + Type.GetHashCode() + Usage.GetHashCode()
			 + Pool.GetHashCode() + MultisampleType.GetHashCode() + MultisampleQuality.GetHashCode()
			 + Width.GetHashCode() + Height.GetHashCode();
	}

	bool SurfaceDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<SurfaceDescription>( value ) );
	}

	bool SurfaceDescription::Equals( SurfaceDescription value )
	{
		return ( Format == value.Format && Type == value.Type && Usage == value.Usage
			 && Pool == value.Pool && MultisampleType == value.MultisampleType && MultisampleQuality == value.MultisampleQuality
			 && Width == value.Width && Height == value.Height );
	}

	bool SurfaceDescription::Equals( SurfaceDescription% value1, SurfaceDescription% value2 )
	{
		return ( value1.Format == value2.Format && value1.Type == value2.Type && value1.Usage == value2.Usage
			 && value1.Pool == value2.Pool && value1.MultisampleType == value2.MultisampleType && value1.MultisampleQuality == value2.MultisampleQuality
			 && value1.Width == value2.Width && value1.Height == value2.Height );
	}
}
}