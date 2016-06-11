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

#include "../Result.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		/// <summary>Defines result codes that are returned by Direct3D 10 functions.</summary>
		/// <unmanaged href="ms679692">HRESULT</unmanaged>
		public ref class ResultCode sealed
		{
		private:
			ResultCode();
			
		public:
			/// <summary>
			/// The method call is invalid. For example, a method's parameter may not be a valid pointer.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb205278">D3DERR_INVALIDCALL</unmanaged>
			property static Result InvalidCall
			{
				Result get();
			};
			
			/// <summary>
			/// The file was not found.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb205278">D3D10_ERROR_FILE_NOT_FOUND</unmanaged>
			property static Result FileNotFound
			{
				Result get();
			}

			/// <summary>
			/// There are too many unique instances of a particular type of state object.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb205278">D3D10_ERROR_TOO_MANY_UNIQUE_STATE_OBJECTS</unmanaged>
			property static Result TooManyUniqueStateObjects
			{
				Result get();
			}

			/// <summary>
			/// The previous blit operation that is transferring information to or from this surface is incomplete.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb205278">D3DERR_WASSTILLDRAWING</unmanaged>
			property static Result WasStillDrawing
			{
				Result get();
			}

			/// <summary>
			/// The index buffer cannot be modified. 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172691">D3DX10_ERR_CANNOT_MODIFY_INDEX_BUFFER</unmanaged>
			property static Result CannotModifyIndexBuffer
			{
				Result get();
			}
			
			/// <summary>
			/// The mesh is invalid.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172691">D3DX10_ERR_INVALID_MESH</unmanaged>
			property static Result InvalidMesh
			{
				Result get();
			}
			
			/// <summary>
			/// Attribute sort is not supported as an optimization technique. 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172691">D3DX10_ERR_CANNOT_ATTR_SORT</unmanaged>
			property static Result CannotSortByAttribute
			{
				Result get();
			}
			
			/// <summary>
			/// Skinning is not supported. 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172691">D3DX10_ERR_SKINNING_NOT_SUPPORTED</unmanaged>
			property static Result SkinningNotSupported
			{
				Result get();
			}
			
			/// <summary>
			/// Too many influences specified. 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172691">D3DX10_ERR_TOO_MANY_INFLUENCES</unmanaged>
			property static Result TooManyInfluences
			{
				Result get();
			}
			
			/// <summary>
			/// The data is invalid.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172691">D3DX10_ERR_INVALID_DATA</unmanaged>
			property static Result InvalidData
			{
				Result get();
			}
			
			/// <summary>
			/// The mesh has no data. 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172691">D3DX10_ERR_LOADED_MESH_HAS_NO_DATA</unmanaged>
			property static Result LoadedMeshHasNoData
			{
				Result get();
			}
			
			/// <summary>
			/// A fragment with that name already exists. 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172691">D3DX10_ERR_DUPLICATE_NAMED_FRAGMENT</unmanaged>
			property static Result DuplicateNamedFragment
			{
				Result get();
			}
			
			/// <summary>
			/// The last item cannot be deleted. 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172691">D3DX10_ERR_CANNOT_REMOVE_LAST_ITEM</unmanaged>
			property static Result CannotRemoveLastItem
			{
				Result get();
			}

			/// <summary>
			/// Represents the result of a successful operation.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="aa378137">S_OK</unmanaged>
			property static Result Success
			{
				Result get();
			};

			/// <summary>
			/// Represents a generic failure result.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="aa378137">E_FAIL</unmanaged>
			property static Result Failure
			{
				Result get();
			};

			/// <summary>
			/// An invalid parameter was passed to the returning function.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb205278">E_INVALIDARG</unmanaged>
			property static Result InvalidArgument
			{
				Result get();
			}

			/// <summary>
			/// Direct3D could not allocate sufficient memory to complete the call.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb205278">E_OUTOFMEMORY</unmanaged>
			property static Result OutOfMemory
			{
				Result get();
			}

			/// <summary>
			/// Alternate success value, indicating a successful but nonstandard completion (the precise meaning depends on context).
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb205278">S_FALSE</unmanaged>
			property static Result False
			{
				Result get();
			}
		};
	}
}