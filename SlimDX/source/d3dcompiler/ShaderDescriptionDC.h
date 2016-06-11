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
		/// Provides a description for a shader.
		/// </summary>
		/// <unmanaged>D3D11_SHADER_DESC</unmanaged>
		public ref class ShaderDescription
		{
		internal:
			ShaderDescription( const D3D11_SHADER_DESC &desc );

		public:
			/// <summary>
			/// Shader version type.
			/// </summary>
			property ShaderVersion Version;

			/// <summary>
			/// The name of the originator of the shader.
			/// </summary>
			property System::String^ Creator;

			/// <summary>
			/// Shader compilation/parse flags.
			/// </summary>
			property ShaderFlags Flags;

			/// <summary>
			/// The number of shader-constant buffers.
			/// </summary>
			property int ConstantBuffers;

			/// <summary>
			/// The number of resource (textures and buffers) bound to a shader.
			/// </summary>
			property int BoundResources;

			/// <summary>
			/// The number of parameters in the input signature.
			/// </summary>
			property int InputParameters;

			/// <summary>
			/// The number of parameters in the output signature.
			/// </summary>
			property int OutputParameters;

			/// <summary>
			/// The number of intermediate-language instructions in the compiled shader.
			/// </summary>
			property int InstructionCount;

			/// <summary>
			/// The number of temporary registers in the compiled shader.
			/// </summary>
			property int TempRegisterCount;

			/// <summary>
			/// Number of temporary arrays used.
			/// </summary>
			property int TempArrayCount;

			/// <summary>
			/// Number of constant defines.
			/// </summary>
			property int DefineCount;

			/// <summary>
			/// Number of declarations (input + output).
			/// </summary>
			property int DeclarationCount;

			/// <summary>
			/// Number of non-categorized texture instructions.
			/// </summary>
			property int TextureNormalInstructions;

			/// <summary>
			/// Number of texture load instructions.
			/// </summary>
			property int TextureLoadInstructions;

			/// <summary>
			/// Number of texture comparison instructions.
			/// </summary>
			property int TextureCompareInstructions;

			/// <summary>
			/// Number of texture bias instructions.
			/// </summary>
			property int TextureBiasInstructions;

			/// <summary>
			/// Number of texture gradient instructions.
			/// </summary>
			property int TextureGradientInstructions;

			/// <summary>
			/// Number of floating point arithmetic instructions used.
			/// </summary>
			property int FloatInstructionCount;

			/// <summary>
			/// Number of signed integer arithmetic instructions used.
			/// </summary>
			property int IntInstructionCount;

			/// <summary>
			/// Number of unsigned integer arithmetic instructions used.
			/// </summary>
			property int UintInstructionCount;

			/// <summary>
			/// Number of static flow control instructions used.
			/// </summary>
			property int StaticFlowControlCount;
			
			/// <summary>
			/// Number of dynamic flow control instructions used.
			/// </summary>
			property int DynamicFlowControlCount;

			/// <summary>
			/// Number of macro instructions used.
			/// </summary>
			property int MacroInstructionCount;

			/// <summary>
			/// Number of array instructions used.
			/// </summary>
			property int ArrayInstructionCount;

			/// <summary>
			/// Number of cut instructions used.
			/// </summary>
			property int CutInstructionCount;

			/// <summary>
			/// Number of emit instructions used.
			/// </summary>
			property int EmitInstructionCount;

			/// <summary>
			/// The output topology of the geometry shader.
			/// </summary>
			property Direct3D11::PrimitiveTopology GeometryShaderOutputTopology;

			/// <summary>
			/// Geometry shader maximum output vertex count.
			/// </summary>
			property int MaximumOutputVertexCount;

			/// <summary>
			/// The geometry shader/hull shader input primitive.
			/// </summary>
			property InputPrimitive InputPrimitive;

			/// <summary>
			/// Number of parameters in the patch-constant signature.
			/// </summary>
			property int PatchConstantParameters;

			/// <summary>
			/// Number of geometry shader instances.
			/// </summary>
			property int GeometryShaderInstanceCount;

			/// <summary>
			/// Number of control points in the hull shader and domain shader.
			/// </summary>
			property int ControlPoints;

			/// <summary>
			/// The tessellator output-primitive type.
			/// </summary>
			property TessellatorOutputPrimitive HullShaderOutputPrimitive;

			/// <summary>
			/// The tessellator partitioning mode.
			/// </summary>
			property TessellatorPartitioning HullShaderPartitioning;

			/// <summary>
			/// The tessellator domain.
			/// </summary>
			property TessellatorDomain TessellatorDomain;

			/// <summary>
			/// Number of barrier instructions in a compute shader.
			/// </summary>
			property int BarrierInstructions;

			/// <summary>
			/// Number of interlocked instructions in a compute shader.
			/// </summary>
			property int InterlockedInstructions;

			/// <summary>
			/// Number of texture writes in a compute shader.
			/// </summary>
			property int TextureStoreInstructions;
		};
	}
}