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

#include "../InternalHelpers.h"

#include "TypeSpecificParameters.h"
#include "ConstantForce.h"
#include "CustomForce.h"
#include "PeriodicForce.h"
#include "RampForce.h"
#include "ConditionSet.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	TypeSpecificParameters::TypeSpecificParameters()
	{
	}

	TypeSpecificParameters::TypeSpecificParameters( void *bytes, int size )
	{
		this->bytes = bytes;
		this->size = size;
	}

	void TypeSpecificParameters::Release( void *data )
	{
		SLIMDX_UNREFERENCED_PARAMETER( data );
	}

	ConstantForce^ TypeSpecificParameters::AsConstantForce()
	{
		return ConstantForce::FromData( bytes, size );
	}

	CustomForce^ TypeSpecificParameters::AsCustomForce()
	{
		return CustomForce::FromData( bytes, size );
	}

	PeriodicForce^ TypeSpecificParameters::AsPeriodicForce()
	{
		return PeriodicForce::FromData( bytes, size );
	}

	RampForce^ TypeSpecificParameters::AsRampForce()
	{
		return RampForce::FromData( bytes, size );
	}

	ConditionSet^ TypeSpecificParameters::AsConditionSet()
	{
		return ConditionSet::FromData( bytes, size );
	}
}
}