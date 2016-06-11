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
#include "../CollectionShim.h"

#include "DeviceDI.h"
#include "DeviceInstance.h"
#include "EffectDI.h"
#include "EffectInfo.h"
#include "EffectFile.h"
#include "CallbacksDI.h"

namespace SlimDX
{
namespace DirectInput
{
	BOOL CALLBACK EnumerateDevices( LPCDIDEVICEINSTANCE lpddi, LPVOID pvRef )
	{
		CollectionShim<DeviceInstance^>* shim = reinterpret_cast<CollectionShim<DeviceInstance^>*>( pvRef );
		shim->GetItems()->Add( gcnew DeviceInstance( *lpddi ) );

		return DIENUM_CONTINUE;
	}

	BOOL CALLBACK EnumerateObjects( LPCDIDEVICEOBJECTINSTANCE lpddoi, LPVOID pvRef )
	{
		CollectionShim<DeviceObjectInstance>* shim = reinterpret_cast<CollectionShim<DeviceObjectInstance>*>( pvRef );
		shim->GetItems()->Add( DeviceObjectInstance( *lpddoi ) );

		return DIENUM_CONTINUE;
	}

	BOOL CALLBACK EnumerateCreatedEffectObjects( LPDIRECTINPUTEFFECT peff, LPVOID pvRef )
	{
		CollectionShim<Effect^>* shim = reinterpret_cast<CollectionShim<Effect^>*>( pvRef );
		shim->GetItems()->Add( Effect::FromPointer( peff ) );

		return DIENUM_CONTINUE;
	}

	BOOL CALLBACK EnumerateEffects( LPCDIEFFECTINFO pdei, LPVOID pvRef )
	{
		CollectionShim<EffectInfo>* shim = reinterpret_cast<CollectionShim<EffectInfo>*>( pvRef );
		shim->GetItems()->Add( EffectInfo( *pdei ) );

		return DIENUM_CONTINUE;
	}

	BOOL CALLBACK EnumerateEffectsInFile( LPCDIFILEEFFECT lpDiFileEf, LPVOID pvRef )
	{
		CollectionShim<EffectFile>* shim = reinterpret_cast<CollectionShim<EffectFile>*>( pvRef );
		shim->GetItems()->Add( EffectFile( *lpDiFileEf ) );

		return DIENUM_CONTINUE;
	}
}
}
