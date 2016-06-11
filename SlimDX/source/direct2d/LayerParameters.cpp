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
#include "stdafx.h"

#include <d2d1.h>
#include <d2d1helper.h>

#include "Direct2DException.h"

#include "LayerParameters.h"

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	LayerParameters::LayerParameters()
	{
	}

	D2D1_LAYER_PARAMETERS LayerParameters::ToUnmanaged()
	{
		D2D1_LAYER_PARAMETERS params;
		params.contentBounds = D2D1::RectF( ContentBounds.Left, ContentBounds.Top, ContentBounds.Right, ContentBounds.Bottom );
		params.geometricMask = GeometricMask == nullptr ? NULL : GeometricMask->InternalPointer;
		params.maskAntialiasMode = static_cast<D2D1_ANTIALIAS_MODE>( AntialiasMode );
		params.opacity = Opacity;
		params.opacityBrush = OpacityBrush == nullptr ? NULL : OpacityBrush->InternalPointer;
		params.layerOptions = static_cast<D2D1_LAYER_OPTIONS>( LayerOptions );

		Matrix3x2 v = MaskTransform;
		memcpy( &params.maskTransform, &v, sizeof( D2D1_MATRIX_3X2_F ) );
		return params;
	}
}
}