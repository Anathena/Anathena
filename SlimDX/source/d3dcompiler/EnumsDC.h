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

#include "../direct3d11/Enums11.h"

namespace SlimDX
{
	namespace D3DCompiler
	{
		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.

		/// <summary>
		/// Indicates a constant buffer's type.
		/// </summary>
		/// <unmanaged>D3D11_CBUFFER_TYPE</unmanaged>
		public enum class ConstantBufferType : System::Int32
		{
			/// <summary>
			/// A buffer containing scalar constants.
			/// </summary>
			ConstantBuffer = D3D11_CT_CBUFFER,

			/// <summary>
			/// A buffer containing texture data.
			/// </summary>
			TextureBuffer = D3D11_CT_TBUFFER,

			/// <summary>
			/// A buffer containing interface pointers.
			/// </summary>
			InterfacePointers = D3D11_CT_INTERFACE_POINTERS,

			/// <summary>
			/// A buffer containing binding information.
			/// </summary>
			ResourceBinding = D3D11_CT_RESOURCE_BIND_INFO
		};

		/// <summary>
		/// Specifies flags that affect the output of shader disassembly.
		/// </summary>
		[System::Flags]
		public enum class DisassemblyFlags : System::Int32
		{
			/// <summary>
			/// No specific flags specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Enable the output of color codes.
			/// </summary>
			EnableColorCodes = D3D_DISASM_ENABLE_COLOR_CODE,

			/// <summary>
			/// Enable the output of default values.
			/// </summary>
			EnableDefaultValues = D3D_DISASM_ENABLE_DEFAULT_VALUE_PRINTS,

			/// <summary>
			/// Enable instruction numbering.
			/// </summary>
			EnableInstructionNumbering = D3D_DISASM_ENABLE_INSTRUCTION_NUMBERING
		};

		/// <summary>Specifies compile- or run-time options during effect creation.</summary>
		/// <unmanaged>D3D10_EFFECT</unmanaged>
		[System::Flags]
		public enum class EffectFlags : System::Int32
		{
			/// <summary>
			/// Standard compile- and run-time behavior.
			/// </summary>
			None = 0,
			
			/// <summary>
			/// Compile the .fx file to a child effect. Child effects do not initialize shared values
			/// (as those values are instead initialized by the effect pool).
			/// </summary>
			ChildEffect = D3D10_EFFECT_COMPILE_CHILD_EFFECT,
			
			/// <summary>
			/// Allow mutable state objects (non-literal expressions may appear in state object definitions). This
			/// has a negative impact on run-time performance.
			/// </summary>
			AllowSlowOperations = D3D10_EFFECT_COMPILE_ALLOW_SLOW_OPS,
			
			/// <summary>
			/// Do not attempt to synchronize with other threads loading effects into the same pool.
			/// </summary>
			SingleThreaded = D3D10_EFFECT_SINGLE_THREADED,
		};

		/// <summary>
		/// Flags that indicate the location of an include file.
		/// </summary>
		/// <unmanaged>D3D10_INCLUDE_TYPE</unmanaged>
		[System::Flags]
		public enum class IncludeType : System::Int32
		{
			/// <summary>
			/// Indicates that the include is in the local path (or paths).
			/// </summary>
			Local = D3D10_INCLUDE_LOCAL,
			
			/// <summary>
			/// Indicates that the include is in the system path (or paths).
			/// </summary>
			System = D3D10_INCLUDE_SYSTEM
		};

		/// <summary>
		/// Indicates how the pipeline interprets geometry or hull shader input primitives.
		/// </summary>
		/// <unmanaged>D3D11_PRIMITIVE</unmanaged>
		public enum class InputPrimitive : System::Int32
		{
			/// <summary>
			/// The shader has not been initialized with an input primitive type.
			/// </summary>
			Undefined = D3D11_PRIMITIVE_UNDEFINED,

			/// <summary>
			/// Interpret the input primitive as a point.
			/// </summary>
			Point = D3D11_PRIMITIVE_POINT,

			/// <summary>
			/// Interpret the input primitive as a line.
			/// </summary>
			Line = D3D11_PRIMITIVE_LINE,

			/// <summary>
			/// Interpret the input primitive as a triangle.
			/// </summary>
			Triangle = D3D11_PRIMITIVE_TRIANGLE,

			/// <summary>
			/// Interpret the input primitive as a line with adjacency data.
			/// </summary>
			LineWithAdjacency = D3D11_PRIMITIVE_LINE_ADJ,

			/// <summary>
			/// Interpret the input primitive as a triangle with adjacency data.
			/// </summary>
			TriangleWithAdjacency = D3D11_PRIMITIVE_TRIANGLE_ADJ,

