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
#include "ObjectDxgi.h"

namespace SlimDX
{
	namespace DXGI
	{
		ref class Adapter;
		
		/// <summary>
		/// Represents a base for objects that produces image data.
		/// </summary>
		/// <unmanaged>IDXGIDevice</unmanaged>
		public ref class Device : DXGIObject
		{
			COMOBJECT(IDXGIDevice, Device);

		private protected:
			Device();

		public:
			/// <summary>
			/// Gets or sets the device's GPU thread priority. Values range from -7 to 7, inclusive, with 0 being normal priority.
			/// </summary>
			property int GpuThreadPriority
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Gets the adapter associated with the device.
			/// </summary>
			property SlimDX::DXGI::Adapter^ Adapter
			{
				SlimDX::DXGI::Adapter^ get();
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class.
			/// </summary>
			/// <param name="device">The COM object implementing the IDXGIDevice interface.</param>
			Device( IComObject^ device );

			/// <summary>
			/// Gets the residency status of a list of resources.
			/// </summary>
			/// <param name="resources">The resources to query.</param>
			/// <returns>A list of residency status values, one for each entry in the input resources list.</returns>
			System::Collections::ObjectModel::ReadOnlyCollection<Residency>^ QueryResourceResidency( System::Collections::Generic::IList<IComObject^>^ resources );
		};
	}
}