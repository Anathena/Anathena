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
	namespace Direct3D11
	{
		/// <summary>
		/// Contains query information about the reliability of a timestamp query.
		/// </summary>
		/// <unmanaged>D3D11_QUERY_DATA_TIMESTAMP_DISJOINT</unmanaged>
		public value class TimestampQueryData : System::IEquatable<TimestampQueryData>
		{
		internal:
			TimestampQueryData( const D3D11_QUERY_DATA_TIMESTAMP_DISJOINT& native );
			
			D3D11_QUERY_DATA_TIMESTAMP_DISJOINT CreateNativeVersion();
			
		public:
			/// <summary>
			/// How frequently the GPU counter increments in Hz.
			/// </summary>
			property System::Int64 Frequency;

			/// <summary>
			/// If this is <c>true</c>, something occurred in between the query's Begin and End calls that caused the 
			/// timestamp counter to become discontinuous or disjoint, such as unplugging the AC chord on a laptop, overheating, or throttling up/down due to laptop savings events. 
			/// The timestamp returned by GetData for a timestamp query is only reliable if IsDisjointed is <c>false</c>.
			/// </summary>
			property bool IsDisjointed;
		
			/// <summary>
			/// Tests for equality between two TimestampQueryDatas.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( TimestampQueryData left, TimestampQueryData right );
			
			/// <summary>
			/// Tests for inequality between two TimestampQueryDatas.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( TimestampQueryData left, TimestampQueryData right );
			
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
			/// <param name="other">A <see cref="TimestampQueryData"/> to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( TimestampQueryData other );

			/// <summary>
			/// Returns a value indicating whether the two BufferDescriptions are equivalent.
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> has the same value as <paramref name="value2"/>; otherwise, <c>false</c>.</returns>
			static bool Equals( TimestampQueryData% value1, TimestampQueryData% value2 );
		};
	}
};