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

namespace SlimDX
{
	namespace Direct3D10
	{
		/// <summary>
		/// Represents a 3D region of a resource's data.
		/// </summary>
		/// <unmanaged>D3D10_BOX</unmanaged>
		public value class ResourceRegion : System::IEquatable<ResourceRegion>
		{
		private:
			UINT m_Left;
			UINT m_Top;
			UINT m_Front;
			UINT m_Right;
			UINT m_Bottom;
			UINT m_Back;

		internal:
			ResourceRegion( const D3D10_BOX& native );
			
			D3D10_BOX CreateNativeVersion();
			
		public:
			/// <summary>
			/// Gets or sets the left boundary of the region.
			/// </summary>
			property int Left
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Gets or sets the top boundary of the region.
			/// </summary>
			property int Top
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Gets or sets the front boundary of the region.
			/// </summary>
			property int Front
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Gets or sets the right boundary of the region.
			/// </summary>
			property int Right
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Gets or sets the bottom boundary of the region.
			/// </summary>
			property int Bottom
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Gets or sets the back boundary of the region.
			/// </summary>
			property int Back
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Tests for equality between two ResourceRegions.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( ResourceRegion left, ResourceRegion right );
			
			/// <summary>
			/// Tests for inequality between two ResourceRegions.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( ResourceRegion left, ResourceRegion right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;
			
			/// <summary>
			/// Returns a value indicating whether this instance is equal to the specified object.
			/// </summary>
			/// <param name="obj">An object to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="obj"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( System::Object^ obj ) override;
			
			/// <summary>
			/// Returns a value indicating whether this instance is equal to the specified object.
			/// </summary>
			/// <param name="other">A <see cref="ResourceRegion"/> to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( ResourceRegion other );
			
			/// <summary>
			/// Returns a value indicating whether the two ResourceRegions are equivalent.
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> has the same value as <paramref name="value2"/>; otherwise, <c>false</c>.</returns>
			static bool Equals( ResourceRegion% value1, ResourceRegion% value2 );
		};
	}
};