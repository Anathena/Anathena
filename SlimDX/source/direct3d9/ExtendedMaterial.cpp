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

#include "../Utilities.h"

#include "ExtendedMaterial.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	// Utility function to convert from D3D color to SlimDX color. Can't put it in Color4 because
	// that thing is shared between D3D 9 and D3D 10.
	Color4 ConvertColor( const D3DCOLORVALUE& color )
	{
		Color4 cv;
		cv.Red = color.r;
		cv.Green = color.g;
		cv.Blue = color.b;
		cv.Alpha = color.a;

		return cv;
	}

	D3DCOLORVALUE ConvertColor( Color4 color )
	{
		D3DCOLORVALUE cv;
		cv.r = color.Red;
		cv.g = color.Green;
		cv.b = color.Blue;
		cv.a = color.Alpha;

		return cv;
	}

	D3DXMATERIAL ExtendedMaterial::ToUnmanaged( ExtendedMaterial material )
	{
		D3DXMATERIAL result;
		result.pTextureFilename = Utilities::AllocateNativeString( material.TextureFileName );
		result.MatD3D.Ambient = ConvertColor( material.MaterialD3D.Ambient );
		result.MatD3D.Diffuse = ConvertColor( material.MaterialD3D.Diffuse );
		result.MatD3D.Specular = ConvertColor( material.MaterialD3D.Specular );
		result.MatD3D.Emissive = ConvertColor( material.MaterialD3D.Emissive );
		result.MatD3D.Power = material.MaterialD3D.Power;

		return result;
	}

	ExtendedMaterial ExtendedMaterial::FromUnmanaged( const D3DXMATERIAL &material )
	{
		ExtendedMaterial result;
		Material mat;
		mat.Diffuse = ConvertColor( material.MatD3D.Diffuse );
		mat.Ambient = ConvertColor( material.MatD3D.Ambient );
		mat.Specular = ConvertColor( material.MatD3D.Specular );
		mat.Emissive = ConvertColor( material.MatD3D.Emissive );
		mat.Power = material.MatD3D.Power;
		result.MaterialD3D = mat;
		result.TextureFileName = gcnew String( material.pTextureFilename );

		return result;
	}

	array<ExtendedMaterial>^ ExtendedMaterial::FromBuffer( ID3DXBuffer* buffer, unsigned int count )
	{
		const D3DXMATERIAL* source  = reinterpret_cast<const D3DXMATERIAL*>( buffer->GetBufferPointer() );

		array<ExtendedMaterial>^ destination = gcnew array<ExtendedMaterial>( count );
		for( unsigned int i = 0; i < count; ++i )
		{
			Material m;
			m.Diffuse = ConvertColor( source[i].MatD3D.Diffuse );
			m.Ambient = ConvertColor( source[i].MatD3D.Ambient );
			m.Specular = ConvertColor( source[i].MatD3D.Specular );
			m.Emissive = ConvertColor( source[i].MatD3D.Emissive );
			m.Power = source[i].MatD3D.Power;
			destination[i].MaterialD3D = m;
			destination[i].TextureFileName = gcnew String( source[i].pTextureFilename );
		}

		return destination;
	}

	bool ExtendedMaterial::operator == ( ExtendedMaterial left, ExtendedMaterial right )
	{
		return ExtendedMaterial::Equals( left, right );
	}

	bool ExtendedMaterial::operator != ( ExtendedMaterial left, ExtendedMaterial right )
	{
		return !ExtendedMaterial::Equals( left, right );
	}

	int ExtendedMaterial::GetHashCode()
	{
		return MaterialD3D.GetHashCode() + TextureFileName->GetHashCode();
	}

	bool ExtendedMaterial::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ExtendedMaterial>( value ) );
	}

	bool ExtendedMaterial::Equals( ExtendedMaterial value )
	{
		return ( MaterialD3D == value.MaterialD3D && TextureFileName == value.TextureFileName );
	}

	bool ExtendedMaterial::Equals( ExtendedMaterial% value1, ExtendedMaterial% value2 )
	{
		return ( value1.MaterialD3D == value2.MaterialD3D && value1.TextureFileName == value2.TextureFileName );
	}
}
}