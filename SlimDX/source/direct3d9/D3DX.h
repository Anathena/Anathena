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

#include "../math/Vector3.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D9
	{
		value class VertexElement;
		enum class VertexFormat;
		enum class Format;

		/// <summary>
		/// Contains miscellaneous constants and methods for use with D3DX.
		/// </summary>
		/// <unmanaged>None</unmanaged>
		public ref class D3DX sealed
		{
		private:
			D3DX() { }

		public:
			/// <summary>
			/// The value used to signify that the default value for a parameter should be used.
			/// </summary>
			/// <unmanaged>D3DX_DEFAULT</unmanaged>
			literal int Default = D3DX_DEFAULT;

			/// <summary>
			/// The default value for non power-of-two textures.
			/// </summary>
			/// <unmanaged>D3DX_DEFAULT_NONPOW2</unmanaged>
			literal int DefaultNonPowerOf2 = D3DX_DEFAULT_NONPOW2;

			/// <summary>
			/// Indicates that the method should load from file.
			/// </summary>
			/// <unmanaged>D3DX_FROM_FILE</unmanaged>
			literal int FromFile = D3DX_FROM_FILE;

			/// <summary>
			/// Indicates that the method should format from file.
			/// </summary>
			/// <unmanaged>D3DFMT_FROM_FILE</unmanaged>
			literal int FormatFromFile = D3DFMT_FROM_FILE;

			/// <summary>
			/// 
			/// </summary>
			/// <unmanaged>D3DXGetDeclVertexSize</unmanaged>
			static int GetDeclarationVertexSize( array<VertexElement>^ elements, int stream );

			/// <unmanaged>D3DXGetFVFVertexSize</unmanaged>
			static int GetFVFVertexSize( VertexFormat fvf );

			/// <summary>
			/// Retrieves a declarator from a Flexible Vertex Format (FVF) code.
			/// </summary>
			/// <unmanaged>D3DXDeclaratorFromFVF</unmanaged>
			static array<VertexElement>^ DeclaratorFromFVF( VertexFormat fvf );

			/// <summary>
			/// Retrieves a Flexible Vertex Format (FVF) code from a declarator.
			/// </summary>
			/// <unmanaged>D3DXFVFFromDeclarator</unmanaged>
			static VertexFormat FVFFromDeclarator( array<VertexElement>^ declarator );

			/// <summary>
			/// Generates an output vertex declaration from the input declaration.
			/// The output declaration is intended for use by the mesh tessellation functions.
			/// </summary>
			/// <unmanaged>D3DXGenerateOutputDecl</unmanaged>
			static array<VertexElement>^ GenerateOutputDeclaration( array<VertexElement>^ declaration );

			/// <summary>Returns the number of elements in the vertex declaration.</summary>
			/// <unmanaged>D3DXGetDeclLength</unmanaged>
			static int GetDeclarationLength( array<VertexElement>^ declaration );

			/// <summary>Gets the size of the rectangle patch.</summary>
			/// <unmanaged>D3DXRectPatchSize</unmanaged>
			static Result GetRectanglePatchSize( float segmentCount, [Out] int% triangleCount, [Out] int% vertexCount );

			/// <summary>Gets the size of the triangle patch.</summary>
			/// <unmanaged>D3DXTriPatchSize</unmanaged>
			static Result GetTrianglePatchSize( float segmentCount, [Out] int% triangleCount, [Out] int% vertexCount );

			/// <summary>Generates a FOURCC Format code from the provided bytes.</summary>
			/// <unmanaged>MAKEFOURCC</unmanaged>
			static Format MakeFourCC( System::Byte c1, System::Byte c2, System::Byte c3, System::Byte c4 );

			/// <summary>Turns on or off all D3DX debug output.</summary>
			/// <unmanaged>D3DXDebugMute</unmanaged>
			static bool DebugMute( bool mute );

			/// <summary>Generates an optimized face remapping for a triangle list.</summary>
			/// <unmanaged>D3DXOptimizeFaces</unmanaged>
			static array<int>^ OptimizeFaces( array<int>^ indices, int faceCount, int vertexCount );

			/// <summary>Generates an optimized face remapping for a triangle list.</summary>
			/// <unmanaged>D3DXOptimizeFaces</unmanaged>
			static array<int>^ OptimizeFaces( array<System::Int16>^ indices, int faceCount, int vertexCount );

			/// <summary>Generates an optimized vertex remapping for a triangle list. </summary>
			/// <unmanaged>D3DXOptimizeFaces</unmanaged>
			static array<int>^ OptimizeVertices( array<int>^ indices, int faceCount, int vertexCount );

			/// <summary>Generates an optimized vertex remapping for a triangle list. </summary>
			/// <unmanaged>D3DXOptimizeFaces</unmanaged>
			static array<int>^ OptimizeVertices( array<System::Int16>^ indices, int faceCount, int vertexCount );

			/// <summary>
			/// Verifies that the compiled version of D3DX matches the runtime version of D3DX.
			/// </summary>
			/// <returns><c>true</c> if the versions match; otherwise, <c>false</c>.</returns>
			/// <unmanaged>D3DXCheckVersion</unmanaged>
			static bool CheckVersion();

			/// <unmanaged>D3DXFresnelTerm</unmanaged>
			static float FresnelTerm( float cosTheta, float refractionIndex );

			static array<Vector3>^ GetVectors( DataStream^ stream, int vertexCount, VertexFormat format );

			static array<Vector3>^ GetVectors( DataStream^ stream, int vertexCount, int stride );
		};
	}
}
