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
#include "stdafx.h"

#include "FontFeature.h"

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	FontFeatureTag FontFeature::BuildTag( String^ tag )
	{
		if( tag->Length != 4 )
			throw gcnew ArgumentException( "Font feature name tag must be four characters long", "tag" );

		return static_cast<FontFeatureTag>( DWRITE_MAKE_OPENTYPE_TAG( tag[0], tag[1], tag[2], tag[3] ) );
	}

	FontFeatureTag FontFeature::BuildTag( char a, char b, char c, char d )
	{
		return static_cast<FontFeatureTag>( DWRITE_MAKE_OPENTYPE_TAG( a, b, c, d ) );
	}
}
}