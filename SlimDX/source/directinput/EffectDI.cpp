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

#include "../ComObject.h"
#include "../Utilities.h"

#include "DirectInput.h"
#include "DirectInputException.h"

#include "DeviceDI.h"
#include "EffectDI.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	Effect::Effect( Device^ device, System::Guid guid )
	{
		IDirectInputEffect *effect;
		GUID nativeGuid = Utilities::ConvertManagedGuid( guid );

		HRESULT hr = device->InternalPointer->CreateEffect( nativeGuid, NULL, &effect, NULL );
		if( RECORD_DINPUT( hr ).IsFailure )
			throw gcnew DirectInputException( Result::Last );

		Construct( effect );
	}

	Effect::Effect( Device^ device, System::Guid guid, EffectParameters parameters )
	{
		IDirectInputEffect *effect;
		GUID nativeGuid = Utilities::ConvertManagedGuid( guid );
		DIEFFECT params = parameters.ToUnmanaged();

		HRESULT hr = device->InternalPointer->CreateEffect( nativeGuid, &params, &effect, NULL );
		parameters.Cleanup( params );

		if( RECORD_DINPUT( hr ).IsFailure )
			throw gcnew DirectInputException( Result::Last );

		Construct( effect );
	}

	EffectParameters Effect::GetParameters()
	{
		return GetParameters( EffectParameterFlags::All );
	}

	EffectParameters Effect::GetParameters( EffectParameterFlags flags )
	{
		DIEFFECT params;
		params.dwSize = sizeof( DIEFFECT );

		HRESULT hr = InternalPointer->GetParameters( &params, static_cast<DWORD>( flags ) );
		if( RECORD_DINPUT( hr ).IsFailure )
			return EffectParameters();

		return EffectParameters( params );
	}

	Result Effect::SetParameters( EffectParameters parameters )
	{
		return SetParameters( parameters, EffectParameterFlags::All );
	}

	Result Effect::SetParameters( EffectParameters parameters, EffectParameterFlags flags )
	{
		DIEFFECT params = parameters.ToUnmanaged();

		HRESULT hr = InternalPointer->SetParameters( &params, static_cast<DWORD>( flags ) );
		parameters.Cleanup( params );

		return RECORD_DINPUT( hr );
	}

	Result Effect::Start()
	{
		return Start( INFINITE, EffectPlayFlags::None );
	}

	Result Effect::Start( int iterations )
	{
		return Start( iterations, EffectPlayFlags::None );
	}

	Result Effect::Start( EffectPlayFlags flags )
	{
		return Start( INFINITE, flags );
	}

	Result Effect::Start( int iterations, EffectPlayFlags flags )
	{
		HRESULT hr = InternalPointer->Start( iterations, static_cast<DWORD>( flags ) );
		return RECORD_DINPUT( hr );
	}

	Result Effect::Stop()
	{
		return RECORD_DINPUT( InternalPointer->Stop() );
	}

	Result Effect::Unload()
	{
		return RECORD_DINPUT( InternalPointer->Unload() );
	}

	Result Effect::Download()
	{
		return RECORD_DINPUT( InternalPointer->Download() );
	}

	array<Byte>^ Effect::Escape( int command, array<Byte>^ data, int outputSize )
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

	EffectStatus Effect::Status::get()
	{
		DWORD flags = 0;

		HRESULT hr = InternalPointer->GetEffectStatus( &flags );
		RECORD_DINPUT( hr );

		return static_cast<EffectStatus>( flags );
	}

	System::Guid Effect::Guid::get()
	{
		GUID guid;

		HRESULT hr = InternalPointer->GetEffectGuid( &guid );
		if( RECORD_DINPUT( hr ).IsFailure )
			return System::Guid::Empty;

		return Utilities::ConvertNativeGuid( guid );
	}
}
}