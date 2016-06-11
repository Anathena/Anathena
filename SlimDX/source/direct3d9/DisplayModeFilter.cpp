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

#include "DisplayModeFilter.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	void DisplayModeFilter::ToUnmanaged( D3DDISPLAYMODEFILTER& filter )
	{
		filter.Size = sizeof(D3DDISPLAYMODEFILTER);
		filter.Format = static_cast<D3DFORMAT>( Format );
		filter.ScanLineOrdering = static_cast<D3DSCANLINEORDERING>( ScanlineOrdering );
	}

	bool DisplayModeFilter::operator == ( DisplayModeFilter left, DisplayModeFilter right )
	{
		return DisplayModeFilter::Equals( left, right );
	}

	bool DisplayModeFilter::operator != ( DisplayModeFilter left, DisplayModeFilter right )
	{
		return !DisplayModeFilter::Equals( left, right );
	}

	int DisplayModeFilter::GetHashCode()
	{
		return Format.GetHashCode() + ScanlineOrdering.GetHashCode();
	}

	bool DisplayModeFilter::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<DisplayModeFilter>( value ) );
	}

	bool DisplayModeFilter::Equals( DisplayModeFilter value )
	{
		return ( Format == value.Format && ScanlineOrdering == value.ScanlineOrdering );
	}

	bool DisplayModeFilter::Equals( DisplayModeFilter% value1, DisplayModeFilter% value2 )
	{
		return ( value1.Format == value2.Format && value1.ScanlineOrdering == value2.ScanlineOrdering );
	}
}
}