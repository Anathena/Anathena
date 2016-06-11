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

#include "CustomForce.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	CustomForce::CustomForce()
	{
	}

	CustomForce^ CustomForce::FromData( void *data, int size )
	{
		if( size != sizeof( DICUSTOMFORCE ) )
			return nullptr;

		CustomForce^ force = gcnew CustomForce();
		DICUSTOMFORCE *ptr = reinterpret_cast<DICUSTOMFORCE*>( data );
		
		force->Channels = ptr->cChannels;
		force->SamplePeriod = ptr->dwSamplePeriod;
		force->Samples = ptr->cSamples;
		force->ForceData = gcnew array<int>( ptr->cSamples );

		pin_ptr<int> pinnedData = &force->ForceData[0];
		memcpy( pinnedData, ptr->rglForceData, sizeof( int ) * force->ForceData->Length );

		return force;
	}

	int CustomForce::Size::get()
	{
		return sizeof( DICUSTOMFORCE );
	}

	void *CustomForce::ToUnmanaged()
	{
		// Manual Allocation: released in Release function
		DICUSTOMFORCE *result = new DICUSTOMFORCE();

		result->cChannels = Channels;
		result->dwSamplePeriod = SamplePeriod;
		result->cSamples = Samples;

		// Manual Allocation: released in Release function
		result->rglForceData = new LONG[ForceData->Length];

		pin_ptr<int> pinnedData = &ForceData[0];
		memcpy( result->rglForceData, pinnedData, sizeof( int ) * ForceData->Length );

		return result;
	}

	void CustomForce::Release( void* data )
	{
		DICUSTOMFORCE *result = reinterpret_cast<DICUSTOMFORCE*>( data );

		delete[] result->rglForceData;
		delete data;
	}

	ConstantForce^ CustomForce::AsConstantForce()
	{
		return nullptr;
	}

	CustomForce^ CustomForce::AsCustomForce()
	{
		return this;
	}

	PeriodicForce^ CustomForce::AsPeriodicForce()
	{
		return nullptr;
	}

	RampForce^ CustomForce::AsRampForce()
	{
		return nullptr;
	}

	ConditionSet^ CustomForce::AsConditionSet()
	{
		return nullptr;
	}
}
}