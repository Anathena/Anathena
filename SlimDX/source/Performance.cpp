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

#include "math/Color4.h"
#include "Performance.h"
#include <vcclr.h>

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
	int Performance::BeginEvent( Color4 color, String^ name )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( name );
		return D3DPERF_BeginEvent( static_cast<int>( color ), pinnedName );
	}

	int Performance::EndEvent()
	{
		return D3DPERF_EndEvent();
	}

	bool Performance::GetStatus()
	{
		return D3DPERF_GetStatus() > 0;
	}

	bool Performance::QueryRepeatFrame()
	{
		return D3DPERF_QueryRepeatFrame() > 0;
	}

	void Performance::SetMarker( Color4 color, String^ name )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( name );
		D3DPERF_SetMarker( static_cast<int>( color ), pinnedName );
	}

	void Performance::SetOptions( PerformanceOptions options )
	{
		D3DPERF_SetOptions( static_cast<DWORD>( options ) );
	}

	void Performance::SetRegion( Color4 color, String^ name )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( name );
		D3DPERF_SetRegion( static_cast<int>( color ), pinnedName );
	}
}
