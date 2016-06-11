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

#include "../d3dcompiler/ShaderParameterDescriptionDC.h"

#include "Direct3D11Exception.h"

#include "EffectShaderDescription11.h"
#include "GeometryShader11.h"
#include "PixelShader11.h"
#include "VertexShader11.h"
#include "HullShader11.h"
#include "DomainShader11.h"
#include "ComputeShader11.h"

#include "EffectShaderVariable11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectShaderVariable::EffectShaderVariable( ID3DX11EffectShaderVariable* pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = pointer;
	}
	
	EffectShaderVariable::EffectShaderVariable( IntPtr pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = reinterpret_cast<ID3DX11EffectShaderVariable*>( pointer.ToPointer() );
	}
	
	PixelShader^ EffectShaderVariable::GetPixelShader( int index )
	{
		ID3D11PixelShader* shader = 0;
		if( RECORD_D3D11( m_Pointer->GetPixelShader( index, &shader ) ).IsFailure )
			return nullptr;
			
		return PixelShader::FromPointer( shader );
	}
	
	VertexShader^ EffectShaderVariable::GetVertexShader( int index )
	{
		ID3D11VertexShader* shader = 0;
		if( RECORD_D3D11( m_Pointer->GetVertexShader( index, &shader ) ).IsFailure )
			return nullptr;
		
		return VertexShader::FromPointer( shader );
	}
	
	GeometryShader^ EffectShaderVariable::GetGeometryShader( int index )
	{
		ID3D11GeometryShader* shader = 0;
		if( RECORD_D3D11( m_Pointer->GetGeometryShader( index, &shader ) ).IsFailure )
			return nullptr;
			
		return GeometryShader::FromPointer( shader );
	}

	HullShader^ EffectShaderVariable::GetHullShader( int index )
	{
		ID3D11HullShader* shader = 0;
		if( RECORD_D3D11( m_Pointer->GetHullShader( index, &shader ) ).IsFailure )
			return nullptr;
			
		return HullShader::FromPointer( shader );
	}

	DomainShader^ EffectShaderVariable::GetDomainShader( int index )
	{
		ID3D11DomainShader* shader = 0;
		if( RECORD_D3D11( m_Pointer->GetDomainShader( index, &shader ) ).IsFailure )
			return nullptr;
			
		return DomainShader::FromPointer( shader );
	}

	ComputeShader^ EffectShaderVariable::GetComputeShader( int index )
	{
		ID3D11ComputeShader* shader = 0;
		if( RECORD_D3D11( m_Pointer->GetComputeShader( index, &shader ) ).IsFailure )
			return nullptr;
			
		return ComputeShader::FromPointer( shader );
	}
	
	D3DCompiler::ShaderParameterDescription EffectShaderVariable::GetInputParameterDescription( int shaderIndex, int parameterIndex )
	{
		D3D11_SIGNATURE_PARAMETER_DESC description;
		if( RECORD_D3D11( m_Pointer->GetInputSignatureElementDesc( shaderIndex, parameterIndex, &description ) ).IsFailure )
			return D3DCompiler::ShaderParameterDescription();
			
		return D3DCompiler::ShaderParameterDescription( description );
	}
	
	D3DCompiler::ShaderParameterDescription EffectShaderVariable::GetOutputParameterDescription( int shaderIndex, int parameterIndex )
	{
		D3D11_SIGNATURE_PARAMETER_DESC description;
		if( RECORD_D3D11( m_Pointer->GetOutputSignatureElementDesc( shaderIndex, parameterIndex, &description ) ).IsFailure )
			return D3DCompiler::ShaderParameterDescription();
			
		return D3DCompiler::ShaderParameterDescription( description );
	}

	D3DCompiler::ShaderParameterDescription EffectShaderVariable::GetPatchConstantDescription( int shaderIndex, int parameterIndex )
	{
		D3D11_SIGNATURE_PARAMETER_DESC description;
		if( RECORD_D3D11( m_Pointer->GetPatchConstantSignatureElementDesc( shaderIndex, parameterIndex, &description ) ).IsFailure )
			return D3DCompiler::ShaderParameterDescription();
			
		return D3DCompiler::ShaderParameterDescription( description );
	}
	
	EffectShaderDescription EffectShaderVariable::GetShaderDescription( int shaderIndex )
	{
		D3DX11_EFFECT_SHADER_DESC description;
		if( RECORD_D3D11( m_Pointer->GetShaderDesc( shaderIndex, &description ) ).IsFailure )
			return EffectShaderDescription();

		return EffectShaderDescription( description );
	}
}
}
