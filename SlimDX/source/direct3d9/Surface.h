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
#include "../DataRectangle.h"

#include "SurfaceDescription9.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	ref class DataStream;

	namespace Direct3D9
	{
		value class ImageInformation;
		ref class DeviceEx;

		/// <summary>Applications use the methods of the Surface class to query and prepare surfaces.</summary>
		/// <unmanaged>IDirect3DSurface9</unmanaged>
		public ref class Surface : public Resource
		{
			COMOBJECT(IDirect3DSurface9, Surface);

		private:
			static Result FromMemory_Internal( Surface^ surface, const void* memory, Filter filter, int colorKey, Format sourceFormat, int sourcePitch, const RECT* sourceRectangle, const RECT* destinationRectangle, array<PaletteEntry>^ sourcePalette, array<PaletteEntry>^ destinationPalette );
			static Result FromFileInMemory_Internal( Surface^ surface, const void* memory, UINT size, Filter filter, int colorKey, const RECT* sourceRectangle, const RECT* destinationRectangle, array<PaletteEntry>^ palette, ImageInformation* imageInformation );

			int ComputeLockedSize(const D3DLOCKED_RECT& lockedRect);

		public:
			virtual ~Surface() { }

			/// <summary>Creates a render target surface.</summary>
			/// <param name="device">The device to use when creating the surface.</param>
			/// <param name="width">The width of the render target surface, in pixels.</param>
			/// <param name="height">The height of the render target surface, in pixels.</param>
			/// <param name="format">The format to use for this render target.</param>
			/// <param name="multisampleType">The multisample type of the buffer.</param>
			/// <param name="multisampleQuality">The multisampling quality level.</param>
			/// <param name="lockable">Specifies whether this surface should be lockable.</param>
			/// <returns>A newly created render target surface.</returns>
			/// <unmanaged>IDirect3DDevice9::CreateRenderTarget</unmanaged>
			static Surface^ CreateRenderTarget( SlimDX::Direct3D9::Device^ device, int width, int height, Format format, MultisampleType multisampleType, int multisampleQuality, bool lockable );
			
			/// <summary>Creates an offscreen surface.</summary>
			/// <param name="device">The device to use when creating the surface.</param>
			/// <param name="width">The width of the surface, in pixels.</param>
			/// <param name="height">The height of the surface, in pixels.</param>
			/// <param name="format">Describes the format to use.</param>
			/// <param name="pool">The memory pool to create the surface in.</param>
			/// <returns>A newly created offscreen surface..</returns>
			/// <unmanaged>IDirect3DDevice9::CreateOffscreenPlainSurface</unmanaged>
			static Surface^ CreateOffscreenPlain( SlimDX::Direct3D9::Device^ device, int width, int height, Format format, Pool pool );
			
			/// <summary>Creates a depth stencil surface.</summary>
			/// <param name="device">The device to use when creating the surface.</param>
			/// <param name="width">The width of the surface, in pixels.</param>
			/// <param name="height">The height of the surface, in pixels.</param>
			/// <param name="format">Describes the format to use.</param>
			/// <param name="multisampleType">The multisample type of the buffer.</param>
			/// <param name="multisampleQuality">The multisampling quality level.</param>
			/// <param name="discard">Whether or not the contents of the surface should be discarded after use.</param>
			/// <returns>A newly created depth-stencil surface.</returns>
			/// <unmanaged>IDirect3DDevice9::CreateDepthStencilSurface</unmanaged>
			static Surface^ CreateDepthStencil( SlimDX::Direct3D9::Device^ device, int width, int height, Format format, MultisampleType multisampleType, int multisampleQuality, bool discard );

			/// <summary>Creates a render target surface that can optionally be shared.</summary>
			/// <param name="device">The device to use when creating the surface.</param>
			/// <param name="width">The width of the render target surface, in pixels.</param>
			/// <param name="height">The height of the render target surface, in pixels.</param>
			/// <param name="format">The format to use for this render target.</param>
			/// <param name="multisampleType">The multisample type of the buffer.</param>
			/// <param name="multisampleQuality">The multisampling quality level.</param>
			/// <param name="lockable">Specifies whether this surface should be lockable.</param>
			/// <returns>A newly created render target surface.</returns>
			/// <unmanaged>IDirect3DDevice9::CreateRenderTarget</unmanaged>
			static Surface^ CreateRenderTarget( SlimDX::Direct3D9::Device^ device, int width, int height, Format format, MultisampleType multisampleType, int multisampleQuality, bool lockable, System::IntPtr% sharedHandle );
			
			/// <summary>Creates an offscreen surface that can optionally be shared.</summary>
			/// <param name="device">The device to use when creating the surface.</param>
			/// <param name="width">The width of the surface, in pixels.</param>
			/// <param name="height">The height of the surface, in pixels.</param>
			/// <param name="format">Describes the format to use.</param>
			/// <param name="pool">The memory pool to create the surface in.</param>
			/// <returns>A newly created offscreen surface..</returns>
			/// <unmanaged>IDirect3DDevice9::CreateOffscreenPlainSurface</unmanaged>
			static Surface^ CreateOffscreenPlain( SlimDX::Direct3D9::Device^ device, int width, int height, Format format, Pool pool, System::IntPtr% sharedHandle );
			
			/// <summary>Creates a depth stencil surface that can optionally be shared.</summary>
			/// <param name="device">The device to use when creating the surface.</param>
			/// <param name="width">The width of the surface, in pixels.</param>
			/// <param name="height">The height of the surface, in pixels.</param>
			/// <param name="format">Describes the format to use.</param>
			/// <param name="multisampleType">The multisample type of the buffer.</param>
			/// <param name="multisampleQuality">The multisampling quality level.</param>
			/// <param name="discard">Whether or not the contents of the surface should be discarded after use.</param>
			/// <returns>A newly created depth-stencil surface.</returns>
			/// <unmanaged>IDirect3DDevice9::CreateDepthStencilSurface</unmanaged>
			static Surface^ CreateDepthStencil( SlimDX::Direct3D9::Device^ device, int width, int height, Format format, MultisampleType multisampleType, int multisampleQuality, bool discard, System::IntPtr% sharedHandle );

			static Surface^ CreateRenderTargetEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format, MultisampleType multisampleType, int multisampleQuality, bool lockable, Usage usage );
			static Surface^ CreateRenderTargetEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format, MultisampleType multisampleType, int multisampleQuality, bool lockable, Usage usage, System::IntPtr% sharedHandle );
			static Surface^ CreateOffscreenPlainEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format, Pool pool, Usage usage );
			static Surface^ CreateOffscreenPlainEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format, Pool pool, Usage usage, System::IntPtr% sharedHandle );
			static Surface^ CreateDepthStencilEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format, MultisampleType multisampleType, int multisampleQuality, bool discard, Usage usage );
			static Surface^ CreateDepthStencilEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format, MultisampleType multisampleType, int multisampleQuality, bool discard, Usage usage, System::IntPtr% sharedHandle );

			/// <summary>Loads a surface from memory.</summary>
			/// <param name="surface">The destination surface that will receive the image.</param>
			/// <param name="memory">The array containing the surface data</param>
			/// <param name="filter">Combination of one or more flags controlling how the image is filtered.</param>
			/// <param name="colorKey">A color value to replace with transparent black, or 0 to disable colorkey.</param>
			/// <param name="sourceFormat">Pixel format of the source image.</param>
			/// <param name="sourcePitch">Surface pitch of the source image, in bytes.</param>
			/// <param name="sourceRectangle">Specifies the dimensions of the source image.</param>
			/// <param name="destinationRectangle">Specifies the destination rectangle to use.</param>
			/// <param name="sourcePalette">The source image palette (256 colors).</param>
			/// <param name="destinationPalette">The destination palette (256 colors).</param>
			/// <returns>A result code indicating success or failure.</returns>
			static Result FromMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey, Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, array<PaletteEntry>^ sourcePalette, array<PaletteEntry>^ destinationPalette );
			
			/// <summary>Loads a surface from memory.</summary>
			/// <param name="surface">The destination surface that will receive the image.</param>
			/// <param name="memory">The array containing the surface data</param>
			/// <param name="filter">Combination of one or more flags controlling how the image is filtered.</param>
			/// <param name="colorKey">A color value to replace with transparent black, or 0 to disable colorkey.</param>
			/// <param name="sourceFormat">Pixel format of the source image.</param>
			/// <param name="sourcePitch">Surface pitch of the source image, in bytes.</param>
			/// <param name="sourceRectangle">Specifies the dimensions of the source image.</param>
			/// <param name="sourcePalette">The source image palette (256 colors).</param>
			/// <param name="destinationPalette">The destination palette (256 colors).</param>
			/// <returns>A result code indicating success or failure.</returns>
			static Result FromMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey, Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle, array<PaletteEntry>^ sourcePalette, array<PaletteEntry>^ destinationPalette );
			
			/// <summary>Loads a surface from memory.</summary>
			/// <param name="surface">The destination surface that will receive the image.</param>
			/// <param name="memory">The array containing the surface data</param>
			/// <param name="filter">Combination of one or more flags controlling how the image is filtered.</param>
			/// <param name="colorKey">A color value to replace with transparent black, or 0 to disable colorkey.</param>
			/// <param name="sourceFormat">Pixel format of the source image.</param>
			/// <param name="sourcePitch">Surface pitch of the source image, in bytes.</param>
			/// <param name="sourceRectangle">Specifies the dimensions of the source image.</param>
			/// <param name="destinationRectangle">Specifies the destination rectangle to use.</param>
			/// <returns>A result code indicating success or failure.</returns>
			static Result FromMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey, Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle );
			
			/// <summary>Loads a surface from memory.</summary>
			/// <param name="surface">The destination surface that will receive the image.</param>
			/// <param name="memory">The array containing the surface data</param>
			/// <param name="filter">Combination of one or more flags controlling how the image is filtered.</param>
			/// <param name="colorKey">A color value to replace with transparent black, or 0 to disable colorkey.</param>
			/// <param name="sourceFormat">Pixel format of the source image.</param>
			/// <param name="sourcePitch">Surface pitch of the source image, in bytes.</param>
			/// <param name="sourceRectangle">Specifies the dimensions of the source image.</param>
			/// <returns>A result code indicating success or failure..</returns>
			static Result FromMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey, Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle );

			/// <summary></summary>
			/// <param name="surface">			   </param>
			/// <param name="stream">			   </param>
			/// <param name="filter">			   </param>
			/// <param name="colorKey">			   </param>
			/// <param name="sourceFormat">		   </param>
			/// <param name="sourcePitch">		   </param>
			/// <param name="sourceRectangle">	   </param>
			/// <param name="destinationRectangle"></param>
			/// <param name="sourcePalette">	   </param>
			/// <param name="destinationPalette">  </param>
			/// <returns>.</returns>
			static Result FromStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey, Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, array<PaletteEntry>^ sourcePalette, array<PaletteEntry>^ destinationPalette );
			static Result FromStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey, Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle, array<PaletteEntry>^ sourcePalette, array<PaletteEntry>^ destinationPalette );
			static Result FromStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey, Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle );
			static Result FromStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey, Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle );

			static Result FromFileInMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation );
			static Result FromFileInMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, [Out] ImageInformation% imageInformation );
			static Result FromFileInMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle );
			static Result FromFileInMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey );

			static Result FromFileInStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation );
			static Result FromFileInStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, [Out] ImageInformation% imageInformation );
			static Result FromFileInStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle );
			static Result FromFileInStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey );

			static Result FromFile( Surface^ surface, System::String^ fileName, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation );
			static Result FromFile( Surface^ surface, System::String^ fileName, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, [Out] ImageInformation% imageInformation );
			static Result FromFile( Surface^ surface, System::String^ fileName, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle );
			static Result FromFile( Surface^ surface, System::String^ fileName, Filter filter, int colorKey );

			static Result FromSurface( Surface^ destinationSurface, Surface^ sourceSurface, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, array<PaletteEntry>^ destinationPalette, array<PaletteEntry>^ sourcePalette );
			static Result FromSurface( Surface^ destinationSurface, Surface^ sourceSurface, Filter filter, int colorKey, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle );
			static Result FromSurface( Surface^ destinationSurface, Surface^ sourceSurface, Filter filter, int colorKey );

			static DataStream^ ToStream( Surface^ surface, ImageFileFormat format, System::Drawing::Rectangle rectangle, array<PaletteEntry>^ palette );
			static DataStream^ ToStream( Surface^ surface, ImageFileFormat format, System::Drawing::Rectangle rectangle );
			static DataStream^ ToStream( Surface^ surface, ImageFileFormat format );

			static Result ToFile( Surface^ surface, System::String^ fileName, ImageFileFormat format, System::Drawing::Rectangle rectangle, array<PaletteEntry>^ palette );
			static Result ToFile( Surface^ surface, System::String^ fileName, ImageFileFormat format, System::Drawing::Rectangle rectangle );
			static Result ToFile( Surface^ surface, System::String^ fileName, ImageFileFormat format );

			/// <summary>Locks a rectangle on the surface. </summary>
			/// <param name="flags">Combination of flags describing the type of lock to perform.</param>
			/// <returns>Data describing the locked area.</returns>
			DataRectangle^ LockRectangle( LockFlags flags );

			/// <summary>Locks a rectangle on the surface.</summary>
			/// <param name="rect">The area to lock.</param>
			/// <param name="flags">Combination of flags describing the type of lock to perform.</param>
			/// <returns>Data describing the locked area.</returns>
			DataRectangle^ LockRectangle( System::Drawing::Rectangle rect, LockFlags flags );
			
			/// <summary>Unlocks a rectangle on the surface.</summary>
			/// <returns>A result code indicating whether the unlock was successful.</returns>
			Result UnlockRectangle();

			System::IntPtr GetDC();
			Result ReleaseDC( System::IntPtr hdc );

			generic<typename TContainer> where TContainer : ComObject
				TContainer GetContainer();

			/// <summary>Returns a description of the surface.</summary>
			property SurfaceDescription Description
			{
				SurfaceDescription get();
			}
		};
	}
}
