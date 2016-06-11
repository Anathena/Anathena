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

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{	
		public ref class StateBlockMask : System::ICloneable, System::IEquatable<StateBlockMask^>
		{
		private:
			bool m_VS;
			initonly array<bool>^ m_VSSamplers;
			initonly array<bool>^ m_VSShaderResources;
			initonly array<bool>^ m_VSConstantBuffers;
			bool m_GS;
			initonly array<bool>^ m_GSSamplers;       
			initonly array<bool>^ m_GSShaderResources;
			initonly array<bool>^ m_GSConstantBuffers;
			bool m_PS;
			initonly array<bool>^ m_PSSamplers;       
			initonly array<bool>^ m_PSShaderResources;
			initonly array<bool>^ m_PSConstantBuffers;
			initonly array<bool>^ m_IAVertexBuffers;  
			bool m_IAIndexBuffer;
			bool m_IAInputLayout;
			bool m_IAPrimitiveTopology;
			bool m_OMRenderTargets;
			bool m_OMDepthStencilState;
			bool m_OMBlendState;
			bool m_RSViewports;
			bool m_RSScissorRects;
			bool m_RSRasterizerState;
			bool m_SOBuffers;
			bool m_Predication;
			
		internal:
			StateBlockMask( const D3D10_STATE_BLOCK_MASK& description );
			
			D3D10_STATE_BLOCK_MASK CreateNativeVersion();
			
			virtual System::Object^ Clone2() = System::ICloneable::Clone;
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="SlimDX::Direct3D10::StateBlockMask"/> class.
			/// </summary>
			StateBlockMask();

			/// <summary>
			/// Initializes a new instance of the <see cref="SlimDX::Direct3D10::StateBlockMask"/> class.
			/// </summary>
			/// <param name="other">The <see cref="SlimDX::Direct3D10::StateBlockMask"/> to copy initial values from.</param>
			StateBlockMask( StateBlockMask^ other );
			
			/// <summary>
			/// Gets or sets a value indicating whether vertex shader state is enabled by the mask.
			/// </summary>
			property bool VertexShader
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets a collection of values indicating which vertex shader sampler state is enabled by the mask.
			/// </summary>
			property array<bool>^ VertexShaderSamplers
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets a collection of values indicating which vertex shader resource state is enabled by the mask.
			/// </summary>
			property array<bool>^ VertexShaderResources
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets a collection of values indicating which vertex shader sampler state is enabled by the mask.
			/// </summary>
			property array<bool>^ VertexShaderConstantBuffers
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether geometry shader state is enabled by the mask.
			/// </summary>
			property bool GeometryShader
			{
				bool get();
				void set( bool value );
			}
		
			/// <summary>
			/// Gets a collection of values indicating which geometry shader sampler state is enabled by the mask.
			/// </summary>
			property array<bool>^ GeometryShaderSamplers
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets a collection of values indicating which geometry shader resource state is enabled by the mask.
			/// </summary>
			property array<bool>^ GeometryShaderResources
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets a collection of values indicating which geometry shader constant buffer state is enabled by the mask.
			/// </summary>
			property array<bool>^ GeometryShaderConstantBuffers
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether pixel shader state is enabled by the mask.
			/// </summary>
			property bool PixelShader
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Gets a collection of values indicating which pixel shader sampler state is enabled by the mask.
			/// </summary>
			property array<bool>^ PixelShaderSamplers
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets a collection of values indicating which pixel shader resource state is enabled by the mask.
			/// </summary>
			property array<bool>^ PixelShaderResources
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets a collection of values indicating which pixel shader constant buffer state is enabled by the mask.
			/// </summary>
			property array<bool>^ PixelShaderConstantBuffers
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets a collection of values indicating which vertex buffer state is enabled by the mask.
			/// </summary>
			property array<bool>^ VertexBuffers
			{
				array<bool>^ get();
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether index bufer state is enabled by the mask.
			/// </summary>
			property bool IndexBuffer
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether input layout state is enabled by the mask.
			/// </summary>
			property bool InputLayout
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether primitive topology state is enabled by the mask.
			/// </summary>
			property bool PrimitiveTopology
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether render target state is enabled by the mask.
			/// </summary>
			property bool RenderTargets
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether depth and stencil state is enabled by the mask.
			/// </summary>
			property bool DepthStencilState
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether blend state is enabled by the mask.
			/// </summary>
			property bool BlendState
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether viewport state is enabled by the mask.
			/// </summary>
			property bool Viewports
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether scissor rectangle state is enabled by the mask.
			/// </summary>
			property bool ScissorRectangles
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether rasterizer state is enabled by the mask.
			/// </summary>
			property bool RasterizerState
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether stream output buffer state is enabled by the mask.
			/// </summary>
			property bool StreamOutputBuffers
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Gets or sets a value indicating whether predication state is enabled by the mask.
			/// </summary>
			property bool Predication
			{
				bool get();
				void set( bool value );
			}
			
			/// <summary>
			/// Clones the instance and returns a new object containing the same values.
			/// </summary>
			/// <returns>A new <see cref="StateBlockMask"/> object containing the same values as the current instance.</returns>
			StateBlockMask^ Clone();
			
			StateBlockMask^ Difference(StateBlockMask^ other);
			static StateBlockMask^ DisableAll();
			static StateBlockMask^ EnableAll();
			StateBlockMask^ Intersect(StateBlockMask^ other);
			StateBlockMask^ Union(StateBlockMask^ other);

			/// <summary>
			/// Tests for equality between two StateBlockMasks.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( StateBlockMask^ left, StateBlockMask^ right );
			
			/// <summary>
			/// Tests for inequality between two StateBlockMasks.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( StateBlockMask^ left, StateBlockMask^ right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;
			
			/// <summary>
			/// Returns a value indicating whether this instance is equal to the specified object.
			/// </summary>
			/// <param name="obj">An object to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="obj"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( System::Object^ obj ) override;
			
			/// <summary>
			/// Returns a value indicating whether this instance is equal to the specified object.
			/// </summary>
			/// <param name="other">A <see cref="StateBlockMask"/> to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( StateBlockMask^ other );
			
			/// <summary>
			/// Returns a value indicating whether the two StateBlockMask are equivalent.
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> has the same value as <paramref name="value2"/>; otherwise, <c>false</c>.</returns>
			static bool Equals( StateBlockMask^ value1, StateBlockMask^ value2 );
		};
	}
};