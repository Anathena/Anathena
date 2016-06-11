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
#include <d3d10.h>
#include "Enums.h"
#include "BlendStateDescription1.h"

namespace SlimDX
{
namespace Direct3D10_1
{
	BlendStateDescription1::BlendStateDescription1( const D3D10_BLEND_DESC1& native )
	{
		AlphaToCoverageEnable = native.AlphaToCoverageEnable ? true : false;
		IndependentBlendEnable = native.IndependentBlendEnable ? true : false;
		
		ConstructLazyProperties();
		for(int index = 0; index < 8; ++index)
		{
			m_RenderTargets[index] = RenderTargetBlendDescription1(native.RenderTarget[index]);
		}
	}
	
	D3D10_BLEND_DESC1 BlendStateDescription1::CreateNativeVersion()
	{
		D3D10_BLEND_DESC1 native;
		native.AlphaToCoverageEnable = AlphaToCoverageEnable;
		native.IndependentBlendEnable = IndependentBlendEnable;
		
		ConstructLazyProperties();
		for(int index = 0; index < 8; ++index)
		{
			native.RenderTarget[index] = m_RenderTargets[index].CreateNativeVersion();
		}
		
		return native;
	}
	
	void BlendStateDescription1::ConstructLazyProperties()
	{
		if(m_RenderTargets == nullptr)
		{
			m_RenderTargets = gcnew array<RenderTargetBlendDescription1>(8);
		}
	}

	array<RenderTargetBlendDescription1>^ BlendStateDescription1::RenderTargets::get()
	{
		ConstructLazyProperties();
		return m_RenderTargets;
	}
}
}
