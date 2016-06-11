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
#include <vcclr.h>

#include "../stack_array.h"
#include "../ComObject.h"
#include "../Utilities.h"
#include "../CollectionShim.h"

#include "DirectInput.h"
#include "DirectInputException.h"

#include "EffectDI.h"
#include "DeviceDI.h"
#include "CallbacksDI.h"

using namespace System;
using namespace System::Threading;
using namespace System::Collections::Generic;
using namespace System::Windows::Forms;

namespace SlimDX
{
namespace DirectInput
{
	Device::Device( DirectInput^ directInput, Guid subsystem )
	{
		IDirectInputDevice8* device;
		HRESULT hr = directInput->InternalPointer->CreateDevice( Utilities::ConvertManagedGuid( subsystem ), &device, NULL );

		if( RECORD_DINPUT( hr ).IsFailure )
			throw gcnew DirectInputException( Result::Last );

		Construct( device );
	}

	Result Device::SetCooperativeLevel( IntPtr handle, CooperativeLevel flags )
	{
		HRESULT hr = InternalPointer->SetCooperativeLevel( static_cast<HWND>( handle.ToPointer() ), static_cast<DWORD>( flags ) );
		return RECORD_DINPUT( hr );
	}

	Result Device::SetCooperativeLevel( Control^ control, CooperativeLevel flags )
	{
		return SetCooperativeLevel( control->Handle, flags );
	}

	Result Device::Acquire()
	{
		HRESULT hr = InternalPointer->Acquire();
		return RecordError( hr );
	}

	Result Device::Unacquire()
	{
		HRESULT hr = InternalPointer->Unacquire();
		return RecordError( hr );
	}

	Result Device::Poll()
	{
		HRESULT hr = InternalPointer->Poll();
		return RecordError( hr );
	}

	Result Device::RunControlPanel()
	{
		HRESULT hr = InternalPointer->RunControlPanel( NULL, 0 );
		return RECORD_DINPUT( hr );
	}

	Result Device::RunControlPanel( Control^ parent )
	{
		HRESULT hr = InternalPointer->RunControlPanel( static_cast<HWND>( parent->Handle.ToPointer() ), 0 );
		return RECORD_DINPUT( hr );
	}

	DeviceImageHeader Device::GetDeviceImages()
	{
		DIDEVICEIMAGEINFOHEADER header;
		memset( &header, 0, sizeof( DIDEVICEIMAGEINFOHEADER ) );
		header.dwSize = sizeof( DIDEVICEIMAGEINFOHEADER );
		header.dwSizeImageInfo = sizeof( DIDEVICEIMAGEINFO );

		HRESULT hr = InternalPointer->GetImageInfo( &header );
		if( RECORD_DINPUT( hr ).IsFailure || header.dwBufferUsed <= 0 )
			return DeviceImageHeader();

		stack_array<DIDEVICEIMAGEINFO> images = stackalloc( DIDEVICEIMAGEINFO, header.dwBufferUsed / sizeof( DIDEVICEIMAGEINFO ) );
		header.dwBufferSize = header.dwBufferUsed;
		header.lprgImageInfoArray = &images[0];

		hr = InternalPointer->GetImageInfo( &header );
		if( RECORD_DINPUT( hr ).IsFailure )
			return DeviceImageHeader();

		return DeviceImageHeader( header );
	}

	EffectInfo Device::GetEffectInfo( Guid guid )
	{
		GUID nativeGuid = Utilities::ConvertManagedGuid( guid );
		DIEFFECTINFO info;
		info.dwSize = sizeof( DIEFFECTINFO );

		HRESULT hr = InternalPointer->GetEffectInfo( &info, nativeGuid );
		if( RECORD_DINPUT( hr ).IsFailure )
			return EffectInfo();

		return EffectInfo( info );
	}

	int Device::GetDeviceData(array<RawBufferedData>^ data, bool peek)
	{
		DWORD length = data->Length;
		stack_array<DIDEVICEOBJECTDATA> native = stackalloc(DIDEVICEOBJECTDATA, length);

		HRESULT hr = InternalPointer->GetDeviceData(sizeof(DIDEVICEOBJECTDATA), &native[0], &length, peek ? DIGDD_PEEK : 0);
		if (RECORD_DINPUT(hr).IsFailure)
			return -1;

		for (DWORD i = 0; i < length; i++)
			data[i] = RawBufferedData::FromUnmanaged(native[i]);

		return length;
	}

