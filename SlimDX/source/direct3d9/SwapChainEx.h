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

#include "Enums.h"
#include "DisplayMode.h"
#include "SwapChain.h"
#include "DisplayModeEx.h"
#include "PresentStatistics.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>
		/// Provides extended support for swap chains in Direct3D9Ex.
		/// </summary>
		/// <unmanaged>IDirect3DSwapChain9Ex</unmanaged>
		public ref class SwapChainEx : SwapChain
		{
			COMOBJECT(IDirect3DSwapChain9Ex, SwapChainEx);

		public:
			SwapChainEx( SwapChain^ source );

			/// <summary>
			/// Retrieves the display mode's spatial resolution, color resolution, and refresh frequency.
			/// </summary>
			/// <returns>Extended display mode information about the adapter.</returns>
			/// <unmanaged>IDirect3DSwapChain9Ex::GetDisplayModeEx</unmanaged>
			DisplayModeEx GetDisplayModeEx();

			/// <summary>
			/// Retrieves the display mode's spatial resolution, color resolution, refresh frequency, and rotation settings.
			/// </summary>
			/// <param name="rotation">Receives the rotation setting of the adapter.</param>
			/// <returns>Extended display mode information about the adapter.</returns>
			/// <unmanaged>IDirect3DSwapChain9Ex::GetDisplayModeEx</unmanaged>
			DisplayModeEx GetDisplayModeEx( [Out] DisplayRotation% rotation );

			property int LastPresentCount
			{
				int get();
			}

			/// <summary>
			/// Get presentation performance statistics.
			/// </summary>
			property Direct3D9::PresentStatistics PresentStatistics
			{
				Direct3D9::PresentStatistics get();
			}
		};
	}
}