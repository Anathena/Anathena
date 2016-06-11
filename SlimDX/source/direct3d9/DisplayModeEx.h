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
#include "DisplayModeFilter.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class Direct3D;

		/// <summary>Describes an extended display mode.</summary>
		/// <unmanaged>D3DDISPLAYMODEEX</unmanaged>
		[System::Runtime::InteropServices::StructLayout( System::Runtime::InteropServices::LayoutKind::Sequential )]
		public value class DisplayModeEx : System::IEquatable<DisplayModeEx>
		{
		internal:
			static DisplayModeEx FromUnmanaged( const D3DDISPLAYMODEEX& mode );
			D3DDISPLAYMODEEX ToUnmanaged();

		public:
			/// <summary>
			/// Screen width, in pixels.
			/// </summary>
			property int Width;

			/// <summary>
			/// Screen height, in pixels.
			/// </summary>
            property int Height;

			/// <summary>
			/// Refresh rate of the monitor.
			/// </summary>
            property int RefreshRate;

			/// <summary>
			/// The surface format of the display mode.
			/// </summary>
            property Format Format;

			/// <summary>
			/// The scanline ordering used by the display mode.
			/// </summary>
			property ScanlineOrdering ScanlineOrdering;

			property float AspectRatio
			{
				float get() { return static_cast<float>(Width) / static_cast<float>(Height); }
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( DisplayModeEx left, DisplayModeEx right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( DisplayModeEx left, DisplayModeEx right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to a specified object. 
			/// </summary>
			/// <param name="obj">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to the specified object. 
			/// </summary>
			/// <param name="other">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( DisplayModeEx other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( DisplayModeEx% value1, DisplayModeEx% value2 );
		};

		/// <summary>Implements a read-only collection of <see cref="DisplayMode"/> instances.</summary>
		public ref class DisplayModeExCollection : public System::Collections::ObjectModel::ReadOnlyCollection<DisplayModeEx>
		{
		internal:
			DisplayModeExCollection( IDirect3D9Ex *direct3D, unsigned int adapter, DisplayModeFilter filter );
		};
	}
}