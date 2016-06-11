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

#include "../math/Color4.h"

#include "../d3dcompiler/ShaderSignatureDC.h"

namespace SlimDX
{
	namespace Direct3D10
	{	
		ref class ShaderSignature;
		
		public value class EffectPassDescription : System::IEquatable<EffectPassDescription>
		{
		private:
			System::String^ m_Name;
			int m_Annotations;
			D3DCompiler::ShaderSignature^ m_Signature;
			int m_StencilRef;
			int m_SampleMask;
			Color4 m_BlendFactor;

		internal:
			EffectPassDescription( const D3D10_PASS_DESC& native );
			
		public:
			property System::String^ Name
			{
				System::String^ get();
			}
			
			property int AnnotationCount
			{
				int get();
			}
			
			property D3DCompiler::ShaderSignature^ Signature
			{
				D3DCompiler::ShaderSignature^ get();
			}
			
			property int StencilReference
			{
				int get();
			}
			
			property int SampleMask
			{
				int get();
			}
			
			property Color4 BlendFactor
			{
				Color4 get();
			}

			static bool operator == ( EffectPassDescription left, EffectPassDescription right );
			static bool operator != ( EffectPassDescription left, EffectPassDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( EffectPassDescription other );
			static bool Equals( EffectPassDescription% value1, EffectPassDescription% value2 );
		};
	}
};