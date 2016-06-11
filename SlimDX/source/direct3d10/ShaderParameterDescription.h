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

#include "../d3dcompiler/EnumsDC.h"

namespace SlimDX
{
	namespace Direct3D10
	{	
		public value class ShaderParameterDescription : System::IEquatable<ShaderParameterDescription>
		{
		private:
			System::String^ m_SemanticName;
			System::UInt32 m_SemanticIndex;
			System::UInt32 m_Register;
			D3DCompiler::SystemValueType m_SystemValueType;
			D3DCompiler::RegisterComponentType m_ComponentType;
			D3DCompiler::RegisterComponentMaskFlags m_Mask;
			D3DCompiler::RegisterComponentMaskFlags m_ReadWriteMask;
		
		internal:
			ShaderParameterDescription( const D3D10_SIGNATURE_PARAMETER_DESC& native );
		
		public:
			property System::String^ SemanticName
			{
				System::String^ get();
			}
			
			property System::UInt32 SemanticIndex
			{
				System::UInt32 get();
			}
			
			property System::UInt32 Register
			{
				System::UInt32 get();
			}
			
			property D3DCompiler::SystemValueType SystemType
			{
				D3DCompiler::SystemValueType get();
			}
			
			property D3DCompiler::RegisterComponentType ComponentType
			{
				D3DCompiler::RegisterComponentType get();
			}
			
			property D3DCompiler::RegisterComponentMaskFlags UsageMask
			{
				D3DCompiler::RegisterComponentMaskFlags get();
			}
			
			property D3DCompiler::RegisterComponentMaskFlags ReadWriteMask
			{
				D3DCompiler::RegisterComponentMaskFlags get();
			}

			static bool operator == ( ShaderParameterDescription left, ShaderParameterDescription right );
			static bool operator != ( ShaderParameterDescription left, ShaderParameterDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( ShaderParameterDescription other );
			static bool Equals( ShaderParameterDescription% value1, ShaderParameterDescription% value2 );
		};
	}
};