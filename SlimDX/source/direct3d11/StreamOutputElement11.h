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
		/// <summary>Describes a single element in a vertex buffer in an output slot.</summary>
		/// <unmanaged>D3D11_SO_DECLARATION_ENTRY</unmanaged>
		public value class StreamOutputElement : System::IEquatable<StreamOutputElement>
		{	
		internal:
			D3D11_SO_DECLARATION_ENTRY CreateNativeVersion();
			
		public:
			/// <summary>
			/// Zero-based stream index.
			/// </summary>
			property int Stream;

			/// <summary>
			/// Type of output element; possible values include: "POSITION", "NORMAL", or "TEXCOORD0". Note that if SemanticName is <c>null</c> then
			/// ComponentCount can be greater than 4 and the described entry will be a gap in the stream out where no data will be written.
			/// </summary>
			property System::String^ SemanticName;

			/// <summary>
			/// Output element's zero-based index. Should be used if, for example, you have more than one texture coordinate stored in each vertex.
			/// </summary>
			property int SemanticIndex;

			/// <summary>
			/// Which component of the entry to begin writing out to. Valid values are 0 to 3. For example, if you only wish to output to the y and z components of a position, then StartComponent should be 1 and ComponentCount should be 2.
			/// </summary>
			property int StartComponent;

			/// <summary>
			/// The number of components of the entry to write out to. Valid values are 1 to 4. For example, if you only wish to output to the y and z components of a position,
			/// then StartComponent should be 1 and ComponentCount should be 2. Note that if SemanticName is <c>null</c> then ComponentCount can be greater than 4 and the described entry will be a gap in the stream out where no data will be written. 
			/// </summary>
			property int ComponentCount;

			/// <summary>
			/// The associated stream output buffer that is bound to the pipeline. Valid values range from 0 to 3.
			/// </summary>
			property int OutputSlot;

			/// <summary>
			/// Initializes a new instance of the <see cref="StreamOutputElement"/> struct.
			/// </summary>
			/// <param name="stream">Zero-based stream index.</param>
			/// <param name="name">Type of output element; possible values include: "POSITION", "NORMAL", or "TEXCOORD0". Note that if SemanticName is <c>null</c> then ComponentCount can be greater than 4 and the described entry will be a gap in the stream out where no data will be written.</param>
			/// <param name="index">Output element's zero-based index. Should be used if, for example, you have more than one texture coordinate stored in each vertex.</param>
			/// <param name="startComponent">Which component of the entry to begin writing out to. Valid values are 0 to 3. For example, if you only wish to output to the y and z components of a position, then StartComponent should be 1 and ComponentCount should be 2.</param>
			/// <param name="componentCount">The number of components of the entry to write out to. Valid values are 1 to 4. For example, if you only wish to output to the y and z components of a position, then StartComponent should be 1 and ComponentCount should be 2. Note that if SemanticName is <c>null</c> then ComponentCount can be greater than 4 and the described entry will be a gap in the stream out where no data will be written. </param>
			/// <param name="outputSlot">The associated stream output buffer that is bound to the pipeline. Valid values range from 0 to 3.</param>
			StreamOutputElement( int stream, System::String^ name, int index, int startComponent, int componentCount, int outputSlot );

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( StreamOutputElement left, StreamOutputElement right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( StreamOutputElement left, StreamOutputElement right );

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
			virtual bool Equals( StreamOutputElement other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( StreamOutputElement% value1, StreamOutputElement% value2 );
		};
	}
};