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

#include "VertexElement.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class Device;

		/// <summary>Applications use the methods of the VertexDeclaration class to encapsulate the vertex shader declaration.</summary>
		/// <unmanaged>IDirect3DVertexDeclaration9</unmanaged>
		public ref class VertexDeclaration : public ComObject
		{
			COMOBJECT(IDirect3DVertexDeclaration9, VertexDeclaration);

		public:
			VertexDeclaration( Device^ device, array<VertexElement>^ elements );

			property array<VertexElement>^ Elements
			{
				array<VertexElement>^ get();
			}
		};
	}
}