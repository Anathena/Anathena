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
#include <dsound.h>

#include "../Utilities.h"

#include "CaptureEffectDescription.h"

namespace SlimDX
{
namespace DirectSound
{
	DSCEFFECTDESC CaptureEffectDescription::Marshal()
	{
		DSCEFFECTDESC description;
		ZeroMemory( &description, sizeof(description) );
		description.dwSize = sizeof( DSCEFFECTDESC );
		description.guidDSCFXClass = Utilities::ConvertManagedGuid( CaptureEffectClass );
		description.guidDSCFXInstance = Utilities::ConvertManagedGuid( CaptureEffectInstance );
		description.dwFlags = DSCFX_LOCSOFTWARE;
		description.dwReserved1 = 0;
		description.dwReserved2 = 0;

		if( LocateInHardware )
			description.dwFlags = DSCFX_LOCHARDWARE;

		return description;
	}

	bool CaptureEffectDescription::operator == ( CaptureEffectDescription left, CaptureEffectDescription right )
	{
		return CaptureEffectDescription::Equals( left, right );
	}

	bool CaptureEffectDescription::operator != ( CaptureEffectDescription left, CaptureEffectDescription right )
	{
		return !CaptureEffectDescription::Equals( left, right );
	}

	int CaptureEffectDescription::GetHashCode()
	{
		return LocateInSoftware.GetHashCode() + LocateInHardware.GetHashCode() + CaptureEffectInstance.GetHashCode() + CaptureEffectClass.GetHashCode();
	}

	bool CaptureEffectDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CaptureEffectDescription>( value ) );
	}

	bool CaptureEffectDescription::Equals( CaptureEffectDescription value )
	{
		return ( LocateInSoftware == value.LocateInSoftware && LocateInHardware == value.LocateInHardware && 
			CaptureEffectInstance == value.CaptureEffectInstance && CaptureEffectClass == value.CaptureEffectClass );
	}

	bool CaptureEffectDescription::Equals( CaptureEffectDescription% value1, CaptureEffectDescription% value2 )
	{
		return value1.Equals( value2 );
	}
}
}