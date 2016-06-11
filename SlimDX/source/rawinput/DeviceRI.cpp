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

#include "../stack_array.h"
#include "../Utilities.h"
#include "../SlimDXException.h"

#include "DeviceRI.h"
#include "KeyboardInfo.h"
#include "MouseInfo.h"
#include "HidInfo.h"

using namespace System;
using namespace System::ComponentModel;
using namespace System::Windows::Forms;
using namespace System::Collections::ObjectModel;
using namespace System::Collections::Generic;

namespace SlimDX
{
namespace RawInput
{
	void Device::RegisterDevice( SlimDX::Multimedia::UsagePage usagePage, SlimDX::Multimedia::UsageId usageId, DeviceFlags flags )
	{
		RegisterDevice( usagePage, usageId, flags, IntPtr::Zero );
	}

	void Device::RegisterDevice( SlimDX::Multimedia::UsagePage usagePage, SlimDX::Multimedia::UsageId usageId, DeviceFlags flags, IntPtr target )
	{
		RegisterDevice(usagePage, usageId, flags, target, true);
	}

	void Device::RegisterDevice( SlimDX::Multimedia::UsagePage usagePage, SlimDX::Multimedia::UsageId usageId, DeviceFlags flags, IntPtr target, bool addThreadFilter )
	{
		RAWINPUTDEVICE device;
		device.usUsagePage = static_cast<USHORT>( usagePage );
		device.usUsage = static_cast<USHORT>( usageId );
		device.dwFlags = static_cast<DWORD>( flags );
		device.hwndTarget = static_cast<HWND>( target.ToPointer() );

		if( RegisterRawInputDevices( &device, 1, sizeof(RAWINPUTDEVICE) ) <= 0 )
			throw gcnew Win32Exception();

		if( filter == nullptr && addThreadFilter )
		{
			filter = gcnew InputMessageFilter();
			Application::AddMessageFilter( filter );
		}
	}

	void Device::HandleMessage( IntPtr message )
	{
		OnWmInput(reinterpret_cast<HRAWINPUT>(message.ToPointer()));
	}

	void Device::OnWmInput( HRAWINPUT handle )
	{
		UINT size = 0;
		GetRawInputData( handle, RID_INPUT, NULL, &size, sizeof(RAWINPUTHEADER) );

		if( size == 0 )
			return;

		// Manual Allocation: cleaned up in the try/finally clause below
		BYTE *bytes = new BYTE[size];

		try
		{
			GetRawInputData( handle, RID_INPUT, bytes, &size, sizeof(RAWINPUTHEADER) );

			RAWINPUT *rawInput = reinterpret_cast<RAWINPUT*>( bytes );

			if( rawInput->header.dwType == RIM_TYPEKEYBOARD )
			{
				KeyboardInput( nullptr, gcnew KeyboardInputEventArgs( rawInput->data.keyboard.MakeCode,
					static_cast<ScanCodeFlags>( rawInput->data.keyboard.Flags ),
					static_cast<Keys>( rawInput->data.keyboard.VKey ),
					static_cast<KeyState>( rawInput->data.keyboard.Message ),
					rawInput->data.keyboard.ExtraInformation, IntPtr( rawInput->header.hDevice ) ) );
			}
			else if( rawInput->header.dwType == RIM_TYPEMOUSE )
			{
				MouseInput( nullptr, gcnew MouseInputEventArgs( static_cast<MouseMode>( rawInput->data.mouse.usFlags ),
					static_cast<MouseButtonFlags>( rawInput->data.mouse.usButtonFlags ),
					static_cast<short>( rawInput->data.mouse.usButtonData ),
					rawInput->data.mouse.ulRawButtons,
					rawInput->data.mouse.lLastX,
					rawInput->data.mouse.lLastY,
					rawInput->data.mouse.ulExtraInformation, IntPtr( rawInput->header.hDevice ) ) );
			}
			else
			{
				int length = rawInput->data.hid.dwCount * rawInput->data.hid.dwSizeHid;
				array<Byte>^ bytes = gcnew array<Byte>( length );
				for( int i = 0; i < length; i++ )
					bytes[i] = rawInput->data.hid.bRawData[i];

				RawInput( nullptr, gcnew RawInputEventArgs( rawInput->data.hid.dwSizeHid, rawInput->data.hid.dwCount, bytes, IntPtr( rawInput->header.hDevice ) ) );
			}
		}
		finally
		{
			delete[] bytes;
		}
	}

