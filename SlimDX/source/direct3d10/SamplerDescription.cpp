#include "stdafx.h"
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

#include <d3d10.h>

#include "SamplerDescription.h"

namespace SlimDX
{
namespace Direct3D10
{
	SamplerDescription::SamplerDescription( const D3D10_SAMPLER_DESC &native )
	{
		Filter = static_cast<Direct3D10::Filter>( native.Filter );
		AddressU = static_cast<TextureAddressMode>( native.AddressU );
		AddressV = static_cast<TextureAddressMode>( native.AddressV );
		AddressW = static_cast<TextureAddressMode>( native.AddressW );
		MipLodBias = native.MipLODBias;
		MaximumAnisotropy = native.MaxAnisotropy;
		ComparisonFunction = static_cast<Comparison>( native.ComparisonFunc );
		BorderColor = Color4( native.BorderColor[0], native.BorderColor[1], native.BorderColor[2], native.BorderColor[3] );
		MinimumLod = native.MinLOD;
		MaximumLod = native.MaxLOD;
	}

	D3D10_SAMPLER_DESC SamplerDescription::CreateNativeVersion()
	{
		D3D10_SAMPLER_DESC native;

		native.Filter = static_cast<D3D10_FILTER>( Filter );
		native.AddressU = static_cast<D3D10_TEXTURE_ADDRESS_MODE>( AddressU );
		native.AddressV = static_cast<D3D10_TEXTURE_ADDRESS_MODE>( AddressV );
		native.AddressW = static_cast<D3D10_TEXTURE_ADDRESS_MODE>( AddressW );
		native.MipLODBias = MipLodBias;
		native.MaxAnisotropy = MaximumAnisotropy;
		native.ComparisonFunc = static_cast<D3D10_COMPARISON_FUNC>( ComparisonFunction );
		native.MinLOD = MinimumLod;
		native.MaxLOD = MaximumLod;

		native.BorderColor[0] = BorderColor.Alpha;
		native.BorderColor[1] = BorderColor.Red;
		native.BorderColor[2] = BorderColor.Green;
		native.BorderColor[3] = BorderColor.Blue;
		
		return native;
	}

	bool SamplerDescription::operator == ( SamplerDescription left, SamplerDescription right )
	{
		return SamplerDescription::Equals( left, right );
	}

	bool SamplerDescription::operator != ( SamplerDescription left, SamplerDescription right )
	{
		return !SamplerDescription::Equals( left, right );
	}

	int SamplerDescription::GetHashCode()
	{
		return Filter.GetHashCode() + AddressU.GetHashCode() + AddressV.GetHashCode() +
			AddressW.GetHashCode() + MipLodBias.GetHashCode() + MaximumAnisotropy.GetHashCode() +
			ComparisonFunction.GetHashCode() + BorderColor.GetHashCode() + MinimumLod.GetHashCode() + MaximumLod.GetHashCode();
	}

	bool SamplerDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<SamplerDescription>( value ) );
	}

	bool SamplerDescription::Equals( SamplerDescription value )
	{
		return ( Filter == value.Filter && AddressU == value.AddressU && AddressV == value.AddressV &&
			AddressW == value.AddressW && BorderColor == value.BorderColor && ComparisonFunction == value.ComparisonFunction &&
			MaximumAnisotropy == value.MaximumAnisotropy && MaximumLod == value.MaximumLod &&
			MinimumLod == value.MinimumLod && MipLodBias == value.MipLodBias );
	}

	bool SamplerDescription::Equals( SamplerDescription% value1, SamplerDescription% value2 )
	{
		return ( value1.Filter == value2.Filter && value1.AddressU == value2.AddressU && value1.AddressV == value2.AddressV &&
			value1.AddressW == value2.AddressW && value1.BorderColor == value2.BorderColor && value1.ComparisonFunction == value2.ComparisonFunction &&
			value1.MaximumAnisotropy == value2.MaximumAnisotropy && value1.MaximumLod == value2.MaximumLod &&
			value1.MinimumLod == value2.MinimumLod && value1.MipLodBias == value2.MipLodBias );
	}
}
}