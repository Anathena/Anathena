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
#include <d3d9.h>
#include <d3dx9.h>

#include "PassDescription.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool PassDescription::operator == ( PassDescription left, PassDescription right )
	{
		return PassDescription::Equals( left, right );
	}

	bool PassDescription::operator != ( PassDescription left, PassDescription right )
	{
		return !PassDescription::Equals( left, right );
	}

	int PassDescription::GetHashCode()
	{
		return Name->GetHashCode() + Annotations.GetHashCode() + VertexShaderFunction.GetHashCode()
			 + PixelShaderFunction.GetHashCode();
	}

	bool PassDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<PassDescription>( value ) );
	}

	bool PassDescription::Equals( PassDescription value )
	{
		return ( Name == value.Name && Annotations == value.Annotations && VertexShaderFunction == value.VertexShaderFunction
			 && PixelShaderFunction == value.PixelShaderFunction );
	}

	bool PassDescription::Equals( PassDescription% value1, PassDescription% value2 )
	{
		return ( value1.Name == value2.Name && value1.Annotations == value2.Annotations && value1.VertexShaderFunction == value2.VertexShaderFunction
			 && value1.PixelShaderFunction == value2.PixelShaderFunction );
	}
}
}