			/// <summary>
			/// Interpret the input primitive as a control point patch with one control point.
			/// </summary>
			PatchWith1ControlPoint = D3D11_PRIMITIVE_1_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with two control points.
			/// </summary>
			PatchWith2ControlPoints = D3D11_PRIMITIVE_2_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with three control points.
			/// </summary>
			PatchWith3ControlPoints = D3D11_PRIMITIVE_3_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with four control points.
			/// </summary>
			PatchWith4ControlPoints = D3D11_PRIMITIVE_4_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with five control points.
			/// </summary>
			PatchWith5ControlPoints = D3D11_PRIMITIVE_5_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with six control points.
			/// </summary>
			PatchWith6ControlPoints = D3D11_PRIMITIVE_6_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with seven control points.
			/// </summary>
			PatchWith7ControlPoints = D3D11_PRIMITIVE_7_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with eight control points.
			/// </summary>
			PatchWith8ControlPoints = D3D11_PRIMITIVE_8_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with nine control points.
			/// </summary>
			PatchWith9ControlPoints = D3D11_PRIMITIVE_9_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with ten control points.
			/// </summary>
			PatchWith10ControlPoints = D3D11_PRIMITIVE_10_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 11 control points.
			/// </summary>
			PatchWith11ControlPoints = D3D11_PRIMITIVE_11_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 12 control points.
			/// </summary>
			PatchWith12ControlPoints = D3D11_PRIMITIVE_12_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 13 control points.
			/// </summary>
			PatchWith13ControlPoints = D3D11_PRIMITIVE_13_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 14 control points.
			/// </summary>
			PatchWith14ControlPoints = D3D11_PRIMITIVE_14_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 15 control points.
			/// </summary>
			PatchWith15ControlPoints = D3D11_PRIMITIVE_15_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 16 control points.
			/// </summary>
			PatchWith16ControlPoints = D3D11_PRIMITIVE_16_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 17 control points.
			/// </summary>
			PatchWith17ControlPoints = D3D11_PRIMITIVE_17_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 18 control points.
			/// </summary>
			PatchWith18ControlPoints = D3D11_PRIMITIVE_18_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 19 control points.
			/// </summary>
			PatchWith19ControlPoints = D3D11_PRIMITIVE_19_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 20 control points.
			/// </summary>
			PatchWith20ControlPoints = D3D11_PRIMITIVE_20_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 21 control points.
			/// </summary>
			PatchWith21ControlPoints = D3D11_PRIMITIVE_21_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 22 control points.
			/// </summary>
			PatchWith22ControlPoints = D3D11_PRIMITIVE_22_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 23 control points.
			/// </summary>
			PatchWith23ControlPoints = D3D11_PRIMITIVE_23_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 24 control points.
			/// </summary>
			PatchWith24ControlPoints = D3D11_PRIMITIVE_24_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 25 control points.
			/// </summary>
			PatchWith25ControlPoints = D3D11_PRIMITIVE_25_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 26 control points.
			/// </summary>
			PatchWith26ControlPoints = D3D11_PRIMITIVE_26_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 27 control points.
			/// </summary>
			PatchWith27ControlPoints = D3D11_PRIMITIVE_27_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 28 control points.
			/// </summary>
			PatchWith28ControlPoints = D3D11_PRIMITIVE_28_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 29 control points.
			/// </summary>
			PatchWith29ControlPoints = D3D11_PRIMITIVE_29_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 30 control points.
			/// </summary>
			PatchWith30ControlPoints = D3D11_PRIMITIVE_30_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 31 control points.
			/// </summary>
			PatchWith31ControlPoints = D3D11_PRIMITIVE_31_CONTROL_POINT_PATCH,

			/// <summary>
			/// Interpret the input primitive as a control point patch with 32 control points.
			/// </summary>
			PatchWith32ControlPoints = D3D11_PRIMITIVE_32_CONTROL_POINT_PATCH
		};

		/// <summary>
		/// Flags that indicate which components of a value are valid.
		/// </summary>
		[System::Flags]
		public enum class RegisterComponentMaskFlags : System::Int32
		{
			/// <summary>
			/// Indicates that no components are valid.
			/// </summary>
			None = 0,
			
			/// <summary>
			/// Indicates that the X (or R) component is valid.
			/// </summary>
			ComponentX = 1,
			
			/// <summary>
			/// Indicates that the Y (or G) component is valid.
			/// </summary>
			ComponentY = 2,
			
			/// <summary>
			/// Indicates that the Z (or B) component is valid.
			/// </summary>
			ComponentZ = 4,
			
			/// <summary>
			/// Indicates that the W (or A) component is valid.
			/// </summary>
			ComponentW = 8,
			
			/// <summary>
			/// Indicates that all components are valid.
			/// </summary>
			All = ComponentX | ComponentY | ComponentZ | ComponentW
		};
		
		/// <summary>Defines possible register component types.</summary>
		/// <unmanaged>D3D10_REGISTER_COMPONENT_TYPE</unmanaged>
		public enum class RegisterComponentType : System::Int32
		{
			/// <summary>
			/// Unknown data type.
			/// </summary>
			Unknown = D3D10_REGISTER_COMPONENT_UNKNOWN,

