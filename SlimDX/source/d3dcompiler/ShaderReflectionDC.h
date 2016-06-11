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
#include "ShaderBytecodeDC.h"

#include "ConstantBufferDC.h"
#include "ShaderDescriptionDC.h"
#include "ShaderParameterDescriptionDC.h"
#include "InputBindingDescriptionDC.h"

namespace SlimDX
{
	namespace D3DCompiler
	{
		/// <summary>
		/// Defines an interface for reflecting on shader information and metadata.
		/// </summary>
		/// <unmanaged>ID3D11ShaderReflection</unmanaged>
		public ref class ShaderReflection : ComObject
		{
			COMOBJECT(ID3D11ShaderReflection, ShaderReflection);

		private:
			ShaderDescription^ description;
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="ShaderReflection"/> class.
			/// </summary>
			/// <param name="bytecode">The compiled shader or effect to reflect upon.</param>
			ShaderReflection( ShaderBytecode^ bytecode );

			/// <summary>
			/// Gets a constant buffer by index.
			/// </summary>
			/// <param name="index">The zero-based index of the constant buffer to retrieve.</param>
			/// <returns>The constant buffer at the given index.</returns>
			ConstantBuffer^ GetConstantBuffer( int index );

			/// <summary>
			/// Gets a constant buffer by name.
			/// </summary>
			/// <param name="name">The name of the constant buffer to retrieve.</param>
			/// <returns>The constant buffer with the given name.</returns>
			ConstantBuffer^ GetConstantBuffer( System::String^ name );

			/// <summary>
			/// Gets the description of an input parameter.
			/// </summary>
			/// <param name="index">The zero-based index of the input parameter whose description is to be retrieved.</param>
			/// <returns>The description of the input parameter at the given index.</returns>
			ShaderParameterDescription GetInputParameterDescription( int index );

			/// <summary>
			/// Gets the description of an output parameter.
			/// </summary>
			/// <param name="index">The zero-based index of the output parameter whose description is to be retrieved.</param>
			/// <returns>The description of the output parameter at the given index.</returns>
			ShaderParameterDescription GetOutputParameterDescription( int index );

			/// <summary>
			/// Gets the description of a patch-constant parameter.
			/// </summary>
			/// <param name="index">The zero-based index of the patch-constant parameter whose description is to be retrieved.</param>
			/// <returns>The description of the patch-constant parameter at the given index.</returns>
			ShaderParameterDescription GetPatchConstantParameterDescription( int index );

			/// <summary>
			/// Gets a description of the resources bound to a shader.
			/// </summary>
			/// <param name="index">Zero-based index of the bound resource.</param>
			/// <returns>A description of the resource bound at the given index.</returns>
			InputBindingDescription GetResourceBindingDescription( int index );

			/// <summary>
			/// Gets a description of the resources bound to a shader.
			/// </summary>
			/// <param name="name">The name of the bound resource.</param>
			/// <returns>A description of the resource bound with the given name.</returns>
			InputBindingDescription GetResourceBindingDescription( System::String^ name );

			/// <summary>
			/// Gets the minimum feature level required to use the shader.
			/// </summary>
			property Direct3D11::FeatureLevel MinimumFeatureLevel
			{
				Direct3D11::FeatureLevel get();
			}

			/// <summary>
			/// Gets the input primitive used by the geometry shader.
			/// </summary>
			property InputPrimitive GeometryShaderInputPrimitive
			{
				InputPrimitive get();
			}

			/// <summary>
			/// Gets the shader's description.
			/// </summary>
			property ShaderDescription^ Description
			{
				ShaderDescription^ get() { return description; }
			}
			
			/// <summary>
			/// Gets the number of bitwise instructions.
			/// </summary>
			property int BitwiseInstructionCount 
			{
				int get();
			}
			
			/// <summary>
			/// Gets the number of conversion instructions.
			/// </summary>
			property int ConversionInstructionCount
			{
				int get();
			}
			
			/// <summary>
			/// Gets the number of move instructions.
			/// </summary>
			property int MoveInstructionCount
			{
				int get();
			}
			
			/// <summary>
			/// Gets the number of conditional move instructions.
			/// </summary>
			property int ConditionalMoveInstructionCount
			{
				int get();
			}
			
			/// <summary>
			/// Gets the number of interface slots.
			/// </summary>
			property int InterfaceSlotCount
			{
				int get();
			}
			
			/// <summary>
			/// Gets a value indicating whether the shader is a sample frequency shader.
			/// </summary>
			property bool IsSampleFrequencyShader
			{
				bool get();
			}
		};
	}
}