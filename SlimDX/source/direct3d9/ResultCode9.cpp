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

#include "ResultCode9.h"

namespace SlimDX
{
namespace Direct3D9
{
	ResultCode::ResultCode()
	{
	}

	Result ResultCode::CannotAttributeSort::get()
	{
		return Result( D3DXERR_CANNOTATTRSORT );
	}
	
	Result ResultCode::CannotModifyIndexBuffer::get()
	{
		return Result( D3DXERR_CANNOTMODIFYINDEXBUFFER );
	}

	Result ResultCode::CannotRemoveLastItem::get()
	{
		return Result( D3DXERR_CANNOTREMOVELASTITEM );
	}
	
	Result ResultCode::ConflictingRenderState::get()
	{
		return Result( D3DERR_CONFLICTINGRENDERSTATE );
	}
	
	Result ResultCode::ConflictingTextureFilter::get()
	{
		return Result( D3DERR_CONFLICTINGTEXTUREFILTER );
	}
	
	Result ResultCode::ConflictingTexturePalette::get()
	{
		return Result( D3DERR_CONFLICTINGTEXTUREPALETTE );
	}
	
	Result ResultCode::DeviceHung::get()
	{
		return Result( D3DERR_DEVICEHUNG );
	}

	Result ResultCode::DeviceLost::get()
	{
		return Result( D3DERR_DEVICELOST );
	}
	
	Result ResultCode::DeviceNotReset::get()
	{
		return Result( D3DERR_DEVICENOTRESET );
	}
	
	Result ResultCode::DeviceRemoved::get()
	{
		return Result( D3DERR_DEVICEREMOVED );
	}
	
	Result ResultCode::DriverInternalError::get()
	{
		return Result( D3DERR_DRIVERINTERNALERROR );
	}

	Result ResultCode::DuplicateNamedFragment::get()
	{
		return Result( D3DXERR_DUPLICATENAMEDFRAGMENT );
	}

	Result ResultCode::Failure::get()
	{
		return Result( E_FAIL );
	}
	
	Result ResultCode::InvalidCall::get()
	{
		return Result( D3DERR_INVALIDCALL );
	}

	Result ResultCode::InvalidData::get()
	{
		return Result( D3DXERR_INVALIDDATA );
	}
	
	Result ResultCode::InvalidDevice::get()
	{
		return Result( D3DERR_INVALIDDEVICE );
	}

	Result ResultCode::InvalidMesh::get()
	{
		return Result( D3DXERR_INVALIDMESH );
	}

	Result ResultCode::LoadedMeshHasNoData::get()
	{
		return Result( D3DXERR_LOADEDMESHASNODATA );
	}
	
	Result ResultCode::MoreData::get()
	{
		return Result( D3DERR_MOREDATA );
	}

	Result ResultCode::NoAutomaticGeneration::get()
	{
		return Result( D3DOK_NOAUTOGEN );
	}
	
	Result ResultCode::NotAvailable::get()
	{
		return Result( D3DERR_NOTAVAILABLE );
	}
	
	Result ResultCode::NotFound::get()
	{
		return Result( D3DERR_NOTFOUND );
	}
	
	Result ResultCode::NotResident::get()
	{
		return Result( S_NOT_RESIDENT );
	}
	
	Result ResultCode::OutOfVideoMemory::get()
	{
		return Result( D3DERR_OUTOFVIDEOMEMORY );
	}

	Result ResultCode::PresentModeChanged::get()
	{
		return Result( S_PRESENT_MODE_CHANGED );
	}

	Result ResultCode::PresentOccluded::get()
	{
		return Result( S_PRESENT_OCCLUDED );
	}

	Result ResultCode::ResidentInSharedMemory::get()
	{
		return Result( S_RESIDENT_IN_SHARED_MEMORY );
	}
	
	Result ResultCode::SkinningNotSupported::get()
	{
		return Result( D3DXERR_SKINNINGNOTSUPPORTED );
	}

	Result ResultCode::Success::get()
	{
		return Result( D3D_OK );
	}

	Result ResultCode::TooManyInfluences::get()
	{
		return Result( D3DXERR_TOOMANYINFLUENCES );
	}
	
	Result ResultCode::TooManyOperations::get()
	{
		return Result( D3DERR_TOOMANYOPERATIONS );
	}
	
	Result ResultCode::UnsupportedAlphaArgument::get()
	{
		return Result( D3DERR_UNSUPPORTEDALPHAARG );
	}
	
	Result ResultCode::UnsupportedAlphaOperation::get()
	{
		return Result( D3DERR_UNSUPPORTEDALPHAOPERATION );
	}
	
	Result ResultCode::UnsupportedColorArgument::get()
	{
		return Result( D3DERR_UNSUPPORTEDCOLORARG );
	}
	
	Result ResultCode::UnsupportedColorOperation::get()
	{
		return Result( D3DERR_UNSUPPORTEDCOLOROPERATION );
	}
	
	Result ResultCode::UnsupportedFactorValue::get()
	{
		return Result( D3DERR_UNSUPPORTEDFACTORVALUE );
	}
	
	Result ResultCode::UnsupportedTextureFilter::get()
	{
		return Result( D3DERR_UNSUPPORTEDTEXTUREFILTER );
	}

	Result ResultCode::WasStillDrawing::get()
	{
		return Result( D3DERR_WASSTILLDRAWING );
	}
	
	Result ResultCode::WrongTextureFormat::get()
	{
		return Result( D3DERR_WRONGTEXTUREFORMAT );
	}
}
}