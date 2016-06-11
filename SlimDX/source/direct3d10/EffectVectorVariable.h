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

#include "../math/Vector2.h"
#include "../math/Vector3.h"
#include "../math/Vector4.h"
#include "../math/Color4.h"

#include "EffectVariable.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		public ref class EffectVectorVariable : public EffectVariable
		{
		private:
			ID3D10EffectVectorVariable* m_Pointer;
			
		internal:
			EffectVectorVariable( ID3D10EffectVectorVariable* pointer );
			
		public:
			EffectVectorVariable( System::IntPtr pointer );
			
			Result Set( Vector2 value );
			Result Set( Vector3 value );
			Result Set( Vector4 value );
			Result Set( Color4 value );
			Result Set( array<Vector4>^ value );
			Result Set( array<Color4>^ value );
			Result Set( array<int>^ value );
			Result Set( array<bool>^ value );
		};
	}
};