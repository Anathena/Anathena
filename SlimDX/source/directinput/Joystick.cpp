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
#include "stdafx.h"

#include <windows.h>
#include <dinput.h>

#include "../stack_array.h"
#include "../ComObject.h"
#include "../Utilities.h"

#include "DirectInput.h"
#include "DirectInputException.h"

#include "Joystick.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Windows::Forms;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace DirectInput
{
	JoystickState^ CreateJoystickState( const DIDEVICEOBJECTDATA &objectData )
	{
		JoystickState^ result = gcnew JoystickState();

		switch( objectData.dwOfs )
		{
		case DIJOFS_RX:
			result->rx = objectData.dwData;
			break;

		case DIJOFS_RY:
			result->ry = objectData.dwData;
			break;

		case DIJOFS_RZ:
			result->rz = objectData.dwData;
			break;

		case DIJOFS_X:
			result->x = objectData.dwData;
			break;

		case DIJOFS_Y:
			result->y = objectData.dwData;
			break;

		case DIJOFS_Z:
			result->z = objectData.dwData;
			break;

		case DIJOFS_SLIDER( 0 ):
			result->sliders[0] = objectData.dwData;
			break;

		case DIJOFS_SLIDER( 1 ):
			result->sliders[1] = objectData.dwData;
			break;

		default:
			bool found = false;
			for( unsigned int i = 0; i < 4; i++ )
			{
				if( objectData.dwOfs == DIJOFS_POV( i ) )
				{
					result->povs[i] = objectData.dwData;
					found = true;
					break;
				}
			}

			if( !found )
			{
				for( unsigned int i = 0; i < 128; i++ )
				{
					if( objectData.dwOfs == DIJOFS_BUTTON( i ) )
					{
						if( objectData.dwData )
							result->pressedButtons[i] = true;
						else
							result->releasedButtons[i] = true;
						break;
					}
				}
			}
		}

		return result;
	}

	Joystick::Joystick( DirectInput^ directInput, Guid subsystem ) : Device( directInput, subsystem )
	{
		HRESULT hr = InternalPointer->SetDataFormat( &c_dfDIJoystick2 );
		if( RECORD_DINPUT( hr ).IsFailure )
			throw gcnew DirectInputException( Result::Last );
	}

	IList<JoystickState^>^ Joystick::GetBufferedData()
	{
		DWORD size = INFINITE;
		HRESULT hr = InternalPointer->GetDeviceData( sizeof( DIDEVICEOBJECTDATA ), NULL, &size, DIGDD_PEEK );
		if( RecordError( hr ).IsFailure )
			return nullptr;

		List<JoystickState^>^ list = gcnew List<JoystickState^>( size );
		if( size == 0 )
			return list;

		stack_array<DIDEVICEOBJECTDATA> data = stackalloc( DIDEVICEOBJECTDATA, size );
		hr = InternalPointer->GetDeviceData( sizeof( DIDEVICEOBJECTDATA ), &data[0], &size, 0 );
		if( RecordError( hr ).IsFailure )
			return nullptr;

		if( size == 0 )
			return list;

		for( unsigned int i = 0; i < size; i++ )
			list->Add( CreateJoystickState( data[i] ) );

		return list;
	}

	Result Joystick::GetCurrentState( JoystickState^% data )
	{
		DIJOYSTATE2 joystate;
		HRESULT hr = InternalPointer->GetDeviceState( sizeof( DIJOYSTATE2 ), &joystate );
		if( RecordError( hr ).IsFailure )
			return Result::Last;

		JoystickState^ state = safe_cast<JoystickState^>( data );
		state->AssignState(joystate);

		return Result::Last;
	}

	JoystickState^ Joystick::GetCurrentState()
	{
		JoystickState^ result = gcnew JoystickState();
		GetCurrentState( result );
		return result;
	}

	ObjectProperties^ Joystick::GetObjectPropertiesByName( String^ name )
	{
		return gcnew ObjectProperties( InternalPointer, name, JoystickState::typeid );
	}

	DeviceObjectInstance Joystick::GetObjectInfoByName( String^ name )
	{
		DIDEVICEOBJECTINSTANCE di;
		di.dwSize = sizeof( DIDEVICEOBJECTINSTANCE );

		HRESULT hr = InternalPointer->GetObjectInfo( &di, Marshal::OffsetOf( JoystickState::typeid, name ).ToInt32(), DIPH_BYUSAGE );
		if( RECORD_DINPUT( hr ).IsFailure )
			return DeviceObjectInstance();

		return DeviceObjectInstance( di );
	}
}
}
