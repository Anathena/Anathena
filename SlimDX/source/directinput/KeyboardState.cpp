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

#include "../ComObject.h"
#include "../Utilities.h"

#include "DirectInput.h"

#include "DeviceDI.h"
#include "KeyboardState.h"
#include "DeviceConstantConverter.h"

using namespace System;
using namespace System::Collections::Generic;

namespace SlimDX
{
namespace DirectInput
{
	KeyboardState::KeyboardState()
	{
		keys = gcnew List<Key>();
		pressed = gcnew List<Key>();
		released = gcnew List<Key>();

		Array^ values = Enum::GetValues( Key::typeid );
		for each( Key key in values )
			keys->Add( key );
	}

	bool KeyboardState::IsPressed( Key key )
	{
		return pressed->Contains( key );
	}

	bool KeyboardState::IsReleased( Key key )
	{
		return released->Contains( key );
	}

	void KeyboardState::UpdateKeys( array<bool>^ states )
	{
		pressed->Clear();
		released->Clear();

		for( int i = 0; i < states->Length; i++ )
		{
			Key key = DeviceConstantConverter::DIKToKey( i );
			if( key == Key::Unknown )
				continue;

			if( states[i] )
				pressed->Add( key );
			else
				released->Add( key );
		}
	}

	void KeyboardState::UpdateKeys( BYTE *keys, int length )
	{
		pressed->Clear();
		released->Clear();

		for( int i = 0; i < length; i++ )
		{
			Key key = DeviceConstantConverter::DIKToKey( i );
			if( key == Key::Unknown )
				continue;

			if( keys[i] )
				pressed->Add( key );
			else
				released->Add( key );
		}
	}

	void KeyboardState::UpdateKey( int index, bool down )
	{
		Key key = DeviceConstantConverter::DIKToKey( index );
		if( key == Key::Unknown )
			return;

		pressed->Remove( key );
		released->Remove( key );

		if( down )
			pressed->Add( key );
		else
			released->Add( key );
	}
}
}