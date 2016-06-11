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
#include "DeviceChildDxgi.h"

namespace SlimDX
{
	ref class DataRectangle;
	value class Result;

	namespace DXGI
	{
		value class SurfaceDescription;
		ref class SwapChain;

		/// <summary>
		/// A <see cref="Surface"/> implements methods for image-data objects.
		/// </summary>
		/// <unmanaged>IDXGISurface</unmanaged>
		public ref class Surface : DeviceChild
		{
			COMOBJECT(IDXGISurface, Surface);

		private protected:
			Surface() { }

		public:
			/// <summary>
			/// Gets a swap chain back buffer.
			/// </summary>
			/// <param name="swapChain">The swap chain to get the buffer from.</param>
			/// <param name="index">The index of the desired buffer.</param>
			/// <returns>The buffer interface, or <c>null</c> on failure.</returns>
			static Surface^ Surface::FromSwapChain( SlimDX::DXGI::SwapChain^ swapChain, int index );

			/// <summary>
			/// Gets the surface's description.
			/// </summary>
			property SurfaceDescription Description
			{
				SurfaceDescription get();
			}

			/// <summary>
			/// Acquires access to the surface data.
			/// </summary>
			/// <param name="flags">Flags specifying CPU access permissions.</param>
			/// <returns>A <see cref="SlimDX::DataRectangle"/> for accessing the mapped data, or <c>null</c> on failure.</returns>.
			DataRectangle^ Map( MapFlags flags );
			
			/// <summary>
			/// Relinquishes access to the surface data.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Unmap();
		};
	}
}