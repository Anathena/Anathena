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

#include "Condition.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	DICONDITION Condition::ToUnmanaged()
	{
		DICONDITION result;

		result.lOffset = Offset;
		result.lPositiveCoefficient = PositiveCoefficient;
		result.lNegativeCoefficient = NegativeCoefficient;
		result.dwPositiveSaturation = PositiveSaturation;
		result.dwNegativeSaturation = NegativeSaturation;
		result.lDeadBand = DeadBand;

		return result;
	}

	bool Condition::operator == ( Condition left, Condition right )
	{
		return Condition::Equals( left, right );
	}

	bool Condition::operator != ( Condition left, Condition right )
	{
		return !Condition::Equals( left, right );
	}

	int Condition::GetHashCode()
	{
		return Offset.GetHashCode() + PositiveCoefficient.GetHashCode() + NegativeCoefficient.GetHashCode() + PositiveSaturation.GetHashCode()
			 + NegativeSaturation.GetHashCode() + DeadBand.GetHashCode();
	}

	bool Condition::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Condition>( value ) );
	}

	bool Condition::Equals( Condition value )
	{
		return ( Offset == value.Offset && PositiveCoefficient == value.PositiveCoefficient && NegativeCoefficient == value.NegativeCoefficient && 
			PositiveSaturation == value.PositiveSaturation && NegativeSaturation == value.NegativeSaturation && DeadBand == value.DeadBand );
	}

	bool Condition::Equals( Condition% value1, Condition% value2 )
	{
		return ( value1.Offset == value2.Offset && value1.PositiveCoefficient == value2.PositiveCoefficient && 
			value1.NegativeCoefficient == value2.NegativeCoefficient && value1.PositiveSaturation == value2.PositiveSaturation
			&& value1.NegativeSaturation == value2.NegativeSaturation && value1.DeadBand == value2.DeadBand );
	}
}
}