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

#include "InputBindingDescriptionDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{
	InputBindingDescription::InputBindingDescription( const D3D11_SHADER_INPUT_BIND_DESC &desc )
	{
		Name = gcnew String(desc.Name);
		Type = static_cast<ShaderInputType>(desc.Type);
		BindPoint = desc.BindPoint;
		BindCount = desc.BindCount;
		Flags = static_cast<ShaderInputFlags>(desc.uFlags);
		ReturnType = static_cast<ResourceReturnType>(desc.ReturnType);
		Dimension = static_cast<Direct3D11::ShaderResourceViewDimension>(desc.Dimension);
		SampleCount = desc.NumSamples;
	}
}
}