	ReadOnlyCollection<DeviceInfo^>^ Device::GetDevices()
    {
		List<DeviceInfo^>^ devices = gcnew List<DeviceInfo^>();
		UINT count = 0;

		if( GetRawInputDeviceList( NULL, &count, sizeof(RAWINPUTDEVICELIST) ) != 0 )
			throw gcnew Win32Exception();

		if( count == 0 )
			return nullptr;

		stack_array<RAWINPUTDEVICELIST> deviceList = stackalloc( RAWINPUTDEVICELIST, count );
		if( GetRawInputDeviceList( &deviceList[0], &count, sizeof(RAWINPUTDEVICELIST) ) == -1 )
			throw gcnew Win32Exception();

		for( unsigned int i = 0; i < count; i++ )
		{
			UINT size = 0;
			BYTE *bytes;
			String^ name;

			GetRawInputDeviceInfo( deviceList[i].hDevice, RIDI_DEVICENAME, NULL, &size );

			if( size != 0 )
			{
				stack_array<TCHAR> chars = stackalloc( TCHAR, size );
				GetRawInputDeviceInfo( deviceList[i].hDevice, RIDI_DEVICENAME, &chars[0], &size );

				name = gcnew String( &chars[0] );
			}

			GetRawInputDeviceInfo( deviceList[i].hDevice, RIDI_DEVICEINFO, NULL, &size );

			if( size == 0 )
				continue;

			// Manual Allocation: cleaned up by the try/finally clause below
			bytes = new BYTE[size];

			try
			{
				RID_DEVICE_INFO *nativeInfo = reinterpret_cast<RID_DEVICE_INFO*>( bytes );
				nativeInfo->cbSize = sizeof( RID_DEVICE_INFO );

				GetRawInputDeviceInfo( deviceList[i].hDevice, RIDI_DEVICEINFO, nativeInfo, &size );                     

				if( nativeInfo->dwType == RIM_TYPEKEYBOARD )
				{
					KeyboardInfo^ info = gcnew KeyboardInfo();

					info->DeviceType = static_cast<DeviceType>( nativeInfo->dwType );
					info->DeviceName = name;
					info->Handle = IntPtr( deviceList[i].hDevice );
					info->KeyboardType = nativeInfo->keyboard.dwType;
					info->Subtype = nativeInfo->keyboard.dwSubType;
					info->KeyboardMode = nativeInfo->keyboard.dwKeyboardMode;
					info->FunctionKeyCount = nativeInfo->keyboard.dwNumberOfFunctionKeys;
					info->IndicatorCount = nativeInfo->keyboard.dwNumberOfIndicators;
					info->TotalKeyCount = nativeInfo->keyboard.dwNumberOfKeysTotal;

					devices->Add( info );
				}
				else if( nativeInfo->dwType == RIM_TYPEMOUSE )
				{
					MouseInfo^ info = gcnew MouseInfo();

					info->DeviceType = static_cast<DeviceType>( nativeInfo->dwType );
					info->DeviceName = name;
					info->Handle = IntPtr( deviceList[i].hDevice );
					info->Id = nativeInfo->mouse.dwId;
					info->ButtonCount = nativeInfo->mouse.dwNumberOfButtons;
					info->SampleRate = nativeInfo->mouse.dwSampleRate;

					devices->Add( info );
				}
				else
				{
					HidInfo^ info = gcnew HidInfo();

					info->DeviceType = static_cast<DeviceType>( nativeInfo->dwType );
					info->DeviceName = name;
					info->Handle = IntPtr( deviceList[i].hDevice );
					info->VendorId = nativeInfo->hid.dwVendorId;
					info->ProductId = nativeInfo->hid.dwProductId;
					info->VersionNumber = nativeInfo->hid.dwVersionNumber;

					devices->Add( info );
				}
			}
			finally
			{
				delete[] bytes;
			}
		}

		return gcnew ReadOnlyCollection<DeviceInfo^>( devices );
	}
}
}