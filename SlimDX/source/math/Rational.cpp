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

#include "Rational.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
	Rational::Rational( int numerator, int denominator ) : Numerator( numerator ), Denominator( denominator )
	{
	}

	Rational::operator float( Rational value )
	{
		return (float)value.Numerator / (float)value.Denominator;
	}

    String^ Rational::ToString() {
        return String::Format( CultureInfo::CurrentCulture, "{0}", ((double)Numerator / Denominator).ToString(CultureInfo::CurrentCulture));
    }

	bool Rational::operator == ( Rational left, Rational right )
	{
		return Rational::Equals( left, right );
	}

	bool Rational::operator != ( Rational left, Rational right )
	{
		return !Rational::Equals( left, right );
	}

	int Rational::GetHashCode()
	{
		return Numerator.GetHashCode() + Denominator.GetHashCode();
	}

	bool Rational::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Rational>( value ) );
	}

	bool Rational::Equals( Rational value )
	{
		return ( Numerator == value.Numerator && Denominator == value.Denominator );
	}

	bool Rational::Equals( Rational% value1, Rational% value2 )
	{
		return ( value1.Numerator == value2.Numerator && value1.Denominator == value2.Denominator );
	}
}