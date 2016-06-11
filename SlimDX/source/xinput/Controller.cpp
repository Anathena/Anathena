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
#include "Controller.h"

#include "../DirectInput/Guids.h"

#include "XInputException.h"

#include "ResultCodeXI.h"

using namespace System;

namespace SlimDX
{
namespace XInput
{
	Controller::Controller( UserIndex userIndex )
	{
		m_UserIndex = static_cast<System::UInt32>( userIndex );

		if (m_UserIndex > 3)
			throw gcnew XInputException("Controllers must specify a valid user index from 0 to 3.");
	}
	
	bool Controller::IsConnected::get()
	{
		XINPUT_STATE state;
		UInt32 result = XInputGetState( m_UserIndex, &state );

		if( result == ERROR_DEVICE_NOT_CONNECTED )
			return false;
		return true;
	}
	
	Guid Controller::SoundRenderGuid::get()
	{
		GUID renderGuid;
		GUID captureGuid;
		RECORD_XINPUT( XInputGetDSoundAudioDeviceGuids( m_UserIndex, &renderGuid, &captureGuid ) );
		
		return Utilities::ConvertNativeGuid( renderGuid );
	}

	Guid Controller::SoundCaptureGuid::get()
	{
		GUID renderGuid;
		GUID captureGuid;
		RECORD_XINPUT( XInputGetDSoundAudioDeviceGuids( m_UserIndex, &renderGuid, &captureGuid ) );

		return Utilities::ConvertNativeGuid( captureGuid );
	}
	
	BatteryInformation Controller::GetBatteryInformation( BatteryDeviceType battery )
	{
		XINPUT_BATTERY_INFORMATION information;
		if( RECORD_XINPUT( ConvertError( XInputGetBatteryInformation( m_UserIndex, static_cast<BYTE>( battery ), &information) ) ).IsFailure )
			return BatteryInformation();
		
		return BatteryInformation( information );
	}
	
	Capabilities Controller::GetCapabilities( DeviceQueryType device )
	{
		XINPUT_CAPABILITIES capabilities;
		if( RECORD_XINPUT( ConvertError( XInputGetCapabilities( m_UserIndex, static_cast<DWORD>( device ), &capabilities) ) ).IsFailure )
			return Capabilities();
		
		return Capabilities( capabilities );
	}
	
	State Controller::GetState()
	{
		XINPUT_STATE state;
		if( RECORD_XINPUT( ConvertError( XInputGetState( m_UserIndex, &state ) ) ).IsFailure )
			return State();
	
		return State( state ); 
	}
	
	Result Controller::GetKeystroke( DeviceQueryType device, Keystroke% result )
	{
		XINPUT_KEYSTROKE keystroke;
		if( RECORD_XINPUT( ConvertError( XInputGetKeystroke( m_UserIndex, static_cast<DWORD>( device ), &keystroke ) ) ).IsSuccess )
			result = Keystroke( keystroke );
		
		return Result::Last;
	}
	
	Result Controller::SetVibration( Vibration vibration )
	{
		XINPUT_VIBRATION nativeVibration = vibration.CreateNativeVersion();
		return RECORD_XINPUT( ConvertError( XInputSetState( m_UserIndex, &nativeVibration ) ) );
	}
	
	void Controller::SetReporting( bool value )
	{
		XInputEnable( value );
	}

	int Controller::ConvertError( int errorCode )
	{
		return HRESULT_FROM_WIN32( errorCode );
	}
}
}
