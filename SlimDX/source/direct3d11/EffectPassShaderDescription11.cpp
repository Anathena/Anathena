#include "stdafx.h"
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

#include <d3d11.h>
#include <d3dx11effect.h>

#include "EffectPassShaderDescription11.h"
#include "EffectShaderVariable11.h"

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectPassShaderDescription::EffectPassShaderDescription( const D3DX11_PASS_SHADER_DESC& native )
	{
		m_ShaderVariable = gcnew EffectShaderVariable( native.pShaderVariable );
		m_ShaderIndex = native.ShaderIndex;
	}
	
	EffectShaderVariable^ EffectPassShaderDescription::Variable::get()
	{
		return m_ShaderVariable;
	}
	
	int EffectPassShaderDescription::Index::get()
	{
		return m_ShaderIndex;
	}

	bool EffectPassShaderDescription::operator == ( EffectPassShaderDescription left, EffectPassShaderDescription right )
	{
		return EffectPassShaderDescription::Equals( left, right );
	}

	bool EffectPassShaderDescription::operator != ( EffectPassShaderDescription left, EffectPassShaderDescription right )
	{
		return !EffectPassShaderDescription::Equals( left, right );
	}

	int EffectPassShaderDescription::GetHashCode()
	{
		return m_ShaderVariable->GetHashCode() + m_ShaderIndex.GetHashCode();
	}

	bool EffectPassShaderDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectPassShaderDescription>( value ) );
	}

	bool EffectPassShaderDescription::Equals( EffectPassShaderDescription value )
	{
		return ( m_ShaderVariable == value.m_ShaderVariable && m_ShaderIndex == value.m_ShaderIndex );
	}

	bool EffectPassShaderDescription::Equals( EffectPassShaderDescription% value1, EffectPassShaderDescription% value2 )
	{
		return ( value1.m_ShaderVariable == value2.m_ShaderVariable && value1.m_ShaderIndex == value2.m_ShaderIndex );
	}
}
}
