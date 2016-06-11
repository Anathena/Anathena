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
	namespace Direct3D10
	{
		ref class EffectPass;
		ref class EffectVariable;
		value class EffectTechniqueDescription;
		
		public ref class EffectTechnique
		{
			ID3D10EffectTechnique *m_Pointer;
			
		internal:
			EffectTechnique( ID3D10EffectTechnique* pointer );
			
		public:
			property EffectTechniqueDescription Description
			{
				EffectTechniqueDescription get();
			}
			
			property bool IsValid
			{
				bool get();
			}
			
			EffectTechnique( System::IntPtr pointer );
			
			EffectVariable^ GetAnnotationByIndex( int index );
			EffectVariable^ GetAnnotationByName( System::String^ name );
			
			EffectPass^ GetPassByIndex( int index );
			EffectPass^ GetPassByName( System::String^ name );
		};
	}
};