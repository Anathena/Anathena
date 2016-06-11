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

#include "../ComObject.h"

#include "../dxgi/Enums.h"

#include "DeviceChild10.h"
#include "Enums.h"
#include "../dxgi/SwapChainDxgi.h"
#include "../dxgi/SurfaceDxgi.h"

#include "TextureLoadInformation10.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		/// <summary>
		/// A resource object.
		/// </summary>
		/// <unmanaged>ID3D10Resource</unmanaged>
		public ref class Resource abstract : public DeviceChild
		{
			COMOBJECT_BASE(ID3D10Resource);

		internal:
			static int GetMipSize( int mipSlice, int baseSliceSize );
			
			static ID3D10Resource* ConstructFromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName, D3DX10_IMAGE_LOAD_INFO* info );
			static ID3D10Resource* ConstructFromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory, D3DX10_IMAGE_LOAD_INFO* info );
			static ID3D10Resource* ConstructFromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, int sizeInBytes, D3DX10_IMAGE_LOAD_INFO* info );
			
			static Resource^ FromPointer( ID3D10Resource* pointer );

		protected:
			Resource() { }

		public:
			/// <summary>
			/// Returns a DXGI Surface for this resource.
			/// </summary>
			/// <returns>The buffer interface, or <c>null</c> on failure.</returns>
			SlimDX::DXGI::Surface^ AsSurface();

			/// <summary>
			/// Gets a swap chain back buffer.
			/// </summary>
			/// <typeparam name="T">The type of the buffer.</typeparam>
			/// <param name="swapChain">The swap chain to get the buffer from.</param>
			/// <param name="index">The index of the desired buffer.</param>
			/// <returns>The buffer interface, or <c>null</c> on failure.</returns>
			generic< class T > where T : Resource, ref class
			static T FromSwapChain( SlimDX::DXGI::SwapChain^ swapChain, int index );

			static Resource^ FromPointer( System::IntPtr pointer );

			/// <summary>
			/// Gets or sets the resource's eviction priority.
			/// </summary>
			property DXGI::ResourcePriority EvictionPriority
			{
				DXGI::ResourcePriority get();
				void set(DXGI::ResourcePriority value);
			}
			
			/// <summary>
			/// Gets the resource's dimension (type).
			/// </summary>
			property ResourceDimension Dimension
			{
				ResourceDimension get();
			}
			
			static Result SaveTextureToFile( Resource^ resource, ImageFileFormat destinationFormat, System::String^ destinationFile );
			static Result LoadTextureFromTexture(Resource^ source, Resource^ destination, TextureLoadInformation loadInformation);
			static Result FilterTexture(Resource^ texture, int sourceLevel, FilterFlags mipFilter);
			
			/// <summary>
			/// Calculates a subresource index.
			/// </summary>
			/// <param name="mipSlice">The index of the desired mip slice.</param>
			/// <param name="arraySlice">The index of the desired array slice.</param>
			/// <param name="mipLevels">The total number of mip levels.</param>
			/// <returns>The subresource index (equivalent to mipSlice + (arraySlice * mipLevels)).</returns>
			static int CalculateSubresourceIndex( int mipSlice, int arraySlice, int mipLevels );

			literal int MaximumMipLevels = D3D10_REQ_MIP_LEVELS;
			literal int ResourceSizeInMegabytes = D3D10_REQ_RESOURCE_SIZE_IN_MEGABYTES;
			literal int MaximumTexture1DArraySize = D3D10_REQ_TEXTURE1D_ARRAY_AXIS_DIMENSION;
			literal int MaximumTexture2DArraySize = D3D10_REQ_TEXTURE1D_ARRAY_AXIS_DIMENSION;
			literal int MaximumTexture1DSize = D3D10_REQ_TEXTURE1D_U_DIMENSION;
			literal int MaximumTexture2DSize = D3D10_REQ_TEXTURE2D_U_OR_V_DIMENSION;
			literal int MaximumTexture3DSize = D3D10_REQ_TEXTURE3D_U_V_OR_W_DIMENSION;
			literal int MaximumTextureCubeSize = D3D10_REQ_TEXTURECUBE_DIMENSION;
		};
	}
};