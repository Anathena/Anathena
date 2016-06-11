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

#include "D3DXEnums.h"
#include "UVAtlasOutput.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D9
	{
		public ref class UVAtlas sealed
		{
		private:
			UVAtlas() { }

		public:
			static UVAtlasOutput^ Create( Mesh^ mesh, int maxChartCount, float maxStretch, int width, int height, float gutter, int textureIndex,
				array<int>^ falseEdges, array<float>^ integratedMetricTensors, UVAtlasQuality quality );

			static UVAtlasOutput^ Create( Mesh^ mesh, int maxChartCount, float maxStretch, int width, int height, float gutter, int textureIndex,
				array<int>^ falseEdges, array<float>^ integratedMetricTensors, UVAtlasQuality quality, System::Predicate<float>^ callback, float callbackFrequency );

			static UVAtlasOutput^ Partition( Mesh^ mesh, int maxChartCount, float maxStretch, int textureIndex,
				array<int>^ falseEdges, array<float>^ integratedMetricTensors, UVAtlasQuality quality, [Out] DataStream^% partitionAdjacency );

			static UVAtlasOutput^ Partition( Mesh^ mesh, int maxChartCount, float maxStretch, int textureIndex,
				array<int>^ falseEdges, array<float>^ integratedMetricTensors, UVAtlasQuality quality, [Out] DataStream^% partitionAdjacency, 
				System::Predicate<float>^ callback, float callbackFrequency );

			static Result Pack( Mesh^ mesh, int width, int height, float gutter, int textureIndex, array<int>^ partitionAdjacency, 
				UVAtlasQuality quality, DataStream^ facePartitioning );

			static Result Pack( Mesh^ mesh, int width, int height, float gutter, int textureIndex, array<int>^ partitionAdjacency, 
				UVAtlasQuality quality, DataStream^ facePartitioning, System::Predicate<float>^ callback, float callbackFrequency );
		};
	}
}