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

#include "IResettable.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class Texture;

		/// <summary>Provides a set of methods that simplify the process of drawing sprites.</summary>
		/// <unmanaged>ID3DXSprite</unmanaged>
		public ref class Sprite : public ComObject, IResettable
		{
			COMOBJECT(ID3DXSprite, Sprite);

		public:
			/// <summary>Initializes a new instance of a sprite object.</summary>
			/// <param name="device">The device to use when creating the new sprite object.</param>
			/// <unmanaged>D3DXCreateSprite</unmanaged>
			Sprite( Device^ device );

			/// <summary>Prepares the object and device for drawing sprites.</summary>
			/// <unmanaged>ID3DXSprite::Begin</unmanaged>
			Result Begin( SpriteFlags flags );
			
			/// <summary>Calls <see cref="Flush()" /> and restores the device state to how it was before <see cref="Begin()" /> was called.</summary>
			/// <unmanaged>ID3DXSprite::End</unmanaged>
			Result End();

			/// <summary>
			/// Forces all batched sprites to be submitted to the device.
			/// Device states remain as they were after the last call to <see cref="Begin(SpriteFlags)" />.
			/// The list of batched sprites is then cleared.
			/// </summary>
			/// <unmanaged>ID3DXSprite::Flush</unmanaged>
			Result Flush();

			/// <summary>
			/// Use this method to release all references to video memory resources and delete all stateblocks.
			/// This method should be called whenever a device is lost, or before resetting a device.
			/// </summary>
			/// <unmanaged>ID3DXFont::OnLostDevice</unmanaged>
			virtual Result OnLostDevice();

			/// <summary>
			/// Use this method to re-acquire resources and save initial state.
			/// </summary>
			/// <unmanaged>ID3DXFont::OnResetDevice</unmanaged>
			virtual Result OnResetDevice();

			property SlimDX::Direct3D9::Device^ Device
			{
				SlimDX::Direct3D9::Device^ get();
			}

			property Matrix Transform
			{
				Matrix get();
				void set( Matrix value );
			}

			/// <summary>
			/// Sets the left-handed world-view transform for a sprite.
			/// A call to this method is required before billboarding or sorting sprites.
			/// </summary>
			/// <unmanaged>ID3DXSprite::SetWorldViewLH</unmanaged>
			Result SetWorldViewLH( Matrix world, Matrix view );

			/// <summary>
			/// Sets the right-handed world-view transform for a sprite.
			/// A call to this method is required before billboarding or sorting sprites.
			/// </summary>
			/// <unmanaged>ID3DXSprite::SetWorldViewRH</unmanaged>
			Result SetWorldViewRH( Matrix world, Matrix view );

			/// <summary>Adds a sprite to the list of batched sprites.</summary>
			/// <param name="sourceRect">A rectangle that specifies the portion of the source texture to use. If null is passed, the entire source image will be used.</param>
			/// <param name="center">Specifies the center position of the sprite.</param>
			/// <param name="position">Specifies the position at which to render the sprite.</param>
			/// <param name="color">The color and alpha channels of the texture are modulated by this color.</param>
			/// <unmanaged>ID3DXSprite::Draw</unmanaged>
			Result Draw( Texture^ texture, System::Nullable<System::Drawing::Rectangle> sourceRect, System::Nullable<Vector3> center, System::Nullable<Vector3> position, Color4 color );

			/// <summary>Adds a sprite to the list of batched sprites.</summary>
			/// <param name="sourceRect">A rectangle that specifies the portion of the source texture to use.</param>
			/// <param name="color">The color and alpha channels of the texture are modulated by this color.</param>
			/// <unmanaged>ID3DXSprite::Draw</unmanaged>
			Result Draw( Texture^ texture, System::Nullable<System::Drawing::Rectangle> sourceRect, Color4 color );

			/// <summary>Adds a sprite to the list of batched sprites.</summary>
			/// <param name="center">Specifies the center position of the sprite.</param>
			/// <param name="position">Specifies the position at which to render the sprite.</param>
			/// <param name="color">The color and alpha channels of the texture are modulated by this color.</param>
			/// <unmanaged>ID3DXSprite::Draw</unmanaged>
			Result Draw( Texture^ texture, System::Nullable<Vector3> center, System::Nullable<Vector3> position, Color4 color );

			/// <summary>Adds a sprite to the list of batched sprites.</summary>
			/// <param name="color">The color and alpha channels of the texture are modulated by this color.</param>
			/// <unmanaged>ID3DXSprite::Draw</unmanaged>
			Result Draw( Texture^ texture, Color4 color );
		};
	}
}
