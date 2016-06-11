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
	namespace DXGI
	{
		/// <summary>
		/// Describes various properties of an Adapter.
		/// </summary>
		/// <unmanaged>DXGI_ADAPTER_DESC</unmanaged>
		public value class AdapterDescription : System::IEquatable<AdapterDescription>
		{
			System::String^ m_Description;
			int m_VendorId;
			int m_DeviceId;
			int m_SubSysId;
			int m_Revision;
			SIZE_T m_DedicatedVideoMemory;
			SIZE_T m_DedicatedSystemMemory;
			SIZE_T m_SharedSystemMemory;
			System::Int64 m_Luid;

		internal:
			AdapterDescription( const DXGI_ADAPTER_DESC& native );

		public:
			/// <summary>
			/// Gets the adapter's description.
			/// </summary>
			property System::String^ Description
			{
				System::String^ get();
			}
			
			/// <summary>
			/// Gets the adapter's vendor ID.
			/// </summary>
			property int VendorId
			{
				int get();
			}

			/// <summary>
			/// Gets the adapter's device ID.
			/// </summary>
			property int DeviceId
			{
				int get();
			}

			/// <summary>
			/// Gets the adapter's subsystem ID.
			/// </summary>
			property int SubsystemId
			{
				int get();
			}

			/// <summary>
			/// Gets the adapter's revision number.
			/// </summary>
			property int Revision
			{
				int get();
			}

			/// <summary>
			/// Gets the number of bytes of video memory not shared with the CPU.
			/// </summary>
			property System::Int64 DedicatedVideoMemory
			{
				System::Int64 get();
			}

			/// <summary>
			/// Gets the number of bytes of system memory not shared with the CPU.
			/// </summary>
			property System::Int64 DedicatedSystemMemory
			{
				System::Int64 get();
			}

			/// <summary>
			/// Gets the number of bytes of system memory shared with the CPU.
			/// </summary>
			property System::Int64 SharedSystemMemory
			{
				System::Int64 get();
			}

			/// <summary>
			/// Gets the adapter's unique identifier.
			/// </summary>
			property System::Int64 Luid
			{
				System::Int64 get();
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( AdapterDescription left, AdapterDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( AdapterDescription left, AdapterDescription right );

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
			virtual bool Equals( AdapterDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( AdapterDescription% value1, AdapterDescription% value2 );
		};
	}
};