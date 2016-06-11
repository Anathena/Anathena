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

#include "ObjectDxgi.h"

namespace SlimDX
{
	namespace DXGI
	{
		ref class Output;
		value class AdapterDescription;

		/// <summary>
		/// Represents a display subsystem (one or more GPUs, DACs, and video memory).
		/// </summary>
		/// <unmanaged>IDXGIAdapter</unmanaged>
		public ref class Adapter : DXGIObject
		{
			COMOBJECT(IDXGIAdapter, Adapter);

		private protected:
			Adapter() { }

		public:
			value class OutputEnumerator sealed : System::Collections::Generic::IEnumerable<Output^>, System::Collections::Generic::IEnumerator<Output^>
			{
			private:
				Adapter^ m_adapter;
				Output^ m_current;
				int m_index;

			internal:
				OutputEnumerator(Adapter^ adapter) : m_adapter(adapter), m_index(0) { }

			public:
				OutputEnumerator GetEnumerator() { return *this; }
				virtual System::Collections::Generic::IEnumerator<Output^>^ GetEnumerator2() = System::Collections::Generic::IEnumerable<Output^>::GetEnumerator { return *this; }
				virtual System::Collections::IEnumerator^ GetEnumerater3() = System::Collections::IEnumerable::GetEnumerator { return *this; }

				virtual bool MoveNext();
				virtual void Reset() { m_index = 0; m_current = nullptr; }

				property Output^ Current { virtual Output^ get() { return m_current; } }
				property System::Object^ Current2 
				{
					virtual System::Object^ get() = System::Collections::IEnumerator::Current::get { return Current; }
				}
			};

		public:
			/// <summary>
			/// Gets the adapter's description.
			/// </summary>
			property AdapterDescription Description
			{
				AdapterDescription get();
			}

			/// <summary>
			/// Lazily enumerates the available outputs on the adapter.
			/// </summary>
			/// <returns>A enumerator that can be used to obtain the available outputs.</returns>
			OutputEnumerator EnumerateOutputs();

			/// <summary>
			/// Gets the number of outputs on the adapter.
			/// </summary>
			/// <returns>The number of outputs on the adapter.</returns>
			int GetOutputCount();
			
			/// <summary>
			/// Gets the specified output.
			/// </summary>
			/// <returns>The desired output, or <c>null</c> on failure.</returns>
			Output^ GetOutput( int index );
			
			/// <summary>
			/// Determines if a device interface for a graphics component is supported by the adapter.
			/// </summary>
			/// <param name="type">The device interface type.</param>
			/// <returns><c>true</c> if the device interface is supported; otherwise, <c>false</c>.</returns>
			bool IsInterfaceSupported( System::Type^ type );
			
			/// <summary>
			/// Determines if a device interface for a graphics component is supported by the adapter.
			/// </summary>
			/// <param name="type">The device interface type.</param>
			/// <param name="userModeVersion">Receives the user-mode driver version of the interface.</param>
			/// <returns><c>true</c> if the device interface is supported; otherwise, <c>false</c>.</returns>
			bool IsInterfaceSupported( System::Type^ type, [Out] System::Int64% userModeVersion );
			
			/// <summary>
			/// Converts the value of the object to its equivalent string representation.
			/// </summary>
			/// <returns>The string representation of the value of this instance.</returns>
			virtual System::String^ ToString() override;
		};
	}
};
