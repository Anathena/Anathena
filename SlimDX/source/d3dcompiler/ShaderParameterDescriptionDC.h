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

#include "EnumsDC.h"

namespace SlimDX
{
	namespace D3DCompiler
	{
		/// <summary>
		/// Describes a parameter in a shader signature.
		/// </summary>
		/// <unmanaged>D3D11_SIGNATURE_PARAMETER_DESC</unmanaged>
		public value class ShaderParameterDescription : System::IEquatable<ShaderParameterDescription>
		{
		private:
			System::String^ m_SemanticName;
			System::UInt32 m_SemanticIndex;
			System::UInt32 m_Register;
			SystemValueType m_SystemValueType;
			RegisterComponentType m_ComponentType;
			RegisterComponentMaskFlags m_Mask;
			RegisterComponentMaskFlags m_ReadWriteMask;
			int m_Stream;
		
		internal:
			ShaderParameterDescription( const D3D11_SIGNATURE_PARAMETER_DESC& native );
		
		public:
			/// <summary>
			/// A per-parameter string that identifies how the data will be used.
			/// </summary>
			property System::String^ SemanticName
			{
				System::String^ get();
			}
			
			/// <summary>
			/// Semantic index that modifies the semantic. Used to differentiate different parameters that use the same semantic.
			/// </summary>
			property System::UInt32 SemanticIndex
			{
				System::UInt32 get();
			}
			
			/// <summary>
			/// The register that will contain this variable's data.
			/// </summary>
			property System::UInt32 Register
			{
				System::UInt32 get();
			}
			
			/// <summary>
			/// A predefined string that determines the functionality of certain pipeline stages.
			/// </summary>
			property SystemValueType SystemType
			{
				SystemValueType get();
			}
			
			/// <summary>
			/// The per-component-data type that is stored in a register. Each register can store up to four-components of data. 
			/// </summary>
			property RegisterComponentType ComponentType
			{
				RegisterComponentType get();
			}
			
			/// <summary>
			/// Mask which indicates which components of a register are used.
			/// </summary>
			property RegisterComponentMaskFlags UsageMask
			{
				RegisterComponentMaskFlags get();
			}
			
			/// <summary>
			/// Mask which indicates whether a given component is never written (if the signature is an output signature) or always read (if the signature is an input signature).
			/// </summary>
			property RegisterComponentMaskFlags ReadWriteMask
			{
				RegisterComponentMaskFlags get();
			}

			/// <summary>
			/// Indicates which stream the geometry shader is using for the signature parameter.
			/// </summary>
			property int Stream
			{
				int get();
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( ShaderParameterDescription left, ShaderParameterDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( ShaderParameterDescription left, ShaderParameterDescription right );

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
			virtual bool Equals( ShaderParameterDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( ShaderParameterDescription% value1, ShaderParameterDescription% value2 );
		};
	}
};