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
	namespace Direct3D9
	{
		/// <summary>Defines result codes that are returned by Direct3D 9 functions.</summary>
		/// <unmanaged href="ms679692">HRESULT</unmanaged>
		public ref class ResultCode sealed
		{
		private:
			ResultCode();
			
		public:
			/// <summary>
			/// Occurs when attribute sorting is not supported as an optimization technique.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DXERR_CANNOTATTRSORT</unmanaged>
			property static Result CannotAttributeSort
			{
				Result get();
			};

			/// <summary>
			/// Occurs when a user tries to modify an immutable index buffer.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DXERR_CANNOTMODIFYINDEXBUFFER</unmanaged>
			property static Result CannotModifyIndexBuffer
			{
				Result get();
			};

			/// <summary>
			/// Occurs when a user tries to remove the last item in a collection.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DXERR_CANNOTREMOVELASTITEM</unmanaged>
			property static Result CannotRemoveLastItem
			{
				Result get();
			};

			/// <summary>
			/// Occurs when two set render states conflict with each other.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_CONFLICTINGRENDERSTATE</unmanaged>
			property static Result ConflictingRenderState
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when two set texture filters conflict with each other.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_CONFLICTINGTEXTUREFILTER</unmanaged>
			property static Result ConflictingTextureFilter
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when two texture palettes conflict with each other.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_CONFLICTINGTEXTUREPALETTE</unmanaged>
			property static Result ConflictingTexturePalette
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when a device is hung.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_DEVICEHUNG</unmanaged>
			property static Result DeviceHung
			{
				Result get();
			}

			/// <summary>
			/// Occurs when a device is lost.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172554">D3DERR_DEVICELOST</unmanaged>
			property static Result DeviceLost
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when a lost yet resettable device has not yet been reset.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172554">D3DERR_DEVICENOTRESET</unmanaged>
			property static Result DeviceNotReset
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when a device is removed from the system.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_DEVICEREMOVED</unmanaged>
			property static Result DeviceRemoved
			{
				Result get();
			};

			/// <summary>
			/// Occurs when a driver internal error is thrown.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_DRIVERINTERNALERROR</unmanaged>
			property static Result DriverInternalError
			{
				Result get();
			};

			/// <summary>
			/// Occurs when a fragment with the specified name already exists.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DXERR_DUPLICATENAMEDFRAGMENT</unmanaged>
			property static Result DuplicateNamedFragment
			{
				Result get();
			};

			/// <summary>
			/// Represents a generic failure result.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="aa378137">E_FAIL</unmanaged>
			property static Result Failure
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when an invalid method call is made. This is usually do to an invalid parameter.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172554">D3DERR_INVALIDCALL</unmanaged>
			property static Result InvalidCall
			{
				Result get();
			};

			/// <summary>
			/// Occurs when passed data is invalid.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DXERR_INVALIDDATA</unmanaged>
			property static Result InvalidData
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when a requested device type is invalid.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_INVALIDDEVICE</unmanaged>
			property static Result InvalidDevice
			{
				Result get();
			};

			/// <summary>
			/// Occurs when an invalid mesh is passed.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DXERR_INVALIDMESH</unmanaged>
			property static Result InvalidMesh
			{
				Result get();
			};

			/// <summary>
			/// Occurs when a loaded mesh has no data.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DXERR_LOADEDMESHASNODATA</unmanaged>
			property static Result LoadedMeshHasNoData
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when there is more data available than the specified buffer can hold.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_MOREDATA</unmanaged>
			property static Result MoreData
			{
				Result get();
			};

			/// <summary>
			/// Occurs when a method succeeds but mipmaps could not be automatically generated.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged>D3DOK_NOAUTOGEN</unmanaged>
			property static Result NoAutomaticGeneration
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the device does not support the queried technique.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb172554">D3DERR_NOTAVAILABLE</unmanaged>
			property static Result NotAvailable
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the requested item cannot be found.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_NOTFOUND</unmanaged>
			property static Result NotFound
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the resource is not resident in memory.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb174339">S_NOT_RESIDENT</unmanaged>
			property static Result NotResident
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the driver runs out of video memory.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_OUTOFVIDEOMEMORY</unmanaged>
			property static Result OutOfVideoMemory
			{
				Result get();
			};

			/// <summary>
			/// Occurs when the desktop display mode changes.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged>S_PRESENT_MODE_CHANGED</unmanaged>
			property static Result PresentModeChanged
			{
				Result get();
			};

			/// <summary>
			/// Occurs when the presentation area is occluded.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219624">S_PRESENT_OCCLUDED</unmanaged>
			property static Result PresentOccluded
			{
				Result get();
			};

			/// <summary>
			/// Occurs when the resource is resident in shared memory.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb174339">S_RESIDENT_IN_SHARED_MEMORY</unmanaged>
			property static Result ResidentInSharedMemory
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when skinning is not supported.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DXERR_SKINNINGNOTSUPPORTED</unmanaged>
			property static Result SkinningNotSupported
			{
				Result get();
			};

			/// <summary>
			/// Represents a generic success result.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb172554">D3D_OK</unmanaged>
			property static Result Success
			{
				Result get();
			};

			/// <summary>
			/// Occurs when too many vertex influences are specified.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DXERR_TOOMANYINFLUENCES</unmanaged>
			property static Result TooManyInfluences
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when more texture-filtering operations are requested than the device can support.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_TOOMANYOPERATIONS</unmanaged>
			property static Result TooManyOperations
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the device does not support the requested alpha blending argument.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_UNSUPPORTEDALPHAARG</unmanaged>
			property static Result UnsupportedAlphaArgument
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the device does not support the requested alpha blending operation.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_UNSUPPORTEDALPHAOPERATION</unmanaged>
			property static Result UnsupportedAlphaOperation
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the device not support the requested color blending argument.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_UNSUPPORTEDCOLORARG</unmanaged>
			property static Result UnsupportedColorArgument
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the device does not support the requested color blending operation.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_UNSUPPORTEDCOLOROPERATION</unmanaged>
			property static Result UnsupportedColorOperation
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the device does not support the requested texture factor value.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_UNSUPPORTEDFACTORVALUE</unmanaged>
			property static Result UnsupportedFactorValue
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the device does not support the requested texture filter.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_UNSUPPORTEDTEXTUREFILTER</unmanaged>
			property static Result UnsupportedTextureFilter
			{
				Result get();
			};
		
			/// <summary>
			/// Occurs when a previous draw operation has not yet completed at the time of a new draw call.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_WASSTILLDRAWING</unmanaged>
			property static Result WasStillDrawing
			{
				Result get();
			};
			
			/// <summary>
			/// Occurs when the pixel format for a texture is invalid.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D3DERR_WRONGTEXTUREFORMAT</unmanaged>
			property static Result WrongTextureFormat
			{
				Result get();
			};
		};
	}
}