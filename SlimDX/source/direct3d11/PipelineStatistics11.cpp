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

#include <d3d11.h>

#include "PipelineStatistics11.h"

namespace SlimDX
{
namespace Direct3D11
{
	PipelineStatistics::PipelineStatistics( const D3D11_QUERY_DATA_PIPELINE_STATISTICS& native )
	{
		InputAssemblerVertices = native.IAVertices;
		InputAssemblerPrimitives = native.IAPrimitives;
		VertexShaderInvocations = native.VSInvocations;
		GeometryShaderInvocations = native.GSInvocations;
		GeometryShaderPrimitives = native.GSPrimitives;
		RasterizedPrimitives = native.CInvocations;
		RenderedPrimitives = native.CPrimitives;
		PixelShaderInvocations = native.PSInvocations;
		HullShaderInvocations = native.HSInvocations;
		DomainShaderInvocations = native.DSInvocations;
		ComputeShaderInvocations = native.CSInvocations;
	}
	
	D3D11_QUERY_DATA_PIPELINE_STATISTICS PipelineStatistics::CreateNativeVersion()
	{
		D3D11_QUERY_DATA_PIPELINE_STATISTICS native;
		
		native.IAVertices = InputAssemblerVertices;
		native.IAPrimitives = InputAssemblerPrimitives;
		native.VSInvocations = VertexShaderInvocations;
		native.GSInvocations = GeometryShaderInvocations;
		native.GSPrimitives = GeometryShaderPrimitives;
		native.CInvocations = RasterizedPrimitives;
		native.CPrimitives = RenderedPrimitives;
		native.PSInvocations = PixelShaderInvocations;
		native.HSInvocations = HullShaderInvocations;
		native.DSInvocations = DomainShaderInvocations;
		native.CSInvocations = ComputeShaderInvocations;

		return native;
	}

	bool PipelineStatistics::operator == ( PipelineStatistics left, PipelineStatistics right )
	{
		return PipelineStatistics::Equals( left, right );
	}

	bool PipelineStatistics::operator != ( PipelineStatistics left, PipelineStatistics right )
	{
		return !PipelineStatistics::Equals( left, right );
	}

	int PipelineStatistics::GetHashCode()
	{
		return InputAssemblerVertices.GetHashCode() + InputAssemblerPrimitives.GetHashCode() + VertexShaderInvocations.GetHashCode()
			 + GeometryShaderInvocations.GetHashCode() + GeometryShaderPrimitives.GetHashCode() + RasterizedPrimitives.GetHashCode() +
			 RenderedPrimitives.GetHashCode() + PixelShaderInvocations.GetHashCode() + DomainShaderInvocations.GetHashCode() +
			 HullShaderInvocations.GetHashCode() + ComputeShaderInvocations.GetHashCode();
	}

	bool PipelineStatistics::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<PipelineStatistics>( value ) );
	}

	bool PipelineStatistics::Equals( PipelineStatistics value )
	{
		return ( InputAssemblerVertices == value.InputAssemblerVertices && InputAssemblerPrimitives == value.InputAssemblerPrimitives && VertexShaderInvocations == value.VertexShaderInvocations
			 && GeometryShaderInvocations == value.GeometryShaderInvocations && GeometryShaderPrimitives == value.GeometryShaderPrimitives &&
			 RasterizedPrimitives == value.RasterizedPrimitives && RenderedPrimitives == value.RenderedPrimitives &&
			 PixelShaderInvocations == value.PixelShaderInvocations && DomainShaderInvocations == value.DomainShaderInvocations &&
			 HullShaderInvocations == value.HullShaderInvocations && ComputeShaderInvocations == value.ComputeShaderInvocations );
	}

	bool PipelineStatistics::Equals( PipelineStatistics% value1, PipelineStatistics% value2 )
	{
		return ( value1.InputAssemblerVertices == value2.InputAssemblerVertices && value1.InputAssemblerPrimitives == value2.InputAssemblerPrimitives && value1.VertexShaderInvocations == value2.VertexShaderInvocations
			 && value1.GeometryShaderInvocations == value2.GeometryShaderInvocations && value1.GeometryShaderPrimitives == value2.GeometryShaderPrimitives &&
			 value1.RasterizedPrimitives == value2.RasterizedPrimitives && value1.RenderedPrimitives == value2.RenderedPrimitives &&
			 value1.PixelShaderInvocations == value2.PixelShaderInvocations && value1.DomainShaderInvocations == value2.DomainShaderInvocations &&
			 value1.HullShaderInvocations == value2.HullShaderInvocations && value1.ComputeShaderInvocations == value2.ComputeShaderInvocations );
	}
}
}
