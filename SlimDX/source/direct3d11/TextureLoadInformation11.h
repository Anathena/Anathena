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

#include "Enums11.h"
#include "ResourceRegion11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Describes parameters used to load a texture from another texture.
		/// </summary>
		/// <unmanaged>D3DX11_TEXTURE_LOAD_INFO</unmanaged>
		public value class TextureLoadInformation
		{
		public:
			/// <summary>
			/// Source texture region. 
			/// </summary>
			property ResourceRegion SourceRegion;

			/// <summary>
			/// Destination texture region.
			/// </summary>
			property ResourceRegion DestinationRegion;

			/// <summary>
			/// Source texture mipmap level.
			/// </summary>
			property int FirstSourceMip;

			/// <summary>
			/// Destination texture mipmap level.
			/// </summary>
			property int FirstDestinationMip;

			/// <summary>
			/// Number of mipmap levels in the source texture.
			/// </summary>
			property int MipCount;

			/// <summary>
			/// First element of the source texture.
			/// </summary>
			property int FirstSourceElement;

			/// <summary>
			/// First element of the destination texture.
			/// </summary>
			property int FirstDestinationElement;

			/// <summary>
			/// Number of elements to load.
			/// </summary>
			property int ElementCount;

			/// <summary>
			/// Filtering options during resampling.
			/// </summary>
			property FilterFlags Filter;

			/// <summary>
			/// Filtering options when generating mip levels.
			/// </summary>
			property FilterFlags MipFilter;
		};
	}
}