			/// <summary>
			/// 32-bit unsigned integer.
			/// </summary>
			UInt32 = D3D10_REGISTER_COMPONENT_UINT32,

			/// <summary>
			/// 32-bit signed integer.
			/// </summary>
			SInt32 = D3D10_REGISTER_COMPONENT_SINT32,

			/// <summary>
			/// 32-bit floating-point number.
			/// </summary>
			Float32 = D3D10_REGISTER_COMPONENT_FLOAT32
		};

		/// <summary>
		/// Indicates return value types.
		/// </summary>
		/// <unmanaged>D3D11_RESOURCE_RETURN_TYPE</unmanaged>
		public enum class ResourceReturnType : System::Int32
		{
			/// <summary>
			/// Return type of unsigned normalized integer.
			/// </summary>
			UNorm = D3D11_RETURN_TYPE_UNORM,

			/// <summary>
			/// Return type of signed normalized integer.
			/// </summary>
			SNorm = D3D11_RETURN_TYPE_SNORM,

			/// <summary>
			/// Return type of signed integer.
			/// </summary>
			SInt = D3D11_RETURN_TYPE_SINT,

			/// <summary>
			/// Return type of unsigned integer.
			/// </summary>
			UInt = D3D11_RETURN_TYPE_UINT,

			/// <summary>
			/// Return type of single precision floating point value.
			/// </summary>
			Float = D3D11_RETURN_TYPE_FLOAT,

			/// <summary>
			/// Return type is unknown.
			/// </summary>
			Mixed = D3D11_RETURN_TYPE_MIXED,

			/// <summary>
			/// Return type of double precision floating point value.
			/// </summary>
			Double = D3D11_RETURN_TYPE_DOUBLE,

			/// <summary>
			/// Return type is a multiple-dword type, such as a double or uint64, and the component is continued from the
			/// previous component that was declared. The first component represents the lower bits. 
			/// </summary>
			Continued = D3D11_RETURN_TYPE_CONTINUED
		};

		/// <summary>Provides various shader compilation flags.</summary>
		/// <unmanaged>D3D10_SHADER</unmanaged>
		[System::Flags]
		public enum class ShaderFlags : System::Int32
		{
			/// <summary>
			/// No specific compilation options.
			/// </summary>
			None = 0,

			/// <summary>
			/// Insert debug file/line/type/symbol information into the resulting shader.
			/// </summary>
			Debug = D3D10_SHADER_DEBUG,

			/// <summary>
			/// Do not validate the generated code against known capabilities and constraints. Only use this with shaders that have been successfully
			/// compiled in the past. Shaders are always validated by DirectX before they are set to the device.
			/// </summary>
			SkipValidation = D3D10_SHADER_SKIP_VALIDATION,

			/// <summary>
			/// Skip optimization during code generation; generally recommended for debug only.
			/// </summary>
			SkipOptimization = D3D10_SHADER_SKIP_OPTIMIZATION,

			/// <summary>
			/// Unless explicitly specified, matrices will be packed in row-major order on input and output from the shader.
			/// </summary>
			PackMatrixRowMajor = D3D10_SHADER_PACK_MATRIX_ROW_MAJOR,
			
			/// <summary>
			/// Unless explicitly specified, matrices will be packed in column-major order on input and output from the shader. This is generally
			/// more efficient, since it allows vector-matrix multiplication to be performed using a series of dot-products.
			/// </summary>
			PackMatrixColumnMajor = D3D10_SHADER_PACK_MATRIX_COLUMN_MAJOR,
			
			/// <summary>
			/// Force all computations to be done with partial precision; this may run faster on some hardware.
			/// </summary>
			PartialPrecision = D3D10_SHADER_PARTIAL_PRECISION,

			/// <summary>
			/// Compile a vertex shader for the next highest shader profile. This option turns debugging on (and optimizations off).
			/// </summary>
			ForceSoftwareVertexShader = D3D10_SHADER_FORCE_VS_SOFTWARE_NO_OPT,
			
			/// <summary>
			/// Compile a pixel shader for the next highest shader profile. This option turns debugging on (and optimizations off).
			/// </summary>
			ForceSoftwarePixelShader = D3D10_SHADER_FORCE_PS_SOFTWARE_NO_OPT,
			
			/// <summary>
			/// Disables Preshaders. Using this flag will cause the compiler to not pull out static expression for evaluation.
			/// </summary>
			NoPreshader = D3D10_SHADER_NO_PRESHADER,
			
			/// <summary>
			/// Tells the compiler to not allow flow-control (when possible).
			/// </summary>
			AvoidFlowControl = D3D10_SHADER_AVOID_FLOW_CONTROL,
			
			/// <summary>
			/// Tells the compiler to use flow-control (when possible).
			/// </summary>
			PreferFlowControl = D3D10_SHADER_PREFER_FLOW_CONTROL,
			
