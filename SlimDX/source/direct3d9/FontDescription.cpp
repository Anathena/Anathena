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
#include <d3d9.h>
#include <d3dx9.h>

#include "FontDescription.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool FontDescription::operator == ( FontDescription left, FontDescription right )
	{
		return FontDescription::Equals( left, right );
	}

	bool FontDescription::operator != ( FontDescription left, FontDescription right )
	{
		return !FontDescription::Equals( left, right );
	}

	int FontDescription::GetHashCode()
	{
		return Height.GetHashCode() + Width.GetHashCode() + Weight.GetHashCode()
			 + MipLevels.GetHashCode() + Italic.GetHashCode() + CharSet.GetHashCode()
			 + OutputPrecision.GetHashCode() + Quality.GetHashCode() + PitchAndFamily.GetHashCode()
			 + FaceName->GetHashCode();
	}

	bool FontDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<FontDescription>( value ) );
	}

	bool FontDescription::Equals( FontDescription value )
	{
		return ( Height == value.Height && Width == value.Width && Weight == value.Weight
			 && MipLevels == value.MipLevels && Italic == value.Italic && CharSet == value.CharSet
			 && OutputPrecision == value.OutputPrecision && Quality == value.Quality && PitchAndFamily == value.PitchAndFamily
			 && FaceName == value.FaceName );
	}

	bool FontDescription::Equals( FontDescription% value1, FontDescription% value2 )
	{
		return ( value1.Height == value2.Height && value1.Width == value2.Width && value1.Weight == value2.Weight
			 && value1.MipLevels == value2.MipLevels && value1.Italic == value2.Italic && value1.CharSet == value2.CharSet
			 && value1.OutputPrecision == value2.OutputPrecision && value1.Quality == value2.Quality && value1.PitchAndFamily == value2.PitchAndFamily
			 && value1.FaceName == value2.FaceName );
	}
}
}