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
	namespace D3DCompiler
	{
		value class ShaderTypeDescription;

		/// <summary>
		/// Defines an interface that provides access to types of shader variables.
		/// </summary>
		/// <unmanaged>ID3D11ShaderReflectionType</unmanaged>
		public ref class ShaderReflectionType
		{
		private:
			ID3D11ShaderReflectionType* m_Pointer;
			ShaderReflectionType^ baseClass;
			ShaderReflectionType^ subType;

			void Init();
			
		internal:
			ShaderReflectionType( ID3D11ShaderReflectionType* pointer );

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="ShaderReflectionType"/> class.
			/// </summary>
			//// <param name="pointer">The unmanaged ID3D11ShaderReflectionType pointer.</param>
			ShaderReflectionType( System::IntPtr pointer );

			/// <summary>
			/// Gets the variable's description.
			/// </summary>
			property ShaderTypeDescription Description
			{
				ShaderTypeDescription get();
			}

			/// <summary>
			/// Gets the type of this variable's base class.
			/// </summary>
			property ShaderReflectionType^ BaseClass
			{
				ShaderReflectionType^ get() { return baseClass; }
			}
			
			/// <summary>
			/// Gets the variable's sub type.
			/// </summary>
			property ShaderReflectionType^ Subtype
			{
				ShaderReflectionType^ get() { return subType; }
			}

			/// <summary>
			/// Gets an interface by index.
			/// </summary>
			/// <param name="index">The zero-based index of the interface to retrieve.</param>
			/// <returns>The interface at the given index.</returns>
			ShaderReflectionType^ GetInterface( int index );

			/// <summary>
			/// Gets a member variable type by index.
			/// </summary>
			/// <param name="index">The zero-based index of the member variable to retrieve.</param>
			/// <returns>The member variable type at the given index.</returns>
			ShaderReflectionType^ GetMemberType( int index );

			/// <summary>
			/// Gets a member variable type by name.
			/// </summary>
			/// <param name="name">The name of the member variable to retrieve.</param>
			/// <returns>The member variable type with the given name.</returns>
			ShaderReflectionType^ GetMemberType( System::String^ name );

			/// <summary>
			/// Gets the name of a member variable type.
			/// </summary>
			/// <param name="index">The zero-based index of the member variable to retrieve.</param>
			/// <returns>The name of the given member variable type.</returns>
			System::String^ GetMemberTypeName( int index );

			/// <summary>
			/// Determines whether the class type implements the given interface.
			/// </summary>
			/// <param name="base">The potential parent type.</param>
			/// <returns><c>true</c> if the class type implements the given interface; otherwise, <c>false</c>.</returns>
			bool ImplementsInterface( ShaderReflectionType^ base );

			/// <summary>
			/// Determines whether the variable type is equivalent to or inherits from the given type.
			/// </summary>
			/// <param name="type">The potential parent type.</param>
			/// <returns><c>true</c> if the class type is equivalent to or inherits from the given type; otherwise, <c>false</c>.</returns>
			bool IsOfType( ShaderReflectionType^ type );

			/// <summary>
			/// Determines whether the underlying variable type is equivalent to that of the specified type.
			/// </summary>
			/// <param name="type">The type to test for equivalence.</param>
			/// <returns><c>true</c> if the underlying class type is equivalent to that of the specified type; otherwise, <c>false</c>.</returns>
			bool IsTypeEqual( ShaderReflectionType^ type );
		};
	}
}