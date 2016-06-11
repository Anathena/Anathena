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
#include "../math/Matrix.h"
#include "../math/Vector4.h"

#include "ConstantTableDescription.h"
#include "ConstantDescription.h"

namespace SlimDX
{
	ref class DataStream;

	namespace Direct3D9
	{
		ref class EffectHandle;
		ref class Device;
		
		/// <summary>
		/// Used to access the constant table. This table contains the variables that are used by high-level language shaders and effects.
		/// </summary>
		/// <unmanaged>ID3DXConstantTable</unmanaged>
		public ref class ConstantTable : public ComObject
		{
			COMOBJECT(ID3DXConstantTable, ConstantTable);

		public:
			EffectHandle^ GetConstant( EffectHandle^ handle, int index );
			EffectHandle^ GetConstant( EffectHandle^ handle, System::String^ name );
			EffectHandle^ GetConstantElement( EffectHandle^ handle, int index );
			ConstantDescription GetConstantDescription( EffectHandle^ handle );
			array<ConstantDescription>^ GetConstantDescriptionArray( EffectHandle^ handle );
			
			int GetSamplerIndex( EffectHandle^ sampler );
			Result SetDefaults( Device^ device );

			/// <summary>
			/// Sets the value of a parameter using the specified data.
			/// </summary>
			/// <param name="parameter">The parameter whose value is to be set.</param>
			/// <param name="value">The new value for the parameter.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			generic<typename T> where T : value class
				Result SetValue( Device^ device, EffectHandle^ parameter, T value );

			/// <summary>
			/// Sets the value of a parameter using the specified data.
			/// </summary>
			/// <param name="parameter">The array parameter whose value is to be set.</param>
			/// <param name="values">The array of new values for the array parameter.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			generic<typename T> where T : value class
				Result SetValue( Device^ device, EffectHandle^ parameter, array<T>^ values );

			property DataStream^ Buffer
			{
				DataStream^ get();
			}

			property int BufferSize
			{
				int get() { return InternalPointer->GetBufferSize(); }
			}

			property ConstantTableDescription Description
			{
				ConstantTableDescription get();
			}
		};
	}
}
