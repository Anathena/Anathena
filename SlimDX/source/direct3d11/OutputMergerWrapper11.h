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

#include "../math/Color4.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class BlendState;
		ref class DepthStencilState;
		ref class DepthStencilView;
		ref class RenderTargetView;
		ref class UnorderedAccessView;
		
		/// <summary>
		/// Defines a wrapper for output-merger related commands on the device.
		/// </summary>
		public ref class OutputMergerWrapper
		{
		private:
			ID3D11DeviceContext* deviceContext;
			
		internal:
			OutputMergerWrapper( ID3D11DeviceContext* device );
			
		public:
			/// <summary>
			/// Gets the depth-stencil state of the output-merger stage.
			/// </summary>
			property SlimDX::Direct3D11::DepthStencilState^ DepthStencilState
			{
				void set( SlimDX::Direct3D11::DepthStencilState^ value );
				SlimDX::Direct3D11::DepthStencilState^ get();
			}
			
			/// <summary>
			/// Gets or sets the depth-stencil reference value.
			/// </summary>
			property int DepthStencilReference
			{
				void set( int value );
				int get();
			}
			
			/// <summary>
			/// Gets or sets the blend state of the output-merger stage.
			/// </summary>
			property SlimDX::Direct3D11::BlendState^ BlendState
			{
				void set( SlimDX::Direct3D11::BlendState^ value );
				SlimDX::Direct3D11::BlendState^ get();
			}
			
			/// <summary>
			/// Gets or sets the blend factor color.
			/// </summary>
			property Color4 BlendFactor
			{
				void set( Color4 value );
				Color4 get();
			}
			
			/// <summary>
			/// Gets or sets the blending mask.
			/// </summary>
			property int BlendSampleMask
			{
				void set( int value );
				int get();
			}

			/// <summary>
			/// Gets the depth-stencil state of the output-merger stage.
			/// </summary>
			/// <param name="state">Returns the currently set depth-stencil state.</param>
			/// <param name="reference">Returns the current depth-stencil reference value.</param>
			void GetDepthStencilState(SlimDX::Direct3D11::DepthStencilState^ %state, int %reference);

			/// <summary>
			/// Sets the depth-stencil state of the output-merger stage.
			/// </summary>
			/// <param name="state">The depth-stencil state to set.</param>
			/// <param name="reference">The depth-stencil reference value to set.</param>
			void SetDepthStencilState(SlimDX::Direct3D11::DepthStencilState^ state, int reference);

			/// <summary>
			/// Gets the blend state of the output-merger stage.
			/// </summary>
			/// <param name="state">Returns the currently set blend state.</param>
			/// <param name="blendFactor">Returns the current blending factor.</param>
			/// <param name="sampleMask">Returns the current sampling mask.</param>
			void GetBlendState(SlimDX::Direct3D11::BlendState^ %state, Color4 %blendFactor, int %sampleMask);

			/// <summary>
			/// Sets the blend state of the output-merger stage.
			/// </summary>
			/// <param name="state">The blend state to set.</param>
			/// <param name="blendFactor">The blending factor to set.</param>
			/// <param name="sampleMask">The sampling mask to set.</param>
			void SetBlendState(SlimDX::Direct3D11::BlendState^ state, Color4 blendFactor, int sampleMask);
			
			/// <summary>
			/// Binds a single render target to the output-merger stage.
			/// </summary>
			/// <param name="renderTargetView">A view of the render target to bind.</param>
			void SetTargets( RenderTargetView^ renderTargetView );

			/// <summary>
			/// Binds a depth-stencil buffer and a single render target to the output-merger stage.
			/// </summary>
			/// <param name="depthStencilView">A view of the depth-stencil buffer to bind.</param>
			/// <param name="renderTargetView">A view of the render target to bind.</param>
			void SetTargets( DepthStencilView^ depthStencilView, RenderTargetView^ renderTargetView );

			/// <summary>
			/// Binds a set of render targets to the output-merger stage.
			/// </summary>
			/// <param name="renderTargetViews">A set of render target views to bind.</param>
			void SetTargets( ... array<RenderTargetView^>^ renderTargetViews );

			/// <summary>
			/// Binds a depth-stencil buffer and a set of render targets to the output-merger stage.
			/// </summary>
			/// <param name="depthStencilView">A view of the depth-stencil buffer to bind.</param>
			/// <param name="renderTargetViews">A set of render target views to bind.</param>
			void SetTargets( DepthStencilView^ depthStencilView, ... array<RenderTargetView^>^ renderTargetViews );

			/// <summary>
			/// Binds a set of unordered access views and a single render target to the output-merger stage.
			/// </summary>
			/// <param name="startSlot">Index into a zero-based array to begin setting unordered access views.</param>
			/// <param name="unorderedAccessViews">A set of unordered access views to bind.</param>
			/// <param name="renderTargetView">A view of the render target to bind.</param>
			void SetTargets( RenderTargetView^ renderTargetView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews );

			/// <summary>
			/// Binds a depth-stencil buffer, a set of unordered access views, and a single render target to the output-merger stage.
			/// </summary>
			/// <param name="depthStencilView">A view of the depth-stencil buffer to bind.</param>
			/// <param name="startSlot">Index into a zero-based array to begin setting unordered access views.</param>
			/// <param name="unorderedAccessViews">A set of unordered access views to bind.</param>
			/// <param name="renderTargetView">A view of the render target to bind.</param>
			void SetTargets( DepthStencilView^ depthStencilView, RenderTargetView^ renderTargetView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews );

			/// <summary>
			/// Binds a set of unordered access views and a set of render targets to the output-merger stage.
			/// </summary>
			/// <param name="startSlot">Index into a zero-based array to begin setting unordered access views.</param>
			/// <param name="unorderedAccessViews">A set of unordered access views to bind.</param>
			/// <param name="renderTargetViews">A set of render target views to bind.</param>
			void SetTargets( int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, ... array<RenderTargetView^>^ renderTargetViews );

			/// <summary>
			/// Binds a depth-stencil buffer, a set of unordered access views, and a set of render targets to the output-merger stage.
			/// </summary>
			/// <param name="depthStencilView">A view of the depth-stencil buffer to bind.</param>
			/// <param name="startSlot">Index into a zero-based array to begin setting unordered access views.</param>
			/// <param name="unorderedAccessViews">A set of unordered access views to bind.</param>
			/// <param name="renderTargetViews">A set of render target views to bind.</param>
			void SetTargets( DepthStencilView^ depthStencilView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, ... array<RenderTargetView^>^ renderTargetViews );

			/// <summary>
			/// Binds a set of unordered access views and a single render target to the output-merger stage.
			/// </summary>
			/// <param name="startSlot">Index into a zero-based array to begin setting unordered access views.</param>
			/// <param name="unorderedAccessViews">A set of unordered access views to bind.</param>
			/// <param name="renderTargetView">A view of the render target to bind.</param>
			/// <param name="initialLengths">An array of Append/Consume buffer offsets. A value of -1 indicates the current offset should be kept. Any other values set the hidden counter for that Appendable/Consumeable UAV.</param>
			void SetTargets( RenderTargetView^ renderTargetView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, array<int>^ initialLengths );
			
			/// <summary>
			/// Binds a depth-stencil buffer, a set of unordered access views, and a single render target to the output-merger stage.
			/// </summary>
			/// <param name="depthStencilView">A view of the depth-stencil buffer to bind.</param>
			/// <param name="startSlot">Index into a zero-based array to begin setting unordered access views.</param>
			/// <param name="unorderedAccessViews">A set of unordered access views to bind.</param>
			/// <param name="renderTargetView">A view of the render target to bind.</param>
			/// <param name="initialLengths">An array of Append/Consume buffer offsets. A value of -1 indicates the current offset should be kept. Any other values set the hidden counter for that Appendable/Consumeable UAV.</param>
			void SetTargets( DepthStencilView^ depthStencilView, RenderTargetView^ renderTargetView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, array<int>^ initialLengths );
			
			/// <summary>
			/// Binds a set of unordered access views and a set of render targets to the output-merger stage.
			/// </summary>
			/// <param name="startSlot">Index into a zero-based array to begin setting unordered access views.</param>
			/// <param name="unorderedAccessViews">A set of unordered access views to bind.</param>
			/// <param name="renderTargetViews">A set of render target views to bind.</param>
			/// <param name="initialLengths">An array of Append/Consume buffer offsets. A value of -1 indicates the current offset should be kept. Any other values set the hidden counter for that Appendable/Consumeable UAV.</param>
			void SetTargets( int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, array<int>^ initialLengths, ... array<RenderTargetView^>^ renderTargetViews );
			
			/// <summary>
			/// Binds a depth-stencil buffer, a set of unordered access views, and a set of render targets to the output-merger stage.
			/// </summary>
			/// <param name="depthStencilView">A view of the depth-stencil buffer to bind.</param>
			/// <param name="startSlot">Index into a zero-based array to begin setting unordered access views.</param>
			/// <param name="unorderedAccessViews">A set of unordered access views to bind.</param>
			/// <param name="renderTargetViews">A set of render target views to bind.</param>
			/// <param name="initialLengths">An array of Append/Consume buffer offsets. A value of -1 indicates the current offset should be kept. Any other values set the hidden counter for that Appendable/Consumeable UAV.</param>
			void SetTargets( DepthStencilView^ depthStencilView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, array<int>^ initialLengths, ... array<RenderTargetView^>^ renderTargetViews );

			/// <summary>
			/// Gets the currently bound depth-stencil view.
			/// </summary>
			/// <returns>The currently bound depth-stencil view.</returns>
			DepthStencilView^ GetDepthStencilView();

			/// <summary>
			/// Gets the set of currently bound render target views.
			/// </summary>
			/// <param name="count">The number of render target views to retrieve.</param>
			/// <returns>An array of bound render target views.</returns>
			array<RenderTargetView^>^ GetRenderTargets( int count );

			/// <summary>
			/// Gets the set of currently bound unordered access views.
			/// </summary>
			/// <param name="startSlot">Zero-based index of the first unordered access view to retrieve.</param>
			/// <param name="count">The number of views to retrieve.</param>
			/// <returns>An array of bound unordered access views.</returns>
			array<UnorderedAccessView^>^ GetUnorderedAccessViews( int startSlot, int count );
		};
	}
};
