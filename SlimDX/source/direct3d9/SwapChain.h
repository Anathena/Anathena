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

#include "RasterStatus.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class PresentParameters;
		ref class Surface;
		ref class Device;

		/// <summary>Applications use the methods of the SwapChain9 class to manipulate a swap chain.</summary>
		/// <unmanaged>IDirect3DSwapChain9</unmanaged>
		public ref class SwapChain : public ComObject
		{
			COMOBJECT(IDirect3DSwapChain9, SwapChain);

		private protected:
			SwapChain();

		public:
			SwapChain( SlimDX::Direct3D9::Device^ device, PresentParameters^ presentParameters );

			property SlimDX::Direct3D9::Device^ Device
			{
				SlimDX::Direct3D9::Device^ get();
			}

			Surface^ GetBackBuffer( int index );
			Result GetFrontBufferData( Surface^ destinationSurface );

			Result Present( SlimDX::Direct3D9::Present flags );
			Result Present( SlimDX::Direct3D9::Present flags, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle );
			Result Present( SlimDX::Direct3D9::Present flags, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, System::IntPtr windowOverride );
			Result Present( SlimDX::Direct3D9::Present flags, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, System::IntPtr windowOverride, System::Drawing::Region^ region );

			property SlimDX::Direct3D9::DisplayMode DisplayMode
			{
				SlimDX::Direct3D9::DisplayMode get();
			}

			property SlimDX::Direct3D9::RasterStatus RasterStatus
			{
				SlimDX::Direct3D9::RasterStatus get();
			}

			property SlimDX::Direct3D9::PresentParameters^ PresentParameters
			{
				SlimDX::Direct3D9::PresentParameters^ get();
			}
		};
	}
}
