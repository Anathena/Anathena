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
	namespace DXGI
	{
		/// <summary>Defines result codes that are returned by DXGI functions.</summary>
		/// <unmanaged href="ms679692">HRESULT</unmanaged>
		public ref class ResultCode sealed
		{
		private:
			ResultCode();
			
		public:
			/// <summary>
			/// Returned when the window content is not visible.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="cc308061">DXGI_STATUS_OCCLUDED</unmanaged>
			property static Result Occluded
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged>DXGI_STATUS_CLIPPED</unmanaged>
			property static Result Clipped
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged>DXGI_STATUS_NO_REDIRECTION</unmanaged>
			property static Result NoRedirection
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged>DXGI_STATUS_NO_DESKTOP_ACCESS</unmanaged>
			property static Result NoDesktopAccess
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged>DXGI_STATUS_GRAPHICS_VIDPN_SOURCE_IN_USE</unmanaged>
			property static Result VideoPresentNetworkSourceInUse
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="cc308061">DXGI_STATUS_MODE_CHANGED</unmanaged>
			property static Result ModeChanged
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="cc308061">DXGI_STATUS_MODE_CHANGE_IN_PROGRESS</unmanaged>
			property static Result ModeChangeInProgress
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_INVALID_CALL</unmanaged>
			property static Result InvalidCall
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_NOT_FOUND</unmanaged>
			property static Result NotFound
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_MORE_DATA</unmanaged>
			property static Result MoreData
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_UNSUPPORTED</unmanaged>
			property static Result Unsupported
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_DEVICE_REMOVED</unmanaged>
			property static Result DeviceRemoved
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_DEVICE_HUNG</unmanaged>
			property static Result DeviceHung
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_DEVICE_RESET</unmanaged>
			property static Result DeviceReset
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_WAS_STILL_DRAWING</unmanaged>
			property static Result WasStillDrawing
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_DRIVER_INTERNAL_ERROR</unmanaged>
			property static Result DriverInternalError
			{
				Result get();
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb509553">DXGI_ERROR_NONEXCLUSIVE</unmanaged>
			property static Result Nonexclusive
			{
				Result get();
			}

			/// <summary>
			/// 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>DXGI_ERROR_NOT_CURRENTLY_AVAILABLE</unmanaged>
			property static Result NotCurrentlyAvailable
			{
				Result get();
			}

			/// <summary>
			/// Represents the result of a successful operation.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb509553">S_OK</unmanaged>
			property static Result Success
			{
				Result get();
			}

			/// <summary>
			/// Represents a generic failure result.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="aa378137">E_FAIL</unmanaged>
			property static Result Failure
			{
				Result get();
			}
		};
	}
}