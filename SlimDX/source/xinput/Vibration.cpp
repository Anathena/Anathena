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

#include "Vibration.h"

using namespace System;

namespace SlimDX
{
namespace XInput
{
	Vibration::Vibration( const XINPUT_VIBRATION& native )
	{
		m_LeftMotorSpeed = native.wLeftMotorSpeed;
		m_RightMotorSpeed = native.wRightMotorSpeed;
	}
	
	XINPUT_VIBRATION Vibration::CreateNativeVersion()
	{
		XINPUT_VIBRATION native;
		native.wLeftMotorSpeed = m_LeftMotorSpeed;
		native.wRightMotorSpeed = m_RightMotorSpeed;
		
		return native;
	}
	
	UInt16 Vibration::LeftMotorSpeed::get()
	{
		return m_LeftMotorSpeed;
	}
	
	void Vibration::LeftMotorSpeed::set( UInt16 value )
	{
		m_LeftMotorSpeed = value;
	}
	
	UInt16 Vibration::RightMotorSpeed::get()
	{
		return m_RightMotorSpeed;
	}
	
	void Vibration::RightMotorSpeed::set( UInt16 value )
	{
		m_RightMotorSpeed = value;
	}
	
	bool Vibration::operator == ( Vibration left, Vibration right )
	{
		return Vibration::Equals( left, right );
	}

	bool Vibration::operator != ( Vibration left, Vibration right )
	{
		return !Vibration::Equals( left, right );
	}

	int Vibration::GetHashCode()
	{
		return m_LeftMotorSpeed.GetHashCode() + m_RightMotorSpeed.GetHashCode();
	}

	bool Vibration::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Vibration>( value ) );
	}

	bool Vibration::Equals( Vibration value )
	{
		return ( m_LeftMotorSpeed == value.m_LeftMotorSpeed && m_RightMotorSpeed == value.m_RightMotorSpeed );
	}

	bool Vibration::Equals( Vibration% value1, Vibration% value2 )
	{
		return ( value1.m_LeftMotorSpeed == value2.m_LeftMotorSpeed && value1.m_RightMotorSpeed == value2.m_RightMotorSpeed );
	}
}
}