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
	namespace DirectInput
	{
		/// <summary>
		/// Defines possible joystick object codes.
		/// </summary>
		public ref class JoystickObjects sealed
		{
		private:
			JoystickObjects() { }

		public:
			/// <summary>
			/// The rotational X axis.
			/// </summary>
			literal int XAxisRotation = DIJOFS_RX;

			/// <summary>
			/// The rotational X axis.
			/// </summary>
			literal int YAxisRotation = DIJOFS_RY;

			/// <summary>
			/// The rotational X axis.
			/// </summary>
			literal int ZAxisRotation = DIJOFS_RZ;

			/// <summary>
			/// The rotational X axis.
			/// </summary>
			literal int XAxis = DIJOFS_X;

			/// <summary>
			/// The rotational X axis.
			/// </summary>
			literal int YAxis = DIJOFS_Y;

			/// <summary>
			/// The rotational X axis.
			/// </summary>
			literal int ZAxis = DIJOFS_Z;

			/// <summary>
			/// Provides scan codes for the specified button.
			/// </summary>
			static int Button( int buttonId ) { return DIJOFS_BUTTON( buttonId ); }

			/// <summary>
			/// Provides scan codes for the specified slider.
			/// </summary>
			static int Slider( int sliderId ) { return DIJOFS_SLIDER( sliderId ); }

			/// <summary>
			/// Provides scan codes for the specified Point-Of-View controller.
			/// </summary>
			static int PointOfView( int povId ) { return DIJOFS_POV( povId ); }
		};
	}
}