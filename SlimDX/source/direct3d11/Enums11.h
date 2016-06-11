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

namespace SlimDX
{
	namespace Direct3D11
	{
#ifdef XMLDOCS
		ref class Effect;
		ref class Sprite;
		ref class Asynchronous;
		ref class DeviceContext;
		value class TimestampQueryData;
#endif
	
		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.

		/// <summary>Optional flags that control the behavior of <see cref="Asynchronous">Asynchronous.GetData</see>.</summary>
		/// <unmanaged>D3D11_ASYNC_GETDATA_FLAG</unmanaged>
		[System::Flags]
		public enum class AsynchronousFlags : System::Int32
		{
			/// <summary>
			/// Indicates no specific behavior.
			/// </summary>
			None = 0,

			/// <summary>
			/// Indicates the command buffer should not be flushed.
			/// Note that this can cause an infinite loop if GetData() is called repeatedly until 
			/// returning successfully: there may still be commands in the buffer than need
			/// to be processed in order for GetData() to return successfully. Since the buffer 
			/// will not be flushed, those commands will not be processed and GetData() will never
			/// return successfully.
			/// </summary>
			DoNotFlush = D3D11_ASYNC_GETDATA_DONOTFLUSH
		};
		
		/// <summary>Identifies how to bind a resource to the pipeline.</summary>
		/// <unmanaged>D3D11_BIND_FLAG</unmanaged>
		[System::Flags]
		public enum class BindFlags : System::Int32
		{
			/// <summary>
			/// Indicates no specific behavior.
			/// </summary>
			None = 0,

			/// <summary>
			/// Indicates the resource can be bound to the input-assembler stage as a vertex buffer. 
			/// </summary>
			VertexBuffer = D3D11_BIND_VERTEX_BUFFER,
			
			/// <summary>
			/// Indicates the resource can be bound to the input-assembler stage as an index buffer 
			/// </summary>
			IndexBuffer = D3D11_BIND_INDEX_BUFFER,

			/// <summary>
			/// Indicates the resource can be bound to the shader stage as a constant buffer 
			/// </summary>
			ConstantBuffer = D3D11_BIND_CONSTANT_BUFFER,

			/// <summary>
			/// Indicates the resource can be bound to the shader stage as a buffer or texture. 
			/// Note that it is invalid to specify this flag and subsequently map the resource
			/// using MapMode.WriteNoOverwrite.
			/// </summary>
			ShaderResource = D3D11_BIND_SHADER_RESOURCE, 

			/// <summary>
			/// Indicates the resource can be bound to the stream-output stage as an output buffer. 
			/// </summary>
			StreamOutput = D3D11_BIND_STREAM_OUTPUT,

			/// <summary>
			/// Indicates the resource can be bound to the output-merger stage as a render target. 
			/// </summary>
			RenderTarget = D3D11_BIND_RENDER_TARGET,

			/// <summary>
			/// Indicates the resource can be bound to the output-merger stage as a depth-stencil target. 
			/// </summary>
			DepthStencil = D3D11_BIND_DEPTH_STENCIL,
			
			/// <summary>
			/// Indicates the resource can be bound as an unordered access resource.
			/// </summary>
			UnorderedAccess = D3D11_BIND_UNORDERED_ACCESS
		};
		
		/// <summary>RGB or alpha blending operation.</summary>
		/// <unmanaged>D3D11_BLEND_OP</unmanaged>
		public enum class BlendOperation : System::Int32
		{
			/// <summary>
			/// Add source 1 and source 2.
			/// </summary>
			Add = D3D11_BLEND_OP_ADD,

			/// <summary>
			/// Subtract source 1 from source 2.
			/// </summary>
			Subtract = D3D11_BLEND_OP_SUBTRACT,

			/// <summary>
			/// Subtract source 2 from source 1.
			/// </summary>
			ReverseSubtract = D3D11_BLEND_OP_REV_SUBTRACT,

			/// <summary>
			/// Find the minimum of source 1 and source 2.
			/// </summary>
			Minimum = D3D11_BLEND_OP_MIN,

			/// <summary>
			/// Find the maximum of source 1 and source 2.
			/// </summary>
			Maximum = D3D11_BLEND_OP_MAX
		};
		
		/// <summary>Blend options. A blend option identifies the data source and an optional pre-blend operation.</summary>
		/// <unmanaged>D3D11_BLEND</unmanaged>
		public enum class BlendOption : System::Int32
		{
			/// <summary>
			/// The data source is the color black (0, 0, 0, 0). No pre-blend operation.
			/// </summary>
			Zero = D3D11_BLEND_ZERO,

			/// <summary>
			/// The data source is the color white (1, 1, 1, 1). No pre-blend operation.
			/// </summary>
			One = D3D11_BLEND_ONE,

			/// <summary>
			/// The data source is color data from a pixel shader. No pre-blend operation.
			/// </summary>
			SourceColor = D3D11_BLEND_SRC_COLOR,

			/// <summary>
			/// The data source is color data from a pixel shader. The pre-blend operation inverts the data, generating 1 - RGB.
			/// </summary>
			InverseSourceColor = D3D11_BLEND_INV_SRC_COLOR,

			/// <summary>
			/// The data source is alpha data from a pixel shader. No pre-blend operation.
			/// </summary>
			SourceAlpha = D3D11_BLEND_SRC_ALPHA,

			/// <summary>
			/// The data source is alpha data from a pixel shader. The pre-blend operation inverts the data, generating 1 - A.
			/// </summary>
			InverseSourceAlpha = D3D11_BLEND_INV_SRC_ALPHA,

			/// <summary>
			/// The data source is alpha data from a render target. No pre-blend operation.
			/// </summary>
			DestinationAlpha = D3D11_BLEND_DEST_ALPHA,

			/// <summary>
			/// The data source is alpha data from a render target. The pre-blend operation inverts the data, generating 1 - A.
			/// </summary>
			InverseDestinationAlpha = D3D11_BLEND_INV_DEST_ALPHA,

			/// <summary>
			/// The data source is color data from a render target. No pre-blend operation.
			/// </summary>
			DestinationColor = D3D11_BLEND_DEST_COLOR,

			/// <summary>
			/// The data source is color data from a render target. The pre-blend operation inverts the data, generating 1 - RGB.
			/// </summary>
			InverseDestinationColor = D3D11_BLEND_INV_DEST_COLOR,

			/// <summary>
			/// The data source is alpha data from a pixel shader. The pre-blend operation clamps the data to 1 or less.
			/// </summary>
			SourceAlphaSaturate = D3D11_BLEND_SRC_ALPHA_SAT,

			/// <summary>
			/// The data source is the blend factor set in the output-merger blend state. No pre-blend operation.
			/// </summary>
			BlendFactor = D3D11_BLEND_BLEND_FACTOR,

			/// <summary>
			/// The data source is the blend factor set in the output-merger blend state. The pre-blend operation inverts the data, generating 1 - blend_factor.
			/// </summary>
			InverseBlendFactor = D3D11_BLEND_INV_BLEND_FACTOR,

			/// <summary>
			/// The data sources are both color data from a pixel shader. No pre-blend operation. This option supports dual-source blending.
			/// </summary>
			SecondarySourceColor = D3D11_BLEND_SRC1_COLOR,

			/// <summary>
			/// The data sources are both color data from a pixel shader. The pre-blend operation inverts the data, generating 1 - RGB. This option supports dual-source blending.
			/// </summary>
			InverseSecondarySourceColor = D3D11_BLEND_INV_SRC1_COLOR,

			/// <summary>
			/// The data sources are both alpha data from a pixel shader. No pre-blend operation. This option supports dual-source blending.
			/// </summary>
			SecondarySourceAlpha = D3D11_BLEND_SRC1_ALPHA,

			/// <summary>
			/// The data sources are both alpha data from a pixel shader. The pre-blend operation inverts the data, generating 1 - A. This option supports dual-source blending.
			/// </summary>
			InverseSecondarySourceAlpha = D3D11_BLEND_INV_SRC1_ALPHA
		};

		/// <summary>
		/// The following flags are used to specify which channels in a texture to operate on.
		/// </summary>
		/// <unmanaged>D3DX11_CHANNEL_FLAG</unmanaged>
		[System::Flags]
		public enum class Channel : System::Int32
		{
			/// <summary>
			/// Indicates the red channel should be used.
			/// </summary>
			Red = D3DX11_CHANNEL_RED,

			/// <summary>
			/// Indicates the blue channel should be used.
			/// </summary>
			Blue = D3DX11_CHANNEL_BLUE,

			/// <summary>
			/// Indicates the green channel should be used.
			/// </summary>
			Green = D3DX11_CHANNEL_GREEN,

			/// <summary>
			/// Indicates the alpha channel should be used.
			/// </summary>
			Alpha = D3DX11_CHANNEL_ALPHA,

			/// <summary>
			/// Indicates the luminances of the red, green, and blue channels should be used.
			/// </summary>
			Luminance = D3DX11_CHANNEL_LUMINANCE
		};
		
		/// <summary>Identifies which components of each pixel of a render target are writable during blending.</summary>
		/// <unmanaged>D3D11_COLOR_WRITE_ENABLE</unmanaged>
		[System::Flags]
		public enum class ColorWriteMaskFlags : System::Int32
		{
			/// <summary>
			/// Indicates no specific behavior.
			/// </summary>
			None = 0,

			/// <summary>
			/// Allow data to be stored in the red component.
			/// </summary>
			Red = D3D11_COLOR_WRITE_ENABLE_RED,

