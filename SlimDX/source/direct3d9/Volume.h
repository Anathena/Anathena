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

#include "Resource.h"
#include "../DataBox.h"

#include "Box.h"
#include "VolumeDescription.h"

namespace SlimDX
{
	ref class DataStream;

	namespace Direct3D9
	{
		value class ImageInformation;

		/// <summary>Applications use the methods of the Volume class to manipulate volume resources.</summary>
		/// <unmanaged>IDirect3DVolume9</unmanaged>
		public ref class Volume : public Resource
		{
			COMOBJECT(IDirect3DVolume9, Volume);

		private:
			static Result FromFileInMemory_Internal( Volume^ volume, const void* memory, UINT size, Filter filter, int colorKey, Box* sourceBox, Box* destinationBox, PaletteEntry* palette, ImageInformation* imageInformation );

		public:
			virtual ~Volume() { }

			static Result FromFileInMemory( Volume^ volume, array<System::Byte>^ memory, Filter filter, int colorKey, Box sourceBox, Box destinationBox, array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation );
			static Result FromFileInMemory( Volume^ volume, array<System::Byte>^ memory, Filter filter, int colorKey, Box sourceBox, Box destinationBox, [Out] ImageInformation% imageInformation );
			static Result FromFileInMemory( Volume^ volume, array<System::Byte>^ memory, Filter filter, int colorKey, Box sourceBox, Box destinationBox );
			static Result FromFileInMemory( Volume^ volume, array<System::Byte>^ memory, Filter filter, int colorKey );

			static Result FromFileInStream( Volume^ volume, System::IO::Stream^stream, Filter filter, int colorKey, Box sourceBox, Box destinationBox, array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation );
			static Result FromFileInStream( Volume^ volume, System::IO::Stream^stream, Filter filter, int colorKey, Box sourceBox, Box destinationBox, [Out] ImageInformation% imageInformation );
			static Result FromFileInStream( Volume^ volume, System::IO::Stream^stream, Filter filter, int colorKey, Box sourceBox, Box destinationBox );
			static Result FromFileInStream( Volume^ volume, System::IO::Stream^stream, Filter filter, int colorKey );

			static Result FromFile( Volume^ volume, System::String^ fileName, Filter filter, int colorKey, Box sourceBox, Box destinationBox, array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation );
			static Result FromFile( Volume^ volume, System::String^ fileName, Filter filter, int colorKey, Box sourceBox, Box destinationBox, [Out] ImageInformation% imageInformation );
			static Result FromFile( Volume^ volume, System::String^ fileName, Filter filter, int colorKey, Box sourceBox, Box destinationBox );
			static Result FromFile( Volume^ volume, System::String^ fileName, Filter filter, int colorKey );

			static Result FromVolume( Volume^ destinationVolume, Volume^ sourceVolume, Filter filter, int colorKey, Box sourceBox, Box destinationBox, array<PaletteEntry>^ destinationPalette, array<PaletteEntry>^ sourcePalette );
			static Result FromVolume( Volume^ destinationVolume, Volume^ sourceVolume, Filter filter, int colorKey, Box sourceBox, Box destinationBox );
			static Result FromVolume( Volume^ destinationVolume, Volume^ sourceVolume, Filter filter, int colorKey );

			static DataStream^ ToStream( Volume^ volume, ImageFileFormat format, Box box, array<PaletteEntry>^ palette );
			static DataStream^ ToStream( Volume^ volume, ImageFileFormat format, Box box );
			static DataStream^ ToStream( Volume^ volume, ImageFileFormat format );

			static Result ToFile( Volume^ volume, System::String^ fileName, ImageFileFormat format, Box box, array<PaletteEntry>^ palette );
			static Result ToFile( Volume^ volume, System::String^ fileName, ImageFileFormat format, Box box );
			static Result ToFile( Volume^ volume, System::String^ fileName, ImageFileFormat format );

			DataBox^ LockBox( LockFlags flags );
			DataBox^ LockBox( Box box, LockFlags flags );
			Result UnlockBox();

			property VolumeDescription Description
			{
				VolumeDescription get();
			}
		};
	}
}
