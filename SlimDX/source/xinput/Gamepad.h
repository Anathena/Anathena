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

#include "Enums.h"

namespace SlimDX
{
	namespace XInput
	{
		/// <summary>
		/// Describes the current state of the Xbox 360 Controller.
		/// </summary>
		/// <remarks>
		/// Each of the thumbstick axis members is a signed value between -32768 and 32767 describing the position of the thumbstick.
		/// A value of 0 is centered. Negative values signify down or to the left. Positive values signify up or to the right.
		/// The constants GamepadLeftThumbDeadZone or GamepadRightThumbDeadZone can be used as a positive and negative value to filter
		/// thumbstick input.
		/// </remarks>
		/// <unmanaged>XINPUT_GAMEPAD</unmanaged>
		public value class Gamepad : System::IEquatable<Gamepad>
		{
		private:
			GamepadButtonFlags m_Buttons;
			System::Byte leftTrigger;
			System::Byte rightTrigger;
			System::Int16 leftThumbX;
			System::Int16 leftThumbY;
			System::Int16 rightThumbX;
			System::Int16 rightThumbY;

		internal:
			Gamepad( const XINPUT_GAMEPAD &native );

		public:
			/// <summary>
			/// Gets a set of flags describing the pressed buttons on the gamepad.
			/// </summary>
			property GamepadButtonFlags Buttons
			{
				GamepadButtonFlags get();
			}

			/// <summary>
			/// Gets the current value of the left trigger analog control. The value is between 0 and 255.
			/// </summary>
            property System::Byte LeftTrigger
            {
                System::Byte get() { return leftTrigger; }
            }

			/// <summary>
			/// Gets the current value of the right trigger analog control. The value is between 0 and 255.
			/// </summary>
            property System::Byte RightTrigger
            {
                System::Byte get() { return rightTrigger; }
            }

			/// <summary>
			/// Gets the left thumbstick y-axis value. The value is between -32768 and 32767.
			/// </summary>
            property System::Int16 LeftThumbX
            {
                System::Int16 get() { return leftThumbX; }
            }

			/// <summary>
			/// Gets the left thumbstick y-axis value. The value is between -32768 and 32767.
			/// </summary>
            property System::Int16 LeftThumbY
            {
                System::Int16 get() { return leftThumbY; }
            }

			/// <summary>
			/// Gets the right thumbstick x-axis value. The value is between -32768 and 32767.
			/// </summary>
            property System::Int16 RightThumbX
            {
                System::Int16 get() { return rightThumbX; }
            }

			/// <summary>
			/// Gets the right thumbstick y-axis value. The value is between -32768 and 32767.
			/// </summary>
            property System::Int16 RightThumbY
            {
                System::Int16 get() { return rightThumbY; }
            }

			/// <summary>
			/// Can be used as a positive and negative value to filter the left thumbstick input.
			/// </summary>
			literal System::Int16 GamepadLeftThumbDeadZone = XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE;

			/// <summary>
			/// Can be used as a positive and negative value to filter the right thumbstick input.
			/// </summary>
			literal System::Int16 GamepadRightThumbDeadZone = XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE;

			/// <summary>
			/// This constant may be used as the value which <see cref="Gamepad::LeftTrigger"/> and <see cref="Gamepad::RightTrigger"/> must be greater than to register as pressed.
			/// </summary>
			literal System::Byte GamepadTriggerThreshold = XINPUT_GAMEPAD_TRIGGER_THRESHOLD;

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( Gamepad left, Gamepad right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( Gamepad left, Gamepad right );

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
			virtual bool Equals( Gamepad other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( Gamepad% value1, Gamepad% value2 );
		};
	}
}