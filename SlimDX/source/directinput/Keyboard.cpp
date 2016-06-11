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

#include "Keyboard.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Windows::Forms;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace DirectInput
{
	Keyboard::Keyboard( DirectInput^ directInput ) : Device( directInput, Utilities::ConvertNativeGuid( GUID_SysKeyboard ) )
	{
		HRESULT hr = InternalPointer->SetDataFormat( &c_dfDIKeyboard );
		if( RECORD_DINPUT( hr ).IsFailure )
			throw gcnew DirectInputException( Result::Last );
	}

	IList<KeyboardState^>^ Keyboard::GetBufferedData()
	{
		DWORD size = INFINITE;
		HRESULT hr = InternalPointer->GetDeviceData( sizeof( DIDEVICEOBJECTDATA ), NULL, &size, DIGDD_PEEK );
		if( RecordError( hr ).IsFailure )
			return nullptr;

		List<KeyboardState^>^ list = gcnew List<KeyboardState^>( size );
		if( size == 0 )
			return list;

		stack_array<DIDEVICEOBJECTDATA> data = stackalloc( DIDEVICEOBJECTDATA, size );
		hr = InternalPointer->GetDeviceData( sizeof( DIDEVICEOBJECTDATA ), &data[0], &size, 0 );
		if( RecordError( hr ).IsFailure )
			return nullptr;

		if( size == 0 )
			return list;

		for( unsigned int i = 0; i < size; i++ )
		{
			KeyboardState^ result = gcnew KeyboardState();
			result->UpdateKey( data[i].dwOfs, data[i].dwData > 0 );

			list->Add( result );
		}

		return list;
	}

	Result Keyboard::GetCurrentState( KeyboardState^% data )
	{
		BYTE keys[256];
		HRESULT hr = InternalPointer->GetDeviceState( 256, keys );
		if( RecordError( hr ).IsFailure )
			return Result::Last;

		data->UpdateKeys( keys, 256 );

		return Result::Last;
	}

	KeyboardState^ Keyboard::GetCurrentState()
	{
		KeyboardState^ result = gcnew KeyboardState();
		GetCurrentState( result );
		return result;
	}

	ObjectProperties^ Keyboard::GetObjectPropertiesByName( String^ name )
	{
		return gcnew ObjectProperties( InternalPointer, name, KeyboardState::typeid );
	}

	DeviceObjectInstance Keyboard::GetObjectInfoByName( String^ name )
	{
		DIDEVICEOBJECTINSTANCE di;
		di.dwSize = sizeof( DIDEVICEOBJECTINSTANCE );

		HRESULT hr = InternalPointer->GetObjectInfo( &di, Marshal::OffsetOf( KeyboardState::typeid, name ).ToInt32(), DIPH_BYUSAGE );
		if( RECORD_DINPUT( hr ).IsFailure )
			return DeviceObjectInstance();

		return DeviceObjectInstance( di );
	}
}
}
