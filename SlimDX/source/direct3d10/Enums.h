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

#include <windows.h>
#include <d3d10.h>
#include <d3dx10.h>

namespace SlimDX
{
#ifdef XMLDOCS
	namespace DXGI
	{
		ref class SwapChain;
	}
#endif

	namespace Direct3D10
	{
#ifdef XMLDOCS
		ref class Device;
		ref class Effect;
		ref class Sprite;
		ref class Debug;
#endif
	
		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.
		
		/// <summary>Optional flags that control the behavior of Asynchronous.GetData.</summary>
		/// <unmanaged>D3D10_ASYNC_GETDATA_FLAG</unmanaged>
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
			DoNotFlush = D3D10_ASYNC_GETDATA_DONOTFLUSH
		};
		
		/// <summary>Identifies how to bind a resource to the pipeline.</summary>
		/// <unmanaged>D3D10_BIND_FLAG</unmanaged>
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
			VertexBuffer = D3D10_BIND_VERTEX_BUFFER,
			
			/// <summary>
			/// Indicates the resource can be bound to the input-assembler stage as an index buffer 
			/// </summary>
			IndexBuffer = D3D10_BIND_INDEX_BUFFER,

			/// <summary>
			/// Indicates the resource can be bound to the shader stage as a constant buffer 
			/// </summary>
			ConstantBuffer = D3D10_BIND_CONSTANT_BUFFER,

			/// <summary>
			/// Indicates the resource can be bound to the shader stage as a buffer or texture. 
			/// Note that it is invalid to specify this flag and subsequently map the resource
			/// using MapMode.WriteNoOverwrite.
			/// </summary>
			ShaderResource = D3D10_BIND_SHADER_RESOURCE, 

			/// <summary>
			/// Indicates the resource can be bound to the stream-output stage as an output buffer. 
			/// </summary>
			StreamOutput = D3D10_BIND_STREAM_OUTPUT,

			/// <summary>
			/// Indicates the resource can be bound to the output-merger stage as a render target. 
			/// </summary>
			RenderTarget = D3D10_BIND_RENDER_TARGET,

