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
#include <dinput.h>

#include "CapabilitiesDI.h"
#include "Guids.h"
#include "DeviceSubType.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	Capabilities::Capabilities( const DIDEVCAPS &caps )
	{
		axesCount = caps.dwAxes;
		buttonCount = caps.dwButtons;
		povCount = caps.dwPOVs;
		ffSamplePeriod = caps.dwFFSamplePeriod;
		ffMinTimeResolution = caps.dwFFMinTimeResolution;
		ffDriverVersion = caps.dwFFDriverVersion;
		firmwareRevision = caps.dwFirmwareRevision;
		hardwareRevision = caps.dwHardwareRevision;
		flags = static_cast<DeviceFlags>( caps.dwFlags );
		type = static_cast<DeviceType>( caps.dwDevType );
		subType = caps.dwDevType >> 8;

		if( ( caps.dwDevType & DIDEVTYPE_HID ) != 0 )
			hid = true;
		else
			hid = false;
	}
}
}