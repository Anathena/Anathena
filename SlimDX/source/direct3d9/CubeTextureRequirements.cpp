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

#include "CubeTextureRequirements.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool CubeTextureRequirements::operator == ( CubeTextureRequirements left, CubeTextureRequirements right )
	{
		return CubeTextureRequirements::Equals( left, right );
	}

	bool CubeTextureRequirements::operator != ( CubeTextureRequirements left, CubeTextureRequirements right )
	{
		return !CubeTextureRequirements::Equals( left, right );
	}

	int CubeTextureRequirements::GetHashCode()
	{
		return Size.GetHashCode() + Format.GetHashCode() + MipLevelCount.GetHashCode();
	}

	bool CubeTextureRequirements::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CubeTextureRequirements>( value ) );
	}

	bool CubeTextureRequirements::Equals( CubeTextureRequirements value )
	{
		return ( Size == value.Size && Format == value.Format && MipLevelCount == value.MipLevelCount );
	}

	bool CubeTextureRequirements::Equals( CubeTextureRequirements% value1, CubeTextureRequirements% value2 )
	{
		return ( value1.Size == value2.Size && value1.Format == value2.Format && value1.MipLevelCount == value2.MipLevelCount );
	}
}
}