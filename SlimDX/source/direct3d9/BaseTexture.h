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

#include "../DataStream.h"
#include "Enums.h"
#include "Resource.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>
		/// Represents a base class for all texture resources.
		/// </summary>
		/// <unmanaged>IDirect3DBaseTexture9</unmanaged>
		public ref class BaseTexture abstract : public Resource
		{
			COMOBJECT_BASE(IDirect3DBaseTexture9);

		protected:
			/// <summary>
			/// Initializes a new instance of the <see cref="BaseTexture"/> class.
			/// </summary>
			BaseTexture() { }

		internal:
			static BaseTexture^ FromUnmanaged( IDirect3DBaseTexture9 *texture );

		public:
			/// <summary>
			/// Releases all resources used by the <see cref="BaseTexture"/>.
			/// </summary>
			virtual ~BaseTexture() { }

			/// <summary>
			/// Filters mipmap levels of a texture.
			/// </summary>
			/// <param name="sourceLevel">The mipmap level whose image is used to generate the subsequent levels.</param>
			/// <param name="filter">Set of filtering options for the mipmap.</param>
			/// <param name="palette">The texture palette.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result FilterTexture( int sourceLevel, Filter filter, array<PaletteEntry>^ palette );
			
			/// <summary>
			/// Filters mipmap levels of a texture.
			/// </summary>
			/// <param name="sourceLevel">The mipmap level whose image is used to generate the subsequent levels.</param>
			/// <param name="filter">Set of filtering options for the mipmap.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result FilterTexture( int sourceLevel, Filter filter );
			
			/// <summary>
			/// Generates mipmap sublevels.
			/// </summary>
			void GenerateMipSublevels();

			/// <summary>
			/// Saves a texture to a stream.
			/// </summary>
			/// <param name="texture">The texture containing the image to save.</param>
			/// <param name="format">The file format to use when saving.</param>
			/// <returns>A <see cref="DataStream"/> containing the texture image.</returns>
			static DataStream^ ToStream( BaseTexture^ texture, ImageFileFormat format );
			
			/// <summary>
			/// Saves a texture to a stream.
			/// </summary>
			/// <param name="texture">The texture containing the image to save.</param>
			/// <param name="format">The file format to use when saving.</param>
			/// <param name="palette">The texture palette.</param>
			/// <returns>A <see cref="DataStream"/> containing the texture image.</returns>
			static DataStream^ ToStream( BaseTexture^ texture, ImageFileFormat format, array<PaletteEntry>^ palette );

			/// <summary>
			/// Saves a texture to a file.
			/// </summary>
			/// <param name="texture">The texture containing the image to save.</param>
			/// <param name="fileName">The name of the file to save to.</param>
			/// <param name="format">The file format to use when saving.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result ToFile( BaseTexture^ texture, System::String^ fileName, ImageFileFormat format );
			
			/// <summary>
			/// Saves a texture to a file.
			/// </summary>
			/// <param name="texture">The texture containing the image to save.</param>
			/// <param name="fileName">The name of the file to save to.</param>
			/// <param name="format">The file format to use when saving.</param>
			/// <param name="palette">The texture palette.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result ToFile( BaseTexture^ texture, System::String^ fileName, ImageFileFormat format, array<PaletteEntry>^ palette );
		
			/// <summary>
			/// Gets or sets the filter used to automatically generate mipmaps.
			/// </summary>
			property TextureFilter AutoMipGenerationFilter
			{
				TextureFilter get() { return ( TextureFilter ) InternalPointer->GetAutoGenFilterType(); }
				void set(TextureFilter value) { InternalPointer->SetAutoGenFilterType( (D3DTEXTUREFILTERTYPE) value ); }
			}
			
			/// <summary>
			/// Gets the number of texture levels in a multilevel texture.
			/// </summary>
			property int LevelCount
			{
				int get() { return InternalPointer->GetLevelCount(); }
			}
			
			/// <summary>
			/// Gets or sets the level of detail for the texture.
			/// </summary>
			property int LevelOfDetail
			{
				int get() { return InternalPointer->GetLOD(); }
				void set(int value) { InternalPointer->SetLOD( value ); }
			}
		};
	}
}
