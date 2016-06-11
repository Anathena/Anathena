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

#include "BlendState.h"

namespace SlimDX
{
	namespace Direct3D10_1
	{
		ref class Device1;
		value class BlendStateDescription1;

		/// <summary>
		/// A state object defining the behavior of the pixel-blending stage of the output merger. 
		/// </summary>
		/// <unmanaged>ID3D10BlendState1</unmanaged>
		public ref class BlendState1 : public SlimDX::Direct3D10::BlendState
		{
			COMOBJECT_CUSTOM(ID3D10BlendState1, BlendState1);
		
		public:
			/// <summary>
			/// Gets the state's description.
			/// </summary>
			property BlendStateDescription1 Description
			{
				BlendStateDescription1 get() new;
			}
			
			/// <summary>
			/// Constructs a new BlendState1 based on the specified description. If the description
			/// is identical to that of an existing BlendState object, the existing BlendState object
			/// is returned instead of a new instance.
			/// </summary>
			/// <param name="device">The device to associate the state object with</param>
			/// <param name="description">The state description.</param>
			/// <returns>The BlendState object.</returns>
			static BlendState1^ FromDescription( Device1^ device, BlendStateDescription1 description );
		};
	}
}