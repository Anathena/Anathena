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
#include "stdafx.h"

#include "D3DCompilerException.h"

#include "ShaderReflectionVariableDC.h"
#include "ShaderVariableDescriptionDC.h"
#include "ShaderReflectionTypeDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{ 
	ShaderReflectionVariable::ShaderReflectionVariable( ID3D11ShaderReflectionVariable* pointer )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = pointer;
	}
	
	ShaderReflectionVariable::ShaderReflectionVariable( System::IntPtr pointer )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = reinterpret_cast<ID3D11ShaderReflectionVariable*>( pointer.ToPointer() );
	}

	ShaderVariableDescription ShaderReflectionVariable::Description::get()
	{
		D3D11_SHADER_VARIABLE_DESC nativeDescription;
		HRESULT hr = m_Pointer->GetDesc( &nativeDescription );
		RECORD_D3DC( hr );

		return ShaderVariableDescription( nativeDescription );
	}

	int ShaderReflectionVariable::GetInterfaceSlot( int arrayIndex )
	{
		return m_Pointer->GetInterfaceSlot( arrayIndex );
	}

	ShaderReflectionType^ ShaderReflectionVariable::GetVariableType()
	{
		ID3D11ShaderReflectionType* type = m_Pointer->GetType();
		if( type == 0 )
			return nullptr;

		return gcnew ShaderReflectionType( type );
	}
}
}