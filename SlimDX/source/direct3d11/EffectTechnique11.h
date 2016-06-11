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

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class EffectPass;
		ref class EffectVariable;
		ref class StateBlockMask;
		value class EffectTechniqueDescription;
		
		/// <summary>
		/// Represents an effect technique, which is a collection of rendering passes.
		/// </summary>
		/// <unmanaged>ID3DX11EffectTechnique</unmanaged>
		public ref class EffectTechnique
		{
			ID3DX11EffectTechnique *m_Pointer;
			
		internal:
			EffectTechnique( ID3DX11EffectTechnique* pointer );
			
		public:
			/// <summary>
			/// Gets the effect techniques's description.
			/// </summary>
			property EffectTechniqueDescription Description
			{
				EffectTechniqueDescription get();
			}
			
			/// <summary>
			/// Indicates whether the technique represents a valid interface.
			/// </summary>
			property bool IsValid
			{
				bool get();
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectTechnique"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectTechnique( System::IntPtr pointer );
			
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
			/// Get a pass by index.
			/// </summary>
			/// <param name="index">The zero-based index of the pass to retrieve.</param>
			/// <returns>The pass at the specified index.</returns>
			EffectPass^ GetPassByIndex( int index );

			/// <summary>
			/// Get a pass by name.
			/// </summary>
			/// <param name="name">The name of the pass to retrieve.</param>
			/// <returns>The pass with the given name.</returns>
			EffectPass^ GetPassByName( System::String^ name );

			/// <summary>
			/// Generate a mask for state changes during the technique.
			/// </summary>
			/// <returns>The created state block mask.</returns>
			StateBlockMask^ ComputeStateBlockMask();
		};
	}
};