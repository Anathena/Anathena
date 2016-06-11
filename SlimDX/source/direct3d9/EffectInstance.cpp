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
#include <d3d9.h>
#include <d3dx9.h>

#include "../Utilities.h"
#include "../DataStream.h"

#include "EffectInstance.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	EffectInstance EffectInstance::FromUnmanaged( const D3DXEFFECTINSTANCE &effect )
	{
		EffectInstance result;

		result.EffectFileName = gcnew String( effect.pEffectFilename );
		result.Defaults = gcnew array<EffectDefault>( effect.NumDefaults );

		for( unsigned int i = 0; i < effect.NumDefaults; i++ )
		{
			result.Defaults[i].ParameterName = gcnew String( effect.pDefaults[i].pParamName );
			result.Defaults[i].Type = static_cast<EffectDefaultType>( effect.pDefaults[i].Type );
			result.Defaults[i].Value = gcnew DataStream( effect.pDefaults[i].pValue, effect.pDefaults[i].NumBytes, true, false, true );
		}

		return result;
	}

	D3DXEFFECTINSTANCE EffectInstance::ToUnmanaged( EffectInstance effect )
	{
		D3DXEFFECTINSTANCE result;
		int count = effect.Defaults->Length;
		result.NumDefaults = count;

		// Manual Allocation: Handled properly
		// this is pretty damn ugly, but I don't think I can see a way around it
		// each method that calls this one must make sure to clean up this array
		// right now, all of them do
		result.pDefaults = new D3DXEFFECTDEFAULT[count];
		result.pEffectFilename = Utilities::AllocateNativeString( effect.EffectFileName );

		for( int i = 0; i < effect.Defaults->Length; i++ )
		{
			result.pDefaults[i].pParamName = Utilities::AllocateNativeString( effect.Defaults[i].ParameterName );
			result.pDefaults[i].Type = static_cast<D3DXEFFECTDEFAULTTYPE>( effect.Defaults[i].Type );
			result.pDefaults[i].NumBytes = static_cast<DWORD>( effect.Defaults[i].Value->Length );
			result.pDefaults[i].pValue = effect.Defaults[i].Value->PositionPointer;
		}

		return result;
	}

	array<EffectInstance>^ EffectInstance::FromBuffer( ID3DXBuffer* buffer, unsigned int count )
	{
		const D3DXEFFECTINSTANCE* source  = reinterpret_cast<const D3DXEFFECTINSTANCE*>( buffer->GetBufferPointer() );

		array<EffectInstance>^ destination = gcnew array<EffectInstance>( count );
		for( unsigned int i = 0; i < count; ++i )
		{
			destination[i].EffectFileName = gcnew String( source[i].pEffectFilename );
			destination[i].Defaults = gcnew array<EffectDefault>( source[i].NumDefaults );
			for( unsigned int x = 0; x < source[i].NumDefaults; ++x )
			{
				destination[i].Defaults[x].ParameterName = gcnew String( source[i].pDefaults[x].pParamName );
				destination[i].Defaults[x].Type = static_cast<EffectDefaultType>( source[i].pDefaults[x].Type );
				destination[i].Defaults[x].Value = gcnew DataStream( source[i].pDefaults[x].pValue, source[i].pDefaults[x].NumBytes, true, false, true );
			}
		}

		return destination;
	}

	bool EffectInstance::operator == ( EffectInstance left, EffectInstance right )
	{
		return EffectInstance::Equals( left, right );
	}

	bool EffectInstance::operator != ( EffectInstance left, EffectInstance right )
	{
		return !EffectInstance::Equals( left, right );
	}

	int EffectInstance::GetHashCode()
	{
		return EffectFileName->GetHashCode() + Defaults->GetHashCode();
	}

	bool EffectInstance::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectInstance>( value ) );
	}

	bool EffectInstance::Equals( EffectInstance value )
	{
		return ( EffectFileName == value.EffectFileName && Defaults == value.Defaults );
	}

	bool EffectInstance::Equals( EffectInstance% value1, EffectInstance% value2 )
	{
		return ( value1.EffectFileName == value2.EffectFileName && value1.Defaults == value2.Defaults );
	}
}
}