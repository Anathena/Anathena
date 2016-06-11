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

#include <dxgi.h>

#include "OutputDescription.h"
#include "../Utilities.h"

using namespace System;

namespace SlimDX
{
namespace DXGI
{ 	
	OutputDescription::OutputDescription( const DXGI_OUTPUT_DESC& native )
	{
		m_DeviceName = gcnew String( native.DeviceName );
		m_DesktopCoordinates = Utilities::ConvertRect( native.DesktopCoordinates );
		m_AttachedToDesktop = native.AttachedToDesktop ? true : false;
		m_Rotation = static_cast<DisplayModeRotation>( native.Rotation );
		m_Monitor = IntPtr( native.Monitor );
	}

	String^ OutputDescription::Name::get()
	{
		return m_DeviceName;
	}

	Drawing::Rectangle OutputDescription::DesktopBounds::get()
	{
		return m_DesktopCoordinates;
	}

	bool OutputDescription::IsAttachedToDesktop::get()
	{
		return m_AttachedToDesktop;
	}

	DisplayModeRotation OutputDescription::Rotation::get()
	{
		return m_Rotation;
	}

	IntPtr OutputDescription::MonitorHandle::get()
	{
		return m_Monitor;
	}

	bool OutputDescription::operator == ( OutputDescription left, OutputDescription right )
	{
		return OutputDescription::Equals( left, right );
	}

	bool OutputDescription::operator != ( OutputDescription left, OutputDescription right )
	{
		return !OutputDescription::Equals( left, right );
	}

	int OutputDescription::GetHashCode()
	{
		return m_DeviceName->GetHashCode() + m_DesktopCoordinates.GetHashCode() + m_AttachedToDesktop.GetHashCode()
			 + m_Rotation.GetHashCode() + m_Monitor.GetHashCode();
	}

	bool OutputDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<OutputDescription>( value ) );
	}

	bool OutputDescription::Equals( OutputDescription value )
	{
		return ( m_DeviceName == value.m_DeviceName && m_DesktopCoordinates == value.m_DesktopCoordinates && m_AttachedToDesktop == value.m_AttachedToDesktop
			 && m_Rotation == value.m_Rotation && m_Monitor == value.m_Monitor );
	}

	bool OutputDescription::Equals( OutputDescription% value1, OutputDescription% value2 )
	{
		return ( value1.m_DeviceName == value2.m_DeviceName && value1.m_DesktopCoordinates == value2.m_DesktopCoordinates && value1.m_AttachedToDesktop == value2.m_AttachedToDesktop
			 && value1.m_Rotation == value2.m_Rotation && value1.m_Monitor == value2.m_Monitor );
	}
}
}
