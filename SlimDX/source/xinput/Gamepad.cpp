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
#include "Gamepad.h"

#include "XInputException.h"

namespace SlimDX
{
namespace XInput
{
	Gamepad::Gamepad( const XINPUT_GAMEPAD &native )
	{
		m_Buttons = static_cast<GamepadButtonFlags>( native.wButtons );
		leftTrigger = native.bLeftTrigger;
		rightTrigger = native.bRightTrigger;
		leftThumbX = native.sThumbLX;
		leftThumbY = native.sThumbLY;
		rightThumbX = native.sThumbRX;
		rightThumbY = native.sThumbRY;
	}
	
	GamepadButtonFlags Gamepad::Buttons::get()
	{
		return m_Buttons;
	}

	bool Gamepad::operator == ( Gamepad left, Gamepad right )
	{
		return Gamepad::Equals( left, right );
	}

	bool Gamepad::operator != ( Gamepad left, Gamepad right )
	{
		return !Gamepad::Equals( left, right );
	}

	int Gamepad::GetHashCode()
	{
		return m_Buttons.GetHashCode() + leftTrigger.GetHashCode() + rightTrigger.GetHashCode()
			 + leftThumbX.GetHashCode() + leftThumbY.GetHashCode() + rightThumbX.GetHashCode()
			 + rightThumbY.GetHashCode();
	}

	bool Gamepad::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Gamepad>( value ) );
	}

	bool Gamepad::Equals( Gamepad value )
	{
		return ( m_Buttons == value.m_Buttons && leftTrigger == value.leftTrigger && rightTrigger == value.rightTrigger
			 && leftThumbX == value.leftThumbX && leftThumbY == value.leftThumbY && rightThumbX == value.rightThumbX
			 && rightThumbY == value.rightThumbY );
	}

	bool Gamepad::Equals( Gamepad% value1, Gamepad% value2 )
	{
		return ( value1.m_Buttons == value2.m_Buttons && value1.leftTrigger == value2.leftTrigger && value1.rightTrigger == value2.rightTrigger
			 && value1.leftThumbX == value2.leftThumbX && value1.leftThumbY == value2.leftThumbY && value1.rightThumbX == value2.rightThumbX
			 && value1.rightThumbY == value2.rightThumbY );
	}
}
}