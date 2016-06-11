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

extern const IID IID_IDWriteFontCollection;

#include "../ComObject.h"

namespace SlimDX
{
	namespace DirectWrite
	{
		ref class FontFamily;
		ref class FontFace;
		ref class Font;

		public ref class FontCollection : public ComObject
		{
			COMOBJECT(IDWriteFontCollection, FontCollection);

		public:
			property int FontFamilyCount
			{
				int get();
			};

			property FontFamily^ default[int]
			{
				FontFamily^ get(int index);
			};

			int FindFamilyName( System::String^ familyName, [Out] bool% exists );
			int FindFamilyName( System::String^ familyName );

			Font^ GetFontFromFontFace( FontFace^ fontFace );
		};
	}
}