	Result Device::SendData( array<ObjectData>^ data, bool overlay )
	{
		DWORD count = data->Length;
		stack_array<DIDEVICEOBJECTDATA> input = stackalloc( DIDEVICEOBJECTDATA, count );

		for( UINT i = 0; i < count; i++ )
		{
			input[i].dwOfs = data[i].Instance;
			input[i].dwData = data[i].Data;
		}

		HRESULT hr = InternalPointer->SendDeviceData( sizeof( DIDEVICEOBJECTDATA ), &input[0], &count, overlay ? DISDD_CONTINUE : 0 );
		return RECORD_DINPUT( hr );
	}

	DeviceObjectInstance Device::GetObjectInfoByOffset( int offset )
	{
		DIDEVICEOBJECTINSTANCE di;
		di.dwSize = sizeof( DIDEVICEOBJECTINSTANCE );

		HRESULT hr = InternalPointer->GetObjectInfo( &di, offset, DIPH_BYOFFSET );
		if( RECORD_DINPUT( hr ).IsFailure )
			return DeviceObjectInstance();

		return DeviceObjectInstance( di );
	}

	DeviceObjectInstance Device::GetObjectInfoByUsage( int usageCode )
	{
		DIDEVICEOBJECTINSTANCE di;
		di.dwSize = sizeof( DIDEVICEOBJECTINSTANCE );

		HRESULT hr = InternalPointer->GetObjectInfo( &di, usageCode, DIPH_BYUSAGE );
		if( RECORD_DINPUT( hr ).IsFailure )
			return DeviceObjectInstance();

		return DeviceObjectInstance( di );
	}

	DeviceObjectInstance Device::GetObjectInfoById( int objectId )
	{
		DIDEVICEOBJECTINSTANCE di;
		di.dwSize = sizeof( DIDEVICEOBJECTINSTANCE );

		HRESULT hr = InternalPointer->GetObjectInfo( &di, objectId, DIPH_BYID );
		if( RECORD_DINPUT( hr ).IsFailure )
			return DeviceObjectInstance();

		return DeviceObjectInstance( di );
	}

	SlimDX::DirectInput::Capabilities^ Device::Capabilities::get()
	{
		if( caps != nullptr )
			return caps;

		DIDEVCAPS c;
		c.dwSize = sizeof( DIDEVCAPS );
		HRESULT hr = InternalPointer->GetCapabilities( &c );
		
		if( RECORD_DINPUT( hr ).IsFailure )
			caps = nullptr;

		caps = gcnew SlimDX::DirectInput::Capabilities( c );
		return caps;
	}

	DeviceInstance^ Device::Information::get()
	{
		if( information != nullptr )
			return information;

		DIDEVICEINSTANCE deviceInstance;
		deviceInstance.dwSize = sizeof(DIDEVICEINSTANCE);
		HRESULT hr = InternalPointer->GetDeviceInfo( &deviceInstance );
		
		if( RECORD_DINPUT( hr ).IsFailure )
			information = nullptr;

		information = gcnew DeviceInstance( deviceInstance );

		return information;
	}

	IList<DeviceObjectInstance>^ Device::GetObjects()
	{
		return GetObjects( ObjectDeviceType::All );
	}

	IList<DeviceObjectInstance>^ Device::GetObjects( ObjectDeviceType objectType )
	{
		List<DeviceObjectInstance>^ results = gcnew List<DeviceObjectInstance>();
		CollectionShim<DeviceObjectInstance> shim( results );

		HRESULT hr = InternalPointer->EnumObjects( static_cast<LPDIENUMDEVICEOBJECTSCALLBACK>( EnumerateObjects ), &shim, static_cast<DWORD>( objectType ) );
		if( RECORD_DINPUT( hr ).IsFailure )
			return nullptr;

		return results;
	}

	IList<EffectInfo>^ Device::GetEffects()
	{
		return GetEffects( EffectType::All );
	}

