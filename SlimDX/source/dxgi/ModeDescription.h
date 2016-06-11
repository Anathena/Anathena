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

#include "../math/Rational.h"

#include "Enums.h"

namespace SlimDX
{
	namespace DXGI
	{
		/// <summary>
		/// Describes a display mode.
		/// </summary>
		/// <unmanaged>DXGI_MODE_DESC</unmanaged>
		public value class ModeDescription : System::IEquatable<ModeDescription>
		{
			int m_Width;
			int m_Height;
			Rational m_RefreshRate;
			Format m_Format;
			DisplayModeScanlineOrdering m_ScanlineOrdering;
			DisplayModeScaling m_Scaling;
			
		internal:
			ModeDescription( const DXGI_MODE_DESC& native );
			
			DXGI_MODE_DESC CreateNativeVersion();
			
		public:
			/// <summary>
			/// Gets or sets the display mode width.
			/// </summary>
			property int Width
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets or sets the display mode height.
			/// </summary>
			property int Height
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets or sets the display mode refresh rate.
			/// </summary>
			property Rational RefreshRate
			{
				Rational get();
				void set( Rational value );
			}

			/// <summary>
			/// Gets or sets the display mode format.
			/// </summary>
			property Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}

			/// <summary>
			/// Gets or sets the scanline ordering.
			/// </summary>
			property DisplayModeScanlineOrdering ScanlineOrdering
			{
				DisplayModeScanlineOrdering get();
				void set( DisplayModeScanlineOrdering value );
			}

			/// <summary>
			/// Gets or sets the display mode scaling.
			/// </summary>
			property DisplayModeScaling Scaling
			{
				DisplayModeScaling get();
				void set( DisplayModeScaling value );
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="ModeDescription"/> structure.
			/// </summary>
			/// <param name="width">The width.</param>
			/// <param name="height">The height.</param>
			/// <param name="refreshRate">The refresh rate.</param>
			/// <param name="format">The format.</param>
			ModeDescription( int width, int height, Rational refreshRate, DXGI::Format format );
			
			/// <summary>
			/// Converts the value of the object to its equivalent string representation.
			/// </summary>
			/// <returns>The string representation of the value of this instance.</returns>
			virtual System::String^ ToString() override;
			
			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( ModeDescription left, ModeDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( ModeDescription left, ModeDescription right );

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
			virtual bool Equals( ModeDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( ModeDescription% value1, ModeDescription% value2 );
		};
	}
};