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

#include "Enums11.h"
#include "DeviceChild11.h"

namespace SlimDX
{
	namespace Direct3D11
	{	
		ref class Device;
		value class DepthStencilStateDescription;
		
		/// <summary>
		/// A state object defining the behavior of the depth and stencil test stages of the output merger. 
		/// </summary>
		/// <unmanaged>ID3D11DepthStencilState</unmanaged>
		public ref class DepthStencilState : public DeviceChild
		{
			COMOBJECT(ID3D11DepthStencilState, DepthStencilState);
		
		public:
			/// <summary>
			/// Gets the state's description.
			/// </summary>
			property DepthStencilStateDescription Description
			{
				DepthStencilStateDescription get();
			}
			
			/// <summary>
			/// Constructs a new DepthStencilState based on the specified description. If the description
			/// is identical to that of an existing DepthStencilState object, the existing DepthStencilState object
			/// is returned instead of a new instance.
			/// </summary>
			/// <param name="device">The device to associate the state object with</param>
			/// <param name="description">The state description.</param>
			/// <returns>The DepthStencilState object.</returns>
			static DepthStencilState^ FromDescription( SlimDX::Direct3D11::Device^ device, DepthStencilStateDescription description );
		};
	}
};