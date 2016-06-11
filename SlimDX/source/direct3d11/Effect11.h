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

#include "../ComObject.h"
#include "../d3dcompiler/ShaderBytecodeDC.h"

#include "Enums11.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		ref class EffectConstantBuffer;
		ref class EffectTechnique;
		ref class EffectVariable;
		ref class EffectGroup;
		ref class ClassLinkage;
		ref class ShaderBytecode;
		value class EffectDescription;
		
		/// <summary>
		/// Manages a set of state objects, resources and shaders for implementing a rendering effect.
		/// </summary>
		/// <unmanaged>ID3DX11Effect</unmanaged>
		public ref class Effect : public ComObject
		{
			COMOBJECT(ID3DX11Effect, Effect);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="Effect"/> class.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="data">The compiled effect source.</param>
			Effect(SlimDX::Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ data);

			/// <summary>
			/// Gets the effect's description.
			/// </summary>
			property EffectDescription Description
			{
				EffectDescription get();
			}
			
			/// <summary>
			/// Indicates whether the effect reflection metadata has been removed from memory.
			/// </summary>
			property bool IsOptimized
			{
				bool get();
			}
			
			/// <summary>
			/// Indicates whether the effect contains valid syntax.
			/// </summary>
			property bool IsValid
			{
				bool get();
			}
			
			/// <summary>
			/// Gets the device used to create the effect.
			/// </summary>
			property Device^ Device
			{
				SlimDX::Direct3D11::Device^ get();
			}
			
			/// <summary>
			/// Gets a class linkage interface for the effect.
			/// </summary>
			property SlimDX::Direct3D11::ClassLinkage^ ClassLinkage
			{
				SlimDX::Direct3D11::ClassLinkage^ get();
			}

			/// <summary>
			/// Get a constant buffer by index.
			/// </summary>
			/// <param name="index">The zero-based index of the constant buffer to retrieve.</param>
			/// <returns>The constant buffer at the specified index.</returns>
			EffectConstantBuffer^ GetConstantBufferByIndex( int index );

			/// <summary>
			/// Get a constant buffer by name.
			/// </summary>
			/// <param name="name">The name of the constant buffer to retrieve.</param>
			/// <returns>The constant buffer with the given name.</returns>
			EffectConstantBuffer^ GetConstantBufferByName( System::String^ name );
			
			/// <summary>
			/// Get a technique by index.
			/// </summary>
			/// <param name="index">The zero-based index of the technique to retrieve.</param>
			/// <returns>The technique at the specified index.</returns>
			EffectTechnique^ GetTechniqueByIndex( int index );

			/// <summary>
			/// Get a technique by name.
			/// </summary>
			/// <param name="name">The name of the technique to retrieve.</param>
			/// <returns>The technique with the given name.</returns>
			EffectTechnique^ GetTechniqueByName( System::String^ name );

			/// <summary>
			/// Get an effect group by index.
			/// </summary>
			/// <param name="index">The zero-based index of the effect group to retrieve.</param>
			/// <returns>The effect group at the specified index.</returns>
			EffectGroup^ GetGroupByIndex( int index );

			/// <summary>
			/// Get an effect group by name.
			/// </summary>
			/// <param name="name">The name of the effect group to retrieve.</param>
			/// <returns>The effect group with the given name.</returns>
			EffectGroup^ GetGroupByName( System::String^ name );
			
			/// <summary>
			/// Get a variable by index.
			/// </summary>
			/// <param name="index">The zero-based index of the variable to retrieve.</param>
			/// <returns>The variable at the specified index.</returns>
			EffectVariable^ GetVariableByIndex( int index );

			/// <summary>
			/// Get a variable by name.
			/// </summary>
			/// <param name="name">The name of the variable to retrieve.</param>
			/// <returns>The variable with the given name.</returns>
			EffectVariable^ GetVariableByName( System::String^ name );

			/// <summary>
			/// Get a variable by semantic.
			/// </summary>
			/// <param name="name">The semantic of the variable to retrieve.</param>
			/// <returns>The variable with the given semantic.</returns>
			EffectVariable^ GetVariableBySemantic( System::String^ name );

			/// <summary>
			/// Creates a copy of the effect interface.
			/// </summary>
			/// <returns>The cloned effect.</returns>
			Effect^ Clone();

			/// <summary>
			/// Creates a copy of the effect interface.
			/// </summary>
			/// <param name="forceNonSingle">Indicates whether to ignore all "single" qualifiers on cbuffers. All cbuffers will have their own buffers created in the cloned effect.</param>
			/// <returns>The cloned effect.</returns>
			Effect^ Clone(bool forceNonSingle);
			
			/// <summary>
			/// Minimize the amount of memory required for an effect by stripping effect metadata.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Optimize();
		};
	}
}
