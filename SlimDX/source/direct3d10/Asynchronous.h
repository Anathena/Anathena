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
#include "../DataStream.h"

#include "DeviceChild10.h"
#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		/// <summary>
		/// Base class for objects that perform asynchronous GPU data access.
		/// </summary>
		/// <unmanaged>ID3D10Asynchronous</unmanaged>
		public ref class Asynchronous abstract : public DeviceChild
		{
			COMOBJECT_BASE(ID3D10Asynchronous);

		protected:
			Asynchronous() { }
		
		public:
			/// <summary>
			/// Gets a value indicating whether or not data is available for consumption.
			/// </summary>
			property bool IsDataAvailable
			{
				bool get();
			}
			
			/// <summary>
			/// Marks the start of a series of asynchronous commands.
			/// </summary>
			void Begin();

			/// <summary>
			/// Marks the end of a series of asynchronous commands.
			/// </summary>
			void End();
			
			/// <summary>
			/// Retrieves data from the GPU asychronously.
			/// </summary>
			/// <returns>The data.</returns>
			DataStream^ GetData();

			/// <summary>
			/// Retrieves data from the GPU asychronously.
			/// </summary>
			/// <param name="flags">Flags indicating how the data should be retrieved.</param>
			/// <returns>The data.</returns>
			DataStream^ GetData( AsynchronousFlags flags );
		};
	}
};