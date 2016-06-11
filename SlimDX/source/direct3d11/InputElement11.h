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

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>Describes a single element for the input-assembler stage.</summary>
		/// <unmanaged>D3D11_INPUT_ELEMENT_DESC</unmanaged>
		public value class InputElement : System::IEquatable<InputElement>
		{
		private:
			System::String^ m_SemanticName;
			int m_SemanticIndex;
			DXGI::Format m_Format;
			int m_InputSlot;
			int m_AlignedByteOffset;
			InputClassification m_InputSlotClass;
			int m_InstanceDataStepRate;
	
		internal:
			D3D11_INPUT_ELEMENT_DESC CreateNativeVersion();
			
		public:
			/// <summary>
			/// The HLSL semantic associated with this element in a shader input-signature.
			/// </summary>
			property System::String^ SemanticName
			{
				System::String^ get();
				void set( System::String^ value );
			}

			/// <summary>
			/// The semantic index for the element. A semantic index modifies a semantic, with an integer index number. A semantic index is only needed in a 
			/// case where there is more than one element with the same semantic. For example, a 4x4 matrix would have four components each with the semantic
			/// name matrix, however each of the four component would have different semantic indices (0, 1, 2, and 3).
			/// </summary>
			property int SemanticIndex
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// The data type of the element data.
			/// </summary>
			property DXGI::Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}

			/// <summary>
			/// An integer value that identifies the input-assembler. Valid values are between 0 and 15.
			/// </summary>
			property int Slot
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Offset (in bytes) between each element. Use AppendAligned for convenience to define the current element directly after the previous one, including any packing if necessary. 
			/// </summary>
			property int AlignedByteOffset
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Identifies the input data class for a single input slot.
			/// </summary>
			property InputClassification Classification
			{
				InputClassification get();
				void set( InputClassification value );
			}

			/// <summary>
			/// The number of instances to draw using the same per-instance data before advancing in the buffer by one element. 
			/// This value must be 0 for an element that contains per-vertex data.
			/// </summary>
			property int InstanceDataStepRate
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Returns a value that can be used for the offset parameter of an InputElement to indicate that the element
			/// should be aligned directly after the previous element, including any packing if neccessary.
			/// </summary>
			/// <returns>A value used to align input elements.</returns>
			/// <unmanaged>D3D11_APPEND_ALIGNED_ELEMENT</unmanaged>
			property static int AppendAligned
			{
				int get();
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="InputElement"/> struct.
			/// </summary>
			/// <param name="name">The HLSL semantic associated with this element in a shader input-signature.</param>
			/// <param name="index">The semantic index for the element. A semantic index modifies a semantic, with an integer index number. A semantic index is only needed in a case where there is more than one element with the same semantic. For example, a 4x4 matrix would have four components each with the semantic name matrix, however each of the four component would have different semantic indices (0, 1, 2, and 3).</param>
			/// <param name="format">The data type of the element data.</param>
			/// <param name="slot">An integer value that identifies the input-assembler. Valid values are between 0 and 15.</param>
			InputElement( System::String^ name, int index, DXGI::Format format, int slot );

			/// <summary>
			/// Initializes a new instance of the <see cref="InputElement"/> struct.
			/// </summary>
			/// <param name="name">The HLSL semantic associated with this element in a shader input-signature.</param>
			/// <param name="index">The semantic index for the element. A semantic index modifies a semantic, with an integer index number. A semantic index is only needed in a case where there is more than one element with the same semantic. For example, a 4x4 matrix would have four components each with the semantic name matrix, however each of the four component would have different semantic indices (0, 1, 2, and 3).</param>
			/// <param name="format">The data type of the element data.</param>
			/// <param name="offset">Offset (in bytes) between each element. Use AppendAligned for convenience to define the current element directly after the previous one, including any packing if necessary.</param>
			/// <param name="slot">An integer value that identifies the input-assembler. Valid values are between 0 and 15.</param>
			InputElement( System::String^ name, int index, DXGI::Format format, int offset, int slot );

			/// <summary>
			/// Initializes a new instance of the <see cref="InputElement"/> struct.
			/// </summary>
			/// <param name="name">The HLSL semantic associated with this element in a shader input-signature.</param>
			/// <param name="index">The semantic index for the element. A semantic index modifies a semantic, with an integer index number. A semantic index is only needed in a case where there is more than one element with the same semantic. For example, a 4x4 matrix would have four components each with the semantic name matrix, however each of the four component would have different semantic indices (0, 1, 2, and 3).</param>
			/// <param name="format">The data type of the element data.</param>
			/// <param name="offset">Offset (in bytes) between each element. Use AppendAligned for convenience to define the current element directly after the previous one, including any packing if necessary.</param>
			/// <param name="slot">An integer value that identifies the input-assembler. Valid values are between 0 and 15.</param>
			/// <param name="slotClass">Identifies the input data class for a single input slot.</param>
			/// <param name="stepRate">The number of instances to draw using the same per-instance data before advancing in the buffer by one element. This value must be 0 for an element that contains per-vertex data.</param>
			InputElement( System::String^ name, int index, DXGI::Format format, int offset, int slot, InputClassification slotClass, int stepRate );

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( InputElement left, InputElement right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( InputElement left, InputElement right );

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
			virtual bool Equals( InputElement other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( InputElement% value1, InputElement% value2 );
		};
	}
};