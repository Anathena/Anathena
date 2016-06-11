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
#include <xinput.h>

#include "State.h"

using namespace System;

namespace SlimDX
{
namespace XInput
{
	State::State( const XINPUT_STATE& native )
	{
		m_PacketNumber = native.dwPacketNumber;
		m_Gamepad = XInput::Gamepad( native.Gamepad );
	}
	
	UInt32 State::PacketNumber::get()
	{
		return m_PacketNumber;
	}
	
	XInput::Gamepad State::Gamepad::get()
	{
		return m_Gamepad;
	}
	
	bool State::operator == ( State left, State right )
	{
		return State::Equals( left, right );
	}

	bool State::operator != ( State left, State right )
	{
		return !State::Equals( left, right );
	}

	int State::GetHashCode()
	{
		return m_PacketNumber.GetHashCode() + m_Gamepad.GetHashCode();
	}

	bool State::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<State>( value ) );
	}

	bool State::Equals( State value )
	{
		return ( m_PacketNumber == value.m_PacketNumber && m_Gamepad == value.m_Gamepad );
	}

	bool State::Equals( State% value1, State% value2 )
	{
		return ( value1.m_PacketNumber == value2.m_PacketNumber && value1.m_Gamepad == value2.m_Gamepad );
	}
}
}