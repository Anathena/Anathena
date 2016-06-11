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

#include "Material.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool Material::operator == ( Material left, Material right )
	{
		return Material::Equals( left, right );
	}

	bool Material::operator != ( Material left, Material right )
	{
		return !Material::Equals( left, right );
	}

	int Material::GetHashCode()
	{
		return Diffuse.GetHashCode() + Ambient.GetHashCode() + Specular.GetHashCode()
			 + Emissive.GetHashCode() + Power.GetHashCode();
	}

	bool Material::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Material>( value ) );
	}

	bool Material::Equals( Material value )
	{
		return ( Diffuse == value.Diffuse && Ambient == value.Ambient && Specular == value.Specular
			 && Emissive == value.Emissive && Power == value.Power );
	}

	bool Material::Equals( Material% value1, Material% value2 )
	{
		return ( value1.Diffuse == value2.Diffuse && value1.Ambient == value2.Ambient && value1.Specular == value2.Specular
			 && value1.Emissive == value2.Emissive && value1.Power == value2.Power );
	}
}
}