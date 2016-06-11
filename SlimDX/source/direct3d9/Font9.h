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

#include "Enums.h"
#include "IResettable.h"

#include "FontDescription.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>Encapsulates the textures and resources needed to render a specific font on a specific device.</summary>
		/// <unmanaged>ID3DXFont</unmanaged>
		public ref class Font : public ComObject, IResettable
		{
			COMOBJECT(ID3DXFont, Font);

		internal:
			void Build( Device^ device, int height, int width, FontWeight weight, int mipLevels, bool italic, CharacterSet characterSet, Precision outputPrecision, FontQuality quality, PitchAndFamily pitchAndFamily, System::String^ faceName );

		public:
			Font( Device^ device, int height, int width, FontWeight weight, int mipLevels, bool italic, CharacterSet characterSet, Precision outputPrecision, FontQuality quality, PitchAndFamily pitchAndFamily, System::String^ faceName );
			Font( Device^ device, System::Drawing::Font^ font );

			/// <summary>Draws formatted text, optionally using a <see cref="SlimDX::Direct3D9::Sprite" /> object for improved performance.</summary>
			/// <unmanaged>ID3DXFont::DrawText</unmanaged>
			int DrawString( Sprite^ sprite, System::String^ text, System::Drawing::Rectangle rectangle, DrawTextFormat format, Color4 color );

			/// <summary>Draws formatted text, optionally using a <see cref="SlimDX::Direct3D9::Sprite" /> object for improved performance.</summary>
			/// <unmanaged>ID3DXFont::DrawText</unmanaged>
			int DrawString( Sprite^ sprite, System::String^ text, System::Drawing::Rectangle rectangle, DrawTextFormat format, int color );

			/// <summary>Draws formatted text, optionally using a <see cref="SlimDX::Direct3D9::Sprite" /> object for improved performance.</summary>
			/// <unmanaged>ID3DXFont::DrawText</unmanaged>
			int DrawString( Sprite^ sprite, System::String^ text, int x, int y, Color4 color );

			/// <summary>Draws formatted text, optionally using a <see cref="SlimDX::Direct3D9::Sprite" /> object for improved performance.</summary>
			/// <unmanaged>ID3DXFont::DrawText</unmanaged>
			int DrawString( Sprite^ sprite, System::String^ text, int x, int y, int color );

			/// <summary>Computes the bounding rectangle for a given string to be rendered using this font.</summary>
			/// <unmanaged>ID3DXFont::DrawText</unmanaged>
			System::Drawing::Rectangle MeasureString( Sprite^ sprite, System::String^ text, DrawTextFormat format );

			/// <summary>Computes the bounding rectangle for a given string to be rendered using this font.</summary>
			/// <unmanaged>ID3DXFont::DrawText</unmanaged>
			int MeasureString( Sprite^ sprite, System::String^ text, DrawTextFormat format, System::Drawing::Rectangle% rectangle );

			/// <summary>Loads a series of characters into video memory to improve the efficiency of rendering to the device.</summary>
			/// <unmanaged>ID3DXFont::PreloadText</unmanaged>
			Result PreloadCharacters( int first, int last );

			/// <summary>Loads a series of glyphs into video memory to improve the efficiency of rendering to the device.</summary>
			/// <unmanaged>ID3DXFont::PreloadText</unmanaged>
			Result PreloadGlyphs( int first, int last );

			/// <summary>Loads formatted text into video memory to improve the efficiency of rendering to the device.</summary>
			/// <unmanaged>ID3DXFont::PreloadText</unmanaged>
			Result PreloadText( System::String^ text );

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

			property FontDescription Description
			{
				FontDescription get();
			}

			property System::IntPtr DeviceContext
			{
				System::IntPtr get();
			}
		};
	}
}
