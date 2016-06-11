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

#include <xaudio2.h>

#include "../multimedia/WaveFormat.h"
#include "../multimedia/WaveFormatExtensible.h"
#include "DeviceDetails.h"

using namespace System;
using namespace SlimDX::Multimedia;

namespace SlimDX
{
namespace XAudio2
{
	DeviceDetails::DeviceDetails( const XAUDIO2_DEVICE_DETAILS &details )
	{
		DeviceId = gcnew String( details.DeviceID );
		DisplayName = gcnew String( details.DisplayName );
		Role = static_cast<DeviceRole>( details.Role );
		OutputFormat = gcnew WaveFormatExtensible( details.OutputFormat );
	}

	DeviceDetails^ DeviceDetails::Clone()
	{
		DeviceDetails^ result = gcnew DeviceDetails();

		result->DeviceId = DeviceId;
		result->DisplayName = DisplayName;
		result->Role = Role;
		result->OutputFormat = OutputFormat;

		return result;
	}

	bool DeviceDetails::operator == ( DeviceDetails^ left, DeviceDetails^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return DeviceDetails::Equals( left, right );
	}

	bool DeviceDetails::operator != ( DeviceDetails^ left, DeviceDetails^ right )
	{
		return !( left == right );
	}

	int DeviceDetails::GetHashCode()
	{		
		return DeviceId->GetHashCode() + DisplayName->GetHashCode() + Role.GetHashCode() +
			OutputFormat->GetHashCode();
	}

	bool DeviceDetails::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<DeviceDetails^>( value ) );
	}

	bool DeviceDetails::Equals( DeviceDetails^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( DeviceId == value->DeviceId && DisplayName == value->DisplayName &&
			Role == value->Role && OutputFormat == value->OutputFormat );
	}

	bool DeviceDetails::Equals( DeviceDetails^ value1, DeviceDetails^ value2 )
	{
		return value1->Equals( value2 );
	}
}
}