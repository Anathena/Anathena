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
#include "../math/Vector2.h"
#include "../math/Vector3.h"
#include "../math/Vector4.h"

#include "Device.h"
#include "EffectHandle.h"

#include "EffectDescription9.h"
#include "FunctionDescription.h"
#include "TechniqueDescription.h"
#include "PassDescription.h"
#include "ParameterDescription.h"

namespace SlimDX
{
	ref class DataStream;

	namespace Direct3D9
	{
		/// <summary>
		/// Provides methods for getting and setting effect parameters such as constants, functions, shaders, and techniques.
		/// </summary>
		/// <unmanaged>ID3DXBaseEffect</unmanaged>
		public ref class BaseEffect abstract : public ComObject
		{
			COMOBJECT_BASE(ID3DXBaseEffect);

		protected:
			/// <summary>
			/// Initializes a new instance of the <see cref="BaseEffect"/> class.
			/// </summary>
			BaseEffect() { }

		public:
			/// <summary>
			/// Gets the handle of an annotation.
			/// </summary>
			/// <param name="handle">Handle of a technique, pass, or top-level parameter.</param>
			/// <param name="index">Annotation index.</param>
			/// <returns>The handle of the annotation.</returns>
			EffectHandle^ GetAnnotation( EffectHandle^ handle, int index );

			/// <summary>
			/// Gets the handle of an annotation.
			/// </summary>
			/// <param name="handle">Handle of a technique, pass, or top-level parameter.</param>
			/// <param name="name">Name of the annotation.</param>
			/// <returns>The handle of the annotation.</returns>
			EffectHandle^ GetAnnotation( EffectHandle^ handle, System::String^ name );

			/// <summary>
			/// Gets the handle of a parameter.
			/// </summary>
			/// <param name="parameter">Handle of the parameter, or <c>null</c> for top-level parameters.</param>
			/// <param name="index">Parameter index.</param>
			/// <returns>The handle of the parameter.</returns>
			EffectHandle^ GetParameter( EffectHandle^ parameter, int index );

			/// <summary>
			/// Gets the handle of a parameter.
			/// </summary>
			/// <param name="parameter">Handle of the parameter, or <c>null</c> for top-level parameters.</param>
			/// <param name="name">Name of the parameter.</param>
			/// <returns>The handle of the parameter.</returns>
			EffectHandle^ GetParameter( EffectHandle^ parameter, System::String^ name );

			/// <summary>
			/// Gets the handle of a parameter by looking up its semantic.
			/// </summary>
			/// <param name="parameter">Handle of the parameter, or <c>null</c> for top-level parameters.</param>
			/// <param name="name">The name of the semantic.</param>
			/// <returns>The handle of the parameter.</returns>
			EffectHandle^ GetParameterBySemantic( EffectHandle^ parameter, System::String^ name );

			/// <summary>
			/// Gets the handle of an array element parameter.
			/// </summary>
			/// <param name="parameter">Handle of the array.</param>
			/// <param name="index">Array parameter index.</param>
			/// <returns>The handle of the parameter.</returns>
			EffectHandle^ GetParameterElement( EffectHandle^ parameter, int index );

			/// <summary>
			/// Gets a parameter or annotation description.
			/// </summary>
			/// <param name="parameter">Handle of the parameter or annotation.</param>
			/// <returns>The description of the specified parameter or annotation.</returns>
			ParameterDescription GetParameterDescription( EffectHandle^ parameter );

			/// <summary>
			/// Gets the handle of a function.
			/// </summary>
			/// <param name="index">Function index.</param>
			/// <returns>The handle of the function.</returns>
			EffectHandle^ GetFunction( int index );

			/// <summary>
			/// Gets the handle of a function.
			/// </summary>
			/// <param name="name">Name of the function.</param>
			/// <returns>The handle of the function.</returns>
			EffectHandle^ GetFunction( System::String^ name );

			/// <summary>
			/// Gets a function description.
			/// </summary>
			/// <param name="handle">Handle to the function.</param>
			/// <returns>The function description.</returns>
			FunctionDescription GetFunctionDescription( EffectHandle^ handle );

			/// <summary>
			/// Gets the handle of a technique.
			/// </summary>
			/// <param name="index">Technique index.</param>
			/// <returns>The handle of the technique.</returns>
			EffectHandle^ GetTechnique( int index );

			/// <summary>
			/// Gets the handle of a technique.
			/// </summary>
			/// <param name="name">Name of the technique.</param>
			/// <returns>The handle of the technique.</returns>
			EffectHandle^ GetTechnique( System::String^ name );

			/// <summary>
			/// Gets a technique description.
			/// </summary>
			/// <param name="handle">Handle to the technique.</param>
			/// <returns>The technique description.</returns>
			TechniqueDescription GetTechniqueDescription( EffectHandle^ handle );

			/// <summary>
			/// Gets the handle of a pass.
			/// </summary>
			/// <param name="technique">Handle of the parent technique.</param>
			/// <param name="index">Pass index.</param>
			/// <returns>The handle of the pass.</returns>
			EffectHandle^ GetPass( EffectHandle^ technique, int index );

			/// <summary>
			/// Gets the handle of a pass.
			/// </summary>
			/// <param name="technique">Handle of the parent technique.</param>
			/// <param name="name">Name of the pass.</param>
			/// <returns>The handle of the pass.</returns>
			EffectHandle^ GetPass( EffectHandle^ technique, System::String^ name );

			/// <summary>
			/// Gets the description of a pass.
			/// </summary>
			/// <param name="handle">Handle to the pass.</param>
			/// <returns>The pass description.</returns>
			PassDescription GetPassDescription( EffectHandle^ handle );
			
			/// <summary>
			/// Gets the value of the specified parameter.
			/// </summary>
			/// <param name="parameter">Handle of the parameter.</param>
			/// <returns>The value of the parameter.</returns>
			generic<typename T> where T : value class
				T GetValue( EffectHandle^ parameter );

			/// <summary>
			/// Gets the value of the specified parameter.
			/// </summary>
			/// <param name="parameter">Handle of the parameter.</param>
			/// <returns>The value of the parameter.</returns>
			generic<typename T> where T : value class
				array<T>^ GetValue( EffectHandle^ parameter, int count );

			/// <summary>
			/// Sets the value of a parameter using the specified data.
			/// </summary>
			/// <param name="parameter">The parameter whose value is to be set.</param>
			/// <param name="value">The new value for the parameter.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			generic<typename T> where T : value class
				Result SetValue( EffectHandle^ parameter, T value );

			/// <summary>
			/// Sets the value of a parameter using the specified data.
			/// </summary>
			/// <param name="parameter">The array parameter whose value is to be set.</param>
			/// <param name="values">The array of new values for the array parameter.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			generic<typename T> where T : value class
				Result SetValue( EffectHandle^ parameter, array<T>^ values );

			/// <summary>
			/// Gets the value of the specified parameter as a texture.
			/// </summary>
			/// <param name="parameter">Handle of the parameter.</param>
			/// <returns>The texture value of the parameter.</returns>
			BaseTexture^ GetTexture( EffectHandle^ parameter );

			/// <summary>
			/// Sets the value of a parameter using the specified data.
			/// </summary>
			/// <param name="parameter">The parameter whose value is to be set.</param>
			/// <param name="value">The new value for the parameter.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetTexture( EffectHandle^ parameter, BaseTexture^ value );

			/// <summary>
			/// Gets the value of the specified parameter as a string.
			/// </summary>
			/// <param name="parameter">Handle of the parameter.</param>
			/// <returns>The string value of the parameter.</returns>
			System::String^ GetString( EffectHandle^ parameter );

			/// <summary>
			/// Sets the value of a parameter using the specified data.
			/// </summary>
			/// <param name="parameter">The parameter whose value is to be set.</param>
			/// <param name="value">The new value for the parameter.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetString( EffectHandle^ parameter, System::String^ value );

			/// <summary>
			/// Gets a pixel shader from the effect.
			/// </summary>
			/// <param name="parameter">The effect parameter identifying the shader.</param>
			/// <returns>The requested pixel shader.</returns>
			PixelShader^ GetPixelShader( EffectHandle^ parameter );

			/// <summary>
			/// Gets a vertex shader from the effect.
			/// </summary>
			/// <param name="parameter">The effect parameter identifying the shader.</param>
			/// <returns>The requested vertex shader.</returns>
			VertexShader^ GetVertexShader( EffectHandle^ parameter );

			/// <summary>
			/// Gets the effect description.
			/// </summary>
			property EffectDescription Description
			{
				EffectDescription get();
			}
		};
   }
}
