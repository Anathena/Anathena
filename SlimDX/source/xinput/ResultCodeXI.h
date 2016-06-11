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
	namespace XInput
	{
		/// <summary>Defines result codes that are returned by XInput functions.</summary>
		/// <unmanaged href="ms679692">HRESULT</unmanaged>
		public ref class ResultCode sealed
		{
		private:
			ResultCode();
			
		public:
			/// <summary>
			/// Occurs when the desired controller is not connected to the system.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="ms681383">ERROR_DEVICE_NOT_CONNECTED</unmanaged>
			property static Result NotConnected
			{
				Result get();
			}
			
			/// <summary>
			/// Occurs when the result of an operation is an empty set.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="ms681387">ERROR_EMPTY</unmanaged>
			property static Result Empty
			{
				Result get();
			}

			/// <summary>
			/// Represents the result of a successful operation.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="ms681382">ERROR_SUCCESS</unmanaged>
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