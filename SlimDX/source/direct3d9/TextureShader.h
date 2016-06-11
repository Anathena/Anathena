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
	namespace Direct3D9
	{
		/// <summary>
		/// Represents a compiled shader designed to fill textures with image data.
		/// </summary>
		/// <unmanaged>ID3DXTextureShader</unmanaged>
		public ref class TextureShader : public ComObject
		{
			COMOBJECT(ID3DXTextureShader, TextureShader);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="TextureShader"/> class.
			/// </summary>
			/// <param name="stream">A stream of compiled shader data.</param>
			TextureShader( DataStream^ stream );

			/// <summary>
			/// Gets a constant by looking up its index.
			/// </summary>
			/// <param name="parent">The parent handle.</param>
			/// <param name="index">The index of the constant.</param>
			/// <returns>A handle to the constant data.</returns>
			EffectHandle^ GetConstant( EffectHandle^ parent, int index );

			/// <summary>
			/// Gets a constant by looking up its name.
			/// </summary>
			/// <param name="parent">The parent handle.</param>
			/// <param name="name">The name of the constant.</param>
			/// <returns>A handle to the constant data.</returns>
			EffectHandle^ GetConstant( EffectHandle^ parent, System::String^ name );

			/// <summary>
			/// Gets a constant from the constant table.
			/// </summary>
			/// <param name="handle">The array handle.</param>
			/// <param name="index">The index of the constant.</param>
			/// <returns>A handle to the constant data.</returns>
			EffectHandle^ GetConstantElement( EffectHandle^ handle, int index );

			/// <summary>
			/// Gets a description for a constant.
			/// </summary>
			/// <param name="handle">The constant handle.</param>
			/// <returns>The description of the constant.</returns>
			ConstantDescription GetConstantDescription( EffectHandle^ handle );

			/// <summary>
			/// Gets a list of descriptions for an array constant.
			/// </summary>
			/// <param name="handle">The array handle.</param>
			/// <returns>The list of constant descriptions.</returns>
			array<ConstantDescription>^ GetConstantDescriptionArray( EffectHandle^ handle );

			/// <summary>
			/// Sets an effect parameter value.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="value">The parameter value.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, bool value );

			/// <summary>
			/// Sets an array of effect parameter values.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="values">The parameter values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, array<bool>^ values );

			/// <summary>
			/// Sets an effect parameter value.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="value">The parameter value.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, int value );

			/// <summary>
			/// Sets an array of effect parameter values.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="values">The parameter values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, array<int>^ values );

			/// <summary>
			/// Sets an effect parameter value.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="value">The parameter value.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, float value );

			/// <summary>
			/// Sets an array of effect parameter values.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="values">The parameter values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, array<float>^ values );

			/// <summary>
			/// Sets an effect parameter value.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="value">The parameter value.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, Vector4 value );

			/// <summary>
			/// Sets an array of effect parameter values.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="values">The parameter values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, array<Vector4>^ values );

			/// <summary>
			/// Sets an effect parameter value.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="value">The parameter value.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, Color4 value );

			/// <summary>
			/// Sets an array of effect parameter values.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="values">The parameter values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, array<Color4>^ values );

			/// <summary>
			/// Sets an effect parameter value.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="value">The parameter value.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, Matrix value );

			/// <summary>
			/// Sets an array of effect parameter values.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="values">The parameter values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValue( EffectHandle^ parameter, array<Matrix>^ values );

			/// <summary>
			/// Sets an effect parameter value.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="value">The parameter value.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValueTranspose( EffectHandle^ parameter, Matrix value );

			/// <summary>
			/// Sets an array of effect parameter values.
			/// </summary>
			/// <param name="parameter">The effect parameter.</param>
			/// <param name="values">The parameter values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetValueTranspose( EffectHandle^ parameter, array<Matrix>^ values );

			/// <summary>
			/// Resets constant values to those defined in the shader.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetDefaults();

			/// <summary>
			/// Gets the constant table description.
			/// </summary>
			property ConstantTableDescription Description
			{
				ConstantTableDescription get();
			}

			/// <summary>
			/// Gets the raw stream of constant data.
			/// </summary>
			property DataStream^ ConstantBuffer
			{
				DataStream^ get();
			}

			/// <summary>
			/// Gets the raw shader stream.
			/// </summary>
			property DataStream^ FunctionStream
			{
				DataStream^ get();
			}
		};
	}
}