			/// <summary>
			/// By default, the HLSL compiler disables strictness on deprecated syntax. Specifying this flag enables strictness which may not allow for legacy syntax.
			/// </summary>
			EnableStrictness = D3D10_SHADER_ENABLE_STRICTNESS,
			
			/// <summary>
			/// This enables older shaders to compile to 4_0 targets.
			/// </summary>
			EnableBackwardsCompatibility = D3D10_SHADER_ENABLE_BACKWARDS_COMPATIBILITY,
			
			/// <summary>
			/// Enables IEEE floating point strictness.
			/// </summary>
			EnableIEEEStrictness = D3D10_SHADER_IEEE_STRICTNESS,
			
			/// <summary>
			/// Lowest optimization level. May produce slower code but will do so more quickly. This may be useful in a highly iterative shader development cycle.
			/// </summary>
			OptimizationLevel0 = D3D10_SHADER_OPTIMIZATION_LEVEL0,
			
			/// <summary>
			/// Second lowest optimization level.
			/// </summary>
			OptimizationLevel1 = D3D10_SHADER_OPTIMIZATION_LEVEL1,
			
			/// <summary>
			/// Second highest optimization level.
			/// </summary>
			OptimizationLevel2 = D3D10_SHADER_OPTIMIZATION_LEVEL2,
			
			/// <summary>
			/// Highest optimization level. Will produce best possible code but may take significantly longer to do so. This will be useful for
			/// final builds of an application where performance is the most important factor.
			/// </summary>
			OptimizationLevel3 = D3D10_SHADER_OPTIMIZATION_LEVEL3,

			/// <summary>
			/// Inform the HLSL compiler to treat all warnings as errors when compiling the shader code. For new shader code, you should use this
			/// option so you can resolve all warnings and ensure the fewest possible hard-to-find code defects.
			/// </summary>
			WarningsAreErrors = D3D10_SHADER_WARNINGS_ARE_ERRORS
		};

		/// <summary>
		/// Identifies shader input options.
		/// </summary>
		/// <unmanaged>D3D10_SHADER_INPUT_FLAGS</unmanaged>
		[System::Flags]
		public enum class ShaderInputFlags : System::Int32
		{
			/// <summary>
			/// Assign a shader input to a register based on the register assignment in the HLSL code (instead of letting the compiler choose the register).
			/// </summary>
			UserPacked = D3D10_SIF_USERPACKED,

			/// <summary>
			/// Use a comparison sampler, which uses the SampleCmp and SampleCmpLevelZero sampling functions.
			/// </summary>
			ComparisonSampler = D3D10_SIF_COMPARISON_SAMPLER,

			/// <summary>
			/// A 2-bit value for encoding texture components.
			/// </summary>
			TextureComponent0 = D3D10_SIF_TEXTURE_COMPONENT_0,

			/// <summary>
			/// A 2-bit value for encoding texture components.
			/// </summary>
			TextureComponent1 = D3D10_SIF_TEXTURE_COMPONENT_1,

			/// <summary>
			/// A 2-bit value for encoding texture components.
			/// </summary>
			TextureComponents = D3D10_SIF_TEXTURE_COMPONENTS
		};

		/// <summary>
		/// Identifies shader resource types.
		/// </summary>
		/// <unmanaged>D3D10_SHADER_INPUT_TYPE</unmanaged>
		public enum class ShaderInputType : System::Int32
		{
			/// <summary>
			/// The shader resource is a constant buffer.
			/// </summary>
			ConstantBuffer = D3D10_SIT_CBUFFER,

			/// <summary>
			/// The shader resource is a texture buffer.
			/// </summary>
			TextureBuffer = D3D10_SIT_TBUFFER,

			/// <summary>
			/// The shader resource is a texture.
			/// </summary>
			Texture = D3D10_SIT_TEXTURE,

			/// <summary>
			/// The shader resource is a sampler.
			/// </summary>
			Sampler = D3D10_SIT_SAMPLER,

			/// <summary>
			/// The shader resource is a typed read and write buffer..
			/// </summary>
			RWTyped = D3D_SIT_UAV_RWTYPED,

			/// <summary>
			/// The shader resource is a structure buffer.
			/// </summary>
			Structured = D3D11_SIT_STRUCTURED,

			/// <summary>
			/// The shader resource is a read and write structured buffer.
			/// </summary>
			RWStructured = D3D11_SIT_UAV_RWSTRUCTURED,

			/// <summary>
			/// The shader resource is a byte-address buffer.
			/// </summary>
			ByteAddress = D3D11_SIT_BYTEADDRESS,

			/// <summary>
			/// The shader resource is a read and write byte-address buffer.
			/// </summary>
			RWByteAddress = D3D11_SIT_UAV_RWBYTEADDRESS,

			/// <summary>
			/// The shader resource is an append-structured buffer.
			/// </summary>
			AppendStructured = D3D11_SIT_UAV_APPEND_STRUCTURED,

