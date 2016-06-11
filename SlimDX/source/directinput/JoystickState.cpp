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

#include "../ComObject.h"
#include "../Utilities.h"

#include "DirectInput.h"

#include "DeviceDI.h"
#include "JoystickState.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	JoystickState::JoystickState()
	{
		sliders = gcnew array<int>( 2 );
		povs = gcnew array<int>( 4 );
		pressedButtons = gcnew array<bool>( 128 );
		releasedButtons = gcnew array<bool>( 128 );
		vsliders = gcnew array<int>( 2 );
		fsliders = gcnew array<int>( 2 );
		asliders = gcnew array<int>( 2 );
	}

	JoystickState::JoystickState( const DIJOYSTATE2 &joystate )
	{
		sliders = gcnew array<int>( 2 );
		povs = gcnew array<int>( 4 );
		pressedButtons = gcnew array<bool>( 128 );
		releasedButtons = gcnew array<bool>( 128 );
		vsliders = gcnew array<int>( 2 );
		fsliders = gcnew array<int>( 2 );
		asliders = gcnew array<int>( 2 );

		AssignState(joystate);
	}

	void JoystickState::AssignState(const DIJOYSTATE2 &joystate)
	{
		x = joystate.lX;
		y = joystate.lY;
		z = joystate.lZ;
		rx = joystate.lRx;
		ry = joystate.lRy;
		rz = joystate.lRz;
		vx = joystate.lVX;
		vy = joystate.lVY;
		vz = joystate.lVZ;
		vrx = joystate.lVRx;
		vry = joystate.lVRy;
		vrz = joystate.lVRz;
		ax = joystate.lAX;
		ay = joystate.lAY;
		az = joystate.lAZ;
		arx = joystate.lARx;
		ary = joystate.lARy;
		arz = joystate.lARz;
		fx = joystate.lFX;
		fy = joystate.lFY;
		fz = joystate.lFZ;
		frx = joystate.lFRx;
		fry = joystate.lFRy;
		frz = joystate.lFRz;

		for( int i = 0; i < 2; i++ )
		{
			sliders[i] = joystate.rglSlider[i];
			asliders[i] = joystate.rglASlider[i];
			vsliders[i] = joystate.rglVSlider[i];
			fsliders[i] = joystate.rglVSlider[i];
		}

		for( int i = 0; i < 4; i++ )
			povs[i] = joystate.rgdwPOV[i];

		for( int i = 0; i < 128; i++ )
		{
			if( joystate.rgbButtons[i] )
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
