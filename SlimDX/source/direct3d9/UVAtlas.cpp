#include "stdafx.h"
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
#include <d3d9.h>
#include <d3dx9.h>

#include "../ComObject.h"
#include "../DataStream.h"

#include "Direct3D9Exception.h"

#include "Device.h"
#include "Mesh.h"
#include "UVAtlas.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	HRESULT WINAPI NativeAtlasCallback( FLOAT fPercentDone, LPVOID lpUserContext )
	{
		GCHandle handle = GCHandle::FromIntPtr( IntPtr( lpUserContext ) );
		Predicate<float>^ callback = safe_cast<Predicate<float>^>( handle.Target );
		HRESULT result;

		try
		{
			result = callback( fPercentDone ) ? S_OK : E_FAIL;
		}
		catch( Exception^ )
		{
			result = E_FAIL;
		}

		return result;
	}

	UVAtlasOutput^ UVAtlas::Create( Mesh^ mesh, int maxChartCount, float maxStretch, int width, int height, float gutter, int textureIndex,
				array<int>^ falseEdges, array<float>^ integratedMetricTensors, UVAtlasQuality quality )
	{
		return Create( mesh, maxChartCount, maxStretch, width, height, gutter, textureIndex, falseEdges, integratedMetricTensors, quality, nullptr, 0.0001f );
	}

	UVAtlasOutput^ UVAtlas::Create( Mesh^ mesh, int maxChartCount, float maxStretch, int width, int height, float gutter, int textureIndex,
				array<int>^ falseEdges, array<float>^ integratedMetricTensors, UVAtlasQuality quality, System::Predicate<float>^ callback, float callbackFrequency )
	{
		ID3DXMesh *meshOut;
		ID3DXBuffer *facePartitioning;
		ID3DXBuffer *vertexRemap;
		FLOAT maxStretchOut;
		UINT numChartsOut;
		HRESULT hr;

		pin_ptr<int> pinnedAdj;
		pin_ptr<int> pinnedFalseEdges;
		pin_ptr<float> pinnedIMTs;

		DWORD *inputAdjacencyPtr = NULL;
		DWORD *falseEdgesPtr = NULL;
		FLOAT *imtPtr = NULL;

		array<int>^ adj = mesh->GetAdjacency();

		if( adj != nullptr )
		{
			pinnedAdj = &adj[0];
			inputAdjacencyPtr = reinterpret_cast<DWORD*>( pinnedAdj );
		}

		if( falseEdges != nullptr )
		{
			pinnedFalseEdges = &falseEdges[0];
			falseEdgesPtr = reinterpret_cast<DWORD*>( pinnedFalseEdges );
		}

		if( integratedMetricTensors != nullptr )
		{
			pinnedIMTs = &integratedMetricTensors[0];
			imtPtr = pinnedIMTs;
		}

		if( callback != nullptr )
		{
			GCHandle handle = GCHandle::Alloc(callback);

			hr = D3DXUVAtlasCreate( mesh->InternalPointer, maxChartCount, maxStretch, width, height, gutter, textureIndex,
				inputAdjacencyPtr, falseEdgesPtr, imtPtr, NativeAtlasCallback, callbackFrequency, GCHandle::ToIntPtr(handle).ToPointer(), 
				static_cast<DWORD>( quality ), &meshOut, &facePartitioning, &vertexRemap, &maxStretchOut, &numChartsOut );

			handle.Free();
		}
		else
		{
			hr = D3DXUVAtlasCreate( mesh->InternalPointer, maxChartCount, maxStretch, width, height, gutter, textureIndex,
				inputAdjacencyPtr, falseEdgesPtr, imtPtr, NULL, callbackFrequency, NULL, static_cast<DWORD>( quality ), &meshOut, 
				&facePartitioning, &vertexRemap, &maxStretchOut, &numChartsOut );
		}

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew UVAtlasOutput( Mesh::FromPointer( meshOut ), gcnew DataStream( facePartitioning ), gcnew DataStream( vertexRemap ), 
			maxStretchOut, numChartsOut );
	}

	UVAtlasOutput^ UVAtlas::Partition( Mesh^ mesh, int maxChartCount, float maxStretch, int textureIndex,
				array<int>^ falseEdges, array<float>^ integratedMetricTensors, UVAtlasQuality quality, [Out] DataStream^% partitionAdjacency )
	{
		return Partition( mesh, maxChartCount, maxStretch, textureIndex, falseEdges, integratedMetricTensors, quality, 
			partitionAdjacency, nullptr, 0.0001f );
	}

	UVAtlasOutput^ UVAtlas::Partition( Mesh^ mesh, int maxChartCount, float maxStretch, int textureIndex,
				array<int>^ falseEdges, array<float>^ integratedMetricTensors, UVAtlasQuality quality, [Out] DataStream^% partitionAdjacency, 
				System::Predicate<float>^ callback, float callbackFrequency )
	{
		ID3DXMesh *meshOut;
		ID3DXBuffer *facePartitioning;
		ID3DXBuffer *vertexRemap;
		ID3DXBuffer *partitionResult;
		FLOAT maxStretchOut;
		UINT numChartsOut;
		HRESULT hr;

		pin_ptr<int> pinnedAdj;
		pin_ptr<int> pinnedFalseEdges;
		pin_ptr<float> pinnedIMTs;

		DWORD *inputAdjacencyPtr = NULL;
		DWORD *falseEdgesPtr = NULL;
		FLOAT *imtPtr = NULL;

		array<int>^ adj = mesh->GetAdjacency();

		if( adj != nullptr )
		{
			pinnedAdj = &adj[0];
			inputAdjacencyPtr = reinterpret_cast<DWORD*>( pinnedAdj );
		}

		if( falseEdges != nullptr )
		{
			pinnedFalseEdges = &falseEdges[0];
			falseEdgesPtr = reinterpret_cast<DWORD*>( pinnedFalseEdges );
		}

		if( integratedMetricTensors != nullptr )
		{
			pinnedIMTs = &integratedMetricTensors[0];
			imtPtr = pinnedIMTs;
		}

		if( callback != nullptr )
		{
			GCHandle handle = GCHandle::Alloc(callback);

			hr = D3DXUVAtlasPartition( mesh->InternalPointer, maxChartCount, maxStretch, textureIndex,
				inputAdjacencyPtr, falseEdgesPtr, imtPtr, NativeAtlasCallback, callbackFrequency, GCHandle::ToIntPtr(handle).ToPointer(), 
				static_cast<DWORD>( quality ), &meshOut, &facePartitioning, &vertexRemap, &partitionResult, &maxStretchOut, &numChartsOut );

			handle.Free();
		}
		else
		{
			hr = D3DXUVAtlasPartition( mesh->InternalPointer, maxChartCount, maxStretch, textureIndex,
				inputAdjacencyPtr, falseEdgesPtr, imtPtr, NULL, callbackFrequency, NULL, static_cast<DWORD>( quality ), &meshOut, 
				&facePartitioning, &vertexRemap, &partitionResult, &maxStretchOut, &numChartsOut );
		}

		if( RECORD_D3D9( hr ).IsFailure )
		{
			partitionAdjacency = nullptr;
			return nullptr;
		}

		partitionAdjacency = gcnew DataStream( partitionResult );

		return gcnew UVAtlasOutput( Mesh::FromPointer( meshOut ), gcnew DataStream( facePartitioning ), gcnew DataStream( vertexRemap ), 
			maxStretchOut, numChartsOut );
	}

	Result UVAtlas::Pack( Mesh^ mesh, int width, int height, float gutter, int textureIndex, array<int>^ partitionAdjacency, UVAtlasQuality quality, DataStream^ facePartitioning )
	{
		return Pack( mesh, width, height, gutter, textureIndex, partitionAdjacency, quality, facePartitioning, nullptr, 0.0001f );
	}

	Result UVAtlas::Pack( Mesh^ mesh, int width, int height, float gutter, int textureIndex, array<int>^ partitionAdjacency, 
		UVAtlasQuality quality, DataStream^ facePartitioning, System::Predicate<float>^ callback, float callbackFrequency )
	{
		HRESULT hr;
		pin_ptr<int> pinnedAdj = &partitionAdjacency[0];

		if( callback != nullptr )
		{
			GCHandle handle = GCHandle::Alloc(callback);

			hr = D3DXUVAtlasPack( mesh->InternalPointer, width, height, gutter, textureIndex,
				reinterpret_cast<DWORD*>( pinnedAdj ), NativeAtlasCallback, callbackFrequency, GCHandle::ToIntPtr(handle).ToPointer(), 
				static_cast<DWORD>( quality ), facePartitioning->GetD3DBuffer() );

			handle.Free();
		}
		else
		{
			hr = D3DXUVAtlasPack( mesh->InternalPointer, width, height, gutter, textureIndex,
				reinterpret_cast<DWORD*>( pinnedAdj ), NULL, callbackFrequency, NULL, 
				static_cast<DWORD>( quality ), facePartitioning->GetD3DBuffer() );
		}

		return RECORD_D3D9( hr );
	}
}
}