			/// <summary>
			/// The shader resource is a consume-structured buffer.
			/// </summary>
			ConsumeStructured = D3D11_SIT_UAV_CONSUME_STRUCTURED,

			/// <summary>
			/// The shader resource is a read and write structured buffer that uses the built-in counter to append or consume.
			/// </summary>
			RWStructuredWithCounter = D3D11_SIT_UAV_RWSTRUCTURED_WITH_COUNTER
		};

		/// <summary>
		/// Identifies shader variable classes.
		/// </summary>
		/// <unmanaged>D3D10_SHADER_VARIABLE_CLASS</unmanaged>
		public enum class ShaderVariableClass : System::Int32
		{
			/// <summary>
			/// The shader variable is a scalar.
			/// </summary>
			Scalar = D3D10_SVC_SCALAR,

			/// <summary>
			/// The shader variable is a vector.
			/// </summary>
			Vector = D3D10_SVC_VECTOR,

			/// <summary>
			/// The shader variable is a row-major matrix.
			/// </summary>
			MatrixRows = D3D10_SVC_MATRIX_ROWS,

			/// <summary>
			/// The shader variable is a column-major matrix.
			/// </summary>
			MatrixColumns = D3D10_SVC_MATRIX_COLUMNS,

			/// <summary>
			/// The shader variable is an object.
			/// </summary>
			Object = D3D10_SVC_OBJECT,

			/// <summary>
			/// The shader variable is a structure.
			/// </summary>
			Struct = D3D10_SVC_STRUCT,

			/// <summary>
			/// The shader variable is a class.
			/// </summary>
			InterfaceClass = D3D11_SVC_INTERFACE_CLASS,

			/// <summary>
			/// The shader variable is an interface pointer.
			/// </summary>
			InterfacePointer = D3D11_SVC_INTERFACE_POINTER,
		};

		/// <summary>
		/// Indicates additional information about a shader variable.
		/// </summary>
		/// <unmanaged>D3D10_SHADER_VARIABLE_FLAGS</unmanaged>
		[System::Flags]
		public enum class ShaderVariableFlags : System::Int32
		{
			/// <summary>
			/// Indicates that the registers assigned to this shader variable were explicitly declared in shader code (instead of automatically assigned by the compiler).
			/// </summary>
			UserPacked = D3D10_SVF_USERPACKED,

			/// <summary>
			/// Indicates that this variable is used by this shader. This value confirms that a particular shader variable
			/// (which can be common to many different shaders) is indeed used by a particular shader. 
			/// </summary>
			Used = D3D10_SVF_USED
		};

		/// <summary>
		/// Identifies possible shader variable types.
		/// </summary>
		/// <unmanaged>D3D10_SHADER_VARIABLE_TYPE</unmanaged>
		public enum class ShaderVariableType : System::Int32
		{
			/// <summary>
			/// The variable is a void pointer.
			/// </summary>
			Void = D3D10_SVT_VOID,

			/// <summary>
			/// The variable is a boolean.
			/// </summary>
			Bool = D3D10_SVT_BOOL,

			/// <summary>
			/// The variable is an integer.
			/// </summary>
			Int = D3D10_SVT_INT,

			/// <summary>
			/// The variable is a floating-point number.
			/// </summary>
			Float = D3D10_SVT_FLOAT,

			/// <summary>
			/// The variable is a string.
			/// </summary>
			String = D3D10_SVT_STRING,

			/// <summary>
			/// The variable is a texture.
			/// </summary>
			Texture = D3D10_SVT_TEXTURE,

			/// <summary>
			/// The variable is a 1D texture.
			/// </summary>
			Texture1D = D3D10_SVT_TEXTURE1D,

			/// <summary>
			/// The variable is a 2D texture.
			/// </summary>
			Texture2D = D3D10_SVT_TEXTURE2D,

			/// <summary>
			/// The variable is a 3D texture.
			/// </summary>
			Texture3D = D3D10_SVT_TEXTURE3D,

			/// <summary>
			/// The variable is a texture cube.
			/// </summary>
			TextureCube = D3D10_SVT_TEXTURECUBE,

			/// <summary>
			/// The variable is a sampler.
			/// </summary>
			Sampler = D3D10_SVT_SAMPLER,

			/// <summary>
			/// The variable is a pixel shader.
			/// </summary>
			PixelShader = D3D10_SVT_PIXELSHADER,

			/// <summary>
			/// The variable is a vertex shader.
			/// </summary>
			VertexShader = D3D10_SVT_VERTEXSHADER,

			/// <summary>
			/// The variable is an unsigned integer.
			/// </summary>
			UInt = D3D10_SVT_UINT,

			/// <summary>
			/// The variable is an 8-bit unsigned integer.
			/// </summary>
			UInt8 = D3D10_SVT_UINT8,

			/// <summary>
			/// The variable is a geometry shader.
			/// </summary>
			GeometryShader = D3D10_SVT_GEOMETRYSHADER,

