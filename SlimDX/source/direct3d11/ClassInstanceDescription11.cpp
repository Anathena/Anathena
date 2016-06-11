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
#include "stdafx.h"
#include <d3d11.h>

#include "ClassInstanceDescription11.h"

namespace SlimDX
{
namespace Direct3D11
{
	ClassInstanceDescription::ClassInstanceDescription( const D3D11_CLASS_INSTANCE_DESC& native )
	{
		InstanceId = native.InstanceId;
		InstanceIndex = native.InstanceIndex;
		TypeId = native.TypeId;
		ConstantBuffer = native.ConstantBuffer;
		BaseConstantBufferOffset = native.BaseConstantBufferOffset;
		BaseTexture = native.BaseTexture;
		BaseSampler = native.BaseSampler;
		IsCreated = native.Created != 0;
	}
	
	D3D11_CLASS_INSTANCE_DESC ClassInstanceDescription::CreateNativeVersion()
	{
		D3D11_CLASS_INSTANCE_DESC native;

		native.InstanceId = InstanceId;
		native.InstanceIndex = InstanceIndex;
		native.TypeId = TypeId;
		native.ConstantBuffer = ConstantBuffer;
		native.BaseConstantBufferOffset = BaseConstantBufferOffset;
		native.BaseTexture = BaseTexture;
		native.BaseSampler = BaseSampler;
		native.Created = IsCreated;
		
		return native;
	}

	bool ClassInstanceDescription::operator == ( ClassInstanceDescription left, ClassInstanceDescription right )
	{
		return ClassInstanceDescription::Equals( left, right );
	}

	bool ClassInstanceDescription::operator != ( ClassInstanceDescription left, ClassInstanceDescription right )
	{
		return !ClassInstanceDescription::Equals( left, right );
	}

	int ClassInstanceDescription::GetHashCode()
	{
		return InstanceId.GetHashCode() + InstanceIndex.GetHashCode() + TypeId.GetHashCode()
			 + ConstantBuffer.GetHashCode() + BaseConstantBufferOffset.GetHashCode() + BaseTexture.GetHashCode()
			 + BaseSampler.GetHashCode() + IsCreated.GetHashCode();
	}

	bool ClassInstanceDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ClassInstanceDescription>( value ) );
	}

	bool ClassInstanceDescription::Equals( ClassInstanceDescription value )
	{
		return ( InstanceId == value.InstanceId && InstanceIndex == value.InstanceIndex && TypeId == value.TypeId
			 && ConstantBuffer == value.ConstantBuffer && BaseConstantBufferOffset == value.BaseConstantBufferOffset && BaseTexture == value.BaseTexture
			 && BaseSampler == value.BaseSampler && IsCreated == value.IsCreated );
	}

	bool ClassInstanceDescription::Equals( ClassInstanceDescription% value1, ClassInstanceDescription% value2 )
	{
		return ( value1.InstanceId == value2.InstanceId && value1.InstanceIndex == value2.InstanceIndex && value1.TypeId == value2.TypeId
			 && value1.ConstantBuffer == value2.ConstantBuffer && value1.BaseConstantBufferOffset == value2.BaseConstantBufferOffset && value1.BaseTexture == value2.BaseTexture
			 && value1.BaseSampler == value2.BaseSampler && value1.IsCreated == value2.IsCreated );
	}
}
}
