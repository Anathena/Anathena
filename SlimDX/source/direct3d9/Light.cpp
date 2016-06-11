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

#include "Light.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool Light::operator == ( Light left, Light right )
	{
		return Light::Equals( left, right );
	}

	bool Light::operator != ( Light left, Light right )
	{
		return !Light::Equals( left, right );
	}

	int Light::GetHashCode()
	{
		return Type.GetHashCode() + Diffuse.GetHashCode() + Specular.GetHashCode()
			 + Ambient.GetHashCode() + Position.GetHashCode() + Direction.GetHashCode()
			 + Range.GetHashCode() + Falloff.GetHashCode() + Attenuation0.GetHashCode()
			 + Attenuation1.GetHashCode() + Attenuation2.GetHashCode() + Theta.GetHashCode()
			 + Phi.GetHashCode();
	}

	bool Light::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Light>( value ) );
	}

	bool Light::Equals( Light value )
	{
		return ( Type == value.Type && Diffuse == value.Diffuse && Specular == value.Specular
			 && Ambient == value.Ambient && Position == value.Position && Direction == value.Direction
			 && Range == value.Range && Falloff == value.Falloff && Attenuation0 == value.Attenuation0
			 && Attenuation1 == value.Attenuation1 && Attenuation2 == value.Attenuation2 && Theta == value.Theta
			 && Phi == value.Phi );
	}

	bool Light::Equals( Light% value1, Light% value2 )
	{
		return ( value1.Type == value2.Type && value1.Diffuse == value2.Diffuse && value1.Specular == value2.Specular
			 && value1.Ambient == value2.Ambient && value1.Position == value2.Position && value1.Direction == value2.Direction
			 && value1.Range == value2.Range && value1.Falloff == value2.Falloff && value1.Attenuation0 == value2.Attenuation0
			 && value1.Attenuation1 == value2.Attenuation1 && value1.Attenuation2 == value2.Attenuation2 && value1.Theta == value2.Theta
			 && value1.Phi == value2.Phi );
	}
}
}