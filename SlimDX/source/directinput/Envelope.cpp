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

#include "Envelope.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	bool Envelope::operator == ( Envelope left, Envelope right )
	{
		return Envelope::Equals( left, right );
	}

	bool Envelope::operator != ( Envelope left, Envelope right )
	{
		return !Envelope::Equals( left, right );
	}

	int Envelope::GetHashCode()
	{
		return AttackLevel.GetHashCode() + AttackTime.GetHashCode() + FadeLevel.GetHashCode()
			 + FadeTime.GetHashCode();
	}

	bool Envelope::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Envelope>( value ) );
	}

	bool Envelope::Equals( Envelope value )
	{
		return ( AttackLevel == value.AttackLevel && AttackTime == value.AttackTime && FadeLevel == value.FadeLevel
			 && FadeTime == value.FadeTime );
	}

	bool Envelope::Equals( Envelope% value1, Envelope% value2 )
	{
		return ( value1.AttackLevel == value2.AttackLevel && value1.AttackTime == value2.AttackTime && value1.FadeLevel == value2.FadeLevel
			 && value1.FadeTime == value2.FadeTime );
	}
}
}