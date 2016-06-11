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
		ref class ClassLinkage;
		value class ClassInstanceDescription;

		/// <summary>
		/// Represents an HLSL class.
		/// </summary>
		/// <unmanaged>ID3D11ClassInstance</unmanaged>
		public ref class ClassInstance : ComObject
		{
			COMOBJECT(ID3D11ClassInstance, ClassInstance);
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="ClassInstance"/> class.
			/// </summary>
			/// <param name="linkage">The linkage object used to create the class instance.</param>
			/// <param name="typeName">The type name of a class to initialize.</param>
			/// <param name="constantBufferOffset">Identifies the constant buffer that contains the class data.</param>
			/// <param name="constantVectorOffset">The four-component vector offset from the start of the constant buffer where the class data will begin. Consequently, this is not a byte offset.</param>
			/// <param name="textureOffset">The texture slot for the first texture; there may be multiple textures following the offset.</param>
			/// <param name="samplerOffset">The sampler slot for the first sampler; there may be multiple samplers following the offset.</param>
			ClassInstance( ClassLinkage^ linkage, System::String^ typeName, int constantBufferOffset, int constantVectorOffset, int textureOffset, int samplerOffset );

			/// <summary>
			/// Gets the linkage object associated with the class.
			/// </summary>
			property ClassLinkage^ Linkage
			{
				ClassLinkage^ get();
			}

			/// <summary>
			/// Gets the description of the class instance.
			/// </summary>
			property ClassInstanceDescription Description
			{
				ClassInstanceDescription get();
			}

			/// <summary>
			/// Gets the instance name of the class.
			/// </summary>
			property System::String^ InstanceName
			{
				System::String^ get();
			} 

			/// <summary>
			/// Gets the name of the class's type.
			/// </summary>
			property System::String^ TypeName
			{
				System::String^ get();
			}
		};
	}
}