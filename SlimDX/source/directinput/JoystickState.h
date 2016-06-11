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
	namespace DirectInput
	{
		/// <summary>
		/// Describes the state of a joystick device.
		/// </summary>
		/// <unmanaged>DIJOYSTATE2</unmanaged>
		public ref class JoystickState
		{
		internal:
			int x;
			int y;
			int z;
			int rx;
			int ry;
			int rz;
			array<int>^ sliders;
			array<int>^ povs;
			array<bool>^ pressedButtons;
			array<bool>^ releasedButtons;
			int vx;
			int vy;
			int vz;
			int vrx;
			int vry;
			int vrz;
			array<int>^ vsliders;
			int ax;
			int ay;
			int az;
			int arx;
			int ary;
			int arz;
			array<int>^ asliders;
			int fx;
			int fy;
			int fz;
			int frx;
			int fry;
			int frz;
			array<int>^ fsliders;

			JoystickState( const DIJOYSTATE2 &joystate );

		public:
			JoystickState();

			/// <summary>
			/// Gets the state of each point-of-view controller on the joystick.
			/// </summary>
			array<int>^ GetPointOfViewControllers()
			{
				return povs;
			}

			/// <summary>
			/// Gets the position of each slider on the joystick.
			/// </summary>
			array<int>^ GetSliders()
			{
				return sliders;
			}

			/// <summary>
			/// Gets the velocity of each slider on the joystick.
			/// </summary>
			array<int>^ GetVelocitySliders()
			{
				return vsliders;
			}

			/// <summary>
			/// Gets the acceleration of each slider on the joystick.
			/// </summary>
			array<int>^ GetAccelerationSliders()
			{
				return asliders;
			}

			/// <summary>
			/// Gets the force of each slider on the joystick.
			/// </summary>
			array<int>^ GetForceSliders()
			{
				return fsliders;
			}

			/// <summary>
			/// Gets the state of each button on the joystick.
			/// </summary>
			array<bool>^ GetButtons()
			{
				return pressedButtons;
			}

			/// <summary>
			/// Gets the X-axis, usually the left-right movement of a stick.
			/// </summary>
			property int X
			{
				int get() { return x; }
			}

			/// <summary>
			/// Gets the Y-axis, usually the forward-backward movement of a stick.
			/// </summary>
			property int Y
			{
				int get() { return y; }
			}

			/// <summary>
			/// Gets the Z-axis, often the throttle control.
			/// </summary>
			property int Z
			{
				int get() { return z; }
			}

			/// <summary>
			/// Gets the X-axis rotation.
			/// </summary>
			property int RotationX
			{
				int get() { return rx; }
			}

			/// <summary>
			/// Gets the Y-axis rotation.
			/// </summary>
			property int RotationY
			{
				int get() { return ry; }
			}

			/// <summary>
			/// Gets the Z-axis rotation.
			/// </summary>
			property int RotationZ
			{
				int get() { return rz; }
			}

			/// <summary>
			/// Gets the X-axis velocity.
			/// </summary>
			property int VelocityX
			{
				int get() { return vx; }
			}

			/// <summary>
			/// Gets the Y-axis velocity.
			/// </summary>
			property int VelocityY
			{
				int get() { return vy; }
			}

			/// <summary>
			/// Gets the Z-axis velocity.
			/// </summary>
			property int VelocityZ
			{
				int get() { return vz; }
			}

			/// <summary>
			/// Gets the X-axis angular velocity.
			/// </summary>
			property int AngularVelocityX
			{
				int get() { return vrx; }
			}

			/// <summary>
			/// Gets the Y-axis angular velocity.
			/// </summary>
			property int AngularVelocityY
			{
				int get() { return vry; }
			}

			/// <summary>
			/// Gets the Z-axis angular velocity.
			/// </summary>
			property int AngularVelocityZ
			{
				int get() { return vrz; }
			}

			/// <summary>
			/// Gets the X-axis acceleration.
			/// </summary>
			property int AccelerationX
			{
				int get() { return ax; }
			}

			/// <summary>
			/// Gets the Y-axis acceleration.
			/// </summary>
			property int AccelerationY
			{
				int get() { return ay; }
			}

			/// <summary>
			/// Gets the Z-axis acceleration.
			/// </summary>
			property int AccelerationZ
			{
				int get() { return az; }
			}

			/// <summary>
			/// Gets the X-axis angular acceleration.
			/// </summary>
			property int AngularAccelerationX
			{
				int get() { return arx; }
			}

			/// <summary>
			/// Gets the Y-axis angular acceleration.
			/// </summary>
			property int AngularAccelerationY
			{
				int get() { return ary; }
			}

			/// <summary>
			/// Gets the Z-axis angular acceleration.
			/// </summary>
			property int AngularAccelerationZ
			{
				int get() { return arz; }
			}

			/// <summary>
			/// Gets the X-axis force.
			/// </summary>
			property int ForceX
			{
				int get() { return fx; }
			}

			/// <summary>
			/// Gets the Y-axis force.
			/// </summary>
			property int ForceY
			{
				int get() { return fy; }
			}

			/// <summary>
			/// Gets the Z-axis force.
			/// </summary>
			property int ForceZ
			{
				int get() { return fz; }
			}

			/// <summary>
			/// Gets the X-axis torque.
			/// </summary>
			property int TorqueX
			{
				int get() { return frx; }
			}

			/// <summary>
			/// Gets the Y-axis torque.
			/// </summary>
			property int TorqueY
			{
				int get() { return fry; }
			}

			/// <summary>
			/// Gets the Z-axis torque.
			/// </summary>
			property int TorqueZ
			{
				int get() { return frz; }
			}

			bool IsPressed( int button ) { return pressedButtons[button]; }
			bool IsReleased( int button ) { return releasedButtons[button]; }

		internal:
			void AssignState( const DIJOYSTATE2 &joystate );
		};
	}
}
