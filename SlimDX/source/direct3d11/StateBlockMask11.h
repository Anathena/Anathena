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

#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Indicates settings for various bits of device state.
		/// </summary>
		/// <unmanaged>D3DX11_STATE_BLOCK_MASK</unmanaged>
		public ref class StateBlockMask
		{			
		internal:
			StateBlockMask( const D3DX11_STATE_BLOCK_MASK& description );
			
		public:			
			/// <summary>
			/// Indicates whether vertex shader state is enabled by the mask.
			/// </summary>
			property bool VertexShader;
			
			/// <summary>
			/// A set of values indicating which vertex shader sampler states are enabled by the mask.
			/// </summary>
			property array<bool>^ VertexShaderSamplers;
			
			/// <summary>
			/// A set of values indicating which vertex shader resources are enabled by the mask.
			/// </summary>
			property array<bool>^ VertexShaderResources;
			
			/// <summary>
			/// A set of values indicating which vertex shader constant buffers are enabled by the mask.
			/// </summary>
			property array<bool>^ VertexShaderConstantBuffers;

			/// <summary>
			/// A set of values indicating which vertex shader interfaces are enabled by the mask.
			/// </summary>
			property array<bool>^ VertexShaderInterfaces;

			/// <summary>
			/// Indicates whether hull shader state is enabled by the mask.
			/// </summary>
			property bool HullShader;
			
			/// <summary>
			/// A set of values indicating which hull shader sampler states are enabled by the mask.
			/// </summary>
			property array<bool>^ HullShaderSamplers;
			
			/// <summary>
			/// A set of values indicating which hull shader resources are enabled by the mask.
			/// </summary>
			property array<bool>^ HullShaderResources;
			
			/// <summary>
			/// A set of values indicating which hull shader constant buffers are enabled by the mask.
			/// </summary>
			property array<bool>^ HullShaderConstantBuffers;

			/// <summary>
			/// A set of values indicating which hull shader interfaces are enabled by the mask.
			/// </summary>
			property array<bool>^ HullShaderInterfaces;

			/// <summary>
			/// Indicates whether domain shader state is enabled by the mask.
			/// </summary>
			property bool DomainShader;
			
			/// <summary>
			/// A set of values indicating which domain shader sampler states are enabled by the mask.
			/// </summary>
			property array<bool>^ DomainShaderSamplers;
			
			/// <summary>
			/// A set of values indicating which domain shader resources are enabled by the mask.
			/// </summary>
			property array<bool>^ DomainShaderResources;
			
			/// <summary>
			/// A set of values indicating which domain shader constant buffers are enabled by the mask.
			/// </summary>
			property array<bool>^ DomainShaderConstantBuffers;

			/// <summary>
			/// A set of values indicating which domain shader interfaces are enabled by the mask.
			/// </summary>
			property array<bool>^ DomainShaderInterfaces;

			/// <summary>
			/// Indicates whether geometry shader state is enabled by the mask.
			/// </summary>
			property bool GeometryShader;
			
			/// <summary>
			/// A set of values indicating which geometry shader sampler states are enabled by the mask.
			/// </summary>
			property array<bool>^ GeometryShaderSamplers;
			
			/// <summary>
			/// A set of values indicating which geometry shader resources are enabled by the mask.
			/// </summary>
			property array<bool>^ GeometryShaderResources;
			
			/// <summary>
			/// A set of values indicating which geometry shader constant buffers are enabled by the mask.
			/// </summary>
			property array<bool>^ GeometryShaderConstantBuffers;

			/// <summary>
			/// A set of values indicating which geometry shader interfaces are enabled by the mask.
			/// </summary>
			property array<bool>^ GeometryShaderInterfaces;

			/// <summary>
			/// Indicates whether pixel shader state is enabled by the mask.
			/// </summary>
			property bool PixelShader;
			
			/// <summary>
			/// A set of values indicating which pixel shader sampler states are enabled by the mask.
			/// </summary>
			property array<bool>^ PixelShaderSamplers;
			
			/// <summary>
			/// A set of values indicating which pixel shader resources are enabled by the mask.
			/// </summary>
			property array<bool>^ PixelShaderResources;
			
			/// <summary>
			/// A set of values indicating which pixel shader constant buffers are enabled by the mask.
			/// </summary>
			property array<bool>^ PixelShaderConstantBuffers;

			/// <summary>
			/// A set of values indicating which pixel shader interfaces are enabled by the mask.
			/// </summary>
			property array<bool>^ PixelShaderInterfaces;

			/// <summary>
			/// Indicates whether pixel shader unordered access views are enabled by the mask.
			/// </summary>
			property bool PixelShaderUnorderedAccessViews;

			/// <summary>
			/// Indicates whether compute shader state is enabled by the mask.
			/// </summary>
			property bool ComputeShader;
			
			/// <summary>
			/// A set of values indicating which compute shader sampler states are enabled by the mask.
			/// </summary>
			property array<bool>^ ComputeShaderSamplers;
			
			/// <summary>
			/// A set of values indicating which compute shader resources are enabled by the mask.
			/// </summary>
			property array<bool>^ ComputeShaderResources;
			
			/// <summary>
			/// A set of values indicating which compute shader constant buffers are enabled by the mask.
			/// </summary>
			property array<bool>^ ComputeShaderConstantBuffers;

			/// <summary>
			/// A set of values indicating which compute shader interfaces are enabled by the mask.
			/// </summary>
			property array<bool>^ ComputeShaderInterfaces;

			/// <summary>
			/// Indicates whether compute shader unordered access views are enabled by the mask.
			/// </summary>
			property bool ComputeShaderUnorderedAccessViews;

			/// <summary>
			/// A set of values indicating which vertex buffers are enabled by the mask.
			/// </summary>
			property array<bool>^ VertexBuffers;
			
			/// <summary>
			/// Indicates whether index bufer state is enabled by the mask.
			/// </summary>
			property bool IndexBuffer;
			
			/// <summary>
			/// Indicates whether input layout state is enabled by the mask.
			/// </summary>
			property bool InputLayout;
			
			/// <summary>
			/// Indicates whether primitive topology state is enabled by the mask.
			/// </summary>
			property bool PrimitiveTopology;
			
			/// <summary>
			/// Indicates whether render target state is enabled by the mask.
			/// </summary>
			property bool RenderTargets;
			
			/// <summary>
			/// Indicates whether depth-stencil state is enabled by the mask.
			/// </summary>
			property bool DepthStencilState;
			
			/// <summary>
			/// Indicates whether blend state is enabled by the mask.
			/// </summary>
			property bool BlendState;
			
			/// <summary>
			/// Indicates whether viewport state is enabled by the mask.
			/// </summary>
			property bool Viewports;
			
			/// <summary>
			/// Indicates whether scissor rectangle state is enabled by the mask.
			/// </summary>
			property bool ScissorRectangles;
			
			/// <summary>
			/// Indicates whether rasterizer state is enabled by the mask.
			/// </summary>
			property bool RasterizerState;
			
			/// <summary>
			/// Indicates whether stream output buffer state is enabled by the mask.
			/// </summary>
			property bool StreamOutputBuffers;
			
			/// <summary>
			/// Indicates whether predication state is enabled by the mask.
			/// </summary>
			property bool Predication;
		};
	}
};