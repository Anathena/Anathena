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
	namespace Direct3D9
	{
		/// <summary>Describes the presentation parameters of a device.</summary>
		/// <unmanaged>D3DPRESENT_PARAMETERS</unmanaged>
		public ref class PresentParameters : System::ICloneable, System::IEquatable<PresentParameters^>
		{
		internal:
			D3DPRESENT_PARAMETERS ToUnmanaged();
			PresentParameters( const D3DPRESENT_PARAMETERS& d3dpp );

			virtual System::Object^ Clone2() = System::ICloneable::Clone
			{
				return Clone();
			}

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="PresentParameters"/> class.
			/// </summary>
			PresentParameters();

			/// <summary>
			/// Gets or sets the width of the back buffer.
			/// </summary>
			property int BackBufferWidth;

			/// <summary>
			/// Gets or sets the height of the back buffer.
			/// </summary>
			property int BackBufferHeight;

			/// <summary>
			/// The back buffer format.
			/// </summary>
			property Format BackBufferFormat;

			/// <summary>
			/// Gets or sets the number of back buffers that will be created.
			/// </summary>
			property int BackBufferCount;

			/// <summary>
			/// Gets or sets the multisampling type.
			/// </summary>
			property MultisampleType Multisample;

			/// <summary>
			/// Gets or sets the quality of the multisampling.
			/// </summary>
			property int MultisampleQuality;

			/// <summary>
			/// Gets or sets the swap effect.
			/// </summary>
			property SwapEffect SwapEffect;

			/// <summary>
			/// Gets or sets the device's window handle.
			/// </summary>
			property System::IntPtr DeviceWindowHandle;

			/// <summary>
			/// Gets or sets a value indicating whether the device will run in windowed or fullscreen mode.
			/// </summary>
			/// <value><c>true</c> to run in windowed mode, <c>false</c> to run in fullscreen mode.</value>
			property bool Windowed;

			/// <summary>
			/// Gets or sets a value indicating whether a depth-stencil surface should be automatically created for the device.
			/// </summary>
			property bool EnableAutoDepthStencil;

			/// <summary>
			/// Gets or sets the format of the automatic depth-stencil buffer.
			/// </summary>
			property Format AutoDepthStencilFormat;

			/// <summary>
			/// Gets or sets flags describing how the device should present its data.
			/// </summary>
			property PresentFlags PresentFlags;

			/// <summary>
			/// Gets or sets the fullscreen refresh rate, in hertz.
			/// </summary>
			property int FullScreenRefreshRateInHertz;

			/// <summary>
			/// Gets or sets the presentation interval.
			/// </summary>
			property PresentInterval PresentationInterval;

			/// <summary>
			/// Clones the instance and returns a new object containing the same values.
			/// </summary>
			/// <returns>A new <see cref="PresentParameters"/> object containing the same values as the current instance.</returns>
			PresentParameters^ Clone();

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( PresentParameters^ left, PresentParameters^ right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( PresentParameters^ left, PresentParameters^ right );

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
			virtual bool Equals( PresentParameters^ other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( PresentParameters^ value1, PresentParameters^ value2 );
		};
	}
}