			/// <summary>
			/// Allow data to be stored in the green component.
			/// </summary>
			Green = D3D11_COLOR_WRITE_ENABLE_GREEN,

			/// <summary>
			/// Allow data to be stored in the blue component.
			/// </summary>
			Blue = D3D11_COLOR_WRITE_ENABLE_BLUE,

			/// <summary>
			/// Allow data to be stored in the alpha component.
			/// </summary>
			Alpha = D3D11_COLOR_WRITE_ENABLE_ALPHA,

			/// <summary>
			/// Allow data to be stored in all components.
			/// </summary>
			All = D3D11_COLOR_WRITE_ENABLE_ALL
		};
		
		/// <summary>Specifies possible comparison functions.</summary>
		/// <unmanaged>D3D11_COMPARISON_FUNC</unmanaged>
		public enum class Comparison : System::Int32
		{
			/// <summary>
			/// Never pass the comparison.
			/// </summary>
			Never = D3D11_COMPARISON_NEVER,

			/// <summary>
			/// Pass the comparison if the source data is less than the destination data.
			/// </summary>
			Less = D3D11_COMPARISON_LESS,

			/// <summary>
			/// Pass the comparison if the source data is equal to the destination data.
			/// </summary>
			Equal = D3D11_COMPARISON_EQUAL,

			/// <summary>
			/// Pass the comparison if the source data is less than or equal to the destination data.
			/// </summary>
			LessEqual = D3D11_COMPARISON_LESS_EQUAL,

			/// <summary>
			/// Pass the comparison if the source data is greater than the destination data.
			/// </summary>
			Greater = D3D11_COMPARISON_GREATER,

			/// <summary>
			/// Pass the comparison if the source data is not equal to the destination data.
			/// </summary>
			NotEqual = D3D11_COMPARISON_NOT_EQUAL,

			/// <summary>
			/// Pass the comparison if the source data is greater than or equal to the destination data.
			/// </summary>
			GreaterEqual = D3D11_COMPARISON_GREATER_EQUAL,

			/// <summary>
			/// Always pass the comparison.
			/// </summary>
			Always = D3D11_COMPARISON_ALWAYS
		};

		/// <summary>Specifies unordered resource support options for a compute shader resource.</summary>
		/// <unmanaged>D3D11_FORMAT_SUPPORT2</unmanaged>
		[System::Flags]
		public enum class ComputeShaderFormatSupport : System::Int32
		{
			/// <summary>
			/// No specific options supported.
			/// </summary>
			None = 0,

			/// <summary>
			/// Format supports atomic add.
			/// </summary>
			AtomicAdd = D3D11_FORMAT_SUPPORT2_UAV_ATOMIC_ADD,

			/// <summary>
			/// Format supports atomic bitwise operations.
			/// </summary>
			AtomicBitwiseOperations = D3D11_FORMAT_SUPPORT2_UAV_ATOMIC_BITWISE_OPS,

			/// <summary>
			/// Format supports atomic compare with store or exchange.
			/// </summary>
			AtomicCompareStoreOrCompareExchange = D3D11_FORMAT_SUPPORT2_UAV_ATOMIC_COMPARE_STORE_OR_COMPARE_EXCHANGE,

			/// <summary>
			/// Format supports atomic exchange.
			/// </summary>
			AtomicExchange = D3D11_FORMAT_SUPPORT2_UAV_ATOMIC_EXCHANGE,

			/// <summary>
			/// Format supports atomic min and max.
			/// </summary>
			AtomicSignedMinMax = D3D11_FORMAT_SUPPORT2_UAV_ATOMIC_SIGNED_MIN_OR_MAX,

			/// <summary>
			/// Format supports atomic unsigned min and max.
			/// </summary>
			AtomicUnsignedMinMax = D3D11_FORMAT_SUPPORT2_UAV_ATOMIC_UNSIGNED_MIN_OR_MAX,

			/// <summary>
			/// Format supports a typed load.
			/// </summary>
			TypedLoad = D3D11_FORMAT_SUPPORT2_UAV_TYPED_LOAD,

			/// <summary>
			/// Format supports a typed store.
			/// </summary>
			TypedStore = D3D11_FORMAT_SUPPORT2_UAV_TYPED_STORE
		};
		
		/// <summary>Specifies possible performance counter types.</summary>
		/// <unmanaged>D3D11_COUNTER</unmanaged>
		public enum class CounterKind : System::Int32
		{
			/// <summary>
			/// A performance counter that is dependent on a hardware device.
			/// </summary>
			DeviceDependent = D3D11_COUNTER_DEVICE_DEPENDENT_0
		};
		
		/// <summary>Defines data types for performance counters.</summary>
		/// <unmanaged>D3D11_COUNTER_TYPE</unmanaged>
		public enum class CounterType : System::Int32
		{
			/// <summary>
			/// 32-bit floating point counter.
			/// </summary>
			Float32 = D3D11_COUNTER_TYPE_FLOAT32,

			/// <summary>
			/// 16-bit unsigned integer counter.
			/// </summary>
			UnsignedInt16 = D3D11_COUNTER_TYPE_UINT16,

			/// <summary>
			/// 32-bit unsigned integer counter.
			/// </summary>
			UnsignedInt32 = D3D11_COUNTER_TYPE_UINT32,

			/// <summary>
			/// 64-bit unsigned integer counter.
			/// </summary>
			UnsignedInt64 = D3D11_COUNTER_TYPE_UINT64
		};
		
		/// <summary>Specifies the types of CPU access allowed for a resource.</summary>
		/// <unmanaged>D3D11_CPU_ACCESS_FLAG</unmanaged>
		[System::Flags]
		public enum class CpuAccessFlags : System::Int32
		{
			/// <summary>
			/// Indicates no specific behavior.
			/// </summary>
			None = 0,

			/// <summary>
			/// The resource is to be mappable so that the CPU can change its contents. Resources created with this flag cannot be set as 
			/// outputs of the pipeline and must be created with either dynamic or staging usage.
			/// </summary>
			Write = D3D11_CPU_ACCESS_WRITE,

			/// <summary>
			/// The resource is to be mappable so that the CPU can read its contents. Resources created with this flag cannot be set as either 
			/// inputs or outputs to the pipeline and must be created with staging usage.
			/// </summary>
			Read = D3D11_CPU_ACCESS_READ
		};
		
		/// <summary>Specifies possible culling modes.</summary>
		/// <unmanaged>D3D11_CULL_MODE</unmanaged>
		public enum class CullMode : System::Int32 
		{
			/// <summary>
			/// Always draw all triangles.
			/// </summary>
			None = D3D11_CULL_NONE,

			/// <summary>
			/// Do not draw triangles that are front-facing.
			/// </summary>
			Front = D3D11_CULL_FRONT,

			/// <summary>
			/// Do not draw triangles that are back-facing.
			/// </summary>
			Back = D3D11_CULL_BACK
		};

		/// <summary>
		/// Contains debug features that can be enabled and disabled.
		/// </summary>
		/// <unmanaged>D3D11_DEBUG_FEATURE</unmanaged>
		[System::Flags]
		public enum class DebugFeatureFlags : System::Int32
		{
			/// <summary>
			/// Application will wait for the GPU to finish processing the rendering operation before continuing.
			/// </summary>
			FinishPerRender = D3D11_DEBUG_FEATURE_FINISH_PER_RENDER_OP,

			/// <summary>
			/// Runtime will additionally call DeviceContext.Flush.
			/// </summary>
			FlushPerRender = D3D11_DEBUG_FEATURE_FLUSH_PER_RENDER_OP,

			/// <summary>
			/// Runtime will call SwapChain.Present. Presentation of render buffers will occur according to the settings established by prior calls
			/// to Debug.SwapChain and Debug.PresentDelay.
			/// </summary>
			PresentPerRender = D3D11_DEBUG_FEATURE_PRESENT_PER_RENDER_OP
		};
		
		/// <summary>Specifies the parts of the depth stencil surface to clear.</summary>
		/// <unmanaged>D3D11_CLEAR_FLAG</unmanaged>
		[System::Flags]
		public enum class DepthStencilClearFlags : System::Int32
		{
			/// <summary>
			/// Clear the depth buffer.
			/// </summary>
			Depth = D3D11_CLEAR_DEPTH,

			/// <summary>
			/// Clear the stencil buffer.
			/// </summary>
			Stencil = D3D11_CLEAR_STENCIL
		};
		
		/// <summary>Specifies how to access a resource used in a depth-stencil view.</summary>
		/// <unmanaged>D3D11_DSV_DIMENSION</unmanaged>
		public enum class DepthStencilViewDimension : System::Int32
		{
			/// <summary>
			/// The resource will be accessed according to its type as determined from the actual instance this enumeration is 
			/// paired with when the depth-stencil view is created.
			/// </summary>
			Unknown = D3D11_DSV_DIMENSION_UNKNOWN,

			/// <summary>
			/// The resource will be accessed as a 1D texture.
			/// </summary>
			Texture1D = D3D11_DSV_DIMENSION_TEXTURE1D,

			/// <summary>
			/// The resource will be accessed as an array of 1D textures.
			/// </summary>
			Texture1DArray = D3D11_DSV_DIMENSION_TEXTURE1DARRAY,

			/// <summary>
			/// The resource will be accessed as a 2D texture.
			/// </summary>
			Texture2D = D3D11_DSV_DIMENSION_TEXTURE2D,

