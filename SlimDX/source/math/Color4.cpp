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

#include <d3dx9.h>

#include "Color4.h"

#include "Color3.h"
#include "Vector3.h"
#include "Vector4.h"

using namespace System;
using namespace System::Globalization;
using namespace System::Drawing;

namespace SlimDX
{
	Color4 Color4::FromUnmanaged( const D3DXCOLOR &color )
	{
		return Color4( color.a, color.r, color.g, color.b );
	}

	D3DXCOLOR Color4::ToUnmanaged()
	{
		D3DXCOLOR result( Red, Green, Blue, Alpha );
		return result;
	}

	Color4::Color4( float alpha, float red, float green, float blue )
	{
		Alpha = alpha;
		Red = red;
		Green = green;
		Blue = blue;
	}

	Color4::Color4( float red, float green, float blue )
	{
		Alpha = 1.0f;
		Red = red;
		Green = green;
		Blue = blue;
	}

	Color4::Color4( Color color )
	{
		Alpha = color.A / 255.0f;
		Red = color.R / 255.0f;
		Green = color.G / 255.0f;
		Blue = color.B / 255.0f;
	}

	Color4::Color4( Color3 color )
	{
		Alpha = 1.0f;
		Red = color.Red;
		Green = color.Green;
		Blue = color.Blue;
	}

	Color4::Color4( Vector3 color )
	{
		Alpha = 1.0f;
		Red = color.X;
		Green = color.Y;
		Blue = color.Z;
	}

	Color4::Color4( Vector4 color )
	{
		Alpha = color.W;
		Red = color.X;
		Green = color.Y;
		Blue = color.Z;
	}

	Color4::Color4( int argb )
	{
		Alpha = ( ( argb >> 24 ) & 255 ) / 255.0f;
		Red = ( ( argb >> 16 ) & 255 ) / 255.0f;
		Green = ( ( argb >> 8 ) & 255 ) / 255.0f;
		Blue = ( argb & 255 ) / 255.0f;
	}

	Color Color4::ToColor()
	{
		return Color::FromArgb( static_cast<int>(Alpha * 255), static_cast<int>(Red * 255), static_cast<int>(Green * 255), static_cast<int>(Blue * 255) );
	}

	Color3 Color4::ToColor3()
	{
		return Color3( Red, Green, Blue );
	}

	int Color4::ToArgb()
	{
		unsigned int a, r, g, b;

		a = static_cast<unsigned int>(Alpha * 255.0f);
		r = static_cast<unsigned int>(Red * 255.0f);
		g = static_cast<unsigned int>(Green * 255.0f);
		b = static_cast<unsigned int>(Blue * 255.0f);

		unsigned int value = b;
		value += g << 8;
		value += r << 16;
		value += a << 24;

		return static_cast<int>( value );
	}

	Vector3 Color4::ToVector3()
	{
		return Vector3( Red, Green, Blue );
	}

	Vector4 Color4::ToVector4()
	{
		return Vector4( Red, Green, Blue, Alpha );
	}

	Color4 Color4::Add( Color4 color1, Color4 color2 )
	{
		return Color4( color1.Alpha + color2.Alpha, color1.Red + color2.Red, color1.Green + color2.Green, color1.Blue + color2.Blue );
	}

	void Color4::Add( Color4% color1, Color4% color2, [Out] Color4% result )
	{
		result = Color4( color1.Alpha + color2.Alpha, color1.Red + color2.Red, color1.Green + color2.Green, color1.Blue + color2.Blue );
	}

	Color4 Color4::Subtract( Color4 color1, Color4 color2 )
	{
		return Color4( color1.Alpha - color2.Alpha, color1.Red - color2.Red, color1.Green - color2.Green, color1.Blue - color2.Blue );
	}

	void Color4::Subtract( Color4% color1, Color4% color2, [Out] Color4% result )
	{
		result = Color4( color1.Alpha - color2.Alpha, color1.Red - color2.Red, color1.Green - color2.Green, color1.Blue - color2.Blue );
	}

	Color4 Color4::Modulate( Color4 color1, Color4 color2 )
	{
		return Color4( color1.Alpha * color2.Alpha, color1.Red * color2.Red, color1.Green * color2.Green, color1.Blue * color2.Blue );
	}

	void Color4::Modulate( Color4% color1, Color4% color2, [Out] Color4% result )
	{
		result = Color4( color1.Alpha * color2.Alpha, color1.Red * color2.Red, color1.Green * color2.Green, color1.Blue * color2.Blue );
	}

	Color4 Color4::Lerp( Color4 color1, Color4 color2, float amount )
	{
		float a = color1.Alpha + amount * ( color2.Alpha - color1.Alpha );
		float r = color1.Red + amount * ( color2.Red - color1.Red );
		float g = color1.Green + amount * ( color2.Green - color1.Green );
		float b = color1.Blue + amount * ( color2.Blue - color1.Blue );

		return Color4( a, r, g, b );
	}

	void Color4::Lerp( Color4% color1, Color4% color2, float amount, [Out] Color4% result )
	{
		float a = color1.Alpha + amount * ( color2.Alpha - color1.Alpha );
		float r = color1.Red + amount * ( color2.Red - color1.Red );
		float g = color1.Green + amount * ( color2.Green - color1.Green );
		float b = color1.Blue + amount * ( color2.Blue - color1.Blue );

		result = Color4( a, r, g, b );
	}

	Color4 Color4::Negate( Color4 color )
	{
		return Color4( 1.0f - color.Alpha, 1.0f - color.Red, 1.0f - color.Green, 1.0f - color.Blue );
	}

