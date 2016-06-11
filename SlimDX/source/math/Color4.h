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
#pragma once

#include "../design/Color4Converter.h"

struct D3DXCOLOR;

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	value class Color3;
	value class Vector3;
	value class Vector4;
		
	/// <summary>
	/// A four-component (RGBA) color value; each component is a float in the range [0,1].
	/// </summary>
	[System::Serializable]
	[System::Runtime::InteropServices::StructLayout(System::Runtime::InteropServices::LayoutKind::Sequential)]
	[System::ComponentModel::TypeConverter( SlimDX::Design::Color4Converter::typeid )]
	public value class Color4
	{
	internal:
		static Color4 FromUnmanaged( const D3DXCOLOR &color );
		D3DXCOLOR ToUnmanaged();

	public:
		/// <summary>
		/// Gets or sets the color's red component.
		/// </summary>
		float Red;

		/// <summary>
		/// Gets or sets the color's green component.
		/// </summary>
		float Green;

		/// <summary>
		/// Gets or sets the color's blue component.
		/// </summary>
		float Blue;

		/// <summary>
		/// Gets or sets the color's alpha component.
		/// </summary>
		float Alpha;

		/// <summary>
		/// Initializes a new instance of the <see cref="Color4"/> structure.
		/// </summary>
		/// <param name="alpha">The alpha component of the color.</param>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		Color4( float alpha, float red, float green, float blue );

		/// <summary>
		/// Initializes a new instance of the <see cref="Color4"/> structure.
		/// </summary>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		Color4( float red, float green, float blue );

		/// <summary>
		/// Initializes a new instance of the <see cref="Color4"/> structure.
		/// </summary>
		/// <param name="color">The color whose components should be converted.</param>
		Color4( System::Drawing::Color color );

		/// <summary>
		/// Initializes a new instance of the <see cref="Color4"/> structure.
		/// </summary>
		/// <param name="color">The color whose components should be converted.</param>
		Color4( Color3 color );

		/// <summary>
		/// Initializes a new instance of the <see cref="Color4"/> structure.
		/// </summary>
		/// <param name="color">The color whose components should be converted.</param>
		Color4( Vector3 color );

		/// <summary>
		/// Initializes a new instance of the <see cref="Color4"/> structure.
		/// </summary>
		/// <param name="color">The color whose components should be converted.</param>
		Color4( Vector4 color );

		/// <summary>
		/// Initializes a new instance of the <see cref="Color4"/> structure.
		/// </summary>
		/// <param name="argb">A packed integer containing all four color components.</param>
		Color4( int argb );

		/// <summary>
		/// Converts the color to a a 3 component (RGB) color.
		/// </summary>
		/// <returns>The 3 component (RGB) color that is equivalent to this instance, without the alpha channel.</returns>
		Color3 ToColor3();

		/// <summary>
		/// Converts the color to a <see cref="System::Drawing::Color"/>.
		/// </summary>
		/// <returns>The <see cref="System::Drawing::Color"/> that is equivalent to this instance.</returns>
		System::Drawing::Color ToColor();

		/// <summary>
		/// Converts the color into a packed integer.
		/// </summary>
		/// <returns>A packed integer containing all four color components.</returns>
		int ToArgb();

		/// <summary>
		/// Converts the color into a three component vector.
		/// </summary>
		/// <returns>A three component vector containing the red, green, and blue components of the color.</returns>
		Vector3 ToVector3();

		/// <summary>
		/// Converts the color into a four component vector.
		/// </summary>
		/// <returns>A four component vector containing all four color components.</returns>
		Vector4 ToVector4();

		/// <summary>
		/// Adds two colors.
		/// </summary>
		/// <param name="color1">The first color to add.</param>
		/// <param name="color2">The second color to add.</param>
		/// <returns>The sum of the two colors.</returns>
		static Color4 Add( Color4 color1, Color4 color2 );

		/// <summary>
		/// Adds two colors.
		/// </summary>
		/// <param name="color1">The first color to add.</param>
		/// <param name="color2">The second color to add.</param>
		/// <param name="result">When the method completes, contains the sum of the two colors.</param>
		static void Add( Color4% color1, Color4% color2, [Out] Color4% result );

		/// <summary>
		/// Subtracts two colors.
		/// </summary>
		/// <param name="color1">The first color to subtract.</param>
		/// <param name="color2">The second color to subtract.</param>
		/// <returns>The difference between the two colors.</returns>
		static Color4 Subtract( Color4 color1, Color4 color2 );

		/// <summary>
		/// Subtracts two colors.
		/// </summary>
		/// <param name="color1">The first color to subtract.</param>
		/// <param name="color2">The second color to subtract.</param>
		/// <param name="result">When the method completes, contains the difference between the two colors.</param>
		static void Subtract( Color4% color1, Color4% color2, [Out] Color4% result );

		/// <summary>
		/// Modulates two colors.
		/// </summary>
		/// <param name="color1">The first color to modulate.</param>
		/// <param name="color2">The second color to modulate.</param>
		/// <returns>The modulation of the two colors.</returns>
		static Color4 Modulate( Color4 color1, Color4 color2 );

		/// <summary>
		/// Modulates two colors.
		/// </summary>
		/// <param name="color1">The first color to modulate.</param>
		/// <param name="color2">The second color to modulate.</param>
		/// <param name="result">When the method completes, contains the modulation of the two colors.</param>
		static void Modulate( Color4% color1, Color4% color2, [Out] Color4% result );

		/// <summary>
		/// Performs a linear interpolation between two colors.
		/// </summary>
		/// <param name="color1">Start color.</param>
		/// <param name="color2">End color.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="color2"/>.</param>
		/// <returns>The linear interpolation of the two colors.</returns>
		/// <remarks>
		/// This method performs the linear interpolation based on the following formula.
		/// <code>color1 + (color2 - color1) * amount</code>
		/// Passing <paramref name="amount"/> a value of 0 will cause <paramref name="color1"/> to be returned; a value of 1 will cause <paramref name="color2"/> to be returned. 
		/// </remarks>
		static Color4 Lerp( Color4 color1, Color4 color2, float amount );

		/// <summary>
		/// Performs a linear interpolation between two colors.
		/// </summary>
		/// <param name="color1">Start color.</param>
		/// <param name="color2">End color.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="color2"/>.</param>
		/// <param name="result">When the method completes, contains the linear interpolation of the two colors.</param>
		/// <remarks>
		/// This method performs the linear interpolation based on the following formula.
		/// <code>color1 + (color2 - color1) * amount</code>
		/// Passing <paramref name="amount"/> a value of 0 will cause <paramref name="color1"/> to be returned; a value of 1 will cause <paramref name="color2"/> to be returned. 
		/// </remarks>
		static void Lerp( Color4% color1, Color4% color2, float amount, [Out] Color4% result );

		/// <summary>
		/// Negates a color.
		/// </summary>
		/// <param name="color">The color to negate.</param>
		/// <returns>The negated color.</returns>
		static Color4 Negate( Color4 color );

		/// <summary>
		/// Negates a color.
		/// </summary>
		/// <param name="color">The color to negate.</param>
		/// <param name="result">When the method completes, contains the negated color.</param>
		static void Negate( Color4% color, [Out] Color4% result );

		/// <summary>
		/// Scales a color by the specified amount.
		/// </summary>
		/// <param name="color">The color to scale.</param>
		/// <param name="scale">The amount by which to scale.</param>
		/// <returns>The scaled color.</returns>
		static Color4 Scale( Color4 color, float scale );

		/// <summary>
		/// Scales a color by the specified amount.
		/// </summary>
		/// <param name="color">The color to scale.</param>
		/// <param name="scale">The amount by which to scale.</param>
		/// <param name="result">When the method completes, contains the scaled color.</param>
		static void Scale( Color4% color, float scale, [Out] Color4% result );

		/// <summary>
		/// Adjusts the contrast of a color.
		/// </summary>
		/// <param name="color">The color whose contrast is to be adjusted.</param>
		/// <param name="contrast">The amount by which to adjust the contrast.</param>
		/// <returns>The adjusted color.</returns>
		static Color4 AdjustContrast( Color4 color, float contrast );

		/// <summary>
		/// Adjusts the contrast of a color.
		/// </summary>
		/// <param name="color">The color whose contrast is to be adjusted.</param>
		/// <param name="contrast">The amount by which to adjust the contrast.</param>
		/// <param name="result">When the method completes, contains the adjusted color.</param>
		static void AdjustContrast( Color4% color, float contrast, [Out] Color4% result );

		/// <summary>
		/// Adjusts the saturation of a color.
		/// </summary>
		/// <param name="color">The color whose saturation is to be adjusted.</param>
		/// <param name="saturation">The amount by which to adjust the saturation.</param>
		/// <returns>The adjusted color.</returns>
		static Color4 AdjustSaturation( Color4 color, float saturation );

		/// <summary>
		/// Adjusts the saturation of a color.
		/// </summary>
		/// <param name="color">The color whose saturation is to be adjusted.</param>
		/// <param name="saturation">The amount by which to adjust the saturation.</param>
		/// <param name="result">When the method completes, contains the adjusted color.</param>
		static void AdjustSaturation( Color4% color, float saturation, [Out] Color4% result );

		/// <summary>
		/// Adds two colors.
		/// </summary>
		/// <param name="left">The first color to add.</param>
		/// <param name="right">The second color to add.</param>
		/// <returns>The sum of the two colors.</returns>
		static Color4 operator + ( Color4 left, Color4 right );

		/// <summary>
		/// Subtracts two colors.
		/// </summary>
		/// <param name="left">The first color to subtract.</param>
		/// <param name="right">The second color to subtract.</param>
		/// <returns>The difference between the two colors.</returns>
		static Color4 operator - ( Color4 left, Color4 right );

		/// <summary>
		/// Negates a color.
		/// </summary>
		/// <param name="value">The color to negate.</param>
		/// <returns>The negated color.</returns>
		static Color4 operator - ( Color4 value );

		/// <summary>
		/// Scales a color by the specified amount.
		/// </summary>
		/// <param name="value">The color to scale.</param>
		/// <param name="scale">The amount by which to scale.</param>
		/// <returns>The scaled color.</returns>
		static Color4 operator * ( Color4 value, float scale );

		/// <summary>
		/// Scales a color by the specified amount.
		/// </summary>
		/// <param name="value">The color to scale.</param>
		/// <param name="scale">The amount by which to scale.</param>
		/// <returns>The scaled color.</returns>
		static Color4 operator * ( float scale, Color4 value );

		/// <summary>
		/// Modulates two colors.
		/// </summary>
		/// <param name="color1">The first color to modulate.</param>
		/// <param name="color2">The second color to modulate.</param>
		/// <returns>The modulation of the two colors.</returns>
		static Color4 operator * ( Color4 color1, Color4 color2 );

		/// <summary>
		/// Tests for equality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator == ( Color4 left, Color4 right );

		/// <summary>
		/// Tests for inequality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator != ( Color4 left, Color4 right );

		/// <summary>
		/// Performs an explicit conversion from <see cref="Color4"/> to <see cref="System::Int32"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static explicit operator int( Color4 value );

		/// <summary>
		/// Performs an explicit conversion from <see cref="Color4"/> to <see cref="Color3"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static explicit operator Color3( Color4 value );

		/// <summary>
		/// Performs an explicit conversion from <see cref="Color4"/> to <see cref="Vector3"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static explicit operator Vector3( Color4 value );

		/// <summary>
		/// Performs an explicit conversion from <see cref="Color4"/> to <see cref="Vector4"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static explicit operator Vector4( Color4 value );

		/// <summary>
		/// Performs an explicit conversion from <see cref="System::Int32"/> to <see cref="Color4"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static explicit operator Color4( int value );

		/// <summary>
		/// Performs an explicit conversion from <see cref="Color3"/> to <see cref="Color4"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static explicit operator Color4( Color3 value );

		/// <summary>
		/// Performs an explicit conversion from <see cref="Vector3"/> to <see cref="Color4"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static explicit operator Color4( Vector3 value );

		/// <summary>
		/// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="Color4"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static explicit operator Color4( Vector4 value );

		/// <summary>
		/// Performs an implicit conversion from <see cref="System::Drawing::Color"/> to <see cref="Color4"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static operator Color4( System::Drawing::Color value );

		/// <summary>
		/// Performs an implicit conversion from <see cref="Color4"/> to <see cref="System::Drawing::Color"/>.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The converted value.</returns>
		static explicit operator System::Drawing::Color( Color4 value );

		/// <summary>
		/// Converts the value of the object to its equivalent string representation.
		/// </summary>
		/// <returns>The string representation of the value of this instance.</returns>
		virtual System::String^ ToString() override;

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		virtual int GetHashCode() override;

		/// <summary>
		/// Returns a value that indicates whether the current instance is equal to a specified object. 
		/// </summary>
		/// <param name="obj">Object to make the comparison with.</param>
		/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
		virtual bool Equals( System::Object^ obj ) override;

		/// <summary>
		/// Returns a value that indicates whether the current instance is equal to the specified object. 
		/// </summary>
		/// <param name="other">Object to make the comparison with.</param>
		/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
		virtual bool Equals( Color4 other );

		/// <summary>
		/// Determines whether the specified object instances are considered equal. 
		/// </summary>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
		/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
		static bool Equals( Color4% value1, Color4% value2 );
	};
}