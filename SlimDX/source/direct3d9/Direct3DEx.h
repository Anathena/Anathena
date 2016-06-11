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

#include "Direct3D.h"
#include "DisplayModeEx.h"
#include "AdapterInformationEx.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D9
	{
		public ref class Direct3DEx : Direct3D
		{
			COMOBJECT_CUSTOM(IDirect3D9Ex, Direct3DEx);

		private:
			AdapterExCollection^ adaptersEx;

		public:
			Direct3DEx();

			/// <summary>
			/// Gets a collection of installed extended adapters.
			/// </summary>
			property AdapterExCollection^ AdaptersEx
			{
				AdapterExCollection^ get() { return adaptersEx; }
			}

			/// <summary>
			/// Retrieves the current extended display mode for the specified adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <returns>The current extended display mode for the specified adapter.</returns>
            DisplayModeEx GetAdapterDisplayModeEx( int adapter );

			/// <summary>
			/// Retrieves the current extended display mode for the specified adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <returns>The current extended display mode for the specified adapter.</returns>
            DisplayModeEx GetAdapterDisplayModeEx( int adapter, [Out] DisplayRotation% rotation );

			/// <summary>
			/// Returns a unique identifier for the adapter that is specific to the adapter hardware.
			/// Applications can use this identifier to define robust mappings across various APIs (Direct3D 9, DXGI).
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <returns>A unique identifier for the given adapter.</returns>
			System::Int64 GetAdapterLuid( int adapter );

			/// <summary>
			/// Returns the number of display modes available on the specified adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="filter">The desired characteristics of the display mode.</param>
			/// <returns>The number of display modes available on the specified adapter.</returns>
            int GetAdapterModeCountEx( int adapter, DisplayModeFilter filter );

			/// <summary>
			/// Returns the actual display mode info based on the given mode index.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="format">The desired characteristics of the display mode.</param>
			/// <param name="modeIndex">Index of the desired display mode.</param>
			/// <returns>The extended display mode that matches the specified information.</returns>
            DisplayModeEx EnumerateAdapterModesEx( int adapter, DisplayModeFilter format, int modeIndex );
		};
	}
}
