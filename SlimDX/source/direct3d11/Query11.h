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

#include "Enums11.h"

#include "Asynchronous11.h"
#include "QueryDescription11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;

		/// <summary>
		/// Defines an interface for querying information from the GPU.
		/// </summary>
		/// <unmanaged>ID3D11Query</unmanaged>
		public ref class Query : public Asynchronous
		{
			COMOBJECT(ID3D11Query, Query);

		private protected:
			Query();

		public:
			/// <summary>
			/// Gets the query's description.
			/// </summary>
			property QueryDescription Description 
			{
				QueryDescription get();
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Query"/> class.
			/// </summary>
			/// <param name="device">The device used to create the query.</param>
			/// <param name="description">The query description.</param>
			Query( SlimDX::Direct3D11::Device^ device, QueryDescription description );
		};
	}
}
