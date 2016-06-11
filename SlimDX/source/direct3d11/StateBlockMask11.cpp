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

#include "../Utilities.h"

#include "StateBlockMask11.h"

using namespace System;

#define MASK(bytes, index) ((bytes[index / 8] & (1 << index)) > 0)

namespace SlimDX
{
namespace Direct3D11
{
	StateBlockMask::StateBlockMask( const D3DX11_STATE_BLOCK_MASK& native )
	{
		VertexShader = native.VS > 0;
		HullShader = native.HS > 0;
		DomainShader = native.DS > 0;
		GeometryShader = native.GS > 0;
		PixelShader = native.PS > 0;
		ComputeShader = native.CS > 0;
		PixelShaderUnorderedAccessViews = native.PSUnorderedAccessViews > 0;
		ComputeShaderUnorderedAccessViews = native.CSUnorderedAccessViews > 0;
		IndexBuffer = native.IAIndexBuffer > 0;
		InputLayout = native.IAInputLayout > 0;
		PrimitiveTopology = native.IAPrimitiveTopology > 0;
		RenderTargets = native.OMRenderTargets > 0;
		DepthStencilState = native.OMDepthStencilState > 0;
		BlendState = native.OMBlendState > 0;
		Viewports = native.RSViewports > 0;
		ScissorRectangles = native.RSScissorRects > 0;
		RasterizerState = native.RSRasterizerState > 0;
		StreamOutputBuffers = native.SOBuffers > 0;
		Predication = native.Predication > 0;

		VertexShaderSamplers = gcnew array<bool>(D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT);
		for (int i = 0; i < VertexShaderSamplers->Length; i++)
			VertexShaderSamplers[i] = MASK(native.VSSamplers, i);

		VertexShaderResources = gcnew array<bool>(D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT);
		for (int i = 0; i < VertexShaderResources->Length; i++)
			VertexShaderResources[i] = MASK(native.VSShaderResources, i);

		VertexShaderConstantBuffers = gcnew array<bool>(D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT);
		for (int i = 0; i < VertexShaderConstantBuffers->Length; i++)
			VertexShaderConstantBuffers[i] = MASK(native.VSConstantBuffers, i);

		VertexShaderInterfaces = gcnew array<bool>(D3D11_SHADER_MAX_INTERFACES);
		for (int i = 0; i < VertexShaderInterfaces->Length; i++)
			VertexShaderInterfaces[i] = MASK(native.VSInterfaces, i);

		HullShaderSamplers = gcnew array<bool>(D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT);
		for (int i = 0; i < HullShaderSamplers->Length; i++)
			HullShaderSamplers[i] = MASK(native.HSSamplers, i);

		HullShaderResources = gcnew array<bool>(D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT);
		for (int i = 0; i < HullShaderResources->Length; i++)
			HullShaderResources[i] = MASK(native.HSShaderResources, i);

		HullShaderConstantBuffers = gcnew array<bool>(D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT);
		for (int i = 0; i < HullShaderConstantBuffers->Length; i++)
			HullShaderConstantBuffers[i] = MASK(native.HSConstantBuffers, i);

		HullShaderInterfaces = gcnew array<bool>(D3D11_SHADER_MAX_INTERFACES);
		for (int i = 0; i < HullShaderInterfaces->Length; i++)
			HullShaderInterfaces[i] = MASK(native.HSInterfaces, i);

		DomainShaderSamplers = gcnew array<bool>(D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT);
		for (int i = 0; i < DomainShaderSamplers->Length; i++)
			DomainShaderSamplers[i] = MASK(native.DSSamplers, i);

		DomainShaderResources = gcnew array<bool>(D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT);
		for (int i = 0; i < DomainShaderResources->Length; i++)
			DomainShaderResources[i] = MASK(native.DSShaderResources, i);

		DomainShaderConstantBuffers = gcnew array<bool>(D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT);
		for (int i = 0; i < DomainShaderConstantBuffers->Length; i++)
			DomainShaderConstantBuffers[i] = MASK(native.DSConstantBuffers, i);

		DomainShaderInterfaces = gcnew array<bool>(D3D11_SHADER_MAX_INTERFACES);
		for (int i = 0; i < DomainShaderInterfaces->Length; i++)
			DomainShaderInterfaces[i] = MASK(native.DSInterfaces, i);

		GeometryShaderSamplers = gcnew array<bool>(D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT);
		for (int i = 0; i < GeometryShaderSamplers->Length; i++)
			GeometryShaderSamplers[i] = MASK(native.GSSamplers, i);

		GeometryShaderResources = gcnew array<bool>(D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT);
		for (int i = 0; i < GeometryShaderResources->Length; i++)
			GeometryShaderResources[i] = MASK(native.GSShaderResources, i);

		GeometryShaderConstantBuffers = gcnew array<bool>(D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT);
		for (int i = 0; i < GeometryShaderConstantBuffers->Length; i++)
			GeometryShaderConstantBuffers[i] = MASK(native.GSConstantBuffers, i);

		GeometryShaderInterfaces = gcnew array<bool>(D3D11_SHADER_MAX_INTERFACES);
		for (int i = 0; i < GeometryShaderInterfaces->Length; i++)
			GeometryShaderInterfaces[i] = MASK(native.GSInterfaces, i);

		PixelShaderSamplers = gcnew array<bool>(D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT);
		for (int i = 0; i < PixelShaderSamplers->Length; i++)
			PixelShaderSamplers[i] = MASK(native.PSSamplers, i);

		PixelShaderResources = gcnew array<bool>(D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT);
		for (int i = 0; i < PixelShaderResources->Length; i++)
			PixelShaderResources[i] = MASK(native.PSShaderResources, i);

		PixelShaderConstantBuffers = gcnew array<bool>(D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT);
		for (int i = 0; i < PixelShaderConstantBuffers->Length; i++)
			PixelShaderConstantBuffers[i] = MASK(native.PSConstantBuffers, i);

		PixelShaderInterfaces = gcnew array<bool>(D3D11_SHADER_MAX_INTERFACES);
		for (int i = 0; i < PixelShaderInterfaces->Length; i++)
			PixelShaderInterfaces[i] = MASK(native.PSInterfaces, i);

		ComputeShaderSamplers = gcnew array<bool>(D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT);
		for (int i = 0; i < ComputeShaderSamplers->Length; i++)
			ComputeShaderSamplers[i] = MASK(native.CSSamplers, i);

		ComputeShaderResources = gcnew array<bool>(D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT);
		for (int i = 0; i < ComputeShaderResources->Length; i++)
			ComputeShaderResources[i] = MASK(native.CSShaderResources, i);

		ComputeShaderConstantBuffers = gcnew array<bool>(D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT);
		for (int i = 0; i < ComputeShaderConstantBuffers->Length; i++)
			ComputeShaderConstantBuffers[i] = MASK(native.CSConstantBuffers, i);

		ComputeShaderInterfaces = gcnew array<bool>(D3D11_SHADER_MAX_INTERFACES);
		for (int i = 0; i < ComputeShaderInterfaces->Length; i++)
			ComputeShaderInterfaces[i] = MASK(native.CSInterfaces, i);

		VertexBuffers = gcnew array<bool>(D3D11_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT);
		for (int i = 0; i < VertexBuffers->Length; i++)
			VertexBuffers[i] = MASK(native.IAVertexBuffers, i);
	}
}
}
