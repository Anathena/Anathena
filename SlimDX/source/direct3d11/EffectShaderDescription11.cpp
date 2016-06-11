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

#include "../DataStream.h"

#include "EffectShaderDescription11.h"

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectShaderDescription::EffectShaderDescription( const D3DX11_EFFECT_SHADER_DESC& native )
	{
		m_InputSignature = gcnew D3DCompiler::ShaderSignature( gcnew DataStream( native.pInputSignature, native.BytecodeLength - (native.pInputSignature - native.pBytecode), true, false ) );
		m_IsInline = native.IsInline ? true : false;
		m_Bytecode = gcnew D3DCompiler::ShaderBytecode( native.pBytecode, native.BytecodeLength );
		m_SODecl = gcnew array<System::String^>( D3D11_SO_STREAM_COUNT );
		for( int i = 0; i < D3D11_SO_STREAM_COUNT; ++i )
		{
			m_SODecl[i] = gcnew System::String( native.SODecls[i] );
		}
		m_NumInputSignatureEntries = native.NumInputSignatureEntries;
		m_NumOutputSignatureEntries = native.NumOutputSignatureEntries;
	}
	
	D3DCompiler::ShaderSignature^ EffectShaderDescription::Signature::get()
	{
		return m_InputSignature;
	}
	
	bool EffectShaderDescription::IsInline::get()
	{
		return m_IsInline;
	}
	
	D3DCompiler::ShaderBytecode^ EffectShaderDescription::Bytecode::get()
	{
		return m_Bytecode;
	}
	
	array<System::String^>^ EffectShaderDescription::StreamOutputDeclaration::get()
	{
		return m_SODecl;
	}

	int EffectShaderDescription::RasterizedStream::get()
	{
		return m_RasterizedStream;
	}

	int EffectShaderDescription::InputParameterCount::get()
	{
		return m_NumInputSignatureEntries;
	}
	
	int EffectShaderDescription::OutputParameterCount::get()
	{
	  return m_NumOutputSignatureEntries;
	}

	int EffectShaderDescription::PatchConstantCount::get()
	{
		return m_NumPatchConstantSignatureEntries;
	}

	bool EffectShaderDescription::operator == ( EffectShaderDescription left, EffectShaderDescription right )
	{
		return EffectShaderDescription::Equals( left, right );
	}

	bool EffectShaderDescription::operator != ( EffectShaderDescription left, EffectShaderDescription right )
	{
		return !EffectShaderDescription::Equals( left, right );
	}

	int EffectShaderDescription::GetHashCode()
	{
		return m_InputSignature->GetHashCode() + m_IsInline.GetHashCode() + m_Bytecode->GetHashCode() + m_SODecl->GetHashCode() + m_NumInputSignatureEntries.GetHashCode() + m_NumOutputSignatureEntries.GetHashCode();
	}

	bool EffectShaderDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectShaderDescription>( value ) );
	}

	bool EffectShaderDescription::Equals( EffectShaderDescription value )
	{
		return ( m_InputSignature == value.m_InputSignature && m_IsInline == value.m_IsInline && m_Bytecode == value.m_Bytecode && m_SODecl == value.m_SODecl && m_NumInputSignatureEntries == value.m_NumInputSignatureEntries && m_NumOutputSignatureEntries == value.m_NumOutputSignatureEntries );
	}

	bool EffectShaderDescription::Equals( EffectShaderDescription% value1, EffectShaderDescription% value2 )
	{
		return ( value1.m_InputSignature == value2.m_InputSignature && value1.m_IsInline == value2.m_IsInline && value1.m_Bytecode == value2.m_Bytecode && value1.m_SODecl == value2.m_SODecl && value1.m_NumInputSignatureEntries == value2.m_NumInputSignatureEntries && value1.m_NumOutputSignatureEntries == value2.m_NumOutputSignatureEntries );
	}
}
}
