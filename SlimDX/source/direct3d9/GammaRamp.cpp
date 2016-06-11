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

#include "GammaRamp.h"

using namespace System;
using namespace System::Collections::ObjectModel;

namespace SlimDX
{
namespace Direct3D9
{
	GammaRamp::GammaRamp()
	{
		red = gcnew Collection<UInt16>();
		green = gcnew Collection<UInt16>();
		blue = gcnew Collection<UInt16>();
	}

	GammaRamp::GammaRamp( const D3DGAMMARAMP &ramp )
	{
		red = gcnew Collection<UInt16>();
		green = gcnew Collection<UInt16>();
		blue = gcnew Collection<UInt16>();

		for( int i = 0; i < 256; i++ )
		{
			red->Add( ramp.red[i] );
			green->Add( ramp.green[i] );
			blue->Add( ramp.blue[i] );
		}
	}

	D3DGAMMARAMP GammaRamp::ToUnmanaged()
	{
		D3DGAMMARAMP result = D3DGAMMARAMP();

		for( int i = 0; i < 256; i++ )
		{
			result.red[i] = red[i];
			result.green[i] = green[i];
			result.blue[i] = blue[i];
		}

		return result;
	}
}
}