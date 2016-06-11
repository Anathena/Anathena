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

#include "Device.h"
#include "Texture.h"
#include "IndexBuffer.h"
#include "VertexBuffer.h"
#include "Mesh.h"
#include "DisplacementParameters.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool DisplacementParameters::operator == ( DisplacementParameters left, DisplacementParameters right )
	{
		return DisplacementParameters::Equals( left, right );
	}

	bool DisplacementParameters::operator != ( DisplacementParameters left, DisplacementParameters right )
	{
		return !DisplacementParameters::Equals( left, right );
	}

	int DisplacementParameters::GetHashCode()
	{
		return Texture->GetHashCode() + MinFilter.GetHashCode() + MagFilter.GetHashCode()
			 + MipFilter.GetHashCode() + Wrap.GetHashCode() + LevelOfDetailBias.GetHashCode();
	}

	bool DisplacementParameters::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<DisplacementParameters>( value ) );
	}

	bool DisplacementParameters::Equals( DisplacementParameters value )
	{
		return ( Texture == value.Texture && MinFilter == value.MinFilter && MagFilter == value.MagFilter
			 && MipFilter == value.MipFilter && Wrap == value.Wrap && LevelOfDetailBias == value.LevelOfDetailBias );
	}

	bool DisplacementParameters::Equals( DisplacementParameters% value1, DisplacementParameters% value2 )
	{
		return ( value1.Texture == value2.Texture && value1.MinFilter == value2.MinFilter && value1.MagFilter == value2.MagFilter
			 && value1.MipFilter == value2.MipFilter && value1.Wrap == value2.Wrap && value1.LevelOfDetailBias == value2.LevelOfDetailBias );
	}
}
}