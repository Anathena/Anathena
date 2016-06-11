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

#include "ConditionSet.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	ConditionSet::ConditionSet()
	{
	}

	ConditionSet^ ConditionSet::FromData( void *data, int size )
	{
		if( size <= 0 || ( size % sizeof( DICONDITION ) ) != 0 )
			return nullptr;

		ConditionSet^ force = gcnew ConditionSet();
		force->Conditions = gcnew array<Condition>( size / sizeof( DICONDITION ) );

		pin_ptr<Condition> pinnedConditions = &force->Conditions[0];
		memcpy( pinnedConditions, data, size );

		return force;
	}

	int ConditionSet::Size::get()
	{
		return sizeof( DICONDITION ) * Conditions->Length;
	}

	void *ConditionSet::ToUnmanaged()
	{
		// Manual Allocation: released in Release function
		DICONDITION *result = new DICONDITION[Conditions->Length];

		for( int i = 0; i < Conditions->Length; i++ )
			result[i] = Conditions[i].ToUnmanaged();

		return result;
	}

	void ConditionSet::Release( void* data )
	{
		delete[] data;
	}

	ConstantForce^ ConditionSet::AsConstantForce()
	{
		return nullptr;
	}

	CustomForce^ ConditionSet::AsCustomForce()
	{
		return nullptr;
	}

	PeriodicForce^ ConditionSet::AsPeriodicForce()
	{
		return nullptr;
	}

	RampForce^ ConditionSet::AsRampForce()
	{
		return nullptr;
	}

	ConditionSet^ ConditionSet::AsConditionSet()
	{
		return this;
	}
}
}