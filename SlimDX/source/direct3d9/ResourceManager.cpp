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

#include "ResourceManager.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool ResourceManager::operator == ( ResourceManager left, ResourceManager right )
	{
		return ResourceManager::Equals( left, right );
	}

	bool ResourceManager::operator != ( ResourceManager left, ResourceManager right )
	{
		return !ResourceManager::Equals( left, right );
	}

	int ResourceManager::GetHashCode()
	{
		return Stats.GetHashCode() + SurfaceStats.GetHashCode() + VolumeStats.GetHashCode()
			 + TextureStats.GetHashCode() + VolumeTextureStats.GetHashCode() + CubeTextureStats.GetHashCode()
			 + VertexBufferStats.GetHashCode() + IndexBufferStats.GetHashCode();
	}

	bool ResourceManager::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ResourceManager>( value ) );
	}

	bool ResourceManager::Equals( ResourceManager value )
	{
		return ( Stats == value.Stats && SurfaceStats == value.SurfaceStats && VolumeStats == value.VolumeStats
			 && TextureStats == value.TextureStats && VolumeTextureStats == value.VolumeTextureStats && CubeTextureStats == value.CubeTextureStats
			 && VertexBufferStats == value.VertexBufferStats && IndexBufferStats == value.IndexBufferStats );
	}

	bool ResourceManager::Equals( ResourceManager% value1, ResourceManager% value2 )
	{
		return ( value1.Stats == value2.Stats && value1.SurfaceStats == value2.SurfaceStats && value1.VolumeStats == value2.VolumeStats
			 && value1.TextureStats == value2.TextureStats && value1.VolumeTextureStats == value2.VolumeTextureStats && value1.CubeTextureStats == value2.CubeTextureStats
			 && value1.VertexBufferStats == value2.VertexBufferStats && value1.IndexBufferStats == value2.IndexBufferStats );
	}
}
}