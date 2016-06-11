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
#pragma once

#include "../math/Color4.h"
#include "../math/Matrix.h"
#include "../math/Vector2.h"
#include "../math/Vector3.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class ShaderResourceView;
#ifdef XMLDOCS
		ref class Sprite;
#endif

		/// <summary>
		/// Describes instance data for a single sprite rendered by the
		/// <see cref="Sprite"/> interface.
		/// </summary>
		/// <unmanaged>D3DX10_SPRITE</unmanaged>
		public ref class SpriteInstance
		{
			Matrix transform;
			Vector2 textureCoordinates;
			Vector2 textureDimensions;
			Color4 color;
			ShaderResourceView^ texture;
			int textureIndex;
		
		internal:
			void ToNativeObject( D3DX10_SPRITE& object );

		public:
			property Matrix Transform
			{
				Matrix get() { return (transform); }
				void set( Matrix value ) { transform = value; }
			}

			property Vector2 TextureCoordinates
			{
				Vector2 get() { return textureCoordinates; }
				void set( Vector2 value ) { textureCoordinates = value; }
			}

			property Vector2 TextureDimensions
			{
				Vector2 get() { return textureDimensions; }
				void set( Vector2 value ) { textureDimensions = value; }
			}
			
			property Color4 Color
			{
				Color4 get() { return color; }
				void set( Color4 value ) { color = value; }
			}

			property ShaderResourceView^ Texture
			{
				ShaderResourceView^ get() { return texture; }
				void set( ShaderResourceView^ value ) { texture = value; }
			}

			property int TextureIndex
			{
				int get() { return textureIndex; }
				void set( int value ) { textureIndex = value; }
			}

			SpriteInstance( ShaderResourceView^ texture, Vector2 coordinates, Vector2 dimensions );
		};
	}
}
