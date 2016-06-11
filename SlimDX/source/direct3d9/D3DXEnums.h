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
	namespace Direct3D9
	{
#ifdef XMLDOCS
		ref class AnimationOutput;
#endif

		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.


		/// <summary>
		/// Specifies which members of the <see cref="AnimationOutput"/> structure are valid.
		/// </summary>
		/// <unmanaged>None</unmanaged>
		[System::Flags]
		public enum class AnimationOutputFlags : System::Int32
		{
			/// <summary>
			/// None of the members are valid.
			/// </summary>
			None = 0,

			/// <summary>
			/// The transformation member is valid.
			/// </summary>
			Transformation = 1,

			/// <summary>
			/// The translation member is valid.
			/// </summary>
			Translation = 2,

			/// <summary>
			/// The scale member is valid.
			/// </summary>
			Scale = 4,

			/// <summary>
			/// The rotation member is valid.
			/// </summary>
			Rotation = 8
		};

		/// <summary>
		/// Flags used to obtain callback information.
		/// </summary>
		/// <unmanaged>D3DXCALLBACK_SEARCH_FLAGS</unmanaged>
		public enum class CallbackSearchFlags : System::Int32
		{
			/// <summary>
			/// Exclude callbacks located at the initial position from the search.
			/// </summary>
			SearchExcludingInitialPosition = D3DXCALLBACK_SEARCH_EXCLUDING_INITIAL_POSITION,

			/// <summary>
			/// Reverse the callback search direction. 
			/// </summary>
			SearchBehindInitialPosition = D3DXCALLBACK_SEARCH_BEHIND_INITIAL_POSITION
		};

		/// <summary>
		/// The following flags are used to specify which channels in a texture to operate on.
		/// </summary>
		/// <unmanaged>D3DX_CHANNEL</unmanaged>
		[System::Flags]
		public enum class Channel : System::Int32
		{
			/// <summary>
			/// Indicates the red channel should be used.
			/// </summary>
			Red = D3DX_CHANNEL_RED,

			/// <summary>
			/// Indicates the blue channel should be used.
			/// </summary>
			Blue = D3DX_CHANNEL_BLUE,

			/// <summary>
			/// Indicates the green channel should be used.
			/// </summary>
			Green = D3DX_CHANNEL_GREEN,

			/// <summary>
			/// Indicates the alpha channel should be used.
			/// </summary>
			Alpha = D3DX_CHANNEL_ALPHA,

			/// <summary>
			/// Indicates the luminances of the red, green, and blue channels should be used.
			/// </summary>
			Luminance = D3DX_CHANNEL_LUMINANCE
		};

		/// <summary>
		/// Defines operations to perform on vertices in preparation for mesh cleaning.
		/// </summary>
		/// <unmanaged>D3DXCLEANTYPE</unmanaged>
		[System::Flags]
		public enum class CleanType : System::Int32
		{
			/// <summary>
			/// Merge triangles that share the same vertex indices but have face normals pointing
			/// in opposite directions (back-facing triangles). Unless the triangles are not split by 
			/// adding a replicated vertex, mesh adjacency data from the two triangles may conflict. 
			/// </summary>
			BackFacing = D3DXCLEAN_BACKFACING,

			/// <summary>
			/// If a vertex is the apex of two triangle fans (a bowtie) and mesh operations will affect
			/// one of the fans, then split the shared vertex into two new vertices. Bowties can cause problems
			/// for operations such as mesh simplification that remove vertices, because removing one vertex
			/// affects two distinct sets of triangles. 
			/// </summary>
			Bowties = D3DXCLEAN_BOWTIES,

			/// <summary>
			/// Use this flag to prevent infinite loops during skinning setup mesh operations.
			/// </summary>
			Skinning = D3DXCLEAN_SKINNING,

			/// <summary>
			/// Use this flag to prevent infinite loops during mesh optimization operations.
			/// </summary>
			Optimization = D3DXCLEAN_OPTIMIZATION,

			/// <summary>
			/// Use this flag to prevent infinite loops during mesh simplification operations.
			/// </summary>
			Simplification = D3DXCLEAN_SIMPLIFICATION
		};

		/// <summary>
		/// Effect data types. The data is contained in the Value member of EffectDefault.
		/// </summary>
		/// <unmanaged>D3DXEFFECTDEFAULTTYPE</unmanaged>
		public enum class EffectDefaultType : System::Int32
		{
			/// <summary>
			/// The data type is a NULL-terminated ASCII text string. 
			/// </summary>
			String = D3DXEDT_STRING,

			/// <summary>
			/// The data type is an array of type float. The number of float types in
			/// the array is specified by Value.Length in EffectDefault.
			/// </summary>
			Floats = D3DXEDT_FLOATS,

			/// <summary>
			/// The data type is a DWORD. 
			/// </summary>
			Dword = D3DXEDT_DWORD,
		};

		/// <summary>
		/// Describes the type of events that can be keyed by the animation controller.
		/// </summary>
		/// <unmanaged>D3DXEVENT_TYPE</unmanaged>
		public enum class EventType : System::Int32
		{
			/// <summary>
			/// Track speed.
			/// </summary>
			TrackSpeed = D3DXEVENT_TRACKSPEED,

			/// <summary>
			/// Track weight.
			/// </summary>
			TrackWeight = D3DXEVENT_TRACKWEIGHT,

			/// <summary>
			/// Track position.
			/// </summary>
			TrackPosition = D3DXEVENT_TRACKPOSITION,

			/// <summary>
			/// Enable flag.
			/// </summary>
			TrackEnable = D3DXEVENT_TRACKENABLE,

			/// <summary>
			/// Priority blend value.
			/// </summary>
			PriorityBlend = D3DXEVENT_PRIORITYBLEND
		};

		/// <summary>
		/// Flags used to specify what filters to use when processing an image.
		/// </summary>
		/// <unmanaged>D3DX_FILTER</unmanaged>
		[System::Flags]
		public enum class Filter : System::Int32
		{
			/// <summary>
			/// Uses the default behavior.
			/// </summary>
			Default = static_cast<int>( D3DX_DEFAULT ),

			/// <summary>
			/// No scaling or filtering will take place. Pixels outside the bounds of the source image are assumed to be transparent black.
			/// </summary>
			None = D3DX_FILTER_NONE,

			/// <summary>
			/// Each destination pixel is computed by sampling the nearest pixel from the source image.
			/// </summary>
			Point = D3DX_FILTER_POINT,

			/// <summary>
			/// Each destination pixel is computed by sampling the four nearest pixels from the source
			/// image. This filter works best when the scale on both axes is less than two.
			/// </summary>
			Linear = D3DX_FILTER_LINEAR,

			/// <summary>
			/// Every pixel in the source image contributes equally to the destination image. This is the slowest of the filters.
			/// </summary>
			Triangle = D3DX_FILTER_TRIANGLE,

			/// <summary>
			/// Each pixel is computed by averaging a 2x2(x2) box of pixels from the source image. 
			/// This filter works only when the dimensions of the destination are half those of the source,
			/// as is the case with mipmaps.
			/// </summary>
			Box = D3DX_FILTER_BOX,

			/// <summary>
			/// Pixels off the edge of the texture on the u-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorU = D3DX_FILTER_MIRROR_U,

			/// <summary>
			/// Pixels off the edge of the texture on the v-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorV = D3DX_FILTER_MIRROR_V,

			/// <summary>
			/// Pixels off the edge of the texture on the w-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorW = D3DX_FILTER_MIRROR_W,

			/// <summary>
			/// Specifying this flag is the same as specifying the MirrorU, MirrorV, and MirrorW flags.
			/// </summary>
			Mirror = D3DX_FILTER_MIRROR,

			/// <summary>
			/// The resulting image must be dithered using a 4x4 ordered dither algorithm.
			/// </summary>
			Dither = D3DX_FILTER_DITHER,

			/// <summary>
			/// Specifying this flag is the same as specifying the SrgbIn and SrgbOut flags.
			/// </summary>
			Srgb = D3DX_FILTER_SRGB,

			/// <summary>
			/// Input data is in sRGB (gamma 2.2) color space.
			/// </summary>
			SrgbIn = D3DX_FILTER_SRGB_IN,

			/// <summary>
			/// The output data is in sRGB (gamma 2.2) color space.
			/// </summary>
			SrgbOut = D3DX_FILTER_SRGB_OUT
		};

		/// <summary>
		/// Options for saving and creating effects.
		/// </summary>
		/// <unmanaged>D3DXFX</unmanaged>
		[System::Flags]
		public enum class FX : System::Int32
		{
			/// <summary>
			/// No flags.
			/// </summary>
			None = 0,

			/// <summary>
			/// No state is saved when calling Effect.Begin or restored when calling Effect.End. 
			/// </summary>
			DoNotSaveState = D3DXFX_DONOTSAVESTATE,

			/// <summary>
			/// A stateblock saves state when calling Effect.Begin and restores state when calling Effect.End.
			/// </summary>
			DoNotSaveShaderState = D3DXFX_DONOTSAVESHADERSTATE,

			/// <summary>
			/// A stateblock saves state (except shaders and shader constants) when
			/// calling Effect.Begin and restores state when calling Effect.End.
			/// </summary>
			DoNotSaveSamplerState = D3DXFX_DONOTSAVESAMPLERSTATE,
		};

		/// <summary>
		/// Describes the supported image file formats.
		/// </summary>
		/// <unmanaged>D3DXIMAGE_FILEFORMAT</unmanaged>
		public enum class ImageFileFormat : System::Int32
		{
			/// <summary>
			/// Windows bitmap (BMP) file format. 
			/// </summary>
			Bmp = D3DXIFF_BMP,

			/// <summary>
			/// Joint Photographics Experts Group (JPEG) compressed file format.
			/// </summary>
			Jpg = D3DXIFF_JPG,

			/// <summary>
			/// Truevision (Targa, or TGA) image file format.
			/// </summary>
			Tga = D3DXIFF_TGA,

			/// <summary>
			/// Portable Network Graphics (PNG) file format.
			/// </summary>
			Png = D3DXIFF_PNG,

			/// <summary>
			/// DirectDraw surface (DDS) file format.
			/// </summary>
			Dds = D3DXIFF_DDS,

			/// <summary>
			/// Portable pixmap (PPM) file format.
			/// </summary>
			Ppm = D3DXIFF_PPM,

			/// <summary>
			/// Windows device-independent bitmap (DIB) file format.
			/// </summary>
			Dib = D3DXIFF_DIB,

			/// <summary>
			/// High dynamic range (HDR) file format.
			/// </summary>
			Hdr = D3DXIFF_HDR,

			/// <summary>
			/// Portable float map file format.
			/// </summary>
			Pfm = D3DXIFF_PFM,
		};

		/// <summary>
		/// Describes the location for the include file.
		/// </summary>
		/// <unmanaged>D3DXINCLUDE_TYPE</unmanaged>
		public enum class IncludeType : System::Int32
		{
			/// <summary>
			/// Look in the local project for the include file.
			/// </summary>
			Local = D3DXINC_LOCAL,

			/// <summary>
			/// Look in the system path for the include file.
			/// </summary>
			System = D3DXINC_SYSTEM,
		};

		/// <summary>
		/// Defines the type of mesh data present in MeshData.
		/// </summary>
		/// <unmanaged>D3DXMESHDATATYPE</unmanaged>
		public enum class MeshDataType : System::Int32
		{
			/// <summary>
			/// The data type is a mesh. See Mesh.
			/// </summary>
			Mesh = D3DXMESHTYPE_MESH,

			/// <summary>
			/// The data type is a progressive mesh. See ProgressiveMesh.
			/// </summary>
			ProgressiveMesh = D3DXMESHTYPE_PMESH,

			/// <summary>
			/// The data type is a patch mesh. See PatchMesh.
			/// </summary>
			PatchMesh = D3DXMESHTYPE_PATCHMESH
		};

		/// <summary>
		/// Flags used to specify creation options for a mesh.
		/// </summary>
		/// <unmanaged>D3DXMESH</unmanaged>
		[System::Flags]
		public enum class MeshFlags : System::Int32
		{
			/// <summary>
			/// The mesh has 32-bit indices instead of 16-bit indices.
			/// </summary>
			Use32Bit = D3DXMESH_32BIT,

			/// <summary>
			/// Use the DoNotClip usage flag for vertex and index buffers.
			/// </summary>
			DoNotClip = D3DXMESH_DONOTCLIP,

			/// <summary>
			/// Use the Points usage flag for vertex and index buffers.
			/// </summary>
			Points = D3DXMESH_POINTS,

			/// <summary>
			/// Use the RTPatches usage flag for vertex and index buffers. 
			/// </summary>
			RTPatches = D3DXMESH_RTPATCHES,

			/// <summary>
			/// Specifying this flag causes the vertex and index buffer of the mesh to be created with the NPatches
			/// flag. This is required if the mesh object is to be rendered using N-patch enhancement using Direct3D. 
			/// </summary>
			NPatches = D3DXMESH_NPATCHES,

			/// <summary>
			/// Use the SystemMemory usage flag for vertex buffers.
			/// </summary>
			VertexBufferSystemMemory = D3DXMESH_VB_SYSTEMMEM,

			/// <summary>
			/// Use the Managed usage flag for vertex buffers. 
			/// </summary>
			VertexBufferManaged = D3DXMESH_VB_MANAGED,

			/// <summary>
			/// Use the WriteOnly usage flag for vertex buffers.
			/// </summary>
			VertexBufferWriteOnly = D3DXMESH_VB_WRITEONLY,

			/// <summary>
			/// Use the Dynamic usage flag for vertex buffers.
			/// </summary>
			VertexBufferDynamic = D3DXMESH_VB_DYNAMIC,

			/// <summary>
			/// Use the SoftwareProcessing usage flag for vertex buffers.
			/// </summary>
			VertexBufferSoftware = D3DXMESH_VB_SOFTWAREPROCESSING,

			/// <summary>
			/// Use the SystemMemory usage flag for index buffers.
			/// </summary>
			IndexBufferSystemMemory = D3DXMESH_IB_SYSTEMMEM,

			/// <summary>
			/// Use the Managed usage flag for index buffers.
			/// </summary>
			IndexBufferManaged = D3DXMESH_IB_MANAGED,

			/// <summary>
			/// Use the WriteOnly usage flag for index buffers.
			/// </summary>
			IndexBufferWriteOnly = D3DXMESH_IB_WRITEONLY,

			/// <summary>
			/// Use the Dynamic usage flag for index buffers.
			/// </summary>
			IndexBufferDynamic = D3DXMESH_IB_DYNAMIC,

			/// <summary>
			/// Use the SoftwareProcessing usage flag for index buffers.
			/// </summary>
			IndexBufferSoftware = D3DXMESH_IB_SOFTWAREPROCESSING,

			/// <summary>
			/// Forces the cloned meshes to share vertex buffers.
			/// </summary>
			VertexBufferShare = D3DXMESH_VB_SHARE,

			/// <summary>
			/// Use hardware processing only. For mixed-mode device, this flag will cause the system 
			/// to use hardware (if supported in hardware) or will default to software processing.
			/// </summary>
			UseHardwareOnly = D3DXMESH_USEHWONLY,

			/// <summary>
			/// Equivalent to specifying both VertexBufferSystemMemory and IndexBufferSystemMemory.
			/// </summary>
			SystemMemory = D3DXMESH_SYSTEMMEM,

			/// <summary>
			/// Equivalent to specifying both VertexBufferManaged and IndexBufferManaged.
			/// </summary>
			Managed = D3DXMESH_MANAGED,

			/// <summary>
			/// Equivalent to specifying both VertexBufferDynamic and IndexBufferDynamic.
			/// </summary>
			WriteOnly = D3DXMESH_WRITEONLY,

			/// <summary>
			/// Equivalent to specifying both D3DXMESH_VB_DYNAMIC and D3DXMESH_IB_DYNAMIC.
			/// </summary>
			Dynamic = D3DXMESH_DYNAMIC,

			/// <summary>
			/// Equivalent to specifying both D3DXMESH_VB_SOFTWAREPROCESSING and D3DXMESH_IB_SOFTWAREPROCESSING.
			/// </summary>
			Software = D3DXMESH_SOFTWAREPROCESSING,
		};

		/// <summary>
		/// Specifies the type of mesh optimization to be performed.
		/// </summary>
		/// <unmanaged>D3DXMESHOPT</unmanaged>
		[System::Flags]
		public enum class MeshOptimizeFlags : System::Int32
		{
			/// <summary>
			/// Reorders faces to remove unused vertices and faces.
			/// </summary>
			Compact = D3DXMESHOPT_COMPACT,

			/// <summary>
			/// Reorders faces to optimize for fewer attribute bundle state changes and enhanced BaseMesh.DrawSubset performance.
			/// </summary>
			AttributeSort = D3DXMESHOPT_ATTRSORT,

			/// <summary>
			/// Reorders faces to increase the cache hit rate of vertex caches.
			/// </summary>
			VertexCache = D3DXMESHOPT_VERTEXCACHE,

			/// <summary>
			/// Reorders faces to maximize length of adjacent triangles.
			/// </summary>
			StripReorder = D3DXMESHOPT_STRIPREORDER,

			/// <summary>
			/// Optimize the faces only; do not optimize the vertices.
			/// </summary>
			IgnoreVertices = D3DXMESHOPT_IGNOREVERTS,

			/// <summary>
			/// While attribute sorting, do not split vertices that are shared between attribute groups.
			/// </summary>
			DoNotSplit = D3DXMESHOPT_DONOTSPLIT,

			/// <summary>
			/// Affects the vertex cache size. Using this flag specifies a default vertex cache size that works well on legacy hardware.
			/// </summary>
			DeviceIndependent = D3DXMESHOPT_DEVICEINDEPENDENT,
		};

		/// <summary>
		/// Specifies simplification options for a mesh.
		/// </summary>
		/// <unmanaged>D3DXMESHSIMP</unmanaged>
		public enum class MeshSimplification : System::Int32
		{
			/// <summary>
			/// The mesh will be simplified by the number of vertices specified in the MinValue parameter.
			/// </summary>
			Vertex = D3DXMESHSIMP_VERTEX,

			/// <summary>
			/// The mesh will be simplified by the number of faces specified in the MinValue parameter.
			/// </summary>
			Face = D3DXMESHSIMP_FACE
		};

		/// <summary>
		/// Normal maps generation constants.
		/// </summary>
		/// <unmanaged>D3DX_NORMALMAP</unmanaged>
		[System::Flags]
		public enum class NormalMapFlags : System::Int32
		{
			/// <summary>
			/// Indicates that pixels off the edge of the texture on the u-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorU = D3DX_NORMALMAP_MIRROR_U,

			/// <summary>
			/// Indicates that pixels off the edge of the texture on the v-axis should be mirrored, not wrapped.
			/// </summary>
			MirrorV = D3DX_NORMALMAP_MIRROR_V,

			/// <summary>
			/// Same as specifying the MirrorU and MirrorV flags.
			/// </summary>
			Mirror = D3DX_NORMALMAP_MIRROR,

			/// <summary>
			/// Inverts the direction of each normal.
			/// </summary>
			InvertSign = D3DX_NORMALMAP_INVERTSIGN,

			/// <summary>
			/// Computes the per-pixel occlusion term and encodes it into the alpha. An alpha of 1 means that the
			/// pixel is not obscured in any way, and an alpha of 0 means that the pixel is completely obscured.
			/// </summary>
			ComputeOcclusion = D3DX_NORMALMAP_COMPUTE_OCCLUSION
		};
		
		/// <summary>
		/// The type of object.
		/// </summary>
		/// <unmanaged>D3DXPARAMETER_CLASS</unmanaged>
		public enum class ParameterClass : System::Int32
		{
			/// <summary>
			/// Constant is a scalar.
			/// </summary>
			Scalar = D3DXPC_SCALAR,

			/// <summary>
			/// Constant is a vector.
			/// </summary>
			Vector = D3DXPC_VECTOR,

			/// <summary>
			/// Constant is a row major matrix.
			/// </summary>
			MatrixRows = D3DXPC_MATRIX_ROWS,

			/// <summary>
			/// Constant is a column major matrix.
			/// </summary>
			MatrixColumns = D3DXPC_MATRIX_COLUMNS,

			/// <summary>
			/// Constant is either a texture, shader, or a string.
			/// </summary>
			Object = D3DXPC_OBJECT,

			/// <summary>
			/// Constant is a structure.
			/// </summary>
			Struct = D3DXPC_STRUCT,
		};

		/// <summary>
		/// These flags provide additional information about effect parameters.
		/// </summary>
		/// <unmanaged>D3DX_PARAMETER</unmanaged>
		[System::Flags]
		public enum class ParameterFlags : System::Int32
		{
			/// <summary>
			/// This parameter is not marked.
			/// </summary>
			None = 0,

			/// <summary>
			/// The value of a parameter will be shared by all effects in the same namespace.
			/// Changing the value in one effect will change it in all shared effects.
			/// </summary>
			Shared = D3DX_PARAMETER_SHARED,

			/// <summary>
			/// This parameter is marked as a literal value.
			/// </summary>
			Literal = D3DX_PARAMETER_LITERAL,

			/// <summary>
			/// This parameter is marked as an annotation.
			/// </summary>
			Annotation = D3DX_PARAMETER_ANNOTATION,
		};

		/// <summary>
		/// Describes the data contained by the enumeration.
		/// </summary>
		/// <unmanaged>D3DXPARAMETER_TYPE</unmanaged>
		public enum class ParameterType : System::Int32
		{
			/// <summary>
			/// Parameter is a void pointer.
			/// </summary>
			Void = D3DXPT_VOID,

			/// <summary>
			/// Parameter is a Boolean. Any non-zero value passed into ConstantTable.SetBool, ConstantTable.SetBoolArray,
			/// ConstantTable.SetValue, ConstantTable.SetVector, or ConstantTable.SetVectorArray will be mapped to 1 (true)
			/// before being written into the constant table; otherwise, the value will be set to 0 in the constant table.
			/// </summary>
			Bool = D3DXPT_BOOL,

			/// <summary>
			/// Parameter is an integer. Any floating-point values passed into ConstantTable.SetValue,
			/// ConstantTable.SetVector, or ConstantTable::SetVectorArray will be rounded off (to zero decimal places)
			/// before being written into the constant table. 
			/// </summary>
			Int = D3DXPT_INT,

			/// <summary>
			/// Parameter is a floating-point number.
			/// </summary>
			Float = D3DXPT_FLOAT,

			/// <summary>
			/// Parameter is a string.
			/// </summary>
			String = D3DXPT_STRING,

			/// <summary>
			/// Parameter is a texture.
			/// </summary>
			Texture = D3DXPT_TEXTURE,

			/// <summary>
			/// Parameter is a 1D texture.
			/// </summary>
			Texture1D = D3DXPT_TEXTURE1D,

			/// <summary>
			/// Parameter is a 2D texture.
			/// </summary>
			Texture2D = D3DXPT_TEXTURE2D,

			/// <summary>
			/// Parameter is a DD texture.
			/// </summary>
			Texture3D = D3DXPT_TEXTURE3D,

			/// <summary>
			/// Parameter is a cube texture.
			/// </summary>
			TextureCube = D3DXPT_TEXTURECUBE,

			/// <summary>
			/// Parameter is a sampler.
			/// </summary>
			Sampler = D3DXPT_SAMPLER,

			/// <summary>
			/// Parameter is a 1D sampler.
			/// </summary>
			Sampler1D = D3DXPT_SAMPLER1D,

			/// <summary>
			/// Parameter is a 2D sampler.
			/// </summary>
			Sampler2D = D3DXPT_SAMPLER2D,

			/// <summary>
			/// Parameter is a 3D sampler.
			/// </summary>
			Sampler3D = D3DXPT_SAMPLER3D,

			/// <summary>
			/// Parameter is a cube sampler.
			/// </summary>
			SamplerCube = D3DXPT_SAMPLERCUBE,

			/// <summary>
			/// Parameter is a pixel shader.
			/// </summary>
			PixelShader = D3DXPT_PIXELSHADER,

			/// <summary>
			/// Parameter is a vertex shader.
			/// </summary>
			VertexShader = D3DXPT_VERTEXSHADER,

			/// <summary>
			/// Parameter is a pixel shader fragment.
			/// </summary>
			PixelFragment = D3DXPT_PIXELFRAGMENT,

			/// <summary>
			/// Parameter is a vertex shader fragment.
			/// </summary>
			VertexFragment = D3DXPT_VERTEXFRAGMENT,

			/// <summary>
			/// Parameter is not supported.
			/// </summary>
			Unsupported = D3DXPT_UNSUPPORTED
		};

		/// <summary>
		/// Mesh patch types.
		/// </summary>
		/// <unmanaged>D3DXPATCHMESHTYPE</unmanaged>
		public enum class PatchMeshType : System::Int32
		{
			/// <summary>
			/// Rectangle patch mesh type.
			/// </summary>
			Rectangle = D3DXPATCHMESH_RECT,

			/// <summary>
			/// Triangle patch mesh type.
			/// </summary>
			Triangle = D3DXPATCHMESH_TRI,

			/// <summary>
			/// N-patch mesh type.
			/// </summary>
			NPatch = D3DXPATCHMESH_NPATCH
		};

		/// <summary>
		/// Defines the type of animation set looping modes used for playback.
		/// </summary>
		/// <unmanaged>D3DXPLAYBACK_TYPE</unmanaged>
		public enum class PlaybackType : System::Int32
		{
			/// <summary>
			/// The animation repeats endlessly.
			/// </summary>
			Loop = D3DXPLAY_LOOP,

			/// <summary>
			/// The animation plays once, and then it stops on the last frame.
			/// </summary>
			Once = D3DXPLAY_ONCE,

			/// <summary>
			/// The animation alternates endlessly between playing forward and playing backward.
			/// </summary>
			PingPong = D3DXPLAY_PINGPONG
		};

		/// <summary>
		/// Data type of the register.
		/// </summary>
		/// <unmanaged>D3DXREGISTER_SET</unmanaged>
		public enum class RegisterSet : System::Int32
		{
			/// <summary>
			/// Boolean value.
			/// </summary>
			Bool = D3DXRS_BOOL,

			/// <summary>
			/// 4D integer number.
			/// </summary>
			Int4 = D3DXRS_INT4,

			/// <summary>
			/// 4D floating-point number.
			/// </summary>
			Float4 = D3DXRS_FLOAT4,

			/// <summary>
			/// The register contains 4D sampler data.
			/// </summary>
			Sampler = D3DXRS_SAMPLER,
		};

		/// <summary>
		/// Flags used for parsing, compiling, or assembling shaders.
		/// </summary>
		/// <unmanaged>D3DXSHADER</unmanaged>
		[System::Flags]
		public enum class ShaderFlags : System::Int32
		{
			/// <summary>
			/// No flags.
			/// </summary>
			None = 0,

			/// <summary>
			/// Unless explicitly specified, matrices will be packed in column-major order (each vector will be in a single
			/// column) when passed to and from the shader. This is generally more efficient because it allows vector-matrix
			/// multiplication to be performed using a series of dot products.
			/// </summary>
			PackMatrixColumnMajor = D3DXSHADER_PACKMATRIX_COLUMNMAJOR,

			/// <summary>
			/// Unless explicitly specified, matrices will be packed in row-major order (each vector will be in a single
			/// row) when passed to or from the shader.
			/// </summary>
			PackMatrixRowMajor = D3DXSHADER_PACKMATRIX_ROWMAJOR,

			/// <summary>
			/// This is a hint to the compiler to avoid using flow-control instructions.
			/// </summary>
			AvoidFlowControl = D3DXSHADER_AVOID_FLOW_CONTROL,

			/// <summary>
			/// Insert debug filename, line numbers, and type and symbol information during shader compile.
			/// </summary>
			Debug = D3DXSHADER_DEBUG,

			/// <summary>
			/// Compile ps_1_x shaders as ps_2_0. Effects that specify ps_1_x targets will instead compile to ps_2_0
			/// targets because this is the minimum shader version supported by the DirectX 10 shader compiler. This flag
			/// has no effect when used with higher level compile targets.
			/// </summary>
			EnableBackwardsCompatibility = D3DXSHADER_ENABLE_BACKWARDS_COMPATIBILITY,

			/// <summary>
			/// Force the compiler to compile against the next highest available software target for pixel shaders.
			/// This flag also turns optimizations off and debugging on.
			/// </summary>
			ForcePSSoftwareNoOpt = D3DXSHADER_FORCE_PS_SOFTWARE_NOOPT,

			/// <summary>
			/// Force the compiler to compile against the next highest available software target for vertex shaders.
			/// This flag also turns optimizations off and debugging on.
			/// </summary>
			ForceVSSoftwareNoOpt = D3DXSHADER_FORCE_VS_SOFTWARE_NOOPT,

			/// <summary>
			/// Disable optimizations that may cause the output of a compiled shader program to differ from the output
			/// of a program compiled with the DirectX 9 shader compiler due to small precision erros in floating point math.
			/// </summary>
			IeeeStrictness = D3DXSHADER_IEEE_STRICTNESS,

			/// <summary>
			/// Disables preshaders. The compiler will not pull out static expressions for evaluation on the host CPU.
			/// Additionally, the compiler will not loft any expressions when compiling stand-alone functions.
			/// </summary>
			NoPreshader = D3DXSHADER_NO_PRESHADER,

			/// <summary>
			/// Lowest optimization level. May produce slower code but will do so more quickly.
			/// This may be useful in a highly iterative shader development cycle.
			/// </summary>
			OptimizationLevel0 = D3DXSHADER_OPTIMIZATION_LEVEL0,

			/// <summary>
			/// Second lowest optimization level.
			/// </summary>
			OptimizationLevel1 = D3DXSHADER_OPTIMIZATION_LEVEL1,

			/// <summary>
			/// Second highest optimization level.
			/// </summary>
			OptimizationLevel2 = D3DXSHADER_OPTIMIZATION_LEVEL2,

			/// <summary>
			/// Highest optimization level. Will produce best possible code but may take significantly longer to do so.
			/// This will be useful for final builds of an application where performance is the most important factor.
			/// </summary>
			OptimizationLevel3 = D3DXSHADER_OPTIMIZATION_LEVEL3,

			/// <summary>
			/// Force all computations in the resulting shader to occur at partial precision
			/// This may result in faster evaluation of shaders on some hardware.
			/// </summary>
			PartialPrecision = D3DXSHADER_PARTIALPRECISION,

			/// <summary>
			/// This is a hint to the compiler to prefer using flow-control instructions.
			/// </summary>
			PreferFlowControl = D3DXSHADER_PREFER_FLOW_CONTROL,

			/// <summary>
			/// Instruct the compiler to skip optimization steps during code generation.
			/// Unless you are trying to isolate a problem in your code and you suspect the
			/// compiler, using this option is not recommended.
			/// </summary>
			SkipValidation = D3DXSHADER_SKIPVALIDATION,

			/// <summary>
			/// Do not validate the generated code against known capabilities and constraints.
			/// This option is recommended only when compiling shaders that are known to work
			/// (that is, shaders that have compiled before without this option). Shaders are
			/// always validated by the runtime before they are set to the device.
			/// </summary>
			SkipOptimization = D3DXSHADER_SKIPOPTIMIZATION,

			/// <summary>
			/// Enable the use of the original Direct3D 9 HLSL compiler. OCT2006_d3dx9_31_x86.cab or
			/// OCT2006_d3dx9_31_x64.cab must be included as part of the applications redist. This flag
			/// is required to compile ps_1_x shaders without using the promotion flag to ps_2_0. Specifying
			/// this flag when obtaining an EffectCompiler interface causes subsequent calls to
			/// EffectCompiler.CompileEffect and EffectCompiler/CompileShader through this object
			/// to use the legacy compiler.
			/// </summary>
			UseLegacyD3DX9_31Dll = D3DXSHADER_USE_LEGACY_D3DX9_31_DLL
		};

		/// <summary>
		/// Flags used to specify sprite rendering options to the flags parameter in the Sprite.Begin method.
		/// </summary>
		/// <unmanaged>D3DXSPRITE</unmanaged>
		[System::Flags]
		public enum class SpriteFlags : System::Int32
		{
			/// <summary>
			/// No flags.
			/// </summary>
			None = 0,

			/// <summary>
			/// The device state is not to be saved or restored when Sprite.Begin or Sprite.End is called.
			/// </summary>
			DoNotSaveState = D3DXSPRITE_DONOTSAVESTATE,

			/// <summary>
			/// The device render state is not to be changed when Sprite.Begin is called. The device is assumed
			/// to be in a valid state to draw vertices containing UsageIndex = 0 in the DeclarationUsage.Position,
			/// DeclarationUsage.TexCoord, and DeclarationUsage.Color data.
			/// </summary>
			DoNotModifyRenderState = D3DXSPRITE_DONOTMODIFY_RENDERSTATE,

			/// <summary>
			/// The world, view, and projection transforms are not modified. The transforms currently set to the
			/// device are used to transform the sprites when the batched sprites are drawn (when Sprite.Flush or
			/// Sprite.End is called). If this flag is not specified, then world, view, and projection
			/// transforms are modified so that sprites are drawn in screen-space coordinates.
			/// </summary>
			ObjectSpace = D3DXSPRITE_OBJECTSPACE,

			/// <summary>
			/// Each sprite will be rotated about its center so that it is facing the viewer.
			/// Sprite.SetWorldViewLH or Sprite.SetWorldViewRH must be called first.
			/// </summary>
			Billboard = D3DXSPRITE_BILLBOARD,

			/// <summary>
			/// Enables alpha blending with RenderState.AlphaTestEnable set to true (for nonzero alpha).
			/// Blend.SourceAlpha will be the source blend state, and Blend.InverseSourceAlpha will be the destination
			/// blend state in calls to Device.SetRenderState. Font expects this flag to be set when drawing text.
			/// </summary>
			AlphaBlend = D3DXSPRITE_ALPHABLEND,

			/// <summary>
			/// Sort sprites by texture prior to drawing. This can improve performance when drawing
			/// non-overlapping sprites of uniform depth. You may also combine SortTexture with either
			/// SortDepthFrontToBack or SortDepthBackToFront. This will sort the list of sprites by depth
			/// first and texture second.
			/// </summary>
			SortTexture = D3DXSPRITE_SORT_TEXTURE,

			/// <summary>
			/// Sprites are sorted by depth in front-to-back order prior to drawing. This procedure is
			/// recommended when drawing opaque sprites of varying depths. You may combine SortDepthFrontToBack
			/// with SortTexture to sort first by depth, and second by texture.
			/// </summary>
			SortDepthFrontToBack = D3DXSPRITE_SORT_DEPTH_FRONTTOBACK,

			/// <summary>
			/// Sprites are sorted by depth in back-to-front order prior to drawing. This procedure is
			/// recommended when drawing transparent sprites of varying depths. You may combine SortDepthBackToFront
			/// with SortTexture to sort first by depth, and second by texture.
			/// </summary>
			SortDepthBackToFront = D3DXSPRITE_SORT_DEPTH_BACKTOFRONT,

			/// <summary>
			/// Disables calling AddRef() on every draw, and Release() on Flush() for better performance.
			/// </summary>
			DoNotAddRefTexture = D3DXSPRITE_DO_NOT_ADDREF_TEXTURE,
		};

		/// <summary>
		/// Defines settings used for mesh tangent frame computations.
		/// </summary>
		/// <unmanaged>D3DXTANGENT</unmanaged>
		[System::Flags]
		public enum class TangentOptions : System::Int32
		{
			/// <summary>
			/// No options.
			/// </summary>
			None = 0,

			/// <summary>
			/// Texture coordinate values in the U direction are between 0 and 1. In this case a texture
			/// coordinate set will be chosen that minimizes the perimeter of the triangle.
			/// </summary>
			WrapU = D3DXTANGENT_WRAP_U,

			/// <summary>
			/// Texture coordinate values in the V direction are between 0 and 1. In this case a texture
			/// coordinate set will be chosen that minimizes the perimeter of the triangle.
			/// </summary>
			WrapV = D3DXTANGENT_WRAP_V,

			/// <summary>
			/// Texture coordinate values in both U and V directions are between 0 and 1. In this case a texture
			/// coordinate set will be chosen that minimizes the perimeter of the triangle.
			/// </summary>
			WrapUV = D3DXTANGENT_WRAP_UV,

			/// <summary>
			/// Do not normalize partial derivatives with respect to texture coordinates. If not normalized,
			/// the scale of the partial derivatives is proportional to the scale of the 3D model divided by the
			/// scale of the triangle in (U, V) space. This scale value provides a measure of how much the
			/// texture is stretched in a given direction. The resulting vector length is a weighted sum of
			/// the lengths of the partial derivatives.
			/// </summary>
			DontNormalizePartials = D3DXTANGENT_DONT_NORMALIZE_PARTIALS,

			/// <summary>
			/// Do not transform texture coordinates to orthogonal Cartesian coordinates. Mutually exclusive
			/// with OrthogonalizeFromU and OrthogonalizeFromV.
			/// </summary>
			DontOrthogonalize = D3DXTANGENT_DONT_ORTHOGONALIZE,

			/// <summary>
			/// Compute the partial derivative with respect to texture coordinate V independently for each vertex,
			/// and then compute the partial derivative with respect to U as the cross product of the partial derivative
			/// with respect to V and the normal vector. Mutually exclusive with DontOrthogonalize and OrthogonalizeFromU.
			/// </summary>
			OrthogonalizeFromV = D3DXTANGENT_ORTHOGONALIZE_FROM_V,

			/// <summary>
			/// Compute the partial derivative with respect to texture coordinate u independently for each vertex,
			/// and then compute the partial derivative with respect to V as the cross product of the normal vector
			/// and the partial derivative with respect to U. Mutually exclusive with DontOrthogonalize and OrthogonalizeFromV.
			/// </summary>
			OrthogonalizeFromU = D3DXTANGENT_ORTHOGONALIZE_FROM_U,

			/// <summary>
			/// Weight the direction of the computed per-vertex normal or partial derivative vector according to the areas
			/// of triangles attached to that vertex. Mutually exclusive with WeightEqual.
			/// </summary>
			WeightByArea = D3DXTANGENT_WEIGHT_BY_AREA,

			/// <summary>
			/// Compute a unit-length normal vector for each triangle of the input mesh.
			/// Mutually exclusive with WeightByArea.
			/// </summary>
			WeightEqual = D3DXTANGENT_WEIGHT_EQUAL,

			/// <summary>
			/// Vertices are ordered in a clockwise direction around each triangle.
			/// The computed normal vector direction is therefore inverted 180 degrees from
			/// the direction computed using counterclockwise vertex ordering. 
			/// </summary>
			WindCW = D3DXTANGENT_WIND_CW,

			/// <summary>
			/// Compute the per-vertex normal vector for each triangle of the input mesh,
			/// and ignore any normal vectors already in the input mesh. 
			/// </summary>
			CalculateNormals = D3DXTANGENT_CALCULATE_NORMALS,

			/// <summary>
			/// The results are stored in the original input mesh, and the output mesh is not used.
			/// </summary>
			GenerateInPlace = D3DXTANGENT_GENERATE_IN_PLACE,
		};

		/// <summary>
		/// Defines the priority type to which an animation track is assigned.
		/// </summary>
		/// <unmanaged>D3DXPRIORITY_TYPE</unmanaged>
		public enum class TrackPriority : System::Int32
		{
			/// <summary>
			/// Track should be blended with all the low-priority tracks before the
			/// low-priority blend is mixed with the high-priority blend.
			/// </summary>
			Low = D3DXPRIORITY_LOW,

			/// <summary>
			/// Track should be blended with all the high-priority tracks before the
			/// high-priority blend is mixed with the low-priority blend.
			/// </summary>
			High = D3DXPRIORITY_HIGH
		};

		/// <summary>
		/// Defines the transition style between values of a mesh animation.
		/// </summary>
		/// <unmanaged>D3DXTRANSITION_TYPE</unmanaged>
		public enum class TransitionType : System::Int32
		{
			/// <summary>
			/// Linear transition between values.
			/// </summary>
			Linear = D3DXTRANSITION_LINEAR,

			/// <summary>
			/// Ease-in, ease-out spline transition between values.
			/// </summary>
			EaseInEaseOut = D3DXTRANSITION_EASEINEASEOUT
		};

		public enum class UVAtlasQuality : System::Int32
		{
			Default = D3DXUVATLAS_DEFAULT,
			GeodesicFast = D3DXUVATLAS_GEODESIC_FAST,
			GeodesicQuality = D3DXUVATLAS_GEODESIC_QUALITY
		};

		/// <summary>
		/// Specifies options for welding together vertices.
		/// </summary>
		/// <unmanaged>D3DXWELDEPSILONSFLAGS</unmanaged>
		[System::Flags]
		public enum class WeldFlags : System::Int32
		{
			/// <summary>
			/// Weld together all vertices that are at the same location.
			/// </summary>
			WeldAll = D3DXWELDEPSILONS_WELDALL,

			/// <summary>
			/// If a given vertex component is within epsilon, modify partially matched vertices so that both components are identical. 
			/// If all components are equal, remove one of the vertices. 
			/// </summary>
			WeldPartialMatches = D3DXWELDEPSILONS_WELDPARTIALMATCHES,

			/// <summary>
			/// Instructs the weld to allow only modifications to vertices and not removal. This flag is 
			/// valid only if WeldPartialMatches is set. It is useful to modify vertices to be equal, 
			/// but not to allow vertices to be removed.
			/// </summary>
			DoNotRemoveVertices = D3DXWELDEPSILONS_DONOTREMOVEVERTICES,

			/// <summary>
			/// Instructs the weld not to split vertices that are in separate attribute groups.
			/// </summary>
			DoNotSplit = D3DXWELDEPSILONS_DONOTSPLIT
		};
	}
}