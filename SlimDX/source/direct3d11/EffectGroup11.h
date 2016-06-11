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
		value class EffectGroupDescription;

		/// <summary>
		/// Represents an effect group.
		/// </summary>
		/// <unmanaged>ID3DX11EffectGroup</unmanaged>
		public ref class EffectGroup
		{
		private:
			ID3DX11EffectGroup *InternalPointer;

		internal:
			EffectGroup(ID3DX11EffectGroup *pointer);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectGroup"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectGroup( System::IntPtr pointer );

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
			/// Indicates whether the effect group represents a valid interface.
			/// </summary>
			property bool IsValid
			{
				bool get();
			}

			/// <summary>
			/// Gets the effect group's description.
			/// </summary>
			property EffectGroupDescription Description
			{
				EffectGroupDescription get();
			}
		};
	}
}