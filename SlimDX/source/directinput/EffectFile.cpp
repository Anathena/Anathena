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
#include <dinput.h>

#include "../Utilities.h"

#include "EffectFile.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	EffectFile::EffectFile( const DIFILEEFFECT &effect )
	{
		Guid = Utilities::ConvertNativeGuid( effect.GuidEffect );
		Parameters = EffectParameters( *effect.lpDiEffect );
		Name = gcnew String( effect.szFriendlyName );
	}

	DIFILEEFFECT EffectFile::ToUnmanaged()
	{
		DIFILEEFFECT result;

		result.dwSize = sizeof( DIFILEEFFECT );
		result.GuidEffect = Utilities::ConvertManagedGuid( Guid );

		// Managed Allocation: must be cleaned up by caller
		result.lpDiEffect = new DIEFFECT( Parameters.ToUnmanaged() );

		array<Byte>^ chars = System::Text::ASCIIEncoding::ASCII->GetBytes( Name );
		pin_ptr<Byte> pinnedChars = &chars[0];

		strncpy_s( result.szFriendlyName, reinterpret_cast<const char *>( pinnedChars ), Name->Length );

		return result;
	}
}
}