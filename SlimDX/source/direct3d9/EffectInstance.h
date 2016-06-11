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

#include "EffectDefault.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		public value class EffectInstance : System::IEquatable<EffectInstance>
		{
		internal:
			static EffectInstance FromUnmanaged( const D3DXEFFECTINSTANCE &effect );
			static D3DXEFFECTINSTANCE ToUnmanaged( EffectInstance effect );
			static array<EffectInstance>^ FromBuffer( ID3DXBuffer* buffer, unsigned int count );

		public:
			property System::String^ EffectFileName;
			property array<EffectDefault>^ Defaults;

			static bool operator == ( EffectInstance left, EffectInstance right );
			static bool operator != ( EffectInstance left, EffectInstance right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( EffectInstance other );
			static bool Equals( EffectInstance% value1, EffectInstance% value2 );
		};
	}
}