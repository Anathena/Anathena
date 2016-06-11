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

#include "../DataStream.h"

#include "Direct3D11Exception.h"

#include "Buffer11.h"
#include "BufferDescription11.h"
#include "CommandList11.h"
#include "Asynchronous11.h"
#include "DeviceContext11.h"
#include "InputAssemblerWrapper11.h"
#include "OutputMergerWrapper11.h"
#include "StreamOutputWrapper11.h"
#include "RasterizerWrapper11.h"
#include "VertexShaderWrapper11.h"
#include "PixelShaderWrapper11.h"
#include "GeometryShaderWrapper11.h"
#include "DomainShaderWrapper11.h"
#include "HullShaderWrapper11.h"
#include "ComputeShaderWrapper11.h"
#include "DepthStencilView11.h"
#include "RenderTargetView11.h"
#include "UnorderedAccessView11.h"
#include "ShaderResourceView11.h"
#include "Resource11.h"
#include "ResourceRegion11.h"
#include "Predicate11.h"
#include "Texture1D11.h"
#include "Texture1DDescription11.h"
#include "Texture2D11.h"
#include "Texture2DDescription11.h"
#include "Texture3D11.h"
#include "Texture3DDescription11.h"
#include "Device11.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D11
{
	void DeviceContext::InitializeSubclasses()
	{
		inputAssembler = gcnew InputAssemblerWrapper( InternalPointer );
		outputMerger = gcnew OutputMergerWrapper( InternalPointer );
		streamOutput = gcnew StreamOutputWrapper( InternalPointer );
		rasterizer = gcnew RasterizerWrapper( InternalPointer );
		vertexShader = gcnew VertexShaderWrapper( InternalPointer );
		pixelShader = gcnew PixelShaderWrapper( InternalPointer );
		geometryShader = gcnew GeometryShaderWrapper( InternalPointer );
		domainShader = gcnew DomainShaderWrapper( InternalPointer );
		hullShader = gcnew HullShaderWrapper( InternalPointer );
		computeShader = gcnew ComputeShaderWrapper( InternalPointer );
	}

	DeviceContext::DeviceContext( ID3D11DeviceContext* pointer, ComObject^ owner )
	{
		Construct( pointer, owner );
		InitializeSubclasses();
	}
	
	DeviceContext::DeviceContext( IntPtr pointer )
	{
		Construct( pointer, NativeInterface );
		InitializeSubclasses();
	}
	
	DeviceContext^ DeviceContext::FromPointer( ID3D11DeviceContext* pointer, ComObject^ owner, ComObjectFlags flags )
	{
		return ConstructFromPointer<DeviceContext,ID3D11DeviceContext>( pointer, owner, flags );
	}
	
	DeviceContext^ DeviceContext::FromPointer( IntPtr pointer )
	{
		return ConstructFromUserPointer<DeviceContext>( pointer );
	}

	DeviceContext::DeviceContext( SlimDX::Direct3D11::Device^ device )
	{
		ID3D11DeviceContext* context;

		HRESULT hr = device->InternalPointer->CreateDeferredContext( 0, &context );
		if( RECORD_D3D11( hr ).IsFailure )
			throw gcnew Direct3D11Exception( Result::Last );

		Construct( context );
		InitializeSubclasses();
	}

	void DeviceContext::Begin( Asynchronous^ data )
	{
		InternalPointer->Begin( data->InternalPointer );
	}

	void DeviceContext::ClearDepthStencilView( DepthStencilView^ view, DepthStencilClearFlags flags, float depth, Byte stencil )
	{
		InternalPointer->ClearDepthStencilView( view->InternalPointer, static_cast<UINT>( flags ), depth, stencil );
	}
	
	void DeviceContext::ClearRenderTargetView( RenderTargetView^ view, Color4 color )
	{
		const float nativeColor[] = { color.Red, color.Green, color.Blue, color.Alpha };
		InternalPointer->ClearRenderTargetView( view->InternalPointer, nativeColor );
	}
	
	void DeviceContext::ClearState()
	{
		InternalPointer->ClearState();
	}

	void DeviceContext::ClearUnorderedAccessView( UnorderedAccessView^ unorderedAccessView, array<int>^ values )
	{
		if( values->Length != 4 )
			throw gcnew ArgumentException( "Must supply exactly four clear values", "values" );

		const UINT nativeValues[] = { values[0], values[1], values[2], values[3] };
		InternalPointer->ClearUnorderedAccessViewUint( unorderedAccessView->InternalPointer, nativeValues );
	}

	void DeviceContext::ClearUnorderedAccessView( UnorderedAccessView^ unorderedAccessView, array<float>^ values )
	{
		if( values->Length != 4 )
			throw gcnew ArgumentException( "Must supply exactly four clear values", "values" );

		const FLOAT nativeValues[] = { values[0], values[1], values[2], values[3] };
		InternalPointer->ClearUnorderedAccessViewFloat( unorderedAccessView->InternalPointer, nativeValues );
	}
	
	void DeviceContext::CopyResource( Resource^ source, Resource^ destination )
	{
		InternalPointer->CopyResource( destination->InternalPointer, source->InternalPointer );
	}

	void DeviceContext::CopyStructureCount( UnorderedAccessView^ unorderedAccessView, Buffer^ destinationBuffer, int destinationOffset )
	{
		InternalPointer->CopyStructureCount( destinationBuffer->InternalPointer, destinationOffset, unorderedAccessView->InternalPointer );
	}
	
	void DeviceContext::CopySubresourceRegion( Resource^ source, int sourceSubresource, ResourceRegion region, Resource^ destination, int destinationSubresource, int x, int y, int z )
	{
		D3D11_BOX nativeRegion = region.CreateNativeVersion();
		InternalPointer->CopySubresourceRegion( destination->InternalPointer, destinationSubresource, x, y, z, source->InternalPointer, sourceSubresource, &nativeRegion );
	}
	
	void DeviceContext::CopySubresourceRegion( Resource^ source, int sourceSubresource, Resource^ destination, int destinationSubresource, int x, int y, int z )
	{
		InternalPointer->CopySubresourceRegion( destination->InternalPointer, destinationSubresource, x, y, z, source->InternalPointer, sourceSubresource, NULL );
	}
	
	void DeviceContext::ResolveSubresource( Resource^ source, int sourceSubresource, Resource^ destination, int destinationSubresource, DXGI::Format format )
	{
		InternalPointer->ResolveSubresource( destination->InternalPointer, destinationSubresource, source->InternalPointer, sourceSubresource, static_cast<DXGI_FORMAT>( format ) );
	}
	
	void DeviceContext::UpdateSubresource( DataBox^ source, Resource^ resource, int subresource ) 
	{
		InternalPointer->UpdateSubresource( resource->InternalPointer, static_cast<UINT>( subresource), 0, source->Data->PositionPointer, source->RowPitch,source->SlicePitch);
	}

	void DeviceContext::UpdateSubresource( DataBox^ source, Resource^ resource, int subresource, ResourceRegion region ) 
	{
		D3D11_BOX nativeRegion = region.CreateNativeVersion();
		InternalPointer->UpdateSubresource( resource->InternalPointer, static_cast<UINT>( subresource), &nativeRegion, source->Data->PositionPointer, source->RowPitch,source->SlicePitch);
	}

	void DeviceContext::Dispatch( int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ )
	{
		InternalPointer->Dispatch( threadGroupCountX, threadGroupCountY, threadGroupCountZ );
	}
	
	void DeviceContext::DispatchIndirect( Buffer^ arguments, int alignedOffset )
	{
		InternalPointer->DispatchIndirect( arguments->InternalPointer, alignedOffset );
	}

	void DeviceContext::Draw( int vertexCount, int startVertexLocation )
	{
		InternalPointer->Draw( vertexCount, startVertexLocation );
	}
	
	void DeviceContext::DrawInstanced( int vertexCountPerInstance, int instanceCount, int startVertexLocation, int startInstanceLocation )
	{
		InternalPointer->DrawInstanced( vertexCountPerInstance, instanceCount, startVertexLocation, startInstanceLocation );
	}

	void DeviceContext::DrawInstancedIndirect( Buffer^ arguments, int alignedOffset )
	{
		InternalPointer->DrawInstancedIndirect( arguments->InternalPointer, alignedOffset );
	}
	
	void DeviceContext::DrawIndexed( int indexCount, int startIndexLocation, int baseVertexLocation )
	{
		InternalPointer->DrawIndexed( indexCount, startIndexLocation, baseVertexLocation );
	}
	
	void DeviceContext::DrawIndexedInstanced( int indexCountPerInstance, int instanceCount, int startIndexLocation, int baseVertexLocation, int startInstanceLocation )
	{
		InternalPointer->DrawIndexedInstanced( indexCountPerInstance, instanceCount, startIndexLocation, baseVertexLocation, startInstanceLocation );
	}

	void DeviceContext::DrawIndexedInstancedIndirect( Buffer^ arguments, int alignedOffset )
	{
		InternalPointer->DrawIndexedInstancedIndirect( arguments->InternalPointer, alignedOffset );
	}
	
	void DeviceContext::DrawAuto()
	{
		InternalPointer->DrawAuto();
	}

	void DeviceContext::End( Asynchronous^ data )
	{
		InternalPointer->End( data->InternalPointer );
	}

	void DeviceContext::ExecuteCommandList( CommandList^ commands, bool restoreState )
	{
		InternalPointer->ExecuteCommandList( commands->InternalPointer, restoreState );
	}

	CommandList^ DeviceContext::FinishCommandList( bool restoreState )
	{
		ID3D11CommandList* commands;

		HRESULT hr = InternalPointer->FinishCommandList( restoreState, &commands );
		if( RECORD_D3D11( hr ).IsFailure )
			return nullptr;

		return CommandList::FromPointer( commands );
	}
	
	void DeviceContext::Flush()
	{
		InternalPointer->Flush();
	}
	
	void DeviceContext::GenerateMips( ShaderResourceView^ view )
	{
		InternalPointer->GenerateMips( view->InternalPointer );
	}

	float DeviceContext::GetMinimumLod( Resource^ resource )
	{
		return InternalPointer->GetResourceMinLOD( resource->InternalPointer );
	}

	void DeviceContext::SetMinimumLod( Resource^ resource, float minimumLod )
	{
		InternalPointer->SetResourceMinLOD( resource->InternalPointer, minimumLod );
	}
	
	bool DeviceContext::IsDataAvailable( Asynchronous^ data )
	{
		return IsDataAvailable( data, AsynchronousFlags::None );
	}
	
	bool DeviceContext::IsDataAvailable( Asynchronous^ data, AsynchronousFlags flags )
	{
		return InternalPointer->GetData( data->InternalPointer, 0, 0, static_cast<UINT>( flags ) ) == S_OK;
	}

	DataStream^ DeviceContext::GetData( Asynchronous^ data )
	{
		return GetData( data, AsynchronousFlags::None );
	}
	
	DataStream^ DeviceContext::GetData( Asynchronous^ data, AsynchronousFlags flags )
	{
		int size = data->InternalPointer->GetDataSize();
		DataStream^ result = gcnew DataStream( size, true, true );

		if( RECORD_D3D11( InternalPointer->GetData( data->InternalPointer, result->PositionPointer, size, static_cast<UINT>( flags ) ) ).IsFailure )
			return nullptr;
		return result;
	}

// disables bogus warning: 
// SlimDX::Direct3D11::DeviceContext::GetData<T>' : recursive on
// all control paths, function will cause runtime stack overflow
#pragma warning(disable:4717)
	generic<typename T> where T : value class
	T DeviceContext::GetData( Asynchronous^ data )
	{
		return GetData<T>( data, AsynchronousFlags::None );
	}

	generic<typename T> where T : value class
	T DeviceContext::GetData( Asynchronous^ data, AsynchronousFlags flags )
	{
		int size = Marshal::SizeOf(T::typeid);
		T result;

		if( RECORD_D3D11( InternalPointer->GetData( data->InternalPointer, &result, size, static_cast<UINT>( flags ) ) ).IsFailure )
			return T();

		return result;
	}
#pragma warning(default:4717)

	void DeviceContext::GetPredication( [Out] Predicate^ %predicate, bool %predicateValue )
	{
		ID3D11Predicate* pointer;
		BOOL value;

		InternalPointer->GetPredication( &pointer, &value );

		predicate = Predicate::FromPointer( pointer );
		predicateValue = value > 0;
	}

	void DeviceContext::SetPredication( Predicate^ predicate, bool predicateValue )
	{
		InternalPointer->SetPredication( predicate->InternalPointer, predicateValue );
	}

	DataBox^ DeviceContext::MapSubresource(Texture1D^ resource, int mipSlice, int arraySlice, MapMode mode, MapFlags flags)
	{
		Texture1DDescription desc = resource->Description;
		int sizeInBytes = Resource::GetMipSize(mipSlice, desc.Width) * Utilities::SizeOfFormatElement(static_cast<DXGI_FORMAT>(desc.Format)) / 8;
		int subresource = D3D11CalcSubresource(mipSlice, arraySlice, desc.MipLevels);

		D3D11_MAPPED_SUBRESOURCE mapped;
		HRESULT hr = InternalPointer->Map(resource->InternalPointer, subresource, static_cast<D3D11_MAP>(mode), static_cast<UINT>(flags), &mapped);
		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		return gcnew DataBox(mapped.RowPitch, mapped.DepthPitch, gcnew DataStream(mapped.pData, sizeInBytes, true, true, false));
	}

	DataBox^ DeviceContext::MapSubresource(Texture2D^ resource, int mipSlice, int arraySlice, MapMode mode, MapFlags flags)
	{
		Texture2DDescription desc = resource->Description;
		int mipHeight = Resource::GetMipSize(mipSlice, desc.Height);
		int subresource = D3D11CalcSubresource(mipSlice, arraySlice, desc.MipLevels);

		D3D11_MAPPED_SUBRESOURCE mapped;
		HRESULT hr = InternalPointer->Map(resource->InternalPointer, subresource, static_cast<D3D11_MAP>(mode), static_cast<UINT>(flags), &mapped);
		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		int sizeInBytes = mipHeight * mapped.RowPitch;
		if (Utilities::IsCompressed(static_cast<DXGI_FORMAT>(desc.Format)))
			sizeInBytes /= 4;

		return gcnew DataBox(mapped.RowPitch, mapped.DepthPitch, gcnew DataStream(mapped.pData, sizeInBytes, true, true, false));
	}

	DataBox^ DeviceContext::MapSubresource(Texture3D^ resource, int mipSlice, int arraySlice, MapMode mode, MapFlags flags)
	{
		Texture3DDescription desc = resource->Description;
		int mipDepth = Resource::GetMipSize(mipSlice, desc.Depth);
		int subresource = D3D11CalcSubresource(mipSlice, arraySlice, desc.MipLevels);

		D3D11_MAPPED_SUBRESOURCE mapped;
		HRESULT hr = InternalPointer->Map(resource->InternalPointer, subresource, static_cast<D3D11_MAP>(mode), static_cast<UINT>(flags), &mapped);
		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		return gcnew DataBox(mapped.RowPitch, mapped.DepthPitch, gcnew DataStream(mapped.pData, mipDepth * mapped.DepthPitch, true, true, false));
	}

	DataBox^ DeviceContext::MapSubresource(Buffer^ resource, MapMode mode, MapFlags flags)
	{
		D3D11_MAPPED_SUBRESOURCE mapped;
		HRESULT hr = InternalPointer->Map(resource->InternalPointer, 0, static_cast<D3D11_MAP>(mode), static_cast<UINT>(flags), &mapped);
		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		return gcnew DataBox(mapped.RowPitch, mapped.DepthPitch, gcnew DataStream(mapped.pData, resource->Description.SizeInBytes, true, true, false));
	}

	DataBox^ DeviceContext::MapSubresource( Resource^ resource, int subresource, MapMode mode, MapFlags flags )
	{
		switch (resource->Dimension)
		{
		case ResourceDimension::Buffer:
			return MapSubresource(safe_cast<Buffer^>(resource), mode, flags);

		case ResourceDimension::Texture1D:
		{
			Texture1D^ texture = safe_cast<Texture1D^>(resource);
			int mipLevels = texture->Description.MipLevels;
			int arraySlice = subresource / mipLevels;
			int mipSlice = subresource % mipLevels;
			return MapSubresource(texture, mipSlice, arraySlice, mode, flags);
		}

		case ResourceDimension::Texture2D:
		{
			Texture2D^ texture = safe_cast<Texture2D^>(resource);
			int mipLevels = texture->Description.MipLevels;
			int arraySlice = subresource / mipLevels;
			int mipSlice = subresource % mipLevels;
			return MapSubresource(texture, mipSlice, arraySlice, mode, flags);
		}

		case ResourceDimension::Texture3D:
		{
			Texture3D^ texture = safe_cast<Texture3D^>(resource);
			int mipLevels = texture->Description.MipLevels;
			int arraySlice = subresource / mipLevels;
			int mipSlice = subresource % mipLevels;
			return MapSubresource(texture, mipSlice, arraySlice, mode, flags);
		}

		default:
			throw gcnew InvalidOperationException("Cannot Map unknown resource type");
		}
	}

	void DeviceContext::UnmapSubresource( Resource^ resource, int subresource )
	{
		InternalPointer->Unmap( resource->InternalPointer, subresource );
	}

	DeviceContextType DeviceContext::Type::get()
	{
		return static_cast<DeviceContextType>( InternalPointer->GetType() );
	}

	InputAssemblerWrapper^ DeviceContext::InputAssembler::get()
	{
		return inputAssembler;
	}
	
	OutputMergerWrapper^ DeviceContext::OutputMerger::get()
	{
		return outputMerger;
	}
	
	StreamOutputWrapper^ DeviceContext::StreamOutput::get()
	{
		return streamOutput;
	}
	
	RasterizerWrapper^ DeviceContext::Rasterizer::get()
	{
		return rasterizer;
	}

	VertexShaderWrapper^ DeviceContext::VertexShader::get()
	{
		return vertexShader;
	}

	PixelShaderWrapper^ DeviceContext::PixelShader::get()
	{
		return pixelShader;
	}

	GeometryShaderWrapper^ DeviceContext::GeometryShader::get()
	{
		return geometryShader;
	}

	DomainShaderWrapper^ DeviceContext::DomainShader::get()
	{
		return domainShader;
	}

	HullShaderWrapper^ DeviceContext::HullShader::get()
	{
		return hullShader;
	}

	ComputeShaderWrapper^ DeviceContext::ComputeShader::get()
	{
		return computeShader;
	}
}
}