	void Color4::Negate( Color4% color, [Out] Color4% result )
	{
		result = Color4( 1.0f - color.Alpha, 1.0f - color.Red, 1.0f - color.Green, 1.0f - color.Blue );
	}

	Color4 Color4::AdjustContrast( Color4 color, float contrast )
	{
		float r = 0.5f + contrast * ( color.Red - 0.5f );
		float g = 0.5f + contrast * ( color.Green - 0.5f );
		float b = 0.5f + contrast * ( color.Blue - 0.5f );

		return Color4( color.Alpha, r, g, b );
	}

	void Color4::AdjustContrast( Color4% color, float contrast, [Out] Color4% result )
	{
		float r = 0.5f + contrast * ( color.Red - 0.5f );
		float g = 0.5f + contrast * ( color.Green - 0.5f );
		float b = 0.5f + contrast * ( color.Blue - 0.5f );

		result = Color4( color.Alpha, r, g, b );
	}

	Color4 Color4::AdjustSaturation( Color4 color, float saturation )
	{
		float grey = color.Red * 0.2125f + color.Green * 0.7154f + color.Blue * 0.0721f;
		float r = grey + saturation * ( color.Red - grey );
		float g = grey + saturation * ( color.Green - grey );
		float b = grey + saturation * ( color.Blue - grey );

		return Color4( color.Alpha, r, g, b );
	}

	void Color4::AdjustSaturation( Color4% color, float saturation, [Out] Color4% result )
	{
		float grey = color.Red * 0.2125f + color.Green * 0.7154f + color.Blue * 0.0721f;
		float r = grey + saturation * ( color.Red - grey );
		float g = grey + saturation * ( color.Green - grey );
		float b = grey + saturation * ( color.Blue - grey );

		result = Color4( color.Alpha, r, g, b );
	}

	Color4 Color4::Scale( Color4 color, float scale )
	{
		return Color4( color.Alpha, color.Red * scale, color.Green * scale, color.Blue * scale );
	}

	void Color4::Scale( Color4% color, float scale, [Out] Color4% result )
	{
		result = Color4( color.Alpha, color.Red * scale, color.Green * scale, color.Blue * scale );
	}

	Color4 Color4::operator + ( Color4 color1, Color4 color2 )
	{
		return Color4( color1.Alpha + color2.Alpha, color1.Red + color2.Red, color1.Green + color2.Green, color1.Blue + color2.Blue );
	}

	Color4 Color4::operator - ( Color4 color1, Color4 color2 )
	{
		return Color4( color1.Alpha - color2.Alpha, color1.Red - color2.Red, color1.Green - color2.Green, color1.Blue - color2.Blue );
	}

	Color4 Color4::operator - ( Color4 color )
	{
		return Color4( 1.0f - color.Alpha, 1.0f - color.Red, 1.0f - color.Green, 1.0f - color.Blue );
	}

	Color4 Color4::operator * ( Color4 color1, Color4 color2 )
	{
		return Color4( color1.Alpha * color2.Alpha, color1.Red * color2.Red, color1.Green * color2.Green, color1.Blue * color2.Blue );
	}

	Color4 Color4::operator * ( Color4 color, float scale )
	{
		return Color4( color.Alpha, color.Red * scale, color.Green * scale, color.Blue * scale );
	}

	Color4 Color4::operator * ( float scale, Color4 value )
	{
		return value * scale;
	}

	bool Color4::operator == ( Color4 left, Color4 right )
	{
		return Color4::Equals( left, right );
	}

	bool Color4::operator != ( Color4 left, Color4 right )
	{
		return !Color4::Equals( left, right );
	}

	Color4::operator int( Color4 value )
	{
		return value.ToArgb();
	}

	Color4::operator Color3( Color4 value )
	{
		return value.ToColor3();
	}

	Color4::operator System::Drawing::Color( Color4 value )
	{
		return value.ToColor();
	}

	Color4::operator Vector3( Color4 value )
	{
		return value.ToVector3();
	}

	Color4::operator Vector4( Color4 value )
	{
		return value.ToVector4();
	}

	Color4::operator Color4( int value )
	{
		return Color4( value );
	}

	Color4::operator Color4( Color3 value )
	{
		return Color4( value );
	}

	Color4::operator Color4( System::Drawing::Color value )
	{
		return Color4( value );
	}

	Color4::operator Color4( Vector3 value )
	{
		return Color4( value );
	}

	Color4::operator Color4( Vector4 value )
	{
		return Color4( value );
	}

	String^ Color4::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "A:{0} R:{1} G:{2} B:{3}", 
			Alpha.ToString(CultureInfo::CurrentCulture), Red.ToString(CultureInfo::CurrentCulture), 
			Green.ToString(CultureInfo::CurrentCulture), Blue.ToString(CultureInfo::CurrentCulture) );
	}

	int Color4::GetHashCode()
	{
		return Alpha.GetHashCode() + Red.GetHashCode() + Green.GetHashCode() + Blue.GetHashCode();
	}

	bool Color4::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Color4>( value ) );
	}

	bool Color4::Equals( Color4 value )
	{
		return ( Alpha == value.Alpha && Red == value.Red && Green == value.Green && Blue == value.Blue );
	}

	bool Color4::Equals( Color4% value1, Color4% value2 )
	{
		return ( value1.Alpha == value2.Alpha && value1.Red == value2.Red && value1.Green == value2.Green && value1.Blue == value2.Blue );
	}
}