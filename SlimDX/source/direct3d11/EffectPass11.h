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

#include "../Result.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class DeviceContext;
		ref class EffectVariable;
		ref class StateBlockMask;
		value class EffectPassDescription;
		value class EffectPassShaderDescription;

		/// <summary>
		/// Encapsulates state assignments within a technique.
		/// </summary>
		/// <unmanaged>ID3DX11EffectPass</unmanaged>
		public ref class EffectPass
		{
		private:
			ID3DX11EffectPass* m_Pointer;

		internal:
			EffectPass( ID3DX11EffectPass* pointer );
			
		public:
			/// <summary>
			/// Gets the effect pass's description.
			/// </summary>
			property EffectPassDescription Description
			{
				EffectPassDescription get();
			}
			
			/// <summary>
			/// Indicates whether the pass represents a valid interface.
			/// </summary>
			property bool IsValid
			{
				bool get();
			}
			
			/// <summary>
			/// Gets a description for the pass's geometry shader.
			/// </summary>
			property EffectPassShaderDescription GeometryShaderDescription
			{
				EffectPassShaderDescription get();
			}
			
			/// <summary>
			/// Gets a description for the pass's vertex shader.
			/// </summary>
			property EffectPassShaderDescription VertexShaderDescription
			{
				EffectPassShaderDescription get();
			}

			/// <summary>
			/// Gets a description for the pass's pixel shader.
			/// </summary>
			property EffectPassShaderDescription PixelShaderDescription
			{
				EffectPassShaderDescription get();
			}

			/// <summary>
			/// Gets a description for the pass's hull shader.
			/// </summary>
			property EffectPassShaderDescription HullShaderDescription
			{
				EffectPassShaderDescription get();
			}

			/// <summary>
			/// Gets a description for the pass's domain shader.
			/// </summary>
			property EffectPassShaderDescription DomainShaderDescription
			{
				EffectPassShaderDescription get();
			}

			/// <summary>
			/// Gets a description for the pass's compute shader.
			/// </summary>
			property EffectPassShaderDescription ComputeShaderDescription
			{
				EffectPassShaderDescription get();
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectPass"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectPass( System::IntPtr pointer );
			
			/// <summary>
			/// Get an annotation by index.
			/// </summary>
			/// <param name="index">The zero-based index of the annotation to retrieve.</param>
			/// <returns>The annotation at the specified index.</returns>
			EffectVariable^ GetAnnotationByIndex( int index );

			/// <summary>
			/// Get an annotation by name.
			/// </summary>
			/// <param name="name">The name of the annotation to retrieve.</param>
			/// <returns>The annotation with the given name.</returns>
			EffectVariable^ GetAnnotationByName( System::String^ name );
			
			/// <summary>
			/// Set the state contained in a pass to the device.
			/// </summary>
			/// <param name="context">The device context to use to set the state.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Apply( DeviceContext^ context );

			/// <summary>
			/// Generate a mask for state changes during the pass.
			/// </summary>
			/// <returns>The created state block mask.</returns>
			StateBlockMask^ ComputeStateBlockMask();
		};
	}
};
