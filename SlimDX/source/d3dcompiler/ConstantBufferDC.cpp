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

#include "ConstantBufferDC.h"
#include "ConstantBufferDescriptionDC.h"
#include "ShaderReflectionVariableDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{ 
	ConstantBuffer::ConstantBuffer( ID3D11ShaderReflectionConstantBuffer* pointer )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = pointer;
	}
	
	ConstantBuffer::ConstantBuffer( System::IntPtr pointer )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = reinterpret_cast<ID3D11ShaderReflectionConstantBuffer*>( pointer.ToPointer() );
	}

	ConstantBufferDescription ConstantBuffer::Description::get()
	{
		D3D11_SHADER_BUFFER_DESC nativeDescription;
		HRESULT hr = m_Pointer->GetDesc( &nativeDescription );
		RECORD_D3DC( hr );

		return ConstantBufferDescription( nativeDescription );
	}

	ShaderReflectionVariable^ ConstantBuffer::GetVariable( int index )
	{
		ID3D11ShaderReflectionVariable* variable = m_Pointer->GetVariableByIndex( index );
		if( variable == 0 )
			return nullptr;

		return gcnew ShaderReflectionVariable( variable );
	}
	
	ShaderReflectionVariable^ ConstantBuffer::GetVariable( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		ID3D11ShaderReflectionVariable* variable = m_Pointer->GetVariableByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 )
			return nullptr;

		return gcnew ShaderReflectionVariable( variable );
	}
}
}