			/// <summary>
			/// The variable is a rasterizer-state object.
			/// </summary>
			Rasterizer = D3D10_SVT_RASTERIZER,

			/// <summary>
			/// The variable is a depth-stencil-state object.
			/// </summary>
			DepthStencil = D3D10_SVT_DEPTHSTENCIL,

			/// <summary>
			/// The variable is a blend-state object.
			/// </summary>
			Blend = D3D10_SVT_BLEND,

			/// <summary>
			/// The variable is a buffer.
			/// </summary>
			Buffer = D3D10_SVT_BUFFER,

			/// <summary>
			/// The variable is a constant buffer.
			/// </summary>
			CBuffer = D3D10_SVT_CBUFFER,

			/// <summary>
			/// The variable is a texture buffer.
			/// </summary>
			TBuffer = D3D10_SVT_TBUFFER,

			/// <summary>
			/// The variable is a 1D-texture array.
			/// </summary>
			Texture1DArray = D3D10_SVT_TEXTURE1DARRAY,

			/// <summary>
			/// The variable is a 2D-texture array.
			/// </summary>
			Texture2DArray = D3D10_SVT_TEXTURE2DARRAY,

			/// <summary>
			/// The variable is a render-target view.
			/// </summary>
			RenderTargetView = D3D10_SVT_RENDERTARGETVIEW,

			/// <summary>
			/// The variable is a depth-stencil view.
			/// </summary>
			DepthStencilView = D3D10_SVT_DEPTHSTENCILVIEW,

			/// <summary>
			/// The variable is a 2D-multisampled texture.
			/// </summary>
			Texture2DMS = D3D10_SVT_TEXTURE2DMS,

			/// <summary>
			/// The variable is a 2D-multisampled-texture array.
			/// </summary>
			Texture2DMSArray = D3D10_SVT_TEXTURE2DMSARRAY,

			/// <summary>
			/// The variable is a texture-cube array.
			/// </summary>
			TextureCubeArray = D3D10_SVT_TEXTURECUBEARRAY,

			/// <summary>
			/// The variable holds a compiled hull-shader binary.
			/// </summary>
			HullShader = D3D11_SVT_HULLSHADER,

			/// <summary>
			/// The variable holds a compiled domain-shader binary.
			/// </summary>
			DomainShader = D3D11_SVT_DOMAINSHADER,

			/// <summary>
			/// The variable is an interface.
			/// </summary>
			InterfacePointer = D3D11_SVT_INTERFACE_POINTER,

			/// <summary>
			/// The variable holds a compiled compute-shader binary.
			/// </summary>
			ComputeShader = D3D11_SVT_COMPUTESHADER,

			/// <summary>
			/// The variable is a double precision (64-bit) floating-point number.
			/// </summary>
			Double = D3D11_SVT_DOUBLE,

			/// <summary>
			/// The variable is a 1D read and write texture.
			/// </summary>
			RWTexture1D = D3D11_SVT_RWTEXTURE1D,

			/// <summary>
			/// The variable is an array of 1D read and write textures.
			/// </summary>
			RWTexture1DArray = D3D11_SVT_RWTEXTURE1DARRAY,

			/// <summary>
			/// The variable is a 2D read and write texture.
			/// </summary>
			RWTexture2D = D3D11_SVT_RWTEXTURE2D,

			/// <summary>
			/// The variable is an array of 2D read and write textures.
			/// </summary>
			RWTexture2DArray = D3D11_SVT_RWTEXTURE2DARRAY,

			/// <summary>
			/// he variable is a 3D read and write texture.
			/// </summary>
			RWTexture3D = D3D11_SVT_RWTEXTURE3D,

			/// <summary>
			/// The variable is a read and write buffer.
			/// </summary>
			RWBuffer = D3D11_SVT_RWBUFFER,

			/// <summary>
			/// The variable is a byte-address buffer.
			/// </summary>
			ByteAddressBuffer = D3D11_SVT_BYTEADDRESS_BUFFER,

			/// <summary>
			/// The variable is a read and write byte-address buffer.
			/// </summary>
			RWByteAddressBuffer = D3D11_SVT_RWBYTEADDRESS_BUFFER,

			/// <summary>
			/// he variable is a structured buffer. 
			/// </summary>
			StructuredBuffer = D3D11_SVT_STRUCTURED_BUFFER,

			/// <summary>
			/// The variable is a read and write structured buffer.
			/// </summary>
			RWStructuredBuffer = D3D11_SVT_RWSTRUCTURED_BUFFER,

			/// <summary>
			/// The variable is an append structured buffer.
			/// </summary>
			AppendStructuredBuffer = D3D11_SVT_APPEND_STRUCTURED_BUFFER,

			/// <summary>
			/// The variable is a consume structured buffer.
			/// </summary>
			ConsumeStructuredBuffer = D3D11_SVT_CONSUME_STRUCTURED_BUFFER
		};

