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
#include <dinput.h>
#include <vcclr.h>

#include "../ComObject.h"
#include "../SlimDXException.h"
#include "../InternalHelpers.h"
#include "../CollectionShim.h"

#include "DirectInput.h"
#include "DirectInputException.h"
#include "DirectInputNotFoundException.h"

#include "CallbacksDI.h"
#include "DeviceDI.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace DirectInput
{
    DirectInput::DirectInput()
	{
		IDirectInput8W* dinput;
		IntPtr hInstance = Marshal::GetHINSTANCE( DirectInput::typeid->Module );

        try
        {
		    HRESULT hr = DirectInput8Create( static_cast<HINSTANCE>( hInstance.ToPointer() ), DIRECTINPUT_VERSION, 
			    IID_IDirectInput8, reinterpret_cast<void**>( &dinput ), NULL );

			RECORD_DINPUT( hr );
        }
        catch( SEHException^ ex )
        {
            throw gcnew DirectInputNotFoundException( "DirectInput was not found. Reinstalling DirectX may fix the problem.", ex );
        }

		if( dinput == NULL )
			throw gcnew DirectInputException( "Could not create DirectInput instance." );

		Construct( dinput );
	}

	Result DirectInput::RunControlPanel()
	{
		HRESULT hr = InternalPointer->RunControlPanel( NULL, 0 );
		return RECORD_DINPUT( hr );
	}

	Result DirectInput::RunControlPanel( IntPtr parent )
	{
		HRESULT hr = InternalPointer->RunControlPanel( static_cast<HWND>( parent.ToPointer() ), 0 );
		return RECORD_DINPUT( hr );
	}

	bool DirectInput::IsDeviceAttached( Guid device )
	{
		HRESULT hr = InternalPointer->GetDeviceStatus( Utilities::ConvertManagedGuid( device ) );
		return hr == DI_OK;
	}

	Guid DirectInput::FindDevice( Guid deviceClass, String^ name )
	{
		GUID result;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( name );

		HRESULT hr = InternalPointer->FindDevice( Utilities::ConvertManagedGuid( deviceClass ),
			reinterpret_cast<LPCTSTR>( pinnedName ), &result );
		RECORD_DINPUT( hr );

		if( FAILED( hr ) )
			return Guid::Empty;

		return Utilities::ConvertNativeGuid( result );
	}

	IList<DeviceInstance^>^ DirectInput::GetDevices()
	{
		return GetDevices( DeviceClass::All, DeviceEnumerationFlags::AllDevices );
	}

	IList<DeviceInstance^>^ DirectInput::GetDevices( DeviceClass deviceClass, DeviceEnumerationFlags enumerationFlags )
	{
		List<DeviceInstance^>^ results = gcnew List<DeviceInstance^>();
		CollectionShim<DeviceInstance^> shim( results );

		HRESULT hr = InternalPointer->EnumDevices( static_cast<DWORD>( deviceClass ), static_cast<LPDIENUMDEVICESCALLBACK>( EnumerateDevices ), &shim, static_cast<DWORD>( enumerationFlags ) );
		if( RECORD_DINPUT( hr ).IsFailure )
			return nullptr;

		return results;
	}

	IList<DeviceInstance^>^ DirectInput::GetDevices( DeviceType deviceType, DeviceEnumerationFlags enumerationFlags )
	{
		List<DeviceInstance^>^ results = gcnew List<DeviceInstance^>();
		CollectionShim<DeviceInstance^> shim( results );

		HRESULT hr = InternalPointer->EnumDevices( static_cast<DWORD>( deviceType ), static_cast<LPDIENUMDEVICESCALLBACK>( EnumerateDevices ), &shim, static_cast<DWORD>( enumerationFlags ) );
		if( RECORD_DINPUT( hr ).IsFailure )
			return nullptr;

		return results;
	}
}
}