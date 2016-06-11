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
#include <windows.h>
#include <dinput.h>

#include "MouseState.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	MouseState::MouseState()
	{
		pressedButtons = gcnew array<bool>( 8 );
		releasedButtons = gcnew array<bool>( 8 );
	}

	MouseState::MouseState( int x, int y, int z )
	{
		X = x;
		Y = y;
		Z = z;
		pressedButtons = gcnew array<bool>( 8 );
		releasedButtons = gcnew array<bool>( 8 );
	}

	MouseState::MouseState( const DIMOUSESTATE2 &state )
	{
		pressedButtons = gcnew array<bool>( 8 );
		releasedButtons = gcnew array<bool>( 8 );
		AssignState( state );
	}

	void MouseState::AssignState( const DIMOUSESTATE2 &state ) 
	{ 
		X = state.lX;
		Y = state.lY;
		Z = state.lZ;

		for( int i = 0; i < 8; i++ )
		{
			if( ( state.rgbButtons[i] & 0x80 ) != 0 )
			{
				pressedButtons[i] = true;
				releasedButtons[i] = false;
			}
			else
			{
				pressedButtons[i] = false;
				releasedButtons[i] = true;
			}
		}
	}
}
}
