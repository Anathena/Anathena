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

#include "RenderTargetBlendDescription11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Provides a description for blending state objects.
		/// </summary>
		/// <unmanaged>D3D11_BLEND_DESC</unmanaged>
		public value class BlendStateDescription : System::IEquatable<BlendStateDescription>
		{
		private:
			array<RenderTargetBlendDescription>^ m_RenderTargets;

			void ConstructLazyProperties();

		internal:
			BlendStateDescription( const D3D11_BLEND_DESC& native );
			
			D3D11_BLEND_DESC CreateNativeVersion();
			
		public:
			/// <summary>
			/// Gets or sets a value specifying whether alpha-to-coverage is used as a multisampling technique when setting a pixel to a rendertarget.
			/// </summary>
			property bool AlphaToCoverageEnable;

			/// <summary>
			/// Gets or sets a value determining whether independent blending is enabled for simultaneous render targets.
			/// </summary>
			property bool IndependentBlendEnable;

			/// <summary>
			/// Gets the set of render-target-blend descriptors describing independent blending operations for up to eight simultaneous render targets.
			/// </summary>
			property array<RenderTargetBlendDescription>^ RenderTargets
			{
				array<RenderTargetBlendDescription>^ get();
			}

			static bool operator == ( BlendStateDescription left, BlendStateDescription right );
			static bool operator != ( BlendStateDescription left, BlendStateDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( BlendStateDescription other );
			static bool Equals( BlendStateDescription% value1, BlendStateDescription% value2 );
		};
	}
}
