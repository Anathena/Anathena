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
#include "stdafx.h"

#include <d2d1.h>
#include <d2d1helper.h>

#include "Direct2DException.h"
#include "ResultCodeD2D.h"

namespace SlimDX
{
namespace Direct2D
{
	ResultCode::ResultCode()
	{
	}

	Result ResultCode::BadNumber::get()
	{
		return Result( D2DERR_BAD_NUMBER );
	}

	Result ResultCode::DisplayFormatNotSupported::get()
	{
		return Result( D2DERR_DISPLAY_FORMAT_NOT_SUPPORTED );
	}

	Result ResultCode::DisplayStateInvalid::get()
	{
		return Result( D2DERR_DISPLAY_STATE_INVALID );
	}

	Result ResultCode::Failure::get()
	{
		return Result( E_FAIL );
	}

	Result ResultCode::IncompatibleBrushTypes::get()
	{
		return Result( D2DERR_INCOMPATIBLE_BRUSH_TYPES );
	}

	Result ResultCode::InsufficientBuffer::get()
	{
		return Result( D2DERR_INSUFFICIENT_BUFFER );
	}

	Result ResultCode::NotInitialized::get()
	{
		return Result( D2DERR_NOT_INITIALIZED );
	}

	Result ResultCode::InternalError::get()
	{
		return Result( D2DERR_INTERNAL_ERROR );
	}

	Result ResultCode::InvalidCall::get()
	{
		return Result( D2DERR_INVALID_CALL );
	}

	Result ResultCode::LayerAlreadyInUse::get()
	{
		return Result( D2DERR_LAYER_ALREADY_IN_USE );
	}

	Result ResultCode::MaxTextureSizeExceeded::get()
	{
		return Result( D2DERR_MAX_TEXTURE_SIZE_EXCEEDED );
	}

	Result ResultCode::NoHardwareDevice::get()
	{
		return Result( D2DERR_NO_HARDWARE_DEVICE );
	}

	Result ResultCode::PopCallDidNotMatchPush::get()
	{
		return Result( D2DERR_POP_CALL_DID_NOT_MATCH_PUSH );
	}

	Result ResultCode::PushPopUnbalanced::get()
	{
		return Result( D2DERR_PUSH_POP_UNBALANCED );
	}

	Result ResultCode::RecreateTarget::get()
	{
		return Result( D2DERR_RECREATE_TARGET );
	}

	Result ResultCode::RenderTargetHasLayerOrClippingRectangle::get()
	{
		return Result( D2DERR_RENDER_TARGET_HAS_LAYER_OR_CLIPRECT );
	}

	Result ResultCode::ScannerFailed::get()
	{
		return Result( D2DERR_SCANNER_FAILED );
	}

	Result ResultCode::ScreenAccessDenied::get()
	{
		return Result( D2DERR_SCREEN_ACCESS_DENIED );
	}

	Result ResultCode::ShaderCompileFailed::get()
	{
		return Result( D2DERR_SHADER_COMPILE_FAILED );
	}

	Result ResultCode::Success::get()
	{
		return Result( S_OK );
	}

	Result ResultCode::TargetNotGdiCompatible::get()
	{
		return Result( D2DERR_TARGET_NOT_GDI_COMPATIBLE );
	}

	Result ResultCode::TextEffectIsWrongType::get()
	{
		return Result( D2DERR_TEXT_EFFECT_IS_WRONG_TYPE );
	}

	Result ResultCode::TextRendererNotReleased::get()
	{
		return Result( D2DERR_TEXT_RENDERER_NOT_RELEASED );
	}

	Result ResultCode::TooManyShaderElements::get()
	{
		return Result( D2DERR_TOO_MANY_SHADER_ELEMENTS );
	}

	Result ResultCode::UnsupportedOperation::get()
	{
		return Result( D2DERR_UNSUPPORTED_OPERATION );
	}

	Result ResultCode::UnsupportedVersion::get()
	{
		return Result( D2DERR_UNSUPPORTED_VERSION );
	}

	Result ResultCode::Win32Error::get()
	{
		return Result( D2DERR_WIN32_ERROR );
	}

	Result ResultCode::WrongFactory::get()
	{
		return Result( D2DERR_WRONG_FACTORY );
	}

	Result ResultCode::WrongResourceDomain::get()
	{
		return Result( D2DERR_WRONG_RESOURCE_DOMAIN );
	}

	Result ResultCode::WrongState::get()
	{
		return Result( D2DERR_WRONG_STATE );
	}

	Result ResultCode::ZeroVector::get()
	{
		return Result( D2DERR_ZERO_VECTOR );
	}
}
}