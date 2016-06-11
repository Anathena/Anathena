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

#include "../ComObject.h"

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class Device;
		ref class Sprite;
		value class FontDescription;
		
		/// <summary>Encapsulates all of the textures and resources needed to render a font through a Direct3D 10 device.</summary>
		/// <unmanaged>ID3DX10Font</unmanaged>
		public ref class Font : public ComObject
		{
			COMOBJECT(ID3DX10Font, Font);
		
		private:
			static ID3DX10Font* Build( Device^ device, int height, int width, FontWeight weight, int mipLevels, bool isItalic, FontCharacterSet characterSet, FontPrecision precision, FontQuality quality, FontPitchAndFamily pitchAndFamily, System::String^ faceName );

		public:
			property FontDescription Description
			{
				FontDescription get();
			}
			
			Font( Device^ device, FontDescription description );
			Font( Device^ device, int height, System::String^ faceName );
			Font( Device^ device, int height, int width, FontWeight weight, int mipLevels, bool isItalic, FontCharacterSet characterSet, FontPrecision precision, FontQuality quality, FontPitchAndFamily pitchAndFamily, System::String^ faceName );

			int Draw( Sprite^ sprite, System::String^ text, System::Drawing::Rectangle rect, FontDrawFlags flags, Color4 color );
			
			System::Drawing::Rectangle Measure( Sprite^ sprite, System::String^ text, System::Drawing::Rectangle rect, FontDrawFlags flags );

			Result PreloadCharacters( int first, int last );
			Result PreloadGlyphs( int first, int last );
			Result PreloadText( System::String^ text );
		};
	}
}