	IList<EffectInfo>^ Device::GetEffects( EffectType type )
	{
		List<EffectInfo>^ results = gcnew List<EffectInfo>();
		CollectionShim<EffectInfo> shim( results );

		HRESULT hr = InternalPointer->EnumEffects( static_cast<LPDIENUMEFFECTSCALLBACK>( EnumerateEffects ), &shim, static_cast<DWORD>( type ) );
		if( RECORD_DINPUT( hr ).IsFailure )
			return nullptr;

		return results;
	}

	IList<EffectFile>^ Device::GetEffectsInFile( String^ fileName )
	{
		return GetEffectsInFile( fileName, EffectFileFlags::None );
	}

	IList<EffectFile>^ Device::GetEffectsInFile( String^ fileName, EffectFileFlags flags )
	{
		List<EffectFile>^ results = gcnew List<EffectFile>();
		CollectionShim<EffectFile> shim( results );
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = InternalPointer->EnumEffectsInFile( reinterpret_cast<LPCWSTR>( pinnedName ), 
			static_cast<LPDIENUMEFFECTSINFILECALLBACK>( EnumerateEffectsInFile ), &shim, static_cast<DWORD>( flags ) );

		if( RECORD_DINPUT( hr ).IsFailure )
			return nullptr;

		return results;
	}

	Result Device::WriteEffectsToFile( String^ fileName, array<EffectFile>^ effects )
	{
		return WriteEffectsToFile( fileName, effects, false );
	}

	Result Device::WriteEffectsToFile( String^ fileName, array<EffectFile>^ effects, bool includeNonstandardEffects )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );
		stack_array<DIFILEEFFECT> entries = stackalloc( DIFILEEFFECT, effects->Length );

		for( int i = 0; i < effects->Length; i++ )
			entries[i] = effects[i].ToUnmanaged();

		HRESULT hr = InternalPointer->WriteEffectToFile( reinterpret_cast<LPCWSTR>( pinnedName ), effects->Length,
			&entries[0], includeNonstandardEffects ? DIFEF_INCLUDENONSTANDARD : 0 );

		for( int i = 0; i < effects->Length; i++ )
			delete entries[i].lpDiEffect;

		return RECORD_DINPUT( hr );
	}

	Result Device::SetNotification( WaitHandle^ eventHandle )
	{
		HRESULT hr = InternalPointer->SetEventNotification( eventHandle->SafeWaitHandle->DangerousGetHandle().ToPointer() );
		return RECORD_DINPUT( hr );
	}

	ObjectProperties^ Device::GetObjectPropertiesById( int objectId )
	{
		return gcnew ObjectProperties( InternalPointer, objectId, false );
	}

	ObjectProperties^ Device::GetObjectPropertiesByUsage( int usageCode )
	{
		return gcnew ObjectProperties( InternalPointer, usageCode, true );
	}

	array<Byte>^ Device::Escape( int command, array<Byte>^ data, int outputSize )
	{
		array<Byte>^ output = gcnew array<Byte>( outputSize );

		pin_ptr<Byte> pinnedData = &data[0];
		pin_ptr<Byte> pinnedOutput = &output[0];

		DIEFFESCAPE input;
		input.dwSize = sizeof(DIEFFESCAPE);
		input.dwCommand = command;
		input.cbInBuffer = data->Length;
		input.cbOutBuffer = output->Length;
		input.lpvInBuffer = pinnedData;
		input.lpvOutBuffer = pinnedOutput;

		HRESULT hr = InternalPointer->Escape( &input );
		if( RECORD_DINPUT( hr ).IsFailure )
			return nullptr;

		return output;
	}

	DeviceProperties^ Device::Properties::get()
	{
		if( properties == nullptr )
			properties = gcnew DeviceProperties( InternalPointer );

		return properties;
	}

	IList<Effect^>^ Device::CreatedEffects::get()
	{
		List<Effect^>^ results = gcnew List<Effect^>();
		CollectionShim<Effect^> shim( results );

		HRESULT hr = InternalPointer->EnumCreatedEffectObjects( static_cast<LPDIENUMCREATEDEFFECTOBJECTSCALLBACK>( EnumerateCreatedEffectObjects ), &shim, 0 );
		if( RECORD_DINPUT( hr ).IsFailure )
			return nullptr;

		return results;
	}
}
}