			/// <summary>
			/// The resource will be accessed as an array of 2D textures.
			/// </summary>
			Texture2DArray = D3D11_DSV_DIMENSION_TEXTURE2DARRAY,

			/// <summary>
			/// The resource will be accessed as a 2D texture with multisampling.
			/// </summary>
			Texture2DMultisampled = D3D11_DSV_DIMENSION_TEXTURE2DMS,

			/// <summary>
			/// The resource will be accessed as an array of 2D textures with multisampling.
			/// </summary>
			Texture2DMultisampledArray = D3D11_DSV_DIMENSION_TEXTURE2DMSARRAY
		};

		[System::Flags]
		public enum class DepthStencilViewFlags : System::Int32
		{
			None = 0,
			ReadOnlyDepth = D3D11_DSV_READ_ONLY_DEPTH,
			ReadOnlyStencil = D3D11_DSV_READ_ONLY_STENCIL
		};
		
		/// <summary>Identifies the portion of a depth-stencil buffer for writing depth data.</summary>
		/// <unmanaged>D3D11_DEPTH_WRITE_MASK</unmanaged>
		public enum class DepthWriteMask : System::Int32
		{
			/// <summary>
			/// Turn off writes to the depth-stencil buffer.
			/// </summary>
			Zero = D3D11_DEPTH_WRITE_MASK_ZERO,

			/// <summary>
			/// Turn on writes to the depth-stencil buffer.
			/// </summary>
			All = D3D11_DEPTH_WRITE_MASK_ALL
		};

		/// <summary>Specifies possible device context types.</summary>
		/// <unmanaged>D3D11_DEVICE_CONTEXT_TYPE</unmanaged>
		public enum class DeviceContextType : System::Int32
		{
			/// <summary>
			/// The device context is an immediate context.
			/// </summary>
			Immediate = D3D11_DEVICE_CONTEXT_IMMEDIATE,

			/// <summary>
			/// The device context is a deferred context.
			/// </summary>
			Deferred = D3D11_DEVICE_CONTEXT_DEFERRED
		};
		
		/// <summary>Describes parameters that are used to create a device.</summary>
		/// <unmanaged>D3D11_CREATE_DEVICE_FLAG</unmanaged>
		[System::Flags]
		public enum class DeviceCreationFlags : System::Int32
		{
			/// <summary>
			/// Indicates no specific behavior.
			/// </summary>
			None = 0,

			/// <summary>
			/// Use this flag if an application will only be calling D3D11 from a single thread. If this flag is not specified, the default 
			/// behavior of D3D11 is to enter a lock during each API call to prevent multiple threads altering internal state. By using this flag 
			/// no locks will be taken which can slightly increase performance, but could result in undefine behavior if D3D11 is called from 
			/// multiple threads.
			/// </summary>
			SingleThreaded = D3D11_CREATE_DEVICE_SINGLETHREADED,

			/// <summary>
			/// Creates a device that supports the debug layer.
			/// </summary>
			Debug = D3D11_CREATE_DEVICE_DEBUG,

			/// <summary>
			/// Prevents multiple threads from being created. This flag is not recommended for general use.
			/// </summary>
			PreventThreadingOptimizations = D3D11_CREATE_DEVICE_PREVENT_INTERNAL_THREADING_OPTIMIZATIONS,

			BgraSupport = D3D11_CREATE_DEVICE_BGRA_SUPPORT
		};
	
		/// <summary>Specifies possible driver types.</summary>
		/// <unmanaged>D3D_DRIVER_TYPE</unmanaged>
		public enum class DriverType : System::Int32
		{
			/// <summary>
			/// The driver type is unknown.
			/// </summary>
			Unknown = D3D_DRIVER_TYPE_UNKNOWN,

			/// <summary>
			/// A hardware driver, which implements Direct3D features in hardware. This is the primary driver that you should use 
			/// in your Direct3D applications because it provides the best performance. A hardware driver uses hardware acceleration
			/// (on supported hardware) but can also use software for parts of the pipeline that are not supported in hardware. This driver 
			/// type is often referred to as a hardware abstraction layer or HAL.
			/// </summary>
			Hardware = D3D_DRIVER_TYPE_HARDWARE,

			/// <summary>
			/// A reference driver, which is a software implementation that supports every Direct3D feature. A reference driver is designed
			/// for accuracy rather than speed and as a result is slow but accurate. The rasterizer portion of the driver does make use of special
			/// CPU instructions whenever it can, but it is not intended for retail applications; use it only for feature testing, demonstration
			/// of functionality, debugging, or verifying bugs in other drivers. This driver is installed by the DirectX SDK.
			/// This driver may be referred to as a REF driver, a reference driver or a reference rasterizer.
			/// </summary>
			Reference = D3D_DRIVER_TYPE_REFERENCE,

			/// <summary>
			/// A NULL driver, which is a reference driver without render capability. This driver is commonly used for debugging
			/// non-rendering API calls, it is not appropriate for retail applications. This driver is installed by the DirectX SDK.
			/// </summary>
			Null = D3D_DRIVER_TYPE_NULL,

			/// <summary>
			/// A software driver, which is a driver implemented completely in software. The software implementation is not intended
			/// for a high-performance application due to its very slow performance.
			/// </summary>
			Software = D3D_DRIVER_TYPE_SOFTWARE,

			/// <summary>
			/// A WARP driver, which is a high-performance software rasterizer. The rasterizer supports all current feature levels
			/// (level 9.1 through level 10.1) with a high performance software implementation when hardware is not available.
			/// </summary>
			Warp = D3D_DRIVER_TYPE_WARP
		};

		/// <summary>Specifies detailed information about effect variables.</summary>
		/// <unmanaged>D3DX11_EFFECT_VARIABLE</unmanaged>
		[System::Flags]
		public enum class EffectVariableFlags : System::Int32
		{
			/// <summary>
			/// Standard effect variable.
			/// </summary>
			None = 0,
			
			/// <summary>
			/// Indicates that the variable is an annotation or global.
			/// </summary>
			Annotation = D3DX11_EFFECT_VARIABLE_ANNOTATION,
			
			/// <summary>
			/// Indicates the variable has been explicitly bound using the register keyword in the effect code.
			/// </summary>
			ExplicitBindPoint = D3DX11_EFFECT_VARIABLE_EXPLICIT_BIND_POINT,
		};

		[System::Flags]
		public enum class FastFourierTransformCreationFlags : System::Int32
		{
			None = 0,
			DoNotPrecomputeBuffers = D3DX11_FFT_CREATE_FLAG_NO_PRECOMPUTE_BUFFERS
		};

		public enum class FastFourierTransformDataType : System::Int32
		{
			Real = D3DX11_FFT_DATA_TYPE_REAL,
			Complex = D3DX11_FFT_DATA_TYPE_COMPLEX
		};

		[System::Flags]
		public enum class FastFourierTransformDimensions : System::Int32
		{
			Dimension1D = D3DX11_FFT_DIM_MASK_1D,
			Dimension2D = D3DX11_FFT_DIM_MASK_2D,
			Dimension3D = D3DX11_FFT_DIM_MASK_3D
		};

		/// <summary>Identifies device features that can be queried for support.</summary>
		/// <unmanaged>D3D11_FEATURE</unmanaged>
		public enum class Feature : System::Int32
		{
			/// <summary>
			/// The device supports the use of doubles in HLSL.
			/// </summary>
			ShaderDoubles,

			/// <summary>
			/// The device supports compute shaders along with raw and structured buffers via shader 4.X.
			/// </summary>
			ComputeShaders
		};

		/// <summary>Describes the set of features targeted by a Direct3D device.</summary>
		/// <unmanaged>D3D_FEATURE_LEVEL</unmanaged>
		public enum class FeatureLevel : System::Int32
		{
			/// <summary>
			/// Targets features supported by Direct3D 11.0 including shader shader model 5.
			/// </summary>
			Level_11_0 = D3D_FEATURE_LEVEL_11_0,

			/// <summary>
			/// Targets features supported by Direct3D 10.0 including shader shader model 4.
			/// </summary>
			Level_10_0 = D3D_FEATURE_LEVEL_10_0,

			/// <summary>
			/// Targets features supported by Direct3D 10.1 including shader shader model 4.
			/// </summary>
			Level_10_1 = D3D_FEATURE_LEVEL_10_1,

			/// <summary>
			/// Targets features supported by Direct3D 9.1 including shader shader model 2.
			/// </summary>
			Level_9_1 = D3D_FEATURE_LEVEL_9_1,

			/// <summary>
			/// Targets features supported by Direct3D 9.2 including shader shader model 2.
			/// </summary>
			Level_9_2 = D3D_FEATURE_LEVEL_9_2,

			/// <summary>
			/// Targets features supported by Direct3D 9.3 including shader shader model 3.
			/// </summary>
			Level_9_3 = D3D_FEATURE_LEVEL_9_3
		};
		
		/// <summary>Determines the fill mode to use when rendering triangles.</summary>
		/// <unmanaged>D3D11_FILL_MODE</unmanaged>
		public enum class FillMode : System::Int32
		{
			/// <summary>
			/// Draw lines connecting the vertices. Adjacent vertices are not drawn.
			/// </summary>
			Wireframe = D3D11_FILL_WIREFRAME,

			/// <summary>
			/// Fill the triangles formed by the vertices. Adjacent vertices are not drawn.
			/// </summary>
			Solid = D3D11_FILL_SOLID
		};

