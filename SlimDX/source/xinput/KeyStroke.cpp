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
#include "Keystroke.h"

#include "XInputException.h"

namespace SlimDX
{
namespace XInput
{
	Keystroke::Keystroke( const XINPUT_KEYSTROKE &native )
	{
		m_VirtualKey = static_cast<GamepadKeyCode>( native.VirtualKey );
		m_Flags = static_cast<KeystrokeFlags>( native.Flags );
		m_UserIndex = static_cast<XInput::UserIndex>( native.UserIndex );
		m_HidCode = native.HidCode;
	}
	
	GamepadKeyCode Keystroke::VirtualKey::get()
	{
		return m_VirtualKey;
	}
	
	KeystrokeFlags Keystroke::Flags::get()
	{
		return m_Flags;
	}
	
	XInput::UserIndex Keystroke::UserIndex::get()
	{
		return m_UserIndex;
	}
	
	int Keystroke::HidCode::get()
	{
		return m_HidCode;
	}

	bool Keystroke::operator == ( Keystroke left, Keystroke right )
	{
		return Keystroke::Equals( left, right );
	}

	bool Keystroke::operator != ( Keystroke left, Keystroke right )
	{
		return !Keystroke::Equals( left, right );
	}

	int Keystroke::GetHashCode()
	{
		return m_VirtualKey.GetHashCode() + m_Flags.GetHashCode() + m_UserIndex.GetHashCode()
			 + m_HidCode.GetHashCode();
	}

	bool Keystroke::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Keystroke>( value ) );
	}

	bool Keystroke::Equals( Keystroke value )
	{
		return ( m_VirtualKey == value.m_VirtualKey && m_Flags == value.m_Flags && m_UserIndex == value.m_UserIndex
			 && m_HidCode == value.m_HidCode );
	}

	bool Keystroke::Equals( Keystroke% value1, Keystroke% value2 )
	{
		return ( value1.m_VirtualKey == value2.m_VirtualKey && value1.m_Flags == value2.m_Flags && value1.m_UserIndex == value2.m_UserIndex
			 && value1.m_HidCode == value2.m_HidCode );
	}
}
}