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

#include <d3dx11effect.h>

#include "EffectDescription11.h"

namespace SlimDX
{
namespace Direct3D11
{ 	
	EffectDescription::EffectDescription( const D3DX11_EFFECT_DESC& native )
	{
		ConstantBufferCount = native.ConstantBuffers;
		GlobalVariableCount = native.GlobalVariables;
		InterfaceVariableCount = native.InterfaceVariables;
		TechniqueCount = native.Techniques;
		GroupCount = native.Groups;
	}

	bool EffectDescription::operator == ( EffectDescription left, EffectDescription right )
	{
		return EffectDescription::Equals( left, right );
	}

	bool EffectDescription::operator != ( EffectDescription left, EffectDescription right )
	{
		return !EffectDescription::Equals( left, right );
	}

	int EffectDescription::GetHashCode()
	{
		return ConstantBufferCount.GetHashCode() + GlobalVariableCount.GetHashCode() + InterfaceVariableCount.GetHashCode() + TechniqueCount.GetHashCode() + GroupCount.GetHashCode();
	}

	bool EffectDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectDescription>( value ) );
	}

	bool EffectDescription::Equals( EffectDescription value )
	{
		return ( ConstantBufferCount == value.ConstantBufferCount && GlobalVariableCount == value.GlobalVariableCount && InterfaceVariableCount == value.InterfaceVariableCount && TechniqueCount == value.TechniqueCount && GroupCount == value.GroupCount );
	}

	bool EffectDescription::Equals( EffectDescription% value1, EffectDescription% value2 )
	{
		return ( value1.ConstantBufferCount == value2.ConstantBufferCount && value1.GlobalVariableCount == value2.GlobalVariableCount && value1.InterfaceVariableCount == value2.InterfaceVariableCount && value1.TechniqueCount == value2.TechniqueCount && value1.GroupCount == value2.GroupCount );
	}
}
}
