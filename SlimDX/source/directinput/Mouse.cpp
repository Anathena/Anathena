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

#include "Mouse.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Windows::Forms;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace DirectInput
{
	MouseState^ CreateMouseState( const DIDEVICEOBJECTDATA &objectData )
	{
		MouseState^ result = gcnew MouseState( 0, 0, 0 );

		switch( objectData.dwOfs )
		{
		case DIMOFS_BUTTON0:
			if( objectData.dwData )
				result->pressedButtons[0] = true;
			else
				result->releasedButtons[0] = true;
			break;

		case DIMOFS_BUTTON1:
			if( objectData.dwData )
				result->pressedButtons[1] = true;
			else
				result->releasedButtons[1] = true;
			break;

		case DIMOFS_BUTTON2:
			if( objectData.dwData )
				result->pressedButtons[2] = true;
			else
				result->releasedButtons[2] = true;
			break;

		case DIMOFS_BUTTON3:
			if( objectData.dwData )
				result->pressedButtons[3] = true;
			else
				result->releasedButtons[3] = true;
			break;

		case DIMOFS_BUTTON4:
			if( objectData.dwData )
				result->pressedButtons[4] = true;
			else
				result->releasedButtons[4] = true;
			break;

		case DIMOFS_BUTTON5:
			if( objectData.dwData )
				result->pressedButtons[5] = true;
			else
				result->releasedButtons[5] = true;
			break;

		case DIMOFS_BUTTON6:
			if( objectData.dwData )
				result->pressedButtons[6] = true;
			else
				result->releasedButtons[6] = true;
			break;

		case DIMOFS_BUTTON7:
			if( objectData.dwData )
				result->pressedButtons[7] = true;
			else
				result->releasedButtons[7] = true;
			break;

		case DIMOFS_X:
			result->X = objectData.dwData;
			break;

		case DIMOFS_Y:
			result->Y = objectData.dwData;
			break;

		case DIMOFS_Z:
			result->Z = objectData.dwData;
			break;
		}

		return result;
	}

	Mouse::Mouse( DirectInput^ directInput ) : Device( directInput, Utilities::ConvertNativeGuid( GUID_SysMouse ) )
	{
		HRESULT hr = InternalPointer->SetDataFormat( &c_dfDIMouse2 );
		if( RECORD_DINPUT( hr ).IsFailure )
			throw gcnew DirectInputException( Result::Last );
	}

	IList<MouseState^>^ Mouse::GetBufferedData()
	{
		DWORD size = INFINITE;
		HRESULT hr = InternalPointer->GetDeviceData( sizeof( DIDEVICEOBJECTDATA ), NULL, &size, DIGDD_PEEK );
		if( RecordError( hr ).IsFailure )
			return nullptr;

		List<MouseState^>^ list = gcnew List<MouseState^>( size );
		if( size == 0 )
			return list;

		stack_array<DIDEVICEOBJECTDATA> data = stackalloc( DIDEVICEOBJECTDATA, size );
		hr = InternalPointer->GetDeviceData( sizeof( DIDEVICEOBJECTDATA ), &data[0], &size, 0 );
		if( RecordError( hr ).IsFailure )
			return nullptr;

		if( size == 0 )
			return list;

		for( unsigned int i = 0; i < size; i++ )
			list->Add( CreateMouseState( data[i] ) );

		return list;
	}

	Result Mouse::GetCurrentState( MouseState^% data )
	{
		DIMOUSESTATE2 state;
		HRESULT hr = InternalPointer->GetDeviceState( sizeof( DIMOUSESTATE2 ), &state );
		if( RecordError( hr ).IsFailure )
			return Result::Last;

		data->AssignState( state );

		return Result::Last;
	}

	MouseState^ Mouse::GetCurrentState()
	{
		MouseState^ result = gcnew MouseState();
		GetCurrentState( result );
		return result;
	}

	ObjectProperties^ Mouse::GetObjectPropertiesByName( String^ name )
	{
		return gcnew ObjectProperties( InternalPointer, name, MouseState::typeid );
	}

	DeviceObjectInstance Mouse::GetObjectInfoByName( String^ name )
	{
		DIDEVICEOBJECTINSTANCE di;
		di.dwSize = sizeof( DIDEVICEOBJECTINSTANCE );

		HRESULT hr = InternalPointer->GetObjectInfo( &di, Marshal::OffsetOf( MouseState::typeid, name ).ToInt32(), DIPH_BYUSAGE );
		if( RECORD_DINPUT( hr ).IsFailure )
			return DeviceObjectInstance();

		return DeviceObjectInstance( di );
	}
}
}
