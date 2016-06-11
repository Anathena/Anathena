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

namespace SlimDX
{
	namespace DXGI
	{
		/// <summary>
		/// Provides basic rendering statistics. All counts are relative to machine boot. 
		/// </summary>
		/// <unmanaged href="bb173060">DXGI_FRAME_STATISTICS</unmanaged>
		public value class FrameStatistics : System::IEquatable<FrameStatistics>
		{
			int m_PresentCount;
			int m_PresentRefreshCount;
			int m_SyncRefreshCount;
			System::Int64 m_SyncQPCTime;
			System::Int64 m_SyncGPUTime;
		
		internal:
			FrameStatistics( const DXGI_FRAME_STATISTICS& native );

		public:
			/// <summary>
			/// Gets the number of times an image has been presented to an output.
			/// </summary>
			property int PresentCount
			{
				int get();
			}

			/// <summary>
			/// Gets the number of times a vertical blank has occured.
			/// </summary>
			property int PresentRefreshCount
			{
				int get();
			}

			/// <summary>
			/// Gets the number of times a vertical blank has occured.
			/// </summary>
			property int SyncRefreshCount
			{
				int get();
			}

			/// <summary>
			/// Gets a value equivalent to that returned by the unmanaged function QueryPerformanceCounter.
			/// </summary>
			property System::Int64 SyncQPCTime
			{
				System::Int64 get();
			}

			/// <summary>
			/// Reserved. Always returns 0.
			/// </summary>
			property System::Int64 SyncGPUTime
			{
				System::Int64 get();
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( FrameStatistics left, FrameStatistics right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( FrameStatistics left, FrameStatistics right );

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
			virtual bool Equals( FrameStatistics other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( FrameStatistics% value1, FrameStatistics% value2 );
		};
	}
};