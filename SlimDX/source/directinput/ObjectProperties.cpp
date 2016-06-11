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

#include "DirectInputException.h"

#include "Enums.h"
#include "DeviceConstantConverter.h"
#include "ObjectProperties.h"
#include "Guids.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace DirectInput
{
	ObjectProperties::ObjectProperties( IDirectInputDevice8W* pointer, String^ name, Type^ dataFormat )
	{
		this->pointer = pointer;
		how = DIPH_BYOFFSET;

		obj = Marshal::OffsetOf( dataFormat, name ).ToInt32();
	}

	ObjectProperties::ObjectProperties( IDirectInputDevice8W* pointer, int value, bool isUsageCode )
	{
		this->pointer = pointer;

		if( isUsageCode )
			how = DIPH_BYUSAGE;
		else
			how = DIPH_BYID;

		obj = value;
	}

	int ObjectProperties::LowerRange::get()
	{
		DIPROPRANGE dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPRANGE );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;

		hr = pointer->GetProperty( DIPROP_RANGE, &dip.diph );
		RECORD_DINPUT( hr );

		if( FAILED( hr ) )
			return 0;

		return dip.lMin;
	}

	int ObjectProperties::UpperRange::get()
	{
		DIPROPRANGE dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPRANGE );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;

		hr = pointer->GetProperty( DIPROP_RANGE, &dip.diph );
		RECORD_DINPUT( hr );

		if( FAILED( hr ) )
			return 0;

		return dip.lMax;
	}

	void ObjectProperties::SetRange( int lowerRange, int upperRange )
	{
		DIPROPRANGE dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPRANGE );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;
		dip.lMin = lowerRange;
		dip.lMax = upperRange;

		hr = pointer->SetProperty( DIPROP_RANGE, &dip.diph );
		RECORD_DINPUT( hr );
	}

	Object^ ObjectProperties::ApplicationData::get()
	{
		DIPROPPOINTER dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPPOINTER );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;

		hr = pointer->GetProperty( DIPROP_APPDATA, &dip.diph );
		RECORD_DINPUT( hr );

		if( FAILED( hr ) || dip.uData == NULL )
			return nullptr;

		IntPtr pointer( reinterpret_cast<void*>( dip.uData ) );
		GCHandle handle = GCHandle::FromIntPtr( pointer );
		if( !handle.IsAllocated )
			return nullptr;

		Object^ obj =  handle.Target;
		handle.Free();
		return obj;
	}

	void ObjectProperties::ApplicationData::set( Object^ value )
	{
		DIPROPPOINTER dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPPOINTER );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;

		GCHandle handle = GCHandle::Alloc( value, GCHandleType::Pinned );
		dip.uData = reinterpret_cast<UINT_PTR>( handle.ToIntPtr( handle ).ToPointer() );

		hr = pointer->SetProperty( DIPROP_APPDATA, &dip.diph );
		RECORD_DINPUT( hr );

		handle.Free();
	}

	int ObjectProperties::DeadZone::get()
	{
		DIPROPDWORD dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPDWORD );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;

		hr = pointer->GetProperty( DIPROP_DEADZONE, &dip.diph );
		RECORD_DINPUT( hr );

		if( FAILED( hr ) )
			return 0;

		return dip.dwData;
	}

	void ObjectProperties::DeadZone::set( int value )
	{
		DIPROPDWORD dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPDWORD );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;
		dip.dwData = value;

		hr = pointer->SetProperty( DIPROP_DEADZONE, &dip.diph );
		RECORD_DINPUT( hr );
	}

	int ObjectProperties::Granularity::get()
	{
		DIPROPDWORD dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPDWORD );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;

		hr = pointer->GetProperty( DIPROP_GRANULARITY, &dip.diph );
		RECORD_DINPUT( hr );

		if( FAILED( hr ) )
			return 0;

		return dip.dwData;
	}

	int ObjectProperties::Saturation::get()
	{
		DIPROPDWORD dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPDWORD );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;

		hr = pointer->GetProperty( DIPROP_SATURATION, &dip.diph );
		RECORD_DINPUT( hr );

		if( FAILED( hr ) )
			return 0;

		return dip.dwData;
	}

	void ObjectProperties::Saturation::set( int value )
	{
		DIPROPDWORD dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPDWORD );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;
		dip.dwData = value;

		hr = pointer->SetProperty( DIPROP_SATURATION, &dip.diph );
		RECORD_DINPUT( hr );
	}

	InputRange ObjectProperties::LogicalRange::get()
	{
		DIPROPRANGE dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPRANGE );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;

		hr = pointer->GetProperty( DIPROP_LOGICALRANGE, &dip.diph );
		if (RECORD_DINPUT(hr).IsFailure)
			return InputRange();

		return InputRange(dip.lMin, dip.lMax);
	}

	InputRange ObjectProperties::PhysicalRange::get()
	{
		DIPROPRANGE dip;
		HRESULT hr;
		dip.diph.dwSize = sizeof( DIPROPRANGE );
		dip.diph.dwHeaderSize = sizeof( DIPROPHEADER );
		dip.diph.dwObj = obj;
		dip.diph.dwHow = how;

		hr = pointer->GetProperty( DIPROP_PHYSICALRANGE, &dip.diph );
		if (RECORD_DINPUT(hr).IsFailure)
			return InputRange();

		return InputRange(dip.lMin, dip.lMax);
	}
}
}