		/// <summary>
		/// Indicates possible shader types.
		/// </summary>
		/// <unmanaged>D3D11_SHADER_VERSION_TYPE</unmanaged>
		public enum class ShaderVersion : System::Int32
		{
			/// <summary>
			/// A pixel shader.
			/// </summary>
			PixelShader = D3D11_SHVER_PIXEL_SHADER,

			/// <summary>
			/// A vertex shader.
			/// </summary>
			VertexShader = D3D11_SHVER_VERTEX_SHADER,

			/// <summary>
			/// A geometry shader.
			/// </summary>
			GeometryShader = D3D11_SHVER_GEOMETRY_SHADER,

			/// <summary>
			/// A hull shader.
			/// </summary>
			HullShader = D3D11_SHVER_HULL_SHADER,

			/// <summary>
			/// A domain shader.
			/// </summary>
			DomainShader = D3D11_SHVER_DOMAIN_SHADER,

			/// <summary>
			/// A compute shader.
			/// </summary>
			ComputeShader = D3D11_SHVER_COMPUTE_SHADER
		};

		/// <summary>
		/// Specifies options for stripping data from a shader or effect.
		/// </summary>
		/// <unmanaged>D3DCOMPILER_STRIP_FLAGS</unmanaged>
		[System::Flags]
		public enum class StripFlags : System::Int32
		{
			/// <summary>
			/// No extra stripping options.
			/// </summary>
			None = 0,

			/// <summary>
			/// Remove reflection data.
			/// </summary>
			StripReflectionData = D3DCOMPILER_STRIP_REFLECTION_DATA,

			/// <summary>
			/// Remove debug information.
			/// </summary>
			StripDebugInformation = D3DCOMPILER_STRIP_DEBUG_INFO,

			/// <summary>
			/// Remove test blob data.
			/// </summary>
			StripTestBlobs = D3DCOMPILER_STRIP_TEST_BLOBS
		};

		/// <summary>Identifies shader parameters that use system-value semantics.</summary>
		/// <unmanaged>D3D10_NAME</unmanaged>
		public enum class SystemValueType : System::Int32
		{
			/// <summary>
			/// This parameter does not use a predefined system-value semantic.
			/// </summary>
			Undefined = D3D10_NAME_UNDEFINED,

			/// <summary>
			/// This parameter contains position data.
			/// </summary>
			Position = D3D10_NAME_POSITION,

			/// <summary>
			/// This parameter contains clip-distance data.
			/// </summary>
			ClipDistance = D3D10_NAME_CLIP_DISTANCE,

			/// <summary>
			/// This parameter contains cull distance data.
			/// </summary>
			CullDistance = D3D10_NAME_CULL_DISTANCE,

			/// <summary>
			/// This parameter contains a render-target-array index.
			/// </summary>
			RenderTargetArrayIndex = D3D10_NAME_RENDER_TARGET_ARRAY_INDEX,

			/// <summary>
			/// This parameter contains a viewport-array index.
			/// </summary>
			ViewportArrayIndex = D3D10_NAME_VIEWPORT_ARRAY_INDEX,

			/// <summary>
			/// This parameter contains a vertex ID.
			/// </summary>
			VertexId = D3D10_NAME_VERTEX_ID,

			/// <summary>
			/// This parameter contains a primitive ID.
			/// </summary>
			PrimitiveId = D3D10_NAME_PRIMITIVE_ID,

			/// <summary>
			/// This parameter contains a instance ID.
			/// </summary>
			InstanceId = D3D10_NAME_INSTANCE_ID,

			/// <summary>
			/// This parameter contains data that identifies whether or not the primitive faces the camera.
			/// </summary>
			FrontFaceFlag = D3D10_NAME_IS_FRONT_FACE,

			/// <summary>
			/// This parameter contains a sampler-array index.
			/// </summary>
			SampleIndex = D3D10_NAME_SAMPLE_INDEX,

			/// <summary>
			/// This parameter contains render-target data.
			/// </summary>
			Target = D3D10_NAME_TARGET,

			/// <summary>
			/// This parameter contains depth data.
			/// </summary>
			Depth = D3D10_NAME_DEPTH,

			/// <summary>
			/// This parameter contains alpha-coverage data.
			/// </summary>
			Coverage = D3D10_NAME_COVERAGE,

			/// <summary>
			/// This parameter signifies that the value is greater than or equal to a reference value. This flag is used to specify conservative depth for a pixel shader.
			/// </summary>
			DepthGreaterEqual = D3D11_NAME_DEPTH_GREATER_EQUAL,

			/// <summary>
			/// This parameter signifies that the value is less than or equal to a reference value. This flag is used to specify conservative depth for a pixel shader.
			/// </summary>
			DepthLessEqual = D3D11_NAME_DEPTH_LESS_EQUAL,

			/// <summary>
			/// his parameter contains one of four tessellation factors that correspond to the amount of parts that a quad patch is broken into along
			/// the given edge. This flag is used to tessellate a quad patch.
			/// </summary>
			FinalQuadEdgeTessellationFactor = D3D11_NAME_FINAL_QUAD_EDGE_TESSFACTOR,

