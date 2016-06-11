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

#include "../DataBox.h"
#include "../DataRectangle.h"

#include "BaseTexture.h"
#include "Surface.h"
#include "Volume.h"
#include "Effect9.h"
#include "ConstantTable.h"
#include "TextureShader.h"
#include "Delegates.h"

#include "ImageInformation.h"
#include "TextureRequirements.h"

#include "CubeTexture.h"
#include "VolumeTexture.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>
		/// Represents a two dimensional texture resource.
		/// </summary>
		/// <unmanaged>IDirect3DTexture9</unmanaged>
		public ref class Texture : public BaseTexture
		{
			COMOBJECT(IDirect3DTexture9, Texture);

		private:
			static Texture^ Texture::FromMemory_Internal( SlimDX::Direct3D9::Device^ device, const void* memory, UINT size, int width, int height, int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey, ImageInformation* imageInformation, PaletteEntry* palette );

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="Texture"/> class.
			/// </summary>
			/// <param name="device">The device used to create the texture.</param>
			/// <param name="width">The width of the texture, in pixels.</param>
			/// <param name="height">The height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels in the texture. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			Texture( SlimDX::Direct3D9::Device^ device, int width, int height, int levelCount, Usage usage, Format format, Pool pool );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Texture"/> class, setting it as a shared resource.
			/// </summary>
			/// <param name="device">The device used to create the texture.</param>
			/// <param name="width">The width of the texture, in pixels.</param>
			/// <param name="height">The height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels in the texture. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="sharedHandle">The variable that will receive the shared handle for this resource.</param>
			/// <remarks>This method is only available in Direct3D9 Ex.</remarks>
			Texture( SlimDX::Direct3D9::Device^ device, int width, int height, int levelCount, Usage usage, Format format, Pool pool, System::IntPtr% sharedHandle );
			
			/// <summary>
			/// Releases all resources used by the <see cref="Texture"/>.
			/// </summary>
			virtual ~Texture() { }

			/// <summary>
			/// Checks texture-creation parameters.
			/// </summary>
			/// <param name="device">Device associated with the texture.</param>
			/// <param name="width">Requested width of the texture.</param>
			/// <param name="height">Requested height of the texture.</param>
			/// <param name="mipLevelCount">Requested number of mipmap levels for the texture.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">Requested format for the texture.</param>
			/// <param name="pool">Memory class where the resource will be placed.</param>
			/// <returns>A value type containing the proposed values to pass to the texture creation functions.</returns>
			static TextureRequirements CheckRequirements( SlimDX::Direct3D9::Device^ device, int width, int height, int mipLevelCount, Usage usage, Format format, Pool pool );

			/// <summary>
			/// Creates a texture from a specified block of memory.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="memory">The memory block representing the texture.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <param name="imageInformation">When the method completes, contains additional information about the texture.</param>
			/// <param name="palette">When the method completes, contains the texture palette.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromMemory( SlimDX::Direct3D9::Device^ device, array<System::Byte>^ memory, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey, [Out] ImageInformation% imageInformation, [Out] array<PaletteEntry>^% palette );
			
			/// <summary>
			/// Creates a texture from a specified block of memory.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="memory">The memory block representing the texture.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <param name="imageInformation">When the method completes, contains additional information about the texture.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromMemory( SlimDX::Direct3D9::Device^ device, array<System::Byte>^ memory, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey, [Out] ImageInformation% imageInformation );

			/// <summary>
			/// Creates a texture from a specified block of memory.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="memory">The memory block representing the texture.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromMemory( SlimDX::Direct3D9::Device^ device, array<System::Byte>^ memory, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey );

			/// <summary>
			/// Creates a texture from a specified block of memory.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="memory">The memory block representing the texture.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromMemory( SlimDX::Direct3D9::Device^ device, array<System::Byte>^ memory, Usage usage, Pool pool );
			
			/// <summary>
			/// Creates a texture from a specified block of memory.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="memory">The memory block representing the texture.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromMemory( SlimDX::Direct3D9::Device^ device, array<System::Byte>^ memory );

			/// <summary>
			/// Creates a texture from a stream.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="stream">The stream containing the texture data.</param>
			/// <param name="sizeBytes">The size of the data, in bytes.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <param name="imageInformation">When the method completes, contains additional information about the texture.</param>
			/// <param name="palette">When the method completes, contains the texture palette.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromStream( SlimDX::Direct3D9::Device^ device, System::IO::Stream^ stream, int sizeBytes, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey, [Out] ImageInformation% imageInformation, [Out] array<PaletteEntry>^% palette );
			
			/// <summary>
			/// Creates a texture from a stream.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="stream">The stream containing the texture data.</param>
			/// <param name="sizeBytes">The size of the data, in bytes.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <param name="imageInformation">When the method completes, contains additional information about the texture.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromStream( SlimDX::Direct3D9::Device^ device, System::IO::Stream^ stream, int sizeBytes, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey, [Out] ImageInformation% imageInformation );
			
			/// <summary>
			/// Creates a texture from a stream.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="stream">The stream containing the texture data.</param>
			/// <param name="sizeBytes">The size of the data, in bytes.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromStream( SlimDX::Direct3D9::Device^ device, System::IO::Stream^ stream, int sizeBytes, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey );
			
			/// <summary>
			/// Creates a texture from a stream.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="stream">The stream containing the texture data.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromStream( SlimDX::Direct3D9::Device^ device, System::IO::Stream^ stream, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey );
			
			/// <summary>
			/// Creates a texture from a stream.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="stream">The stream containing the texture data.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromStream( SlimDX::Direct3D9::Device^ device, System::IO::Stream^ stream, Usage usage, Pool pool );
			
			/// <summary>
			/// Creates a texture from a stream.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="stream">The stream containing the texture data.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromStream( SlimDX::Direct3D9::Device^ device, System::IO::Stream^ stream );

			/// <summary>
			/// Loads a texture from file.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="fileName">The name of the file containing the image.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <param name="imageInformation">When the method completes, contains additional information about the texture.</param>
			/// <param name="palette">When the method completes, contains the texture palette.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromFile( SlimDX::Direct3D9::Device^ device, System::String^ fileName, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey, [Out] ImageInformation% imageInformation, [Out] array<PaletteEntry>^% palette );
			
			/// <summary>
			/// Loads a texture from file.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="fileName">The name of the file containing the image.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <param name="imageInformation">When the method completes, contains additional information about the texture.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromFile( SlimDX::Direct3D9::Device^ device, System::String^ fileName, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey, [Out] ImageInformation% imageInformation );
			
			/// <summary>
			/// Loads a texture from file.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="fileName">The name of the file containing the image.</param>
			/// <param name="width">Width of the texture, in pixels.</param>
			/// <param name="height">Height of the texture, in pixels.</param>
			/// <param name="levelCount">Number of mipmap levels requested. If set to 0, mipmaps will automatically be generated.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="format">The surface format of the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <param name="filter">Combination of flags controlling how the image is filtered.</param>
			/// <param name="mipFilter">Combination of flags controlling how the mipmaps are filtered.</param>
			/// <param name="colorKey">Color value in the image to replace with black, or 0 to disable the color key.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromFile( SlimDX::Direct3D9::Device^ device, System::String^ fileName, int width, int height, int levelCount, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey );
			
			/// <summary>
			/// Loads a texture from file.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="fileName">The name of the file containing the image.</param>
			/// <param name="usage">The requested usage for the texture.</param>
			/// <param name="pool">The memory class into which the texture should be placed.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromFile( SlimDX::Direct3D9::Device^ device, System::String^ fileName, Usage usage, Pool pool );
			
			/// <summary>
			/// Loads a texture from file.
			/// </summary>
			/// <param name="device">The device to be associated with the texture.</param>
			/// <param name="fileName">The name of the file containing the image.</param>
			/// <returns>The created texture object.</returns>
			static Texture^ FromFile( SlimDX::Direct3D9::Device^ device, System::String^ fileName );

			/// <summary>
			/// Converts a height map into a normal map.
			/// </summary>
			/// <param name="texture">The destination texture.</param>
			/// <param name="sourceTexture">The source height map texture.</param>
			/// <param name="palette">The texture palette.</param>
			/// <param name="flags">Flags the control the generation of normal maps.</param>
			/// <param name="channel">Flags specifying the source of height information.</param>
			/// <param name="amplitude">Constant multiplier that increases or decreases values in the map. Higher values make bumps more visible, while lower values make them less visible.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result ComputeNormalMap( Texture^ texture, Texture^ sourceTexture, array<PaletteEntry>^ palette, NormalMapFlags flags, Channel channel, float amplitude );
			
			/// <summary>
			/// Converts a height map into a normal map.
			/// </summary>
			/// <param name="texture">The destination texture.</param>
			/// <param name="sourceTexture">The source height map texture.</param>
			/// <param name="flags">Flags the control the generation of normal maps.</param>
			/// <param name="channel">Flags specifying the source of height information.</param>
			/// <param name="amplitude">Constant multiplier that increases or decreases values in the map. Higher values make bumps more visible, while lower values make them less visible.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result ComputeNormalMap( Texture^ texture, Texture^ sourceTexture, NormalMapFlags flags, Channel channel, float amplitude );

			/// <summary>
			/// Uses a user-provided function to fill each texel of each mip level of a given texture.
			/// </summary>
			/// <param name="callback">A function that is used to fill the texture.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
			Result Fill(Fill2DCallback^ callback);

			/// <summary>
			/// Uses a compiled HLSL shader function to fill each texel of each mip level of a given texture.
			/// </summary>
			/// <param name="shader">A texture shader object that is used to fill the texture.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Fill( TextureShader^ shader );

			/// <summary>
			/// Locks a texture resource.
			/// </summary>
			/// <param name="level">The level of the texture to lock.</param>
			/// <param name="flags">Locking flags.</param>
			/// <returns>A <see cref="DataRectangle"/> describing the locked region.</returns>
			DataRectangle^ LockRectangle( int level, LockFlags flags );

			/// <summary>
			/// Locks a rectangle on a texture resource.
			/// </summary>
			/// <param name="level">The level of the texture to lock.</param>
			/// <param name="rectangle">The region on the texture to lock.</param>
			/// <param name="flags">Locking flags.</param>
			/// <returns>A <see cref="DataRectangle"/> describing the locked region.</returns>
			DataRectangle^ LockRectangle( int level, System::Drawing::Rectangle rectangle, LockFlags flags );

			/// <summary>
			/// Unlocks a previously locked region of a texture.
			/// </summary>
			/// <param name="level">The level of the texture to unlock.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result UnlockRectangle( int level );

			/// <summary>
			/// Adds a dirty region to a texture resource.
			/// </summary>
			/// <param name="rectangle">A rectangle specifying the dirty region to add.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result AddDirtyRectangle( System::Drawing::Rectangle rectangle );

			/// <summary>
			/// Marks the entire texture as dirty.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result AddDirtyRectangle();

			/// <summary>
			/// Retrieves a level description of a texture resource.
			/// </summary>
			/// <param name="level">Identifies the level whose description is to be retrieved.</param>
			/// <returns>The surface description of the specified level.</returns>
			SurfaceDescription GetLevelDescription( int level );

			/// <summary>
			/// Retrieves the specified surface level.
			/// </summary>
			/// <param name="level">Identifies the level whose surface is to be retrieved.</param>
			/// <returns>The surface of the specified level.</returns>
			Surface^ GetSurfaceLevel( int level );
		};
	}
}
