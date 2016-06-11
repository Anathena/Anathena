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

namespace SlimDX
{
	namespace Direct2D
	{
		/// <summary>Defines result codes that are returned by Direct2D functions.</summary>
		/// <unmanaged href="ms679692">HRESULT</unmanaged>
		public ref class ResultCode sealed
		{
		private:
			ResultCode();
			
		public:
			/// <summary>
			/// Invalid number.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_BAD_NUMBER</unmanaged>
			property static Result BadNumber
			{
				Result get();
			};

			/// <summary>
			/// The display format needed to render is not supported by the hardware device.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_DISPLAY_FORMAT_NOT_SUPPORTED</unmanaged>
			property static Result DisplayFormatNotSupported
			{
				Result get();
			};

			/// <summary>
			/// A valid display state could not be determined.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_DISPLAY_STATE_INVALID</unmanaged>
			property static Result DisplayStateInvalid
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
			/// The brush types are incompatible for the call.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_INCOMPATIBLE_BRUSH_TYPES</unmanaged>
			property static Result IncompatibleBrushTypes
			{
				Result get();
			};

			/// <summary>
			/// The supplied buffer was too small to accomodate the data.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_INSUFFICIENT_BUFFER</unmanaged>
			property static Result InsufficientBuffer
			{
				Result get();
			};

			/// <summary>
			/// The object has not yet been initialized.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_NOT_INITIALIZED</unmanaged>
			property static Result NotInitialized
			{
				Result get();
			};

			/// <summary>
			/// An internal error (D2D bug) occurred. On checked builds, we would assert.
			/// The application should close this instance of D2D and should consider restarting its process.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_INTERNAL_ERROR</unmanaged>
			property static Result InternalError
			{
				Result get();
			};

			/// <summary>
			/// A call to this method is invalid.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_INVALID_CALL</unmanaged>
			property static Result InvalidCall
			{
				Result get();
			};

			/// <summary>
			/// A layer resource was used more than once.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_LAYER_ALREADY_IN_USE</unmanaged>
			property static Result LayerAlreadyInUse
			{
				Result get();
			};

			/// <summary>
			/// Requested DX surface size exceeded maximum texture size.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_MAX_TEXTURE_SIZE_EXCEEDED</unmanaged>
			property static Result MaxTextureSizeExceeded
			{
				Result get();
			};

			/// <summary>
			/// No HW rendering device is available for this operation.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_NO_HARDWARE_DEVICE</unmanaged>
			property static Result NoHardwareDevice
			{
				Result get();
			};

			/// <summary>
			/// The pop call did not match the corresponding push call.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_POP_CALL_DID_NOT_MATCH_PUSH</unmanaged>
			property static Result PopCallDidNotMatchPush
			{
				Result get();
			};

			/// <summary>
			/// The push and pop calls were unbalanced.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_PUSH_POP_UNBALANCED</unmanaged>
			property static Result PushPopUnbalanced
			{
				Result get();
			};

			/// <summary>
			/// There has been a presentation error that may be recoverable. The caller
			/// needs to recreate, rerender the entire frame, and reattempt present.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_RECREATE_TARGET</unmanaged>
			property static Result RecreateTarget
			{
				Result get();
			};

			/// <summary>
			/// Attempted to copy from a render target while a layer or clip rect is applied.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_RENDER_TARGET_HAS_LAYER_OR_CLIPRECT</unmanaged>
			property static Result RenderTargetHasLayerOrClippingRectangle
			{
				Result get();
			};

			/// <summary>
			/// The geometry scanner failed to process the data.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_SCANNER_FAILED</unmanaged>
			property static Result ScannerFailed
			{
				Result get();
			};

			/// <summary>
			/// D2D could not access the screen.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_SCREEN_ACCESS_DENIED</unmanaged>
			property static Result ScreenAccessDenied
			{
				Result get();
			};

			/// <summary>
			/// Shader compilation failed.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_SHADER_COMPILE_FAILED</unmanaged>
			property static Result ShaderCompileFailed
			{
				Result get();
			};

			/// <summary>
			/// Represents the result of a successful operation.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="aa378137">E_FAIL</unmanaged>
			property static Result Success
			{
				Result get();
			};

			/// <summary>
			/// The render target is not compatible with GDI.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_TARGET_NOT_GDI_COMPATIBLE</unmanaged>
			property static Result TargetNotGdiCompatible
			{
				Result get();
			};

			/// <summary>
			/// A text client drawing effect object is of the wrong type.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_TEXT_EFFECT_IS_WRONG_TYPE</unmanaged>
			property static Result TextEffectIsWrongType
			{
				Result get();
			};

			/// <summary>
			/// The application is holding a reference to the IDWriteTextRenderer interface 
			/// after the corresponding DrawText or DrawTextLayout call has returned.
			/// The TextRenderer instance will be zombied.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_TEXT_RENDERER_NOT_RELEASED</unmanaged>
			property static Result TextRendererNotReleased
			{
				Result get();
			};

			/// <summary>
			/// Shader construction failed because it was too complex.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_TOO_MANY_SHADER_ELEMENTS</unmanaged>
			property static Result TooManyShaderElements
			{
				Result get();
			};

			/// <summary>
			/// The requested opertion is not supported.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_UNSUPPORTED_OPERATION</unmanaged>
			property static Result UnsupportedOperation
			{
				Result get();
			};

			/// <summary>
			/// The requested D2D version is not supported.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_UNSUPPORTED_VERSION</unmanaged>
			property static Result UnsupportedVersion
			{
				Result get();
			};

			/// <summary>
			/// An unknown Win32 failure occurred.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_WIN32_ERROR</unmanaged>
			property static Result Win32Error
			{
				Result get();
			};

			/// <summary>
			/// Objects used together must be created from the same factory instance.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_WRONG_FACTORY</unmanaged>
			property static Result WrongFactory
			{
				Result get();
			};

			/// <summary>
			/// The resource was realized on the wrong render target.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_WRONG_RESOURCE_DOMAIN</unmanaged>
			property static Result WrongResourceDomain
			{
				Result get();
			};

			/// <summary>
			/// The object was not in the correct state to process the method.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_WRONG_STATE</unmanaged>
			property static Result WrongState
			{
				Result get();
			};

			/// <summary>
			/// The supplied vector is vero.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>D2DERR_ZERO_VECTOR</unmanaged>
			property static Result ZeroVector
			{
				Result get();
			};
		};
	}
}