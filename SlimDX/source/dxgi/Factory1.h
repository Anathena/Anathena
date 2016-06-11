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

#include "Factory.h"

namespace SlimDX
{
	namespace DXGI
	{
		ref class Adapter1;

		/// <summary>
		/// Implements methods for generating DXGI objects.
		/// </summary>
		/// <unmanaged>IDXGIFactory1</unmanaged>
		public ref class Factory1 : Factory
		{
			COMOBJECT_CUSTOM(IDXGIFactory1, Factory1);

		public:
			value class AdapterEnumerator1 sealed : System::Collections::Generic::IEnumerable<Adapter1^>, System::Collections::Generic::IEnumerator<Adapter1^>
			{
			private:
				Factory1^ m_factory;
				Adapter1^ m_current;
				int m_index;

			internal:
				AdapterEnumerator1(Factory1^ factory) : m_factory(factory), m_index(0) { }

			public:
				AdapterEnumerator1 GetEnumerator() { return *this; }
				virtual System::Collections::Generic::IEnumerator<Adapter1^>^ GetEnumerator2() = System::Collections::Generic::IEnumerable<Adapter1^>::GetEnumerator { return *this; }
				virtual System::Collections::IEnumerator^ GetEnumerater3() = System::Collections::IEnumerable::GetEnumerator { return *this; }

				virtual bool MoveNext();
				virtual void Reset() { m_index = 0; m_current = nullptr; }

				property Adapter1^ Current { virtual Adapter1^ get() { return m_current; } }
				property System::Object^ Current2 
				{
					virtual System::Object^ get() = System::Collections::IEnumerator::Current::get { return Current; }
				}
			};

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="Factory1"/> class.
			/// </summary>
			Factory1();

			/// <summary>
			/// Lazily enumerates the available adapters, including remote adapters and those without visible outputs.
			/// </summary>
			/// <returns>A enumerator that can be used to obtain the available adapters.</returns>
			AdapterEnumerator1 EnumerateAdapters1();

			/// <summary>
			/// Gets the number of available adapters, including remote adapters and those without visible outputs.
			/// </summary>
			/// <returns>The total number of available adapters.</returns>
			int GetAdapterCount1();

			/// <summary>
			/// Gets the adapter at the specified index.
			/// </summary>
			/// <param name="index">The index of the desired adapter.</param>
			/// <returns>The specified adapter, or <c>null</c> on failure.</returns>
			Adapter1^ GetAdapter1(int index);

			/// <summary>
			/// Gets a value indicating the possible need to re-enumerate adapters -- new adapter(s) have become available, current adapter(s) have become unavailable.
			/// </summary>
			property bool IsCurrent { bool get(); }
		};
	}
}
