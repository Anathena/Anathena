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

#include "Material.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		public value class ExtendedMaterial : System::IEquatable<ExtendedMaterial>
		{
		internal:
			static D3DXMATERIAL ToUnmanaged( ExtendedMaterial material );
			static ExtendedMaterial FromUnmanaged( const D3DXMATERIAL &material );
			static array<ExtendedMaterial>^ FromBuffer( ID3DXBuffer* buffer, unsigned int count );

		public:
			property Material MaterialD3D;
			property System::String^ TextureFileName;

			static bool operator == ( ExtendedMaterial left, ExtendedMaterial right );
			static bool operator != ( ExtendedMaterial left, ExtendedMaterial right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( ExtendedMaterial other );
			static bool Equals( ExtendedMaterial% value1, ExtendedMaterial% value2 );
		};
	}
}