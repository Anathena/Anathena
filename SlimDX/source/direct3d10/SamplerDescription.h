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

#include "../math/color4.h"

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{	
		public value class SamplerDescription : System::IEquatable<SamplerDescription>
		{
		internal:
			SamplerDescription( const D3D10_SAMPLER_DESC& native );
			
			D3D10_SAMPLER_DESC CreateNativeVersion();
			
		public:
			property Filter Filter;
			property TextureAddressMode AddressU;
			property TextureAddressMode AddressV;
			property TextureAddressMode AddressW;
			property float MipLodBias;
			property int MaximumAnisotropy;
			property Comparison ComparisonFunction;
			property Color4 BorderColor;
			property float MinimumLod;
			property float MaximumLod;

			static bool operator == ( SamplerDescription left, SamplerDescription right );
			static bool operator != ( SamplerDescription left, SamplerDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( SamplerDescription other );
			static bool Equals( SamplerDescription% value1, SamplerDescription% value2 );
		};
	}
};