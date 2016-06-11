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

#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		value class EffectTypeDescription;

		/// <summary>
		/// Represents the type of an effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectType</unmanaged>
		public ref class EffectType
		{
		private:
			ID3DX11EffectType* m_Pointer;
			
		internal:
			EffectType( ID3DX11EffectType* pointer );

		public:
			/// <summary>
			/// Gets the effect type's description.
			/// </summary>
			property EffectTypeDescription Description
			{
				EffectTypeDescription get();
			}
			
			/// <summary>
			/// Indicates whether the type represents a valid interface.
			/// </summary>
			property bool IsValid
			{
				bool get();
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectType"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectType( System::IntPtr pointer );

			/// <summary>
			/// Get the name of a type member.
			/// </summary>
			/// <param name="index">The zero-based index of the structure member type to retrieve.</param>
			/// <returns>The name of the specified member.</returns>
			System::String^ GetMemberName( int index );

			/// <summary>
			/// Get the semantic of a type member.
			/// </summary>
			/// <param name="index">The zero-based index of the structure member type to retrieve.</param>
			/// <returns>The semantic of the specified member.</returns>
			System::String^ GetMemberSemantic( int index );
			
			/// <summary>
			/// Get a structure member type by index.
			/// </summary>
			/// <param name="index">The zero-based index of the structure member type to retrieve.</param>
			/// <returns>The structure member type at the specified index.</returns>
			EffectType^ GetMemberTypeByIndex( int index );

			/// <summary>
			/// Get a structure member type by name.
			/// </summary>
			/// <param name="name">The name of the structure member type to retrieve.</param>
			/// <returns>The structure member type with the given name.</returns>
			EffectType^ GetMemberTypeByName( System::String^ name );

			/// <summary>
			/// Get a structure member type by semantic.
			/// </summary>
			/// <param name="name">The semantic of the structure member type to retrieve.</param>
			/// <returns>The structure member type with the given semantic.</returns>
			EffectType^ GetMemberTypeBySemantic( System::String^ name );
		};
	}
};