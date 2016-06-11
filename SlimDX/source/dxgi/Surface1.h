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

#include "SurfaceDxgi.h"

namespace SlimDX
{
	namespace DXGI
	{
		/// <summary>
		/// Extends the standard surface interface to provide GDI interop support.
		/// </summary>
		/// <unmanaged>IDXGISurface1</unmanaged>
		public ref class Surface1 : Surface
		{
			COMOBJECT(IDXGISurface1, Surface1);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="Surface1"/> class.
			/// </summary>
			/// <param name="surface">The COM object implementing the IDXGISurface1 interface.</param>
			Surface1( IComObject^ surface );

			/// <summary>
			/// Returns a device context (DC) that allows you to render to a DXGI surface using GDI.
			/// </summary>
			/// <param name="discard">If <c>true</c> the application will not preserve any rendering with GDI; otherwise, <c>false</c>. 
			/// If <c>false</c>, any previous rendering to the device context will be preserved. 
			/// This flag is ideal for simply reading the device context and not doing any rendering to the surface.</param>
			/// <returns>A pointer to an HDC handle that represents the current device context for GDI rendering.</returns>
			System::IntPtr GetDC(bool discard);

			/// <summary>
			/// Releases the GDI device context (DC) associated with the current surface and allows rendering using Direct3D.
			/// </summary>
			/// <param name="dirtyRegion">A pointer to a rectangle that identifies the dirty region of the surface. A dirty region is
			/// any part of the surface that you have used for GDI rendering and that you want to preserve. This is used as a 
			/// performance hint to graphics subsystem in certain scenarios. Do not use this parameter to restrict rendering to 
			/// the specified rectangular region. The area specified by the rectangle will be used as a performance hint to indicate 
			/// what areas have been manipulated by GDI rendering.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ReleaseDC(System::Drawing::Rectangle dirtyRegion);

			/// <summary>
			/// Releases the GDI device context (DC) associated with the current surface and allows rendering using Direct3D.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ReleaseDC();
		};
	}
}
