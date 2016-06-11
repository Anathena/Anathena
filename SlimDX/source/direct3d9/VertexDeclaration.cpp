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
#include <dxerr.h>

#include "../stack_array.h"

#include "Device.h"
#include "VertexDeclaration.h"

#include "Direct3D9Exception.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	VertexDeclaration::VertexDeclaration( Device^ device, array<VertexElement>^ elements )
	{
		if( elements == nullptr )
			throw gcnew ArgumentNullException( "elements" );

		pin_ptr<VertexElement> pinnedElements = &elements[0];
		D3DVERTEXELEMENT9 *elementPtr = reinterpret_cast<D3DVERTEXELEMENT9*>( pinnedElements );

		stack_array<D3DVERTEXELEMENT9> nativeElements;
		if (elements[elements->Length - 1] != VertexElement::VertexDeclarationEnd)
		{
			D3DVERTEXELEMENT9 end = D3DDECL_END();

			nativeElements = stack_array<D3DVERTEXELEMENT9>(elements->Length + 1);
			memcpy(&nativeElements[0], elementPtr, sizeof(D3DVERTEXELEMENT9) * elements->Length);
			nativeElements[elements->Length] = end;

			elementPtr = &nativeElements[0];
		}
		
		IDirect3DVertexDeclaration9* decl;

		HRESULT hr = device->InternalPointer->CreateVertexDeclaration( elementPtr, &decl );
		if( RECORD_D3D9(hr).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(decl);
	}

	array<VertexElement>^ VertexDeclaration::Elements::get()
	{
		unsigned int count = 0;

		HRESULT hr = InternalPointer->GetDeclaration( 0, &count );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		array<VertexElement>^ decl = gcnew array<VertexElement>( count );
		pin_ptr<VertexElement> pinnedDecl = &decl[0];

		hr = InternalPointer->GetDeclaration( reinterpret_cast<D3DVERTEXELEMENT9*>( pinnedDecl ), &count );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return decl;
	}
}
}