		/// <summary>Specifies filtering options used during texture sampling.</summary>
		/// <unmanaged>D3D11_FILTER</unmanaged>
		public enum class Filter : System::Int32
		{
			/// <summary>
			/// Use point sampling for minification, magnification, and mip-level sampling.
			/// </summary>
			MinMagMipPoint = D3D11_FILTER_MIN_MAG_MIP_POINT,

			/// <summary>
			/// Use point sampling for minification and magnification; use linear interpolation for mip-level sampling.
			/// </summary>
			MinMagPointMipLinear = D3D11_FILTER_MIN_MAG_POINT_MIP_LINEAR,

			/// <summary>
			/// Use point sampling for minification; use linear interpolation for magnification; use point sampling for mip-level sampling.
			/// </summary>
			MinPointMagLinearMipPoint = D3D11_FILTER_MIN_POINT_MAG_LINEAR_MIP_POINT,

			/// <summary>
			/// Use point sampling for minification; use linear interpolation for magnification and mip-level sampling.
			/// </summary>
			MinPointMagMipLinear = D3D11_FILTER_MIN_POINT_MAG_MIP_LINEAR,

			/// <summary>
			/// Use linear interpolation for minification; use point sampling for magnification and mip-level sampling.
			/// </summary>
			MinLinearMagMipPoint = D3D11_FILTER_MIN_LINEAR_MAG_MIP_POINT,

			/// <summary>
			/// Use linear interpolation for minification; use point sampling for magnification; use linear interpolation for mip-level sampling.
			/// </summary>
			MinLinearMagPointMipLinear = D3D11_FILTER_MIN_LINEAR_MAG_POINT_MIP_LINEAR,

			/// <summary>
			/// Use linear interpolation for minification and magnification; use point sampling for mip-level sampling.
			/// </summary>
			MinMagLinearMipPoint = D3D11_FILTER_MIN_MAG_LINEAR_MIP_POINT,

			/// <summary>
			/// Use linear interpolation for minification, magnification, and mip-level sampling.
			/// </summary>
			MinMagMipLinear = D3D11_FILTER_MIN_MAG_MIP_LINEAR,

			/// <summary>
			/// Use anisotropic interpolation for minification, magnification, and mip-level sampling.
			/// </summary>
			Anisotropic = D3D11_FILTER_ANISOTROPIC,

			/// <summary>
			/// Use point sampling for minification, magnification, and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinMagMipPoint = D3D11_FILTER_COMPARISON_MIN_MAG_MIP_POINT,

			/// <summary>
			/// Use point sampling for minification and magnification; use linear interpolation for mip-level sampling.
			/// Compare the result to the comparison value.
			/// </summary>
			ComparisonMinMagPointMipLinear = D3D11_FILTER_COMPARISON_MIN_MAG_POINT_MIP_LINEAR,

			/// <summary>
			/// Use point sampling for minification; use linear interpolation for magnification; use point sampling for mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinPointMagLinearMipPoint = D3D11_FILTER_COMPARISON_MIN_POINT_MAG_LINEAR_MIP_POINT,

			/// <summary>
			/// Use point sampling for minification; use linear interpolation for magnification and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinPointMagMipLinear = D3D11_FILTER_COMPARISON_MIN_POINT_MAG_MIP_LINEAR,

			/// <summary>
			/// Use linear interpolation for minification; use point sampling for magnification and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinLinearMagMipPoint = D3D11_FILTER_COMPARISON_MIN_LINEAR_MAG_MIP_POINT,

			/// <summary>
			/// Use linear interpolation for minification; use point sampling for magnification; use linear interpolation for mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinLinearMagPointMipLinear = D3D11_FILTER_COMPARISON_MIN_LINEAR_MAG_POINT_MIP_LINEAR,

			/// <summary>
			/// Use linear interpolation for minification and magnification; use point sampling for mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinMagLinearMipPoint = D3D11_FILTER_COMPARISON_MIN_MAG_LINEAR_MIP_POINT,

			/// <summary>
			/// Use linear interpolation for minification, magnification, and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinMagMipLinear = D3D11_FILTER_COMPARISON_MIN_MAG_MIP_LINEAR,

			/// <summary>
			/// Use anisotropic interpolation for minification, magnification, and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonAnisotropic = D3D11_FILTER_COMPARISON_ANISOTROPIC
		};
		
		/// <summary>Specifies possible texture filtering flags.</summary>
		/// <unmanaged>D3DX11_FILTER_FLAG</unmanaged>
		[System::Flags]
		public enum class FilterFlags : System::Int32
		{
			/// <summary>
			/// No scaling or filtering will take place. Pixels outside the bounds of the source image are assumed to be transparent black.
			/// </summary>
			None = D3DX11_FILTER_NONE,

			/// <summary>
			/// Each destination pixel is computed by sampling the nearest pixel from the source image.
			/// </summary>
			Point = D3DX11_FILTER_POINT,

			/// <summary>
			/// Each destination pixel is computed by sampling the four nearest pixels from the source image. This filter
			/// works best when the scale on both axes is less than two.
			/// </summary>
			Linear = D3DX11_FILTER_LINEAR,

			/// <summary>
			/// Every pixel in the source image contributes equally to the destination image. This is the slowest of the filters.
			/// </summary>
			Triangle = D3DX11_FILTER_TRIANGLE,

			/// <summary>
			/// Each pixel is computed by averaging a 2x2(x2) box of pixels from the source image. This filter works only when the
			/// dimensions of the destination are half those of the source, as is the case with mipmaps.
			/// </summary>
			Box = D3DX11_FILTER_BOX,

			/// <summary>
			/// Pixels off the edge of the texture on the u-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorU = D3DX11_FILTER_MIRROR_U,

			/// <summary>
			/// Pixels off the edge of the texture on the v-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorV = D3DX11_FILTER_MIRROR_V,

			/// <summary>
			/// Pixels off the edge of the texture on the w-axis should be mirrored, not wrapped.
			/// </summary>
			MirrowW = D3DX11_FILTER_MIRROR_W,

			/// <summary>
			/// Combines the MirrorU, MirrorV, and MirrorW flags.
			/// </summary>
			Mirror = D3DX11_FILTER_MIRROR,

			/// <summary>
			/// The resulting image must be dithered using a 4x4 ordered dither algorithm. This happens when converting from one format to another.
			/// </summary>
			Dither = D3DX11_FILTER_DITHER,

			/// <summary>
			/// Do diffuse dithering on the image when changing from one format to another.
			/// </summary>
			DitherDiffusion = D3DX11_FILTER_DITHER_DIFFUSION,

			/// <summary>
			/// Input data is in standard RGB (sRGB) color space.
			/// </summary>
			StandardRgbIn = D3DX11_FILTER_SRGB_IN,

			/// <summary>
			/// Output data is in standard RGB (sRGB) color space.
			/// </summary>
			StandardRgbOut = D3DX11_FILTER_SRGB_OUT,

			/// <summary>
			/// Combines the StandardRgbIn and StandardRgbOut flags.
			/// </summary>
			StandardRgb = D3DX11_FILTER_SRGB
		};
		
		/// <summary>Identifies which resources are supported for a given format and given device.</summary>
		/// <unmanaged>D3D11_FORMAT_SUPPORT</unmanaged>
		[System::Flags]
		public enum class FormatSupport : System::Int32
		{
			/// <summary>
			/// No features are supported.
			/// </summary>
			None = 0,

			/// <summary>
			/// Buffer resources are supported.
			/// </summary>
			Buffer = D3D11_FORMAT_SUPPORT_BUFFER,

			/// <summary>
			/// Vertex buffers are supported.
			/// </summary>
			VertexBuffer = D3D11_FORMAT_SUPPORT_IA_VERTEX_BUFFER,

			/// <summary>
			/// Index buffers are supported.
			/// </summary>
			IndexBuffer = D3D11_FORMAT_SUPPORT_IA_INDEX_BUFFER,

			/// <summary>
			/// Streaming output buffers are supported.
			/// </summary>
			StreamOutputBuffer = D3D11_FORMAT_SUPPORT_SO_BUFFER,

			/// <summary>
			/// 1D textures are supported.
			/// </summary>
			Texture1D = D3D11_FORMAT_SUPPORT_TEXTURE1D,

			/// <summary>
			/// 2D textures are supported.
			/// </summary>
			Texture2D = D3D11_FORMAT_SUPPORT_TEXTURE2D,

			/// <summary>
			/// 3D textures are supported.
			/// </summary>
			Texture3D = D3D11_FORMAT_SUPPORT_TEXTURE3D,

			/// <summary>
			/// Cube textures are supported.
			/// </summary>
			TextureCube = D3D11_FORMAT_SUPPORT_TEXTURECUBE,

			/// <summary>
			/// The intrinsic HLSL "load" function is supported.
			/// </summary>
			ShaderLoadIntrinsic = D3D11_FORMAT_SUPPORT_SHADER_LOAD,

			/// <summary>
			/// The intrinsic HLSL "sample" function is supported.
			/// </summary>
			ShaderSampleIntrinsic = D3D11_FORMAT_SUPPORT_SHADER_SAMPLE,

			/// <summary>
			/// The intrinsic HLSL "samplecmp" and "samplecmplevelzero" are supported.
			/// </summary>
			ShaderSampleComparisonIntrinsic = D3D11_FORMAT_SUPPORT_SHADER_SAMPLE_COMPARISON,

			/// <summary>
			/// Mipmaps are supported.
			/// </summary>
			MipMap = D3D11_FORMAT_SUPPORT_MIP,

			/// <summary>
			/// Automatic generation of mipmaps is supported.
			/// </summary>
			MipMapAutoGeneration = D3D11_FORMAT_SUPPORT_MIP_AUTOGEN,

