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
#include "Query11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;

		/// <summary>
		/// Used to determine whether geometry should be processed depending on the results of a previous draw call.
		/// </summary>
		/// <unmanaged>ID3D11Predicate</unmanaged>
		public ref class Predicate : public Query
		{
			COMOBJECT(ID3D11Predicate, Predicate);
	
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="Predicate"/> class.
			/// </summary>
			/// <param name="device">The device used to create the predicate object.</param>
			/// <param name="description">The query description.</param>
			Predicate( SlimDX::Direct3D11::Device^ device, QueryDescription description );
		};
	}
}
