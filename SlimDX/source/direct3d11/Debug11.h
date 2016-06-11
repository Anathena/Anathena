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

#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		ref class DeviceContext;
		
		/// <summary>
		/// An interface for controlling debug state and validating pipeline settings.
		/// </summary>
		/// <unmanaged>ID3D11Debug</unmanaged>
		public ref class Debug : public ComObject
		{
			COMOBJECT(ID3D11Debug, Debug);
		
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
			/// Initializes a new instance of the <see cref="Debug"/> class.
			/// </summary>
			/// <param name="device">The device with which the debug interface is to be associated.</param>
			Debug( Device^ device );
			
			/// <summary>
			/// Validates the current pipeline state of a device context.
			/// </summary>
			/// <param name="context">The device context to validate.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ValidateContext(DeviceContext^ context);

			/// <summary>
			/// Validates the device context for dispatching operations.
			/// </summary>
			/// <param name="context">The device context to validate.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ValidateContextForDispatch(DeviceContext^ context);

			/// <summary>
			/// Reports information about live device objects.
			/// </summary>
			/// <param name="level">Options for specifying the amount of detail in the output.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ReportLiveDeviceObjects(ReportingLevel level);
		};
	}
};