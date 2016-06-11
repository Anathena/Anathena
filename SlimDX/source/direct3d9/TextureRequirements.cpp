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

#include "TextureRequirements.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool TextureRequirements::operator == ( TextureRequirements left, TextureRequirements right )
	{
		return TextureRequirements::Equals( left, right );
	}

	bool TextureRequirements::operator != ( TextureRequirements left, TextureRequirements right )
	{
		return !TextureRequirements::Equals( left, right );
	}

	int TextureRequirements::GetHashCode()
	{
		return Width.GetHashCode() + Height.GetHashCode() + Format.GetHashCode()
			 + MipLevelCount.GetHashCode();
	}

	bool TextureRequirements::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<TextureRequirements>( value ) );
	}

	bool TextureRequirements::Equals( TextureRequirements value )
	{
		return ( Width == value.Width && Height == value.Height && Format == value.Format
			 && MipLevelCount == value.MipLevelCount );
	}

	bool TextureRequirements::Equals( TextureRequirements% value1, TextureRequirements% value2 )
	{
		return ( value1.Width == value2.Width && value1.Height == value2.Height && value1.Format == value2.Format
			 && value1.MipLevelCount == value2.MipLevelCount );
	}
}
}