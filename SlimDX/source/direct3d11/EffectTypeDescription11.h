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

#include "Enums11.h"
#include "../d3dcompiler/EnumsDC.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Provides a description for effect types.
		/// </summary>
		/// <unmanaged>D3DX11_EFFECT_TYPE_DESC</unmanaged>
		public value class EffectTypeDescription : System::IEquatable<EffectTypeDescription>
		{
		private:
			System::String^ m_TypeName;
			D3DCompiler::ShaderVariableClass m_Class;
			D3DCompiler::ShaderVariableType m_Type;
			int m_Elements;
			int m_Members;
			int m_Rows;
			int m_Columns;
			int m_PackedSize;
			int m_UnpackedSize;
			int m_Stride;

		internal:
			EffectTypeDescription( const D3DX11_EFFECT_TYPE_DESC& native );
			
		public:
			/// <summary>
			/// Name of the type, for example "float4" or "MyStruct".
			/// </summary>
			property System::String^ TypeName
			{
				System::String^ get();
			}
			
			/// <summary>
			/// The variable class.
			/// </summary>
			property D3DCompiler::ShaderVariableClass Class
			{
				D3DCompiler::ShaderVariableClass get();
			}
			
			/// <summary>
			/// The variable type.
			/// </summary>
			property D3DCompiler::ShaderVariableType Type
			{
				D3DCompiler::ShaderVariableType get();
			}
			
			/// <summary>
			/// Number of elements in this type (0 if not an array).
			/// </summary>
			property int Elements
			{
				int get();
			}
			
			/// <summary>
			/// Number of members (0 if not a structure).
			/// </summary>
			property int Members
			{
				int get();
			}
			
			/// <summary>
			/// Number of rows in this type (0 if not a numeric primitive).
			/// </summary>
			property int Rows
			{
				int get();
			}
			
			/// <summary>
			/// Number of columns in this type (0 if not a numeric primitive).
			/// </summary>
			property int Columns
			{
				int get();
			}
			
			/// <summary>
			/// Number of bytes required to represent this data type, when tightly packed.
			/// </summary>
			property int PackedSize
			{
				int get();
			}
			
			/// <summary>
			/// Number of bytes occupied by this data type, when laid out in a constant buffer.
			/// </summary>
			property int UnpackedSize
			{
				int get();
			}
			
			/// <summary>
			/// Number of bytes to seek between elements, when laid out in a constant buffer.
			/// </summary>
			property int Stride
			{
				int get();
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( EffectTypeDescription left, EffectTypeDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( EffectTypeDescription left, EffectTypeDescription right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to a specified object. 
			/// </summary>
			/// <param name="obj">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to the specified object. 
			/// </summary>
			/// <param name="other">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( EffectTypeDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( EffectTypeDescription% value1, EffectTypeDescription% value2 );
		};
	}
};