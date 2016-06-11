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

#include "PeriodicForce.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	PeriodicForce::PeriodicForce()
	{
	}

	PeriodicForce^ PeriodicForce::FromData( void *data, int size )
	{
		if( size != sizeof( DIPERIODIC ) )
			return nullptr;

		PeriodicForce^ force = gcnew PeriodicForce();
		DIPERIODIC *ptr = reinterpret_cast<DIPERIODIC*>( data );
		
		force->Magnitude = ptr->dwMagnitude;
		force->Offset = ptr->lOffset;
		force->Phase = ptr->dwPhase;
		force->Period = ptr->dwPeriod;

		return force;
	}

	int PeriodicForce::Size::get()
	{
		return sizeof( DIPERIODIC );
	}

	void *PeriodicForce::ToUnmanaged()
	{
		// Manual Allocation: released in Release function
		DIPERIODIC *result = new DIPERIODIC();

		result->dwMagnitude = Magnitude;
		result->dwPeriod = Period;
		result->dwPhase = Phase;
		result->lOffset = Offset;

		return result;
	}

	void PeriodicForce::Release( void* data )
	{
		delete data;
	}

	ConstantForce^ PeriodicForce::AsConstantForce()
	{
		return nullptr;
	}

	CustomForce^ PeriodicForce::AsCustomForce()
	{
		return nullptr;
	}

	PeriodicForce^ PeriodicForce::AsPeriodicForce()
	{
		return this;
	}

	RampForce^ PeriodicForce::AsRampForce()
	{
		return nullptr;
	}

	ConditionSet^ PeriodicForce::AsConditionSet()
	{
		return nullptr;
	}
}
}