			/// <summary>
			/// Rendertargets are supported.
			/// </summary>
			RenderTarget = D3D11_FORMAT_SUPPORT_RENDER_TARGET,

			/// <summary>
			/// Blend operations are supported.
			/// </summary>
			BlendOperation = D3D11_FORMAT_SUPPORT_BLENDABLE,

			/// <summary>
			/// Depth-stencil surfaces are supported.
			/// </summary>
			DepthStencil = D3D11_FORMAT_SUPPORT_DEPTH_STENCIL,

			/// <summary>
			/// CPU locking is supported.
			/// </summary>
			CpuLocking = D3D11_FORMAT_SUPPORT_CPU_LOCKABLE,

			/// <summary>
			/// Multisampling resolution is supported.
			/// </summary>
			MultisampleResolve = D3D11_FORMAT_SUPPORT_MULTISAMPLE_RESOLVE,

			/// <summary>
			/// The format can be displayed on screen.
			/// </summary>
			FormatDisplaySupport = D3D11_FORMAT_SUPPORT_DISPLAY,

			/// <summary>
			/// The format can be cast to another format.
			/// </summary>
			FormatCastSupport = D3D11_FORMAT_SUPPORT_CAST_WITHIN_BIT_LAYOUT,

			/// <summary>
			/// The format can be used as a multisample render target.
			/// </summary>
			FormatMultisampleRenderTargetSupport = D3D11_FORMAT_SUPPORT_MULTISAMPLE_RENDERTARGET,

			/// <summary>
			/// The format can be used as a multisample texture and read into a shader with the HLSL "load" function.
			/// </summary>
			FormatMultisampleLoadSupport = D3D11_FORMAT_SUPPORT_MULTISAMPLE_LOAD,

			/// <summary>
			/// The intrinsic HLSL "gather" function is supported. Available in Direct3D 10.1 or higher.
			/// </summary>
			ShaderGatherIntrinsic = D3D11_FORMAT_SUPPORT_SHADER_GATHER,

			/// <summary>
			/// The format supports casting when the resource is used as a back buffer.
			/// </summary>
			BackBufferCast = D3D11_FORMAT_SUPPORT_BACK_BUFFER_CAST,

			/// <summary>
			/// The format can be used for an unordered access view.
			/// </summary>
			UnorderedAccessView = D3D11_FORMAT_SUPPORT_TYPED_UNORDERED_ACCESS_VIEW,

			/// <summary>
			/// The format can be used with the HLSL "gather" and "compare" functions.
			/// </summary>
			ShaderGatherComparisonIntrinsic = D3D11_FORMAT_SUPPORT_SHADER_GATHER_COMPARISON
		};
		
		/// <summary>Specifies image file formats supported by runtime.</summary>
		/// <unmanaged>D3DX11_IMAGE_FILE_FORMAT</unmanaged>
		public enum class ImageFileFormat : System::Int32
		{
			/// <summary>
			/// Windows bitmap (BMP) file format. Contains a header that describes the resolution of the device on which the
			/// rectangle of pixels was created, the dimensions of the rectangle, the size of the array of bits, a logical palette,
			/// and an array of bits that defines the relationship between pixels in the bitmapped image and entries in the logical palette.
			/// </summary>
			Bmp = D3DX11_IFF_BMP,

			/// <summary>
			/// Joint Photographic Experts Group (JPEG) compressed file format. Specifies variable compression of 24-bit RGB color
			/// and 8-bit gray-scale Tagged Image File Format (TIFF) image document files.
			/// </summary>
			Jpg = D3DX11_IFF_JPG,

			/// <summary>
			/// Portable Network Graphics (PNG) file format. A non-proprietary bitmap format using lossless compression.
			/// </summary>
			Png = D3DX11_IFF_PNG,

			/// <summary>
			/// DirectDraw surface (DDS) file format. Stores textures, volume textures, and cubic environment maps, with or without
			/// mipmap levels, and with or without pixel compression.
			/// </summary>
			Dds = D3DX11_IFF_DDS,

			/// <summary>
			/// Tagged Image File Format (TIFF).
			/// </summary>
			Tiff = D3DX11_IFF_TIFF,

			/// <summary>
			/// Graphics Interchange Format (GIF).
			/// </summary>
			Gif = D3DX11_IFF_GIF,

			/// <summary>
			/// Windows Media Player format (WMP).
			/// </summary>
			Wmp = D3DX11_IFF_WMP
		};
		
		/// <summary>Specifies possible types of data contained in an input slot.</summary>
		/// <unmanaged>D3D11_INPUT_CLASSIFICATION</unmanaged>
		public enum class InputClassification : System::Int32
		{
			/// <summary>
			/// Input data is per-vertex data.
			/// </summary>
			PerVertexData = D3D11_INPUT_PER_VERTEX_DATA,

			/// <summary>
			/// Input data is per-instance data.
			/// </summary>
			PerInstanceData = D3D11_INPUT_PER_INSTANCE_DATA
		};
		
		/// <summary>Specifies how the CPU should respond when Map() is called on a resource being used by the GPU.</summary>
		/// <unmanaged>D3D11_MAP_FLAG</unmanaged>
		[System::Flags]
		public enum class MapFlags : System::Int32
		{
			/// <summary>
			/// Wait for the resource to become available.
			/// </summary>
			None = 0,

			/// <summary>
			/// Do not wait for the resource to become available. The map method will return WasStillRendering when the GPU blocks the CPU from
			/// accessing a resource.
			/// </summary>
			DoNotWait = D3D11_MAP_FLAG_DO_NOT_WAIT
		};
		
		/// <summary>Identifies modes in which a resource can be accessed by the CPU.</summary>
		/// <unmanaged>D3D11_MAP</unmanaged>
		public enum class MapMode : System::Int32
		{
			/// <summary>
			/// Resource is mapped for reading. The resource must have been created with read access.
			/// </summary>
			Read = D3D11_MAP_READ,

			/// <summary>
			/// Resource is mapped for writing. The resource must have been created with write access.
			/// </summary>
			Write = D3D11_MAP_WRITE,

			/// <summary>
			/// Resource is mapped for reading and writing. The resource must have been created with read and write access.
			/// </summary>
			ReadWrite = D3D11_MAP_READ_WRITE,

			/// <summary>
			/// Resource is mapped for writing; the previous contents of the resource will be undefined. The resource must have been created with write access.
			/// </summary>
			WriteDiscard = D3D11_MAP_WRITE_DISCARD,

			/// <summary>
			/// Resource is mapped for writing; the existing contents of the resource cannot be overwritten. This flag is only valid on
			/// vertex and index buffers. The resource must have been created with write access.
			/// </summary>
			WriteNoOverwrite = D3D11_MAP_WRITE_NO_OVERWRITE
		};

		/// <summary>
		/// Specifies various categories of debug messages.
		/// </summary>
		/// <unmanaged>D3D11_MESSAGE_CATEGORY</unmanaged>
		public enum class MessageCategory : System::Int32
		{
			/// <summary>
			/// The message is a custom application-defined message.
			/// </summary>
			ApplicationDefined = D3D11_MESSAGE_CATEGORY_APPLICATION_DEFINED,

			/// <summary>
			/// The message is contains miscellaneous information.
			/// </summary>
			Miscellaneous = D3D11_MESSAGE_CATEGORY_MISCELLANEOUS,

			/// <summary>
			/// The message pertains to initialization.
			/// </summary>
			Initialization = D3D11_MESSAGE_CATEGORY_INITIALIZATION,

			/// <summary>
			/// The message pertains to cleanup.
			/// </summary>
			Cleanup = D3D11_MESSAGE_CATEGORY_CLEANUP,

			/// <summary>
			/// The message pertains to compilation.
			/// </summary>
			Compilation = D3D11_MESSAGE_CATEGORY_COMPILATION,

			/// <summary>
			/// The message pertains to state creation.
			/// </summary>
			StateCreation = D3D11_MESSAGE_CATEGORY_STATE_CREATION,

			/// <summary>
			/// The message pertains to the setting of state.
			/// </summary>
			StateSetting = D3D11_MESSAGE_CATEGORY_STATE_SETTING,

			/// <summary>
			/// The message pertains to the getting of state.
			/// </summary>
			StateGetting = D3D11_MESSAGE_CATEGORY_STATE_GETTING,

			/// <summary>
			/// The message pertains to resource manipulation.
			/// </summary>
			ResourceManipulation = D3D11_MESSAGE_CATEGORY_RESOURCE_MANIPULATION,

			/// <summary>
			/// The message pertains to application execution.
			/// </summary>
			Execution = D3D11_MESSAGE_CATEGORY_EXECUTION
		};

		/// <summary>
		/// Specifies debug message severity levels for an information queue.
		/// </summary>
		/// <unmanaged>D3D11_MESSAGE_SEVERITY</unmanaged>
		public enum class MessageSeverity : System::Int32
		{
			/// <summary>
			/// The message indicates that some type of corruption has occured.
			/// </summary>
			Corruption = D3D11_MESSAGE_SEVERITY_CORRUPTION,

			/// <summary>
			/// The message indicates that an error has occurred.
			/// </summary>
			Error = D3D11_MESSAGE_SEVERITY_ERROR,

			/// <summary>
			/// The message indicates that a warning has occurred.
			/// </summary>
			Warning = D3D11_MESSAGE_SEVERITY_WARNING,

