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

namespace SlimDX
{
	namespace Direct3D10	
	{	
		ref class EffectShaderVariable;
		
		public value class EffectPassShaderDescription : System::IEquatable<EffectPassShaderDescription>
		{
			EffectShaderVariable^ m_ShaderVariable;
			int m_ShaderIndex;
			
		internal:
			EffectPassShaderDescription( const D3D10_PASS_SHADER_DESC& native );
			
		public:
			property EffectShaderVariable^ Variable
			{
				EffectShaderVariable^ get();
			}
			
			property int Index
			{
				int get();
			}

			static bool operator == ( EffectPassShaderDescription left, EffectPassShaderDescription right );
			static bool operator != ( EffectPassShaderDescription left, EffectPassShaderDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( EffectPassShaderDescription other );
			static bool Equals( EffectPassShaderDescription% value1, EffectPassShaderDescription% value2 );
		};
	}
};