			/// <summary>
			/// This parameter contains one of two tessellation factors that correspond to the amount of parts that a quad patch is broken into vertically
			/// and horizontally within the patch. This flag is used to tessellate a quad patch.
			/// </summary>
			FinalQuadInsideTessellationFactor = D3D11_NAME_FINAL_QUAD_INSIDE_TESSFACTOR,

			/// <summary>
			/// This parameter contains one of three tessellation factors that correspond to the amount of parts that a tri patch is broken into
			/// along the given edge. This flag is used to tessellate a tri patch.
			/// </summary>
			FinalTriangleEdgeTessellationFactor = D3D11_NAME_FINAL_TRI_EDGE_TESSFACTOR,

			/// <summary>
			/// This parameter contains the tessellation factor that corresponds to the amount of parts that a tri patch is broken into within the patch.
			/// This flag is used to tessellate a tri patch.
			/// </summary>
			FinalTriangleInsideTessellationFactor = D3D11_NAME_FINAL_TRI_INSIDE_TESSFACTOR,

			/// <summary>
			/// This parameter contains the tessellation factor that corresponds to the amount of lines broken into within the patch. This flag is used to tessellate an isolines patch.
			/// </summary>
			FinalLineDetailTessellationFactor = D3D11_NAME_FINAL_LINE_DETAIL_TESSFACTOR,

			/// <summary>
			/// This parameter contains the tessellation factor that corresponds to the amount of lines that are created within the patch. This flag is used to tessellate an isolines patch.
			/// </summary>
			FinalLineDensityTessellationFactor = D3D11_NAME_FINAL_LINE_DENSITY_TESSFACTOR
		};

		/// <summary>
		/// Specifies domain options for tessellator data.
		/// </summary>
		/// <unmanaged>D3D11_TESSELLATOR_DOMAIN</unmanaged>
		public enum class TessellatorDomain : System::Int32
		{
			/// <summary>
			/// The data type is undefined.
			/// </summary>
			Undefined = D3D11_TESSELLATOR_DOMAIN_UNDEFINED,

			/// <summary>
			/// Isoline data.
			/// </summary>
			Isoline = D3D11_TESSELLATOR_DOMAIN_ISOLINE,

			/// <summary>
			/// Triangle data.
			/// </summary>
			Triangle = D3D11_TESSELLATOR_DOMAIN_TRI,

			/// <summary>
			/// Quad data.
			/// </summary>
			Quad = D3D11_TESSELLATOR_DOMAIN_QUAD
		};

		/// <summary>
		/// Specifies output primitive types for the tessellator.
		/// </summary>
		/// <unmanaged>D3D11_TESSELLATOR_OUTPUT_PRIMITIVE</unmanaged>
		public enum class TessellatorOutputPrimitive : System::Int32
		{
			/// <summary>
			/// The output primitive type is undefined.
			/// </summary>
			Undefined = D3D11_TESSELLATOR_OUTPUT_UNDEFINED,

			/// <summary>
			/// The output primitive type a point.
			/// </summary>
			Point = D3D11_TESSELLATOR_OUTPUT_POINT,

			/// <summary>
			/// The output primitive type is a line.
			/// </summary>
			Line = D3D11_TESSELLATOR_OUTPUT_LINE,

			/// <summary>
			/// The output primitive type is a clockwise triangle.
			/// </summary>
			TriangleClockwise = D3D11_TESSELLATOR_OUTPUT_TRIANGLE_CW,

			/// <summary>
			/// The output primitive type is a counter clockwise triangle.
			/// </summary>
			TriangleCounterclockwise = D3D11_TESSELLATOR_OUTPUT_TRIANGLE_CCW
		};

		/// <summary>
		/// Specifies tessellator partitioning options.
		/// </summary>
		/// <unmanaged>D3D11_TESSELLATOR_PARTITIONING</unmanaged>
		public enum class TessellatorPartitioning : System::Int32
		{
			/// <summary>
			/// The partitioning type is undefined.
			/// </summary>
			Undefined = D3D11_TESSELLATOR_PARTITIONING_UNDEFINED,

			/// <summary>
			/// Partition with integers only.
			/// </summary>
			Integer = D3D11_TESSELLATOR_PARTITIONING_INTEGER,

			/// <summary>
			/// Partition with a power-of-two number only.
			/// </summary>
			PowerOfTwo = D3D11_TESSELLATOR_PARTITIONING_POW2,

			/// <summary>
			/// Partition with an odd, fractional number.
			/// </summary>
			FractionalOdd = D3D11_TESSELLATOR_PARTITIONING_FRACTIONAL_ODD,

			/// <summary>
			/// Partition with an even, fractional number.
			/// </summary>
			FractionalEven = D3D11_TESSELLATOR_PARTITIONING_FRACTIONAL_EVEN
		};
	}
}