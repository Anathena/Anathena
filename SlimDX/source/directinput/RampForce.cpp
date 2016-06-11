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

#include "RampForce.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	RampForce::RampForce()
	{
	}

	RampForce^ RampForce::FromData( void *data, int size )
	{
		if( size != sizeof( DIRAMPFORCE ) )
			return nullptr;

		RampForce^ force = gcnew RampForce();
		DIRAMPFORCE *ptr = reinterpret_cast<DIRAMPFORCE*>( data );
		
		force->Start = ptr->lStart;
		force->End = ptr->lEnd;

		return force;
	}

	int RampForce::Size::get()
	{
		return sizeof( DICONSTANTFORCE );
	}

	void *RampForce::ToUnmanaged()
	{
		// Manual Allocation: released in Release function
		DIRAMPFORCE *result = new DIRAMPFORCE();

		result->lStart = Start;
		result->lEnd = End;

		return result;
	}

	void RampForce::Release( void* data )
	{
		delete data;
	}

	ConstantForce^ RampForce::AsConstantForce()
	{
		return nullptr;
	}

	CustomForce^ RampForce::AsCustomForce()
	{
		return nullptr;
	}

	PeriodicForce^ RampForce::AsPeriodicForce()
	{
		return nullptr;
	}

	RampForce^ RampForce::AsRampForce()
	{
		return this;
	}

	ConditionSet^ RampForce::AsConditionSet()
	{
		return nullptr;
	}
}
}