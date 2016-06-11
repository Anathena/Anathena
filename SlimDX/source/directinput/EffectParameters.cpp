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

#include "EffectParameters.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace DirectInput
{
	EffectParameters::EffectParameters( const DIEFFECT &effect )
	{
		Flags = static_cast<EffectFlags>( effect.dwFlags );
		Duration = effect.dwDuration;
		SamplePeriod = effect.dwSamplePeriod;
		Gain = effect.dwGain;
		TriggerButton = effect.dwTriggerButton;
		TriggerRepeatInterval = effect.dwTriggerRepeatInterval;
		StartDelay = effect.dwStartDelay;

		if( effect.rgdwAxes != NULL && effect.cAxes > 0 )
		{
			axes = gcnew array<int>( effect.cAxes );
			for( UINT i = 0; i < effect.cAxes; i++ )
				axes[i] = effect.rgdwAxes[i];
		}

		if( effect.rglDirection != NULL && effect.cAxes > 0 )
		{
			directions = gcnew array<int>( effect.cAxes );
			for( UINT i = 0; i < effect.cAxes; i++ )
				directions[i] = effect.rglDirection[i];
		}

		if( effect.lpEnvelope != NULL )
		{
			DirectInput::Envelope env;
			env.AttackLevel = effect.lpEnvelope->dwAttackLevel;
			env.AttackTime = effect.lpEnvelope->dwAttackTime;
			env.FadeLevel = effect.lpEnvelope->dwFadeLevel;
			env.FadeTime = effect.lpEnvelope->dwFadeTime;

			Envelope = env;
		}

		if( effect.cbTypeSpecificParams > 0 && effect.lpvTypeSpecificParams != NULL )
			Parameters = gcnew TypeSpecificParameters( effect.lpvTypeSpecificParams, effect.cbTypeSpecificParams );
	}

	DIEFFECT EffectParameters::ToUnmanaged()
	{
		DIEFFECT result;
		memset( &result, 0, sizeof( DIEFFECT ) );

		result.dwSize = sizeof( DIEFFECT );
		result.dwFlags = static_cast<DWORD>( Flags );
		result.dwDuration = Duration;
		result.dwSamplePeriod = SamplePeriod;
		result.dwGain = Gain;
		result.dwTriggerButton = TriggerButton;
		result.dwTriggerRepeatInterval = TriggerRepeatInterval;
		result.dwStartDelay = StartDelay;

		if( axes != nullptr )
		{
			result.cAxes = axes->Length;

			// Manual Allocation: cleaned up in Cleanup() method
			result.rgdwAxes = new DWORD[axes->Length];

			pin_ptr<int> pinnedAxes = &axes[0];
			memcpy( result.rgdwAxes, pinnedAxes, sizeof( DWORD ) * axes->Length );
		}

		if( directions != nullptr )
		{
			// Manual Allocation: cleaned up in Cleanup() method
			result.rglDirection = new LONG[axes->Length];

			pin_ptr<int> pinnedDirections = &directions[0];
			memcpy( result.rglDirection, pinnedDirections, sizeof( LONG ) * axes->Length );
		}

		if( Envelope.HasValue )
		{
			// Manual Allocation: cleaned up in Cleanup() method
			DIENVELOPE *env = new DIENVELOPE();
			env->dwSize = sizeof( DIENVELOPE );
			env->dwAttackLevel = Envelope.Value.AttackLevel;
			env->dwAttackTime = Envelope.Value.AttackTime;
			env->dwFadeLevel = Envelope.Value.FadeLevel;
			env->dwFadeTime = Envelope.Value.FadeTime;

			result.lpEnvelope = env;
		}

		if( Parameters != nullptr )
		{
			result.cbTypeSpecificParams = Parameters->Size;
			result.lpvTypeSpecificParams = Parameters->ToUnmanaged();
		}

		return result;
	}

	void EffectParameters::Cleanup( const DIEFFECT &effect )
	{
		if( effect.rgdwAxes != NULL )
			delete[] effect.rgdwAxes;

		if( effect.rglDirection != NULL )
			delete[] effect.rglDirection;

		if( effect.lpEnvelope != NULL )
			delete effect.lpEnvelope;

		if( effect.lpvTypeSpecificParams != NULL && Parameters != nullptr )
			Parameters->Release( effect.lpvTypeSpecificParams );
	}

	void EffectParameters::GetAxes( [Out] array<int>^ %axes, [Out] array<int>^ %directions )
	{
		axes = this->axes;
		directions = this->directions;
	}

	void EffectParameters::SetAxes( array<int>^ axes, array<int>^ directions )
	{
		this->axes = axes;
		this->directions = directions;
	}
}
}