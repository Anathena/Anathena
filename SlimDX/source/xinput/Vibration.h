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

namespace SlimDX
{
	namespace XInput
	{
		/// <summary>
		/// Specifies motor speed levels for the vibration function of a controller.
		/// </summary>
		/// <remarks>
		/// The left motor is the low-frequency rumble motor. The right motor is the high-frequency rumble motor. 
		/// The two motors are not the same, and they create different vibration effects.
		/// </remarks>
		/// <unmanaged>XINPUT_VIBRATION</unmanaged>
		public value class Vibration : System::IEquatable<Vibration>
		{
		private:
			System::UInt16 m_LeftMotorSpeed;
			System::UInt16 m_RightMotorSpeed;
		
		internal:
			Vibration( const XINPUT_VIBRATION& native );
			
			XINPUT_VIBRATION CreateNativeVersion();
			
		public:
			/// <summary>
			/// Gets or sets the left motor speed. 
			/// </summary>
			/// <remarks>
			/// Valid values are in the range 0 to 65,535. Zero signifies no motor use; 65,535 signifies 100% motor use.
			/// </remarks>
			property System::UInt16 LeftMotorSpeed
			{
				System::UInt16 get();
				void set( System::UInt16 value );
			}

			/// <summary>
			/// Gets or sets the right motor speed. 
			/// </summary>
			/// <remarks>
			/// Valid values are in the range 0 to 65,535. Zero signifies no motor use; 65,535 signifies 100% motor use.
			/// </remarks>
			property System::UInt16 RightMotorSpeed
			{
				System::UInt16 get();
				void set( System::UInt16 value );
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( Vibration left, Vibration right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( Vibration left, Vibration right );

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
			virtual bool Equals( Vibration other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( Vibration% value1, Vibration% value2 );
		};
	}
}