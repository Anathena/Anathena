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
		/// <summary>
		/// Provides a description for effect variables.
		/// </summary>
		/// <unmanaged>D3DX11_EFFECT_VARIABLE_DESC</unmanaged>
		public value class EffectVariableDescription : System::IEquatable<EffectVariableDescription>
		{
		private:
			System::String^ m_Name;
			System::String^ m_Semantic;
			EffectVariableFlags m_Flags;
			int m_Annotations;
			int m_BufferOffset;
			int m_ExplicitBindPoint;

		internal:
			EffectVariableDescription( const D3DX11_EFFECT_VARIABLE_DESC& native );
			
		public:
			/// <summary>
			/// Name of this variable, annotation, or structure member.
			/// </summary>
			property System::String^ Name
			{
				System::String^ get();
			}
			
			/// <summary>
			/// Semantic string of this variable or structure member.
			/// </summary>
			property System::String^ Semantic
			{
				System::String^ get();
			}
			
			/// <summary>
			/// Optional flags for effect variables.
			/// </summary>
			property EffectVariableFlags Flags
			{
				EffectVariableFlags get();
			}
			
			/// <summary>
			/// Number of annotations on this variable.
			/// </summary>
			property int AnnotationCount
			{
				int get();
			}
			
			/// <summary>
			/// Offset into containing cbuffer or tbuffer (always 0 for annotations or variables not in constant buffers).
			/// </summary>
			property int ConstantBufferOffset
			{
				int get();
			}
			
			/// <summary>
			/// Used if the variable has been explicitly bound using the register keyword.
			/// </summary>
			property int ExplicitBindPoint
			{
				int get();
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( EffectVariableDescription left, EffectVariableDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( EffectVariableDescription left, EffectVariableDescription right );

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
			virtual bool Equals( EffectVariableDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( EffectVariableDescription% value1, EffectVariableDescription% value2 );
		};
	}
};