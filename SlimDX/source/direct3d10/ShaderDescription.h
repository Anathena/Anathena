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
#include "../d3dcompiler/ShaderBytecodeDC.h"
#include "../d3dcompiler/ShaderSignatureDC.h"

namespace SlimDX
{
	namespace Direct3D10
	{	
		public value class ShaderDescription : System::IEquatable<ShaderDescription>
		{
		private:
			D3DCompiler::ShaderSignature^ m_InputSignature;
			bool m_IsInline;
			D3DCompiler::ShaderBytecode^ m_Bytecode;
			System::String^ m_SODecl;
			int m_NumInputSignatureEntries;
			int m_NumOutputSignatureEntries;
		
		internal:
			ShaderDescription( const D3D10_EFFECT_SHADER_DESC& native );
		
		public:
			property D3DCompiler::ShaderSignature^ Signature
			{
				D3DCompiler::ShaderSignature^ get();
			}
			
			property bool IsInline
			{
				bool get();
			}
			
			property D3DCompiler::ShaderBytecode^ Bytecode
			{
				D3DCompiler::ShaderBytecode^ get();
			}
			
			property System::String^ StreamOutputDeclaration
			{
				System::String^ get();
			}
			
			property int InputParameterCount
			{
				int get();
			}
			
			property int OutputParameterCount
			{
				int get();
			}

			static bool operator == ( ShaderDescription left, ShaderDescription right );
			static bool operator != ( ShaderDescription left, ShaderDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( ShaderDescription other );
			static bool Equals( ShaderDescription% value1, ShaderDescription% value2 );
		};
	}
};