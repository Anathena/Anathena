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
#include <vcclr.h>

#include "../ComObject.h"
#include "../DataStream.h"
#include "../Utilities.h"
#include "../SlimDXException.h"

#include "Device.h"
#include "Mesh.h"
#include "MeshData.h"
#include "ProgressiveMesh.h"
#include "PatchMesh.h"
#include "MeshContainer.h"
#include "SkinInfo.h"
#include "IAllocateHierarchy.h"
#include "AnimationFrame.h"
#include "DefaultAllocateHierarchy.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	DefaultAllocateHierarchy::DefaultAllocateHierarchy()
	{
	}

	DefaultAllocateHierarchy^ DefaultAllocateHierarchy::DefaultAllocator::get()
	{
		if( allocator == nullptr )
			allocator = gcnew DefaultAllocateHierarchy();

		return allocator;
	}

	MeshContainer^ DefaultAllocateHierarchy::CreateMeshContainer( System::String^ name, MeshData^ meshData, array<ExtendedMaterial>^ materials, array<EffectInstance>^ effectInstances, 
		array<int>^ adjacency, SkinInfo^ skinInfo )
	{
		MeshContainer^ container = gcnew MeshContainer();
		container->Name = name;
        container->MeshData = meshData;
        container->SetMaterials( materials );
        container->SetEffects( effectInstances );
        container->SetAdjacency( adjacency );
        container->SkinInfo = skinInfo;

		return container;
	}

	Frame^ DefaultAllocateHierarchy::CreateFrame( System::String^ name )
	{
		Frame^ frame = gcnew Frame();
		frame->Name = name;

		return frame;
	}

	void DefaultAllocateHierarchy::DestroyFrame( Frame^ frame )
	{
		// managed delete: Dispose()
		delete frame;
	}

	void DefaultAllocateHierarchy::DestroyMeshContainer( MeshContainer^ container )
	{
		// managed delete: Dispose()
		delete container;
	}
}
}