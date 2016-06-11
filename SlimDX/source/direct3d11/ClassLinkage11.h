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

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class ClassInstance;
		ref class Device;

		/// <summary>
		/// Encapsulates HLSL dynamic class linkage.
		/// </summary>
		/// <unmanaged>ID3D11ClassLinkage</unmanaged>
		public ref class ClassLinkage : ComObject
		{
			COMOBJECT(ID3D11ClassLinkage, ClassLinkage);
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="ClassLinkage"/> class.
			/// </summary>
			/// <param name="device">The device used to create the class linkage object.</param>
			ClassLinkage( Device^ device );

			/// <summary>
			/// Gets the class-instance object that represents the specified HLSL class.
			/// </summary>
			/// <param name="name">The name of a class for which to get the class instance.</param>
			/// <param name="index">The index of the class instance.</param>
			/// <returns>The specified class instance.</returns>
			ClassInstance^ GetInstance( System::String^ name, int index );
		};
	}
}