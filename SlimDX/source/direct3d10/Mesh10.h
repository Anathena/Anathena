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

#include "InputElement.h"
#include "MeshAttributeRange.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class Buffer;
		ref class Device;
		ref class MeshBuffer;

		public ref class Mesh : public ComObject
		{
			COMOBJECT(ID3DX10Mesh, Mesh);
	
		public:
			property MeshFlags Flags 
			{
				MeshFlags get();
			}

			property int FaceCount 
			{
				int get();
			}

			property int VertexCount 
			{
				int get();
			}

			property int VertexBufferCount 
			{
				int get();
			}

			Mesh( Device^ device, array<InputElement>^ elements, System::String^ positionElement, int vertexCount, int faceCount, MeshFlags flags );

			Mesh^ Clone( array<InputElement>^ elements, System::String^ positionElement, MeshFlags flags );

			Result GenerateAdjacencyAndPointRepresentation( float epsilon );
			Result GenerateAttributeBufferFromTable();
			Result GenerateGeometryShaderAdjacency();

			/// <summary>
			/// Commits changes to the mesh back to the device.
			/// </summary>
			/// <returns>A result code.</returns>
			Result Commit();

			/// <summary>
			/// Discards data that has been committed to the device.
			/// </summary>
			/// <param name="flags">Flags indicating which pieces of data to discard.</param>
			/// <returns>A result code.</returns>
			Result Discard( MeshDiscardFlags flags );
			
			/// <summary>
			/// Optimizes the mesh data.
			/// </summary>
			/// <param name="flags">Flags indicating which optimizations to perform.</param>
			/// <returns>A result code.</returns>
			Result Optimize( MeshOptimizeFlags flags );
			
			/// <summary>
			/// Optimizes the mesh data.
			/// </summary>
			/// <param name="flags">Flags indicating which optimizations to perform.</param>
			/// <param name="faceRemap">An array of integers, one per face, that map the original mesh faces to the optimized mesh faces.</param>
			/// <param name="vertexRemap">An array of integers, one per vertex, that map the original mesh vertices to the optimized mesh vertices.</param>
			/// <returns>A result code.</returns>
			Result Optimize( MeshOptimizeFlags flags, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap );
			
			Result DrawSubset( int id );
			Result DrawSubsetInstanced( int id, int count, int startLocation );
			
			MeshBuffer^ GetIndexBuffer();
			MeshBuffer^ GetVertexBuffer( int index );
			MeshBuffer^ GetPointRepresentationBuffer();
			MeshBuffer^ GetAdjacencyBuffer();
			MeshBuffer^ GetAttributeBuffer();
			
			Result SetIndexData( DataStream^ data, int count );
			Result SetVertexData( int index, DataStream^ data );
			Result SetPointRepresentationData( DataStream^ data );
			Result SetAdjacencyData( DataStream^ data );
			Result SetAttributeData( DataStream^ data );

			Result SetAttributeTable( array<MeshAttributeRange>^ data );
			System::Collections::ObjectModel::ReadOnlyCollection<MeshAttributeRange>^ GetAttributeTable();
			
			Buffer^ GetDeviceIndexBuffer();
			Buffer^ GetDeviceVertexBuffer( int index );
		};
	}
}
