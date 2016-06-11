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

#include <d3dx10.h>
#include <vcclr.h>

#include "Direct3D10Exception.h"

#include "Device10.h"
#include "Font10.h"
#include "FontDescription10.h"
#include "Sprite10.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{
	Font::Font( Device^ device, FontDescription description )
	{
		Construct( Build( device, description.Height, description.Width, description.Weight, description.MipLevels, description.IsItalic, description.CharacterSet, description.Precision, description.Quality, description.PitchAndFamily, description.FaceName ) );
	}

	Font::Font( Device^ device, int height, String^ faceName )
	{
		Construct( Build( device, height, 0, FontWeight::Normal, 1, false, FontCharacterSet::Default, FontPrecision::Default, FontQuality::Default, FontPitchAndFamily::Default, faceName ) );
	}

	Font::Font( Device^ device, int height, int width, FontWeight weight, int mipLevels, bool isItalic, FontCharacterSet characterSet, FontPrecision precision, FontQuality quality, FontPitchAndFamily pitchAndFamily, String^ faceName )
	{
		Construct( Build( device, height, width, weight, mipLevels, isItalic, characterSet, precision, quality, pitchAndFamily, faceName ) );
	}

	ID3DX10Font* Font::Build( Device^ device, int height, int width, FontWeight weight, int mipLevels, bool isItalic, FontCharacterSet characterSet, FontPrecision precision, FontQuality quality, FontPitchAndFamily pitchAndFamily, String^ faceName )
	{
		ID3DX10Font* font = 0;
		pin_ptr<const wchar_t> pinned_name = PtrToStringChars( faceName ); 
		if( RECORD_D3D10( D3DX10CreateFont( device->InternalPointer, height, width, static_cast<UINT>( weight ), mipLevels, isItalic, static_cast<UINT>( characterSet ), static_cast<UINT>( precision ), static_cast< UINT>( quality ), static_cast<UINT>( pitchAndFamily ), pinned_name, &font ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );

		return font;
	}
	
	FontDescription Font::Description::get()
	{
		D3DX10_FONT_DESC nativeDescription;
		if( RECORD_D3D10( InternalPointer->GetDesc( &nativeDescription ) ).IsFailure )
			return FontDescription();
			
		return FontDescription( nativeDescription );
	}

	int Font::Draw( Sprite^ sprite, String^ text, Drawing::Rectangle rect, FontDrawFlags flags, Color4 color )
	{
		ID3DX10Sprite* nativeSprite = sprite == nullptr ? 0 : sprite->InternalPointer;
		pin_ptr<const wchar_t> pinned_text = PtrToStringChars( text );
		RECT nativeRect = { rect.Left, rect.Top, rect.Right, rect.Bottom };

		int result = InternalPointer->DrawTextW( nativeSprite, pinned_text, text->Length, &nativeRect,
			static_cast<DWORD>( flags ), color.ToUnmanaged() );
		
		return result;
	}

	Drawing::Rectangle Font::Measure( Sprite^ sprite, String^ text, Drawing::Rectangle rect, FontDrawFlags flags )
	{
		ID3DX10Sprite* nativeSprite = sprite == nullptr ? 0 : sprite->InternalPointer;
		pin_ptr<const wchar_t> pinned_text = PtrToStringChars( text );
		RECT nativeRect = { rect.Left, rect.Top, rect.Right, rect.Bottom };
		InternalPointer->DrawTextW( nativeSprite, pinned_text, text->Length, &nativeRect, static_cast<DWORD>( flags ) | DT_CALCRECT, 0xFFFFFFFF );
		
		return Drawing::Rectangle( nativeRect.left, nativeRect.top, nativeRect.right - nativeRect.left, nativeRect.bottom - nativeRect.top );
	}

	Result Font::PreloadCharacters( int first, int last )
	{
		return RECORD_D3D10( InternalPointer->PreloadCharacters( first, last ) );
	}

	Result Font::PreloadGlyphs( int first, int last )
	{
		return RECORD_D3D10( InternalPointer->PreloadGlyphs( first, last ) );
	}

	Result Font::PreloadText( String^ text )
	{
		pin_ptr<const wchar_t> pinned_text = PtrToStringChars( text );
		return RECORD_D3D10( InternalPointer->PreloadText( pinned_text, text->Length ) );
	}
}
}