			/// <summary>
			/// The message contains generic debug information.
			/// </summary>
			Info = D3D11_MESSAGE_SEVERITY_INFO
		};

		/// <summary>
		/// Normal maps generation constants.
		/// </summary>
		/// <unmanaged>D3DX11_NORMALMAP_FLAG</unmanaged>
		[System::Flags]
		public enum class NormalMapFlags : System::Int32
		{
			/// <summary>
			/// Indicates that pixels off the edge of the texture on the u-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorU = D3DX11_NORMALMAP_MIRROR_U,

			/// <summary>
			/// Indicates that pixels off the edge of the texture on the v-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorV = D3DX11_NORMALMAP_MIRROR_V,

			/// <summary>
			/// Same as specifying the MirrorU and MirrorV flags.
			/// </summary>
			Mirror = D3DX11_NORMALMAP_MIRROR,

			/// <summary>
			/// Inverts the direction of each normal.
			/// </summary>
			InvertSign = D3DX11_NORMALMAP_INVERTSIGN,

			/// <summary>
			/// Computes the per-pixel occlusion term and encodes it into the alpha. An alpha of 1 means that the
			/// pixel is not obscured in any way, and an alpha of 0 means that the pixel is completely obscured.
			/// </summary>
			ComputeOcclusion = D3DX11_NORMALMAP_COMPUTE_OCCLUSION
		};
		
		/// <summary>Specifies how the pipeline should interpret vertex data bound to the input assembler stage.</summary>
		/// <unmanaged>D3D11_PRIMITIVE_TOPOLOGY</unmanaged>
		public enum class PrimitiveTopology : System::Int32
		{
			/// <summary>
			/// The IA stage has not been initialized with a primitive topology. The IA stage will not function properly unless a primitive topology is defined.
			/// </summary>
			Undefined = D3D11_PRIMITIVE_TOPOLOGY_UNDEFINED,

			/// <summary>
			/// Interpret the vertex data as a list of points.
			/// </summary>
			PointList = D3D11_PRIMITIVE_TOPOLOGY_POINTLIST,

			/// <summary>
			/// Interpret the vertex data as a list of lines.
			/// </summary>
			LineList = D3D11_PRIMITIVE_TOPOLOGY_LINELIST,

			/// <summary>
			/// Interpret the vertex data as a line strip.
			/// </summary>
			LineStrip = D3D11_PRIMITIVE_TOPOLOGY_LINESTRIP,

			/// <summary>
			/// Interpret the vertex data as a list of triangles.
			/// </summary>
			TriangleList = D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST,

			/// <summary>
			/// Interpret the vertex data as a triangle strip.
			/// </summary>
			TriangleStrip = D3D11_PRIMITIVE_TOPOLOGY_TRIANGLESTRIP,

			/// <summary>
			/// Interpret the vertex data as list of lines with adjacency data.
			/// </summary>
			LineListWithAdjacency = D3D11_PRIMITIVE_TOPOLOGY_LINELIST_ADJ,

			/// <summary>
			/// Interpret the vertex data as line strip with adjacency data.
			/// </summary>
			LineStripWithAdjacency = D3D11_PRIMITIVE_TOPOLOGY_LINESTRIP_ADJ,

			/// <summary>
			/// Interpret the vertex data as list of triangles with adjacency data.
			/// </summary>
			TriangleListWithAdjacency = D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST_ADJ,

			/// <summary>
			/// Interpret the vertex data as triangle strip with adjacency data.
			/// </summary>
			TriangleStripWithAdjacency = D3D11_PRIMITIVE_TOPOLOGY_TRIANGLESTRIP_ADJ,

			/// <summary>
			/// Interpret the vertex data as a patch list with 1 control point.
			/// </summary>
			PatchListWith1ControlPoint = D3D11_PRIMITIVE_TOPOLOGY_1_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 2 control points.
			/// </summary>
			PatchListWith2ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_2_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 3 control points.
			/// </summary>
			PatchListWith3ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_3_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 4 control points.
			/// </summary>
			PatchListWith4ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_4_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 5 control points.
			/// </summary>
			PatchListWith5ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_5_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 6 control points.
			/// </summary>
			PatchListWith6ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_6_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 7 control points.
			/// </summary>
			PatchListWith7ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_7_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 8 control points.
			/// </summary>
			PatchListWith8ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_8_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 9 control points.
			/// </summary>
			PatchListWith9ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_9_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 10 control points.
			/// </summary>
			PatchListWith10ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_10_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 11 control points.
			/// </summary>
			PatchListWith11ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_11_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 12 control points.
			/// </summary>
			PatchListWith12ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_12_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 13 control points.
			/// </summary>
			PatchListWith13ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_13_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 14 control points.
			/// </summary>
			PatchListWith14ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_14_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 15 control points.
			/// </summary>
			PatchListWith15ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_15_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 16 control points.
			/// </summary>
			PatchListWith16ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_16_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 17 control points.
			/// </summary>
			PatchListWith17ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_17_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 18 control points.
			/// </summary>
			PatchListWith18ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_18_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 19 control points.
			/// </summary>
			PatchListWith19ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_19_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 20 control points.
			/// </summary>
			PatchListWith20ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_20_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 21 control points.
			/// </summary>
			PatchListWith21ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_21_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 22 control points.
			/// </summary>
			PatchListWith22ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_22_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 23 control points.
			/// </summary>
			PatchListWith23ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_23_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 24 control points.
			/// </summary>
			PatchListWith24ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_24_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 25 control points.
			/// </summary>
			PatchListWith25ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_25_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 26 control points.
			/// </summary>
			PatchListWith26ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_26_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 27 control points.
			/// </summary>
			PatchListWith27ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_27_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 28 control points.
			/// </summary>
			PatchListWith28ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_28_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 29 control points.
			/// </summary>
			PatchListWith29ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_29_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 30 control points.
			/// </summary>
			PatchListWith30ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_30_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 31 control points.
			/// </summary>
			PatchListWith31ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_31_CONTROL_POINT_PATCHLIST,

			/// <summary>
			/// Interpret the vertex data as a patch list with 32 control points.
			/// </summary>
			PatchListWith32ControlPoints = D3D11_PRIMITIVE_TOPOLOGY_32_CONTROL_POINT_PATCHLIST
		};
		
		/// <summary>
		/// Specifies flags for describing query options and behavior.
		/// </summary>
		/// <unmanaged>D3D11_QUERY_MISC_FLAG</unmanaged>
		[System::Flags]
		public enum class QueryFlags : System::Int32
		{
			/// <summary>
			/// No particular flags specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Tell the hardware that if it is not yet sure if something is hidden or not to draw it anyway. This is only used with
			/// an occlusion predicate. Predication data cannot be returned to your application
			/// via <see cref="DeviceContext">DeviceContext.GetData</see> when using this flag.
			/// </summary>
			PredicateHint = D3D11_QUERY_MISC_PREDICATEHINT,
		};
		
		/// <summary>
		/// Defines possible query types.
		/// </summary>
		/// <unmanaged>D3D11_QUERY</unmanaged>
		public enum class QueryType : System::Int32
		{
			/// <summary>
			/// Determines whether or not the GPU is finished processing commands. When the GPU is finished processing commands
			/// <see cref="DeviceContext">DeviceContext.GetData</see> will return success, and the result will be a boolean with a value of <c>true</c>.
			/// When using this type of query, DeviceContext.Begin is disabled.
			/// </summary>
			Event = D3D11_QUERY_EVENT,

			/// <summary>
			/// Get the number of samples that passed the depth and stencil tests in between DeviceContext.Begin and DeviceContext.End.
			/// DeviceContext.GetData returns a long. If a depth or stencil test is disabled, then each of those tests will be counted as a pass.
			/// </summary>
			Occlusion = D3D11_QUERY_OCCLUSION,

			/// <summary>
			/// Get a timestamp value where DeviceContext.GetData returns a long. This kind of query is only useful if two timestamp queries
			/// are done in the middle of a TimestampDisjoint query. The difference of two timestamps can be used to determine how many
			/// ticks have elapsed, and the TimestampDisjoint query will determine if that difference is a reliable value and also
			/// has a value that shows how to convert the number of ticks into seconds. When using this type of query,
			/// DeviceContext.Begin is disabled. 
			/// </summary>
			Timestamp = D3D11_QUERY_TIMESTAMP,

			/// <summary>
			/// Determines whether or not a Timestamp query is returning reliable values, and also gives the frequency of the processor
			/// enabling you to convert the number of elapsed ticks into seconds. DeviceContext.GetData will return a <see cref="TimestampQueryData"/> instance.
			/// This type of query should only be invoked once per frame or less. 
			/// </summary>
			TimestampDisjoint = D3D11_QUERY_TIMESTAMP_DISJOINT,

			/// <summary>
			/// Get pipeline statistics, such as the number of pixel shader invocations in between DeviceContext.Begin and DeviceContext.End.
			/// DeviceContext.GetData will return a <see cref="PipelineStatistics"/> instance.
			/// </summary>
			PipelineStatistics = D3D11_QUERY_PIPELINE_STATISTICS,

			/// <summary>
			/// Similar to an occlusion query, except DeviceContext.GetData returns a boolean indicating whether or not any samples passed
			// the depth and stencil tests; <c>true</c> meaning at least one passed, and <c>false</c> meaning none passed.
			/// </summary>
			Predicate = D3D11_QUERY_OCCLUSION_PREDICATE,

			/// <summary>
			/// Get streaming output statistics, such as the number of primitives streamed out in between DeviceContext.Begin and DeviceContext.End.
			/// DeviceContext.GetData will return a <see cref="StreamOutputStatistics"/> structure.
			/// </summary>
			StreamOutputStatistics = D3D11_QUERY_SO_STATISTICS,

