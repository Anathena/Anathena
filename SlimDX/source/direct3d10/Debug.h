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

#include "../ComObject.h"

#include "../dxgi/SwapChainDxgi.h"

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class Device;
		
		/// <summary>
		/// An interface for controlling debug state and validating pipeline settings.
		/// </summary>
		/// <unmanaged>ID3D10Debug</unmanaged>
		public ref class Debug : public ComObject
		{
			COMOBJECT(ID3D10Debug, Debug);
		
		public:
			/// <summary>
			/// Gets or sets the flags indicating which debug features are enabled.
			/// </summary>
			property DebugFeatureFlags FeatureFlags
			{
				DebugFeatureFlags get();
				void set( DebugFeatureFlags value );
			}
			
			/// <summary>
			/// Gets or sets the number of milliseconds to delay after a swap chain is presented.
			/// </summary>
			property int PresentDelay
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Gets or sets the swap chain the runtime will use for automatic presentation.
			/// </summary>
			property DXGI::SwapChain^ SwapChain
			{
				DXGI::SwapChain^ get();
				void set( DXGI::SwapChain^ value );
			}
			
			/// <summary>
			/// Constructs a new Debug interface.
			/// </summary>
			/// <param name="device">The Device with which the Debug interface is to be associated.</param>
			Debug( Device^ device );
			
			/// <summary>
			/// Validates the current pipeline state.
			/// </summary>
			/// <returns>A Result code.</returns>
			Result ValidatePipelineState();
		};
	}
};