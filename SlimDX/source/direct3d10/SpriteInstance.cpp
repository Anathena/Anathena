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

#include <d3d10.h>
#include <d3dx10.h>

#include "SpriteInstance.h"
#include "ShaderResourceView.h"

#include "../math/Matrix.h"
#include "../math/Vector2.h"

namespace SlimDX
{
namespace Direct3D10
{
	SpriteInstance::SpriteInstance( ShaderResourceView^ texture, Vector2 coordinates, Vector2 dimensions )
	: transform( Matrix::Identity ), textureCoordinates( coordinates ), textureDimensions( dimensions),
		color( 1.0f, 1.0f, 1.0f, 1.0 ), texture( texture )
	{
	}

	void SpriteInstance::ToNativeObject( D3DX10_SPRITE& object )
	{
		pin_ptr<Matrix> pinnedTransform = &transform;
		pin_ptr<Vector2> pinnedCoordinates = &textureCoordinates;
		pin_ptr<Vector2> pinnedDimensions = &textureDimensions;
		pin_ptr<Color4> pinnedColor = &color;

		object.matWorld = *reinterpret_cast<D3DXMATRIX*>( pinnedTransform );
		object.TexCoord = *reinterpret_cast<D3DXVECTOR2*>( pinnedCoordinates );
		object.TexSize = *reinterpret_cast<D3DXVECTOR2*>( pinnedDimensions );
		object.ColorModulate = *reinterpret_cast<D3DXCOLOR*>( pinnedColor );
		object.pTexture = static_cast<ID3D10ShaderResourceView*>( texture->InternalPointer );
		object.TextureIndex = textureIndex;
	}
}
}
