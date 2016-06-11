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

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class EffectConstantBuffer;
		ref class EffectMatrixVariable;
		ref class EffectResourceVariable;
		ref class EffectShaderVariable;
		ref class EffectScalarVariable;
		ref class EffectVectorVariable;
		ref class EffectStringVariable;
		value class EffectVariableDescription;
		
		public ref class EffectVariable
		{
		private:
			ID3D10EffectVariable* m_Pointer;
			
		internal:
			EffectVariable( ID3D10EffectVariable* pointer );

		public:
			property EffectVariableDescription Description
			{
				EffectVariableDescription get();
			}
			
			property bool IsValid
			{
				bool get();
			}
			
			EffectVariable( System::IntPtr pointer );
			
			EffectVariable^ GetAnnotationByIndex( int index );
			EffectVariable^ GetAnnotationByName( System::String^ name );
			
			EffectVariable^ GetElement( int index );
			
			EffectVariable^ GetMemberByIndex( int index );
			EffectVariable^ GetMemberByName( System::String^ name );
			EffectVariable^ GetMemberBySemantic( System::String^ name );
			
			EffectConstantBuffer^ AsConstantBuffer();
			EffectMatrixVariable^ AsMatrix();
			EffectResourceVariable^ AsResource();
			EffectShaderVariable^ AsShader();
			EffectScalarVariable^ AsScalar();
			EffectVectorVariable^ AsVector();
			EffectStringVariable^ AsString();

			/// <summary>
			/// Sets the value of the variable using raw bytes.
			/// </summary>
			/// <param name="count">The number of bytes to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetRawValue(DataStream^ data, int count);

			/// <summary>
			/// Gets the value of the variable in raw bytes.
			/// </summary>
			/// <param name="count">The number of bytes to retrieve.</param>
			/// <returns>The raw data representing the value of the variable.</returns>
			DataStream^ GetRawValue(int count);
		};
	}
};