			/// <summary>
			/// Indicates the resource can be bound to the output-merger stage as a depth-stencil target. 
			/// </summary>
			DepthStencil = D3D10_BIND_DEPTH_STENCIL
		};
		
		/// <summary>RGB or alpha blending operation.</summary>
		/// <unmanaged>D3D10_BLEND_OP</unmanaged>
		public enum class BlendOperation : System::Int32
		{
			/// <summary>
			/// Adds the source to the destination.
			/// </summary>
			Add = D3D10_BLEND_OP_ADD,
			
			/// <summary>
			/// Subtracts the source from the destination.
			/// </summary>
			Subtract = D3D10_BLEND_OP_SUBTRACT,
			
			/// <summary>
			/// Subtracts the destination from the source.
			/// </summary>
			ReverseSubtract = D3D10_BLEND_OP_REV_SUBTRACT,
			
			/// <summary>
			/// Takes the lesser of the source and destination values.
			/// </summary>
			Minimum = D3D10_BLEND_OP_MIN,
			
			/// <summary>
			/// Takes the greater of the source and destination values.
			/// </summary>
			Maximum = D3D10_BLEND_OP_MAX
		};
		
		/// <summary>Blend options. A blend option identifies the data source and an optional pre-blend operation.</summary>
		/// <unmanaged>D3D10_BLEND</unmanaged>
		public enum class BlendOption : System::Int32
		{
			/// <summary>
			/// The source is the color (0,0,0,0) with no pre-blend.
			/// </summary>
			Zero = D3D10_BLEND_ZERO,
			
			/// <summary>
			/// The source is the color (1,1,1,1) with no pre-blend.
			/// </summary>
			One = D3D10_BLEND_ONE,
			
			/// <summary>
			/// The source is the color (RGB) from a pixel shader with no pre-blend.
			/// </summary>
			SourceColor = D3D10_BLEND_SRC_COLOR,
			
			/// <summary>
			/// The source is the color (RGB) from a pixel shader with an inverting pre-blend (1 - RGB).
			/// </summary>
			InverseSourceColor = D3D10_BLEND_INV_SRC_COLOR,
			
			/// <summary>
			/// The source is the alpha (A) from a pixel shader with no pre-blend.
			/// </summary>
			SourceAlpha = D3D10_BLEND_SRC_ALPHA,
			
			/// <summary>
			/// The source is the alpha (A) from a pixel shader with an inverting pre-blend (1 - A).
			/// </summary>
			InverseSourceAlpha = D3D10_BLEND_INV_SRC_ALPHA,
			
			/// <summary>
			/// The source is the alpha (A) from a rendertarget with no pre-blend.
			/// </summary>
			DestinationAlpha = D3D10_BLEND_DEST_ALPHA,
			
			/// <summary>
			/// The source is the alpha (A) from a rendertarget with an inverting pre-blend (1 - A).
			/// </summary>
			InverseDestinationAlpha = D3D10_BLEND_INV_DEST_ALPHA,
			
			/// <summary>
			/// The source is the color (RGB) from a rendertarget with no pre-blend.
			/// </summary>
			DestinationColor = D3D10_BLEND_DEST_COLOR,
			
			/// <summary>
			/// The source is the color (RGB) from a rendertarget with an inverting pre-blend (1 - A).
			/// </summary>
			InverseDestinationColor = D3D10_BLEND_INV_DEST_COLOR,
			
			/// <summary>
			/// The source is the alpha (A) from a render target with a saturation pre-blend (clamping to 1 or less).
			/// </summary>
			SourceAlphaSaturate = D3D10_BLEND_SRC_ALPHA_SAT,
			
			/// <summary>
			/// The source is the blend factor (F) from the current blend state with no pre-blend operation.
			/// </summary>
			BlendFactor = D3D10_BLEND_BLEND_FACTOR,
			
			/// <summary>
			/// The source is the blend factor (F) from the current blend state with an inverting pre-blend (1 - F).
			/// </summary>
			InverseBlendFactor = D3D10_BLEND_INV_BLEND_FACTOR,
			
			/// <summary>
			/// The sources are both color (RGB) outputs from a pixel shader. There is no pre-blend operation. This option
			/// allows for dual-source blending.
			/// </summary>
			SecondarySourceColor = D3D10_BLEND_SRC1_COLOR,
			
			/// <summary>
			/// The sources are both color (RGB) outputs from a pixel shader. There is an inverting pre-blend (1 - RGB). This option
			/// allows for dual-source blending.
			/// </summary>
			InverseSecondarySourceColor = D3D10_BLEND_INV_SRC1_COLOR,
			
			/// <summary>
			/// The sources are both alpha (A) outputs from a pixel shader. There is no pre-blend. This option
			/// allows for dual-source blending.
			/// </summary>
			SecondarySourceAlpha = D3D10_BLEND_SRC1_ALPHA,
			
			/// <summary>
			/// The sources are both alpha (A) outputs from a pixel shader. There is an inverting pre-blend (1 - A). This option
			/// allows for dual-source blending.
			/// </summary>
			InverseSecondarySourceAlpha = D3D10_BLEND_INV_SRC1_ALPHA
		};

		/// <summary>
		/// The following flags are used to specify which channels in a texture to operate on.
		/// </summary>
		/// <unmanaged>D3DX10_CHANNEL_FLAG</unmanaged>
		[System::Flags]
		public enum class Channel : System::Int32
		{
			/// <summary>
			/// Indicates the red channel should be used.
			/// </summary>
			Red = D3DX10_CHANNEL_RED,

			/// <summary>
			/// Indicates the blue channel should be used.
			/// </summary>
			Blue = D3DX10_CHANNEL_BLUE,

			/// <summary>
			/// Indicates the green channel should be used.
			/// </summary>
			Green = D3DX10_CHANNEL_GREEN,

			/// <summary>
			/// Indicates the alpha channel should be used.
			/// </summary>
			Alpha = D3DX10_CHANNEL_ALPHA,

			/// <summary>
			/// Indicates the luminances of the red, green, and blue channels should be used.
			/// </summary>
			Luminance = D3DX10_CHANNEL_LUMINANCE
		};
		
		/// <summary>Identifies mask values for writing to a render target.</summary>
		/// <unmanaged>D3D10_COLOR_WRITE_ENABLE</unmanaged>
		[System::Flags]
		public enum class ColorWriteMaskFlags : System::Int32
		{
			/// <summary>
			/// Indicates that writes to the red component should be enabled.
			/// </summary>
			Red = D3D10_COLOR_WRITE_ENABLE_RED,
			
			/// <summary>
			/// Indicates that writes to the green component should be enabled.
			/// </summary>
			Green = D3D10_COLOR_WRITE_ENABLE_GREEN,
			
			/// <summary>
			/// Indicates that writes to the blue component should be enabled.
			/// </summary>
			Blue = D3D10_COLOR_WRITE_ENABLE_BLUE,
			
			/// <summary>
			/// Indicates that writes to the alpha component should be enabled.
			/// </summary>
			Alpha = D3D10_COLOR_WRITE_ENABLE_ALPHA,
			
			/// <summary>
			/// Indicates that writes to all components should be enabled.
			/// </summary>
			All = D3D10_COLOR_WRITE_ENABLE_ALL
		};
		
		/// <summary>Comparison options.</summary>
		/// <unmanaged>D3D10_COMPARISON_FUNC</unmanaged>
		public enum class Comparison : System::Int32
		{
			/// <summary>
			/// The test never passes.
			/// </summary>
			Never = D3D10_COMPARISON_NEVER,
			
			/// <summary>
			/// The test passes if (source &lt; destination).
			/// </summary>
			Less = D3D10_COMPARISON_LESS,
			
			/// <summary>
			/// The test passes if (source == destination).
			/// </summary>
			Equal = D3D10_COMPARISON_EQUAL,
			
			/// <summary>
			/// The test passes if (source &lt;= destination).
			/// </summary>
			LessEqual = D3D10_COMPARISON_LESS_EQUAL,
			
			/// <summary>
			/// The test passes if (source &gt; destination).
			/// </summary>
			Greater = D3D10_COMPARISON_GREATER,
			
			/// <summary>
			/// The test passes if (source != destination).
			/// </summary>
			NotEqual = D3D10_COMPARISON_NOT_EQUAL,
			
			/// <summary>
			/// The test passes if (source &gt;= destination).
			/// </summary>
			GreaterEqual = D3D10_COMPARISON_GREATER_EQUAL,
			
			/// <summary>
			/// The test always passes.
			/// </summary>
			Always = D3D10_COMPARISON_ALWAYS
		};
		
		/// <summary>Performance counter types.</summary>
		/// <unmanaged>D3D10_COUNTER</unmanaged>
		public enum class CounterKind : System::Int32
		{
			/// <summary>
			/// The counter for the percentage of time the GPU is idle.
			/// </summary>
			Idle = D3D10_COUNTER_GPU_IDLE,
			
			/// <summary>
			/// The counter for the percentage of time the GPU is processing vertices.
			/// </summary>
			VertexProcessing = D3D10_COUNTER_VERTEX_PROCESSING,
			
			/// <summary>
			/// The counter for the percentage of time the GPU is processing geometry.
			/// </summary>
			GeometryProcessing = D3D10_COUNTER_GEOMETRY_PROCESSING,
			
			/// <summary>
			/// The counter for the percentage of time the GPU is processing pixels. 
			/// </summary>
			PixelProcessing = D3D10_COUNTER_PIXEL_PROCESSING,
			
			/// <summary>
			/// The counter for the percentage of time the GPU is performing processing that is not vertex, pixel or geometry processing.
			/// </summary>
			OtherProcessing = D3D10_COUNTER_OTHER_GPU_PROCESSING,
			
			/// <summary>
			/// The counter for the percentage of bandwidth used on a host adapter.
			/// </summary>
			HostAdapterBandwidth = D3D10_COUNTER_HOST_ADAPTER_BANDWIDTH_UTILIZATION,
			
			/// <summary>
			/// The counter for the percentage of bandwidth used by local video memory.
			/// </summary>
			LocalVideoMemoryBandwidth = D3D10_COUNTER_LOCAL_VIDMEM_BANDWIDTH_UTILIZATION,
			
			/// <summary>
			/// The counter for the percentage of throughput for vertices.
			/// </summary>
			VertexThroughput = D3D10_COUNTER_VERTEX_THROUGHPUT_UTILIZATION,
			
			/// <summary>
			/// The counter for the percentage of throughput used for triangle setup.
			/// </summary>
			TriangleSetupThroughput = D3D10_COUNTER_TRIANGLE_SETUP_THROUGHPUT_UTILIZATION,
			
			/// <summary>
			/// The counter for the percentage of throughput used for fillrate.
			/// </summary>
			FillRateThroughput = D3D10_COUNTER_FILLRATE_THROUGHPUT_UTILIZATION,
			
			/// <summary>
			/// The counter for the percentage of time the vertex shader spends sampling resources.
			/// </summary>
			VertexShaderMemoryLimited = D3D10_COUNTER_VS_MEMORY_LIMITED,
			
			/// <summary>
			/// The counter for the percentage of time the vertex shader spends performing computations.
			/// </summary>
			VertexShaderComputationLimited = D3D10_COUNTER_VS_COMPUTATION_LIMITED,
			
			/// <summary>
			/// The counter for the percentage of time the geometry shader spends sampling resources.
			/// </summary>
			GeometryShaderMemoryLimited = D3D10_COUNTER_GS_MEMORY_LIMITED,
			
			/// <summary>
			/// The counter for the percentage of time the geometry shader spends performing computations.
			/// </summary>
			GeometryShaderComputationLimited = D3D10_COUNTER_GS_COMPUTATION_LIMITED,
			
			/// <summary>
			/// The counter for the percentage of time the pixel shader spends sampling resources.
			/// </summary>
			PixelShaderMemoryLimited = D3D10_COUNTER_PS_MEMORY_LIMITED,
			
			/// <summary>
			/// The counter for the percentage of time the pixel shader spends performing computations.
			/// </summary>
			PixelShaderComputationLimited = D3D10_COUNTER_PS_COMPUTATION_LIMITED,
			
			/// <summary>
			/// The counter for the percentage of vertex data that was read from the cache.
			/// </summary>
			PostTransformCacheHitRate = D3D10_COUNTER_POST_TRANSFORM_CACHE_HIT_RATE,
			
			/// <summary>
			/// The counter for the percentage of texel data that was read from the cache.
			/// </summary>
			TextureCacheHitRate = D3D10_COUNTER_TEXTURE_CACHE_HIT_RATE
		};
		
		/// <summary>Data type of a performance counter.</summary>
		/// <unmanaged>D3D10_COUNTER_TYPE</unmanaged>
		public enum class CounterType : System::Int32
		{
			/// <summary>
			/// 32-bit floating point.
			/// </summary>
			Float32 = D3D10_COUNTER_TYPE_FLOAT32,
			
			/// <summary>
			/// 16-bit unsigned integer.
			/// </summary>
			UnsignedInt16 = D3D10_COUNTER_TYPE_UINT16,
			
			/// <summary>
			/// 32-bit unsigned integer.
			/// </summary>
			UnsignedInt32 = D3D10_COUNTER_TYPE_UINT32, 
			
			/// <summary>
			/// 64-bit unsigned integer.
			/// </summary>
			UnsignedInt64 = D3D10_COUNTER_TYPE_UINT64
		};
		
		/// <summary>Specifies the types of CPU access allowed for a resource.</summary>
		/// <unmanaged>D3D10_CPU_ACCESS_FLAG</unmanaged>
		[System::Flags]
		public enum class CpuAccessFlags : System::Int32
		{
			/// <summary>
			/// No access is required on the CPU.
			/// </summary>
			None = 0,
			
			/// <summary>
			/// Write access is required on the CPU.
			/// </summary>
			Write = D3D10_CPU_ACCESS_WRITE,
			
			/// <summary>
			/// Read access is required on the CPU.
			/// </summary>
			Read = D3D10_CPU_ACCESS_READ
		};
		
		/// <summary>Indicates triangles facing a particular direction are not drawn.</summary>
		/// <unmanaged>D3D10_CULL_MODE</unmanaged>
		public enum class CullMode : System::Int32 
		{
			/// <summary>
			/// Do not cull; all triangles are rendered.
			/// </summary>
			None = D3D10_CULL_NONE,
			
			/// <summary>
			/// Cull triangles that are front-facing.
			/// </summary>
			Front = D3D10_CULL_FRONT,
			
			/// <summary>
			/// Cull triangles that are back-facing.
			/// </summary>
			Back = D3D10_CULL_BACK
		};
		
		/// <summary>Flags controlling which debug features are enabled.</summary>
		/// <unmanaged href="bb173520">D3D10_DEBUG_FEATURE</unmanaged>
		[System::Flags]
		public enum class DebugFeatureFlags : System::Int32
		{
			/// <summary>
			/// The application will wait for the GPU to complete the render operation before continuing.
			/// </summary>
			FinishPerRender = D3D10_DEBUG_FEATURE_FINISH_PER_RENDER_OP,
			
			/// <summary>
			/// The runtime will call <see cref="SlimDX::Direct3D10::Device">Device.Flush</see> after the render operation.
			/// </summary>
			FlushPerRender = D3D10_DEBUG_FEATURE_FLUSH_PER_RENDER_OP,
			
			/// <summary>
			/// The runtime will call <see cref="SlimDX::DXGI::SwapChain">SwapChain.Present</see> after the render operation.
			/// Presentation of buffers will occur according to the settings specified by the 
			/// <see cref="SlimDX::Direct3D10::Debug">Debug.SwapChain</see> and <see cref="SlimDX::Direct3D10::Debug">Debug.PresentDelay</see> properties.
			/// </summary>
			PresentPerRender = D3D10_DEBUG_FEATURE_PRESENT_PER_RENDER_OP
		};
		
		/// <summary>Specifies which parts of the depth stencil buffer to clear.</summary>
		/// <unmanaged>D3D10_CLEAR_FLAG</unmanaged>
		[System::Flags]
		public enum class DepthStencilClearFlags : System::Int32
		{
			/// <summary>
			/// Indicates the depth buffer should be cleared.
			/// </summary>
			Depth = D3D10_CLEAR_DEPTH,
			
			/// <summary>
			/// Indicates the stencil buffer should be cleared.
			/// </summary>
			Stencil = D3D10_CLEAR_STENCIL
		};
		
		/// <summary>Specified the dimension (and access methodology) for depth-stencil resource views.</summary>
		/// <unmanaged>D3D10_DSV_DIMENSION</unmanaged>
		public enum class DepthStencilViewDimension : System::Int32
		{
			/// <summary>
			/// The resource will be accessed according to its type, determined by the actual resource specified
			/// during the creation of the view.
			/// </summary>
			Unknown = D3D10_DSV_DIMENSION_UNKNOWN,
			
			/// <summary>
			/// The resource will be accessed as a 1D texture.
			/// </summary>
			Texture1D = D3D10_DSV_DIMENSION_TEXTURE1D,
			
			/// <summary>
			/// The resource will be accessed as an array of 1D textures.
			/// </summary>
			Texture1DArray = D3D10_DSV_DIMENSION_TEXTURE1DARRAY,
			
			/// <summary>
			/// The resource will be accessed as a 2D texture.
			/// </summary>
			Texture2D = D3D10_DSV_DIMENSION_TEXTURE2D,
			
			/// <summary>
			/// The resource will be accessed as an array of 2D textures.
			/// </summary>
			Texture2DArray = D3D10_DSV_DIMENSION_TEXTURE2DARRAY,
			
			/// <summary>
			/// The resource will be accessed as a 2D texture with multisampling support.
			/// </summary>
			Texture2DMultisampled = D3D10_DSV_DIMENSION_TEXTURE2DMS,
			
			/// <summary>
			/// The resource will be accessed as an array of 2D textures with multisampling support.
			/// </summary>
			Texture2DMultisampledArray = D3D10_DSV_DIMENSION_TEXTURE2DMSARRAY
		};
		
		/// <summary>Identifies mask values for writing to a depth-stencil buffer.</summary>
		/// <unmanaged>D3D10_DEPTH_WRITE_MASK</unmanaged>
		public enum class DepthWriteMask : System::Int32
		{
			/// <summary>
			/// Disables all writes to the depth-stencil buffer.
			/// </summary>
			Zero = D3D10_DEPTH_WRITE_MASK_ZERO,
			
			/// <summary>
			/// Enables all writes to the depth-stencil buffer.
			/// </summary>
			All = D3D10_DEPTH_WRITE_MASK_ALL
		};
		
		/// <summary>Flags controlling the creation of a device.</summary>
		/// <unmanaged>D3D10_CREATE_DEVICE_FLAG</unmanaged>
		[System::Flags]
		public enum class DeviceCreationFlags : System::Int32
		{
			/// <summary>
			/// Indicates no specific behavior.
			/// </summary>
			None = 0,
			
			/// <summary>
			/// Indicates that a single-threaded device should be created; by default, Direct3D 10 devices are
			/// thread-safe. Specifying this flag causes thread-safety to be disabled.
			/// </summary>
			SingleThreaded = D3D10_CREATE_DEVICE_SINGLETHREADED,
			
			/// <summary>
			/// Indicates that the device should support the debug layer.
			/// </summary>
			Debug = D3D10_CREATE_DEVICE_DEBUG,
			
			/// <summary>
			/// Indicates that both a REF and HAL device should be created, allowing the application to toggle
			/// between them.
			/// </summary>
			SwitchToRef = D3D10_CREATE_DEVICE_SWITCH_TO_REF,
			
			/// <summary>
			/// Reserved. Do not use.
			/// </summary>
			PreventThreadingOptimizations = D3D10_CREATE_DEVICE_PREVENT_INTERNAL_THREADING_OPTIMIZATIONS,

			AllowNullFromMap = D3D10_CREATE_DEVICE_ALLOW_NULL_FROM_MAP,

			BgraSupport = D3D10_CREATE_DEVICE_BGRA_SUPPORT
		};
	
		/// <summary>Specifies a device driver type.</summary>
		/// <unmanaged>D3D10_DRIVER_TYPE</unmanaged>
		public enum class DriverType : System::Int32
		{	
			/// <summary>
			/// A hardware (HAL) device.
			/// </summary>
			Hardware = D3D10_DRIVER_TYPE_HARDWARE,
			
			/// <summary>
			/// A software reference (REF) device.
			/// </summary>
			Reference = D3D10_DRIVER_TYPE_REFERENCE,
		
			/// <summary>
			/// A null device (a REF device with no rendering capability).
			/// </summary>
			Null = D3D10_DRIVER_TYPE_NULL,
			
			/// <summary>
			/// Reserved. Do not use.
			/// </summary>
			Software = D3D10_DRIVER_TYPE_SOFTWARE,

			/// <summary>
			/// A WARP high performance software rasterizer.
			/// </summary>
			Warp = D3D10_DRIVER_TYPE_WARP,
		};

		/// <summary>Specifies detailed information about <see cref="SlimDX::Direct3D10::Effect">Effect</see> variables.</summary>
		/// <unmanaged>D3D10_EFFECT_VARIABLE</unmanaged>
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
			Annotation = D3D10_EFFECT_VARIABLE_ANNOTATION,
			
			/// <summary>
			/// Indicates that the variable or constant buffer resides in an effect pool. 
			/// </summary>
			Pooled = D3D10_EFFECT_VARIABLE_POOLED,
			
			/// <summary>
			/// Indicates the variable has been explicitly bound using the register keyword in the effect code.
			/// </summary>
			ExplicitBindPoint = D3D10_EFFECT_VARIABLE_EXPLICIT_BIND_POINT,
		};
		
		/// <summary>Determines the fill mode to use when rendering triangles.</summary>
		/// <unmanaged>D3D10_FILL_MODE</unmanaged>
		public enum class FillMode : System::Int32
		{
			/// <summary>
			/// Draw lines connecting the vertices. Adjacent vertices are not drawn.
			/// </summary>
			Wireframe = D3D10_FILL_WIREFRAME,
			
			/// <summary>
			/// Fill the triangles formed by the vertices. Adjacent vertices are not drawn.
			/// </summary>
			Solid = D3D10_FILL_SOLID
		};
		
		/// <summary>Specifies filtering options used during texture sampling.</summary>
		/// <unmanaged>D3D10_FILTER</unmanaged>
		public enum class Filter : System::Int32
		{
			/// <summary>
			/// Use point sampling for minification, magnification, and mip-level sampling.
			/// </summary>
			MinMagMipPoint = D3D10_FILTER_MIN_MAG_MIP_POINT,
			
			/// <summary>
			/// Use point sampling for minification and magnification; use linear interpolation for mip-level sampling.
			/// </summary>
			MinMagPointMipLinear = D3D10_FILTER_MIN_MAG_POINT_MIP_LINEAR,
			
			/// <summary>
			/// Use point sampling for minification; use linear interpolation for magnification; use point sampling for mip-level sampling.
			/// </summary>
			MinPointMagLinearMipPoint = D3D10_FILTER_MIN_POINT_MAG_LINEAR_MIP_POINT,
			
			/// <summary>
			/// Use point sampling for minification; use linear interpolation for magnification and mip-level sampling.
			/// </summary>
			MinPointMagMipLinear = D3D10_FILTER_MIN_POINT_MAG_MIP_LINEAR,
			
			/// <summary>
			/// Use linear interpolation for minification; use point sampling for magnification and mip-level sampling.
			/// </summary>
			MinLinearMagMipPoint = D3D10_FILTER_MIN_LINEAR_MAG_MIP_POINT,
			
			/// <summary>
			/// Use linear interpolation for minification; use point sampling for magnification; use linear interpolation for mip-level sampling.
			/// </summary>
			MinLinearMagPointMipLinear = D3D10_FILTER_MIN_LINEAR_MAG_POINT_MIP_LINEAR,
			
			/// <summary>
			/// Use linear interpolation for minification and magnification; use point sampling for mip-level sampling.
			/// </summary>
			MinMagLinearMipPoint = D3D10_FILTER_MIN_MAG_LINEAR_MIP_POINT,
			
			/// <summary>
			/// Use linear interpolation for minification, magnification, and mip-level sampling.
			/// </summary>
			MinMagMipLinear = D3D10_FILTER_MIN_MAG_MIP_LINEAR,
			
			/// <summary>
			/// Use anisotropic interpolation for minification, magnification, and mip-level sampling.
			/// </summary>
			Anisotropic = D3D10_FILTER_ANISOTROPIC,
			
			/// <summary>
			/// Use point sampling for minification, magnification, and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinMagMipPoint = D3D10_FILTER_COMPARISON_MIN_MAG_MIP_POINT,
			
			/// <summary>
			/// Use point sampling for minification and magnification; use linear interpolation for mip-level sampling.
			/// Compare the result to the comparison value.
			/// </summary>
			ComparisonMinMagPointMipLinear = D3D10_FILTER_COMPARISON_MIN_MAG_POINT_MIP_LINEAR,
			
			/// <summary>
			/// Use point sampling for minification; use linear interpolation for magnification; use point sampling for mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinPointMagLinearMipPoint = D3D10_FILTER_COMPARISON_MIN_POINT_MAG_LINEAR_MIP_POINT,
			
			/// <summary>
			/// Use point sampling for minification; use linear interpolation for magnification and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinPointMagMipLinear = D3D10_FILTER_COMPARISON_MIN_POINT_MAG_MIP_LINEAR,
			
			/// <summary>
			/// Use linear interpolation for minification; use point sampling for magnification and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinLinearMagMipPoint = D3D10_FILTER_COMPARISON_MIN_LINEAR_MAG_MIP_POINT,
			
			/// <summary>
			/// Use linear interpolation for minification; use point sampling for magnification; use linear interpolation for mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinLinearMagPointMipLinear = D3D10_FILTER_COMPARISON_MIN_LINEAR_MAG_POINT_MIP_LINEAR,
			
			/// <summary>
			/// Use linear interpolation for minification and magnification; use point sampling for mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinMagLinearMipPoint = D3D10_FILTER_COMPARISON_MIN_MAG_LINEAR_MIP_POINT,
			
			/// <summary>
			/// Use linear interpolation for minification, magnification, and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonMinMagMipLinear = D3D10_FILTER_COMPARISON_MIN_MAG_MIP_LINEAR,
			
			/// <summary>
			/// Use anisotropic interpolation for minification, magnification, and mip-level sampling. Compare the result to the comparison value.
			/// </summary>
			ComparisonAnisotropic = D3D10_FILTER_COMPARISON_ANISOTROPIC,
			
			/// <summary>
			/// For use with pixel shaders utilizing textures in the R1_UNorm format.
			/// </summary>
			Texture1Bit = D3D10_FILTER_TEXT_1BIT
		};
		
		/// <summary>Specifies possible texture filtering flags.</summary>
		/// <unmanaged>D3DX10_FILTER_FLAG</unmanaged>
		[System::Flags]
		public enum class FilterFlags : System::Int32
		{
			/// <summary>
			/// No scaling or filtering will take place. Pixels outside the bounds of the source image are assumed to be transparent black.
			/// </summary>
			None = D3DX10_FILTER_NONE,
			
			/// <summary>
			/// Each destination pixel is computed by sampling the nearest pixel from the source image.
			/// </summary>
			Point = D3DX10_FILTER_POINT,
			
			/// <summary>
			/// Each destination pixel is computed by sampling the four nearest pixels from the source image. This filter
			/// works best when the scale on both axes is less than two.
			/// </summary>
			Linear = D3DX10_FILTER_LINEAR,
			
			/// <summary>
			/// Every pixel in the source image contributes equally to the destination image. This is the slowest of the filters.
			/// </summary>
			Triangle = D3DX10_FILTER_TRIANGLE,
			
			/// <summary>
			/// Each pixel is computed by averaging a 2x2(x2) box of pixels from the source image. This filter works only when the
			/// dimensions of the destination are half those of the source, as is the case with mipmaps.
			/// </summary>
			Box = D3DX10_FILTER_BOX,
			
			/// <summary>
			/// Pixels off the edge of the texture on the u-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorU = D3DX10_FILTER_MIRROR_U,
			
			/// <summary>
			/// Pixels off the edge of the texture on the v-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorV = D3DX10_FILTER_MIRROR_V,
			
			/// <summary>
			/// Pixels off the edge of the texture on the w-axis should be mirrored, not wrapped.
			/// </summary>
			MirrowW = D3DX10_FILTER_MIRROR_W,
			
			/// <summary>
			/// Combines the MirrorU, MirrorV, and MirrorW flags.
			/// </summary>
			Mirror = D3DX10_FILTER_MIRROR,
			
			/// <summary>
			/// The resulting image must be dithered using a 4x4 ordered dither algorithm. This happens when converting from one format to another.
			/// </summary>
			Dither = D3DX10_FILTER_DITHER,
			
			/// <summary>
			/// Do diffuse dithering on the image when changing from one format to another.
			/// </summary>
			DitherDiffusion = D3DX10_FILTER_DITHER_DIFFUSION,
			
			/// <summary>
			/// Input data is in standard RGB (sRGB) color space.
			/// </summary>
			StandardRgbIn = D3DX10_FILTER_SRGB_IN,
			
			/// <summary>
			/// Output data is in standard RGB (sRGB) color space.
			/// </summary>
			StandardRgbOut = D3DX10_FILTER_SRGB_OUT,
			
			/// <summary>
			/// Combines the StandardRgbIn and StandardRgbOut flags.
			/// </summary>
			StandardRgb = D3DX10_FILTER_SRGB,
		};
	
		/// <summary>
		/// Defines possible character sets for fonts.
		/// </summary>
		/// <unmanaged>CHARSET</unmanaged>
		public enum class FontCharacterSet : System::Int32
		{
			/// <summary>
			/// The ANSI character set.
			/// </summary>
			Ansi = ANSI_CHARSET,

			/// <summary>
			/// The default system character set.
			/// </summary>
			Default = DEFAULT_CHARSET,

			/// <summary>
			/// The symbol character set.
			/// </summary>
			Symbol = SYMBOL_CHARSET,

			/// <summary>
			/// The ShiftJIS character set.
			/// </summary>
			ShiftJIS = SHIFTJIS_CHARSET,

			/// <summary>
			/// The Hangul character set.
			/// </summary>
			Hangul = HANGUL_CHARSET,

			/// <summary>
			/// The GB2312 character set.
			/// </summary>
			GB2312 = GB2312_CHARSET,

			/// <summary>
			/// The Chinese character set.
			/// </summary>
			ChineseBig5 = CHINESEBIG5_CHARSET,

			/// <summary>
			/// The OEM character set.
			/// </summary>
			Oem = OEM_CHARSET,

			/// <summary>
			/// The Johab character set.
			/// </summary>
			Johab = JOHAB_CHARSET,

			/// <summary>
			/// The Hebrew character set.
			/// </summary>
			Hebrew = HEBREW_CHARSET,

			/// <summary>
			/// The Arabic character set.
			/// </summary>
			Arabic = ARABIC_CHARSET,

			/// <summary>
			/// The Greek character set.
			/// </summary>
			Greek = GREEK_CHARSET,

			/// <summary>
			/// The Turkish character set.
			/// </summary>
			Turkish = TURKISH_CHARSET,

			/// <summary>
			/// The Vietnamese character set.
			/// </summary>
			Vietnamese = VIETNAMESE_CHARSET,

			/// <summary>
			/// The Thai character set.
			/// </summary>
			Thai = THAI_CHARSET,

			/// <summary>
			/// The East Europe character set.
			/// </summary>
			EastEurope = EASTEUROPE_CHARSET,

			/// <summary>
			/// The Russian character set.
			/// </summary>
			Russian = RUSSIAN_CHARSET,

			/// <summary>
			/// The Baltic character set.
			/// </summary>
			Baltic = BALTIC_CHARSET,

			/// <summary>
			/// The Mac character set.
			/// </summary>
			Mac = MAC_CHARSET
		};

		/// <summary>
		/// Specifies formatting options for text rendering.
		/// </summary>
		/// <unmanaged href="bb773199">DT</unmanaged>
		[System::Flags]
		public enum class FontDrawFlags : System::Int32
		{
			/// <summary>
			/// Align the text to the top.
			/// </summary>
			Top = DT_TOP,

			/// <summary>
			/// Align the text to the left.
			/// </summary>
			Left = DT_LEFT,

			/// <summary>
			/// Align the text to the center.
			/// </summary>
			Center = DT_CENTER,

			/// <summary>
			/// Align the text to the right.
			/// </summary>
			Right = DT_RIGHT,

			/// <summary>
			/// Vertically align the text to the center.
			/// </summary>
			VerticalCenter = DT_VCENTER,

			/// <summary>
			/// Align the text to the bottom.
			/// </summary>
			Bottom = DT_BOTTOM,

			/// <summary>
			/// Allow word breaks.
			/// </summary>
			WordBreak = DT_WORDBREAK,

			/// <summary>
			/// Force all text to a single line.
			/// </summary>
			SingleLine = DT_SINGLELINE,

			/// <summary>
			/// Expand tab characters.
			/// </summary>
			ExpandTabs = DT_EXPANDTABS,

			/// <summary>
			/// Don't clip the text.
			/// </summary>
			NoClip = DT_NOCLIP,

			/// <summary>
			/// Rendering the text in right-to-left reading order.
			/// </summary>
			RtlReading = DT_RTLREADING,
		};
		
		/// <summary>
		/// Defines pitch and family settings for fonts.
		/// </summary>
		/// <unmanaged href="ms901140">(various font constants)</unmanaged>
		[System::Flags]
		public enum class FontPitchAndFamily : System::Int32
		{
			/// <summary>
			/// Default pitch.
			/// </summary>
			Default = DEFAULT_PITCH,

			/// <summary>
			/// Fixed pitch.
			/// </summary>
			Fixed = FIXED_PITCH,

			/// <summary>
			/// Variable pitch.
			/// </summary>
			Variable = VARIABLE_PITCH,

			/// <summary>
			/// Mono pitch.
			/// </summary>
			Mono = MONO_FONT,

			/// <summary>
			/// The font family doesn't matter.
			/// </summary>
			DontCare = FF_DONTCARE,

			/// <summary>
			/// Use the Roman family.
			/// </summary>
			Roman = FF_ROMAN,

			/// <summary>
			/// Use the Swiss family.
			/// </summary>
			Swiss = FF_SWISS,

			/// <summary>
			/// Use the Modern family.
			/// </summary>
			Modern = FF_MODERN,

			/// <summary>
			/// Use the Script family.
			/// </summary>
			Script = FF_SCRIPT,

			/// <summary>
			/// Use the Decorative family.
			/// </summary>
			Decorative = FF_DECORATIVE,
		};

		/// <summary>
		/// Defines precision levels for font rendering.
		/// </summary>
		/// <unmanaged href="cc215248">OutPrecision</unmanaged>
		public enum class FontPrecision : System::Int32
		{
			/// <summary>
			/// Default precision.
			/// </summary>
			Default = OUT_DEFAULT_PRECIS,

			/// <summary>
			/// String-level precision.
			/// </summary>
			String = OUT_STRING_PRECIS,

			/// <summary>
			/// Character-level precision.
			/// </summary>
			Character = OUT_CHARACTER_PRECIS,

			/// <summary>
			/// Stroke-level precision.
			/// </summary>
			Stroke = OUT_STROKE_PRECIS,

			/// <summary>
			/// TrueType precision.
			/// </summary>
			TrueType = OUT_TT_PRECIS,

			/// <summary>
			/// Device precision.
			/// </summary>
			Device = OUT_DEVICE_PRECIS,

			/// <summary>
			/// Raster precision.
			/// </summary>
			Raster = OUT_RASTER_PRECIS,

			/// <summary>
			/// TrueType only precision.
			/// </summary>
			TrueTypeOnly = OUT_TT_ONLY_PRECIS,

			/// <summary>
			/// Outline precision.
			/// </summary>
			Outline = OUT_OUTLINE_PRECIS,

			/// <summary>
			/// Screen outline precision.
			/// </summary>
			ScreenOutline = OUT_SCREEN_OUTLINE_PRECIS,

			/// <summary>
			/// PostScript only precision.
			/// </summary>
			PostScriptOnly = OUT_PS_ONLY_PRECIS,
		};

		/// <summary>
		/// Specifies quality options for font rendering.
		/// </summary>
		/// <unmanaged href="ms901140">QUALITY</unmanaged>
		public enum class FontQuality : System::Int32
		{
			/// <summary>
			/// Default quality levels.
			/// </summary>
			Default = DEFAULT_QUALITY,

			/// <summary>
			/// Draft quality.
			/// </summary>
			Draft = DRAFT_QUALITY,

			/// <summary>
			/// Proof quality.
			/// </summary>
			Proof = PROOF_QUALITY,

			/// <summary>
			/// Non-antialiased quality.
			/// </summary>
			NonAntialiased = NONANTIALIASED_QUALITY,

			/// <summary>
			/// Antialiased quality.
			/// </summary>
			Antialiased = ANTIALIASED_QUALITY,

			/// <summary>
			/// Clear type quality.
			/// </summary>
			ClearType = CLEARTYPE_QUALITY,

			/// <summary>
			/// Clear type natural quality.
			/// </summary>
			ClearTypeNatural = CLEARTYPE_NATURAL_QUALITY,
		};

		/// <summary>
		/// Specifies weights for font rendering.
		/// </summary>
		/// <unmanaged href="ms901140">FW</unmanaged>
		public enum class FontWeight : System::Int32
		{
			/// <summary>
			/// The font weight doesn't matter.
			/// </summary>
			DoNotCare = FW_DONTCARE,

			/// <summary>
			/// Make the font thin.
			/// </summary>
			Thin = FW_THIN,

			/// <summary>
			/// Make the font extra light.
			/// </summary>
			ExtraLight = FW_EXTRALIGHT,

			/// <summary>
			/// Make the font ultra light.
			/// </summary>
			UltraLight = FW_ULTRALIGHT,

			/// <summary>
			/// Make the font light.
			/// </summary>
			Light = FW_LIGHT,

			/// <summary>
			/// Use a normal weight.
			/// </summary>
			Normal = FW_NORMAL,

			/// <summary>
			/// Use a regular weight.
			/// </summary>
			Regular = FW_REGULAR,

			/// <summary>
			/// Use a medium weight.
			/// </summary>
			Medium = FW_MEDIUM,

			/// <summary>
			/// Use a semi-bold weight.
			/// </summary>
			SemiBold = FW_SEMIBOLD,

			/// <summary>
			/// Use a demi-bold weight.
			/// </summary>
			DemiBold = FW_DEMIBOLD,

			/// <summary>
			/// Use a bold weight.
			/// </summary>
			Bold = FW_BOLD,

			/// <summary>
			/// Use an extra bold weight.
			/// </summary>
			ExtraBold = FW_EXTRABOLD,

			/// <summary>
			/// Use an ultra bold weight.
			/// </summary>
			UltraBold = FW_ULTRABOLD,

			/// <summary>
			/// Use a heavy weight.
			/// </summary>
			Heavy = FW_HEAVY,

			/// <summary>
			/// Use a black weight.
			/// </summary>
			Black = FW_BLACK,
		};
		
		/// <summary>Identifies which resources are supported for a given format and given device.</summary>
		/// <unmanaged>D3D10_FORMAT_SUPPORT</unmanaged>
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
			Buffer = D3D10_FORMAT_SUPPORT_BUFFER,
			
			/// <summary>
			/// Vertex buffers are supported.
			/// </summary>
			VertexBuffer = D3D10_FORMAT_SUPPORT_IA_VERTEX_BUFFER,
			
			/// <summary>
			/// Index buffers are supported.
			/// </summary>
			IndexBuffer = D3D10_FORMAT_SUPPORT_IA_INDEX_BUFFER,
			
			/// <summary>
			/// Streaming output buffers are supported.
			/// </summary>
			StreamOutputBuffer = D3D10_FORMAT_SUPPORT_SO_BUFFER,
			
			/// <summary>
			/// 1D textures are supported.
			/// </summary>
			Texture1D = D3D10_FORMAT_SUPPORT_TEXTURE1D,
			
			/// <summary>
			/// 2D textures are supported.
			/// </summary>
			Texture2D = D3D10_FORMAT_SUPPORT_TEXTURE2D,
			
			/// <summary>
			/// 3D textures are supported.
			/// </summary>
			Texture3D = D3D10_FORMAT_SUPPORT_TEXTURE3D,
			
			/// <summary>
			/// Cube textures are supported.
			/// </summary>
			TextureCube = D3D10_FORMAT_SUPPORT_TEXTURECUBE,
		
			/// <summary>
			/// The intrinsic HLSL "load" function is supported.
			/// </summary>
			ShaderLoadIntrinsic = D3D10_FORMAT_SUPPORT_SHADER_LOAD,
			
			/// <summary>
			/// The intrinsic HLSL "sample" function is supported.
			/// </summary>
			ShaderSampleIntrinsic = D3D10_FORMAT_SUPPORT_SHADER_SAMPLE,
			
			/// <summary>
			/// The intrinsic HLSL "samplecmp" and "samplecmplevelzero" are supported.
			/// </summary>
			ShaderSampleComparisonIntrinsic = D3D10_FORMAT_SUPPORT_SHADER_SAMPLE_COMPARISON,
			
			/// <summary>
			/// Mipmaps are supported.
			/// </summary>
			MipMap = D3D10_FORMAT_SUPPORT_MIP,
			
			/// <summary>
			/// Automatic generation of mipmaps is supported.
			/// </summary>
			MipMapAutoGeneration = D3D10_FORMAT_SUPPORT_MIP_AUTOGEN,
			
			/// <summary>
			/// Rendertargets are supported.
			/// </summary>
			RenderTarget = D3D10_FORMAT_SUPPORT_RENDER_TARGET,
			
			/// <summary>
			/// Blend operations are supported.
			/// </summary>
			BlendOperation = D3D10_FORMAT_SUPPORT_BLENDABLE,
			
			/// <summary>
			/// Depth-stencil surfaces are supported.
			/// </summary>
			DepthStencil = D3D10_FORMAT_SUPPORT_DEPTH_STENCIL,
			
			/// <summary>
			/// CPU locking is supported.
			/// </summary>
			CpuLocking = D3D10_FORMAT_SUPPORT_CPU_LOCKABLE,
			
			/// <summary>
			/// Multisampling resolution is supported.
			/// </summary>
			MultisampleResolve = D3D10_FORMAT_SUPPORT_MULTISAMPLE_RESOLVE,
			
			/// <summary>
			/// The format can be displayed on screen.
			/// </summary>
			FormatDisplaySupport = D3D10_FORMAT_SUPPORT_DISPLAY,
			
			/// <summary>
			/// The format can be cast to another format.
			/// </summary>
			FormatCastSupport = D3D10_FORMAT_SUPPORT_CAST_WITHIN_BIT_LAYOUT,
			
			/// <summary>
			/// The format can be used as a multisample render target.
			/// </summary>
			FormatMultisampleRenderTargetSupport = D3D10_FORMAT_SUPPORT_MULTISAMPLE_RENDERTARGET,
			
			/// <summary>
			/// The format can be used as a multisample texture and read into a shader with the HLSL "load" function.
			/// </summary>
			FormatMultisampleLoadSupport = D3D10_FORMAT_SUPPORT_MULTISAMPLE_LOAD
		};
		
		/// <summary>Specifies image file formats supported by runtime.</summary>
		/// <unmanaged>D3DX10_IMAGE_FILE_FORMAT</unmanaged>
		public enum class ImageFileFormat : System::Int32
		{
			/// <summary>
			/// Windows bitmap (BMP) file format. Contains a header that describes the resolution of the device on which the
			/// rectangle of pixels was created, the dimensions of the rectangle, the size of the array of bits, a logical palette,
			/// and an array of bits that defines the relationship between pixels in the bitmapped image and entries in the logical palette.
			/// </summary>
			Bmp = D3DX10_IFF_BMP,
			
			/// <summary>
			/// Joint Photographic Experts Group (JPEG) compressed file format. Specifies variable compression of 24-bit RGB color
			/// and 8-bit gray-scale Tagged Image File Format (TIFF) image document files.
			/// </summary>
			Jpg = D3DX10_IFF_JPG,
			
			/// <summary>
			/// Portable Network Graphics (PNG) file format. A non-proprietary bitmap format using lossless compression.
			/// </summary>
			Png = D3DX10_IFF_PNG,
			
			/// <summary>
			/// DirectDraw surface (DDS) file format. Stores textures, volume textures, and cubic environment maps, with or without
			/// mipmap levels, and with or without pixel compression.
			/// </summary>
			Dds = D3DX10_IFF_DDS,
			
			/// <summary>
			/// Tagged Image File Format (TIFF).
			/// </summary>
			Tiff = D3DX10_IFF_TIFF,
			
			/// <summary>
			/// Graphics Interchange Format (GIF).
			/// </summary>
			Gif = D3DX10_IFF_GIF,
			
			/// <summary>
			/// Windows Media Player format (WMP).
			/// </summary>
			Wmp = D3DX10_IFF_WMP,
		};
		
		/// <summary>Type of data contained in an input slot.</summary>
		/// <unmanaged>D3D10_INPUT_CLASSIFICATION</unmanaged>
		public enum class InputClassification : System::Int32
		{
			/// <summary>
			/// The data in the input slot is per-vertex data.
			/// </summary>
			PerVertexData = D3D10_INPUT_PER_VERTEX_DATA,
			
			/// <summary>
			/// The data in the input slot is per-instance data.
			/// </summary>
			PerInstanceData = D3D10_INPUT_PER_INSTANCE_DATA
		};
		
		/// <summary>Identifies a resource to be accessed for reading and writing by the CPU.</summary>
		/// <unmanaged>D3D10_MAP</unmanaged>
		public enum class MapMode : System::Int32
		{
			/// <summary>
			/// Resource is mapped for reading. The resource must have been created with read access.
			/// </summary>
			Read = D3D10_MAP_READ,
			
			/// <summary>
			/// Resource is mapped for writing. The resource must have been created with write access.
			/// </summary>
			Write = D3D10_MAP_WRITE,
			
			/// <summary>
			/// Resource is mapped for reading and writing. The resource must have been created with read and write access.
			/// </summary>
			ReadWrite = D3D10_MAP_READ_WRITE,
			
			/// <summary>
			/// Resource is mapped for writing; the previous contents of the resource will be undefined. The resource must have been created with write access.
			/// </summary>
			WriteDiscard = D3D10_MAP_WRITE_DISCARD,
			
			/// <summary>
			/// Resource is mapped for writing; the existing contents of the resource cannot be overwritten. This flag is only valid on
			/// vertex and index buffers. The resource must have been created with write access.
			/// </summary>
			WriteNoOverwrite = D3D10_MAP_WRITE_NO_OVERWRITE
		};
		
		/// <summary>Specifies how the CPU should respond when Map() is called on a resource being used by the GPU.</summary>
		/// <unmanaged>D3D10_MAP_FLAG</unmanaged>
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
			DoNotWait = D3D10_MAP_FLAG_DO_NOT_WAIT
		};

		/// <summary>Specifies creation options for a mesh.</summary>
		/// <unmanaged>D3DX10_MESH</unmanaged>
		[System::Flags]
		public enum class MeshFlags : System::Int32
		{
			/// <summary>
			/// Indicates standard mesh creation options..
			/// </summary>
			None = 0,
			
			/// <summary>
			/// Indicates that the mesh should use 32-bit indices rather than 16-bit indices.
			/// </summary>
			Has32BitIndices = D3DX10_MESH_32_BIT,
			
			/// <summary>
			/// Indicates that the mesh should contain adjacency information.
			/// </summary>
			HasAdjacency = D3DX10_MESH_GS_ADJACENCY
		};

		/// <summary>Specifies which types of mesh data to discard from the device.</summary>
		/// <unmanaged>D3DX10_MESH_DISCARD_FLAGS</unmanaged>
		[System::Flags]
		public enum class MeshDiscardFlags : System::Int32
		{
			/// <summary>
			/// Indicates that the attribute buffer should be discarded.
			/// </summary>
			DiscardAttributeBuffer = D3DX10_MESH_DISCARD_ATTRIBUTE_BUFFER,
			
			/// <summary>
			/// Indicates that the attribute table should be discarded.
			/// </summary>
			DiscardAttributeTable = D3DX10_MESH_DISCARD_ATTRIBUTE_TABLE,
			
			/// <summary>
			/// Indicates that the point representation buffer should be discarded.
			/// </summary>
			DiscardPointRepresentation = D3DX10_MESH_DISCARD_POINTREPS,
			
			/// <summary>
			/// Indicates that the adjacency buffer should be discarded.
			/// </summary>
			DiscardAdjacency = D3DX10_MESH_DISCARD_ADJACENCY,
			
			/// <summary>
			/// Indicates that any buffers committed to the device should be discarded.
			/// </summary>
			DiscardDeviceBuffers = D3DX10_MESH_DISCARD_DEVICE_BUFFERS
		};
		
		/// <summary>
		/// Values that indicate how to alter mesh data during an optimization operation.
		/// </summary>
		/// <unmanaged>D3DX10_MESHOPT</unmanaged>
		[System::Flags]
		public enum class MeshOptimizeFlags : System::Int32
		{
			/// <summary>
			/// Reorders faces to remove unused vertices and faces.
			/// </summary>
			Compact = D3DX10_MESHOPT_COMPACT,
			
			/// <summary>
			/// Reorders faces to optimize for fewer attribute bundle state changes and enhanced draw performance.
			/// </summary>
			AttributeSort = D3DX10_MESHOPT_ATTR_SORT,
	    
			/// <summary>
			/// Reorders faces to increase the cache hit rate of vertex caches.
			/// </summary>
			VertexCache = D3DX10_MESHOPT_VERTEX_CACHE,
	    
			/// <summary>
			/// Reorders faces to maximize length of adjacent triangles.
			/// </summary>
			StripReorder = D3DX10_MESHOPT_STRIP_REORDER,
	    
			/// <summary>
			/// Optimize the faces only; do not optimize the vertices.
			/// </summary>
			IgnoreVertices = D3DX10_MESHOPT_IGNORE_VERTS,
			
			/// <summary>
			/// While attribute sorting, do not split vertices that are shared between attribute groups.
			/// </summary>
			DoNotSplit = D3DX10_MESHOPT_DO_NOT_SPLIT,
			
			/// <summary>
			/// Affects the vertex cache size. Using this flag specifies a default vertex cache size that works well on legacy hardware.
			/// </summary>
			DeviceIndependent = D3DX10_MESHOPT_DEVICE_INDEPENDENT,
		};

		/// <summary>
		/// Normal maps generation constants.
		/// </summary>
		/// <unmanaged>D3DX10_NORMALMAP_FLAG</unmanaged>
		[System::Flags]
		public enum class NormalMapFlags : System::Int32
		{
			/// <summary>
			/// Indicates that pixels off the edge of the texture on the u-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorU = D3DX10_NORMALMAP_MIRROR_U,

			/// <summary>
			/// Indicates that pixels off the edge of the texture on the v-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorV = D3DX10_NORMALMAP_MIRROR_V,

			/// <summary>
			/// Same as specifying the MirrorU and MirrorV flags.
			/// </summary>
			Mirror = D3DX10_NORMALMAP_MIRROR,

			/// <summary>
			/// Inverts the direction of each normal.
			/// </summary>
			InvertSign = D3DX10_NORMALMAP_INVERTSIGN,

			/// <summary>
			/// Computes the per-pixel occlusion term and encodes it into the alpha. An alpha of 1 means that the
			/// pixel is not obscured in any way, and an alpha of 0 means that the pixel is completely obscured.
			/// </summary>
			ComputeOcclusion = D3DX10_NORMALMAP_COMPUTE_OCCLUSION
		};
		
		/// <summary>Specifies how the pipeline should interpret vertex data bound to the input assembler stage.</summary>
		/// <unmanaged>D3D10_PRIMITIVE_TOPOLOGY</unmanaged>
		public enum class PrimitiveTopology : System::Int32
		{
			/// <summary>
			/// The IA stage has not been initialized with a primitive topology. The IA stage will not function properly unless a primitive topology is defined.
			/// </summary>
			Undefined = D3D10_PRIMITIVE_TOPOLOGY_UNDEFINED,
			
			/// <summary>
			/// Interpret the vertex data as a list of points.
			/// </summary>
			PointList = D3D10_PRIMITIVE_TOPOLOGY_POINTLIST,
			
			/// <summary>
			/// Interpret the vertex data as a list of lines.
			/// </summary>
			LineList = D3D10_PRIMITIVE_TOPOLOGY_LINELIST,
			
			/// <summary>
			/// Interpret the vertex data as a line strip.
			/// </summary>
			LineStrip = D3D10_PRIMITIVE_TOPOLOGY_LINESTRIP,
			
			/// <summary>
			/// Interpret the vertex data as a list of triangles.
			/// </summary>
			TriangleList = D3D10_PRIMITIVE_TOPOLOGY_TRIANGLELIST,
			
			/// <summary>
			/// Interpret the vertex data as a triangle strip.
			/// </summary>
			TriangleStrip = D3D10_PRIMITIVE_TOPOLOGY_TRIANGLESTRIP,
			
			/// <summary>
			/// Interpret the vertex data as list of lines with adjacency data.
			/// </summary>
			LineListWithAdjacency = D3D10_PRIMITIVE_TOPOLOGY_LINELIST_ADJ,
			
			/// <summary>
			/// Interpret the vertex data as line strip with adjacency data.
			/// </summary>
			LineStripWithAdjacency = D3D10_PRIMITIVE_TOPOLOGY_LINESTRIP_ADJ,
			
			/// <summary>
			/// Interpret the vertex data as list of triangles with adjacency data.
			/// </summary>
			TriangleListWithAdjacency = D3D10_PRIMITIVE_TOPOLOGY_TRIANGLELIST_ADJ,
			
			/// <summary>
			/// Interpret the vertex data as triangle strip with adjacency data.
			/// </summary>
			TriangleStripWithAdjacency = D3D10_PRIMITIVE_TOPOLOGY_TRIANGLESTRIP_ADJ
		};
		
		/// <summary>
		/// Flags for describing query options and behavior.
		/// </summary>
		/// <unmanaged>D3D10_QUERY_MISC_FLAG</unmanaged>
		[System::Flags]
		public enum class QueryFlags : System::Int32
		{
			None = 0,
			PredicateHint = D3D10_QUERY_MISC_PREDICATEHINT,
		};
		
		/// <summary>
		/// Values which description query types.
		/// </summary>
		/// <unmanaged>D3D10_QUERY</unmanaged>
		public enum class QueryType : System::Int32
		{
			Event = D3D10_QUERY_EVENT,
			Occlusion = D3D10_QUERY_OCCLUSION,
			Timestamp = D3D10_QUERY_TIMESTAMP,
			TimestampDisjoint = D3D10_QUERY_TIMESTAMP_DISJOINT,
			PipelineStatistics = D3D10_QUERY_PIPELINE_STATISTICS,
			Predicate = D3D10_QUERY_OCCLUSION_PREDICATE,
			StreamOutputStatistics = D3D10_QUERY_SO_STATISTICS,
			StreamOutputOverflowPredicate = D3D10_QUERY_SO_OVERFLOW_PREDICATE,
		};
		
		/// <summary></summary>
		/// <unmanaged>D3D10_RTV_DIMENSION</unmanaged>
		public enum class RenderTargetViewDimension : System::Int32
		{
			Unknown = D3D10_RTV_DIMENSION_UNKNOWN,
			Buffer = D3D10_RTV_DIMENSION_BUFFER,
			Texture1D = D3D10_RTV_DIMENSION_TEXTURE1D ,
			Texture1DArray = D3D10_RTV_DIMENSION_TEXTURE1DARRAY,
			Texture2D = D3D10_RTV_DIMENSION_TEXTURE2D,
			Texture2DArray = D3D10_RTV_DIMENSION_TEXTURE2DARRAY,
			Texture2DMultisampled = D3D10_RTV_DIMENSION_TEXTURE2DMS,
			Texture2DMultisampledArray = D3D10_RTV_DIMENSION_TEXTURE2DMSARRAY,
			Texture3D = D3D10_RTV_DIMENSION_TEXTURE3D
		};
		
		/// <summary></summary>
		/// <unmanaged>D3D10_RESOURCE_DIMENSION</unmanaged>
		public enum class ResourceDimension : System::Int32
		{
			Unknown = D3D10_RESOURCE_DIMENSION_UNKNOWN,
			Buffer = D3D10_RESOURCE_DIMENSION_BUFFER,
			Texture1D = D3D10_RESOURCE_DIMENSION_TEXTURE1D,
			Texture2D = D3D10_RESOURCE_DIMENSION_TEXTURE2D,
			Texture3D = D3D10_RESOURCE_DIMENSION_TEXTURE3D
		};
		
		/// <summary></summary>
		/// <unmanaged>D3D10_RESOURCE_MISC_FLAG</unmanaged>
		[System::Flags]
		public enum class ResourceOptionFlags : System::Int32
		{
			None = 0,
			GenerateMipMaps = D3D10_RESOURCE_MISC_GENERATE_MIPS,
			Shared = D3D10_RESOURCE_MISC_SHARED,
			TextureCube = D3D10_RESOURCE_MISC_TEXTURECUBE,
			KeyedMutex = D3D10_RESOURCE_MISC_SHARED_KEYEDMUTEX,
			GdiCompatible = D3D10_RESOURCE_MISC_GDI_COMPATIBLE
		};

		/// <summary></summary>
		/// <unmanaged>D3D10_USAGE</unmanaged>
		public enum class ResourceUsage : System::Int32
		{
			Default = D3D10_USAGE_DEFAULT,
			Immutable = D3D10_USAGE_IMMUTABLE,
			Dynamic = D3D10_USAGE_DYNAMIC,
			Staging = D3D10_USAGE_STAGING
		};
		
		/// <summary></summary>
		/// <unmanaged>D3D10_RTV_DIMENSION</unmanaged>
		public enum class ResourceViewDimension : System::Int32
		{
			Unknown = D3D10_RTV_DIMENSION_UNKNOWN,
			Buffer = D3D10_RTV_DIMENSION_BUFFER,
			Texture1D = D3D10_RTV_DIMENSION_TEXTURE1D,
			Texture1DArray = D3D10_RTV_DIMENSION_TEXTURE1DARRAY,
			Texture2D = D3D10_RTV_DIMENSION_TEXTURE2D,
			Texture2DArray = D3D10_RTV_DIMENSION_TEXTURE2DARRAY,
			Texture2DMultisampled = D3D10_RTV_DIMENSION_TEXTURE2DMS,
			Texture2DMultisampledArray = D3D10_RTV_DIMENSION_TEXTURE2DMSARRAY,
			Texture3D = D3D10_RTV_DIMENSION_TEXTURE3D
		};
		
		/// <summary>Identifies the type of the resource that will be viewed.</summary>
		/// <unmanaged>D3D10_SRV_DIMENSION</unmanaged>
		public enum class ShaderResourceViewDimension : System::Int32
		{
			Unknown = D3D10_SRV_DIMENSION_UNKNOWN,
			Buffer = D3D10_SRV_DIMENSION_BUFFER,
			Texture1D = D3D10_SRV_DIMENSION_TEXTURE1D,
			Texture1DArray = D3D10_SRV_DIMENSION_TEXTURE1DARRAY,
			Texture2D = D3D10_SRV_DIMENSION_TEXTURE2D,
			Texture2DArray = D3D10_SRV_DIMENSION_TEXTURE2DARRAY,
			Texture2DMultisampled = D3D10_SRV_DIMENSION_TEXTURE2DMS,
			Texture2DMultisampledArray = D3D10_SRV_DIMENSION_TEXTURE2DMSARRAY,
			Texture3D = D3D10_SRV_DIMENSION_TEXTURE3D,
			TextureCube = D3D10_SRV_DIMENSION_TEXTURECUBE
		};

		/// <summary>
		/// Flags that control the behavior of <see cref="Sprite"/> rendering.
		/// </summary>
		/// <unmanaged>D3DX10_SPRITE_FLAG</unmanaged>
		[System::Flags]
		public enum class SpriteFlags : System::Int32
		{
			/// <summary>
			/// No flags.
			/// </summary>
			None = 0,

			/// <summary>
			/// Group sprites by texture before rendering; improves performance when many sprites
			/// share textures.
			/// </summary>
			GroupByTexture = D3DX10_SPRITE_SORT_TEXTURE,
			
			/// <summary>
			/// Sort the sprites from back to front based on their depth; sprites further away from the
			/// camera will be rendered first.
			/// </summary>
			SortBackToFront = D3DX10_SPRITE_SORT_DEPTH_BACK_TO_FRONT,
			
			/// <summary>
			/// Sort the sprites from front to back based on their depth; sprites closer to the
			/// camera will be rendered first.
			/// </summary>
			SortFrontToBack = D3DX10_SPRITE_SORT_DEPTH_FRONT_TO_BACK,
			
			/// <summary>
			/// Saves render state when sprites are rendered, and restores the previous state once rendering
			/// is complete.
			/// </summary>
			SaveState = D3DX10_SPRITE_SAVE_STATE,
			
			/// <summary>
			/// Increases the reference count of textures when DrawSpritesBuffered is called.
			/// </summary>
			IncreaseTextureReferenceCount = D3DX10_SPRITE_ADDREF_TEXTURES
		};

		/// <summary>The stencil operations that can be performed during depth-stencil testing.</summary>
		/// <unmanaged>D3D10_STENCIL_OP</unmanaged>
		public enum class StencilOperation : System::Int32
		{
			Keep = D3D10_STENCIL_OP_KEEP,
			Zero = D3D10_STENCIL_OP_ZERO,
			Replace = D3D10_STENCIL_OP_REPLACE,
			IncrementAndClamp = D3D10_STENCIL_OP_INCR_SAT,
			DecrementAndClamp = D3D10_STENCIL_OP_DECR_SAT,
			Invert = D3D10_STENCIL_OP_INVERT,
			Increment = D3D10_STENCIL_OP_INCR,
			Decrement = D3D10_STENCIL_OP_DECR
		};

		public enum class TextureAddressMode : System::Int32
		{
			Wrap = D3D10_TEXTURE_ADDRESS_WRAP,
			Mirror = D3D10_TEXTURE_ADDRESS_MIRROR,
			Clamp = D3D10_TEXTURE_ADDRESS_CLAMP,
			Border = D3D10_TEXTURE_ADDRESS_BORDER,
			MirrorOnce = D3D10_TEXTURE_ADDRESS_MIRROR_ONCE
		};
	}
}