			/// <summary>
			/// Determines whether or not any of the streaming output buffers overflowed in between DeviceContext.Begin and DeviceContext.End.
			/// DeviceContext.GetData returns a boolean; <c>true</c> meaning there was an overflow, and <c>false</c> meaning there was not an overflow.
			/// If streaming output writes to multiple buffers, and one of the buffers overflows, then it will stop writing to all the output buffers.
			/// When an overflow is detected by Direct3D it is prevented from happening - no memory is corrupted. This predication may be used
			/// in conjunction with a StreamOutputStatistics query so that when an overflow occurs the query will let the application know how
			/// much memory was needed to prevent an overflow.
			/// </summary>
			StreamOutputOverflowPredicate = D3D11_QUERY_SO_OVERFLOW_PREDICATE,

			StreamOutputStatisticsStream0 = D3D11_QUERY_SO_STATISTICS_STREAM0,
			StreamOutputOverflowPredicateStream0 = D3D11_QUERY_SO_OVERFLOW_PREDICATE_STREAM0,
			StreamOutputStatisticsStream1 = D3D11_QUERY_SO_STATISTICS_STREAM1,
			StreamOutputOverflowPredicateStream1 = D3D11_QUERY_SO_OVERFLOW_PREDICATE_STREAM1,
			StreamOutputStatisticsStream2 = D3D11_QUERY_SO_STATISTICS_STREAM2,
			StreamOutputOverflowPredicateStream2 = D3D11_QUERY_SO_OVERFLOW_PREDICATE_STREAM2,
			StreamOutputStatisticsStream3 = D3D11_QUERY_SO_STATISTICS_STREAM3,
			StreamOutputOverflowPredicateStream3 = D3D11_QUERY_SO_OVERFLOW_PREDICATE_STREAM3
		};
		
		/// <summary>Identifies the type of resource that will be viewed as a render target.</summary>
		/// <unmanaged>D3D11_RTV_DIMENSION</unmanaged>
		public enum class RenderTargetViewDimension : System::Int32
		{
			/// <summary>
			/// The resource will be accessed as a buffer.
			/// </summary>
			Buffer = D3D11_RTV_DIMENSION_BUFFER,

			/// <summary>
			/// The resource will be accessed as a 1D texture.
			/// </summary>
			Texture1D = D3D11_RTV_DIMENSION_TEXTURE1D,

			/// <summary>
			/// The resource will be accessed as an array of 1D textures.
			/// </summary>
			Texture1DArray = D3D11_RTV_DIMENSION_TEXTURE1DARRAY,

			/// <summary>
			/// The resource will be accessed as a 2D texture.
			/// </summary>
			Texture2D = D3D11_RTV_DIMENSION_TEXTURE2D,

			/// <summary>
			/// The resource will be accessed as an array of 2D textures.
			/// </summary>
			Texture2DArray = D3D11_RTV_DIMENSION_TEXTURE2DARRAY,

			/// <summary>
			/// The resource will be accessed as a 2D texture with multisampling.
			/// </summary>
			Texture2DMultisampled = D3D11_RTV_DIMENSION_TEXTURE2DMS,

			/// <summary>
			/// The resource will be accessed as an array of 2D textures with multisampling.
			/// </summary>
			Texture2DMultisampledArray = D3D11_RTV_DIMENSION_TEXTURE2DMSARRAY,

			/// <summary>
			/// The resource will be accessed as a 3D texture.
			/// </summary>
			Texture3D = D3D11_RTV_DIMENSION_TEXTURE3D
		};

		/// <summary>
		/// Options for the amount of information returned about live device objects.
		/// </summary>
		/// <unmanaged>D3D11_RLDO_FLAGS</unmanaged>
		public enum class ReportingLevel : System::Int32
		{
			/// <summary>
			/// Reports only a summary of the device information.
			/// </summary>
			Summary = D3D11_RLDO_SUMMARY,

			/// <summary>
			/// Gives a detailed report of the device information.
			/// </summary>
			Detail = D3D11_RLDO_DETAIL
		};
		
		/// <summary>Identifies the type of resource being used.</summary>
		/// <unmanaged>D3D11_RESOURCE_DIMENSION</unmanaged>
		public enum class ResourceDimension : System::Int32
		{
			/// <summary>
			/// Resource is of unknown type.
			/// </summary>
			Unknown = D3D11_RESOURCE_DIMENSION_UNKNOWN,

			/// <summary>
			/// Resource is a buffer.
			/// </summary>
			Buffer = D3D11_RESOURCE_DIMENSION_BUFFER,

			/// <summary>
			/// Resource is a 1D texture.
			/// </summary>
			Texture1D = D3D11_RESOURCE_DIMENSION_TEXTURE1D,

			/// <summary>
			/// Resource is a 2D texture.
			/// </summary>
			Texture2D = D3D11_RESOURCE_DIMENSION_TEXTURE2D,

			/// <summary>
			/// Resource is a 3D texture.
			/// </summary>
			Texture3D = D3D11_RESOURCE_DIMENSION_TEXTURE3D
		};
		
		/// <summary>Identifies other, less common options for resources.</summary>
		/// <unmanaged>D3D11_RESOURCE_MISC_FLAG</unmanaged>
		[System::Flags]
		public enum class ResourceOptionFlags : System::Int32
		{
			/// <summary>
			/// No additional options specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Enables mipmap generation on a texture resource. The resource must be created with the bind flags that specify that the
			/// resource is a render target and a shader resource.
			/// </summary>
			GenerateMipMaps = D3D11_RESOURCE_MISC_GENERATE_MIPS,

			/// <summary>
			/// Enables resource data sharing between two or more Direct3D devices. The only resources that can be shared are 2D non-mipmapped textures.
			/// </summary>
			Shared = D3D11_RESOURCE_MISC_SHARED,

			/// <summary>
			/// Enables a resource to be a cube texture created from a Texture2DArray that contains 6 textures.
			/// </summary>
			TextureCube = D3D11_RESOURCE_MISC_TEXTURECUBE,

			/// <summary>
			/// Enables instancing of GPU-generated content.
			/// </summary>
			DrawIndirect = D3D11_RESOURCE_MISC_DRAWINDIRECT_ARGS,

			/// <summary>
			/// Enables a resource as a byte address buffer.
			/// </summary>
			RawBuffer = D3D11_RESOURCE_MISC_BUFFER_ALLOW_RAW_VIEWS,

			/// <summary>
			/// Enables a resource as a structured buffer.
			/// </summary>
			StructuredBuffer = D3D11_RESOURCE_MISC_BUFFER_STRUCTURED,

			/// <summary>
			/// Enables a resource with a clamped depth bias.
			/// </summary>
			ClampedResource = D3D11_RESOURCE_MISC_RESOURCE_CLAMP,

			/// <summary>
			/// Enables a resource as a keyed mutex.
			/// </summary>
			KeyedMutex = D3D11_RESOURCE_MISC_SHARED_KEYEDMUTEX,

			/// <summary>
			/// Renders a resource compatible with GDI.
			/// </summary>
			GdiCompatible = D3D11_RESOURCE_MISC_GDI_COMPATIBLE
		};

		/// <summary>
		/// Identifies expected resource use during rendering. The usage directly reflects whether a resource is 
		/// accessible by the CPU and/or the GPU.
		/// </summary>
		/// <unmanaged>D3D11_USAGE</unmanaged>
		public enum class ResourceUsage : System::Int32
		{
			/// <summary>
			/// A resource that requires read and write access by the GPU. This is likely to be the most common usage choice.
			/// </summary>
			Default = D3D11_USAGE_DEFAULT,

			/// <summary>
			/// A resource that can only be read by the GPU. It cannot be written by the GPU, and cannot be accessed at all by the CPU.
			/// This type of resource must be initialized when it is created, since it cannot be changed after creation.
			/// </summary>
			Immutable = D3D11_USAGE_IMMUTABLE,

			/// <summary>
			/// A resource that is accessible by both the GPU (read only) and the CPU (write only). A dynamic resource is a good choice
			/// for a resource that will be updated by the CPU at least once per frame. There are two ways to update a dynamic resource:
			/// if your data is laid exactly the way the resource stores it, use DeviceContext.UpdateSubresource, otherwise, use a Map method.
			/// </summary>
			Dynamic = D3D11_USAGE_DYNAMIC,

			/// <summary>
			/// A resource that supports data transfer (copy) from the GPU to the CPU.
			/// </summary>
			Staging = D3D11_USAGE_STAGING
		};

		/// <summary>Identifies the type for scan data.</summary>
		/// <unmanaged>D3DX11_SCAN_DATA_TYPE</unmanaged>
		public enum class ScanDataType : System::Int32
		{
			/// <summary>
			/// The type of the scan is float.
			/// </summary>
			Float = D3DX11_SCAN_DATA_TYPE_FLOAT,

			/// <summary>
			/// The type of the scan is int.
			/// </summary>
			Int = D3DX11_SCAN_DATA_TYPE_INT,

			/// <summary>
			/// The type of the scan is uint.
			/// </summary>
			UInt = D3DX11_SCAN_DATA_TYPE_UINT
		};

		/// <summary>Identifies the direction of scanning.</summary>
		/// <unmanaged>D3DX11_SCAN_DIRECTION</unmanaged>
		public enum class ScanDirection : System::Int32
		{
			/// <summary>
			/// The direction is forward.
			/// </summary>
			Forward = D3DX11_SCAN_DIRECTION_FORWARD,

