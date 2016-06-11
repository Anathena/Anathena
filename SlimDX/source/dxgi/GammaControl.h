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

#include "../math/Color3.h"

namespace SlimDX
{
	namespace DXGI
	{
		/// <summary>
		/// Describes gamma control settings.
		/// </summary>
		/// <unmanaged>DXGI_GAMMA_CONTROL</unmanaged>
		public ref class GammaControl
		{
			Color3 m_Scale;
			Color3 m_Offset;
			array<Color3>^ m_GammaCurve;

		internal:			
			DXGI_GAMMA_CONTROL CreateNativeVersion();
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="GammaControl"/> class.
			/// </summary>
			GammaControl();

			/// <summary>
			/// Gets or sets a scaling factor applied to gamma RGB values.
			/// </summary>
			property Color3 Scale
			{
				Color3 get();
				void set( Color3 value );
			}
			
			/// <summary>
			/// Gets or sets an offset applied to gamma RGB values.
			/// </summary>
			property Color3 Offset
			{
				Color3 get();
				void set( Color3 value );
			}
			
			/// <summary>
			/// Gets the list of RGB control points defining the gamma curve.
			/// </summary>
			property array<Color3>^ ControlPoints
			{
				array<Color3>^ get();
			}
		};
	}
};