			/// <summary>
			/// The direction is backward.
			/// </summary>
			Backward = D3DX11_SCAN_DIRECTION_BACKWARD
		};

		/// <summary>Identifies the operation for a scan.</summary>
		/// <unmanaged>D3DX11_SCAN_OPCODE</unmanaged>
		public enum class ScanOpCode : System::Int32
		{
			/// <summary>
			/// The operation is add.
			/// </summary>
			Add = D3DX11_SCAN_OPCODE_ADD,

			/// <summary>
			/// The operation is min.
			/// </summary>
			Min = D3DX11_SCAN_OPCODE_MIN,

			/// <summary>
			/// The operation is max.
			/// </summary>
			Max = D3DX11_SCAN_OPCODE_MAX,

			/// <summary>
			/// The operation is mul.
			/// </summary>
			Mul = D3DX11_SCAN_OPCODE_MUL,

			/// <summary>
			/// The operation is and.
			/// </summary>
			And = D3DX11_SCAN_OPCODE_AND,

			/// <summary>
			/// The operation is or.
			/// </summary>
			Or = D3DX11_SCAN_OPCODE_OR,

			/// <summary>
			/// The operation is XOR.
			/// </summary>
			XOR = D3DX11_SCAN_OPCODE_XOR
		};
		
		/// <summary>Identifies the type of the resource that will be viewed.</summary>
		/// <unmanaged>D3D11_SRV_DIMENSION</unmanaged>
		public enum class ShaderResourceViewDimension : System::Int32
		{
			/// <summary>
			/// The type of the resource is unknown.
			/// </summary>
			Unknown = D3D11_SRV_DIMENSION_UNKNOWN,

			/// <summary>
			/// The resource will be accessed as a buffer.
			/// </summary>
			Buffer = D3D11_SRV_DIMENSION_BUFFER,

			/// <summary>
			/// The resource will be accessed as a 1D texture.
			/// </summary>
			Texture1D = D3D11_SRV_DIMENSION_TEXTURE1D,

			/// <summary>
			/// The resource will be accessed as an array of 1D textures.
			/// </summary>
			Texture1DArray = D3D11_SRV_DIMENSION_TEXTURE1DARRAY,

			/// <summary>
			/// The resource will be accessed as a 2D texture.
			/// </summary>
			Texture2D = D3D11_SRV_DIMENSION_TEXTURE2D,

			/// <summary>
			/// The resource will be accessed as an array of 2D textures.
			/// </summary>
			Texture2DArray = D3D11_SRV_DIMENSION_TEXTURE2DARRAY,

			/// <summary>
			/// The resource will be accessed as a 2D texture with multisampling.
			/// </summary>
			Texture2DMultisampled = D3D11_SRV_DIMENSION_TEXTURE2DMS,

			/// <summary>
			/// The resource will be accessed as an array of 2D textures with multisampling.
			/// </summary>
			Texture2DMultisampledArray = D3D11_SRV_DIMENSION_TEXTURE2DMSARRAY,

			/// <summary>
			/// The resource will be accessed as a 3D texture.
			/// </summary>
			Texture3D = D3D11_SRV_DIMENSION_TEXTURE3D,

			/// <summary>
			/// The resource will be accessed as a cube texture.
			/// </summary>
			TextureCube = D3D11_SRV_DIMENSION_TEXTURECUBE,

			/// <summary>
			/// The resource will be accessed as an array of cube textures.
			/// </summary>
			TextureCubeArray = D3D11_SRV_DIMENSION_TEXTURECUBEARRAY,

			/// <summary>
			/// The resource will be accessed as an extended buffer.
			/// </summary>
			ExtendedBuffer = D3D11_SRV_DIMENSION_BUFFEREX
		};

		/// <summary>Specifies shader resource view extended buffer options.</summary>
		/// <unmanaged>D3D11_BUFFEREX_SRV_FLAG</unmanaged>
		[System::Flags]
		public enum class ShaderResourceViewExtendedBufferFlags : System::Int32
		{
			/// <summary>
			/// No specific flags specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Resource contains raw, unstructured data.
			/// </summary>
			RawData = D3D11_BUFFEREX_SRV_FLAG_RAW
		};

		/// <summary>Specifies the stencil operations that can be performed during depth-stencil testing.</summary>
		/// <unmanaged>D3D11_STENCIL_OP</unmanaged>
		public enum class StencilOperation : System::Int32
		{
			/// <summary>
			/// Keep the existing stencil data.
			/// </summary>
			Keep = D3D11_STENCIL_OP_KEEP,

			/// <summary>
			/// Set the stencil data to 0.
			/// </summary>
			Zero = D3D11_STENCIL_OP_ZERO,

			/// <summary>
			/// Set the stencil data to the reference value.
			/// </summary>
			Replace = D3D11_STENCIL_OP_REPLACE,

			/// <summary>
			/// Increment the stencil value by 1, and clamp the result.
			/// </summary>
			IncrementAndClamp = D3D11_STENCIL_OP_INCR_SAT,

			/// <summary>
			/// Decrement the stencil value by 1, and clamp the result.
			/// </summary>
			DecrementAndClamp = D3D11_STENCIL_OP_DECR_SAT,

			/// <summary>
			/// Invert the stencil data.
			/// </summary>
			Invert = D3D11_STENCIL_OP_INVERT,

			/// <summary>
			/// Increment the stencil value by 1, and wrap the result if necessary.
			/// </summary>
			Increment = D3D11_STENCIL_OP_INCR,

			/// <summary>
			/// Decrement the stencil value by 1, and wrap the result if necessary.
			/// </summary>
			Decrement = D3D11_STENCIL_OP_DECR
		};

		/// <summary>Identifies techniques for resolving texture coordinates that are outside of the boundaries of a texture.</summary>
		/// <unmanaged>D3D11_TEXTURE_ADDRESS_MODE</unmanaged>
		public enum class TextureAddressMode : System::Int32
		{
			/// <summary>
			/// Tile the texture at every (u,v) integer junction. For example, for u values between 0 and 3, the texture is repeated three times.
			/// </summary>
			Wrap = D3D11_TEXTURE_ADDRESS_WRAP,

			/// <summary>
			/// Flip the texture at every (u,v) integer junction. For u values between 0 and 1, for example, the texture is addressed normally;
			/// between 1 and 2, the texture is flipped (mirrored); between 2 and 3, the texture is normal again; and so on.
			/// </summary>
			Mirror = D3D11_TEXTURE_ADDRESS_MIRROR,

			/// <summary>
			/// Texture coordinates outside the range [0.0, 1.0] are set to the texture color at 0.0 or 1.0, respectively.
			/// </summary>
			Clamp = D3D11_TEXTURE_ADDRESS_CLAMP,

			/// <summary>
			/// Texture coordinates outside the range [0.0, 1.0] are set to the border color.
			/// </summary>
			Border = D3D11_TEXTURE_ADDRESS_BORDER,

			/// <summary>
			/// Similar to Mirror and Clamp. Takes the absolute value of the texture coordinate (thus, mirroring around 0), and then
			/// clamps to the maximum value.
			/// </summary>
			MirrorOnce = D3D11_TEXTURE_ADDRESS_MIRROR_ONCE
		};

		/// <summary>Specifies unordered-access-view buffer options.</summary>
		/// <unmanaged>D3D11_BUFFER_UAV_FLAG</unmanaged>
		[System::Flags]
		public enum class UnorderedAccessViewBufferFlags : System::Int32
		{
			/// <summary>
			/// No specific flags specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Resource contains raw, unstructured data.
			/// </summary>
			RawData = D3D11_BUFFER_UAV_FLAG_RAW,

			/// <summary>
			/// Allow data to be appended to the end of the buffer.
			/// </summary>
			AllowAppend = D3D11_BUFFER_UAV_FLAG_APPEND,

			/// <summary>
			/// Adds a counter to the unordered-access-view buffer.
			/// </summary>
			Counter = D3D11_BUFFER_UAV_FLAG_COUNTER
		};

		/// <summary>Identifies the type of the resource that will be viewed.</summary>
		/// <unmanaged>D3D11_UAV_DIMENSION</unmanaged>
		public enum class UnorderedAccessViewDimension : System::Int32
		{
			/// <summary>
			/// The type of the resource is unknown.
			/// </summary>
			Unknown = D3D11_UAV_DIMENSION_UNKNOWN,

			/// <summary>
			/// The resource will be accessed as a buffer.
			/// </summary>
			Buffer = D3D11_UAV_DIMENSION_BUFFER,

			/// <summary>
			/// The resource will be accessed as a 1D texture.
			/// </summary>
			Texture1D = D3D11_UAV_DIMENSION_TEXTURE1D,

			/// <summary>
			/// The resource will be accessed as an array of 1D textures.
			/// </summary>
			Texture1DArray = D3D11_UAV_DIMENSION_TEXTURE1DARRAY,

			/// <summary>
			/// The resource will be accessed as a 2D texture.
			/// </summary>
			Texture2D = D3D11_UAV_DIMENSION_TEXTURE2D,

			/// <summary>
			/// The resource will be accessed as an array of 2D textures.
			/// </summary>
			Texture2DArray = D3D11_UAV_DIMENSION_TEXTURE2DARRAY,

			/// <summary>
			/// The resource will be accessed as a 3D texture.
			/// </summary>
			Texture3D = D3D11_UAV_DIMENSION_TEXTURE3D
		};
	}
}